using System;
using System.IO;
using System.Text;
using APE.Parser;
using APE;
using CompilerModel.Lexer;
using CompilerModel.Symbols;
using CompilerModel.APE;
using CompilerModel.Trace;
using CompilerModel.Structures;

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
                CompilerModel.Symbols.Input inputChain;
                //FileStream rulez;
                CompilatorLivePlus.Lexer.Lexer _lexer;
                public CompilerModel.Symbols.Env _environment;
                public CompilerModel.Symbols.Env _previousEnvironment;

                public Sintatic(CompilatorLivePlus.Lexer.Lexer lex)
                {
                    _lexer = lex;
                    _environment = new CompilerModel.Symbols.Env( null);
                       //rulez = new FileStream("regras-codigo.txt", FileMode.OpenOrCreate, FileAccess.Read);
                }

                public void Run()
                {

                    //Automaton Parser
                    APEParser parser = new APEParser();
                    StackAutomaton automaton = parser.GetStackAutomaton();
                    
                    //Recognizer initialization
                    Recognizer recognizer = new Recognizer(automaton);

                    Console.WriteLine(":: Stack automaton parsed successfully. See log.txt for details.");
                    Tracer.putLog(":: Automaton Parsed: \n" + automaton.ToString(), this.ToString());
                    
                    Console.Write(":: Lexer running... ");

                    //An input is used here to act returning tokens to sintatic as a buffer.
                    inputChain = new CompilerModel.Symbols.Input();
                    while (!_lexer.hasEnded())
                    {
                        Token escan = _lexer.scan();
                        inputChain.Add(escan);
                    }
                    inputChain.resetNext();

                    Console.WriteLine(" Done!");

                    //Recognizer                    
                    Console.WriteLine(":: Sintatic will begin. Press any key to continue... ");
                    Console.ReadLine();

                    int i = 0; //Just a help to debug this code w/ conditional breakpoints, can be removed later.
                    while (inputChain.hasNext())
                    {
                        Token currentToken, nextToken;

                        currentToken = inputChain.getNext();
                        nextToken = inputChain.getLookAHead();

                        CheckScopeEnvironment(currentToken);

                        //Create a symbol for this token
                        Symbol symbol = new Symbol();
                        symbol.Id = currentToken.tag.ToString();
                        symbol.Token = currentToken;
                        recognizer.Semantic.Symbols.AddSymbol(symbol);

                        if (!recognizer.RunTransition(currentToken, nextToken, _environment))
                        {
                            //ERRO!!
                            Console.WriteLine("!! Syntax Error: Line " + currentToken.Line + ", Token " + currentToken.tag + " (ASCII Code) not recognized by sintatic parser.");
                            Console.WriteLine("!! Current Automaton: " + recognizer.CurrentAutomaton.Name);
                            Console.WriteLine("!! Current State: " + recognizer.CurrentState.Id);
                            Console.WriteLine("!! Stack: " + GetStackAutomatonNames(recognizer.Stack));
                            Console.WriteLine("!! i: "+i);

                            break;
                        }
                        i++;
                    }

                    if (recognizer.CurrentState.FinalState && recognizer.Stack.Empty)
                        Console.WriteLine("\n:: Code finally compiled.");
                    else
                        Console.WriteLine("!! Sytax errors detected. Recognizer terminated.");

                }

                private string GetStackAutomatonNames(Stack stack)
                {
                    StringBuilder sb = new StringBuilder();
                    while (!stack.Empty)
                    {
                        StackPair sp = ((StackPair)stack.Pop());
                        sb.Append("(" + sp.Automaton.Name + "," + sp.State.Id + ")");
                    }
                    return sb.ToString();
                }

                private void CheckScopeEnvironment(Token _tok)
                {
                    if (isOpenScope(_tok))
                    {
                        // se for um token de abertura de block, salva o escopo e cria um novo
                        _previousEnvironment = _environment;
                        _environment = new CompilerModel.Symbols.Env(_environment);

                    }
                    else if (isCloseScope(_tok))
                    {
                        _environment = _previousEnvironment;
                        _previousEnvironment = _environment.Previous;
                    }

                    _environment.put(_tok, _tok.tag.ToString());
                }

                public bool isOpenScope(Token _tok)
                {
                    switch (_tok.tag)
                    {
                        case (int)Tag.PROGRAM:
                            {
                                _environment.CloseScope = (int)Tag.END;
                                return true;
                            }
                        case (int)Tag.FUNCTION:
                            {
                                _environment.CloseScope = (int)Tag.ENDFUNCTION;
                                return true;
                            }
                        case (int)Tag.SUB:
                            {
                                _environment.CloseScope = (int)Tag.ENDSUB;
                                return true;
                            }
                        case (int)Tag.STRUCT:
                            {
                                _environment.CloseScope = (int)Tag.ENDSTRUCT;
                                return true;
                            }
                        case (int)Tag.IF:
                            {
                                _environment.CloseScope = (int)Tag.ENDIF;
                                return true;
                            }
                        case (int)Tag.WHILE:
                            {
                                _environment.CloseScope = (int)Tag.ENDLOOP;
                                return true;
                            }
                    }
                    return false;
                }

                public bool isCloseScope(Token _tok)
                {
                    if (_previousEnvironment != null)
                        return _tok.tag == _previousEnvironment.CloseScope;
                    else
                        return false;
                }
    }
}
