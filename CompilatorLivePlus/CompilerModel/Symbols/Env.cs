using System;
using System.Collections.Generic;
using System.Text;
using CompilerModel.Lexer;
using CompilerModel.Structures;

namespace CompilerModel.Symbols
{
    public class Env
    {
        public SymbolTable Symbols;
        public Env Previous;
        public int CloseScope;
        public Env(Env n)
        {
            Symbols = new SymbolTable();
            Previous  = n;
        }
        public void AddSymbol(Token _tok)
        {
            Symbol _sym = new Symbol();
            _sym.Id = _tok.tag.ToString();
            _sym.Token = _tok;
            Symbols.AddSymbol(_sym);
        }
        public Symbol GetSymbol(Token _tok)
        {
            for (Env e = this; e != null; e = e.Previous)
            {
                Symbol found = e.Symbols.GetSymbol(_tok.ToString());
                if (found != null)
                    return found;
            }
            return null;
        }
    }
}
