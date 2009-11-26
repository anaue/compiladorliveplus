using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CompilerModel.APE;
using CompilerModel.Lexer;

namespace APE.Parser
{
    public class APEParser
    {
        private StreamReader _text;
        public static StackAutomaton stackAutomaton;


        public APEParser()
        {
        }

        public StackAutomaton ParseFromRoot(string root)
        {
            root = root.ToUpper();
            stackAutomaton = new StackAutomaton();
            stackAutomaton.addAutomaton(GetAutomaton(root, new List<Automaton>()));
            stackAutomaton.Start = stackAutomaton.Automata.Find(In => In.Name == root);
            return stackAutomaton;
        }

        public StackAutomaton GetStackAutomaton()
        {
            if (stackAutomaton != null)
                return stackAutomaton;
            else
            {
                return ParseFromRoot("comando");
            }
        }

        public Automaton GetAutomaton(string AName, List<Automaton> parentAutomata)
        {
            AName = AName.ToUpper();
            if (stackAutomaton.Automata.Count != 0 && stackAutomaton.Automata.Exists(In => In.Name == AName))
                return stackAutomaton.Automata.Find(In => In.Name == AName);
            else if (parentAutomata.Exists(In => In.Name == AName))
            {
                Automaton pai = parentAutomata.Find(In => In.Name == AName);
                parentAutomata.Remove(pai);
                return pai;
            }
            else
            {
                string path = "GrammarDefinitions/";
                return ParseAutomaton(AName, path, parentAutomata);
            }
        }

        private Automaton ParseAutomaton(string AName, string path, List<Automaton> parentAutomata)
        {
            _text = File.OpenText(path + AName + ".txt");

            string name = TokenizeField("name:")[1].Trim().ToUpper();
            Automaton automaton = new Automaton(name, ParseInitialState());
            ParseFinalStates(automaton);
            ParseTransitions(automaton, parentAutomata);

            return automaton;
        }

        private void ParseTransitions(Automaton automaton, List<Automaton> parentAutomata)
        {
            string currentLine;

            while(!_text.EndOfStream){
                currentLine = _text.ReadLine().Trim().Replace("\",\"", "_COMMA").Replace("\"\"\"", "_QUOTE");
                if (!currentLine.Equals(String.Empty))
                {
                    State currentState;
                    State nextState;
                    string left = currentLine.Split(new char[] { '-', '>' }, StringSplitOptions.RemoveEmptyEntries)[0];
                    string nextStateNumber = currentLine.Split(new char[] { '-', '>' }, StringSplitOptions.RemoveEmptyEntries)[1];
                    int nextNum;
                    if (Int32.TryParse(nextStateNumber, out nextNum))
                    {
                        bool newState = false;
                        bool newNextState = false;
                        string[] pair = left.Trim().Trim('(',')').Split(new char[] {',',' '}, StringSplitOptions.RemoveEmptyEntries);
                        int currentStateNum;
                        if(Int32.TryParse(pair[0], out currentStateNum)){

                            if (automaton.States.Exists(In => In.Id == currentStateNum))
                                currentState = automaton.States.Find(In => In.Id == currentStateNum);
                            else
                            {
                                newState = true;
                                currentState = new State(currentStateNum);
                            }

                            if (automaton.States.Exists(In => In.Id == nextNum))
                                nextState = automaton.States.Find(In => In.Id == nextNum);
                            else
                            {
                                newNextState = true;
                                nextState = new State(nextNum);
                            }

                            
                            if (pair[1].Contains("_COMMA"))
                            {
                                Token token;
                                token = new Token(",");
                                currentState.addTransition(new Transition(token, nextState));
                            }
                            else if (pair[1].Contains("_QUOTE"))
                            {
                                Token token;
                                token = new Token("\"");
                                currentState.addTransition(new Transition(token, nextState));
                            }
                            else if (pair[1].Contains("\""))
                            {
                                Token token;
                                token = new Token(pair[1].Trim('"').ToUpper());
                                currentState.addTransition(new Transition(token, nextState));
                            }
                            else
                            {
                                SubmachineCall subMachineCall;
                                string name = pair[1].Trim('"').ToUpper();
                                if (name != automaton.Name)
                                {
                                    Automaton refAutomaton;
                                    parentAutomata.Add(automaton);
                                    refAutomaton = (new APEParser()).GetAutomaton(name, parentAutomata);
                                    subMachineCall = new SubmachineCall(nextState, refAutomaton);
                                    stackAutomaton.addAutomaton(refAutomaton);
                                }
                                else
                                    subMachineCall = new SubmachineCall(currentState, automaton);

                                currentState.addTransition(subMachineCall);
                            }

                            if(newState) automaton.addState(currentState);
                            if (newNextState) automaton.addState(nextState);
                        }
                    }

                } else
                    continue;
            }
        }

        private void ParseSubmachines()
        {

        }

        private void ParseFinalStates(Automaton automaton)
        {
            string[] finalStates = TokenizeField("final:");
            int finalStateNum;
            if (finalStates != null && finalStates.Length > 1)
            {
                for (int i = 1; i < finalStates.Length; i++)
                {
                    if (Int32.TryParse(finalStates[i], out finalStateNum))
                        automaton.addState(new State(finalStateNum, true));
                    else
                        throw new ApplicationException("Wrong format of APE source file: invalid values for field 'final'.");
                }
            }
            else
                throw new ApplicationException("Wrong format of APE source file: field 'final' not found.");
        }

        private State ParseInitialState()
        {
            State initial;
            string[] values = TokenizeField("initial:");

            int state_num;
            if (values != null && Int32.TryParse(values[1], out state_num))
            {
                initial = new State(Int32.Parse(values[1]));
            }
            else
                throw new ApplicationException("Wrong format of APE source file: field 'initial' not found.");
            
            return initial;
        }

        private string[] TokenizeField(string fieldName)
        {
            String currentLine;
            currentLine = String.Empty;
            while (!currentLine.ToLower().Contains(fieldName) && !_text.EndOfStream)
                    currentLine = _text.ReadLine().ToLower();

            if (!_text.EndOfStream)
                return currentLine.Trim().Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            else
                return null;
        }

    }
}
