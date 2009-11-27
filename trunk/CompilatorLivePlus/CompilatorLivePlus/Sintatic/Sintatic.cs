﻿using System;
using System.IO;
using System.Text;
using APE.Parser;
using APE;
using CompilerModel.Lexer;
using CompilerModel.Symbols;
using CompilerModel.APE;

namespace CompilatorLivePlus.Sintatic
{

            //{new Token("IF"), new Token("("), new Token("TRUE"), new Token(")"), new Token("BEGIN"), new Token};
            //Token[] chain = new Token[] {new Token("IF"), new Token("NUM"), new Token("<"),new Token("ID"), new Token("then"),
            //    new Token("ID"),new Token("="),new Token("ID"),new Token("-"),new Token("NUM"),new Token(";"),new Token("ID"),
            //    new Token("="),new Token("ID"),new Token("*"),new Token("NUM"),new Token(";"),new Token("ENDIF"), new Token(";")
            //};

            //Console.WriteLine("Accept: " + recognizer.Recognize(chain));


            class Sintatic
            {
                StreamReader sr;
                CompilerModel.Symbols.Input inChain;
                //FileStream rulez;
                CompilatorLivePlus.Lexer.Lexer _lex;
                public CompilerModel.Symbols.Env _env;
                public CompilerModel.Symbols.Env _prevEnv;

                public Sintatic(CompilatorLivePlus.Lexer.Lexer lex)
                {
                    _lex = lex;
                    _env = new CompilerModel.Symbols.Env( null);
                       //rulez = new FileStream("regras-codigo.txt", FileMode.OpenOrCreate, FileAccess.Read);
                }

                public void Run()
                {
                    APEParser parser = new APEParser();
                    StackAutomaton automaton = parser.GetStackAutomaton();
                    Console.Write(automaton.ToString());
                    
                    inChain = new CompilerModel.Symbols.Input();
                    
                    while (!_lex.hasEnded())
                    {
                        Token escan = _lex.scan();
                        inChain.Add(escan);
                    }

                    inChain.resetNext();
                    
                    Console.ReadLine();

                    Recognizer recognizer = new APE.Recognizer(automaton);
                    Console.WriteLine("Accept: " + recognizer.Recognize(inChain));


                    while (inChain.hasNext())
                    {
                        Token _tok = inChain.getNext();


                        if (isOpenScope(_tok)) 
                        {
                            // se for um token de abertura de block, salva o escopo e cria um novo
                            _prevEnv = _env;
                            _env = new CompilerModel.Symbols.Env(_env);
                            
                        }
                        else if (isCloseScope(_tok))
                        {
                            _env = _prevEnv;
                            _prevEnv = _env.Previous;
                        }
                        
                        _env.put(_tok, _tok.tag.ToString());

                        
                    }

                    
                }
                public bool isOpenScope(Token _tok)
                {
                    switch (_tok.tag)
                    {
                        case (int)Tag.PROGRAM:
                            {
                                _env.CloseScope = (int)Tag.END;
                                return true;
                            }
                        case (int)Tag.FUNCTION:
                            {
                                _env.CloseScope = (int)Tag.ENDFUNCTION;
                                return true;
                            }
                        case (int)Tag.SUB:
                            {
                                _env.CloseScope = (int)Tag.ENDSUB;
                                return true;
                            }
                        case (int)Tag.STRUCT:
                            {
                                _env.CloseScope = (int)Tag.ENDSTRUCT;
                                return true;
                            }
                        case (int)Tag.IF:
                            {
                                _env.CloseScope = (int)Tag.ENDIF;
                                return true;
                            }
                        case (int)Tag.WHILE:
                            {
                                _env.CloseScope = (int)Tag.ENDLOOP;
                                return true;
                            }
                    }
                    return false;
                }

                public bool isCloseScope(Token _tok)
                {
                    if (_prevEnv != null)
                        return _tok.tag == _prevEnv.CloseScope;
                    else
                        return false;
                }
    }
}
