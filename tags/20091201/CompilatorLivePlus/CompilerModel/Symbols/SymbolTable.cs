using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompilerModel.Structures;

namespace CompilerModel.Symbols
{
    public class SymbolTable
    {
        private Hashtable _hash;

        public SymbolTable()
        {
            _hash = new Hashtable();
        }

        public Symbol GetSymbol(string id)
        {
            return (Symbol)_hash.getElement(id);
        }

        public Symbol GetSymbol(Symbol symbol)
        {
            return (Symbol)_hash.getElement(symbol.Id);
        }

        public void AddSymbol(Symbol symbol)
        {
            _hash.Add(symbol.Id, symbol);
        }

    }
}
