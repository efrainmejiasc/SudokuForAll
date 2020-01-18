using SudokuDeTodos.Engine;
using SudokuDeTodos.Models.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SudokuDeTodos.Vista
{
    public partial class GameOne : System.Web.UI.Page
    {
        private EngineGameChild Game = new EngineGameChild();
        private EngineDataGame ValorGame = EngineDataGame.Instance();
        private LetrasJuegoFEG LetrasJuegoFEG = new LetrasJuegoFEG();
        private LetrasJuegoACB LetrasJuegoACB = new LetrasJuegoACB();
        private TextBox[,] txtSudoku = new TextBox[9, 9]; //ARRAY CONTENTIVO DE LOS TEXTBOX DEL GRAFICO DEL SUDOKU
        private bool contadorActivado = false;
        private string[] solo = new string[27];
        private string[] oculto = new string[27];



        protected void Page_Load(object sender, EventArgs e)
        {     
            if (IsPostBack)
            {
            }
            else if (!IsPostBack)
            {
                ValorGame.PathArchivo = Server.MapPath("~/GameFile/test.jll");
                txtSudoku = AsociarTxtMatriz(txtSudoku);
                txtSudoku = Game.SetearTextBoxLimpio(txtSudoku);
                AbrirJuego();
            }
        }

        public void AbrirJuego()
        {
            bool resultado = Game.AbrirJuego(ValorGame.PathArchivo);
            if (resultado)
            {
                SetearJuego(); //existe valor ingresado
            }
            else
            {
                Game.IgualarIngresadoInicio(ValorGame.valorIngresado, ValorGame.valorInicio);
                Game.ElejiblesInstantaneos(ValorGame.valorIngresado, ValorGame.valorCandidato);
                Game.CandidatosSinEliminados(ValorGame.valorIngresado, ValorGame.valorCandidato, ValorGame.valorEliminado);
                txtSudoku = Game.SetearTextBoxJuego(txtSudoku, ValorGame.valorIngresado, ValorGame.valorInicio);
            }
            ContadorIngresado();
        }

        private void SetearJuego()
        {
            Game.ElejiblesInstantaneos(ValorGame.valorIngresado, ValorGame.valorCandidato);
            Game.CandidatosSinEliminados(ValorGame.valorIngresado, ValorGame.valorCandidato, ValorGame.valorEliminado);
            txtSudoku = Game.SetearTextBoxJuego(txtSudoku, ValorGame.valorIngresado, ValorGame.valorInicio);
        }

        private void ContadorIngresado()
        {
            ValorGame.contadorIngresado = Game.ContadorIngresado(ValorGame.valorIngresado);
            SetSoloOculto();
            SetLetrasJuegoACB();
            SetLetrasJuegoFEG();
            if (!contadorActivado)
            {
                btnA.Visible = false;
                btnB.Visible = false;
            }
            else
            {
                btnA.Visible = true;
                btnB.Visible = true;
            }
        }

        private void SetSoloOculto()
        {
            solo = Game.CandidatoSolo(ValorGame.valorIngresado, ValorGame.valorCandidatoSinEliminados);
            oculto = new string[27];
            System.Windows.Forms.ListBox valor = new System.Windows.Forms.ListBox ();
            for (int f = 0; f <= 8; f++)
            {
                valor = Game.MapeoFilaCandidatoOcultoFila(ValorGame.valorIngresado, ValorGame.valorCandidatoSinEliminados, f);
                oculto = Game.SetearOcultoFila(oculto, valor, f, ValorGame.valorCandidatoSinEliminados);
                valor.Items.Clear();
                valor = Game.MapeoFilaCandidatoOcultoColumna(ValorGame.valorIngresado, ValorGame.valorCandidatoSinEliminados, f);
                oculto = Game.SetearOcultoColumna(oculto, valor, f, ValorGame.valorCandidatoSinEliminados);
                valor.Items.Clear();
                valor = Game.MapeoFilaCandidatoOcultoRecuadro(ValorGame.valorIngresado, ValorGame.valorCandidatoSinEliminados, f);
                oculto = Game.SetearOcultoRecuadro(oculto, valor, f, ValorGame.valorCandidatoSinEliminados);
                valor.Items.Clear();
            }
        }

        private void SetLetrasJuegoACB()
        {
            LetrasJuegoACB = Game.SetLetrasJuegoACB(solo, oculto);
            btnA.Text = LetrasJuegoACB.A.ToString();
            btnB.Text = LetrasJuegoACB.B.ToString();
            if (LetrasJuegoACB.A + LetrasJuegoACB.B > 0)
            {
                //btnBB.Visible = EngineData.Falso;
                //btnBB.Visible = true;
            }
            else
            {
               // btnBB.Visible = EngineData.Verdadero;
            }
            if (!LetrasJuegoACB.C)
            {
                //btnC.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Look));
                //btnBB.Visible = EngineData.Verdadero;
            }
            else
            {
                //btnC.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.UnLook));
                //btnBB.Visible = EngineData.Falso;
                //btnBB.Visible = EngineData.Verdadero;
            }
        }


        private void SetLetrasJuegoFEG()
        {
            LetrasJuegoFEG = Game.SetLetrasJuegoFEG(ValorGame.contadorIngresado, ValorGame.valorIngresado, ValorGame.valorCandidatoSinEliminados);
            if (LetrasJuegoACB.A + LetrasJuegoACB.B == 0 && Game.Visibilidad70(LetrasJuegoFEG.F))
            {
                btnB.Visible = true;
            }
            else
            {
                btnB.Visible = false;
                btnB.Visible = true;

            }
            btnC.Visible = Game.Visibilidad70(LetrasJuegoFEG.F);
            btnF.Text = LetrasJuegoFEG.F.ToString();
            btnE.Text = LetrasJuegoFEG.E.ToString();
            btnG.Text = LetrasJuegoFEG.G.ToString();
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

    }
}