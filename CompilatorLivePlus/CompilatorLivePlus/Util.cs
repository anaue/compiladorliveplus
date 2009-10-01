using System;
using System.Text;

namespace CompilatorLivePlus
{
    class Hashtable
    {
        private static int _tamTable = 100;
        private object[] _hashTable;

        public Hashtable(){
        _hashTable = new object[_tamTable];

        }
        public void put(string chave, object obj)
        {
            _hashTable[funcHash(chave)] = obj;
        }
        public object get(string chave)
        {
            return _hashTable[funcHash(chave)];
        }

        private int funcHash( object chave)
        {
            //return _tamTable % chave;
            return modulo(chave.GetHashCode()) % _tamTable;
        }
        private int modulo(int inteiro)
        {
            if (inteiro < 0)
                return -inteiro;
            else
                return inteiro;
        }

    }
    static class Character
    {
        public static bool isWhiteSpace(char peek)
        {
            return (peek == 32);
        }
        public static bool isTabSpace(char peek)
        {
            return (peek == 9);
        }
        public static bool isBreakLine(char peek)
        {
            return (peek == 10);
        }
        public static bool isComment(char peek)
        {
            return (peek == 47);
        }

        public static bool isDigit(char peek)
        {
            return (peek >= 48 && peek <= 57);
        }
        public static bool isLetter(char peek)
        {
            return (peek >= 65 && peek <= 90) || (peek >= 97 && peek <= 122);
        }
        public static bool isLetterOrDigit(char peek)
        {
            return isLetter(peek) || isDigit(peek);
        }
    }
}
