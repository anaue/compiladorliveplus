using System;
using System.Collections.Generic;
using System.Text;
using APE.Lexer;

namespace APE.Model
{
    public class StackAutomaton
    {
        public List<Automaton> Automata;
        public Automaton Start;

        public StackAutomaton()
        {
            Automata = new List<Automaton>();
        }


        public StackAutomaton(Automaton startAutomaton)
        {
            Automata = new List<Automaton>();
            Automata.Add(startAutomaton);
            Start = startAutomaton;
        }

        public void addAutomaton(Automaton automaton)
        {
            Automata.Add(automaton);
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            foreach (Automaton at in Automata)
            {
                str.AppendLine("Automato: " + at.Name);
                str.AppendLine("\nEstado Inicial: " + at.Start.Id);
                foreach (State st in at.States)
                {
                    str.AppendLine("\tEstado " + st.Id);
                    str.AppendLine("\t\tFinal: " + st.FinalState + "\n\t\tTransicoes:\n");
                    foreach (Transition tr in st.Transitions)
                    {
                        if (tr.GetType() != typeof(SubmachineCall))
                        {
                            string tagName = Enum.GetName(typeof(Tag), Enum.Parse(typeof(Tag), tr.Input.tag.ToString()));
                            str.Append("\t\t\t(" + st.Id + ", " + (tagName == null? '\'' + Convert.ToString((char)tr.Input.tag) + '\'': tagName));
                            str.Append(") -> " + tr.NextState.Id + " \n");
                        }
                        else
                        {
                            SubmachineCall sc = (SubmachineCall)tr;
                            str.Append("\t\t\t(" + sc.CalledAutomaton.Name);
                            str.Append("; " + sc.BackState.Id + ")\n");
                        }
                    }
                    str.Append("\n");
                }
            }
            return str.ToString();
        }

    }
}
