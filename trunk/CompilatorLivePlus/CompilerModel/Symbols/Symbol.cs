﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompilerModel.Lexer;

namespace CompilerModel.Symbols
{
    public class Symbol
    {
        public string Id;
        public Token Token;
        public string Name;
        public string Type;
        public object OperationalValue;
        public string PrintableValue;
    }
}
