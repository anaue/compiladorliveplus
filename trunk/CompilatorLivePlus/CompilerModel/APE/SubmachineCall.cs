using System;
using System.Collections.Generic;
using System.Text;
using CompilerModel.Lexer;

namespace CompilerModel.APE
{
    public class SubmachineCall: Transition
    {
        public Automaton CalledAutomaton;

        public SubmachineCall(State backState, Automaton called)
        {
            base.NextState = backState;
            this.CalledAutomaton = called;
        }

    }
}
