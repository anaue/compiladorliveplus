using System;
using System.Collections.Generic;
using System.Text;
using CompilatorLivePlus.Lexer;

namespace CompilatorLivePlus.APE
{
    public class Transition
    {
        public Token Input;
        public State NextState;
        public Delegate SemanticAction;

    }
}
