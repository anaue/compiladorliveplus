using System;
using System.Collections.Generic;
using System.Text;
using CompilerModel.Lexer;


namespace CompilerModel.Structures
{
    public class Hashtable
    {
        private static int _tamTable = 1024;
        private System.Collections.Hashtable _hashTable;

        public Hashtable()
        {
            _hashTable = new System.Collections.Hashtable();

        }
        public void Add(string chave, object obj)
        {
            _hashTable[chave] = obj;
        }

        
        public object getElement(string chave)
        {
            return _hashTable[chave];
        }

        //public string getChave(string obj)
        //{
        //    string chave = "";
        //    for (int indice = 0; indice < _tamTable; indice++)
        //    {
        //        if (_hashTable[indice] != null)
        //        {
        //            if (_hashTable[indice].GetType() == Type.GetType("Word"))
        //            {
        //                if (((Word)_hashTable[indice]).Lexeme == obj)
        //                    chave = ((Word)_hashTable[indice]).tag.ToString();
        //            }
        //        }
        //    }
        //    return chave;
        //}

        //private int funcHash(string chave)
        //{
        //    //return _tamTable % chave;
        //    return modulo(chave.GetHashCode()) % _tamTable + chave.Length;
        //}
        //private int modulo(int inteiro)
        //{
        //    if (inteiro < 0)
        //        return -inteiro;
        //    else
        //        return inteiro;
        //}

    }
}
