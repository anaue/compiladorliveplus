using System;
using System.Collections.Generic;
using System.Text;
using CompilerModel.Lexer;

namespace CompilerModel.APE
{
    public class SubmachineCall: Transition
    {
        public State BackState;
        public Automaton CalledAutomaton;

        public SubmachineCall(State backState, Automaton called)
        {
            this.BackState = backState;
            this.CalledAutomaton = called;
        }

    }
}
