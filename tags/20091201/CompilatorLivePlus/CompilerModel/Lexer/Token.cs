using System;
using System.Text;

namespace CompilerModel.Lexer
{
    public class Token
    {
        public int tag;
        public int Line;

        public Token(String ttag)
        {
            ttag = ttag.ToUpper();
            string[] names = Enum.GetNames(typeof(Tag));
            bool tagFound = false;
            switch (ttag)
            {
                case "==":
                    ttag = "EQUAL";
                    break;
                case "!=":
                    ttag = "NEQUAL";
                    break;
                case ">=":
                    ttag = "GEQUAL";
                    break;
                case "<=":
                    ttag = "LEQUAL";
                    break;
                default:
                    break;
            }
            foreach (string item in names)
            {
                if (item == ttag)
                {
                    tag = (int)((Tag)Enum.Parse(typeof(Tag), ttag.ToUpper().Trim()));
                    tagFound = true;
                    break;
                }
            }
            if (!tagFound)
            {
                if (ttag.Length == 1)
                    tag = (int)Convert.ToChar(ttag);
                else
                    throw new ApplicationException("Token " + ttag + " not found.");
            }
        }

        public Token(int t, int line)
        {
            tag = t;
            Line = line;
        }

        public override string ToString()
        {
            return tag.ToString();
        }

        public override bool Equals(object obj)
        {
            return ((Token)obj).tag == tag;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

}
