using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using APE.Parser;
using CompilerModel.APE;
using CompilerModel.Lexer;

namespace APE
{
    class Program
    {
        static void Main(string[] args)
        {
            APEParser parser = new APEParser();
            StackAutomaton automaton = parser.GetStackAutomaton();
            Console.Write(automaton.ToString());
            Console.ReadLine();
            Recognizer recognizer = new Recognizer(automaton, "test.txt");
            //{new Token("IF"), new Token("("), new Token("TRUE"), new Token(")"), new Token("BEGIN"), new Token};
            Token[] chain = new Token[] {new Token("IF"), new Token("NUM"), new Token("<"),new Token("ID"), new Token("then"),
                new Token("ID"),new Token("="),new Token("ID"),new Token("-"),new Token("NUM"),new Token(";"),new Token("ID"),
                new Token("="),new Token("ID"),new Token("*"),new Token("NUM"),new Token(";"),new Token("ENDIF"), new Token(";")
            };

            //chain = new Token[] {new Token("ID"),new Token("="),new Token("ID"),new Token("-"),new Token("NUM"), new Token(";")};

            //Console.WriteLine("Accept: " + recognizer.Recognize(chain));

            Console.ReadLine();

            System.Diagnostics.ProcessStartInfo procInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c" + "java -jar mvn\\montador.jar Output\\mvn.txt");
            procInfo.RedirectStandardOutput = true;
            procInfo.UseShellExecute = false;
            procInfo.CreateNoWindow = true;

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo = procInfo;
            process.Start();
            Console.ReadLine();
        }

    }
}
