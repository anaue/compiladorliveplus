using System;
using System.Collections.Generic;
using System.Text;

namespace APE.Lexer
{
    public enum Tag
    {
        TRUE = 257,
        FALSE = 258,
        AND = 259,
        OR = 260,
        FUNCTION = 261,
        BEGIN = 262,
        RETURN = 263,
        ENDFUNCTION = 264,
        SUB = 265,
        ENDSUB = 266,
        PROGRAM = 267,
        END = 268,
        INT = 269,
        FLOAT = 270,
        BOOL = 271,
        STRING = 272,
        STRUCT = 273,
        ENDSTRUCT = 274,
        IF = 275,
        THEN = 276,
        ELSE = 277,
        ENDIF = 278,
        WHILE = 279,
        LOOP = 280,
        ENDLOOP = 281,
        INPUT = 282,
        OUTPUT = 283,
        CALL = 284,
        EQUAL = 285,
        NEQUAL = 286,
        LEQUAL = 287,
        GEQUAL = 288,
        NUM = 289,
        ID = 290
    }
}
