using System;
using CompilatorLivePlus.Lexer;
using System.Text;
using CompilerModel.Lexer;

namespace CompilatorLivePlus.Input
{
    public class Input
    {
        static int maxInput = 2048;
        private Token[] entrada;
        private int level, next;

        public Input()
        {
            entrada = new Token[maxInput];
            level = next = 0;
        }
        public void Add( Token _token)
        {
            if (level < maxInput)
            {
                entrada[level] = _token;
                level++;
            }
        }
        public Token getNext()
        {
            Token retorno = entrada[next];
            next++;
            return retorno;
        }
        public bool hasNext()
        {
            return (level > next);
        }
        public void resetNext()
        {
            next = 0;
        }
    }
}
