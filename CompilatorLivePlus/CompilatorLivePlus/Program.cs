using System;
using System.Text;
using System.IO;

namespace CompilatorLivePlus
{
    class Program
    {



        static void Main(string[] args)
        {
            CompilatorLivePlus.Lexer.Lexer lex = new CompilatorLivePlus.Lexer.Lexer();
            try
            {       

                Sintatic.Sintatic sint = new CompilatorLivePlus.Sintatic.Sintatic(lex);
                sint.Run();
                Console.WriteLine("Linhas:" + lex.line);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na linha:" + lex.line);
                Console.WriteLine(ex.Message);
            }

#if TESTELEXICO

#endif     
            
            Console.ReadLine();
            
        }
       

    }
}
