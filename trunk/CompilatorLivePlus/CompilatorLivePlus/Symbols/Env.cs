using System;
using System.Collections.Generic;
using System.Text;
using CompilatorLivePlus.Lexer;

namespace CompilatorLivePlus.Symbols
{
  
    public class Env
    {
        private Hashtable table;
        protected Env anterior;
        public Env(Env n)
        {
            table = new Hashtable();
            anterior = n;
        }
        public void put(Token w, string id)
        {
            table.Add(id, w);
        }
        public string get(Token w)
        {
            for (Env e = this; e != null; e = e.anterior)
            {
                string found = (string)(e.table.getChave(w));
                if (found != null)
                    return found;
            }
            return null;
        }
    }
}
