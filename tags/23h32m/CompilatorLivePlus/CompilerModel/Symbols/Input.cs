using System;
using CompilerModel.Lexer;
using System.Text;

namespace CompilerModel.Symbols
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
            
            return retorno;
        }

        public Token getLookAHead()
        {
            //if (lookAHead == maxInput)
            //    return null;
            if (next+1 > maxInput)
                return null;

            Token retorno = entrada[next];
            //lookAHead++;
            return retorno;
        }
        public bool hasNext()
        {
            return (level > next);
        }
        public void resetNext()
        {
            next =1;
        }
        //public void LookedAHead()
        //{
        //    next = lookAHead + 1;
        //    lookAHead = next + 1;
        //}
    }
}
