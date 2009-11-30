using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompilerModel.APE;
using CompilerModel.Lexer;
using CompilerModel.Symbols;
using CompilerModel.Structures;
using System.Reflection;
using CompilerModel.Semantic;

namespace APE
{
    public class Recognizer
    {
        private StackAutomaton _ape;
        public State CurrentState;
        public Automaton CurrentAutomaton;
        private Stack _stack;
        public SemanticActions Semantic;

        public StackAutomaton Ape
        {
            get { return _ape; }
            set { _ape = value; }
        }

        public Stack Stack
        {
            get { return _stack; }
            set { _stack = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ape">Stack Automaton to be run</param>
        public Recognizer(StackAutomaton ape, String outputFile)
        {
            _ape = ape;
            _stack = new Stack();
            Semantic = new SemanticActions(outputFile);
            CurrentAutomaton = _ape.Automata.Find(In=>In.Name == ape.Start.Name);
            CurrentState = CurrentAutomaton.States.Find(In => In.Id == CurrentAutomaton.Start.Id);
        }

        /// <summary>
        /// Executa uma transicao, seja uma chamada de subrotina, seja uma transicao interna.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="nextToken"></param>
        /// <param name="currentEnvironment"></param>
        /// <returns></returns>
        public bool RunTransition(Token input, Token nextToken, Env currentEnvironment)
        {
            if (input == null)
                return false;
            //Preferencialmente, procura-se transicoes internas
            List<Transition> internalTransitions = CurrentState.Transitions.FindAll(In => In.GetType() != typeof(SubmachineCall));
            foreach (Transition tr in internalTransitions)
            {
                if (tr.Input.tag == input.tag) //Achou transicao interna que consome o token
                {
                    CurrentState = CurrentAutomaton.States.Find(In => In.Id == tr.NextState.Id);
                    
                    //Chamada da acao semantica
                    RunSemanticAction(tr.SemanticActionName, currentEnvironment, input);

                    if (CurrentState.FinalState && !CheckLookAhead(CurrentState, nextToken))
                    {
                        if (!_stack.Empty)
                        {
                            StackPair stackPair = (StackPair)_stack.Pop();
                            GoToSubmachine(stackPair.Automaton, stackPair.State);
                        }

                        return true;
                    }

                    return true;
                }
            } 
            
            //Nao achou nenhuma transicao interna para o token, tenta uma submaquina

            List<Transition> listSubmachineCall = CurrentState.Transitions.FindAll(In => In.GetType() == typeof(SubmachineCall));
            if (listSubmachineCall.Count > 0)
            {
                if (listSubmachineCall.Count == 1) //Encontrou uma submaquina para fazer a chamada. A executa.
                {
                    SubmachineCall call = ((SubmachineCall)listSubmachineCall[0]);

                    _stack.Push(new StackPair(CurrentAutomaton, call.NextState));
                    GoToSubmachine(call.CalledAutomaton);
                    RunTransition(input, nextToken, currentEnvironment);
                    //Chamada da acao semantica no retorno da submaquina
                    RunSemanticAction(call.SemanticActionName, currentEnvironment, input);
                    return true;
                }
                else
                //Ha' nao-determinismo pois ha mais de uma chamada de submaquina para esse estado. Tentar achar o destino olhando o follow.
                {
                    //Olha o FOLLOW
                    foreach (Transition tr in listSubmachineCall)
                    {
                        SubmachineCall sc = ((SubmachineCall)tr);
                        //Devo verificar se o proximo token condiz com uma das submaquinas.
                        if (sc.CalledAutomaton.Start.HasTranstionsForToken(input))
                        {
                            _stack.Push(new StackPair(CurrentAutomaton, sc.NextState));
                            GoToSubmachine(sc.CalledAutomaton);
                            RunTransition(input, nextToken, currentEnvironment);
                            return true;
                        }
                    }
                }
            }

            //Se nao achou transicao interna nem submaquina, verifica se e' estado final de uma
            //submaquina, caso contrario, retorna erro sintatico.
            if (CurrentState.FinalState && !CheckLookAhead(CurrentState, input))
            {
                if (!_stack.Empty)
                {
                    StackPair stackPair = (StackPair)_stack.Pop();
                    GoToSubmachine(stackPair.Automaton, stackPair.State);
                    RunTransition(input, nextToken, currentEnvironment);
                }

                return true;
            }
            return false;
        }

        /// <summary>
        /// Executa uma chamada de uma funcao semantica definida no APE atraves de Reflection
        /// </summary>
        /// <param name="semanticActionName">Nome da Acao semantica</param>
        /// <param name="currentEnvironment">Ambiente corrente</param>
        public void RunSemanticAction(String semanticActionName, Env currentEnvironment, Token input)
        {
            MethodInfo methodInfo = typeof(SemanticActions).GetMethod(semanticActionName,new[]{typeof(Env), typeof(Token)});
            // Use the instance to call the method without arguments
            methodInfo.Invoke(Semantic, new Object []{currentEnvironment, input});
            CompilerModel.Trace.Tracer.putLog("Called Method: " + semanticActionName, MethodInfo.GetCurrentMethod().ReflectedType.ToString());

        }

        /// <summary>
        /// Verifica se um estado possui transicao para um token - utilizado para lookahead.
        /// </summary>
        /// <param name="CurrentState">Analyzed State</param>
        /// <param name="nextToken">Transition Input Token</param>
        /// <returns>Retorna verdadeiro se transicao existe</returns>
        public bool CheckLookAhead(State CurrentState, Token nextToken)
        {
            if (nextToken != null)
                return CurrentState.Transitions.Exists(In => In.Input.Equals(nextToken));
            else
                return false;
        }

        /// <summary>
        /// Troca de submaquina com uma transicao vazia para um determinado estado
        /// </summary>
        /// <param name="automaton">Target automaton</param>
        /// <param name="state">Target automaton state</param>
        public void GoToSubmachine(Automaton automaton, State state)
        {
            CurrentAutomaton = automaton;
            CurrentState = state;
        }

        /// <summary>
        /// Troca de submaquina com uma transicao vazia para o estado inicial
        /// </summary>
        /// <param name="automaton">Target automaton</param>
        public void GoToSubmachine(Automaton automaton)
        {
            GoToSubmachine(automaton, automaton.Start);
        }


        /// <summary>
        /// Metodo de reconhecimento (TEST ONLY)
        /// </summary>
        /// <param name="chain"></param>
        /// <returns></returns>
        public bool Recognize(Input chain)
        {
            int i=0;
            bool error = false;
            //while (!(CurrentState.FinalState && i < chain.Length && !StateHasTransitionsForToken(CurrentState, chain[i])))
            while (!(CurrentState.FinalState && _stack.Empty))
            {
                //if (!RunTransition(chain[i], i < chain.Length - 1 ? chain[i + 1] : null))
                if (!RunTransition(chain.getNext(), chain.getLookAHead(), null))
                {
                    error = true;
                    break;
                }
                i++;
            }

            if (error)
                throw new ApplicationException("Syntax Error: Token Number " + i);
            else 
                return true;
        }
    }
}
