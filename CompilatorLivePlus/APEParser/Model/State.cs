using System;
using System.Collections.Generic;
using System.Text;
using APE.Lexer;

namespace APE.Model
{
    public class State
    {
        public int Id;
        public List<Transition> Transitions = new List<Transition>();
        public bool FinalState;

        public State(int id)
        {
            Id = id;
            FinalState = false;
        }

        public State(int id, bool isFinal)
        {
            Id = id;
            FinalState = isFinal;
        }

        public void addTransition(Transition transition)
        {
            Transitions.Add(transition);
        }

        public void setAsFinalState()
        {
            FinalState = true;
        }

        public State getNextState(Token token)
        {
            foreach (Transition item in Transitions)
            {
                if (item.Input.tag == token.tag)
                    return item.NextState;
            }
            return null;
        }

    }
}
