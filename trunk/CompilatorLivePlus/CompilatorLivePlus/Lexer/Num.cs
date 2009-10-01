using System;
using System.Collections.Generic;
using System.Text;

namespace CompilatorLivePlus.Lexer
{
    class Num: Token
    {
        private int value;

        public int Value
        {
            get { return this.value; }
        }
        public Num(int v): base((int)Tag.NUM)
        {
            value = v;
        }

    }
}
