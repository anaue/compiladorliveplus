using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using APE.Parser;
using APE.Model;

namespace APE
{
    class Program
    {
        static void Main(string[] args)
        {
            APEParser parser = new APEParser("automato_expressao_booleana.txt");
            StackAutomaton automaton = parser.GetAutomaton();
            Console.Write(automaton.ToString());

            Console.ReadLine();
        }
    }
}
