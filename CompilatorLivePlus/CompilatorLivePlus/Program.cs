using System;
using System.Text;
using System.IO;

namespace CompilatorLivePlus
{
    class Program
    {

        static void Main(string[] args)
        {
            string entrada, saida;
            if (args.Length < 1)
            {
                Console.Write("Specify the source file path: ");
                entrada = Console.ReadLine();
                Console.WriteLine();
            }
            else
                entrada = args[0];

            if (args.Length < 2)
            {
                Console.Write("Specify the destination file path: ");
                Console.WriteLine();
                saida = Console.ReadLine();
            }
            else
                saida = args[1];

            Console.WriteLine("\n:: Reading " + entrada + "... ");
            CompilatorLivePlus.Lexer.Lexer lex = new CompilatorLivePlus.Lexer.Lexer(entrada);
            try
            {       
                Sintatic.Sintatic sintatic = new CompilatorLivePlus.Sintatic.Sintatic(lex, saida);
                sintatic.Run();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na linha:" + lex.line);
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();

            //Console.WriteLine("Press enter to start MVN execution...");
            //Console.ReadLine();
            //System.Diagnostics.ProcessStartInfo procInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c java -jar mvn\\montador.jar Output\\mvn.txt");
            //procInfo.RedirectStandardOutput = true;
            //procInfo.UseShellExecute = false;
            //procInfo.CreateNoWindow = true;


            //System.Diagnostics.Process process = new System.Diagnostics.Process();
            //process.StartInfo = procInfo;
            //process.Start();


            //string result = process.StandardOutput.ReadToEnd();

            //Console.WriteLine(result);
            ////Console.ReadLine();

            //procInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c java -jar mvn\\mvnpcs.jar");
            //procInfo.RedirectStandardOutput = false;
            //procInfo.UseShellExecute = false;
            //procInfo.CreateNoWindow = false;

            //process.Dispose();
            //process = new System.Diagnostics.Process();
            //process.StartInfo = procInfo;
            //process.Start();
            
            //Console.WriteLine(result);
        }
       

    }
}
