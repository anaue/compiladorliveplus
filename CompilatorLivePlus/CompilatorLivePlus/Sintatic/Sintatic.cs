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
    class Sintatic
    {
        CompilerModel.Symbols.Input inputChain;
        //FileStream rulez;
        CompilatorLivePlus.Lexer.Lexer _lexer;
        public CompilerModel.Symbols.Env _environment;
        public CompilerModel.Symbols.Env _previousEnvironment;
        private string mvnPath;

        public Sintatic(CompilatorLivePlus.Lexer.Lexer lex, string mvnCode)
        {
            _lexer = lex;
            _environment = new CompilerModel.Symbols.Env(null);
            //rulez = new FileStream("regras-codigo.txt", FileMode.OpenOrCreate, FileAccess.Read);
            mvnPath = mvnCode;
        }

        public void Run()
        {

            //Automaton Parser
            APEParser parser = new APEParser();
            StackAutomaton automaton = parser.GetStackAutomaton();

            //Recognizer initialization
            Recognizer recognizer = new Recognizer(automaton, mvnPath);

            Console.WriteLine(":: Stack automaton parsed successfully. See log.txt for details.");
            Tracer.putLog(":: Automaton Parsed: \n" + automaton.ToString(), this.ToString());

            Console.Write(":: Lexer starting... ");

            //An input is used here to act returning tokens to sintatic as a buffer.
            inputChain = new CompilerModel.Symbols.Input();
            while (!_lexer.hasEnded())
            {
                Token escan = _lexer.scan();
                inputChain.Add(escan);
            }
            inputChain.resetNext();

            Console.WriteLine("Done!");

            //Recognizer                    
            Console.Write(":: Compiling... ");

            int i = 0; //Just a help to debug this code w/ conditional breakpoints, can be removed later.
            while (inputChain.hasNext())
            {
                Token currentToken, nextToken;

                currentToken = inputChain.getNext();
                nextToken = inputChain.getLookAHead();

                //ManageScopeEnvironment(currentToken);
                if (isOpenScope(currentToken))
                {
                    // se for um token de abertura de block, salva o escopo e cria um novo
                    _previousEnvironment = _environment;
                    _environment = new CompilerModel.Symbols.Env(_environment);
                    Tracer.putLog("Escopo Aberto", "Sintatic");
                }

                if (!recognizer.RunTransition(currentToken, nextToken, _environment))
                {
                    //ERRO!!
                    Console.WriteLine("\n\n!! Syntax Error: Line " + currentToken.Line + ", Token " + currentToken.tag + " (ASCII Code) not recognized.");
                    Console.WriteLine("!! Current Automaton: " + recognizer.CurrentAutomaton.Name);
                    Console.WriteLine("!! Current State: " + recognizer.CurrentState.Id);
                    Console.WriteLine("!! Stack: " + GetStackAutomatonNames(recognizer.Stack));
                    Console.WriteLine("!! i: " + i);

                    break;
                }
                if (isCloseScope(currentToken))
                {
                    _environment = _previousEnvironment;
                    _previousEnvironment = _environment.Previous;
                    Tracer.putLog("Escopo Fechado", "Sintatic");
                }

                i++;
            }

            if (recognizer.CurrentState.FinalState && recognizer.Stack.Empty)
            {
                recognizer.Semantic.SaveOutput();
                Console.WriteLine("Done!");
            }
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

        //private void ManageScopeEnvironment(Token _tok)
        //{
        //    if (isOpenScope(currentToken))
        //    {
        //        // se for um token de abertura de block, salva o escopo e cria um novo
        //        _previousEnvironment = _environment;
        //        _environment = new CompilerModel.Symbols.Env(_environment);
        //        Tracer.putLog("Escopo Aberto", "Sintatic");
        //    }
        //    else if (isCloseScope(_tok))
        //    {
        //        _environment = _previousEnvironment;
        //        _previousEnvironment = _environment.Previous;
        //        Tracer.putLog("Escopo Fechado", "Sintatic");
        //    }

        //    _environment.AddSymbol(_tok);
        //}

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
