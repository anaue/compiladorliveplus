using System;
using System.Text;

namespace CompilatorLivePlus.Lexer
{
    class Lexer
    {
        public int line = 1;
        private char peek = char.MinValue;
        private Hashtable words = new Hashtable();

        void reserve(Word t)
        {
            words.put(t.Lexeme, t);
        }
        public Lexer()
        {
            reserve(new Word((int)Tag.TRUE, "true"));
            reserve(new Word((int)Tag.FALSE, "false"));
        }
        public Token scan()
        {
            bool isComment = false;
            for (; ; peek = (char)Console.Read())
            {
                if (Character.isWhiteSpace(peek) || Character.isTabSpace(peek)) continue;
                if (peek == 47)
                {
                    peek = (char)Console.Read();
                }

                else if (Character.isBreakLine(peek))
                {
                    line = line + 1;
                }
                
                else break;
            }
            if (Character.isDigit(peek)) // is digit ?
            {
                int v = 0;
                do
                {
                    v = 10 * v + (peek - 48);
                    peek = (char)Console.Read();
                } while (peek >= 48 && peek <= 57);
                return new Num(v);
            }
            if (Character.isLetter(peek))
            {
                string bufStr = string.Empty;
                do
                {
                    bufStr = bufStr + peek.ToString();
                    peek = (char)Console.Read();
                } while (Character.isLetterOrDigit(peek));

                Word w = (Word)words.get(bufStr);
                if (w != null) return w;
                w= new Word((int)Tag.ID, bufStr);
                words.put(bufStr, w);
                return w;

            }
            Token t = new Token(peek);
            peek = char.MinValue;
            return t;

        }
    }
}
