using System;
using System.Text;

namespace CompilatorLivePlus.Lexer
{
    class Lexer
    {
        public int line = 1;
        private char peek;
        private Hashtable words = new Hashtable();
        public int[][] regras;
        public int[] finals;

        void reserve(Word t)
        {
            words.Add(t.Lexeme, t);
        }
        public Lexer()
        {
            reserve(new Word((int)Tag.TRUE, "true"));
            reserve(new Word((int)Tag.FALSE, "false"));
            reserve(new Word((int)Tag.PROGRAM, "program"));
            reserve(new Word((int)Tag.END, "end"));
            reserve(new Word((int)Tag.IF, "if"));
            reserve(new Word((int)Tag.AND, "AND"));
            reserve(new Word((int)Tag.OR, "OR"));
            reserve(new Word((int)Tag.FUNCTION, "function"));
            reserve(new Word((int)Tag.BEGIN, "begin"));
            reserve(new Word((int)Tag.RETURN, "return"));
            reserve(new Word((int)Tag.ENDFUNCTION, "endfunction"));
            reserve(new Word((int)Tag.SUB, "sub"));
            reserve(new Word((int)Tag.ENDSUB, "endsub"));
            reserve(new Word((int)Tag.INT, "int"));
            reserve(new Word((int)Tag.FLOAT, "float"));
            reserve(new Word((int)Tag.BOOL, "bool"));
            reserve(new Word((int)Tag.STRING, "string"));
            reserve(new Word((int)Tag.STRUCT, "struct"));
            reserve(new Word((int)Tag.ENDSTRUCT, "endestruct"));
            reserve(new Word((int)Tag.THEN, "then"));
            reserve(new Word((int)Tag.ELSE, "else"));
            reserve(new Word((int)Tag.ENDIF, "endif"));
            reserve(new Word((int)Tag.WHILE, "while"));
            reserve(new Word((int)Tag.LOOP, "loop"));
            reserve(new Word((int)Tag.ENDLOOP, "endloop"));
            reserve(new Word((int)Tag.INPUT, "input"));
            reserve(new Word((int)Tag.OUTPUT, "output"));
            reserve(new Word((int)Tag.CALL, "call"));
        }
        public Token scan()
        {

            for (; ; peek = (char)Program.sr.Read())
            {
                if (Character.isWhiteSpace(peek) || Character.isTabSpace(peek) || Character.isLineFeed(peek)) continue;
                if (peek == 47) // char / comentario
                {
                    peek = (char)Program.sr.Read();

                    if (peek == 47) // char 
                    {
                        while (!Character.isCarriegeReturn(peek))
                        {
                            peek = (char)Program.sr.Read();
                        }
                    }
                    else
                        return new Token(47);
                }
                if (Character.isCarriegeReturn(peek))
                {
                    line = line + 1;
#if TesteLexico
                    Console.WriteLine(" ");
#endif
                }

                else break;
            }
            switch ((int)peek)
            {
                case 61: // char =
                    peek = (char)Program.sr.Read();
                    if ((int)peek == 61)
                    {
                        peek = (char)Program.sr.Read();
                        return new Word((int)Tag.EQUAL, "==");
                    }
                    else
                        return new Token(61);
                case 33: // char !
                    peek = (char)Program.sr.Read();
                    if ((int)peek == 61)
                    {
                        peek = (char)Program.sr.Read();
                        return new Word((int)Tag.NEQUAL, "!=");
                    }
                    else
                        return new Token(33);

                case 60: // char <
                    peek = (char)Program.sr.Read();
                    if ((int)peek == 61)
                    {
                        peek = (char)Program.sr.Read();
                        return new Word((int)Tag.LEQUAL, "<=");
                    }
                    else
                        return new Token(60);
                case 62: // char >
                    peek = (char)Program.sr.Read();
                    if ((int)peek == 61)
                    {
                        peek = (char)Program.sr.Read();
                        return new Word((int)Tag.GEQUAL, ">=");
                    }
                    else
                        return new Token(62);
            }

            if (Character.isDigit(peek)) // is digit ?
            {
                int v = 0;
                do
                {
                    v = 10 * v + (peek - 48);
                    peek = (char)Program.sr.Read();
                } while (Character.isDigit(peek));
                return new Num(v);
            }
            if (Character.isLetter(peek))
            {
                string bufStr = string.Empty;
                do
                {
                    bufStr = bufStr + peek.ToString();
                    peek = (char)Program.sr.Read();
                } while (Character.isLetterOrDigit(peek));

                Word w = (Word)words.getElement(bufStr);
                if (w != null) return w;
                w = new Word((int)Tag.ID, bufStr);
                words.Add(bufStr, w);
                return w;

            }
            Token t = new Token(peek);
            peek = (char)Program.sr.Read();
            return t;

        }

        public void loadRulez()
        {
            regras = new int[300][];
            finals = new int[2];
            finals[1] = 13;
            finals[0] = 13;

            regras[0] = new int[300];
            regras[1] = new int[300];
            regras[4] = new int[300];
            regras[6] = new int[300];

            //regras[0] = new int[(int)Tag.ID + 1];
            regras[0][(int)Tag.ID] = 0;
            //regras[0] = new int[(int)Tag.PROGRAM + 1];
            regras[0][(int)Tag.PROGRAM] = 4;
            //regras[4] = new int[(int)Tag.ID + 1];
            regras[4][(int)Tag.ID] = 6;
            //regras[6] = new int[(int)Tag.THEN + 1];
            regras[6][(int)Tag.THEN] = 8;
        }
    }
}

