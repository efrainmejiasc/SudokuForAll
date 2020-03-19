using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SudokuDeTodos.Models.Sistema
{
    public class TagForms
    {
        public string Fila { get; set; }
        public string Numeros { get; set; }
        public string Columna { get; set; }
        public string Recuadro { get; set; }
        public string Solucion { get; set; }
        public string Candidatos { get; set; }
        public string JuegoNuevo { get; set; }
        public string CandidatosExcluidos { get; set; }
        public string CandidatosOrganizados{get;set; }
        public string CandidatosIndividuales { get; set; }
        public string NummerosYCandidatosExcluidos { get; set; }

    }
}