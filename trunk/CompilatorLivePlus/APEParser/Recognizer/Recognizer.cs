using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using APE.Model;
using APE.Lexer;

namespace APE
{
    public class Recognizer
    {
        private StackAutomaton _ape;
        public State CurrentState;
        public Automaton CurrentAutomaton;

        public Recognizer(StackAutomaton ape)
        {
            _ape = ape;
            CurrentAutomaton = _ape.Automata.Find(In=>In.Name == ape.Start.Name);
            CurrentState = CurrentAutomaton.States.Find(In => In.Id == CurrentAutomaton.Start.Id);
        }

        public bool RunTransition(Token input)
        {
            foreach (Transition tr in CurrentState.Transitions)
            {
                if (typeof(SubmachineCall) == tr.GetType())
                {

                }
                else
                {
                    if (tr.Input.tag == input.tag)
                    {
                        CurrentState = CurrentAutomaton.States.Find(In => In.Id == tr.NextState.Id);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool Recognize(Token[] chain)
        {
            int i=0;
            bool error = false;
            while (!CurrentState.FinalState || (i !=0 && i<chain.Length && chain[i].Equals(new Token(";"))))
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

    }
}
