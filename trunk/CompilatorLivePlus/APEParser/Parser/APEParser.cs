using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using APE.Model;
using APE.Lexer;

namespace APE.Parser
{
    public class APEParser
    {
        private StreamReader _text;

        public APEParser(string sourceFilePath)
        {
            _text = File.OpenText(sourceFilePath);
        }

        public StackAutomaton GetAutomaton()
        {
            string name = TokenizeField("name:")[1].Trim().ToUpper();
            Automaton automaton = new Automaton(name, ParseInitialState());
            StackAutomaton ape = new StackAutomaton(automaton);
            ParseFinalStates(ape, name);
            ParseTransitions(ape, name);

            return new StackAutomaton(automaton);
        }

        private void ParseTransitions(StackAutomaton ape, string currentAutomatonName)
        {
            string currentLine;

            while(!_text.EndOfStream){
                currentLine = _text.ReadLine().Trim();
                if (!currentLine.Equals(String.Empty))
                {
                    State temp;
                    string left = currentLine.Split(new char[] { '-', '>' }, StringSplitOptions.RemoveEmptyEntries)[0];
                    string nextState = currentLine.Split(new char[] { '-', '>' }, StringSplitOptions.RemoveEmptyEntries)[1];
                    int nextNum;
                    if (Int32.TryParse(nextState, out nextNum))
                    {
                        string[] pair = left.Trim().Trim('(',')').Split(new char[] {',',' '}, StringSplitOptions.RemoveEmptyEntries);
                        int currentStateNum;
                        if(Int32.TryParse(pair[0], out currentStateNum)){
                            temp = new State(currentStateNum);
                            if (pair[1].Contains("\""))
                            {
                                Token token = new Token(pair[1].Trim('"').ToUpper());
                                temp.addTransition(new Transition(token, new State(nextNum)));
                            }
                            else
                            {
                                string name = pair[1].Trim('"').ToUpper();
                                Automaton refAutomaton = new Automaton(name);
                                SubmachineCall subMachineCall = new SubmachineCall(temp, refAutomaton);
                                ape.addAutomaton(refAutomaton);
                                temp.addTransition(subMachineCall);
                            }

                            ape.Automata.Find(In => In.Name == currentAutomatonName).addState(temp);
                        }
                    }

                } else
                    continue;
            }
        }

        private void ParseFinalStates(StackAutomaton ape, string currentAutomatonName)
        {
            string[] finalStates = TokenizeField("final:");
            int finalStateNum;
            if (finalStates != null && finalStates.Length > 1)
            {
                for (int i = 1; i < finalStates.Length; i++)
                {
                    if (Int32.TryParse(finalStates[1], out finalStateNum))
                        ape.Automata.Find(In => In.Name == currentAutomatonName).addState(new State(finalStateNum, true));
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
