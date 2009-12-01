using System;
using System.Collections.Generic;
using System.Text;
using APE.Lexer;


namespace APE.Model
{
    public class Transition
    {
        public Token Input;
        public State NextState;
        public Delegate SemanticAction;

        public Transition()
        {

        }

        public Transition(Token input, State nextState)
        {
            NextState = nextState;
            Input = input;
        }

    }
}
