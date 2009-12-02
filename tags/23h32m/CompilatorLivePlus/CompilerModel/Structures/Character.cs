using System;
using System.Text;

namespace CompilerModel.Structures
{

    public static class Character
    {
        public static bool isWhiteSpace(char peek)
        {
            return ((int)peek == 32);
        }
        public static bool isTabSpace(char peek)
        {
            return ((int)peek == 9);
        }
        public static bool isCarriegeReturn(char peek)
        {
            return ((int)peek == 10 );
        }
        public static bool isLineFeed(char peek)
        {
            return ((int)peek == 13);
        }
        public static bool isComment(char peek)
        {
            return ((int)peek == 47);
        }

        public static bool isDigit(char peek)
        {
            return ((int)peek >= 48 && (int)peek <= 57);
        }
        public static bool isLetter(char peek)
        {
            return ((int)peek >= 65 && (int)peek <= 90) || ((int)peek >= 97 && (int)peek <= 122);
        }
        public static bool isLetterOrDigit(char peek)
        {
            return isLetter(peek) || isDigit(peek) || ( (int)peek == 95);
        }
    }
}
