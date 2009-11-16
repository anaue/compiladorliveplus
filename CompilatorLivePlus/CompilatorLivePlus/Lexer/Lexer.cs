using System;
using System.IO;
using System.Text;

namespace CompilatorLivePlus.Lexer
{
    class Lexer
    {
        static FileStream file;
        StreamReader sr;
        
        public int line = 1;
        private char peek;
        private Hashtable words = new Hashtable();

        void reserve(Word t)
        {
            words.Add(t.Lexeme, t);
        }
        public Lexer()
        {

            sr = new StreamReader(new FileStream("entrada_lexico.txt", FileMode.OpenOrCreate, FileAccess.Read));
            

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
            for (; ; peek = (char) sr.Read())
            {
                if (Character.isWhiteSpace(peek) || Character.isTabSpace(peek) || Character.isLineFeed(peek)) continue;
                if (peek == 47) // char / comentario
                {
                    peek = (char)sr.Read();

                    if (peek == 47) // char 
                    {
                        while (!Character.isCarriegeReturn(peek))
                        {
                            peek = (char)sr.Read();
                        }
                    }
                    else
                        return new Token(47);
                }
                if (Character.isCarriegeReturn(peek))
                {
                    line = line + 1;
                }

                else break;
            }
            switch ((int)peek)
            {
                case 61: // char =
                    peek = (char)sr.Read();
                    if ((int)peek == 61)
                    {
                        peek = (char)sr.Read();
                        return new Word((int)Tag.EQUAL, "==");
                    }
                    else
                        return new Token(61);
                case 33: // char !
                    peek = (char)sr.Read();
                    if ((int)peek == 61)
                    {
                        peek = (char)sr.Read();
                        return new Word((int)Tag.NEQUAL, "!=");
                    }
                    else
                        return new Token(33);

                case 60: // char <
                    peek = (char)sr.Read();
                    if ((int)peek == 61)
                    {
                        peek = (char)sr.Read();
                        return new Word((int)Tag.LEQUAL, "<=");
                    }
                    else
                        return new Token(60);
                case 62: // char >
                    peek = (char)sr.Read();
                    if ((int)peek == 61)
                    {
                        peek = (char)sr.Read();
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
                    peek = (char)sr.Read();
                } while (Character.isDigit(peek));
                return new Num(v);
            }
            if (Character.isLetter(peek))
            {
                string bufStr = string.Empty;
                do
                {
                    bufStr = bufStr + peek.ToString();
                    peek = (char)sr.Read();
                } while (Character.isLetterOrDigit(peek));

                Word w = (Word)words.getElement(bufStr);
                if (w != null) return w;

                w = new Word((int)Tag.ID, bufStr);
                words.Add(bufStr, w);
                return w;

            }
            Token t = new Token(peek);
            peek = (char)sr.Read();
            return t;

        }
        public bool hasEnded()
        {
            if (!sr.EndOfStream)
                return false;
            else
            {
                sr.Close();
                file.Close();
                return true;
            }
        }
    }
}

