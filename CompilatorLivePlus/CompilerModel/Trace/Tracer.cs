using System;
using System.IO;
using System.Text;

namespace CompilerModel.Trace
{

    public class Tracer
    {
        
        static StreamWriter _log;

        public Tracer()
        {
            //_log = new StreamWriter();
        }
        static public void putLog(string _msg, string _typeClass)
        {
            try
            {
                string record = (_typeClass + ": " + _msg);
              
                using (StreamWriter _log = new StreamWriter("log.txt", true))
                {
                    _log.WriteLine(record);
                }
                
            }
            catch (Exception) 
            {
                throw new Exception(" Falha no log");
            }
        }
    }

}
