using System;
using System.Text;

namespace CompilerModel.Lexer
{
    public class Word : Token
    {
        private string lexeme;
        public string Lexeme
        {
            get { return lexeme; }
        }
        public Word(int t, string s, int line): base(t, line)
        {
            lexeme = s;
            base.Line = line;
        }
    }
}
