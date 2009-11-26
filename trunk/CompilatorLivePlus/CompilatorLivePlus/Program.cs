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

            Sintatic.Sintatic sint = new CompilatorLivePlus.Sintatic.Sintatic(lex);
            sint.Run();
            

#if TESTELEXICO


            //while (inChain.hasNext())
            //{
            //    Lexer.Token _input = inChain.getNext();
            //    if (_input != null)
            //    {
            //        Console.Write(_input.tag+ "    ");
            //        try
            //        {
            //            Console.Write(((Lexer.Word)_input).Lexeme);
            //        }
            //        catch (Exception)
            //        {
            //            try
            //            {
            //                Console.Write(((Lexer.Num)_input).Value);

            //            }
            //            catch (Exception)
            //            {
            //                Console.Write(Char.ConvertFromUtf32(_input.tag));
            //            }
            //        }
            //        finally
            //        {
            //            Console.Write("\n");
            //        }

            //        if (_input.tag == 59)
            //            Console.WriteLine(" ");
            //    }
            //}
            
            Console.WriteLine("linhas:" + lex.line);
            
#endif
            Console.ReadLine();
            
        }
       

    }
}
