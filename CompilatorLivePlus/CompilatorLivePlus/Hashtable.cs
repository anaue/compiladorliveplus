using System;
using System.Collections.Generic;
using System.Text;
using CompilatorLivePlus.Lexer;


namespace CompilatorLivePlus
{
    class Hashtable
    {
        private static int _tamTable = 1024;
        private object[] _hashTable;

        public Hashtable()
        {
            _hashTable = new object[_tamTable];

        }
        public void Add(string chave, object obj)
        {
            _hashTable[funcHash(chave)] = obj;
        }
        public string getChave(object obj)
        {
            string chave = "";
            for (int indice = 0; indice < _tamTable;indice++)
            {
                if (_hashTable[indice] != null)
                {
                    if (((Token)_hashTable[indice]).ToString().Equals(obj))
                        chave = ((Token)_hashTable[indice]).tag.ToString();
                }
            }
            return chave;
        }

        public object getElement(string chave)
        {
            return (_hashTable[funcHash(chave)]);
        }
        private int funcHash(string chave)
        {
            //return _tamTable % chave;
            return modulo(chave.GetHashCode()) % _tamTable + chave.Length;
        }
        private int modulo(int inteiro)
        {
            if (inteiro < 0)
                return -inteiro;
            else
                return inteiro;
        }

    }
}
