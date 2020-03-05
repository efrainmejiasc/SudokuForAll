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
    public partial class GameBTwo : System.Web.UI.Page
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
        private string circuito = string.Empty;
        private string obj2 = string.Empty;
        private EventArgs eve2 = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (System.Web.HttpContext.Current.Session["Email"] == null)
            //Response.Redirect("~/Home/Index");
            btnGrup.ImageUrl = "~/Content/imagen/Izq.JPG";
            btnGrup2.ImageUrl = "~/Content/imagen/Der.JPG";

            System.Web.HttpContext.Current.Session["PathArchivo"] = ValorGame.PathArchivo;
            txtSudoku = AsociarTxtMatriz(txtSudoku);
            txtSudoku2 = AsociarTxtMatriz2(txtSudoku2);
            txtSudoku = Game.SetearTextBoxLimpio(txtSudoku);
            txtSudoku2 = Game.SetearTextBoxLimpio(txtSudoku2);
            AbrirJuego();

            if (IsPostBack)
            {

            }
            else
            {
                txtNota2.Text = "0"; ;
            }
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
             Game.ContadorCandidatoEspecifico(EngineDataGame.uno, valorIngresado, valorCandidatoSinEliminados);
             Game.ContadorCandidatoEspecifico(EngineDataGame.dos, valorIngresado, valorCandidatoSinEliminados);
             Game.ContadorCandidatoEspecifico(EngineDataGame.tres, valorIngresado, valorCandidatoSinEliminados);
             Game.ContadorCandidatoEspecifico(EngineDataGame.cuatro, valorIngresado, valorCandidatoSinEliminados);
             Game.ContadorCandidatoEspecifico(EngineDataGame.cinco, valorIngresado, valorCandidatoSinEliminados);
             Game.ContadorCandidatoEspecifico(EngineDataGame.seis, valorIngresado, valorCandidatoSinEliminados);
             Game.ContadorCandidatoEspecifico(EngineDataGame.siete, valorIngresado, valorCandidatoSinEliminados);
             Game.ContadorCandidatoEspecifico(EngineDataGame.ocho, valorIngresado, valorCandidatoSinEliminados);
             Game.ContadorCandidatoEspecifico(EngineDataGame.nueve, valorIngresado, valorCandidatoSinEliminados);
        }
        protected void btnEE_Click(object sender, EventArgs e)
        {
            string lado = string.Empty;
            string txt = idTxt.Value.Replace("#", "");

            if (txt.Contains('_'))
                lado = EngineDataGame.btnDerecha;
            else
                lado = EngineDataGame.btnIzquierda;

            int lnt = txt.Length;
            int row = Convert.ToInt32(txt.Substring(lnt - 2, 1));
            int col = Convert.ToInt32(txt.Substring(lnt - 1, 1));
            if (idTxt.Value == string.Empty || idTxt.Value == null) return;

            Button btn = (Button)sender;
            switch (btn.ID)
            {
                case (EngineDataGame.eliminar):
                    if (lado != EngineDataGame.btnIzquierda) return;
                    if (txtNota.Value == string.Empty) return;
                    string candidatoEliminar = number.Value;
                    if (candidatoEliminar == string.Empty) return;
                    int[] fc = new int[2];
                    fc = ProcesoEliminar(row);
                    valorEliminado[fc[0], fc[1]] = valorEliminado[fc[0], fc[1]] + " " + candidatoEliminar;
                    valorEliminado[fc[0], fc[1]] = Game.OrdenarCadena(valorEliminado[fc[0], fc[1]]);
                    txtSudoku[row, col].BackColor = Color.White;
                    txtSudoku[row, col].Text = string.Empty;
                    SetearJuego();
                    GrupoActualizar();
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
                    SetearJuego();
                    ActualizarCandidato(candidatoRestablecer);
                    GrupoActualizar();
                    break;
            }
            ContadorIngresado();
            Game.GuardarJuego(ValorGame.PathArchivo, valorIngresado, valorEliminado, valorInicio, valorSolucion);
        }

        public int[] ProcesoEliminar(int row)
        {
            int[] fc = new int[2];
            string celda = string.Empty;
            if (row == 0) { celda = tC1.Text; }
            else if (row == 1) { celda = tC2.Text; }
            else if (row == 2) { celda = tC3.Text; }
            else if (row == 3) { celda = tC4.Text; }
            else if (row == 4) { celda = tC5.Text; }
            else if (row == 5) { celda = tC6.Text; }
            else if (row == 6) { celda = tC7.Text; }
            else if (row == 7) { celda = tC8.Text; }
            else if (row == 8) { celda = tC9.Text; }
            fc[0] = Convert.ToInt16(celda.Trim().Substring(0, 1)) - 1;
            fc[1] = Convert.ToInt16(celda.Trim().Substring(1, 1)) - 1;
            return fc;
        }

        private void GrupoActualizar()
        {
            string cadena = txtNota.Value;
            string obj = string.Empty;
            EventArgs eve = null;
            switch (cadena)
            {
                case ("F1"):
                    obj = "fila1ToolStripMenuItem";
                    FilaEstado_Click(obj, eve);
                    break;
                case ("F2"):
                    obj = "fila2ToolStripMenuItem";
                    FilaEstado_Click(obj, eve);
                    break;
                case ("F3"):
                    obj = "fila3ToolStripMenuItem";
                    FilaEstado_Click(obj, eve);
                    break;
                case ("F4"):
                    obj = "fila4ToolStripMenuItem";
                    FilaEstado_Click(obj, eve);
                    break;
                case ("F5"):
                    obj = "fila5ToolStripMenuItem";
                    FilaEstado_Click(obj, eve);
                    break;
                case ("F6"):
                    obj = "fila6ToolStripMenuItem";
                    FilaEstado_Click(obj, eve);
                    break;
                case ("F7"):
                    obj = "fila7ToolStripMenuItem";
                    FilaEstado_Click(obj, eve);
                    break;
                case ("F8"):
                    obj = "fila8ToolStripMenuItem";
                    FilaEstado_Click(obj, eve);
                    break;
                case ("F9"):
                    obj = "fila9ToolStripMenuItem";
                    FilaEstado_Click(obj, eve);
                    break;
                case ("C1"):
                    obj = "columna1ToolStripMenuItem";
                    ColumnaEstado_Click(obj, eve);
                    break;
                case ("C2"):
                    obj = "columna2ToolStripMenuItem";
                    ColumnaEstado_Click(obj, eve);
                    break;
                case ("C3"):
                    obj = "columna3ToolStripMenuItem";
                    ColumnaEstado_Click(obj, eve);
                    break;
                case ("C4"):
                    obj = "columna4ToolStripMenuItem";
                    ColumnaEstado_Click(obj, eve);
                    break;
                case ("C5"):
                    obj = "columna5ToolStripMenuItem";
                    ColumnaEstado_Click(obj, eve);
                    break;
                case ("C6"):
                    obj = "columna6ToolStripMenuItem";
                    ColumnaEstado_Click(obj, eve);
                    break;
                case ("C7"):
                    obj = "columna7ToolStripMenuItem";
                    ColumnaEstado_Click(obj, eve);
                    break;
                case ("C8"):
                    obj = "columna8ToolStripMenuItem";
                    ColumnaEstado_Click(obj, eve);
                    break;
                case ("C9"):
                    obj = "columna9ToolStripMenuItem";
                    ColumnaEstado_Click(obj, eve);
                    break;
                case ("R1"):
                    obj = "recuadro1ToolStripMenuItem";
                    EstadoRecuadro_Click(obj, eve);
                    break;
                case ("R2"):
                    obj = "recuadro2ToolStripMenuItem";
                    EstadoRecuadro_Click(obj, eve);
                    break;
                case ("R3"):
                    obj = "recuadro3ToolStripMenuItem";
                    EstadoRecuadro_Click(obj, eve);
                    break;
                case ("R4"):
                    obj = "recuadro4ToolStripMenuItem";
                    EstadoRecuadro_Click(obj, eve);
                    break;
                case ("R5"):
                    obj = "recuadro5ToolStripMenuItem";
                    EstadoRecuadro_Click(obj, eve);
                    break;
                case ("R6"):
                    obj = "recuadro6ToolStripMenuItem";
                    EstadoRecuadro_Click(obj, eve);
                    break;
                case ("R7"):
                    obj = "recuadro7ToolStripMenuItem";
                    EstadoRecuadro_Click(obj, eve);
                    break;
                case ("R8"):
                    obj = "recuadro8ToolStripMenuItem";
                    EstadoRecuadro_Click(obj, eve);
                    break;
                case ("R9"):
                    obj = "recuadro9ToolStripMenuItem";
                    EstadoRecuadro_Click(obj, eve);
                    break;
            }
        }

        private void ActualizarCandidato(string v)
        {
            switch (v)
            {
                case (EngineDataGame.uno):
                    Game.ContadorCandidatoEspecifico(EngineDataGame.uno, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Game.SetearTextBoxCandidatoEspecifico(EngineDataGame.uno, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    break;
                case (EngineDataGame.dos):
                   Game.ContadorCandidatoEspecifico(EngineDataGame.dos, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Game.SetearTextBoxCandidatoEspecifico(EngineDataGame.dos, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    break;
                case (EngineDataGame.tres):
                     Game.ContadorCandidatoEspecifico(EngineDataGame.tres, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Game.SetearTextBoxCandidatoEspecifico(EngineDataGame.tres, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    break;
                case (EngineDataGame.cuatro):
                     Game.ContadorCandidatoEspecifico(EngineDataGame.cuatro, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Game.SetearTextBoxCandidatoEspecifico(EngineDataGame.cuatro, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    break;
                case (EngineDataGame.cinco):
                    Game.ContadorCandidatoEspecifico(EngineDataGame.cinco, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Game.SetearTextBoxCandidatoEspecifico(EngineDataGame.cinco, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    break;
                case (EngineDataGame.seis):
                     Game.ContadorCandidatoEspecifico(EngineDataGame.seis, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Game.SetearTextBoxCandidatoEspecifico(EngineDataGame.seis, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    break;
                case (EngineDataGame.siete):
                     Game.ContadorCandidatoEspecifico(EngineDataGame.siete, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Game.SetearTextBoxCandidatoEspecifico(EngineDataGame.siete, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    break;
                case (EngineDataGame.ocho):
                     Game.ContadorCandidatoEspecifico(EngineDataGame.ocho, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Game.SetearTextBoxCandidatoEspecifico(EngineDataGame.ocho, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    break;
                case (EngineDataGame.nueve):
                    Game.ContadorCandidatoEspecifico(EngineDataGame.nueve, valorIngresado, valorCandidatoSinEliminados);
                    txtSudoku = Game.SetearTextBoxCandidatoEspecifico(EngineDataGame.nueve, txtSudoku, valorIngresado, valorCandidatoSinEliminados);
                    break;
            }
        }


        private void FilaEstado_Click(string sender, EventArgs e)
        {
            string nombreFila = string.Empty;
            ToolStripMenuItem miPlantilla = new ToolStripMenuItem();
            miPlantilla.Name = sender;
            int fila = -1;
            if (miPlantilla.Name == "fila1ToolStripMenuItem") { fila = 0; nombreFila = "Fila Nº 1"; txtNota.Value = "F1"; txtNota2.Text = "1"; }
            else if (miPlantilla.Name == "fila2ToolStripMenuItem") { fila = 1; nombreFila = "Fila Nº 2"; txtNota.Value = "F2"; txtNota2.Text = "2"; }
            else if (miPlantilla.Name == "fila3ToolStripMenuItem") { fila = 2; nombreFila = "Fila Nº 3"; txtNota.Value = "F3"; txtNota2.Text = "3"; }
            else if (miPlantilla.Name == "fila4ToolStripMenuItem") { fila = 3; nombreFila = "Fila Nº 4"; txtNota.Value = "F4"; txtNota2.Text = "4"; }
            else if (miPlantilla.Name == "fila5ToolStripMenuItem") { fila = 4; nombreFila = "Fila Nº 5"; txtNota.Value = "F5"; txtNota2.Text = "5"; }
            else if (miPlantilla.Name == "fila6ToolStripMenuItem") { fila = 5; nombreFila = "Fila Nº 6"; txtNota.Value = "F6"; txtNota2.Text = "6"; }
            else if (miPlantilla.Name == "fila7ToolStripMenuItem") { fila = 6; nombreFila = "Fila Nº 7"; txtNota.Value = "F7"; txtNota2.Text = "7"; }
            else if (miPlantilla.Name == "fila8ToolStripMenuItem") { fila = 7; nombreFila = "Fila Nº 8"; txtNota.Value = "F8"; txtNota2.Text = "8"; }
            else if (miPlantilla.Name == "fila9ToolStripMenuItem") { fila = 8; nombreFila = "Fila Nº 9"; txtNota.Value = "F9"; txtNota2.Text = "9"; }


            string[,] valorPlantilla = new string[9, 9];
            bool existeValor = Game.ExisteValorIngresado(valorIngresado);
            if (!existeValor)
            {
                valorPlantilla = Game.InicioPlantillaVacio(valorPlantilla);
                txtSudoku = Game.SetearPlantillaVacia(txtSudoku, valorPlantilla);
            }
            else
            {
                valorCandidato = Game.ElejiblesInstantaneos(valorIngresado, valorCandidato);
                valorPlantilla = Game.ObtenerSetearValoresFila(valorIngresado, valorCandidato, valorEliminado, fila);
                txtSudoku = Game.SetearPlantillaVacia(txtSudoku, valorPlantilla);
            }
            string[] f = new string[9];
            f = ValorGame.GetNumeroFila(fila);
            tC1.Text= f[0];
            tC2.Text = f[1];
            tC3.Text = f[2];
            tC4.Text = f[3];
            tC5.Text = f[4];
            tC6.Text = f[5];
            tC7.Text = f[6];
            tC8.Text = f[7];
            tC9.Text = f[8];
            txt00.BackColor = Color.WhiteSmoke;
        }

        private void ColumnaEstado_Click(string sender, EventArgs e)
        {
            string nombreColumna = string.Empty; 
            ToolStripMenuItem miPlantilla = new ToolStripMenuItem();
            miPlantilla.Name = sender;
            int columna = -1;
            if (miPlantilla.Name == "columna1ToolStripMenuItem") { columna = 0; nombreColumna = "Columna Nº 1"; txtNota.Value = "C1"; txtNota2.Text = "1"; }
            else if (miPlantilla.Name == "columna2ToolStripMenuItem") { columna = 1; nombreColumna = "Columna Nº 2"; txtNota.Value = "C2"; txtNota2.Text = "2"; }
            else if (miPlantilla.Name == "columna3ToolStripMenuItem") { columna = 2; nombreColumna = "Columna Nº 3"; txtNota.Value = "C3"; txtNota2.Text = "3"; }
            else if (miPlantilla.Name == "columna4ToolStripMenuItem") { columna = 3; nombreColumna = "Columna Nº 4"; txtNota.Value = "C4"; txtNota2.Text = "4"; }
            else if (miPlantilla.Name == "columna5ToolStripMenuItem") { columna = 4; nombreColumna = "Columna Nº 5"; txtNota.Value = "C5"; txtNota2.Text = "5"; }
            else if (miPlantilla.Name == "columna6ToolStripMenuItem") { columna = 5; nombreColumna = "Columna Nº 6"; txtNota.Value = "C6"; txtNota2.Text = "6"; }
            else if (miPlantilla.Name == "columna7ToolStripMenuItem") { columna = 6; nombreColumna = "Columna Nº 7"; txtNota.Value = "C7"; txtNota2.Text= "7"; }
            else if (miPlantilla.Name == "columna8ToolStripMenuItem") { columna = 7; nombreColumna = "Columna Nº 8"; txtNota.Value = "C8"; txtNota2.Text = "8"; }
            else if (miPlantilla.Name == "columna9ToolStripMenuItem") { columna = 8; nombreColumna = "Columna Nº 9"; txtNota.Value = "C9"; txtNota2.Text = "9"; }

            string[,] valorPlantilla = new string[9, 9];
            bool existeValor = Game.ExisteValorIngresado(valorIngresado);
            if (!existeValor)
            {
                valorPlantilla = Game.InicioPlantillaVacio(valorPlantilla);
                txtSudoku = Game.SetearPlantillaVacia(txtSudoku, valorPlantilla);
            }
            else
            {
                valorCandidato = Game.ElejiblesInstantaneos(valorIngresado, valorCandidato);
                valorPlantilla = Game.ObtenerSetearValoresColumna(valorIngresado, valorCandidato, valorEliminado, columna);
                txtSudoku = Game.SetearPlantillaVacia(txtSudoku, valorPlantilla);
            }

            string[] c = new string[9];
            c = ValorGame.GetNumeroColumna(columna);
            tC1.Text = c[0];
            tC2.Text = c[1];
            tC3.Text = c[2];
            tC4.Text = c[3];
            tC5.Text = c[4];
            tC6.Text = c[5];
            tC7.Text = c[6];
            tC8.Text = c[7];
            tC9.Text = c[8];
        }

        private void EstadoRecuadro_Click(string sender, EventArgs e)
        {
            string nombreRecuadro = string.Empty;
            ToolStripMenuItem miPlantilla = new ToolStripMenuItem();
            miPlantilla.Name = sender;
            int rangoF = -1, rangoC = -1, rec = -1;
            if (miPlantilla.Name == "recuadro1ToolStripMenuItem") { rangoF = 0; rangoC = 0; nombreRecuadro = "Recuadro Nº 1"; rec = 0; txtNota.Value = "R1"; txtNota2.Text = "1"; }
            else if (miPlantilla.Name == "recuadro2ToolStripMenuItem") { rangoF = 0; rangoC = 3; nombreRecuadro = "Recuadro Nº 2"; rec = 1; txtNota.Value = "R2"; txtNota2.Text = "2"; }
            else if (miPlantilla.Name == "recuadro3ToolStripMenuItem") { rangoF = 0; rangoC = 6; nombreRecuadro = "Recuadro Nº 3"; rec = 2; txtNota.Value = "R3"; txtNota2.Text = "3"; }
            else if (miPlantilla.Name == "recuadro4ToolStripMenuItem") { rangoF = 3; rangoC = 0; nombreRecuadro = "Recuadro Nº 4"; rec = 3; txtNota.Value = "R4"; txtNota2.Text= "4"; }
            else if (miPlantilla.Name == "recuadro5ToolStripMenuItem") { rangoF = 3; rangoC = 3; nombreRecuadro = "Recuadro Nº 5"; rec = 4; txtNota.Value = "R5"; txtNota2.Text = "5"; }
            else if (miPlantilla.Name == "recuadro6ToolStripMenuItem") { rangoF = 3; rangoC = 6; nombreRecuadro = "Recuadro Nº 6"; rec = 5; txtNota.Value = "R6"; txtNota2.Text = "6"; }
            else if (miPlantilla.Name == "recuadro7ToolStripMenuItem") { rangoF = 6; rangoC = 0; nombreRecuadro = "Recuadro Nº 7"; rec = 6; txtNota.Value = "R7"; txtNota2.Text = "7"; }
            else if (miPlantilla.Name == "recuadro8ToolStripMenuItem") { rangoF = 6; rangoC = 3; nombreRecuadro = "Recuadro Nº 8"; rec = 7; txtNota.Value = "R8"; txtNota2.Text = "8"; }
            else if (miPlantilla.Name == "recuadro9ToolStripMenuItem") { rangoF = 6; rangoC = 6; nombreRecuadro = "Recuadro Nº 9"; rec = 8; txtNota.Value = "R9"; txtNota2.Text = "9"; }

            string[,] valorPlantilla = new string[9, 9];
            bool existeValor = Game.ExisteValorIngresado(valorIngresado);
            if (!existeValor)
            {
                valorPlantilla = Game.InicioPlantillaVacio(valorPlantilla);
                txtSudoku = Game.SetearPlantillaVacia(txtSudoku, valorPlantilla);
            }
            else
            {
                valorCandidato = Game.ElejiblesInstantaneos(valorIngresado, valorCandidato);
                valorPlantilla = Game.ObtenerSetearValoresRecuadro(valorIngresado, valorCandidato, valorEliminado, rangoF, rangoC);
                txtSudoku = Game.SetearPlantillaVacia(txtSudoku, valorPlantilla);
            }

            string[] r = new string[9];
            r = ValorGame.GetNumeroRecuadro(rec);
            tC1.Text = r[0];
            tC2.Text = r[1];
            tC3.Text = r[2];
            tC4.Text = r[3];
            tC5.Text = r[4];
            tC6.Text = r[5];
            tC7.Text = r[6];
            tC8.Text = r[7];
            tC9.Text = r[8];
        }

        public class ToolStripMenuItem { 
            public string Name { get; set; }
        }

        protected void btnGrup_Click(object sender, ImageClickEventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["Circuito"] != null)
                circuito = System.Web.HttpContext.Current.Session["Circuito"].ToString();
            else
                return;
            string cadena = txtNota.Value;
            string obj = string.Empty;
            EventArgs eve = null;

            if (circuito == "FILA")
            {
                if (cadena == string.Empty || cadena.Contains("C") || cadena.Contains("R"))
                {
                    cadena = "F9";
                    txtNota.Value = cadena;
                    obj = "fila9ToolStripMenuItem";
                }
                else
                {
                    switch (cadena)
                    {
                        case ("F9"):
                            txtNota.Value = "F8";
                            obj = "fila8ToolStripMenuItem";
                            break;
                        case ("F8"):
                            txtNota.Value = "F7";
                            obj = "fila7ToolStripMenuItem";
                            break;
                        case ("F7"):
                            txtNota.Value = "F6";
                            obj = "fila6ToolStripMenuItem";
                            break;
                        case ("F6"):
                            txtNota.Value = "F5";
                            obj = "fila5ToolStripMenuItem";
                            break;
                        case ("F5"):
                            txtNota.Value = "F4";
                            obj = "fila4ToolStripMenuItem";
                            break;
                        case ("F4"):
                            txtNota.Value = "F3";
                            obj = "fila3ToolStripMenuItem";
                            break;
                        case ("F3"):
                            txtNota.Value = "F2";
                            obj = "fila2ToolStripMenuItem";
                            break;
                        case ("F2"):
                            txtNota.Value = "F1";
                            obj = "fila1ToolStripMenuItem";
                            break;
                        case ("F1"):
                            txtNota.Value = "F9";
                            obj = "fila9ToolStripMenuItem";
                            break;
                    }
                }
                FilaEstado_Click(obj, eve);
            }
            else if (circuito == "COLUMNA")
            {
                if (cadena == string.Empty || cadena.Contains("F") || cadena.Contains("R"))
                {
                    cadena = "C9";
                    txtNota.Value = cadena;
                    obj = "columna9ToolStripMenuItem";
                }
                else
                {
                    switch (cadena)
                    {
                        case ("C9"):
                            txtNota.Value = "C8";
                            obj = "columna8ToolStripMenuItem";
                            break;
                        case ("C8"):
                            txtNota.Value = "C7";
                            obj = "columna7ToolStripMenuItem";
                            break;
                        case ("C7"):
                            txtNota.Value = "C6";
                            obj = "columna6ToolStripMenuItem";
                            break;
                        case ("C6"):
                            txtNota.Value = "C5";
                            obj = "columna5ToolStripMenuItem";
                            break;
                        case ("C5"):
                            txtNota.Value = "C4";
                            obj = "columna4ToolStripMenuItem";
                            break;
                        case ("C4"):
                            txtNota.Value = "C3";
                            obj = "columna3ToolStripMenuItem";
                            break;
                        case ("C3"):
                            txtNota.Value = "C2";
                            obj = "columna2ToolStripMenuItem";
                            break;
                        case ("C2"):
                            txtNota.Value = "C1";
                            obj = "columna1ToolStripMenuItem";
                            break;
                        case ("C1"):
                            txtNota.Value = "C9";
                            obj = "columna9ToolStripMenuItem";
                            break;
                    }
                }
                ColumnaEstado_Click(obj, eve);
            }
            else if (circuito == "RECUADRO")
            {
                if (cadena == string.Empty || cadena.Contains("F") || cadena.Contains("C"))
                {
                    cadena = "R9";
                    txtNota.Value = cadena;
                    obj = "recuadro9ToolStripMenuItem";
                }
                else
                {
                    switch (cadena)
                    {
                        case ("R9"):
                            txtNota.Value = "R8";
                            obj = "recuadro8ToolStripMenuItem";
                            break;
                        case ("R8"):
                            txtNota.Value = "R7";
                            obj = "recuadro7ToolStripMenuItem";
                            break;
                        case ("R7"):
                            txtNota.Value = "R6";
                            obj = "recuadro6ToolStripMenuItem";
                            break;
                        case ("R6"):
                            txtNota.Value = "R5";
                            obj = "recuadro5ToolStripMenuItem";
                            break;
                        case ("R5"):
                            txtNota.Value = "R4";
                            obj = "recuadro4ToolStripMenuItem";
                            break;
                        case ("R4"):
                            txtNota.Value = "R3";
                            obj = "recuadro3ToolStripMenuItem";
                            break;
                        case ("R3"):
                            txtNota.Value = "R2";
                            obj = "recuadro2ToolStripMenuItem";
                            break;
                        case ("R2"):
                            txtNota.Value = "R1";
                            obj = "recuadro1ToolStripMenuItem";
                            break;
                        case ("R1"):
                            txtNota.Value = "R9";
                            obj = "recuadro9ToolStripMenuItem";
                            break;
                    }
                }
                EstadoRecuadro_Click(obj, eve);
            }
            else
            {

            }
            obj2 = obj;
            label1.Value = "filaRecuadroColumna";
            txtSudoku = Game.SetearTextColorInicio(txtSudoku);
        }


        protected void btnGrup2_Click(object sender, ImageClickEventArgs e)
        {

            if (System.Web.HttpContext.Current.Session["Circuito"] != null)
                circuito = System.Web.HttpContext.Current.Session["Circuito"].ToString();
            else
                return;

            string cadena = txtNota.Value;
            string obj = string.Empty;
            EventArgs eve = null;

            if (circuito == "FILA")
            {
                if (cadena == string.Empty || cadena.Contains("C") || cadena.Contains("R"))
                {
                    cadena = "F1";
                    txtNota.Value = cadena;
                    obj = "fila1ToolStripMenuItem";
                }
                else
                {
                    switch (cadena)
                    {
                        case ("F1"):
                            txtNota.Value = "F2";
                            obj = "fila2ToolStripMenuItem";
                            break;
                        case ("F2"):
                            txtNota.Value = "F3";
                            obj = "fila3ToolStripMenuItem";
                            break;
                        case ("F3"):
                            txtNota.Value = "F4";
                            obj = "fila4ToolStripMenuItem";
                            break;
                        case ("F4"):
                            txtNota.Value = "F5";
                            obj = "fila5ToolStripMenuItem";
                            break;
                        case ("F5"):
                            txtNota.Value = "F6";
                            obj = "fila6ToolStripMenuItem";
                            break;
                        case ("F6"):
                            txtNota.Value = "F7";
                            obj = "fila7ToolStripMenuItem";
                            break;
                        case ("F7"):
                            txtNota.Value = "F8";
                            obj = "fila8ToolStripMenuItem";
                            break;
                        case ("F8"):
                            txtNota.Value = "F9";
                            obj = "fila9ToolStripMenuItem";
                            break;
                        case ("F9"):
                            txtNota.Value = "F1";
                            obj = "fila1ToolStripMenuItem";
                            break;
                    }
                }
                FilaEstado_Click(obj, eve);
            }
            else if (circuito == "COLUMNA")
            {
                if (cadena == string.Empty || cadena.Contains("F") || cadena.Contains("R"))
                {
                    cadena = "C1";
                    txtNota.Value = cadena;
                    obj = "columna1ToolStripMenuItem";
                }
                else
                {
                    switch (cadena)
                    {
                        case ("C1"):
                            txtNota.Value = "C2";
                            obj = "columna2ToolStripMenuItem";
                            break;
                        case ("C2"):
                            txtNota.Value = "C3";
                            obj = "columna3ToolStripMenuItem";
                            break;
                        case ("C3"):
                            txtNota.Value = "C4";
                            obj = "columna4ToolStripMenuItem";
                            break;
                        case ("C4"):
                            txtNota.Value = "C5";
                            obj = "columna5ToolStripMenuItem";
                            break;
                        case ("C5"):
                            txtNota.Value = "C6";
                            obj = "columna6ToolStripMenuItem";
                            break;
                        case ("C6"):
                            txtNota.Value = "C7";
                            obj = "columna7ToolStripMenuItem";
                            break;
                        case ("C7"):
                            txtNota.Value = "C8";
                            obj = "columna8ToolStripMenuItem";
                            break;
                        case ("C8"):
                            txtNota.Value = "C9";
                            obj = "columna9ToolStripMenuItem";
                            break;
                        case ("C9"):
                            txtNota.Value = "C1";
                            obj = "columna1ToolStripMenuItem";
                            break;
                    }
                }
                ColumnaEstado_Click(obj, eve);
            }
            else if (circuito == "RECUADRO")
            {
                if (cadena == string.Empty || cadena.Contains("F") || cadena.Contains("C"))
                {
                    cadena = "R1";
                    txtNota.Value = cadena;
                    obj = "recuadro1ToolStripMenuItem";
                }
                else
                {
                    switch (cadena)
                    {
                        case ("R1"):
                            txtNota.Value = "R2";
                            obj = "recuadro2ToolStripMenuItem";
                            break;
                        case ("R2"):
                            txtNota.Value = "R3";
                            obj = "recuadro3ToolStripMenuItem";
                            break;
                        case ("R3"):
                            txtNota.Value = "R4";
                            obj = "recuadro4ToolStripMenuItem";
                            break;
                        case ("R4"):
                            txtNota.Value = "R5";
                            obj = "recuadro5ToolStripMenuItem";
                            break;
                        case ("R5"):
                            txtNota.Value = "R6";
                            obj = "recuadro6ToolStripMenuItem";
                            break;
                        case ("R6"):
                            txtNota.Value = "R7";
                            obj = "recuadro7ToolStripMenuItem";
                            break;
                        case ("R7"):
                            txtNota.Value = "R8";
                            obj = "recuadro8ToolStripMenuItem";
                            break;
                        case ("R8"):
                            txtNota.Value = "R9";
                            obj = "recuadro9ToolStripMenuItem";
                            break;
                        case ("R9"):
                            txtNota.Value = "R1";
                            obj = "recuadro1ToolStripMenuItem";
                            break;
                    }
                }
                EstadoRecuadro_Click(obj, eve);
            }
            else
            {


            }
            obj2 = obj;
            label1.Value = "filaRecuadroColumna";
            txtSudoku = Game.SetearTextColorInicio(txtSudoku);
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

        protected void btnAA_Click(object sender, EventArgs e)
        {
            Response.Redirect("GameATwo.aspx");
        }
    }
}