using System;
using CompilatorLivePlus.Lexer;
using System.Text;

namespace CompilatorLivePlus.Input
{
    public class Input
    {
        static int maxInput = 2048;
        private Lexer.Token[] entrada;
        private int level, next;

        public Input()
        {
            entrada = new Lexer.Token[maxInput];
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
