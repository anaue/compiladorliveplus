using System;
using System.IO;
using System.Text;

namespace CompilatorLivePlus.Sintatic
{
            class Sintatic
            {
                StreamReader sr;
                Input.Input inChain;
                //FileStream rulez;
                CompilatorLivePlus.Lexer.Lexer _lex;
                public CompilatorLivePlus.Symbols.Env _env;
                public CompilatorLivePlus.Symbols.Env _prevEnv;

                public Sintatic(CompilatorLivePlus.Lexer.Lexer lex)
                {
                    _lex = lex;
                    _env = new CompilatorLivePlus.Symbols.Env( null);
                       //rulez = new FileStream("regras-codigo.txt", FileMode.OpenOrCreate, FileAccess.Read);
                }

                public void Program()
                {
                    inChain = new CompilatorLivePlus.Input.Input();

                    while (!_lex.hasEnded())
                    {
                        Lexer.Token escan = _lex.scan();
                        inChain.Add(escan);
                    }

                    while (inChain.hasNext())
                    {
                        Lexer.Token _tok = inChain.getNext();

                        if (isOpenScope(_tok)) 
                        {// se for um token de abertura de block, salva o escopo e cria um novo
                            _prevEnv = _env;
                            _env = new CompilatorLivePlus.Symbols.Env(_env);
                            
                        }else if (isCloseScope(_tok))
                        {
                            _env = _prevEnv;
                            _prevEnv = _env.Previous;
                        }
                        _env.put(_tok, _tok.tag.ToString());
                    }
                }
                public bool isOpenScope(Lexer.Token _tok)
                {
                    switch (_tok.tag)
                    {
                        case (int)Lexer.Tag.PROGRAM:
                            {
                                _env.CloseScope = (int)Lexer.Tag.END;
                                return true;
                            }
                        case (int)Lexer.Tag.FUNCTION:
                            {
                                _env.CloseScope = (int)Lexer.Tag.ENDFUNCTION;
                                return true;
                            }
                        case (int)Lexer.Tag.SUB:
                            {
                                _env.CloseScope = (int)Lexer.Tag.ENDSUB;
                                return true;
                            }
                        case (int)Lexer.Tag.STRUCT:
                            {
                                _env.CloseScope = (int)Lexer.Tag.ENDSTRUCT;
                                return true;
                            }
                        case (int)Lexer.Tag.IF:
                            {
                                _env.CloseScope = (int)Lexer.Tag.ENDIF;
                                return true;
                            }
                        case (int)Lexer.Tag.WHILE:
                            {
                                _env.CloseScope = (int)Lexer.Tag.ENDLOOP;
                                return true;
                            }
                    }
                    return false;
                }

                public bool isCloseScope(Lexer.Token _tok)
                {
                    if (_prevEnv != null)
                        return _tok.tag == _prevEnv.CloseScope;
                    else
                        return false;
                }
    }
}
