using System;
using System.Collections.Generic;
using System.Text;
using CompilerModel.Lexer;

namespace CompilerModel.APE
{
    public class Automaton
    {
        public string Name;
        public List<State> States;
        public State Start;

        public Automaton(string name)
        {
            Name = name;
        }

        public Automaton(string name, State startState)
        {
            Name = name;
            States = new List<State>();
            States.Add(startState);
            Start = startState;
        }

        public void addState(State state)
        {
            if (!States.Exists(a => a.Id == state.Id))
                States.Add(state);
            else
            {
                updateState(state.Id, state.Transitions);
            }
        }

        public void updateState(int id, List<Transition> transitions)
        {
            State temp = States.Find(a => a.Id == id);
            temp.Transitions.AddRange(transitions);
        }

    }
}
