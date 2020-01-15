using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using SudokuDeTodos.Models.Game;

namespace SudokuDeTodos.Engine
{
    public class EngineGame 
    {
        private string exception = string.Empty;
        private int recuadro = -1;

        //LIMPIAR TEXTBOX
        public TextBox[,] SetearTextBoxLimpio(TextBox[,] cajaTexto)
        {
            if (cajaTexto[0, 0] == null) return cajaTexto;
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    cajaTexto[f, c].Text = string.Empty;
                }
            }
            return cajaTexto;
        }

        //ABRIR ARCHIVO
        public ArrayList AbrirValoresArchivo(string pathArchivo)
        {
            ArrayList arrText = new ArrayList();
            String sLine = string.Empty;
            try
            {
                System.IO.StreamReader objReader = new System.IO.StreamReader(pathArchivo);
                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    if (sLine != null) arrText.Add(sLine);
                }
                objReader.Close();
            }
            catch (Exception ex) 
            {
               exception= ex.ToString(); 
            }

            return arrText;
        }

        public string[,] SetValorIngresado(ArrayList arrText, string[,] valorIngresado)
        {
            valorIngresado = new string[9, 9];
            for (int f = 0; f <= 8; f++)
            {
                string[] lineaVector = arrText[f].ToString().Split('-');

                if (f >= 0 && f <= 8)
                {
                    if (lineaVector.Length != 9) return valorIngresado;
                    for (int columna = 0; columna <= 8; columna++)
                    {
                        if (lineaVector[columna] != EngineDataGame.Zero)
                        {
                            valorIngresado[f, columna] = lineaVector[columna];
                        }
                    }
                }

            }
            return valorIngresado;
        }

        public string[,] SetValorEliminado(ArrayList arrText, string[,] valorEliminado)
        {
            valorEliminado = new string[9, 9];
            for (int f = 0; f <= 17; f++)
            {
                string[] lineaVector = arrText[f].ToString().Split('-');

                if (f >= 9 && f <= 17)
                {
                    if (lineaVector.Length != 9) return valorEliminado;
                    for (int columna = 0; columna <= 8; columna++)
                    {
                        if (lineaVector[columna] != EngineDataGame.Zero)
                        {
                            valorEliminado[f - 9, columna] = lineaVector[columna];
                        }
                    }
                }

            }
            return valorEliminado;
        }

        public string[,] SetValorInicio(ArrayList arrText, string[,] valorInicio)
        {
            valorInicio = new string[9, 9];
            int fila = 0;
            for (int f = 0; f <= 26; f++)
            {
                if (f >= 18 && f <= 26)
                {
                    string[] lineaVector = arrText[f].ToString().Split('-');
                    if (lineaVector.Length != 9) return valorInicio;
                    for (int columna = 0; columna <= 8; columna++)
                    {
                        if (lineaVector[columna] != EngineDataGame.Zero)
                        {
                            valorInicio[fila, columna] = lineaVector[columna];
                        }
                    }
                    fila++;
                }
            }
            return valorInicio;
        }

        public string[,] SetValorSolucion(ArrayList arrText, string[,] valorSolucion)
        {
            int fila = 0;
            valorSolucion = new string[9, 9];
            for (int f = 0; f <= 35; f++)
            {
                if (f >= 27 && f <= 35)
                {
                    string[] lineaVector = arrText[f].ToString().Split('-');
                    if (lineaVector.Length != 9) return valorSolucion;
                    for (int columna = 0; columna <= 8; columna++)
                    {
                        if (lineaVector[columna] != EngineDataGame.Zero)
                        {
                            valorSolucion[fila, columna] = lineaVector[columna];
                        }
                    }
                    fila++;
                }
            }
            return valorSolucion;
        }

        public bool ExisteValorIngresado(string[,] plantilla)
        {
            bool existeValor = false;
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (plantilla[f, c] != null && plantilla[f, c] != string.Empty)
                    {
                        existeValor = true;
                        return existeValor;
                    }
                }
            }
            return existeValor;
        }


        public string[,] IgualarIngresadoInicio(string[,] vIngresado, string[,] vInicio)
        {

            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (vInicio[f, c] != null && vInicio[f, c] != string.Empty)
                    {
                        vIngresado[f, c] = vInicio[f, c];
                    }
                }
            }
            return vIngresado;
        }


        // METODOS NUMEROS + CANDIDATOS 
        public string[,] ElejiblesInstantaneos(string[,] valorIngresado, string[,] valorCandidato)
        {
             valorCandidato = new string[9, 9];
             System.Windows.Forms.ListBox enterRecuadro = new  System.Windows.Forms.ListBox();
             System.Windows.Forms.ListBox enterFila = new  System.Windows.Forms.ListBox();
             System.Windows.Forms.ListBox enterColumna = new  System.Windows.Forms.ListBox();
             System.Windows.Forms.ListBox elejiblesRecuadro = new  System.Windows.Forms.ListBox();
             System.Windows.Forms.ListBox elejiblesFila = new  System.Windows.Forms.ListBox();
             System.Windows.Forms.ListBox elejiblesColumna = new  System.Windows.Forms.ListBox();
             System.Windows.Forms.ListBox elejiblesCelda = new  System.Windows.Forms.ListBox();
            String valor = string.Empty;

            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (valorIngresado[f, c] == null || valorIngresado[f, c] == string.Empty)
                    {
                        int[] posicion = new int[2];
                        enterRecuadro.Items.Clear(); elejiblesRecuadro.Items.Clear();
                        enterFila.Items.Clear(); elejiblesFila.Items.Clear();
                        enterColumna.Items.Clear(); elejiblesColumna.Items.Clear();
                        elejiblesCelda.Items.Clear();
                        posicion = GetRecuadro(f, c);
                        enterRecuadro = IngresadoRecuadro(posicion[0], posicion[1], enterRecuadro, valorIngresado);
                        elejiblesRecuadro = CandidatosRecuadro(enterRecuadro);
                        enterFila = IngresadoFila(f, enterFila, valorIngresado);
                        elejiblesFila = CandidatosFila(enterFila);
                        enterColumna = IngresadoColumna(c, enterColumna, valorIngresado);
                        elejiblesColumna = CandidatosColumna(enterColumna);
                        elejiblesCelda = ElejiblesDefinitivo(elejiblesRecuadro, elejiblesFila, elejiblesColumna);
                        valor = string.Empty; int indic = 1;
                        foreach (String v in elejiblesCelda.Items)
                        {
                            if (indic == 3 || indic == 6 || indic == 9)
                            {
                                valor = valor + " " + v + Environment.NewLine;
                            }
                            else
                            {
                                valor = valor + " " + v.Trim();
                            }
                            indic++;
                        }
                        valorCandidato[f, c] = valor;

                    }
                    else
                    {
                        valorCandidato[f, c] = valorIngresado[f, c];
                    }
                }
            }

            return valorCandidato;
        }

        private int[] GetRecuadro(int f, int c)
        {
            int[] p = new int[2];
            if ((f >= 0 && f <= 2) && (c >= 0 && c <= 2)) { p[0] = 0; p[1] = 0; recuadro = 0; }
            else if ((f >= 0 && f <= 2) && (c >= 3 && c <= 5)) { p[0] = 0; p[1] = 3; recuadro = 1; }
            else if ((f >= 0 && f <= 2) && (c >= 6 && c <= 8)) { p[0] = 0; p[1] = 6; recuadro = 2; }
            else if ((f >= 3 && f <= 5) && (c >= 0 && c <= 2)) { p[0] = 3; p[1] = 0; recuadro = 3; }
            else if ((f >= 3 && f <= 5) && (c >= 3 && c <= 5)) { p[0] = 3; p[1] = 3; recuadro = 4; }
            else if ((f >= 3 && f <= 5) && (c >= 6 && c <= 8)) { p[0] = 3; p[1] = 6; recuadro = 5; }
            else if ((f >= 6 && f <= 8) && (c >= 0 && c <= 2)) { p[0] = 6; p[1] = 0; recuadro = 6; }
            else if ((f >= 6 && f <= 8) && (c >= 3 && c <= 5)) { p[0] = 6; p[1] = 3; recuadro = 7; }
            else if ((f >= 6 && f <= 8) && (c >= 6 && c <= 8)) { p[0] = 6; p[1] = 6; recuadro = 8; }
            return p;
        }

        private System.Windows.Forms.ListBox IngresadoRecuadro(int fila, int columna, System.Windows.Forms.ListBox listaRecuadro, string[,] valorIngresado)
        {
            listaRecuadro = new System.Windows.Forms.ListBox();
            for (int f = fila; f <= fila + 2; f++)
            {
                for (int c = columna; c <= columna + 2; c++)
                {
                    if (valorIngresado[f, c] != null && valorIngresado[f, c] != "")
                    {
                        listaRecuadro.Items.Add(valorIngresado[f, c]);
                    }
                }
            }
            return listaRecuadro;
        }

        private System.Windows.Forms.ListBox CandidatosRecuadro(System.Windows.Forms.ListBox listaRecuadro)
        {
            System.Windows.Forms.ListBox listaRecuadroElejible = new System.Windows.Forms.ListBox();
            for (int i = 1; i <= 9; i++)
            {
                if (!listaRecuadro.Items.Contains(i.ToString()))
                {
                    listaRecuadroElejible.Items.Add(i.ToString());
                }
            }
            return listaRecuadroElejible;
        }

        private System.Windows.Forms.ListBox IngresadoFila(int fila, System.Windows.Forms.ListBox listaFila, string[,] valorIngresado)
        {
            listaFila = new System.Windows.Forms.ListBox();
            for (int columna = 0; columna <= 8; columna++)
            {
                if (valorIngresado[fila, columna] != null && valorIngresado[fila, columna] != string.Empty)
                {
                    listaFila.Items.Add(valorIngresado[fila, columna]);
                }
            }
            return listaFila;
        }

        private System.Windows.Forms.ListBox CandidatosFila(System.Windows.Forms.ListBox listaFila)
        {
            System.Windows.Forms.ListBox listaFilaElejible = new System.Windows.Forms.ListBox();
            for (int i = 1; i <= 9; i++)
            {
                if (!listaFila.Items.Contains(i.ToString()))
                {
                    listaFilaElejible.Items.Add(i.ToString());
                }
            }
            return listaFilaElejible;
        }

        private System.Windows.Forms.ListBox IngresadoColumna(int columna, System.Windows.Forms.ListBox listaColumna, string[,] valorIngresado)
        {
            listaColumna = new System.Windows.Forms.ListBox();
            for (int fila = 0; fila <= 8; fila++)
            {
                if (valorIngresado[fila, columna] != null && valorIngresado[fila, columna] != string.Empty)
                {
                    listaColumna.Items.Add(valorIngresado[fila, columna]);
                }
            }
            return listaColumna;
        }


        private System.Windows.Forms.ListBox CandidatosColumna(System.Windows.Forms.ListBox listaColumna)
        {
            System.Windows.Forms.ListBox listaColumnaElejible = new System.Windows.Forms.ListBox();
            for (int i = 1; i <= 9; i++)
            {
                if (!listaColumna.Items.Contains(i.ToString()))
                {
                    listaColumnaElejible.Items.Add(i.ToString());
                }
            }
            return listaColumnaElejible;
        }

        private System.Windows.Forms.ListBox ElejiblesDefinitivo(System.Windows.Forms.ListBox listaR, System.Windows.Forms.ListBox listaF, System.Windows.Forms.ListBox listaC)
        {
            System.Windows.Forms.ListBox listaDefinitiva = new System.Windows.Forms.ListBox();
            for (int i = 1; i <= 9; i++)
            {
                if (listaR.Items.Contains(i.ToString()) && listaF.Items.Contains(i.ToString()) && listaC.Items.Contains(i.ToString()))
                {
                    listaDefinitiva.Items.Add(i.ToString());
                }
            }
            return listaDefinitiva;
        }

        // SIN ELIMINADOS
        public string[,] CandidatosSinEliminados(string[,] valorIngresado, string[,] valorCandidato, string[,] valorEliminado)
        {
            System.Windows.Forms.ListBox candidatosOrganizados = new  System.Windows.Forms.ListBox();
            System.Windows.Forms.ListBox eliminarOrganizados = new  System.Windows.Forms.ListBox();
            string[,] valorCandidatoSinEliminados = new string[9, 9];
            valorCandidato = new string[9, 9];
            string candidatosFC = string.Empty;
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (valorIngresado[f, c] == null || valorIngresado[f, c] == string.Empty)
                    {
                        if (valorEliminado[f, c] != null && valorEliminado[f, c] != string.Empty)
                        {
                            candidatosOrganizados.Items.Clear();
                            eliminarOrganizados.Items.Clear();
                            candidatosFC = valorCandidato[f, c];
                            candidatosOrganizados = OrganizarCandidatos(candidatosOrganizados, candidatosFC);
                            eliminarOrganizados = OrganizarLista(eliminarOrganizados, valorEliminado[f, c]);
                            candidatosOrganizados = QuitarEliminados(candidatosOrganizados, eliminarOrganizados);
                            valorCandidatoSinEliminados = EstablecerCandidatosSinEliminados(candidatosOrganizados, f, c, valorCandidatoSinEliminados);
                        }
                        else
                        {
                            valorCandidatoSinEliminados[f, c] = valorCandidato[f, c];
                        }
                    }
                    else
                    {
                        valorCandidatoSinEliminados[f, c] = valorIngresado[f, c];
                    }
                }
            }
            return valorCandidatoSinEliminados;
        }


        private System.Windows.Forms.ListBox OrganizarCandidatos(System.Windows.Forms.ListBox lista, string candidatosFC)
        {
            string[] item = new string [18];
            System.Windows.Forms.ListBox listaAux = new System.Windows.Forms.ListBox();
            if(candidatosFC != null)
            {
                candidatosFC = candidatosFC.Trim();
                item = candidatosFC.Split(' ');
            }
              
            for (int i = 0; i <= item.Length - 1; i++)
            {
                if (item[i] != null && item[i] != string.Empty)
                    listaAux.Items.Add(item[i].Trim());
            }
            foreach (string n in listaAux.Items)
            {
                if (n.Trim().Length == 1)
                {
                    lista.Items.Add(n.Trim());
                }
                if (n.Trim().Length > 1)
                {
                    lista.Items.Add(n.Substring(0, 1));
                    lista.Items.Add(n.Substring(2, 2));
                }
            }
            return lista;
        }

        private  System.Windows.Forms.ListBox OrganizarLista( System.Windows.Forms.ListBox lista, string cadena)
        {
             System.Windows.Forms.ListBox listaAux = new  System.Windows.Forms.ListBox();
            cadena = cadena.Trim();
            string[] item = cadena.Split(' ');
            for (int i = 0; i <= item.Length - 1; i++)
            {
                listaAux.Items.Add(item[i].Trim());
            }
            foreach (string n in listaAux.Items)
            {
                if (n.Trim().Length == 1) { lista.Items.Add(n.Trim()); }
                if (n.Trim().Length > 1)
                {
                    lista.Items.Add(n.Substring(0, 1));
                    lista.Items.Add(n.Substring(2, 2));
                }
            }
            return lista;
        }

        private  System.Windows.Forms.ListBox QuitarEliminados( System.Windows.Forms.ListBox candidatosOrganizados,  System.Windows.Forms.ListBox cadenaEliminado)
        {
            int index = -1;
             System.Windows.Forms.ListBox eliminados = new  System.Windows.Forms.ListBox();
            eliminados.Items.Clear();
            foreach (string valor in cadenaEliminado.Items)
            {
                index = candidatosOrganizados.FindString(valor.Trim());
                if (index > -1)
                {
                    eliminados.Items.Add(valor.Trim());
                    candidatosOrganizados.Items.RemoveAt(index);
                }
            }
            return candidatosOrganizados;
        }

        private string[,] EstablecerCandidatosSinEliminados( System.Windows.Forms.ListBox candidatosFinal, int f, int c, string[,] valorCandidatoSinEliminados)
        {
            if (candidatosFinal.Items.Count > 0)
            {
                string valor = string.Empty; int indic = 1;
                foreach (String v in candidatosFinal.Items)
                {
                    String I = v.Trim();
                    if (indic == 3 || indic == 6 || indic == 9)
                    {
                        valor = valor + " " + I + Environment.NewLine;
                    }
                    else
                    {
                        valor = valor + " " + I;
                    }
                    indic++;
                }
                valorCandidatoSinEliminados[f, c] = valor;
            }
            else
            {
                valorCandidatoSinEliminados[f, c] = string.Empty;
            }

            return valorCandidatoSinEliminados;
        }

        public TextBox[,] SetearTextBoxJuego(TextBox[,] cajaTexto, string[,] vIngresado, string[,] vInicio)
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    cajaTexto[f, c].ForeColor = Color.Blue;
                    cajaTexto[f, c].ReadOnly = false;
                    if (vIngresado[f, c] != null && vIngresado[f, c] != string.Empty)
                    {
                        cajaTexto[f, c].ForeColor = Color.Blue;
                        cajaTexto[f, c].Text = vIngresado[f, c];
                        //cajaTexto[f, c].Font = new Font(EngineData.TipoLetra, 20);
                        if (vInicio[f, c] != null && vInicio[f, c] != string.Empty)
                        {
                            cajaTexto[f, c].ForeColor = Color.Black;
                            cajaTexto[f, c].ReadOnly = true;
                        }

                    }
                    //cajaTexto[f, c].TextAlign = HorizontalAlignment.Center;
                }
            }
            return cajaTexto;
        }

        public TextBox[,] SetearTextBoxJuego(TextBox[,] cajaTexto, string[,] vIngresado, string[,] vCandidato, string[,] vInicio, Color colorA, Color colorB, float fontBig = 20, float fontSmall = 8, string lado = EngineDataGame.Right)
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (lado == EngineDataGame.Right)
                    {
                        if (vIngresado[f, c] != null && vIngresado[f, c] != string.Empty)
                        {
                            cajaTexto[f, c].Text = vIngresado[f, c];
                            //cajaTexto[f, c].Font = new Font(EngineData.TipoLetra, fontBig);
                            cajaTexto[f, c].ForeColor = colorA;
                        }
                        else if (vIngresado[f, c] == null || vIngresado[f, c] == string.Empty)
                        {
                            cajaTexto[f, c].Text = vCandidato[f, c];
                            //cajaTexto[f, c].Font = new Font(EngineData.TipoLetra, fontSmall);
                            cajaTexto[f, c].ForeColor = colorB;
                        }
                    }
                    else
                    {
                        if (vIngresado[f, c] != null && vIngresado[f, c] != string.Empty)
                        {
                            cajaTexto[f, c].Text = vIngresado[f, c];
                            //cajaTexto[f, c].Font = new Font(EngineData.TipoLetra, fontBig);
                            cajaTexto[f, c].ForeColor = colorA;
                            if (vInicio[f, c] != null && vInicio[f, c] != string.Empty)
                            {
                                cajaTexto[f, c].ForeColor = Color.Black;
                            }

                        }
                    }
                    //cajaTexto[f, c].TextAlign = HorizontalAlignment.Center;
                }
            }
            return cajaTexto;
        }

        public TextBox[,] SetearTextBoxNumeroEliminados(TextBox[,] cajaTexto, string[,] vIngresado, string[,] vEliminado)
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (vIngresado[f, c] != null && vIngresado[f, c] != string.Empty)
                    {
                        cajaTexto[f, c].Text = vIngresado[f, c];
                        //cajaTexto[f, c].Font = new Font(EngineData.TipoLetra, 20);
                        cajaTexto[f, c].ForeColor = Color.Lime;
                        //cajaTexto[f, c].TextAlign = HorizontalAlignment.Center;
                    }
                    else
                    {
                        if (vEliminado[f, c] != null && vEliminado[f, c] != string.Empty)
                        {
                            cajaTexto[f, c].Text = vEliminado[f, c];
                            //cajaTexto[f, c].Font = new Font(EngineData.TipoLetra, 8);
                            //cajaTexto[f, c].TextMode = TextBoxMode.MultiLine;
                            cajaTexto[f, c].ForeColor = Color.Red;
                            //cajaTexto[f, c].Columns = 3;
                            //cajaTexto[f, c].TextAlign = HorizontalAlignment.Left;
                        }
                        else
                        {
                            cajaTexto[f, c].Text = string.Empty;
                        }
                    }

                }
            }
            return cajaTexto;
        }



        public LetrasJuegoFEG SetLetrasJuegoFEG(int num, string[,] valorIngresado, string[,] valorCandidatoSinEliminados)
        {
            LetrasJuegoFEG letrasFEG = new LetrasJuegoFEG
            {
                F = num,
                E = 81 - num,
                G = ContadorCandidatos(valorIngresado, valorCandidatoSinEliminados)
            };
            return letrasFEG;
        }

        public int ContadorIngresado(string[,] valorIngresado)
        {
            int contadorIngresado = 0;
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (valorIngresado[f, c] != null && valorIngresado[f, c] != string.Empty) { contadorIngresado++; }
                }
            }
            return contadorIngresado;
        }
        public int ContadorCandidatos(string[,] valorIngresado, string[,] valorCandidatoSinEliminados)
        {
            int contadorCandidatos = 0;
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (valorIngresado[f, c] == null || valorIngresado[f, c] == string.Empty)
                    {
                       if (valorCandidatoSinEliminados[f, c] != null)
                        {
                            valorCandidatoSinEliminados[f, c] = valorCandidatoSinEliminados[f, c].Replace(" ", "");
                            contadorCandidatos = contadorCandidatos + valorCandidatoSinEliminados[f, c].Length;
                        }
                    }
                }
            }
            return contadorCandidatos;
        }


    }
}