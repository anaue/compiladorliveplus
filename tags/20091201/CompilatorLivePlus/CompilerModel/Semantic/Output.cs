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
        private string _label = "\t";
        private StringBuilder _reservedArea;
        public int LineCode;


        public Output(string path)
        {
            LineCode = 0;
            _pathName = path;
            _output = new StringBuilder();
            _reservedArea = new StringBuilder();
        }

        public void WriteCode(string codeLine)
        {
            _output.AppendLine(_label + "\t" + codeLine);
            _label = "\t";
            LineCode++;
        }


        public void WriteCommentedCode(string codeLine, string comment)
        {
            _output.AppendLine(_label+ "\t" + codeLine + " ;\t" + comment);
            _label = "\t";
            LineCode++;
        }

        public void SetLabelCode(string label)
        {
            _label = label;
        }

        public void SaveFile()
        {
            string directoryName = Path.GetDirectoryName(_pathName);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            _writer = new StreamWriter(_pathName, false, Encoding.Default);
            _writer.Write(_output.ToString() + _reservedArea.ToString());
            _writer.Close();
        }

        public override string ToString()
        {
            _output.AppendLine(_reservedArea.ToString());
            return _output.ToString();
        }

        public void WriteReservedArea(string codeLine)
        {
            _reservedArea.AppendLine("\t\t"+ codeLine);
        }

        internal string GenerateVarName(string _name)
        {

            _name = _name.ToUpper().Replace("_","");
            _name = _name.Replace("1", "");
            _name = _name.Replace("2", "");
            _name = _name.Replace("3", "");
            _name = _name.Replace("4", "");
            _name = _name.Replace("5", "");
            _name = _name.Replace("6", "");
            _name = _name.Replace("7", "");
            _name = _name.Replace("8", "");
            _name = _name.Replace("9", "");
            _name = _name.Replace("0", "");

            return _name;
        }
    }
}
