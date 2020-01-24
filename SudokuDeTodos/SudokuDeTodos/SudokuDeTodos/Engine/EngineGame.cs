using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using SudokuDeTodos.Models.Game;

namespace SudokuDeTodos.Engine
{
    public class EngineGame 
    {
        private EngineDataGame ValorGame = EngineDataGame.Instance();
        private EngineData Valor = EngineData.Instance();
        private int recuadro = -1;
        private int[] pos = new int[2];

        public bool CreateFile (string pathSource,string pathArchivo)
        {
            bool resultado = false;
            if (File.Exists(pathArchivo))
                resultado = true;
            else
                System.IO.File.Copy(pathSource, pathArchivo, true);

            return resultado;
        }

        // METODOS DEL JUEGO////////////////////////////////////////////////////////////////////
        public Button[] ColoresPincel(Button[] v)
        {
            v[0].BackColor = Color.Silver;
            v[1].BackColor = Color.SkyBlue;
            v[2].BackColor = Color.CornflowerBlue;
            v[3].BackColor = Color.LightCoral;
            v[4].BackColor = Color.Crimson;
            v[5].BackColor = Color.PaleGreen;
            v[6].BackColor = Color.YellowGreen;
            v[7].BackColor = Color.LightSalmon;
            v[8].BackColor = Color.Orange;
            return v;
        }

        public string[,] CandidatosJuego(string[,] vIngresado, string[,] valorCandidato)
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (vIngresado[f, c] == string.Empty || vIngresado[f, c] == null)
                        valorCandidato[f, c] = "1 2 3" + Environment.NewLine + "4 5 6" + Environment.NewLine + "7 8 9";
                    else
                        valorCandidato[f, c] = vIngresado[f, c];
                }
            }
            return valorCandidato;
        }

        public TextBox[,] SetearTextBoxJuego(TextBox[,] cajaTexto, string[,] vIngresado, string[,] vInicio)
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    cajaTexto[f, c].ForeColor = Color.Blue;
                    cajaTexto[f, c].ReadOnly = false;
                    cajaTexto[f, c].Attributes.Add("autocomplete", "off");
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

        public TextBox[,] SetearTextBoxJuegoInicio(TextBox[,] cajaTexto, string[,] vIngresado, string[,] vInicio)
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (vIngresado[f, c] != null && vIngresado[f, c] != string.Empty)
                    {
                        cajaTexto[f, c].Text = vIngresado[f, c];
                        //cajaTexto[f, c].Font = new Font(EngineDataGame.TipoLetra, 20);
                        cajaTexto[f, c].ForeColor = Color.Blue;
                        if (vInicio[f, c] != null && vInicio[f, c] != string.Empty)
                        {
                            cajaTexto[f, c].ForeColor = Color.Black;
                        }

                    }
                    //cajaTexto[f, c].TextAlign = HorizontalAlignment.Center;
                }
            }
            return cajaTexto;
        }

        public TextBox[,] SetearTextBoxJuegoNumerosIngresados(TextBox[,] cajaTexto, string[,] vIngresado, string[,] vInicio)
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (vIngresado[f, c] != null && vIngresado[f, c] != string.Empty)
                    {
                        if (vInicio[f, c] == null || vInicio[f, c] == string.Empty)
                        {
                            cajaTexto[f, c].Text = vIngresado[f, c];
                            //cajaTexto[f, c].Font = new Font(EngineDataGame.TipoLetra, 20);
                            cajaTexto[f, c].ForeColor = Color.Blue;
                        }
                    }
                    //cajaTexto[f, c].TextAlign = HorizontalAlignment.Center;
                }
            }
            return cajaTexto;
        }

        public TextBox[,] SetearTextParaNumerosN(TextBox[,] cajaTexto, string[,] vIngresado)
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    cajaTexto[f, c].BackColor = Color.WhiteSmoke;
                    if (vIngresado[f, c] != string.Empty || vIngresado[f, c] != null)
                    {
                        cajaTexto[f, c].Text = string.Empty;
                    }
                }
            }
            return cajaTexto;
        }

        public TextBox[,] SetearTextParaNumerosNConColorFiltro(TextBox[,] cajaTexto, string[,] vIngresado)
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (vIngresado[f, c] != string.Empty || vIngresado[f, c] != null)
                    {
                        cajaTexto[f, c].Text = string.Empty;
                    }
                }
            }
            return cajaTexto;
        }

        public TextBox[,] SetearTextBoxNumerosN(TextBox[,] cajaTexto, string[,] vIngresado, string[,] vInicio)
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (vInicio[f, c] == null || vInicio[f, c] == string.Empty)
                    {
                        if (vIngresado[f, c] != null && vIngresado[f, c] != string.Empty)
                        {
                            cajaTexto[f, c].Text = vIngresado[f, c];
                            //cajaTexto[f, c].Font = new Font(EngineDataGame.TipoLetra, 20);
                        }
                    }
                    //cajaTexto[f, c].TextAlign = HorizontalAlignment.Center;
                }
            }
            return cajaTexto;
        }

        public TextBox[,] SetearTextBoxJuegoNumerosInicioMasIngresados(TextBox[,] cajaTexto, string[,] vIngresado, string[,] vInicio)
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (vIngresado[f, c] != null && vIngresado[f, c] != string.Empty)
                    {
                        cajaTexto[f, c].Text = vIngresado[f, c];
                        cajaTexto[f, c].ForeColor = Color.Blue;
                    }
                    if (vInicio[f, c] != null && vInicio[f, c] != string.Empty)
                    {
                        cajaTexto[f, c].Text = vInicio[f, c];
                        cajaTexto[f, c].ForeColor = Color.Black;
                    }
                    //cajaTexto[f, c].Font = new Font(EngineDataGame.TipoLetra, 20);
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
                        //cajaTexto[f, c].Font = new Font(EngineDataGame.TipoLetra, 20);
                        cajaTexto[f, c].ForeColor = Color.Green;
                        //cajaTexto[f, c].TextAlign = HorizontalAlignment.Center;
                    }
                    else
                    {
                        if (vEliminado[f, c] != null && vEliminado[f, c] != string.Empty)
                        {
                            cajaTexto[f, c].Text = vEliminado[f, c];
                            //cajaTexto[f, c].Font = new Font(EngineDataGame.TipoLetra, 8);
                            cajaTexto[f, c].ForeColor = Color.Red;
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

        public TextBox[,] SetearTextBoxSoloLectura(TextBox[,] cajaTexto)
        {
            if (cajaTexto[0, 0] == null) return cajaTexto;
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    cajaTexto[f, c].ReadOnly = true;
                }
            }
            return cajaTexto;
        }

        public string[,] LimpiarArreglo(string[,] arreglo)
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    arreglo[f, c] = null;
                }
            }
            return arreglo;
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

        public int[] Position(string sentido, int f, int c)
        {
            switch (sentido)
            {
                case "Up":
                    pos[0] = f - 1; pos[1] = c;
                    break;
                case "Down":
                    pos[0] = f + 1; pos[1] = c;
                    break;
                case "Right":
                    pos[0] = f; pos[1] = c + 1;
                    break;
                case "Left":
                    pos[0] = f; pos[1] = c - 1;
                    break;
            }
            return pos;
        }

        public TextBox[,] SetearTextColorInicio(TextBox[,] cajaTexto)
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    cajaTexto[f, c].BackColor = Color.WhiteSmoke;
                }
            }
            return cajaTexto;
        }

        public TextBox[,] SetearTextBoxCandidatos(TextBox[,] cajaTexto, string[,] vIngresado, string[,] vCandidatosSinEliminados)
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (vIngresado[f, c] != null && vIngresado[f, c] != string.Empty)
                    {

                    }
                    else
                    {
                        //cajaTexto[f, c].Font = new Font(EngineDataGame.TipoLetra, 8);
                        cajaTexto[f, c].ForeColor = Color.Blue;
                        cajaTexto[f, c].Text = vCandidatosSinEliminados[f, c];
                    }
                    //cajaTexto[f, c].TextAlign = HorizontalAlignment.Center;
                }
            }
            return cajaTexto;
        }

        public TextBox[,] SetearTextBoxEliminados(TextBox[,] cajaTexto, string[,] vEliminado)
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    //cajaTexto[f, c].Font = new Font(EngineDataGame.TipoLetra, 8);
                    cajaTexto[f, c].ForeColor = Color.Red;
                    cajaTexto[f, c].Text = vEliminado[f, c];
                    //cajaTexto[f, c].TextAlign = HorizontalAlignment.Left;
                }
            }
            return cajaTexto;
        }

        public string ContadorCandidatoEspecifico(string n, string[,] vIngresado, string[,] vCandidatoSinEliminados)
        {
            int count = 0;
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (vIngresado[f, c] == null || vIngresado[f, c] == string.Empty)
                    {
                        if (vCandidatoSinEliminados[f, c].Contains(n))
                        {
                            count++;
                        }
                    }
                }
            }

            return count.ToString();
        }

        public TextBox[,] SetearTextBoxCandidatoEspecifico(string n, TextBox[,] cajaTexto, string[,] vIngresado, string[,] vCandidatoSinEliminados)
        {
            string[,] numFiltro = new string[9, 9];
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (vIngresado[f, c] == null || vIngresado[f, c] == string.Empty)
                    {
                        if (vCandidatoSinEliminados[f, c].Contains(n))
                        {
                            //cajaTexto[f, c].Font = new Font(EngineDataGame.TipoLetra, 20);
                            cajaTexto[f, c].ForeColor = Color.Blue;
                            cajaTexto[f, c].BackColor = Color.WhiteSmoke;
                            cajaTexto[f, c].Text = n;
                            numFiltro[f, c] = n;
                        }
                        else
                        {
                            cajaTexto[f, c].Text = string.Empty;
                            numFiltro[f, c] = string.Empty;
                            cajaTexto[f, c].BackColor = Color.WhiteSmoke;
                        }
                    }
                    else
                    {
                        cajaTexto[f, c].BackColor = Color.WhiteSmoke;
                    }

                    //cajaTexto[f, c].TextAlign = HorizontalAlignment.Center;
                }
            }
            ValorGame.SetNumFiltro(numFiltro);
            return cajaTexto;
        }

        public string OrdenarCadena(string v)
        {
            v = v.Trim();
            string[] a = v.Split(' ');
            int aux1 = 0;
            int aux2 = 0;
            for (int i = 0; i <= a.Length - 1; i++)
            {
                for (int j = 0; j <= a.Length - 1; j++)
                {
                    if (Convert.ToInt32(a[i]) <= Convert.ToInt32(a[j]))
                    {
                        aux1 = Convert.ToInt32(a[j]);
                        aux2 = Convert.ToInt32(a[i]);
                        a[i] = aux1.ToString();
                        a[j] = aux2.ToString();
                    }
                }
            }

            v = string.Empty;

            for (int i = 0; i <= a.Length - 1; i++)
            {
                if (i == 0)
                {
                    v = a[i];
                }
                else if (i == 1 || i == 3 || i == 4 || i == 6 || i == 7)
                {
                    v = v.Trim() + " " + a[i].Trim();
                }
                else if (i == 2 || i == 5 || i == 8)
                {
                    v = v.Trim() + " " + a[i].Trim() + Environment.NewLine;
                }
            }

            return v.Trim();
        }

        // METODOS NUMEROS + CANDIDATOS 
        public string[,] ElejiblesInstantaneos(string[,] valorIngresado, string[,] valorCandidato)
        {
            System.Windows.Forms.ListBox enterRecuadro = new System.Windows.Forms.ListBox();
            System.Windows.Forms.ListBox enterFila = new System.Windows.Forms.ListBox();
            System.Windows.Forms.ListBox enterColumna = new System.Windows.Forms.ListBox();
            System.Windows.Forms.ListBox elejiblesRecuadro = new System.Windows.Forms.ListBox();
            System.Windows.Forms.ListBox elejiblesFila = new System.Windows.Forms.ListBox();
            System.Windows.Forms.ListBox elejiblesColumna = new System.Windows.Forms.ListBox();
            System.Windows.Forms.ListBox elejiblesCelda = new System.Windows.Forms.ListBox();
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
            System.Windows.Forms.ListBox candidatosOrganizados = new System.Windows.Forms.ListBox();
            System.Windows.Forms.ListBox eliminarOrganizados = new System.Windows.Forms.ListBox();
            string[,] valorCandidatoSinEliminados = new string[9, 9];
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
            System.Windows.Forms.ListBox listaAux = new System.Windows.Forms.ListBox();
            candidatosFC = candidatosFC.Trim();
            string[] item = candidatosFC.Split(' ');
            for (int i = 0; i <= item.Length - 1; i++)
            {
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

        private System.Windows.Forms.ListBox OrganizarLista(System.Windows.Forms.ListBox lista, string cadena)
        {
            System.Windows.Forms.ListBox listaAux = new System.Windows.Forms.ListBox();
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

        private System.Windows.Forms.ListBox QuitarEliminados(System.Windows.Forms.ListBox candidatosOrganizados, System.Windows.Forms.ListBox cadenaEliminado)
        {
            int index = -1;
            System.Windows.Forms.ListBox eliminados = new System.Windows.Forms.ListBox();
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

        private string[,] EstablecerCandidatosSinEliminados(System.Windows.Forms.ListBox candidatosFinal, int f, int c, string[,] valorCandidatoSinEliminados)
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

        public TextBox[,] SetearTextBoxJuegoSinEliminados(TextBox[,] cajaTexto, string[,] valorCandidatoSinEliminados)
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (valorCandidatoSinEliminados != null && valorCandidatoSinEliminados[f, c] != string.Empty)
                    {
                        cajaTexto[f, c].Text = valorCandidatoSinEliminados[f, c];
                        //cajaTexto[f, c].TextAlign = HorizontalAlignment.Center;
                    }
                }
            }
            return cajaTexto;
        }

        // GUARDAR ARCHIVO
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

        public void GuardarValoresIngresados(string pathArchivo, string[,] valorIngresado)
        {
            if (pathArchivo != null && pathArchivo != "")
            {
                string[] partes = pathArchivo.Split('\\');
                string nombreArchivo = partes[partes.Length - 1];
                string vLinea = string.Empty;
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathArchivo))
                {
                    string vIngresado = string.Empty;
                    for (int f = 0; f <= 8; f++)
                    {
                        for (int c = 0; c <= 8; c++)
                        {
                            if (valorIngresado[f, c] != null && valorIngresado[f, c] != string.Empty)
                            {
                                vIngresado = valorIngresado[f, c].Trim();
                            }
                            else
                            {
                                vIngresado = EngineDataGame.Zero;
                            }
                            if (c == 0) vLinea = vIngresado + "-";
                            else if (c > 0 && c < 8) vLinea = vLinea + vIngresado + "-";
                            else if (c == 8) vLinea = vLinea + vIngresado;
                        }
                        file.WriteLine(vLinea);
                        vLinea = string.Empty;
                    }
                }
            }
        }

        public void GuardarValoresEliminados(string pathArchivo, string[,] valorEliminado)
        {
            if (pathArchivo != null && pathArchivo != "")
            {
                string[] partes = pathArchivo.Split('\\');
                string nombreArchivo = partes[partes.Length - 1];
                string vLinea = string.Empty;
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathArchivo, true))
                {
                    string vEliminado = string.Empty;
                    for (int f = 0; f <= 8; f++)
                    {
                        for (int c = 0; c <= 8; c++)
                        {
                            if (valorEliminado[f, c] != string.Empty && valorEliminado[f, c] != null)
                            {
                                vEliminado = valorEliminado[f, c].Trim();
                                if (vEliminado == string.Empty) vEliminado = EngineDataGame.Zero;
                            }
                            else
                            {
                                vEliminado = EngineDataGame.Zero;
                            }
                            if (c == 0) vLinea = vEliminado + "-";
                            else if (c > 0 && c < 8) vLinea = vLinea + vEliminado + "-";
                            else if (c == 8) vLinea = vLinea + vEliminado;
                        }

                        file.WriteLine(vLinea);
                        vLinea = string.Empty;
                    }

                }
            }
        }

        public void GuardarValoresEliminados81(string pathArchivo, string[,] valorEliminado)
        {
            if (pathArchivo != null && pathArchivo != "")
            {
                string[] partes = pathArchivo.Split('\\');
                string nombreArchivo = partes[partes.Length - 1];
                string vLinea = string.Empty;
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathArchivo, true))
                {
                    string vEliminado = string.Empty;
                    for (int f = 0; f <= 8; f++)
                    {
                        for (int c = 0; c <= 8; c++)
                        {
                            if (valorEliminado[f, c] != null && valorEliminado[f, c] != string.Empty)
                            {
                                vEliminado = valorEliminado[f, c].Trim();
                            }
                            else
                            {
                                vEliminado = EngineDataGame.Zero;
                            }
                            if (c == 0) vLinea = vEliminado + "-";
                            else if (c > 0 && c < 8) vLinea = vLinea + vEliminado + "-";
                            else if (c == 8) vLinea = vLinea + vEliminado;
                        }
                        file.WriteLine(vLinea);
                        vLinea = string.Empty;
                    }

                }
            }
        }

        public void GuardarValoresInicio(string pathArchivo, string[,] valorInicio)
        {
            if (pathArchivo != null && pathArchivo != string.Empty)
            {
                string[] partes = pathArchivo.Split('\\');
                string nombreArchivo = partes[partes.Length - 1];
                string vLinea = string.Empty;
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathArchivo, true))
                {
                    string vInicio = string.Empty;
                    for (int f = 0; f <= 8; f++)
                    {
                        for (int c = 0; c <= 8; c++)
                        {
                            if (valorInicio[f, c] != null && valorInicio[f, c] != string.Empty)
                            {
                                vInicio = valorInicio[f, c].Trim();
                            }
                            else
                            {
                                vInicio = EngineDataGame.Zero;
                            }
                            if (c == 0) vLinea = vInicio + "-";
                            else if (c > 0 && c < 8) vLinea = vLinea + vInicio + "-";
                            else if (c == 8) vLinea = vLinea + vInicio;
                        }
                        file.WriteLine(vLinea);
                        vLinea = string.Empty;
                    }

                }
            }
        }

        public void GuardarValoresSolucion(string pathArchivo, string[,] valorSolucion)
        {
            if (pathArchivo != null && pathArchivo != "")
            {
                string[] partes = pathArchivo.Split('\\');
                string nombreArchivo = partes[partes.Length - 1];
                string vLinea = string.Empty;
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathArchivo, true))
                {
                    string vSolucion = string.Empty;
                    for (int f = 0; f <= 8; f++)
                    {
                        for (int c = 0; c <= 8; c++)
                        {
                            if (valorSolucion[f, c] != null && valorSolucion[f, c] != string.Empty)
                            {
                                vSolucion = valorSolucion[f, c].Trim();
                            }
                            else
                            {
                                vSolucion = EngineDataGame.Zero;
                            }
                            if (c == 0) vLinea = vSolucion + "-";
                            else if (c > 0 && c < 8) vLinea = vLinea + vSolucion + "-";
                            else if (c == 8) vLinea = vLinea + vSolucion;
                        }
                        file.WriteLine(vLinea);
                        vLinea = string.Empty;
                    }

                }
            }
        }

      /* 
       //ATRIBUTOS ARCHIVO
        public void ReadWriteTxt(string pathArchivo)
        {
            FileAttributes atributosAnteriores = File.GetAttributes(pathArchivo);
            File.SetAttributes(pathArchivo, atributosAnteriores & ~FileAttributes.ReadOnly);
        }

        public void OnlyReadTxt(string pathArchivo)
        {
            FileAttributes atributosAnteriores = File.GetAttributes(pathArchivo);
            File.SetAttributes(pathArchivo, atributosAnteriores | FileAttributes.ReadOnly);
        }

        public bool StatusOnlyReadTxt(string pathArchivo)
        {
            bool r = false;
            FileAttributes atributos = File.GetAttributes(pathArchivo);
            if ((atributos & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                r = true;
            }
            return r;
        }
        */

        public bool ExiteArchivo(string pathArchivo)
        {
            bool resultado = false;
            if (File.Exists(pathArchivo))
            {
                resultado = true;
            }
            return resultado;
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
            catch  { }

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

        public string[,] SetearValorTxtSudoku(TextBox[,] value)
        {
            string[,] valor = new string[9, 9];
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (value[f, c].Text != null && value[f, c].Text != string.Empty)
                    {
                        valor[f, c] = value[f, c].Text;
                    }
                }
            }
            return valor;
        }

        //FILAS COLUMNAS RECUADROS
        public string[,] ObtenerSetearValoresFila(string[,] valorIngresado, string[,] valorCandidato, string[,] valorEliminado, int fila)//MANEJA PLANTILLA FILAS
        {
            string[,] valorPlantilla = new string[9, 9];
            System.Windows.Forms.ListBox candidatos = new System.Windows.Forms.ListBox();
            System.Windows.Forms.ListBox eliminadosOrganizados = new System.Windows.Forms.ListBox();
            for (int c = 0; c <= 8; c++)
            {
                if (valorIngresado[fila, c] == null || valorIngresado[fila, c] == string.Empty)
                {
                    int col = 0;
                    candidatos.Items.Clear();
                    candidatos = OrganizarLista(candidatos, valorCandidato[fila, c]);
                    if (valorEliminado[fila, c] != null && valorEliminado[fila, c] != string.Empty)//Si Existe Eliminado -> Eliminarlo
                    {
                        eliminadosOrganizados.Items.Clear();
                        eliminadosOrganizados = OrganizarLista(eliminadosOrganizados, valorEliminado[fila, c]);
                        candidatos = QuitarEliminados(candidatos, eliminadosOrganizados);
                    }
                    foreach (string candidato in candidatos.Items)
                    {
                        col = Convert.ToInt32(candidato) - 1;
                        valorPlantilla[c, col] = candidato;
                    }
                    candidatos.Items.Clear();
                }
            }
            return valorPlantilla;
        }

        public string[,] ObtenerSetearValoresColumna(string[,] valorIngresado, string[,] valorCandidato, string[,] valorEliminado, int columna) //MANEJA PLANTILLAS COLUMNA
        {
            string[,] valorPlantilla = new string[9, 9];
            System.Windows.Forms.ListBox candidatos = new System.Windows.Forms.ListBox();
            System.Windows.Forms.ListBox eliminadosOrganizados = new System.Windows.Forms.ListBox();
            for (int f = 0; f <= 8; f++)
            {
                if (valorIngresado[f, columna] == null || valorIngresado[f, columna] == string.Empty)
                {
                    int col = 0;
                    candidatos.Items.Clear();
                    candidatos = OrganizarLista(candidatos, valorCandidato[f, columna]);
                    if (valorEliminado[f, columna] != null && valorEliminado[f, columna] != string.Empty)//Si Existe Eliminado -> Eliminarlo
                    {
                        eliminadosOrganizados.Items.Clear();
                        eliminadosOrganizados = OrganizarLista(eliminadosOrganizados, valorEliminado[f, columna]);
                        candidatos = QuitarEliminados(candidatos, eliminadosOrganizados);
                    }
                    foreach (String candidato in candidatos.Items)
                    {
                        col = Convert.ToInt32(candidato) - 1;
                        valorPlantilla[f, col] = candidato;
                    }
                    candidatos.Items.Clear();
                }
            }
            return valorPlantilla;
        }

        public string[,] ObtenerSetearValoresRecuadro(string[,] valorIngresado, string[,] valorCandidato, string[,] valorEliminado, int fila, int columna)//MANEJA PLANTILLAS RECUADRO
        {
            string[,] valorPlantilla = new string[9, 9];
            System.Windows.Forms.ListBox candidatos = new System.Windows.Forms.ListBox();
            System.Windows.Forms.ListBox eliminadosOrganizados = new System.Windows.Forms.ListBox();
            int i = 0;
            for (int f = fila; f <= fila + 2; f++)
            {
                for (int c = columna; c <= columna + 2; c++)
                {
                    if (valorIngresado[f, c] == null || valorIngresado[f, c] == string.Empty)
                    {
                        int col = 0;
                        candidatos.Items.Clear();
                        candidatos = OrganizarLista(candidatos, valorCandidato[f, c]);
                        if (valorEliminado[f, c] != null && valorEliminado[f, c] != string.Empty)//Si Existe Eliminado -> Eliminarlo
                        {
                            eliminadosOrganizados.Items.Clear();
                            eliminadosOrganizados = OrganizarLista(eliminadosOrganizados, valorEliminado[f, c]);
                            candidatos = QuitarEliminados(candidatos, eliminadosOrganizados);
                        }
                        foreach (String candidato in candidatos.Items)
                        {
                            col = Convert.ToInt32(candidato) - 1;
                            valorPlantilla[i, col] = candidato;
                        }
                        candidatos.Items.Clear();
                    }
                    i++;
                }
            }
            return valorPlantilla;
        }

        public string[,] InicioPlantillaVacio(string[,] plantilla)
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    plantilla[f, c] = (c + 1).ToString();
                }
            }
            return plantilla;
        }

        public TextBox[,] SetearPlantillaVacia(TextBox[,] cajaTexto, string[,] plantilla)
        {
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    cajaTexto[f, c].Text = plantilla[f, c];
                }
            }
            return cajaTexto;
        }

        // CANDIDATOS SOLO DEL JUEGO
        public string[] CandidatoSolo(string[,] valorIngresado, string[,] valorCandidatoSinEliminados)
        {
            string[] solo = new string[27];
            int r = -1;
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (valorIngresado[f, c] == string.Empty || valorIngresado[f, c] == null)
                    {
                        valorCandidatoSinEliminados[f, c] = valorCandidatoSinEliminados[f, c].Replace(Environment.NewLine, "");
                        valorCandidatoSinEliminados[f, c] = valorCandidatoSinEliminados[f, c].Replace(" ", "");
                        valorCandidatoSinEliminados[f, c] = valorCandidatoSinEliminados[f, c].Trim();
                        if (valorCandidatoSinEliminados[f, c].Length == 1)
                        {
                            r = NumeroRecuadro(f, c);
                            solo[f] = (f + 1).ToString() + (c + 1).ToString() + valorCandidatoSinEliminados[f, c];
                            solo[c + 9] = (f + 1).ToString() + (c + 1).ToString() + valorCandidatoSinEliminados[f, c];
                            solo[r + 18] = (f + 1).ToString() + (c + 1).ToString() + valorCandidatoSinEliminados[f, c];
                        }
                    }
                }
            }
            return solo;
        }

        // CANDIDATOS OCULTOS DEL JUEGO
        public System.Windows.Forms.ListBox MapeoFilaCandidatoOcultoFila(string[,] valorIngresado, string[,] valorCandidatoSinEliminados, int f)
        {
            System.Windows.Forms.ListBox valor = Agregar1_9();
            for (int c = 0; c <= 8; c++)
            {
                valor = ConcatenarCandidatosFila(valor, valorIngresado, valorCandidatoSinEliminados, f, c);
            }
            valor = RemoverCaracterInicio(valor);
            return valor;
        }

        private System.Windows.Forms.ListBox ConcatenarCandidatosFila(System.Windows.Forms.ListBox valor, string[,] valorIngresado, string[,] valorCandidatoSinEliminados, int f, int c)
        {
            if (valorIngresado[f, c] == string.Empty || valorIngresado[f, c] == null)
            {
                valorCandidatoSinEliminados[f, c] = valorCandidatoSinEliminados[f, c].Replace(Environment.NewLine, "");
                valorCandidatoSinEliminados[f, c] = valorCandidatoSinEliminados[f, c].Replace(" ", "");
                valorCandidatoSinEliminados[f, c] = valorCandidatoSinEliminados[f, c].Trim();
                if (valorCandidatoSinEliminados[f, c].Contains(EngineDataGame.uno))
                {
                    valor.Items[0] = valor.Items[0].ToString() + EngineDataGame.uno;
                }
                else
                {
                    valor.Items[0] = valor.Items[0].ToString() + EngineDataGame.Zero;
                }
                if (valorCandidatoSinEliminados[f, c].Contains(EngineDataGame.dos))
                {
                    valor.Items[1] = valor.Items[1].ToString() + EngineDataGame.dos;
                }
                else
                {
                    valor.Items[1] = valor.Items[1].ToString() + EngineDataGame.Zero;
                }
                if (valorCandidatoSinEliminados[f, c].Contains(EngineDataGame.tres))
                {
                    valor.Items[2] = valor.Items[2].ToString() + EngineDataGame.tres;
                }
                else
                {
                    valor.Items[2] = valor.Items[2].ToString() + EngineDataGame.Zero;
                }
                if (valorCandidatoSinEliminados[f, c].Contains(EngineDataGame.cuatro))
                {
                    valor.Items[3] = valor.Items[3].ToString() + EngineDataGame.cuatro;
                }
                else
                {
                    valor.Items[3] = valor.Items[3].ToString() + EngineDataGame.Zero;
                }
                if (valorCandidatoSinEliminados[f, c].Contains(EngineDataGame.cinco))
                {
                    valor.Items[4] = valor.Items[4].ToString() + EngineDataGame.cinco;
                }
                else
                {
                    valor.Items[4] = valor.Items[4].ToString() + EngineDataGame.Zero;
                }
                if (valorCandidatoSinEliminados[f, c].Contains(EngineDataGame.seis))
                {
                    valor.Items[5] = valor.Items[5].ToString() + EngineDataGame.seis;
                }
                else
                {
                    valor.Items[5] = valor.Items[5].ToString() + EngineDataGame.Zero;
                }
                if (valorCandidatoSinEliminados[f, c].Contains(EngineDataGame.siete))
                {
                    valor.Items[6] = valor.Items[6].ToString() + EngineDataGame.siete;
                }
                else
                {
                    valor.Items[6] = valor.Items[6].ToString() + EngineDataGame.Zero;
                }
                if (valorCandidatoSinEliminados[f, c].Contains(EngineDataGame.ocho))
                {
                    valor.Items[7] = valor.Items[7].ToString() + EngineDataGame.ocho;
                }
                else
                {
                    valor.Items[7] = valor.Items[7].ToString() + EngineDataGame.Zero;
                }
                if (valorCandidatoSinEliminados[f, c].Contains(EngineDataGame.nueve))
                {
                    valor.Items[8] = valor.Items[8].ToString() + EngineDataGame.nueve;
                }
                else
                {
                    valor.Items[8] = valor.Items[8].ToString() + EngineDataGame.Zero;
                }
            }
            else
            {
                valor = Agregar0(valor);
            }

            return valor;
        }

        public string[] SetearOcultoFila(string[] oculto, System.Windows.Forms.ListBox valor, int f, string[,] valorCandidatoSinEliminados)
        {
            string cadena = string.Empty;
            int cont = 0;
            int columna = 0;
            string valorCelda = string.Empty;
            for (int i = 0; i <= 8; i++)
            {
                cont = 0;
                cadena = valor.Items[i].ToString();
                for (int c = 0; c <= 8; c++)
                {
                    if (cadena.Substring(c, 1) != EngineDataGame.Zero)
                    {
                        cont++;
                        columna = c;
                        valorCelda = cadena.Substring(c, 1);
                    }
                }
                if (cont == 1)
                {
                    if (valorCandidatoSinEliminados[f, columna].Trim().Length > 1)
                    {
                        oculto[f] = (f + 1).ToString() + (columna + 1).ToString() + valorCelda;
                    }
                }
            }
            return oculto;
        }

        // **********************************************************************************************************************************************************************
        public System.Windows.Forms.ListBox MapeoFilaCandidatoOcultoColumna(string[,] valorIngresado, string[,] valorCandidatoSinEliminados, int c)
        {
            System.Windows.Forms.ListBox valor = Agregar1_9();
            for (int f = 0; f <= 8; f++)
            {
                valor = ConcatenarCandidatosColumna(valor, valorIngresado, valorCandidatoSinEliminados, f, c);
            }
            valor = RemoverCaracterInicio(valor);
            return valor;
        }

        private System.Windows.Forms.ListBox ConcatenarCandidatosColumna(System.Windows.Forms.ListBox valor, string[,] valorIngresado, string[,] valorCandidatoSinEliminados, int f, int c)
        {
            if (valorIngresado[f, c] == string.Empty || valorIngresado[f, c] == null)
            {
                valorCandidatoSinEliminados[f, c] = valorCandidatoSinEliminados[f, c].Replace(Environment.NewLine, "");
                valorCandidatoSinEliminados[f, c] = valorCandidatoSinEliminados[f, c].Replace(" ", "");
                valorCandidatoSinEliminados[f, c] = valorCandidatoSinEliminados[f, c].Trim();
                if (valorCandidatoSinEliminados[f, c].Contains(EngineDataGame.uno))
                {
                    valor.Items[0] = valor.Items[0].ToString() + EngineDataGame.uno;
                }
                else
                {
                    valor.Items[0] = valor.Items[0].ToString() + EngineDataGame.Zero;
                }
                if (valorCandidatoSinEliminados[f, c].Contains(EngineDataGame.dos))
                {
                    valor.Items[1] = valor.Items[1].ToString() + EngineDataGame.dos;
                }
                else
                {
                    valor.Items[1] = valor.Items[1].ToString() + EngineDataGame.Zero;
                }
                if (valorCandidatoSinEliminados[f, c].Contains(EngineDataGame.tres))
                {
                    valor.Items[2] = valor.Items[2].ToString() + EngineDataGame.tres;
                }
                else
                {
                    valor.Items[2] = valor.Items[2].ToString() + EngineDataGame.Zero;
                }
                if (valorCandidatoSinEliminados[f, c].Contains(EngineDataGame.cuatro))
                {
                    valor.Items[3] = valor.Items[3].ToString() + EngineDataGame.cuatro;
                }
                else
                {
                    valor.Items[3] = valor.Items[3].ToString() + EngineDataGame.Zero;
                }
                if (valorCandidatoSinEliminados[f, c].Contains(EngineDataGame.cinco))
                {
                    valor.Items[4] = valor.Items[4].ToString() + EngineDataGame.cinco;
                }
                else
                {
                    valor.Items[4] = valor.Items[4].ToString() + EngineDataGame.Zero;
                }
                if (valorCandidatoSinEliminados[f, c].Contains(EngineDataGame.seis))
                {
                    valor.Items[5] = valor.Items[5].ToString() + EngineDataGame.seis;
                }
                else
                {
                    valor.Items[5] = valor.Items[5].ToString() + EngineDataGame.Zero;
                }
                if (valorCandidatoSinEliminados[f, c].Contains(EngineDataGame.siete))
                {
                    valor.Items[6] = valor.Items[6].ToString() + EngineDataGame.siete;
                }
                else
                {
                    valor.Items[6] = valor.Items[6].ToString() + EngineDataGame.Zero;
                }
                if (valorCandidatoSinEliminados[f, c].Contains(EngineDataGame.ocho))
                {
                    valor.Items[7] = valor.Items[7].ToString() + EngineDataGame.ocho;
                }
                else
                {
                    valor.Items[7] = valor.Items[7].ToString() + EngineDataGame.Zero;
                }
                if (valorCandidatoSinEliminados[f, c].Contains(EngineDataGame.nueve))
                {
                    valor.Items[8] = valor.Items[8].ToString() + EngineDataGame.nueve;
                }
                else
                {
                    valor.Items[8] = valor.Items[8].ToString() + EngineDataGame.Zero;
                }
            }
            else
            {
                valor = Agregar0(valor);
            }

            return valor;
        }

        public string[] SetearOcultoColumna(string[] oculto, System.Windows.Forms.ListBox valor, int c, string[,] valorCandidatoSinEliminados)
        {
            string cadena = string.Empty;
            int cont = 0;
            int fila = 0;
            string valorCelda = string.Empty;
            for (int i = 0; i <= 8; i++)
            {
                cont = 0;
                cadena = valor.Items[i].ToString();
                for (int f = 0; f <= 8; f++)
                {
                    if (cadena.Substring(f, 1) != EngineDataGame.Zero)
                    {
                        cont++;
                        fila = f;
                        valorCelda = cadena.Substring(f, 1);
                    }
                }
                if (cont == 1)
                {
                    if (valorCandidatoSinEliminados[fila, c].Trim().Length > 1)
                    {
                        oculto[c + 9] = (fila + 1).ToString() + (c + 1).ToString() + valorCelda;
                    }
                }
            }
            return oculto;
        }

        // **********************************************************************************************************************************************************************
        public System.Windows.Forms.ListBox MapeoFilaCandidatoOcultoRecuadro(string[,] valorIngresado, string[,] valorCandidatoSinEliminados, int recuadro)
        {
            System.Windows.Forms.ListBox valor = Agregar1_9();
            int[] rango = new int[2];
            rango = RangoRecuadro(recuadro);
            int fila = rango[0];
            int columna = rango[1];

            for (int f = fila; f <= fila + 2; f++)
            {
                for (int c = columna; c <= columna + 2; c++)
                {
                    valor = ConcatenarCandidatosRecuadro(valor, valorIngresado, valorCandidatoSinEliminados, f, c);
                }
            }
            valor = RemoverCaracterInicio(valor);
            return valor;
        }

        private System.Windows.Forms.ListBox ConcatenarCandidatosRecuadro(System.Windows.Forms.ListBox valor, string[,] valorIngresado, string[,] valorCandidatoSinEliminados, int f, int c)
        {
            if (valorIngresado[f, c] == string.Empty || valorIngresado[f, c] == null)
            {
                valorCandidatoSinEliminados[f, c] = valorCandidatoSinEliminados[f, c].Replace(Environment.NewLine, "");
                valorCandidatoSinEliminados[f, c] = valorCandidatoSinEliminados[f, c].Replace(" ", "");
                valorCandidatoSinEliminados[f, c] = valorCandidatoSinEliminados[f, c].Trim();
                if (valorCandidatoSinEliminados[f, c].Contains(EngineDataGame.uno))
                {
                    valor.Items[0] = valor.Items[0].ToString() + EngineDataGame.uno;
                }
                else
                {
                    valor.Items[0] = valor.Items[0].ToString() + EngineDataGame.Zero;
                }
                if (valorCandidatoSinEliminados[f, c].Contains(EngineDataGame.dos))
                {
                    valor.Items[1] = valor.Items[1].ToString() + EngineDataGame.dos;
                }
                else
                {
                    valor.Items[1] = valor.Items[1].ToString() + EngineDataGame.Zero;
                }
                if (valorCandidatoSinEliminados[f, c].Contains(EngineDataGame.tres))
                {
                    valor.Items[2] = valor.Items[2].ToString() + EngineDataGame.tres;
                }
                else
                {
                    valor.Items[2] = valor.Items[2].ToString() + EngineDataGame.Zero;
                }
                if (valorCandidatoSinEliminados[f, c].Contains(EngineDataGame.cuatro))
                {
                    valor.Items[3] = valor.Items[3].ToString() + EngineDataGame.cuatro;
                }
                else
                {
                    valor.Items[3] = valor.Items[3].ToString() + EngineDataGame.Zero;
                }
                if (valorCandidatoSinEliminados[f, c].Contains(EngineDataGame.cinco))
                {
                    valor.Items[4] = valor.Items[4].ToString() + EngineDataGame.cinco;
                }
                else
                {
                    valor.Items[4] = valor.Items[4].ToString() + EngineDataGame.Zero;
                }
                if (valorCandidatoSinEliminados[f, c].Contains(EngineDataGame.seis))
                {
                    valor.Items[5] = valor.Items[5].ToString() + EngineDataGame.seis;
                }
                else
                {
                    valor.Items[5] = valor.Items[5].ToString() + EngineDataGame.Zero;
                }
                if (valorCandidatoSinEliminados[f, c].Contains(EngineDataGame.siete))
                {
                    valor.Items[6] = valor.Items[6].ToString() + EngineDataGame.siete;
                }
                else
                {
                    valor.Items[6] = valor.Items[6].ToString() + EngineDataGame.Zero;
                }
                if (valorCandidatoSinEliminados[f, c].Contains(EngineDataGame.ocho))
                {
                    valor.Items[7] = valor.Items[7].ToString() + EngineDataGame.ocho;
                }
                else
                {
                    valor.Items[7] = valor.Items[7].ToString() + EngineDataGame.Zero;
                }
                if (valorCandidatoSinEliminados[f, c].Contains(EngineDataGame.nueve))
                {
                    valor.Items[8] = valor.Items[8].ToString() + EngineDataGame.nueve;
                }
                else
                {
                    valor.Items[8] = valor.Items[8].ToString() + EngineDataGame.Zero;
                }
            }
            else
            {
                valor = Agregar0(valor);
            }

            return valor;
        }

        public string[] SetearOcultoRecuadro(string[] oculto, System.Windows.Forms.ListBox valor, int recuadro, string[,] valorCandidatoSinEliminados)
        {
            string cadena = string.Empty;
            int cont = 0;
            int[] obj = new int[2];
            int fila = 0;
            int columna = 0;
            string valorCelda = string.Empty;

            for (int i = 0; i <= 8; i++)
            {
                cont = 0;
                cadena = valor.Items[i].ToString();
                for (int c = 0; c <= 8; c++)
                {
                    if (cadena.Substring(c, 1) != EngineDataGame.Zero)
                    {
                        cont++;
                        obj = FilaRecuadro(recuadro, c);
                        fila = obj[0];
                        columna = obj[1];
                        valorCelda = cadena.Substring(c, 1);
                    }
                }
                if (cont == 1)
                {
                    if (valorCandidatoSinEliminados[fila, columna].Trim().Length > 1)
                    {
                        oculto[fila + 18] = (fila + 1).ToString() + (columna + 1).ToString() + valorCelda;
                    }
                }
            }
            return oculto;
        }

        private int[] FilaRecuadro(int recuadro, int z)
        {
            int[] obj = new int[2];
            if (recuadro == 0)
            {
                if (z >= 0 && z <= 2)
                {
                    if (z == 0) { obj[0] = 0; obj[1] = 0; }
                    else if (z == 1) { obj[0] = 0; obj[1] = 1; }
                    else if (z == 2) { obj[0] = 0; obj[1] = 2; }

                }
                else if (z >= 3 && z <= 5)
                {
                    if (z == 3) { obj[0] = 1; obj[1] = 0; }
                    else if (z == 4) { obj[0] = 1; obj[1] = 1; }
                    else if (z == 5) { obj[0] = 1; obj[1] = 2; }
                }
                else if (z >= 6 && z <= 8)
                {
                    if (z == 6) { obj[0] = 2; obj[1] = 0; }
                    else if (z == 7) { obj[0] = 2; obj[1] = 1; }
                    else if (z == 8) { obj[0] = 2; obj[1] = 2; }
                }
            }
            else if (recuadro == 1)
            {
                if (z >= 0 && z <= 2)
                {
                    if (z == 0) { obj[0] = 0; obj[1] = 3; }
                    else if (z == 1) { obj[0] = 0; obj[1] = 4; }
                    else if (z == 2) { obj[0] = 0; obj[1] = 5; }
                }
                else if (z >= 3 && z <= 5)
                {
                    if (z == 3) { obj[0] = 1; obj[1] = 3; }
                    else if (z == 4) { obj[0] = 1; obj[1] = 4; }
                    else if (z == 5) { obj[0] = 1; obj[1] = 5; }
                }
                else if (z >= 6 && z <= 8)
                {
                    if (z == 6) { obj[0] = 2; obj[1] = 3; }
                    else if (z == 7) { obj[0] = 2; obj[1] = 4; }
                    else if (z == 8) { obj[0] = 2; obj[1] = 5; }
                }
            }
            else if (recuadro == 2)
            {
                if (z >= 0 && z <= 2)
                {
                    if (z == 0) { obj[0] = 0; obj[1] = 6; }
                    else if (z == 1) { obj[0] = 0; obj[1] = 7; }
                    else if (z == 2) { obj[0] = 0; obj[1] = 8; }
                }
                else if (z >= 3 && z <= 5)
                {
                    if (z == 3) { obj[0] = 1; obj[1] = 6; }
                    else if (z == 4) { obj[0] = 1; obj[1] = 7; }
                    else if (z == 5) { obj[0] = 1; obj[1] = 8; }
                }
                else if (z >= 6 && z <= 8)
                {
                    if (z == 6) { obj[0] = 2; obj[1] = 6; }
                    else if (z == 7) { obj[0] = 2; obj[1] = 7; }
                    else if (z == 8) { obj[0] = 2; obj[1] = 8; }
                }
            }
            else if (recuadro == 3)
            {
                if (z >= 0 && z <= 2)
                {
                    if (z == 0) { obj[0] = 3; obj[1] = 0; }
                    else if (z == 1) { obj[0] = 3; obj[1] = 1; }
                    else if (z == 2) { obj[0] = 3; obj[1] = 2; }
                }
                else if (z >= 3 && z <= 5)
                {
                    if (z == 3) { obj[0] = 4; obj[1] = 0; }
                    else if (z == 4) { obj[0] = 4; obj[1] = 1; }
                    else if (z == 5) { obj[0] = 4; obj[1] = 2; }
                }
                else if (z >= 6 && z <= 8)
                {
                    if (z == 6) { obj[0] = 5; obj[1] = 0; }
                    else if (z == 7) { obj[0] = 5; obj[1] = 1; }
                    else if (z == 8) { obj[0] = 5; obj[1] = 2; }
                }
            }
            else if (recuadro == 4)
            {
                if (z >= 0 && z <= 2)
                {
                    if (z == 0) { obj[0] = 3; obj[1] = 3; }
                    else if (z == 1) { obj[0] = 3; obj[1] = 4; }
                    else if (z == 2) { obj[0] = 3; obj[1] = 5; }
                }
                else if (z >= 3 && z <= 5)
                {
                    if (z == 3) { obj[0] = 4; obj[1] = 3; }
                    else if (z == 4) { obj[0] = 4; obj[1] = 4; }
                    else if (z == 5) { obj[0] = 4; obj[1] = 5; }
                }
                else if (z >= 6 && z <= 8)
                {
                    if (z == 6) { obj[0] = 5; obj[1] = 3; }
                    else if (z == 7) { obj[0] = 5; obj[1] = 4; }
                    else if (z == 8) { obj[0] = 5; obj[1] = 5; }
                }
            }
            else if (recuadro == 5)
            {
                if (z >= 0 && z <= 2)
                {
                    if (z == 0) { obj[0] = 3; obj[1] = 6; }
                    else if (z == 1) { obj[0] = 3; obj[1] = 7; }
                    else if (z == 2) { obj[0] = 3; obj[1] = 8; }
                }
                else if (z >= 3 && z <= 5)
                {
                    if (z == 3) { obj[0] = 4; obj[1] = 6; }
                    else if (z == 4) { obj[0] = 4; obj[1] = 7; }
                    else if (z == 5) { obj[0] = 4; obj[1] = 8; }
                }
                else if (z >= 6 && z <= 8)
                {
                    if (z == 6) { obj[0] = 5; obj[1] = 6; }
                    else if (z == 7) { obj[0] = 5; obj[1] = 7; }
                    else if (z == 8) { obj[0] = 5; obj[1] = 8; }
                }
            }
            else if (recuadro == 6)
            {
                if (z >= 0 && z <= 2)
                {
                    if (z == 0) { obj[0] = 6; obj[1] = 0; }
                    else if (z == 1) { obj[0] = 6; obj[1] = 1; }
                    else if (z == 2) { obj[0] = 6; obj[1] = 2; }
                }
                else if (z >= 3 && z <= 5)
                {
                    if (z == 3) { obj[0] = 7; obj[1] = 0; }
                    else if (z == 4) { obj[0] = 7; obj[1] = 1; }
                    else if (z == 5) { obj[0] = 7; obj[1] = 2; }
                }
                else if (z >= 6 && z <= 8)
                {
                    if (z == 6) { obj[0] = 8; obj[1] = 0; }
                    else if (z == 7) { obj[0] = 8; obj[1] = 1; }
                    else if (z == 8) { obj[0] = 8; obj[1] = 2; }
                }
            }
            else if (recuadro == 7)
            {
                if (z >= 0 && z <= 2)
                {
                    if (z == 0) { obj[0] = 6; obj[1] = 3; }
                    else if (z == 1) { obj[0] = 6; obj[1] = 4; }
                    else if (z == 2) { obj[0] = 6; obj[1] = 5; }
                }
                else if (z >= 3 && z <= 5)
                {
                    if (z == 3) { obj[0] = 7; obj[1] = 3; }
                    else if (z == 4) { obj[0] = 7; obj[1] = 4; }
                    else if (z == 5) { obj[0] = 7; obj[1] = 5; }
                }
                else if (z >= 6 && z <= 8)
                {
                    if (z == 6) { obj[0] = 8; obj[1] = 3; }
                    else if (z == 7) { obj[0] = 8; obj[1] = 4; }
                    else if (z == 8) { obj[0] = 8; obj[1] = 5; }
                }
            }
            else if (recuadro == 8)
            {
                if (z >= 0 && z <= 2)
                {
                    if (z == 0) { obj[0] = 6; obj[1] = 6; }
                    else if (z == 1) { obj[0] = 6; obj[1] = 7; }
                    else if (z == 2) { obj[0] = 6; obj[1] = 8; }
                }
                else if (z >= 3 && z <= 5)
                {
                    if (z == 3) { obj[0] = 7; obj[1] = 6; }
                    else if (z == 4) { obj[0] = 7; obj[1] = 7; }
                    else if (z == 5) { obj[0] = 7; obj[1] = 8; }
                }
                else if (z >= 6 && z <= 8)
                {
                    if (z == 6) { obj[0] = 8; obj[1] = 6; }
                    else if (z == 7) { obj[0] = 8; obj[1] = 7; }
                    else if (z == 8) { obj[0] = 8; obj[1] = 8; }
                }
            }
            return obj;
        }

        // ***********************************************************************************************************************************************************************
        private System.Windows.Forms.ListBox Agregar1_9()
        {
            System.Windows.Forms.ListBox valor = new System.Windows.Forms.ListBox();
            for (int i = 1; i <= 9; i++)
            {
                valor.Items.Add(i.ToString());
            }
            return valor;
        }

        private System.Windows.Forms.ListBox Agregar0(System.Windows.Forms.ListBox valor)
        {
            for (int i = 0; i <= 8; i++)
            {
                valor.Items[i] = valor.Items[i].ToString() + EngineDataGame.Zero;
            }
            return valor;
        }

        private System.Windows.Forms.ListBox RemoverCaracterInicio(System.Windows.Forms.ListBox valor)
        {
            for (int n = 0; n <= 8; n++)
            {
                valor.Items[n] = valor.Items[n].ToString().Remove(0, 1);
            }
            return valor;
        }

        //NUMERO RECUADRO
        public int NumeroRecuadro(int f, int c)
        {
            int recuadro = -1;

            if ((f >= 0 && f <= 2) && (c >= 0 && c <= 2)) { recuadro = 0; }
            else if ((f >= 0 && f <= 2) && (c >= 3 && c <= 5)) { recuadro = 1; }
            else if ((f >= 0 && f <= 2) && (c >= 6 && c <= 8)) { recuadro = 2; }

            else if ((f >= 3 && f <= 5) && (c >= 0 && c <= 2)) { recuadro = 3; }
            else if ((f >= 3 && f <= 5) && (c >= 3 && c <= 5)) { recuadro = 4; }
            else if ((f >= 3 && f <= 5) && (c >= 6 && c <= 8)) { recuadro = 5; }

            else if ((f >= 6 && f <= 8) && (c >= 0 && c <= 2)) { recuadro = 6; }
            else if ((f >= 6 && f <= 8) && (c >= 3 && c <= 5)) { recuadro = 7; }
            else if ((f >= 6 && f <= 8) && (c >= 6 && c <= 8)) { recuadro = 8; }

            return recuadro;
        }

        public int[] RangoRecuadro(int recuadro)
        {
            int[] rango = new int[2];
            if (recuadro == 0) { rango[0] = 0; rango[1] = 0; }
            else if (recuadro == 1) { rango[0] = 0; rango[1] = 3; }
            else if (recuadro == 2) { rango[0] = 0; rango[1] = 6; }
            else if (recuadro == 3) { rango[0] = 3; rango[1] = 0; }
            else if (recuadro == 4) { rango[0] = 3; rango[1] = 3; }
            else if (recuadro == 5) { rango[0] = 3; rango[1] = 6; }
            else if (recuadro == 6) { rango[0] = 6; rango[1] = 0; }
            else if (recuadro == 7) { rango[0] = 6; rango[1] = 3; }
            else if (recuadro == 8) { rango[0] = 6; rango[1] = 6; }
            return rango;
        }


        public LetrasJuegoACB SetLetrasJuegoACB(string[] solo, string[] oculto)
        {
            double contSolo = 0;
            double contOculto = 0;
            bool c = EngineDataGame.Falso;
            for (int i = 0; i <= solo.Length - 1; i++)
            {
                if (solo[i] != null && solo[i] != string.Empty)
                {
                    contSolo++;
                }
            }
            contSolo = contSolo / 3;

            for (int i = 0; i <= oculto.Length - 1; i++)
            {
                if (oculto[i] != null && oculto[i] != string.Empty)
                {
                    contOculto++;
                }
            }

            if (contSolo + contOculto == 0) c = EngineDataGame.Falso;
            else if (contSolo + contOculto > 0) c = EngineDataGame.Verdadero;

            LetrasJuegoACB letrasACB = new LetrasJuegoACB
            {
                A = RedondeoNumero(contSolo),
                B = RedondeoNumero(contOculto),
                C = c
            };
            return letrasACB;
        }

        private double RedondeoNumero(double n)
        {
            if (n > 0 && n <= 1) n = 1;
            else if (n > 1 && n <= 2) n = 2;
            else if (n > 2 && n <= 3) n = 3;
            else if (n > 3 && n <= 4) n = 4;
            else if (n > 4 && n <= 5) n = 5;
            else if (n > 5 && n <= 6) n = 6;
            else if (n > 6 && n <= 7) n = 7;
            return n;
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

        public int ContadorCandidatos(string[,] valorIngresado, string[,] valorCandidatoSinEliminados)
        {
            int contadorCandidatos = 0;
            for (int f = 0; f <= 8; f++)
            {
                for (int c = 0; c <= 8; c++)
                {
                    if (valorIngresado[f, c] == null || valorIngresado[f, c] == string.Empty)
                    {
                        valorCandidatoSinEliminados[f, c] = valorCandidatoSinEliminados[f, c].Replace(System.Environment.NewLine, "");
                        valorCandidatoSinEliminados[f, c] = valorCandidatoSinEliminados[f, c].Replace(" ", "");
                        contadorCandidatos = contadorCandidatos + valorCandidatoSinEliminados[f, c].Length;
                    }
                }
            }
            return contadorCandidatos;
        }

        public bool Visibilidad70(double v)
        {
            bool visible = EngineDataGame.Falso;
            if (v < 70) visible = EngineDataGame.Verdadero;

            return visible;
        }

        //CREAR TABLAS 
        public DataTable CrearTabla()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("POS.");
            dt.Columns.Add("G.");
            dt.Columns.Add("Nº");
            dt.Columns.Add("c.v.");
            dt.Columns.Add("o.de.j.");
            return dt;
        }

        public DataTable CrearTabla1()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("POS.");
            dt.Columns.Add("G.");
            dt.Columns.Add("Nº");
            dt.Columns.Add("c.v.");
            dt.Columns.Add("o.de.j.");
            AgregarFilas(dt, 27, "TABLA1");
            return dt;
        }

        public DataTable CrearTabla2()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("POS.");
            dt.Columns.Add("G.");
            dt.Columns.Add("Nº");
            dt.Columns.Add("SOLO");
            dt.Columns.Add("OCULTO");
            AgregarFilas(dt, 27, "TABLA2");
            return dt;
        }

        private DataTable AgregarFilas(DataTable dt, int nF, string tabla)
        {
            string nombreIdioma = ValorGame.GetNombreIdioma();
            string fila = Valor.Fila(nombreIdioma);
            string columna = Valor.Columna(nombreIdioma);
            string recuadro = Valor.Recuadro(nombreIdioma);

            for (int i = 1; i <= nF; i++)
            {
                if (tabla == "TABLA2")
                {
                    if (i >= 1 && i <= 9)
                        dt.Rows.Add(i, fila, i, "");
                    else if (i >= 10 && i <= 18)
                        dt.Rows.Add(i, columna, i - 9, "");
                    else if (i >= 19 && i <= 27)
                        dt.Rows.Add(i, recuadro, i - 18, "");
                }
                else
                {
                    if (i >= 1 && i <= 9)
                        dt.Rows.Add(i, fila, i, "", "");
                    else if (i >= 10 && i <= 18)
                        dt.Rows.Add(i, columna, i - 9, "", "");
                    else if (i >= 19 && i <= 27)
                        dt.Rows.Add(i, recuadro, i - 18, "", "");
                }
            }
            return dt;
        }

        /*
        // FORMATO DATAGRIDVIEW
        public DataGridView FormatoDataGridView1(DataGridView dgv)
        {
            //dataGridView1.AutoGenerateColumns = EngineDataGame.Falso;
            dgv.Columns[0].Width = 50;
            dgv.Columns[1].Width = 50;
            dgv.Columns[2].Width = 50;
            dgv.Columns[3].Width = 50;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ClearSelection();
            return dgv;
        }

        public DataGridView FormatoDataGridView2(DataGridView dgv)
        {
            dgv.Columns[0].Width = 50;
            dgv.Columns[1].Width = 50;
            dgv.Columns[2].Width = 50;
            dgv.Columns[3].Width = 50;
            dgv.Columns[4].Width = 80;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ClearSelection();
            return dgv;
        }
        */

        //CONTABILIZAR GRUPOS VACIOS

        public int ContableFila(int f, string[,] vIngresado)
        {
            int cVacias = 0;
            for (int c = 0; c <= 8; c++)
            {
                if (vIngresado[f, c] == string.Empty || vIngresado[f, c] == null)
                {
                    cVacias++;
                }
            }
            return cVacias;
        }

        public int ContableColumna(int c, string[,] vIngresado)
        {
            int cVacias = 0;
            for (int f = 0; f <= 8; f++)
            {
                if (vIngresado[f, c] == string.Empty || vIngresado[f, c] == null)
                {
                    cVacias++;
                }
            }
            return cVacias;
        }

        public int ContableRecuadro(int fila, int columna, string[,] vIngresado)
        {
            int cVacias = 0;
            for (int f = fila; f <= fila + 2; f++)
            {
                for (int c = columna; c <= columna + 2; c++)
                {
                    if (vIngresado[f, c] == string.Empty || vIngresado[f, c] == null)
                    {
                        cVacias++;
                    }
                }
            }
            return cVacias;
        }

        public DataTable ContarGruposVacios(DataTable dt, string[,] valorIngresado)
        {
            int n = 0;
            int cv = 0;
            int[] rec = new int[2];
            foreach (DataRow r in dt.Rows)
            {
                if (n >= 0 && n <= 8)
                {
                    cv = ContableFila(n, valorIngresado);
                    r[3] = cv.ToString();
                }
                else if (n >= 9 && n <= 17)
                {
                    cv = ContableColumna(n - 9, valorIngresado);
                    r[3] = cv.ToString();
                }
                else if (n >= 18 && n <= 26)
                {
                    rec = RangoRecuadro(n - 18);
                    cv = ContableRecuadro(rec[0], rec[1], valorIngresado);
                    r[3] = cv.ToString();
                }
                n++;
            }
            return dt;
        }

        public DataTable MostrarSoloOculto(DataTable dt, string[] solo, string[] oculto)
        {
            int n = 0;
            string fila = string.Empty;
            string columna = string.Empty;
            int recuadro = 0;

            foreach (DataRow r in dt.Rows)
            {
                r[4] = string.Empty;
            }
            foreach (DataRow r in dt.Rows)
            {
                if (solo[n] != null || oculto[n] != null)
                {
                    if (n >= 0 && n <= 17)
                    {
                        r[4] = "?";
                    }
                    else
                    {
                        if (solo[n] != null)
                        {
                            r[4] = "?";
                        }
                        if (oculto[n] != null)
                        {
                            fila = oculto[n].Substring(0, 1);
                            columna = oculto[n].Substring(1, 1);
                            recuadro = NumeroRecuadro(Convert.ToInt16(fila) - 1, Convert.ToInt16(columna) - 1);
                            if (recuadro == 0) { dt.Rows[18][4] = "?"; }
                            else if (recuadro == 1) { dt.Rows[19][4] = "?"; }
                            else if (recuadro == 2) { dt.Rows[20][4] = "?"; }
                            else if (recuadro == 3) { dt.Rows[21][4] = "?"; }
                            else if (recuadro == 4) { dt.Rows[22][4] = "?"; }
                            else if (recuadro == 5) { dt.Rows[23][4] = "?"; }
                            else if (recuadro == 6) { dt.Rows[24][4] = "?"; }
                            else if (recuadro == 7) { dt.Rows[25][4] = "?"; }
                            else if (recuadro == 8) { dt.Rows[26][4] = "?"; }

                        }
                    }
                }
                n++;
            }
            return dt;
        }

        public DataTable OrdernadorLetraNumerico(DataTable dt)
        {
            DataTable cero = new DataTable();
            cero = CrearTabla();
            DataTable uno = new DataTable();
            uno = CrearTabla();
            DataTable dos = new DataTable();
            dos = CrearTabla();
            DataTable tres = new DataTable();
            tres = CrearTabla();
            DataTable cuatro = new DataTable();
            cuatro = CrearTabla();
            DataTable cinco = new DataTable();
            cinco = CrearTabla();
            DataTable seis = new DataTable();
            seis = CrearTabla();
            DataTable siete = new DataTable();
            siete = CrearTabla();
            DataTable ocho = new DataTable();
            ocho = CrearTabla();
            DataTable nueve = new DataTable();
            nueve = CrearTabla();
            DataTable tabla = new DataTable();
            tabla = CrearTabla();

            foreach (DataRow r in dt.Rows)
            {
                if ((r[1].ToString() == "Fila" || r[1].ToString() == "Linha" || r[1].ToString() == "Row") && r[3].ToString() == "0")
                {
                    cero.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }
                else if ((r[1].ToString() == "Fila" || r[1].ToString() == "Linha" || r[1].ToString() == "Row") && r[3].ToString() == "1")
                {
                    uno.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }
                else if ((r[1].ToString() == "Fila" || r[1].ToString() == "Linha" || r[1].ToString() == "Row") && r[3].ToString() == "2")
                {
                    dos.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }
                else if ((r[1].ToString() == "Fila" || r[1].ToString() == "Linha" || r[1].ToString() == "Row") && r[3].ToString() == "3")
                {
                    tres.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }
                else if ((r[1].ToString() == "Fila" || r[1].ToString() == "Linha" || r[1].ToString() == "Row") && r[3].ToString() == "4")
                {
                    cuatro.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }
                else if ((r[1].ToString() == "Fila" || r[1].ToString() == "Linha" || r[1].ToString() == "Row") && r[3].ToString() == "5")
                {
                    cinco.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }
                else if ((r[1].ToString() == "Fila" || r[1].ToString() == "Linha" || r[1].ToString() == "Row") && r[3].ToString() == "6")
                {
                    seis.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }
                else if ((r[1].ToString() == "Fila" || r[1].ToString() == "Linha" || r[1].ToString() == "Row") && r[3].ToString() == "7")
                {
                    siete.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }
                else if ((r[1].ToString() == "Fila" || r[1].ToString() == "Linha" || r[1].ToString() == "Row") && r[3].ToString() == "8")
                {
                    ocho.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }
                else if ((r[1].ToString() == "Fila" || r[1].ToString() == "Linha" || r[1].ToString() == "Row") && r[3].ToString() == "9")
                {
                    nueve.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }

            }

            foreach (DataRow r in dt.Rows)
            {
                if (r[1].ToString().Substring(0, 1) == "C" && r[3].ToString() == "0")
                {
                    cero.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }
                else if (r[1].ToString().Substring(0, 1) == "C" && r[3].ToString() == "1")
                {
                    uno.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }
                else if (r[1].ToString().Substring(0, 1) == "C" && r[3].ToString() == "2")
                {
                    dos.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }
                else if (r[1].ToString().Substring(0, 1) == "C" && r[3].ToString() == "3")
                {
                    tres.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }
                else if (r[1].ToString().Substring(0, 1) == "C" && r[3].ToString() == "4")
                {
                    cuatro.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }
                else if (r[1].ToString().Substring(0, 1) == "C" && r[3].ToString() == "5")
                {
                    cinco.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }
                else if (r[1].ToString().Substring(0, 1) == "C" && r[3].ToString() == "6")
                {
                    seis.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }
                else if (r[1].ToString().Substring(0, 1) == "C" && r[3].ToString() == "7")
                {
                    siete.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }
                else if (r[1].ToString().Substring(0, 1) == "C" && r[3].ToString() == "8")
                {
                    ocho.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }
                else if (r[1].ToString().Substring(0, 1) == "C" && r[3].ToString() == "9")
                {
                    nueve.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }

            }

            foreach (DataRow r in dt.Rows)
            {
                if ((r[1].ToString() == "Quadrante" || r[1].ToString() == "Box" || r[1].ToString() == "Recuadro") && r[3].ToString() == "0")
                {
                    cero.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }
                else if ((r[1].ToString() == "Quadrante" || r[1].ToString() == "Box" || r[1].ToString() == "Recuadro") && r[3].ToString() == "1")
                {
                    uno.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }
                else if ((r[1].ToString() == "Quadrante" || r[1].ToString() == "Box" || r[1].ToString() == "Recuadro") && r[3].ToString() == "2")
                {
                    dos.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }
                else if ((r[1].ToString() == "Quadrante" || r[1].ToString() == "Box" || r[1].ToString() == "Recuadro") && r[3].ToString() == "3")
                {
                    tres.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }
                else if ((r[1].ToString() == "Quadrante" || r[1].ToString() == "Box" || r[1].ToString() == "Recuadro") && r[3].ToString() == "4")
                {
                    cuatro.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }
                else if ((r[1].ToString() == "Quadrante" || r[1].ToString() == "Box" || r[1].ToString() == "Recuadro") && r[3].ToString() == "5")
                {
                    cinco.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }
                else if ((r[1].ToString() == "Quadrante" || r[1].ToString() == "Box" || r[1].ToString() == "Recuadro") && r[3].ToString() == "6")
                {
                    seis.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }
                else if ((r[1].ToString() == "Quadrante" || r[1].ToString() == "Box" || r[1].ToString() == "Recuadro") && r[3].ToString() == "7")
                {
                    siete.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }
                else if ((r[1].ToString() == "Quadrante" || r[1].ToString() == "Box" || r[1].ToString() == "Recuadro") && r[3].ToString() == "8")
                {
                    ocho.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }
                else if ((r[1].ToString() == "Quadrante" || r[1].ToString() == "Box" || r[1].ToString() == "Recuadro") && r[3].ToString() == "9")
                {
                    nueve.Rows.Add(r[0], r[1], r[2], r[3], r[4]);
                }

            }

            tabla.Merge(cero);
            tabla.Merge(uno);
            tabla.Merge(dos);
            tabla.Merge(tres);
            tabla.Merge(cuatro);
            tabla.Merge(cinco);
            tabla.Merge(seis);
            tabla.Merge(siete);
            tabla.Merge(ocho);
            tabla.Merge(nueve);

            int n = 1;
            foreach (DataRow r in tabla.Rows)
            {
                r[0] = n.ToString();
                n++;
            }

            return tabla;
        }
        //CANDIDATOS 23

        public TextBox[,] CandidatosFinalistas2(TextBox[,] cajaTexto, Color color)
        {
            for (int fila = 0; fila <= 8; fila++)
            {
                for (int columna = 0; columna <= 8; columna++)
                {
                    if (cajaTexto[fila, columna].Text.Trim().Length == 3)
                    {
                        cajaTexto[fila, columna].BackColor = color;
                    }
                }
            }
            return cajaTexto;
        }

        public TextBox[,] CandidatosFinalistas3(TextBox[,] cajaTexto, Color color, Color color2)
        {
            for (int fila = 0; fila <= 8; fila++)
            {
                for (int columna = 0; columna <= 8; columna++)
                {
                    if (cajaTexto[fila, columna].Text.Trim().Length == 3)
                    {
                        cajaTexto[fila, columna].BackColor = color2;
                    }
                    else if (cajaTexto[fila, columna].Text.Trim().Length == 5)
                    {
                        cajaTexto[fila, columna].BackColor = color;
                    }
                }
            }
            return cajaTexto;
        }

        // RETORNA NOMBRE DEL ARCHIVO 
        public string NombreJuego(string pathArchivo)
        {
            string[] partes = pathArchivo.Split('\\');
            string nombreArchivo = partes[partes.Length - 1];
            string[] nombreJuego = nombreArchivo.Split('.');
            return nombreJuego[0];
        }

        public bool SolucionCompleta(string[,] solucion)
        {
            bool resultado = true;
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    if (solucion[i, j] == string.Empty || solucion[i, j] == null)
                    {
                        return false;
                    }
                }
            }
            return resultado;
        }


    }
}