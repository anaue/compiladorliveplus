using System;
using System.Collections.Generic;
using System.Text;
using CompilatorLivePlus.Lexer;

namespace CompilatorLivePlus.APE
{
    public class SubmachineCall: Transition
    {
        public State BackState;
        public Automaton CalledAutomata;
    }
}
