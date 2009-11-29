using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CompilerModel.Semantic
{
    public class Output
    {
        private StreamWriter _writer;
        private StringBuilder _output;
        private String _pathName;
        public int LineCode;


        public Output(string path)
        {
            LineCode = 0;
            _pathName = path;
            _output = new StringBuilder();
        }

        public void WriteCode(string codeLine)
        {
            _output.AppendLine("\t\t" + codeLine + " ;");
            LineCode++;
        }

        public void WriteCommentedCode(string codeLine, string comment)
        {
            _output.AppendLine("\t\t" + codeLine + " ;\t" + comment);
            LineCode++;
        }

        public void WriteCode(string label, string codeLine)
        {
            _output.AppendLine(label + "\t" + codeLine + " ;");
            LineCode++;
        }

        public void WriteCommentedCode(string label, string codeLine, string comment)
        {
            _output.AppendLine(label + "\t" + codeLine + " ;\t" + comment);
            LineCode++;
        }

        public void SaveFile()
        {
            string directoryName = Path.GetDirectoryName(_pathName);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            _writer = new StreamWriter(_pathName, false, Encoding.Default);
            _writer.Write(_output.ToString());
            _writer.Close();
        }

        public override string ToString()
        {
            return _output.ToString();
        }


        internal string GenerateVarName()
        {

            return "A";
        }
    }
}
