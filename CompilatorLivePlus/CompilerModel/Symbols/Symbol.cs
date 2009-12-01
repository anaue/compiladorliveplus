using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompilerModel.Lexer;

namespace CompilerModel.Symbols
{
    public class Symbol
    {
        public Symbol(Token _tok)
        {
            this.Token = _tok;
            this.Id = _tok.ToString();
            this.Name = _tok.ToString();

            if (typeof(Word) == _tok.GetType())
                this.OperationalValue = ((Word)_tok).Lexeme;
            else if(typeof(Num) == _tok.GetType())
                this.OperationalValue = ((Num)_tok).Value.ToString();
            else
                this.OperationalValue = _tok.ToString();
            
            if (typeof(string) == this.OperationalValue.GetType())
                this.TargetName = (string)this.OperationalValue;

        }
        public Symbol() { }

        public string Id;
        public Token Token;
        public string Name;
        public string TargetName;
        public string Type;
        public object OperationalValue;
        public string PrintableValue;
        public bool Initialized;
        public bool Used;
        //public bool MemoryPosition;
    }
}
