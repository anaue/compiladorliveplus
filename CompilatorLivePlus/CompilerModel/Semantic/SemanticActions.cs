using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompilerModel.Symbols;
using CompilerModel.Lexer;

namespace CompilerModel.Semantic
{
    public enum CommandType
    {
        ATRIB = 0,
        COND,
        ITERATIVE,
        INPUT,
        OUTPUT,
        SUBROTINE
    }
    public enum BoolOperator
    {
        MAIOR = 0,
        MENOR,
        MAIORIGUAL,
        MENORIGUAL,
        IGUAL,
        DIFERENTE
    }
    public enum VariableType
    {
        INT =0,
        FLOAT,
        BOOL,
        STRING,
        STRUCT
    }

    public class SemanticActions
    {
        private Output _out;
        private Symbol _reg1;
        private Symbol _acc; // tipo do valor no acumulador
        private int _sizeVAR;
        
        private int _typeCMD;
        private int _typeBoolOper;
        private int _typeTYPE;
        
        //
        private const string REG1 = "REGA";
        private const string REG2 = "REGB";
        private const string REG3 = "REGC";
        private const string LABELFALSE = "FALSE";
        private const string TYPE_MVN_INT = "int";
        private const string TYPE_MVN_FLOAT = "float";
        private const string TYPE_MVN_BOOL = "bool";
        //

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="outputFile"></param>
        public SemanticActions(string outputFile)
        {
            _out = new Output(outputFile);
        }

        public void SaveOutput()
        {
            _out.SaveFile();
        }

        #region ACOES SEMANTICAS

        #region COMANDO

        public void AS_COMANDO_ATRIB_1(Env _environment, Token _tok)
        {
            _typeCMD = (int)CommandType.ATRIB;
            _reg1 = _environment.GetSymbol( _tok );
            if (_reg1 != null)
            {
                _reg1.TargetName = _out.GenerateVarName(_reg1.Name);
                
            }
            else
            {
                throw new Exception("SEMANTIC: variable not declared");

            }
            //_out.WriteCommentedCode("", "AS_COMANDO_ATRIB_1");
        }

        public void AS_COMANDO_IF(Env _environment, Token _tok)
        {
            _typeCMD = (int)CommandType.COND;
            _acc = new Symbol();
            _acc.Token = _tok;
        }

        public void AS_COMANDO_WHILE(Env _environment, Token _tok)
        {
            _typeCMD = (int)CommandType.ITERATIVE;
            _out.SetLabelCode("WHILE");
            _acc = new Symbol();
            _acc.Token = _tok;
        }

        public void AS_COMANDO_INPUT_1(Env _environment, Token _tok)
        {
            _typeCMD = (int)CommandType.INPUT;
            _acc = new Symbol();
            _acc.Token = _tok;
        }

        public void AS_COMANDO_OUTPUT (Env _environment, Token _tok)
        {
            _typeCMD = (int)CommandType.OUTPUT;
            _acc = new Symbol();
            _acc.Token = _tok;
        }

        public void AS_COMANDO_CALL(Env _environment, Token _tok)
        {
            _typeCMD = (int)CommandType.SUBROTINE;
            _acc = new Symbol();
            _acc.Token = _tok;
        }

        public void AS_COMANDO_ATRIB_2(Env _environment, Token _tok)
        {
            // recebe o sinal de atribuição '='
            _acc = new Symbol();
            _acc.Token = _tok;
            
        }

        public void AS_COMANDO_CALL_EB(Env _environment, Token _tok)
        {
            
        }

        public void AS_COMANDO_INPUT_2(Env _environment, Token _tok)
        {
            
            if (_reg1 != null)
            {

                if ( _tok.tag == (int)Tag.ID) // valores possiveis de entrada?
                {
                    Symbol _sym = new Symbol();
                    _sym.Id = _tok.ToString();
                    _sym.Name = ((Word)_tok).Lexeme;
                    _sym.Initialized = true;
                    _sym.Used = true;
                    //_sym.OperationalValue = ;
                    //_sym.PrintableValue =;
                    ////
                    throw new NotImplementedException("Comando Input");
                    ////
                }
            }
        }

        public void AS_COMANDO_ATRIB_3(Env _environment, Token _tok)
        {
            //o token devera ser um ID ou '(' do booleano
            
        }

        public void AS_COMANDO_12(Env _environment, Token _tok)
        {
            
        }

        public void AS_COMANDO_13(Env _environment, Token _tok)
        {
            
        }

        public void AS_COMANDO_EXIT(Env _environment, Token _tok)
        {
            switch (_typeCMD)
            {
                case (int)CommandType.ATRIB:
                    {
                        if (_reg1.Type == _acc.Type) // verifica o tipo do accumulador com a variavel a ser atribuida
                        {
                            //_buffer.Initialized = true;
                            //_buffer.Used = true;
                            _out.WriteCommentedCode("MM " + _reg1.TargetName, "AS_COMANDO_EXIT");
                        }
                        break;
                    }
                case (int)CommandType.COND:
                    {
                        break;
                    }
                case (int)CommandType.ITERATIVE:
                    {
                        break;
                    }
                case (int)CommandType.INPUT:
                    {
                        break;
                    }
                case (int)CommandType.OUTPUT:
                    {
                        break;
                    }
                case (int)CommandType.SUBROTINE:
                    {
                        break;
                    }
         

            }
            _acc = new Symbol();

        }

        public void AS_COMANDO_15(Env _environment, Token _tok)
        {
            
        }

        public void AS_COMANDO_16(Env _environment, Token _tok)
        {
            
        }

        public void AS_COMANDO_17(Env _environment, Token _tok)
        {
            
        }

        public void AS_COMANDO_THEN(Env _environment, Token _tok)
        {
            _out.WriteCommentedCode("MM " + REG2, "AS_COMANDO_THEN");

            resultado_booleano("AS_COMANDO_THEN");

            //_out.WriteCommentedCode("LD =" + _varReg2, "AS_COMANDO_THEN");
            //_out.WriteCommentedCode("- " + _varReg1, "COD COMPLEMENTAR BOOLEANO");
            //_out.WriteCommentedCode("JN " + _labelFalse, "SE FOR MENOR, PULA"); 

            _acc = new Symbol();
            _acc.Token = _tok;            
        }

        public void AS_COMANDO_19(Env _environment, Token _tok)
        {
            
        }

        public void AS_COMANDO_20(Env _environment, Token _tok)
        {
            
        }

        public void AS_COMANDO_ELSE(Env _environment, Token _tok)
        {
            _out.SetLabelCode(LABELFALSE);
        }

        public void AS_COMANDO_23(Env _environment, Token _tok)
        {
            
        }

        public void AS_COMANDO_24(Env _environment, Token _tok)
        {
            
        }

        public void AS_COMANDO_ENDIF(Env _environment, Token _tok)
        {
            _out.SetLabelCode(LABELFALSE);
        }

        public void AS_COMANDO_LOOP(Env _environment, Token _tok)
        {
            _out.WriteCommentedCode("MM " + REG2, "AS_COMANDO_LOOP");

            resultado_booleano("AS_COMANDO_LOOP");

        }

        public void AS_COMANDO_27(Env _environment, Token _tok)
        {
            
        }

        public void AS_COMANDO_28(Env _environment, Token _tok)
        {
            
        }

        public void AS_COMANDO_ENDLOOP(Env _environment, Token _tok)
        {
            _out.WriteCommentedCode("JP WHILE"," RETORNA PARA O LOOP");
            _out.SetLabelCode(LABELFALSE);
        }

        #endregion COMANDO Automata

        #region CODIGO

        public void AS_CODIGO_START(Env _environment, Token _tok)
        {
            //_out.WriteCode("INPUT K /0");
            _out.WriteCode("JP INICIO");
            _out.WriteCode(REG1+" K /0"); // CRIA UMA VARIAVEL AUXILIAR PARA CONTAS
            _out.WriteCode(REG2+" K /0"); // CRIA UMA VARIAVEL AUXILIAR PARA CONTAS
            _out.WriteCode(REG3 + " K /0"); // CRIA UMA VARIAVEL AUXILIAR PARA CONTAS

        }

        public void AS_CODIGO_2(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_3(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_4(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_5(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_6(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_7(Env _environment, Token _tok)
        {

        }

        /// <summary>
        /// Recebe o ID, e prepara a declaracao
        /// </summary>
        /// <param name="_environment"></param>
        /// <param name="_tok"></param>
        public void AS_CODIGO_DECLARE_1(Env _environment, Token _tok)
        {
            _reg1 = new Symbol();
            _reg1.Id = _tok.ToString();
            _reg1.Token = _tok;
            _reg1.Name = ((Word)_tok).Lexeme;
            _reg1.TargetName = _out.GenerateVarName(_reg1.Name);
        }

        /// <summary>
        /// Recebe o token 'INICIO'
        /// </summary>
        /// <param name="_environment"></param>
        /// <param name="_tok"></param>
        public void AS_CODIGO_BEGIN(Env _environment, Token _tok)
        {
            _out.WriteCommentedCode("@ /256", "area de programa");
            _out.SetLabelCode("INICIO");
        }

        public void AS_CODIGO_10(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_11(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_12(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_13(Env _environment, Token _tok)
        {

        }

        /// <summary>
        /// Associa o type ao ID declarado
        /// e insere na tabela de simbolos
        /// </summary>
        /// <param name="_environment"></param>
        /// <param name="_tok"></param>
        public void AS_CODIGO_CALLTIPO(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_CALLCOMANDO(Env _environment, Token _tok)
        {
            //_out.WriteCommentedCode("", "AS_CODIGO_CALLCOMANDO");
        }

        public void AS_CODIGO_16(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_ENDPROGRAM(Env _environment, Token _tok)
        {
            _out.WriteCommentedCode("HM /0", "AS_CODIGO_ENDPROGRAM");
        }

        public void AS_CODIGO_19(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_20(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_21(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_DECLARE_3(Env _environment, Token _tok)
        {
            if (_reg1 != null)
            {
                _reg1.Id = ((Word)_reg1.Token).Lexeme;

                if (_acc != null)
                {
                    _reg1.Type = ((Word)_acc.Token).Lexeme; // atribuicao do tipo recebido
                    _environment.AddSymbol(_reg1);
                    _out.WriteCommentedCode(_reg1.TargetName + " K /0", "AS_CODIGO_DECLARE_2");
                }
                
            }
            _reg1 = null;
        }

        public void AS_CODIGO_24(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_25(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_27(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_28(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_29(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_30(Env _environment, Token _tok)
        {
            
        }

        public void AS_CODIGO_31(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_32(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_33(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_34(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_35(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_36(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_37(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_38(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_39(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_40(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_41(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_42(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_43(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_44(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_45(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_46(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_47(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_48(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_49(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_50(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_51(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_52(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_53(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_54(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_55(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_56(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_57(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_58(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_59(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_60(Env _environment, Token _tok)
        {

        }

        public void AS_CODIGO_61(Env _environment, Token _tok)
        {

        }

        #endregion CODIGO Automata

        #region EXPRESSAO ARITMETICA

        /// <summary>
        /// verifica o token do acumulador se nao eh operacao aritmetica 
        /// entao guarda o token recebido no acumulador
        /// </summary>
        /// <param name="_environment"></param>
        /// <param name="_tok"></param>
        public void AS_EA_ID(Env _environment, Token _tok)
        {
            Symbol _sym = _environment.GetSymbol(_tok);

            if (_sym == null)
                throw new Exception("SEMANTIC: var not declared");

            _sym.TargetName = _out.GenerateVarName(_sym.Name);
            

            RealizaOperacaoBoolAritm(_acc, _sym);


            _acc = _sym;

        }
        /// <summary>
        /// Recebe um Token do tipo NUM
        /// DEVE SER INTEIRO (acho)
        /// </summary>
        /// <param name="_environment"></param>
        /// <param name="_tok"></param>
        public void AS_EA_NUM(Env _environment, Token _tok)
        {
            Symbol _sym = new Symbol();
            _sym.TargetName= ((Num)_tok).Value.ToString();
            _sym.Token = _tok;
            _sym.Type = TYPE_MVN_INT;

            if (_sym == null)
                throw new Exception("SEMANTIC: var not declared");

            RealizaOperacaoBoolAritmNumerico(_acc, _sym);

            _acc = _sym;

        }

        public void AS_EA_3(Env _environment, Token _tok)
        {

        }

        public void AS_EA_4(Env _environment, Token _tok)
        {

        }

        public void AS_EA_EXPONENCIAL(Env _environment, Token _tok)
        {
            //_out.WriteCommentedCode("LD " + _accumulator.TargetName, "AS_EA_EXPONENCIAL");
            _acc.Token = _tok;
        }

        public void AS_EA_MULTI(Env _environment, Token _tok)
        {
            //_out.WriteCommentedCode("LD " + _accumulator.TargetName, "AS_EA_MULTI");
            _acc.Token = _tok;
        }

        public void AS_EA_DIVISAO(Env _environment, Token _tok)
        {
           // _out.WriteCommentedCode("LD " + _accumulator.TargetName, "AS_EA_DIVISAO");
            _acc.Token = _tok;
        }

        public void AS_EA_SUBTRACAO(Env _environment, Token _tok)
        {
            //_out.WriteCommentedCode("LD " + _accumulator.TargetName, "AS_EA_SUBTRACAO");
            _acc.Token = _tok;
        }

        public void AS_EA_SOMA(Env _environment, Token _tok)
        {
            _acc.Token = _tok;
        }

        public void AS_EA_15(Env _environment, Token _tok)
        {

        }

        public void AS_EA_16(Env _environment, Token _tok)
        {

        }

        public void AS_EA_17(Env _environment, Token _tok)
        {

        }

        public void AS_EA_18(Env _environment, Token _tok)
        {

        }

        public void AS_EA_19(Env _environment, Token _tok)
        {

        }

        public void AS_EA_20(Env _environment, Token _tok)
        {

        }

        public void AS_EA_21(Env _environment, Token _tok)
        {

        }

        #endregion EXPRESSAO ARITMETICA Automata

        #region EXPRESSAO BOOLEANA

        public void AS_EB_CALL_EA(Env _environment, Token _tok)
        {
            //Symbol _sym = _environment.GetSymbol(_tok);
            //_accType = _sym.Type;
        }

        public void AS_EB_2(Env _environment, Token _tok)
        {

        }

        public void AS_EB_3(Env _environment, Token _tok)
        {

        }

        public void AS_EB_4(Env _environment, Token _tok)
        {

        }

        public void AS_EB_MAIOR(Env _environment, Token _tok)
        {
            _typeBoolOper = (int)BoolOperator.MAIOR;
            if (_acc.Type == TYPE_MVN_INT) //verifica se eh do tipo INT
            {
                _acc = new Symbol();
                _acc.Token = _tok;
                _out.WriteCommentedCode("MM "+ REG1, "AS_EB_MAIOR");
            }
            else
            {
                throw new Exception("SEMANTIC: wrong type of expression");
            }
        }

        public void AS_EB_MENOR(Env _environment, Token _tok)
        {
            _typeBoolOper = (int)BoolOperator.MENOR;
            if (_acc.Type == TYPE_MVN_INT) //verifica se eh do tipo INT
            {
                _acc = new Symbol();
                _acc.Token = _tok;
            }
            else
            {
                throw new Exception("SEMANTIC: wrong type of expression");
            }
        }

        public void AS_EB_MAIORIQUAL(Env _environment, Token _tok)
        {
            _typeBoolOper = (int)BoolOperator.MAIORIGUAL;
            if (_acc.Type == TYPE_MVN_INT) //verifica se eh do tipo INT
            {
                _acc = new Symbol();
                _acc.Token = _tok;
            }
            else
            {
                throw new Exception("SEMANTIC: wrong type of expression");
            }
        }

        public void AS_EB_MENORIQUAL(Env _environment, Token _tok)
        {
            _typeBoolOper = (int)BoolOperator.MAIORIGUAL;
            if (_acc.Type == TYPE_MVN_INT) //verifica se eh do tipo INT
            {
                _acc = new Symbol();
                _acc.Token = _tok;
            }
            else
            {
                throw new Exception("SEMANTIC: wrong type of expression");
            }
        }

        public void AS_EB_IGUAL(Env _environment, Token _tok)
        {
            _typeBoolOper = (int)BoolOperator.IGUAL;
            _acc = new Symbol();
            _acc.Token = _tok;
        }

        public void AS_EB_DIFERENTE(Env _environment, Token _tok)
        {
            _typeBoolOper = (int)BoolOperator.DIFERENTE;
            _acc = new Symbol();
            _acc.Token = _tok;
        }

        public void AS_EB_11(Env _environment, Token _tok)
        {

        }

        public void AS_EB_12(Env _environment, Token _tok)
        {

        }

        public void AS_EB_13(Env _environment, Token _tok)
        {

        }

        public void AS_EB_14(Env _environment, Token _tok)
        {

        }

        public void AS_EB_15(Env _environment, Token _tok)
        {

        }

        public void AS_EB_16(Env _environment, Token _tok)
        {

        }

        public void AS_EB_17(Env _environment, Token _tok)
        {

        }

        public void AS_EB_EXPARIT_2(Env _environment, Token _tok)
        {
            //Symbol _sym = _environment.GetSymbol(_tok);
            //_accType = _sym.Type;
        }

        public void AS_EB_19(Env _environment, Token _tok)
        {

        }

        #endregion EXPRESSAO BOOLEANA Automata

        #region TIPO

        public void AS_TIPO_NUM(Env _environment, Token _tok)
        {
            _acc = new Symbol();
            _acc.Token = _tok;

            if (((Word)_tok).Lexeme == TYPE_MVN_INT)
            {
                _acc.Type = TYPE_MVN_INT;
                _sizeVAR=2;
            }
            else if (((Word)_tok).Lexeme == TYPE_MVN_FLOAT)
            {
                _acc.Type = TYPE_MVN_FLOAT;
                _sizeVAR = 4;
            }
            else if (((Word)_tok).Lexeme == TYPE_MVN_BOOL)
            {
                _acc.Type = TYPE_MVN_BOOL;
                _sizeVAR = 1;
            }
        }

        public void AS_TIPO_STRING(Env _environment, Token _tok)
        {

        }

        public void AS_TIPO_STRUCT(Env _environment, Token _tok)
        {

        }

        public void AS_TIPO_ARRAY(Env _environment, Token _tok)
        {
            
            _acc.Type += "[";
        }

        public void AS_TIPO_7(Env _environment, Token _tok)
        {

        }

        public void AS_TIPO_ARRAYSIZE(Env _environment, Token _tok)
        {

            _sizeVAR = (_sizeVAR * ((Num)_tok).Value);
            _acc.Name = _sizeVAR.ToString();

        }

        public void AS_TIPO_9(Env _environment, Token _tok)
        {

        }

        public void AS_TIPO_ARRAYSEPARATOR(Env _environment, Token _tok)
        {
            _acc.Type += "][";
        }

        public void AS_TIPO_ENDARRAY(Env _environment, Token _tok)
        {
            if (_reg1 != null)
            {
                _reg1.Id = ((Word)_reg1.Token).Lexeme;
                _acc.Type += "]";
                if (_acc != null)
                {

                    _reg1.Type = _acc.Type;
                    _environment.AddSymbol(_reg1);
                    _out.WriteReservedArea("P" + _reg1.TargetName + " K " + _reg1.TargetName);
                    _out.WriteReservedArea(_reg1.TargetName + " $ " + _sizeVAR);
                }
            }

            _reg1 = null;
        }

        public void AS_TIPO_12(Env _environment, Token _tok)
        {

        }

        public void AS_TIPO_13(Env _environment, Token _tok)
        {

        }

        public void AS_TIPO_14(Env _environment, Token _tok)
        {

        }

        public void AS_TIPO_15(Env _environment, Token _tok)
        {

        }

        public void AS_TIPO_16(Env _environment, Token _tok)
        {

        }

        public void AS_TIPO_17(Env _environment, Token _tok)
        {

        }


        #endregion TIPO Automata

        #endregion ACOES SEMANTICAS

        private void resultado_booleano(string _comment)
        {
            switch (_typeBoolOper)
            {
                case (int)BoolOperator.MAIOR:
                    {
                        _out.WriteCommentedCode("LD " + REG1, _comment);
                        _out.WriteCommentedCode("- " + REG2, "COD COMPLEMENTAR BOOLEANO");
                        _out.WriteCommentedCode("JN " + LABELFALSE, "SE FOR MENOR, PULA");
                        _out.WriteCommentedCode("JZ " + LABELFALSE, "SE FOR IGUAL, PULA");
                        break;
                    }
                case (int)BoolOperator.MENOR:
                    {
                        _out.WriteCommentedCode("LD " + REG2, _comment);
                        _out.WriteCommentedCode("- " + REG1, "COD COMPLEMENTAR BOOLEANO");
                        _out.WriteCommentedCode("JN " + LABELFALSE, "SE FOR MENOR, PULA");
                        _out.WriteCommentedCode("JZ " + LABELFALSE, "SE FOR IGUAL, PULA");
                        break;
                    }
                case (int)BoolOperator.MAIORIGUAL:
                    {
                        _out.WriteCommentedCode("LD " + REG2, _comment);
                        _out.WriteCommentedCode("- " + REG1, "COD COMPLEMENTAR BOOLEANO");
                        _out.WriteCommentedCode("JN " + LABELFALSE, "SE FOR MENOR, PULA");

                        break;
                    }
                case (int)BoolOperator.MENORIGUAL:
                    {
                        _out.WriteCommentedCode("LD " + REG2, _comment);
                        _out.WriteCommentedCode("- " + REG1, "COD COMPLEMENTAR BOOLEANO");
                        _out.WriteCommentedCode("JN " + LABELFALSE, "SE FOR MENOR, PULA");
                        break;
                    }
                case (int)BoolOperator.IGUAL:
                    {
                        _out.WriteCommentedCode("LD " + REG1, _comment);
                        //_out.WriteCommentedCode("- " + _varReg2, "COD COMPLEMENTAR BOOLEANO");
                        //_out.WriteCommentedCode("JN " + _labelFalse, "SE FOR IGUAL, PULA");
                        break;
                    }
                case (int)BoolOperator.DIFERENTE:
                    {
                        _out.WriteCommentedCode("LD " + REG2, _comment);
                        _out.WriteCommentedCode("- " + REG1, "COD COMPLEMENTAR BOOLEANO");
                        _out.WriteCommentedCode("JZ " + LABELFALSE, "SE FOR IGUAL, PULA");
                        break;
                    }

            }
        }

        private void RealizaOperacaoBoolAritm(Symbol _accumulator, Symbol _sym)
        {

            string _codeLoadValue = "LD " + _sym.TargetName;
            if (_accumulator.Token != null)
            {
                switch (_accumulator.Token.tag)
                {
                    case (int)Operator.PLUS:
                        {
                            _out.WriteCommentedCode("+ " + _sym.TargetName, "AS_EA_ID");
                            break;
                        }
                    case (int)Operator.MINUS:
                        {
                            _out.WriteCommentedCode("- " + _sym.TargetName, "AS_EA_ID");
                            break;
                        }
                    case (int)Operator.TIMES:
                        {
                            _out.WriteCommentedCode("* " + _sym.TargetName, "AS_EA_ID");
                            break;
                        }
                    case (int)Operator.DIV:
                        {
                            _out.WriteCommentedCode("/ " + _sym.TargetName, "AS_EA_ID");
                            break;
                        }
                    case (int)Tag.IF:
                        {
                            _out.WriteCommentedCode("LD " + _sym.TargetName, "AS_EA_ID");
                            _reg1 = _sym; // guarda no registrador 
                            break;
                        }
                    case (int)Tag.WHILE:
                        {
                            _out.WriteCommentedCode(_codeLoadValue, "AS_EA_ID");
                            _reg1 = _sym; // guarda no registrador 
                            break;
                        }
                    case (int)Tag.NEQUAL: // char '!=' 
                        {
                            _out.WriteCommentedCode(_codeLoadValue, "AS_EA_ID");
                            break;
                        }
                    case (int)Tag.EQUAL: // ==
                        {
                            _out.WriteCommentedCode(_codeLoadValue, "AS_EA_ID");
                            break;
                        }
                    case 62: // char '>' 
                        {
                            _out.WriteCommentedCode(_codeLoadValue, "AS_EA_ID");
                            break;
                        }
                    case 60: // char '<'
                        {
                            _out.WriteCommentedCode(_codeLoadValue, "AS_EA_ID");
                            break;
                        }
                    case 61: //char '='
                        {
                            _out.WriteCommentedCode(_codeLoadValue, "AS_EA_ID");
                            break;
                        }
                    case (int)Tag.GEQUAL:
                        {
                            _out.WriteCommentedCode(_codeLoadValue, "AS_EA_ID");
                            break;
                        }

                    case (int)Tag.LEQUAL:
                        {
                            _out.WriteCommentedCode(_codeLoadValue, "AS_EA_NUM");
                            break;
                        }

                }
            }

        }

        private void RealizaOperacaoBoolAritmNumerico(Symbol _accumulator, Symbol _sym)
        {
            string _codeLoadValue = "LV =" + _sym.TargetName;
            if (_accumulator.Token != null)
            {
                switch (_accumulator.Token.tag)
                {
                    case (int)Operator.PLUS:
                        {
                            _out.WriteCommentedCode("MM " + REG2, "AS_EA_NUM"); // salva o que esta dentro
                            _out.WriteCommentedCode(_codeLoadValue, "AS_EA_NUM");
                            _out.WriteCommentedCode("MM " + REG3, "AS_EA_NUM"); // salva o que esta dentro
                            _out.WriteCommentedCode("LD " + REG2, "AS_EA_NUM"); // salva o que esta dentro
                            _out.WriteCommentedCode("+ " + REG3, "AS_EA_ID");
                            break;
                        }
                    case (int)Operator.MINUS:
                        {
                            _out.WriteCommentedCode("MM " + REG2, "AS_EA_NUM"); // salva o que esta dentro
                            _out.WriteCommentedCode(_codeLoadValue, "AS_EA_NUM");
                            _out.WriteCommentedCode("MM " + REG3, "AS_EA_NUM"); // salva o que esta dentro
                            _out.WriteCommentedCode("LD " + REG2, "AS_EA_NUM"); // salva o que esta dentro
                            _out.WriteCommentedCode("- " + REG3, "AS_EA_ID");
                            break;
                        }
                    case (int)Operator.TIMES:
                        {
                            _out.WriteCommentedCode("MM " + REG2, "AS_EA_NUM"); // salva o que esta dentro
                            _out.WriteCommentedCode(_codeLoadValue, "AS_EA_NUM");
                            _out.WriteCommentedCode("* " + REG2, "AS_EA_ID");
                            break;
                        }
                    case (int)Operator.DIV:
                        {
                            _out.WriteCommentedCode("MM " + REG2, "AS_EA_NUM"); // salva o que esta dentro
                            _out.WriteCommentedCode(_codeLoadValue, "AS_EA_NUM");
                            _out.WriteCommentedCode("MM " + REG3, "AS_EA_NUM"); // salva o que esta dentro
                            _out.WriteCommentedCode("LD " + REG2, "AS_EA_NUM"); // salva o que esta dentro
                            _out.WriteCommentedCode("/ " + REG3, "AS_EA_ID");
                            break;
                        }
                    case (int)Tag.IF:
                        {
                            _out.WriteCommentedCode(_codeLoadValue, "AS_EA_ID");
                            _reg1 = _sym; // guarda no registrador 
                            break;
                        }
                    case (int)Tag.WHILE:
                        {
                            _out.WriteCommentedCode(_codeLoadValue, "AS_EA_ID");
                            _reg1 = _sym; // guarda no registrador 
                            break;
                        }
                    case (int)Tag.NEQUAL: // char '!=' 
                        {
                            _out.WriteCommentedCode(_codeLoadValue, "AS_EA_NUM");
                            break;
                        }
                    case (int)Tag.EQUAL: // ==
                        {
                            _out.WriteCommentedCode(_codeLoadValue, "AS_EA_NUM");
                            break;
                        }
                    case 62: // char '>' 
                        {
                            _out.WriteCommentedCode(_codeLoadValue, "AS_EA_NUM");
                            break;
                        }
                    case 60: // char '<'
                        {
                            _out.WriteCommentedCode(_codeLoadValue, "AS_EA_NUM");
                            break;
                        }
                    case 61: //char '='
                        {
                            _out.WriteCommentedCode(_codeLoadValue, "AS_EA_NUM");
                            break;
                        }
                    case (int)Tag.GEQUAL:
                        {
                            _out.WriteCommentedCode(_codeLoadValue, "AS_EA_NUM");
                            break;
                        }

                    case (int)Tag.LEQUAL:
                        {
                            _out.WriteCommentedCode(_codeLoadValue, "AS_EA_NUM");
                            break;
                        }


                }
            }
        }

    }
}
