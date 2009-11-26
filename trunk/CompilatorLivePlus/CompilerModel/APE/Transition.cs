using System;
using System.Collections.Generic;
using System.Text;
using CompilerModel.Lexer;


namespace CompilerModel.APE
{
    public class Transition
    {
        public Token Input;
        public State NextState;
        public String SemanticActionName;
        

        public Transition()
        {
            
        }

        public Transition(Token input, State nextState, string semanticActionName)
        {
            NextState = nextState;
            Input = input;
            SemanticActionName = semanticActionName;
        }

    }
}
