using System;
using System.Text;
using System.IO;

namespace CompilatorLivePlus
{

    class Program
    {

        public static string[] words;
        static FileStream file;
        public static StreamReader sr;
        static FileStream rulez;


        static void Main(string[] args)
        {
            Input.Input inChain = new CompilatorLivePlus.Input.Input();
            CompilatorLivePlus.Lexer.Lexer lex = new CompilatorLivePlus.Lexer.Lexer();            
            //Parser parse = new Parser(lex);
            //parse.program();

#if TESTELEXICO

            file = new FileStream("entrada_lexico.txt", FileMode.OpenOrCreate, FileAccess.Read);
            rulez = new FileStream("regras-codigo.txt", FileMode.OpenOrCreate, FileAccess.Read);

            Console.WriteLine("modo NORMAL");

            sr = new StreamReader(file);
            while (!sr.EndOfStream)
            {
                Lexer.Token escan = lex.scan();
               
                inChain.Add(escan);
            }
            sr = new StreamReader(rulez);

            lex.loadRulez();
 
            Input.Automaton _main = new Input.Automaton( lex.regras , lex.finals);
            if (_main.Accept(inChain))
                Console.WriteLine("aceita");
            else
                Console.WriteLine("não aceita");


            inChain.resetNext();


            while (inChain.hasNext())
            {
                Lexer.Token _input = inChain.getNext();
                if (_input != null)
                {
                    Console.Write(_input.tag+ "    ");
                    try
                    {
                        Console.Write(((Lexer.Word)_input).Lexeme);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            Console.Write(((Lexer.Num)_input).Value);

                        }
                        catch (Exception)
                        {
                            Console.Write(Char.ConvertFromUtf32(_input.tag));
                        }
                    }
                    finally
                    {
                        Console.Write("\n");
                    }

                    if (_input.tag == 59)
                        Console.WriteLine(" ");
                }
            }
            Console.WriteLine("linhas:" + lex.line);
            file.Close();
#endif
            Console.ReadLine();
            
        }
       

    }
}
