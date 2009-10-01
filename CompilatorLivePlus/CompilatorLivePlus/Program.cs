using System;
using System.Text;

namespace CompilatorLivePlus
{
    class Program
    {
        public static string[] words;
        static void Main(string[] args)
        {
#if DEBUG 
            CompilatorLivePlus.Lexer.Lexer lex = new CompilatorLivePlus.Lexer.Lexer();
            lex.scan();
            lex.scan();
            //Parser parse = new Parser(lex);
            //parse.program();
            Console.WriteLine("\n");
#endif
        }
    }
}
