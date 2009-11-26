using System;
using CompilerModel.Lexer;
using System.Text;

namespace CompilatorLivePlus.Input
{
    public class Input
    {
        static int maxInput = 2048;
        private Token[] entrada;
        private int level, next, lookAHead;

        public Input()
        {
            entrada = new Token[maxInput];
            level = next = 0;
        }
        public void Add(Token _token)
        {
            if (level < maxInput)
            {
                entrada[level] = _token;
                level++;
            }
            else
            {
                throw new Exception("INPUT: Max input reached");
            }
        }
        public Token getNext()
        {
            if (next == maxInput)
                throw new Exception("INPUT: Next input invalid");
            Token retorno = entrada[next];
            next++;
            lookAHead = next + 1;
            return retorno;
        }

        public Token getLookAHead()
        {
            if (lookAHead == maxInput)
                throw new Exception("INPUT: Look a head invalid");
            Token retorno = entrada[lookAHead];
            lookAHead++;
            return retorno;
        }
        public bool hasNext()
        {
            return (level > next);
        }
        public void resetNext()
        {
            next = 0;
            lookAHead = next + 1;
        }
    }
}
