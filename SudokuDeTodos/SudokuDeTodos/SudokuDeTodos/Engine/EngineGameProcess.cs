using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SudokuDeTodos.Engine
{
    public class EngineGameProcess : IEngineGameProcess
    {
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
    }
}