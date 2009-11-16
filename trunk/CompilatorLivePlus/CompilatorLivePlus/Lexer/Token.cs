using System;
using System.Text;

namespace CompilatorLivePlus.Lexer
{
    public class Token
    {
        public int tag;
        public Token(int t)
        {
            tag = t;
        }
        public override string ToString()
        {
            
            return tag.ToString();
        }
    }

}
