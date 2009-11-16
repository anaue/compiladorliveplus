using System;
using System.IO;
using System.Text;
using CompilatorLivePlus.Symbols;
using CompilatorLivePlus.Lexer;

namespace CompilatorLivePlus.Input
{
    public class Automaton
    {
        private int[][] rules;
        private int[] finals;
        private int actualState;

        public Automaton(int[][] _rules, int[] _finals)
        {
            actualState = 0;
            rules = _rules;
            finals = _finals;
        }
        public bool Accept(Input _input )
        {
            Token nextTok;

            while (_input.hasNext())
            {
                nextTok = _input.getNext();
                if (nextTok != null)
                    actualState = rules[actualState][nextTok.tag];
            }
            foreach( int final in finals)
            {
                if (final == actualState)
                    return true;
            }
            return false;
            
        }
    }
}
