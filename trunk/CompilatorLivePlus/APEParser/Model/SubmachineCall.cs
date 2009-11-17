using System;
using System.Collections.Generic;
using System.Text;
using APE.Lexer;

namespace APE.Model
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
