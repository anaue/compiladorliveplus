using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompilerModel.APE;
using CompilerModel.Lexer;
using CompilerModel.Structures;

namespace APE
{
    public class Recognizer
    {
        private StackAutomaton _ape;
        public State CurrentState;
        public Automaton CurrentAutomaton;
        private Stack _stack;

        public Recognizer(StackAutomaton ape)
        {
            _ape = ape;
            _stack = new Stack();
            CurrentAutomaton = _ape.Automata.Find(In=>In.Name == ape.Start.Name);
            CurrentState = CurrentAutomaton.States.Find(In => In.Id == CurrentAutomaton.Start.Id);
        }

        public bool RunTransition(Token input)
        {
            //Preferencialmente, procura-se transicoes internas
            List<Transition> internalTransitions = CurrentState.Transitions.FindAll(In => In.GetType() != typeof(SubmachineCall));
            foreach (Transition tr in internalTransitions)
            {
                if (tr.Input.tag == input.tag)
                {
                    CurrentState = CurrentAutomaton.States.Find(In => In.Id == tr.NextState.Id);
                    if (CurrentState.FinalState && !_stack.Empty)
                    {
                        StackPair destination = (StackPair)_stack.Pop();
                        CurrentAutomaton = destination.Automaton;
                        CurrentState = destination.State;
                    }
                    return true;
                }
            }

            List<Transition> listSubmachineCall = CurrentState.Transitions.FindAll(In => In.GetType() == typeof(SubmachineCall));
            if (listSubmachineCall.Count > 0)
            {
                if (listSubmachineCall.Count == 1)
                {
                    SubmachineCall call = ((SubmachineCall)listSubmachineCall[0]);

                    _stack.Push(new StackPair(CurrentAutomaton, call.NextState));
                    GoToSubmachine(call.CalledAutomaton);
                    RunTransition(input);
                    return true;
                }
                else
                //Ha' nao-determinismo pois ha mais de uma chamada de submaquina para esse estado. Fazer lookahead
                {
                    //Olha o FOLLOW
                    foreach (Transition tr in listSubmachineCall)
                    {
                        SubmachineCall sc = ((SubmachineCall)tr);
                        //Devo verificar se o proximo token condiz com uma das submaquinas.
                        if (sc.CalledAutomaton.Start.HasTranstionsForToken(input))
                        {
                            _stack.Push(new StackPair(CurrentAutomaton, sc.NextState));
                            GoToSubmachine(sc.CalledAutomaton);
                            RunTransition(input);
                            return true;
                        }
                    }
                }
            }

            if (CurrentState.FinalState)
            {
                if (!_stack.Empty)
                {
                    StackPair stackPair = (StackPair)_stack.Pop();
                    GoToSubmachine(stackPair.Automaton, stackPair.State);
                }
                return true;
            }
            return false;
            
        }

        private void GoToSubmachine(Automaton automaton, State state)
        {
            CurrentAutomaton = automaton;
            CurrentState = state;
        }

        private void GoToSubmachine(Automaton automaton)
        {
            CurrentAutomaton = automaton;
            CurrentState = CurrentAutomaton.Start;
        }

        public bool Recognize(Token[] chain)
        {
            int i=0;
            bool error = false;
            //while (!(CurrentState.FinalState && i < chain.Length && !StateHasTransitionsForToken(CurrentState, chain[i])))
            while (!(CurrentState.FinalState && _stack.Empty))
            {
                if (!RunTransition(chain[i]))
                {
                    error = true;
                    break;
                }
                i++;
            }

            if (error)
                throw new ApplicationException("Sintatic Error! Index " + i);
            else 
                return true;
        }

        private bool StateHasTransitionsForToken(State CurrentState, Token nextToken)
        {
            return CurrentState.Transitions.Exists(In => In.Input.Equals(nextToken));
        }

    }
}
