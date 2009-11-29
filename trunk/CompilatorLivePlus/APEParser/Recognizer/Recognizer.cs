using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompilerModel.APE;
using CompilerModel.Lexer;
using CompilerModel.Symbols;
using CompilerModel.Structures;
using System.Reflection;
using CompilerModel.Semantic;

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

        public bool RunTransition(Token input, Token nextToken)
        {
            if (input == null)
                return false;
            //Preferencialmente, procura-se transicoes internas
            List<Transition> internalTransitions = CurrentState.Transitions.FindAll(In => In.GetType() != typeof(SubmachineCall));
            foreach (Transition tr in internalTransitions)
            {
                if (tr.Input.tag == input.tag)
                {
                    CurrentState = CurrentAutomaton.States.Find(In => In.Id == tr.NextState.Id);
                    
                    //Chamada da acao semantica
                    RunSemanticAction(tr.SemanticActionName);

                    if (CurrentState.FinalState && !CheckLookAhead(CurrentState, nextToken))
                    {
                        if (!_stack.Empty)
                        {
                            StackPair stackPair = (StackPair)_stack.Pop();
                            GoToSubmachine(stackPair.Automaton, stackPair.State);
                            //RunTransition(input, nextToken);
                        }
                        return true;
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
                    RunTransition(input, nextToken);
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
                            RunTransition(input, nextToken);
                            return true;
                        }
                    }
                }
            }

            if (CurrentState.FinalState && !CheckLookAhead(CurrentState, input))
            {
                if (!_stack.Empty)
                {
                    StackPair stackPair = (StackPair)_stack.Pop();
                    GoToSubmachine(stackPair.Automaton, stackPair.State);
                    RunTransition(input, nextToken);
                }
                return true;
            }
            return false;
        }

        private static void RunSemanticAction(String semanticActionName)
        {
            SemanticActions sa = new SemanticActions();
            MethodInfo methodInfo = typeof(SemanticActions).GetMethod(semanticActionName);
            // Use the instance to call the method without arguments
            methodInfo.Invoke(sa, null);

            CompilerModel.Trace.Tracer.putLog("Called Method: " + semanticActionName, MethodInfo.GetCurrentMethod().ReflectedType.ToString());

        }

        private bool CheckLookAhead(State CurrentState, Token nextToken)
        {
            if (nextToken != null)
                return CurrentState.Transitions.Exists(In => In.Input.Equals(nextToken));
            else
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

        public bool Recognize(Input chain)
        {
            int i=0;
            bool error = false;
            //while (!(CurrentState.FinalState && i < chain.Length && !StateHasTransitionsForToken(CurrentState, chain[i])))
            while (!(CurrentState.FinalState && _stack.Empty))
            {
                //if (!RunTransition(chain[i], i < chain.Length - 1 ? chain[i + 1] : null))
                if (!RunTransition(chain.getNext(), chain.getLookAHead()))
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
