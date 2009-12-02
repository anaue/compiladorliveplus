using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompilerModel.APE
{
    public class StackPair
    {
        #region Members
        
        private Automaton _automaton;
        private State _state;
        
        #endregion Members

        #region Properties

        public State State
        {
            get { return _state; }
            set { _state = value; }
        }

        public Automaton Automaton
        {
            get { return _automaton; }
            set { _automaton = value; }
        }

        #endregion Properties


        public StackPair(Automaton automaton, State state)
        {
            _automaton = automaton;
            _state = state;
        }
    }
}
