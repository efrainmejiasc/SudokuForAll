using SudokuDeTodos.Engine;
using SudokuDeTodos.Models.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SudokuDeTodos.Vista
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private EngineGameChild Game = new EngineGameChild();
        private EngineDataGame ValorGame = EngineDataGame.Instance();
        private LetrasJuegoFEG LetrasJuegoFEG = new LetrasJuegoFEG();
        private LetrasJuegoACB LetrasJuegoACB = new LetrasJuegoACB();
        private TextBox[,] txtSudoku = new TextBox[9, 9]; //ARRAY CONTENTIVO DE LOS TEXTBOX DEL GRAFICO DEL SUDOKU
        private TextBox[,] txtSudoku2 = new TextBox[9, 9];
        private string[,] valorIngresado = new string[9, 9];//ARRAY CONTENTIVO DE LOS VALORES INGRESADOS 
        private string[,] valorCandidato = new string[9, 9];//ARRAY CONTENTIVO DE LOS VALORES CANDIDATOS 
        private string[,] valorEliminado = new string[9, 9];//ARRAY CONTENTIVO DE LOS VALORES ELIMINADOS
        private string[,] valorCandidatoSinEliminados = new string[9, 9];
        private string[,] valorInicio = new string[9, 9];
        private string[,] valorSolucion = new string[9, 9];
        private bool contadorActivado = false;
        private string[] solo = new string[27];
        private string[] oculto = new string[27];
        private string[,] valorFiltrado = new string[9, 9];
        protected void Page_Load(object sender, EventArgs e)
        {
            txtSudoku = AsociarTxtMatriz(txtSudoku);
            txtSudoku2 = AsociarTxtMatriz2(txtSudoku2);
            txtSudoku = Game.SetearTextBoxLimpio(txtSudoku);
            txtSudoku2 = Game.SetearTextBoxLimpio(txtSudoku2);
            AbrirJuego();
        }
        public void AbrirJuego()
        {
            ArrayList arrText = Game.AbrirValoresArchivo(ValorGame.PathArchivo);
            valorIngresado = Game.SetValorIngresado(arrText, valorIngresado);
            valorEliminado = Game.SetValorEliminado(arrText, valorEliminado);
            valorInicio = Game.SetValorInicio(arrText, valorInicio);
            valorSolucion = Game.SetValorSolucion(arrText, valorSolucion);
            bool resultado = Game.ExisteValorIngresado(valorIngresado);
            SetearJuego();
        }

        private void SetearJuego()
        {
            valorCandidato = Game.ElejiblesInstantaneos(valorIngresado, valorCandidato);
            valorCandidatoSinEliminados = Game.CandidatosSinEliminados(valorIngresado, valorCandidato, valorEliminado);
            txtSudoku = Game.SetearTextBoxJuegoInicio(txtSudoku, valorSolucion, valorInicio);
            txtSudoku2 = Game.SetearTextBoxEliminados(txtSudoku2, valorEliminado);
        }

        protected void btnAA_Click(object sender, EventArgs e)
        {
            Response.Redirect("GameATwo.aspx");
        }
        private TextBox[,] AsociarTxtMatriz(TextBox[,] txtSudoku)
        {
            /////////////////////////////////////////////////////////////////////////////
            txtSudoku[0, 0] = txt00; txtSudoku[0, 1] = txt01; txtSudoku[0, 2] = txt02;
            txtSudoku[1, 0] = txt10; txtSudoku[1, 1] = txt11; txtSudoku[1, 2] = txt12;
            txtSudoku[2, 0] = txt20; txtSudoku[2, 1] = txt21; txtSudoku[2, 2] = txt22;

            txtSudoku[0, 3] = txt03; txtSudoku[0, 4] = txt04; txtSudoku[0, 5] = txt05;
            txtSudoku[1, 3] = txt13; txtSudoku[1, 4] = txt14; txtSudoku[1, 5] = txt15;
            txtSudoku[2, 3] = txt23; txtSudoku[2, 4] = txt24; txtSudoku[2, 5] = txt25;

            txtSudoku[0, 6] = txt06; txtSudoku[0, 7] = txt07; txtSudoku[0, 8] = txt08;
            txtSudoku[1, 6] = txt16; txtSudoku[1, 7] = txt17; txtSudoku[1, 8] = txt18;
            txtSudoku[2, 6] = txt26; txtSudoku[2, 7] = txt27; txtSudoku[2, 8] = txt28;
            ////////////////////////////////////////////////////////////////////////////
            txtSudoku[3, 0] = txt30; txtSudoku[3, 1] = txt31; txtSudoku[3, 2] = txt32;
            txtSudoku[4, 0] = txt40; txtSudoku[4, 1] = txt41; txtSudoku[4, 2] = txt42;
            txtSudoku[5, 0] = txt50; txtSudoku[5, 1] = txt51; txtSudoku[5, 2] = txt52;

            txtSudoku[3, 3] = txt33; txtSudoku[3, 4] = txt34; txtSudoku[3, 5] = txt35;
            txtSudoku[4, 3] = txt43; txtSudoku[4, 4] = txt44; txtSudoku[4, 5] = txt45;
            txtSudoku[5, 3] = txt53; txtSudoku[5, 4] = txt54; txtSudoku[5, 5] = txt55;

            txtSudoku[3, 6] = txt36; txtSudoku[3, 7] = txt37; txtSudoku[3, 8] = txt38;
            txtSudoku[4, 6] = txt46; txtSudoku[4, 7] = txt47; txtSudoku[4, 8] = txt48;
            txtSudoku[5, 6] = txt56; txtSudoku[5, 7] = txt57; txtSudoku[5, 8] = txt58;
            ////////////////////////////////////////////////////////////////////////////
            txtSudoku[6, 0] = txt60; txtSudoku[6, 1] = txt61; txtSudoku[6, 2] = txt62;
            txtSudoku[7, 0] = txt70; txtSudoku[7, 1] = txt71; txtSudoku[7, 2] = txt72;
            txtSudoku[8, 0] = txt80; txtSudoku[8, 1] = txt81; txtSudoku[8, 2] = txt82;

            txtSudoku[6, 3] = txt63; txtSudoku[6, 4] = txt64; txtSudoku[6, 5] = txt65;
            txtSudoku[7, 3] = txt73; txtSudoku[7, 4] = txt74; txtSudoku[7, 5] = txt75;
            txtSudoku[8, 3] = txt83; txtSudoku[8, 4] = txt84; txtSudoku[8, 5] = txt85;

            txtSudoku[6, 6] = txt66; txtSudoku[6, 7] = txt67; txtSudoku[6, 8] = txt68;
            txtSudoku[7, 6] = txt76; txtSudoku[7, 7] = txt77; txtSudoku[7, 8] = txt78;
            txtSudoku[8, 6] = txt86; txtSudoku[8, 7] = txt87; txtSudoku[8, 8] = txt88;
            ////////////////////////////////////////////////////////////////////////////

            return txtSudoku;
        }


        private TextBox[,] AsociarTxtMatriz2(TextBox[,] txtSudoku2)
        {
            /////////////////////////////////////////////////////////////////////////////
            txtSudoku2[0, 0] = txt_00; txtSudoku2[0, 1] = txt_01; txtSudoku2[0, 2] = txt_02;
            txtSudoku2[1, 0] = txt_10; txtSudoku2[1, 1] = txt_11; txtSudoku2[1, 2] = txt_12;
            txtSudoku2[2, 0] = txt_20; txtSudoku2[2, 1] = txt_21; txtSudoku2[2, 2] = txt_22;

            txtSudoku2[0, 3] = txt_03; txtSudoku2[0, 4] = txt_04; txtSudoku2[0, 5] = txt_05;
            txtSudoku2[1, 3] = txt_13; txtSudoku2[1, 4] = txt_14; txtSudoku2[1, 5] = txt_15;
            txtSudoku2[2, 3] = txt_23; txtSudoku2[2, 4] = txt_24; txtSudoku2[2, 5] = txt_25;

            txtSudoku2[0, 6] = txt_06; txtSudoku2[0, 7] = txt_07; txtSudoku2[0, 8] = txt_08;
            txtSudoku2[1, 6] = txt_16; txtSudoku2[1, 7] = txt_17; txtSudoku2[1, 8] = txt_18;
            txtSudoku2[2, 6] = txt_26; txtSudoku2[2, 7] = txt_27; txtSudoku2[2, 8] = txt_28;
            ////////////////////////////////////////////////////////////////////////////
            txtSudoku2[3, 0] = txt_30; txtSudoku2[3, 1] = txt_31; txtSudoku2[3, 2] = txt_32;
            txtSudoku2[4, 0] = txt_40; txtSudoku2[4, 1] = txt_41; txtSudoku2[4, 2] = txt_42;
            txtSudoku2[5, 0] = txt_50; txtSudoku2[5, 1] = txt_51; txtSudoku2[5, 2] = txt_52;

            txtSudoku2[3, 3] = txt_33; txtSudoku2[3, 4] = txt_34; txtSudoku2[3, 5] = txt_35;
            txtSudoku2[4, 3] = txt_43; txtSudoku2[4, 4] = txt_44; txtSudoku2[4, 5] = txt_45;
            txtSudoku2[5, 3] = txt_53; txtSudoku2[5, 4] = txt_54; txtSudoku2[5, 5] = txt_55;

            txtSudoku2[3, 6] = txt_36; txtSudoku2[3, 7] = txt_37; txtSudoku2[3, 8] = txt_38;
            txtSudoku2[4, 6] = txt_46; txtSudoku2[4, 7] = txt_47; txtSudoku2[4, 8] = txt_48;
            txtSudoku2[5, 6] = txt_56; txtSudoku2[5, 7] = txt_57; txtSudoku2[5, 8] = txt58;
            ////////////////////////////////////////////////////////////////////////////
            txtSudoku2[6, 0] = txt_60; txtSudoku2[6, 1] = txt_61; txtSudoku2[6, 2] = txt_62;
            txtSudoku2[7, 0] = txt_70; txtSudoku2[7, 1] = txt_71; txtSudoku2[7, 2] = txt_72;
            txtSudoku2[8, 0] = txt_80; txtSudoku2[8, 1] = txt_81; txtSudoku2[8, 2] = txt_82;

            txtSudoku2[6, 3] = txt_63; txtSudoku2[6, 4] = txt_64; txtSudoku2[6, 5] = txt_65;
            txtSudoku2[7, 3] = txt_73; txtSudoku2[7, 4] = txt_74; txtSudoku2[7, 5] = txt_75;
            txtSudoku2[8, 3] = txt_83; txtSudoku2[8, 4] = txt_84; txtSudoku2[8, 5] = txt_85;

            txtSudoku2[6, 6] = txt_66; txtSudoku2[6, 7] = txt_67; txtSudoku2[6, 8] = txt_68;
            txtSudoku2[7, 6] = txt_76; txtSudoku2[7, 7] = txt_77; txtSudoku2[7, 8] = txt_78;
            txtSudoku2[8, 6] = txt_86; txtSudoku2[8, 7] = txt_87; txtSudoku2[8, 8] = txt_88;
            ////////////////////////////////////////////////////////////////////////////

            return txtSudoku2;
        }

     
    }
}