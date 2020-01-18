using SudokuDeTodos.Models.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SudokuDeTodos.Engine
{
    public class EngineGameChild : EngineGame
    {
        private string[,] valorIngresado = new string[9, 9];//ARRAY CONTENTIVO DE LOS VALORES INGRESADOS 
        private string[,] valorCandidato = new string[9, 9];//ARRAY CONTENTIVO DE LOS VALORES CANDIDATOS 
        private string[,] valorEliminado = new string[9, 9];//ARRAY CONTENTIVO DE LOS VALORES ELIMINADOS
        private string[,] valorInicio = new string[9, 9];
        private string[,] valorSolucion = new string[9, 9];

        public bool AbrirJuego(string pathArchivo)
        {
            EngineDataGame ValorGame = EngineDataGame.Instance();
            bool resultado = false;
            ArrayList arrText = AbrirValoresArchivo(pathArchivo);
            ValorGame.valorIngresado = SetValorIngresado(arrText, valorIngresado);
            ValorGame.valorEliminado = SetValorEliminado(arrText, valorEliminado);
            ValorGame.valorInicio = SetValorInicio(arrText, valorInicio);
            ValorGame.valorSolucion = SetValorSolucion(arrText, valorSolucion);
            ValorGame.valorCandidato = ElejiblesInstantaneos(valorSolucion, valorCandidato);
            ValorGame.valorCandidatoSinEliminados = CandidatosSinEliminados(ValorGame.valorSolucion, ValorGame.valorCandidato, ValorGame.valorEliminado);
            resultado =  ExisteValorIngresado(valorIngresado);
            return resultado;
        }

    
    }
}