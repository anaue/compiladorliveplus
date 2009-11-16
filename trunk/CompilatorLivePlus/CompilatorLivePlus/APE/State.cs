using System;
using System.Collections.Generic;
using System.Text;
using CompilatorLivePlus.Lexer;

namespace CompilatorLivePlus.APE
{
    public class State
    {
        public int Id;
        public List<Transition> Transitions;
        public bool finalState;

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
