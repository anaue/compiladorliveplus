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
        private StringBuilder _codeArea;
        private String _pathName;
        private string _label;
        private StringBuilder _reservedArea;
        private StringBuilder _memoryArea;
        private static List<string> _alreadyDeclared;
        public bool _reserved;
        public int CodeLines;
        public int MemoryLines;
        public int ReservedLines;


        public Output(string path)
        {
            CodeLines = 0;
            ReservedLines = 0;
            MemoryLines = 0;
            _reserved = false;
            _label = "\t";

            _pathName = path;
            _codeArea = new StringBuilder();
            _reservedArea = new StringBuilder();
            _memoryArea = new StringBuilder();
            WriteVarArea("JP INICIO");
            _alreadyDeclared = new List<string>();
        }

        public void WriteCode(string codeLine)
        {
            if (_reserved)
            {
                _codeArea.AppendLine(_label + "\t" + codeLine);
                CodeLines++;
            }
            else
            {
                _reservedArea.AppendLine(_label + "\t" + codeLine);
                ReservedLines++;
            }
            _label = "\t";
        }


        public void WriteCode(string codeLine, string comment)
        {
            if (_reserved)
            {
                _codeArea.AppendLine(_label + "\t" + codeLine + " ;\t" + comment);
                CodeLines++;
            }
            else
            {
                _reservedArea.AppendLine(_label + "\t" + codeLine);
                ReservedLines++;
            }
            _label = "\t";

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
            _writer.Write(_memoryArea.ToString() + "@ /" + (2 * (MemoryLines+1)).ToString("X") + "\n" + _codeArea.ToString() + _reservedArea.ToString());
            _writer.Close();
        }

        public override string ToString()
        {
            return _memoryArea.ToString() +"\n" + _codeArea.ToString() + "\n" + _reservedArea.ToString();
        }

        public void WriteVarArea(string varName, string initial_value)
        {
            if (!_alreadyDeclared.Exists(In => In.Equals(varName)))
            {
                _alreadyDeclared.Add(varName);
                _memoryArea.AppendLine("\t\t" + varName + " K " + initial_value);
                MemoryLines++;
            }
        }

        public void WriteVarArea(string code)
        {
            _memoryArea.AppendLine("\t\t" + code);
            MemoryLines++;
        }

        internal string GenerateVarName(string _name)
        {
            return _name.ToUpper();
        }
    }
}
