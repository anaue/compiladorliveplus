using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using APE.Parser;
using APE.Model;
using APE.Lexer;

namespace APE
{
    class Program
    {
        static void Main(string[] args)
        {
            APEParser parser = new APEParser("automato_expressao_booleana.txt");
            StackAutomaton automaton = parser.GetAutomaton();
            Console.Write(automaton.ToString());

            Recognizer recognizer = new Recognizer(automaton);
            Token[] chain = new Token[] {new Token("OR"), new Token("OR"), new Token("AND")};
            
            Console.WriteLine("Accept: " + recognizer.Recognize(chain));

            Console.ReadLine();
        }
    }
}
