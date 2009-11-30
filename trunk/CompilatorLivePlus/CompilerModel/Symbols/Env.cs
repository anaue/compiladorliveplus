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
            if (typeof(Word) == _tok.GetType())
            {
                _sym.Id = ((Word)_tok).Lexeme;
                _sym.Token = _tok;
                Symbols.AddSymbol(_sym);
            }
        }
        public void AddSymbol(Symbol _sym)
        {
            Symbols.AddSymbol(_sym);
        }
        public Symbol GetSymbol(Token _tok)
        {
            if (typeof(Word) == _tok.GetType())
            {

                for (Env e = this; e != null; e = e.Previous)
                {
                    Symbol found = e.Symbols.GetSymbol(((Word)_tok).Lexeme);
                    if (found != null)
                        return found;
                }
            }

            return null;
        }
    }
}
