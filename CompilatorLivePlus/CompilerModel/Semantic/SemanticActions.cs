using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompilerModel.Symbols;
using CompilerModel.Lexer;

namespace CompilerModel.Semantic
{
    public class SemanticActions
    {
        public SymbolTable Symbols;
        private Output _out;

        public SemanticActions(string outputFile)
        {
            Symbols = new SymbolTable();
            _out = new Output(outputFile);
        }

        public void SaveOutput()
        {
            _out.SaveFile();
        }

        #region COMANDO

        public void AS_COMANDO_1(Env environment)
        {
            
        }

        public void AS_COMANDO_2(Env environment)
        {

        }

        public void AS_COMANDO_3(Env environment)
        {

        }

        public void AS_COMANDO_4(Env environment)
        {
            
        }

        public void AS_COMANDO_5(Env environment)
        {
            
        }

        public void AS_COMANDO_6(Env environment)
        {
            
        }

        public void AS_COMANDO_7(Env environment)
        {
            
        }

        public void AS_COMANDO_8(Env environment)
        {
            
        }

        public void AS_COMANDO_9(Env environment)
        {
            
        }

        public void AS_COMANDO_10(Env environment)
        {
            
        }

        public void AS_COMANDO_11(Env environment)
        {
            
        }

        public void AS_COMANDO_12(Env environment)
        {
            
        }

        public void AS_COMANDO_13(Env environment)
        {
            
        }

        public void AS_COMANDO_14(Env environment)
        {
            
        }

        public void AS_COMANDO_15(Env environment)
        {
            
        }

        public void AS_COMANDO_16(Env environment)
        {
            
        }

        public void AS_COMANDO_17(Env environment)
        {
            
        }

        public void AS_COMANDO_18(Env environment)
        {
            
        }

        public void AS_COMANDO_19(Env environment)
        {
            
        }

        public void AS_COMANDO_20(Env environment)
        {
            
        }

        public void AS_COMANDO_21(Env environment)
        {
            
        }

        public void AS_COMANDO_22(Env environment)
        {
            
        }

        public void AS_COMANDO_23(Env environment)
        {
            
        }

        public void AS_COMANDO_24(Env environment)
        {
            
        }

        public void AS_COMANDO_25(Env environment)
        {
            
        }

        public void AS_COMANDO_26(Env environment)
        {
            
        }

        public void AS_COMANDO_27(Env environment)
        {
            
        }

        public void AS_COMANDO_28(Env environment)
        {
            
        }

        public void AS_COMANDO_29(Env environment)
        {
            
        }

        #endregion COMANDO Automata

        #region CODIGO

        public void AS_CODIGO_START(Env environment)
        {
            _out.WriteCommentedCode("@=0", "area de programa");
            _out.WriteCode("JP INICIO");
        }

        public void AS_CODIGO_2(Env environment)
        {

        }

        public void AS_CODIGO_3(Env environment)
        {

        }

        public void AS_CODIGO_4(Env environment)
        {

        }

        public void AS_CODIGO_5(Env environment)
        {

        }

        public void AS_CODIGO_6(Env environment)
        {

        }

        public void AS_CODIGO_7(Env environment)
        {

        }

        public void AS_CODIGO_8(Env environment)
        {

        }

        public void AS_CODIGO_9(Env environment)
        {

        }

        public void AS_CODIGO_10(Env environment)
        {

        }

        public void AS_CODIGO_11(Env environment)
        {

        }

        public void AS_CODIGO_12(Env environment)
        {

        }

        public void AS_CODIGO_13(Env environment)
        {

        }

        public void AS_CODIGO_14(Env environment)
        {

        }

        public void AS_CODIGO_15(Env environment)
        {

        }

        public void AS_CODIGO_16(Env environment)
        {

        }

        public void AS_CODIGO_17(Env environment)
        {

        }

        public void AS_CODIGO_18(Env environment)
        {

        }

        public void AS_CODIGO_19(Env environment)
        {

        }

        public void AS_CODIGO_20(Env environment)
        {

        }

        public void AS_CODIGO_21(Env environment)
        {

        }

        public void AS_CODIGO_22(Env environment)
        {

        }

        public void AS_CODIGO_23(Env environment)
        {

        }

        public void AS_CODIGO_24(Env environment)
        {

        }

        public void AS_CODIGO_25(Env environment)
        {

        }

        public void AS_CODIGO_26(Env environment)
        {

        }

        public void AS_CODIGO_27(Env environment)
        {

        }

        public void AS_CODIGO_28(Env environment)
        {

        }

        public void AS_CODIGO_29(Env environment)
        {

        }

        public void AS_CODIGO_30(Env environment)
        {
            
        }

        public void AS_CODIGO_31(Env environment)
        {

        }

        public void AS_CODIGO_32(Env environment)
        {

        }

        public void AS_CODIGO_33(Env environment)
        {

        }

        public void AS_CODIGO_34(Env environment)
        {

        }

        public void AS_CODIGO_35(Env environment)
        {

        }

        public void AS_CODIGO_36(Env environment)
        {

        }

        public void AS_CODIGO_37(Env environment)
        {

        }

        public void AS_CODIGO_38(Env environment)
        {

        }

        public void AS_CODIGO_39(Env environment)
        {

        }

        public void AS_CODIGO_40(Env environment)
        {

        }

        public void AS_CODIGO_41(Env environment)
        {

        }

        public void AS_CODIGO_42(Env environment)
        {

        }

        public void AS_CODIGO_43(Env environment)
        {

        }

        public void AS_CODIGO_44(Env environment)
        {

        }

        public void AS_CODIGO_45(Env environment)
        {

        }

        public void AS_CODIGO_46(Env environment)
        {

        }

        public void AS_CODIGO_47(Env environment)
        {

        }

        public void AS_CODIGO_48(Env environment)
        {

        }

        public void AS_CODIGO_49(Env environment)
        {

        }

        public void AS_CODIGO_50(Env environment)
        {

        }

        public void AS_CODIGO_51(Env environment)
        {

        }

        public void AS_CODIGO_52(Env environment)
        {

        }

        public void AS_CODIGO_53(Env environment)
        {

        }

        public void AS_CODIGO_54(Env environment)
        {

        }

        public void AS_CODIGO_55(Env environment)
        {

        }

        public void AS_CODIGO_56(Env environment)
        {

        }

        public void AS_CODIGO_57(Env environment)
        {

        }

        public void AS_CODIGO_58(Env environment)
        {

        }

        public void AS_CODIGO_59(Env environment)
        {

        }

        public void AS_CODIGO_60(Env environment)
        {

        }

        public void AS_CODIGO_61(Env environment)
        {

        }

        #endregion CODIGO Automata

        #region EXPRESSAO ARITMETICA

        public void AS_EA_1(Env environment)
        {

        }

        public void AS_EA_2(Env environment)
        {

        }

        public void AS_EA_3(Env environment)
        {

        }

        public void AS_EA_4(Env environment)
        {

        }

        public void AS_EA_5(Env environment)
        {

        }

        public void AS_EA_6(Env environment)
        {

        }

        public void AS_EA_7(Env environment)
        {

        }

        public void AS_EA_8(Env environment)
        {

        }

        public void AS_EA_9(Env environment)
        {

        }

        public void AS_EA_10(Env environment)
        {

        }

        public void AS_EA_11(Env environment)
        {

        }

        public void AS_EA_12(Env environment)
        {

        }

        public void AS_EA_13(Env environment)
        {

        }

        public void AS_EA_14(Env environment)
        {

        }

        public void AS_EA_15(Env environment)
        {

        }

        public void AS_EA_16(Env environment)
        {

        }

        public void AS_EA_17(Env environment)
        {

        }

        public void AS_EA_18(Env environment)
        {

        }

        public void AS_EA_19(Env environment)
        {

        }

        public void AS_EA_20(Env environment)
        {

        }

        public void AS_EA_21(Env environment)
        {

        }

        #endregion EXPRESSAO ARITMETICA Automata

        #region EXPRESSAO BOOLEANA

        public void AS_EB_1(Env environment)
        {

        }

        public void AS_EB_2(Env environment)
        {

        }

        public void AS_EB_3(Env environment)
        {

        }

        public void AS_EB_4(Env environment)
        {

        }

        public void AS_EB_5(Env environment)
        {

        }

        public void AS_EB_6(Env environment)
        {

        }

        public void AS_EB_7(Env environment)
        {

        }

        public void AS_EB_8(Env environment)
        {

        }

        public void AS_EB_9(Env environment)
        {

        }

        public void AS_EB_10(Env environment)
        {

        }

        public void AS_EB_11(Env environment)
        {

        }

        public void AS_EB_12(Env environment)
        {

        }

        public void AS_EB_13(Env environment)
        {

        }

        public void AS_EB_14(Env environment)
        {

        }

        public void AS_EB_15(Env environment)
        {

        }

        public void AS_EB_16(Env environment)
        {

        }

        public void AS_EB_17(Env environment)
        {

        }

        public void AS_EB_18(Env environment)
        {

        }

        public void AS_EB_19(Env environment)
        {

        }

        #endregion EXPRESSAO BOOLEANA Automata

        #region TIPO

        public void AS_TIPO_1(Env environment)
        {

        }

        public void AS_TIPO_2(Env environment)
        {

        }

        public void AS_TIPO_3(Env environment)
        {

        }

        public void AS_TIPO_4(Env environment)
        {

        }

        public void AS_TIPO_5(Env environment)
        {

        }

        public void AS_TIPO_6(Env environment)
        {

        }

        public void AS_TIPO_7(Env environment)
        {

        }

        public void AS_TIPO_8(Env environment)
        {

        }

        public void AS_TIPO_9(Env environment)
        {

        }

        public void AS_TIPO_10(Env environment)
        {

        }

        public void AS_TIPO_11(Env environment)
        {

        }

        public void AS_TIPO_12(Env environment)
        {

        }

        public void AS_TIPO_13(Env environment)
        {

        }

        public void AS_TIPO_14(Env environment)
        {

        }

        public void AS_TIPO_15(Env environment)
        {

        }

        public void AS_TIPO_16(Env environment)
        {

        }

        public void AS_TIPO_17(Env environment)
        {

        }


        #endregion TIPO Automata
    }
}
