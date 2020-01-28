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
    public partial class GameBOne : System.Web.UI.Page
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
        private int numeroFiltrado = 0;


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
            if (resultado)
            {
                ValorGame.valorInicio = valorInicio;
                ValorGame.valorIngresado = valorIngresado;
                ValorGame.valorEliminado = valorEliminado;
                ValorGame.valorSolucion = valorSolucion;
                SetearJuego(); //existe valor ingresado
            }
            else
            {
                ValorGame.valorInicio = valorInicio;
                valorIngresado = Game.IgualarIngresadoInicio(valorIngresado, valorInicio);
                valorCandidato = Game.ElejiblesInstantaneos(valorIngresado, valorCandidato);
                valorCandidatoSinEliminados = Game.CandidatosSinEliminados(valorIngresado, valorCandidato, valorEliminado);
                txtSudoku = Game.SetearTextBoxJuego(txtSudoku, valorIngresado, valorInicio);

                ValorGame.valorInicio = valorInicio;
                ValorGame.valorIngresado = valorIngresado;
                ValorGame.valorEliminado = valorEliminado;
                ValorGame.valorSolucion = valorSolucion;
            }
            ContadorIngresado();
        }

        private void SetearJuego()
        {
            valorCandidato = Game.ElejiblesInstantaneos(valorIngresado, valorCandidato);
            valorCandidatoSinEliminados = Game.CandidatosSinEliminados(valorIngresado, valorCandidato, valorEliminado);
            txtSudoku2 = Game.SetearTextBoxEliminados(txtSudoku2, valorEliminado);
            txtSudoku = Game.TextReadOnly(txtSudoku);
            ActualizarContadoresCandidatos();
        }

        private void ContadorIngresado()
        {
            ValorGame.contadorIngresado = Game.ContadorIngresado(valorIngresado);
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
            solo = Game.CandidatoSolo(valorIngresado, valorCandidatoSinEliminados);
            oculto = new string[27];
            System.Windows.Forms.ListBox valor = new System.Windows.Forms.ListBox();
            for (int f = 0; f <= 8; f++)
            {
                valor = Game.MapeoFilaCandidatoOcultoFila(valorIngresado, valorCandidatoSinEliminados, f);
                oculto = Game.SetearOcultoFila(oculto, valor, f, valorCandidatoSinEliminados);
                valor.Items.Clear();
                valor = Game.MapeoFilaCandidatoOcultoColumna(valorIngresado, valorCandidatoSinEliminados, f);
                oculto = Game.SetearOcultoColumna(oculto, valor, f, valorCandidatoSinEliminados);
                valor.Items.Clear();
                valor = Game.MapeoFilaCandidatoOcultoRecuadro(valorIngresado, valorCandidatoSinEliminados, f);
                oculto = Game.SetearOcultoRecuadro(oculto, valor, f, valorCandidatoSinEliminados);
                valor.Items.Clear();
            }
        }
        private void SetLetrasJuegoACB()
        {
            LetrasJuegoACB = Game.SetLetrasJuegoACB(solo, oculto);
            btnA.Text = LetrasJuegoACB.A.ToString();
            btnB.Text = LetrasJuegoACB.B.ToString();
            if (!LetrasJuegoACB.C)
                btnC.ImageUrl = "~/Content/imagen/Look.JPG";
            else
                btnC.ImageUrl = "~/Content/imagen/UnLook.JPG";
        }

        private LetrasJuegoFEG SetLetrasJuegoFEG()
        {
            LetrasJuegoFEG = Game.SetLetrasJuegoFEG(ValorGame.contadorIngresado, valorIngresado, valorCandidatoSinEliminados);
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
            return LetrasJuegoFEG;
        }

        private void ActualizarContadoresCandidatos()
        {
            lbl1.Text = Game.ContadorCandidatoEspecifico(EngineDataGame.uno, valorIngresado, valorCandidatoSinEliminados);
            lbl2.Text = Game.ContadorCandidatoEspecifico(EngineDataGame.dos, valorIngresado, valorCandidatoSinEliminados);
            lbl3.Text = Game.ContadorCandidatoEspecifico(EngineDataGame.tres, valorIngresado, valorCandidatoSinEliminados);
            lbl4.Text = Game.ContadorCandidatoEspecifico(EngineDataGame.cuatro, valorIngresado, valorCandidatoSinEliminados);
            lbl5.Text = Game.ContadorCandidatoEspecifico(EngineDataGame.cinco, valorIngresado, valorCandidatoSinEliminados);
            lbl6.Text = Game.ContadorCandidatoEspecifico(EngineDataGame.seis, valorIngresado, valorCandidatoSinEliminados);
            lbl7.Text = Game.ContadorCandidatoEspecifico(EngineDataGame.siete, valorIngresado, valorCandidatoSinEliminados);
            lbl8.Text = Game.ContadorCandidatoEspecifico(EngineDataGame.ocho, valorIngresado, valorCandidatoSinEliminados);
            lbl9.Text = Game.ContadorCandidatoEspecifico(EngineDataGame.nueve, valorIngresado, valorCandidatoSinEliminados);

            lbl1.Enabled = false;
            lbl2.Enabled = false;
            lbl3.Enabled = false;
            lbl4.Enabled = false;
            lbl5.Enabled = false;
            lbl6.Enabled = false;
            lbl7.Enabled = false;
            lbl8.Enabled = false;
            lbl9.Enabled = false;
        }

  
        protected void btn1_Click(object sender, EventArgs e)
        {
            AbrirJuego();
            Button btn = sender as Button;
            switch (btn.ID)
            {
                case (EngineDataGame.Btn1):
                    lbl1.Text = Game.ContadorCandidatoEspecifico(EngineDataGame.uno, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Game.SetearTextBoxCandidatoEspecifico(EngineDataGame.uno, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    numeroFiltrado = 1;
                    break;
                case (EngineDataGame.Btn2):
                    lbl2.Text = Game.ContadorCandidatoEspecifico(EngineDataGame.dos, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Game.SetearTextBoxCandidatoEspecifico(EngineDataGame.dos, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    numeroFiltrado = 2;
                    break;
                case (EngineDataGame.Btn3):
                    lbl3.Text = Game.ContadorCandidatoEspecifico(EngineDataGame.tres, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Game.SetearTextBoxCandidatoEspecifico(EngineDataGame.tres, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    numeroFiltrado = 3;
                    break;
                case (EngineDataGame.Btn4):
                    lbl4.Text = Game.ContadorCandidatoEspecifico(EngineDataGame.cuatro, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Game.SetearTextBoxCandidatoEspecifico(EngineDataGame.cuatro, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    numeroFiltrado = 4;
                    break;
                case (EngineDataGame.Btn5):
                    lbl5.Text = Game.ContadorCandidatoEspecifico(EngineDataGame.cinco, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Game.SetearTextBoxCandidatoEspecifico(EngineDataGame.cinco, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    numeroFiltrado = 5;
                    break;
                case (EngineDataGame.Btn6):
                    lbl6.Text = Game.ContadorCandidatoEspecifico(EngineDataGame.seis, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Game.SetearTextBoxCandidatoEspecifico(EngineDataGame.seis, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    numeroFiltrado = 6;
                    break;
                case (EngineDataGame.Btn7):
                    lbl7.Text = Game.ContadorCandidatoEspecifico(EngineDataGame.siete, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Game.SetearTextBoxCandidatoEspecifico(EngineDataGame.siete, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    numeroFiltrado = 7;
                    break;
                case (EngineDataGame.Btn8):
                    lbl8.Text = Game.ContadorCandidatoEspecifico(EngineDataGame.ocho, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Game.SetearTextBoxCandidatoEspecifico(EngineDataGame.ocho, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    numeroFiltrado = 8;
                    break;
                case (EngineDataGame.Btn9):
                    lbl9.Text = Game.ContadorCandidatoEspecifico(EngineDataGame.nueve, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Game.SetearTextBoxCandidatoEspecifico(EngineDataGame.nueve, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    numeroFiltrado = 9;
                    break;
            }
            valorFiltrado = ValorGame.GetNumFiltro();
        }

        protected void btnEE_Click(object sender, EventArgs e)
        {
            string lado = string.Empty;
            string txt = idTxt.Value.Replace("#","");

            if (txt.Contains('_'))
                lado = EngineDataGame.btnDerecha;
            else
                lado = EngineDataGame.btnIzquierda;

            int lnt = txt.Length;
            int row = Convert.ToInt32(txt.Substring(lnt-2,1));
            int col = Convert.ToInt32(txt.Substring(lnt-1,1)); 
            if (idTxt.Value == string.Empty || idTxt.Value == null) return;
           // AbrirJuego();
            Button btn = (Button)sender;
            switch (btn.ID)
            {
                case (EngineDataGame.eliminar):
                    if (lado != EngineDataGame.btnIzquierda) return;
                    string candidatoEliminar = number.Value;
                    if (candidatoEliminar == string.Empty) return;
                    if (valorEliminado[row, col] != null && valorEliminado[row, col] != string.Empty)
                    {
                        if (valorEliminado[row, col].Contains(candidatoEliminar))
                        {
                            ActualizarContadoresCandidatos();
                            SetearJuego();
                            return;
                        }
                    }

                    if (valorEliminado[row, col] != null && valorEliminado[row, col] != string.Empty)
                    {
                        valorEliminado[row, col] = valorEliminado[row, col].Trim() + " " + candidatoEliminar.Trim();
                    }
                    else
                    {
                        valorEliminado[row, col] = valorEliminado[row, col] + " " + candidatoEliminar.Trim();
                    }

                    valorEliminado[row, col] = Game.OrdenarCadena(valorEliminado[row, col]);
                    txtSudoku[row, col].BackColor = Color.White;
                    txtSudoku[row, col].Text = string.Empty;
                    ActualizarContadoresCandidatos();
                    SetearJuego();
                    break;
                case (EngineDataGame.restablecer):
                    if (lado != EngineDataGame.btnDerecha) return;
                    string candidatoRestablecer = number.Value.Trim();
                    if (candidatoRestablecer == string.Empty) return;
                    if (candidatoRestablecer.Length == 1)
                    {
                        if (valorEliminado[row, col] != null)
                            valorEliminado[row, col] = valorEliminado[row, col].Replace(candidatoRestablecer, "");
                    }
                    else if (candidatoRestablecer.Length > 1)
                    {
                        if (valorEliminado[row, col] != null)
                            valorEliminado[row, col] = valorEliminado[row, col].Replace(candidatoRestablecer, "");
                    }

                    txtSudoku2[row, col].Text = valorEliminado[row, col];
                    ActualizarContadoresCandidatos();
                    SetearJuego();
                    ActualizarCandidato(candidatoRestablecer);
                    string[,] o = ValorGame.valorSolucion;
                    int m = 0;
                    break;
            }
            ContadorIngresado();
            Game.GuardarJuego(ValorGame.PathArchivo, valorIngresado, valorEliminado, valorInicio, valorSolucion);
        }

        private void ActualizarCandidato(string v)
        {
            switch (v)
            {
                case (EngineDataGame.uno):
                    lbl1.Text = Game.ContadorCandidatoEspecifico(EngineDataGame.uno, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Game.SetearTextBoxCandidatoEspecifico(EngineDataGame.uno, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    break;
                case (EngineDataGame.dos):
                    lbl2.Text = Game.ContadorCandidatoEspecifico(EngineDataGame.dos, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Game.SetearTextBoxCandidatoEspecifico(EngineDataGame.dos, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    break;
                case (EngineDataGame.tres):
                    lbl3.Text = Game.ContadorCandidatoEspecifico(EngineDataGame.tres, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Game.SetearTextBoxCandidatoEspecifico(EngineDataGame.tres, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    break;
                case (EngineDataGame.cuatro):
                    lbl4.Text = Game.ContadorCandidatoEspecifico(EngineDataGame.cuatro, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Game.SetearTextBoxCandidatoEspecifico(EngineDataGame.cuatro, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    break;
                case (EngineDataGame.cinco):
                    lbl5.Text = Game.ContadorCandidatoEspecifico(EngineDataGame.cinco, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Game.SetearTextBoxCandidatoEspecifico(EngineDataGame.cinco, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    break;
                case (EngineDataGame.seis):
                    lbl6.Text = Game.ContadorCandidatoEspecifico(EngineDataGame.seis, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Game.SetearTextBoxCandidatoEspecifico(EngineDataGame.seis, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    break;
                case (EngineDataGame.siete):
                    lbl7.Text = Game.ContadorCandidatoEspecifico(EngineDataGame.siete, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Game.SetearTextBoxCandidatoEspecifico(EngineDataGame.siete, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    break;
                case (EngineDataGame.ocho):
                    lbl8.Text = Game.ContadorCandidatoEspecifico(EngineDataGame.ocho, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Game.SetearTextBoxCandidatoEspecifico(EngineDataGame.ocho, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    break;
                case (EngineDataGame.nueve):
                    lbl9.Text = Game.ContadorCandidatoEspecifico(EngineDataGame.nueve, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Game.SetearTextBoxCandidatoEspecifico(EngineDataGame.nueve, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    break;
            }
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
