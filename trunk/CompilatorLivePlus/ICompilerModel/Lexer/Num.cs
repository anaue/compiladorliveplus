using System;
using System.Collections.Generic;
using System.Text;

namespace CompilerModel.Lexer
{
    public class Num: Token
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
