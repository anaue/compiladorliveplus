using System;
using System.IO;
using System.Text;

namespace CompilatorLivePlus.Sintatic
{
            class Sintatic
            {
                StreamReader sr;
                Input.Input inChain;
                //FileStream rulez;
                CompilatorLivePlus.Lexer.Lexer _lex;
                public CompilatorLivePlus.Symbols.Env _environment;


                public Sintatic(CompilatorLivePlus.Lexer.Lexer lex)
                {
                    _lex = lex;
                    _environment = new CompilatorLivePlus.Symbols.Env( null);

                    //rulez = new FileStream("regras-codigo.txt", FileMode.OpenOrCreate, FileAccess.Read);
                }

                public void Program()
                {
                    inChain = new CompilatorLivePlus.Input.Input();

                    while (!_lex.hasEnded())
                    {
                        Lexer.Token escan = _lex.scan();
                        inChain.Add(escan);
                    }
                    //sr = new StreamReader(rulez);

                    if (inChain.hasNext())
                    {
                        Console.WriteLine(" ");
                    }
                }

    }
}
