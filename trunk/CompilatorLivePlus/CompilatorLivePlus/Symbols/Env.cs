using System;
using System.Collections.Generic;
using System.Text;
using CompilerModel.Lexer;
using CompilerModel.Structures;

namespace CompilerModel.Symbols
{
    public class Env
    {
        private Hashtable table;
        public Env Previous;
        public int CloseScope;
        public Env(Env n)
        {
            table = new Hashtable();
            Previous  = n;
        }
        public void put(Token w, string id)
        {
            table.Add(id, w);
        }
        public string get(Token w)
        {
            for (Env e = this; e != null; e = e.Previous)
            {
                string found = (string)(e.table.getChave(w.ToString()));
                if (found != null)
                    return found;
            }
            return null;
        }
    }
}
