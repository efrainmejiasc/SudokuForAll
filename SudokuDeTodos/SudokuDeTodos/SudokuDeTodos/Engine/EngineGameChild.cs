using SudokuDeTodos.Models.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SudokuDeTodos.Engine
{
    public class EngineGameChild : EngineGame, IEngineGameChild
    {
        private EngineDataGame ValorGame = EngineDataGame.Instance();
        private LetrasJuego LetrasJuego = new LetrasJuego();
        private LetrasJuegoACB LetrasJuegoACB = new LetrasJuegoACB();
        private LetrasJuegoFEG LetrasJuegoFEG = new LetrasJuegoFEG();
        private string[,] valorIngresado = new string[9, 9];//ARRAY CONTENTIVO DE LOS VALORES INGRESADOS 
        private string[,] valorCandidato = new string[9, 9];//ARRAY CONTENTIVO DE LOS VALORES CANDIDATOS 
        private string[,] valorEliminado = new string[9, 9];//ARRAY CONTENTIVO DE LOS VALORES ELIMINADOS
        private string[,] valorCandidatoSinEliminados = new string[9, 9];
        private string[,] valorInicio = new string[9, 9];
        private string[,] valorSolucion = new string[9, 9];
        private string[] solo = new string[27];
        private string[] oculto = new string[27];


        public ArrayList ReadFile()
        {
            ArrayList arrText = AbrirValoresArchivo(ValorGame.PathArchivo);
            return arrText;
        }

        public LetrasJuego _ContadorIngresado(bool contadorActivado)
        {
            ArrayList arrText = AbrirValoresArchivo(ValorGame.PathArchivo);
            valorIngresado = SetValorIngresado(arrText, valorIngresado);
            valorCandidato = ElejiblesInstantaneos(valorIngresado, valorCandidato);
            valorEliminado = SetValorEliminado(arrText, valorEliminado);
            valorInicio = SetValorInicio(arrText, valorInicio);
            valorSolucion = SetValorSolucion(arrText, valorSolucion);
            valorCandidatoSinEliminados = CandidatosSinEliminados(valorIngresado, valorCandidato, valorEliminado);

            this.LetrasJuego = new LetrasJuego();
            LetrasJuego.ContadorIngresado = ContadorIngresado(valorIngresado);
            _SetSoloOculto();
           LetrasJuego = _SetLetrasJuegoACB(LetrasJuego);
           LetrasJuego = _SetLetrasJuegoFEG(LetrasJuego);
            if (!contadorActivado)
            {
                LetrasJuego.BtnA = EngineDataGame.Falso;
                LetrasJuego.BtnB = EngineDataGame.Falso;
            }
            else
            {
                LetrasJuego.BtnA = EngineDataGame.Verdadero;
                LetrasJuego.BtnB = EngineDataGame.Verdadero;
            }
            return LetrasJuego;
        }

        private void _SetSoloOculto()
        {
            solo = CandidatoSolo(valorIngresado, valorCandidatoSinEliminados);
            oculto = new string[27];
            System.Windows.Forms.ListBox valor = new System.Windows.Forms.ListBox();
            for (int f = 0; f <= 8; f++)
            {
                valor = MapeoFilaCandidatoOcultoFila(valorIngresado, valorCandidatoSinEliminados, f);
                oculto = SetearOcultoFila(oculto, valor, f, valorCandidatoSinEliminados);
                valor.Items.Clear();
                valor = MapeoFilaCandidatoOcultoColumna(valorIngresado, valorCandidatoSinEliminados, f);
                oculto = SetearOcultoColumna(oculto, valor, f, valorCandidatoSinEliminados);
                valor.Items.Clear();
                valor = MapeoFilaCandidatoOcultoRecuadro(valorIngresado, valorCandidatoSinEliminados, f);
                oculto = SetearOcultoRecuadro(oculto, valor, f, valorCandidatoSinEliminados);
                valor.Items.Clear();
            }
        }
        private LetrasJuego _SetLetrasJuegoACB(LetrasJuego LetrasJuego)
        {
            LetrasJuego.LetrasJuegoACB = SetLetrasJuegoACB(solo, oculto);
            if (LetrasJuegoACB.A + LetrasJuegoACB.B > 0)
            {
                //LetrasJuego.BtnBB = EngineDataGame.Falso;
                LetrasJuego.BtnBB = EngineDataGame.Verdadero;
            }
            else
            {
                LetrasJuego.BtnBB = EngineDataGame.Verdadero;
            }
            if (!LetrasJuegoACB.C)
            {
                LetrasJuego.ImagenUrl = "~/Content/imagen/Look.JPG";
                LetrasJuego.BtnBB = EngineDataGame.Verdadero;
            }
            else
            {
                LetrasJuego.ImagenUrl = "~/Content/imagen/UnLook.JPG";
                //LetrasJuego.BtnBB = EngineDataGame.Falso;
                LetrasJuego.BtnBB = EngineDataGame.Verdadero;
            }
            return LetrasJuego;
        }


        private LetrasJuego _SetLetrasJuegoFEG(LetrasJuego LetrasJuego)
        {
            LetrasJuego.LetrasJuegoFEG = SetLetrasJuegoFEG(LetrasJuego.ContadorIngresado, valorIngresado, valorCandidatoSinEliminados);
            if (LetrasJuegoACB.A + LetrasJuegoACB.B == 0 && Visibilidad70(LetrasJuegoFEG.F))
            {
                LetrasJuego.BtnB = true;
            }
            else
            {
                //LetrasJuego.BtnB = EngineDataGame.Falso;
                LetrasJuego.BtnB = EngineDataGame.Verdadero;
            }
            LetrasJuego.BtnC = Visibilidad70(LetrasJuegoFEG.F);
            return LetrasJuego;
        }


    }
}