using System;
using System.Text;

namespace CompilatorLivePlus.Lexer
{
    class Word : Token
    {
        private string lexeme;
        public string Lexeme
        {
            get { return lexeme; }
        }
        public Word(int t, string s): base(t)
        {
            lexeme = s;
        }
    }
}
