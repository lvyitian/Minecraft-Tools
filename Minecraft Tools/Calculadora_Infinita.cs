using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Tools
{
    /// <summary>
    /// This class contains methods capable of making almost infinite calculations. Unlike most calculators it's very powerful, although a bit slower, but at least it can give full results with numbers made of thousands of numbers (I'm not sure there are words to say those large numbers yet). It can solve super large and complex calculations by splitting them into smaller ones, which is a bit slower and at the same time allows it to finish the calculation instead of givig errors like other calculators do. To operate it defines the numbers as base 10 (the one we usually use), but it splits them by it's power, so it will only store numbers from 0 to 9 for each power of 10, and it will save as much numbers as needed. Note that some functions aren't fully finished yet.
    /// </summary>
    internal static class Calculadora_Infinita
    {
        internal static readonly List<int> Lista_Cero = new List<int>(new int[] { 0 });
        internal static readonly List<int> Lista_Uno = new List<int>(new int[] { 1 });
        internal static readonly List<int> Lista_Dos = new List<int>(new int[] { 2 });
        internal static readonly List<int> Lista_Número_2 = new List<int>(new int[1] { 2 });

        /// <summary>
        /// Converts any number into any possible base. The returned figures are in the selected base.
        /// </summary>
        /// <param name="Lista_1">The number to convert.</param>
        /// <param name="Lista_Base">The base number.</param>
        /// <returns>Returns the number converted into figures in the selected base.</returns>
        internal static List<List<int>> Operación_Convertir_a_Base(List<int> Lista_1, List<int> Lista_Base)
        {
            List<List<int>> Lista_Números = new List<List<int>>();
            if (Lista_1.Count > 0 && Lista_Base.Count > 0 && Operación_Es_Cero(Lista_1) == false && Operación_Obtener_Mayor(Lista_Base, new List<int>(new int[] { 1 })) < 0)
            {
                if (Operación_Obtener_Mayor(Lista_Base, new List<int>(new int[] { 1, 0 })) != 0)
                {
                    List<int> Lista_Resto = new List<int>();
                    List<int> Lista_Cociente = Lista_1.GetRange(0, Lista_1.Count);
                    while (Operación_Es_Cero(Lista_Cociente) == false)
                    {
                        Lista_Cociente = Operación_División(Lista_Cociente, Lista_Base, out Lista_Resto);
                        Lista_Números.Insert(0, Lista_Resto.GetRange(0, Lista_Resto.Count));
                    }
                }
                else Lista_Números.Add(Lista_1.GetRange(0, Lista_1.Count));
            }
            else Lista_Números.Add(new List<int>(new int[] { 0 }));
            return Lista_Números;
        }

        internal static List<int> Operación_Convertir_a_Binario(List<int> Lista_1)
        {
            List<int> Lista_Números = new List<int>();
            if (Lista_1.Count > 0 && !Operación_Es_Cero(Lista_1))
            {
                List<int> Lista_Resto = new List<int>();
                List<int> Lista_Cociente = Lista_1.GetRange(0, Lista_1.Count);
                while (!Operación_Es_Cero(Lista_Cociente))
                {
                    Lista_Cociente = Operación_División(Lista_Cociente, Lista_Número_2, out Lista_Resto);
                    Lista_Números.InsertRange(0, Lista_Resto.GetRange(0, Lista_Resto.Count));
                }
            }
            else Lista_Números.Add(0);
            return Lista_Números;
        }

        /// <summary>
        /// Converts any number from any possible base. The returned figures are in base 10.
        /// </summary>
        /// <param name="Lista_1">The number to convert.</param>
        /// <param name="Lista_Base">The base number.</param>
        /// <returns>Returns the number converted into figures from the selected base.</returns>
        internal static List<int> Operación_Convertir_desde_Base(List<List<int>> Lista_1, List<int> Lista_Base)
        {
            List<int> Lista_Números = new List<int>(new int[] { 0 });
            if (Lista_1.Count > 0 && Lista_Base.Count > 0 && Operación_Obtener_Mayor(Lista_Base, new List<int>(new int[] { 1 })) < 0)
            {
                for (int Índice = Lista_1.Count - 1, Exponente = 0; Índice >= 0; Índice--, Exponente++)
                {
                    if (!Operación_Es_Cero(Lista_1[Índice])) Lista_Números = Operación_Suma(Lista_Números, Operación_Multiplicación(Lista_1[Índice], Operación_Potencia(Lista_Base, Traducir_Número(Exponente.ToString()))));
                    //List<int> Lista_Temporal = Operación_Multiplicación(Lista_1[Índice], Operación_Potencia(Lista_2, Traducir_Número(Exponente.ToString())));
                    //Editor_RTF.Text += Traducir_Número(Lista_Temporal) + "\r\n";
                    //Lista_Números = Operación_Suma(Lista_Números, Lista_Temporal);
                    /*List<int> Lista_Temporal = new List<List<int>>();
                    for (int Subíndice = 0; Subíndice < Lista_1[Índice].Count; Subíndice++)
                    {
                        Lista_Números = Operación_Potencia(Lista_1[Índice]);
                    }*/
                }
            }
            //else Lista_Números.Add(0);
            return Lista_Números;
        }

        internal static List<int> Operación_Convertir_desde_Binario(List<int> Lista_1)
        {
            List<int> Lista_Números = new List<int>(new int[] { 0 });
            //if (Lista_1.Count > 0)
            {
                for (int Índice = Lista_1.Count - 1, Exponente = 0; Índice >= 0; Índice--, Exponente++)
                {
                    if (Lista_1[Índice] != 0) Lista_Números = Operación_Suma(Lista_Números, Operación_Potencia_Lenta(Lista_Dos, Traducir_Número(Exponente.ToString())));
                    //List<int> Lista_Temporal = Operación_Multiplicación(Lista_1[Índice], Operación_Potencia(Lista_2, Traducir_Número(Exponente.ToString())));
                    //Editor_RTF.Text += Traducir_Número(Lista_Temporal) + "\r\n";
                    //Lista_Números = Operación_Suma(Lista_Números, Lista_Temporal);
                    /*List<int> Lista_Temporal = new List<List<int>>();
                    for (int Subíndice = 0; Subíndice < Lista_1[Índice].Count; Subíndice++)
                    {
                        Lista_Números = Operación_Potencia(Lista_1[Índice]);
                    }*/
                }
            }
            //else Lista_Números.Add(0);
            return Lista_Números;
        }

        internal static List<int> Operación_División(List<int> Lista_1, List<int> Lista_2, out List<int> Lista_Resto)
        {
            //Editor_RTF.Text = null;
            Lista_Resto = new List<int>();
            List<int> Lista_Números = new List<int>();
            if (Lista_1.Count > 0 && Lista_2.Count > 0 && Operación_Es_Cero(Lista_1) == false && Operación_Es_Cero(Lista_2) == false)
            {
                int Comparación = Operación_Obtener_Mayor(Lista_1, Lista_2);
                if (Comparación < 0)
                {
                    List<int> Lista_Inicial = new List<int>();
                    List<int> Lista_Final = new List<int>();
                    int Longitud = 1;
                    for (; Longitud <= Lista_1.Count; Longitud++)
                    {
                        Lista_Inicial = Lista_1.GetRange(0, Longitud);
                        if (Operación_Obtener_Mayor(Lista_Inicial, Lista_2) <= 0) break;
                    }

                    //while (Longitud < Lista_1.Count && Lista_Inicial.Count > 1) //Operación_Es_Cero(Lista_Inicial) == false)
                    for (;;)
                    {
                        int Multiplicador = 1;
                        for (; Multiplicador <= 10; Multiplicador++)
                        {
                            Lista_Final = Operación_Multiplicación(Lista_2, Traducir_Número(Multiplicador.ToString()));
                            if (Operación_Obtener_Mayor(Lista_Inicial, Lista_Final) > 0)
                            {
                                Multiplicador--;
                                Lista_Final = Operación_Multiplicación(Lista_2, Traducir_Número(Multiplicador.ToString()));
                                break;
                            }
                        }
                        Lista_Números.Add(Multiplicador);
                        //Editor_RTF.Text += Traducir_Número(Lista_Inicial) + " ini y " + Multiplicador.ToString() + " mul y " + Traducir_Número(Lista_Final) + "\r\n";
                        Lista_Inicial = Operación_Resta(Lista_Inicial, Lista_Final);
                        /*if (Operación_Es_Cero(Lista_Inicial)) break;
                        else */
                        if (Longitud >= Lista_1.Count)
                        {
                            if (Operación_Es_Cero(Lista_Inicial) == false) Lista_Resto = Lista_Inicial;
                            break;
                        }
                        Lista_Inicial.Add(Lista_1[Longitud]);
                        //Editor_RTF.Text += "resto = " + Traducir_Número(Lista_Inicial) + "\r\n";
                        Longitud++;
                    }
                }
                else if (Comparación > 0)
                {
                    Lista_Números.Add(0);
                    Lista_Resto = Lista_1.GetRange(0, Lista_1.Count);
                }
                else Lista_Números.Add(1);
            }
            else Lista_Números.Add(0);
            if (Lista_Resto.Count <= 0) Lista_Resto.Add(0);
            return Lista_Números;
        }

        internal static bool Operación_Es_Cero(List<int> Lista_Números)
        {
            for (int Índice = 0; Índice < Lista_Números.Count; Índice++)
            {
                if (Lista_Números[Índice] != 0) return false;
            }
            return true;
        }

        internal static void Operación_Igualar_Longitudes(ref List<int> Lista_1, ref List<int> Lista_2)
        {
            if (Lista_1.Count != Lista_2.Count) // Falta en Decimal...
            {
                int Longitud = Math.Max(Lista_1.Count, Lista_2.Count);
                while (Lista_1.Count < Longitud) Lista_1.Insert(0, 0);
                while (Lista_2.Count < Longitud) Lista_2.Insert(0, 0);
            }
        }

        internal static List<int> Operación_Multiplicación(List<int> Lista_1, List<int> Lista_2)
        {
            List<int> Lista_Números = new List<int>();
            if (Lista_1.Count > 0 && Lista_2.Count > 0)
            {
                //Operación_Igualar_Longitud(ref Lista_1, ref Lista_2);
                List<List<int>> Lista_Listas_Números = new List<List<int>>();
                for (int Índice_2 = Lista_2.Count - 1, Desplazamiento = 0, Resto = 0, Valor = 0; Índice_2 >= 0; Índice_2--, Desplazamiento++)
                {
                    Lista_Números = new List<int>();
                    Resto = 0;
                    for (int Índice_1 = Lista_1.Count - 1; Índice_1 >= 0; Índice_1--)
                    {
                        Valor = (Lista_1[Índice_1] * Lista_2[Índice_2]) + Resto;
                        if (Valor < -9 || Valor > 9)
                        {
                            Resto = Valor / 10;
                            Valor -= Resto * 10;
                        }
                        else Resto = 0;
                        Lista_Números.Insert(0, Valor);
                    }
                    if (Resto != 0) Lista_Números.Insert(0, Resto);
                    //for (int Índice = 0; Índice < Desplazamiento; Índice++) Lista_Números.Add(0);
                    Lista_Números.AddRange(new int[Desplazamiento]);
                    while (Lista_Números.Count > 1 && Lista_Números[0] == 0) Lista_Números.RemoveAt(0);
                    //Editor_RTF.Text += Traducir_Número(Lista_Números) + "\r\n";
                    Lista_Listas_Números.Add(Lista_Números.GetRange(0, Lista_Números.Count));
                }
                Lista_Números = Lista_Listas_Números[0];
                for (int Índice = 1; Índice < Lista_Listas_Números.Count; Índice++) Lista_Números = Operación_Suma(Lista_Números, Lista_Listas_Números[Índice]);
                while (Lista_Números.Count > 1 && Lista_Números[0] == 0) Lista_Números.RemoveAt(0);
                //Editor_RTF.Text += Traducir_Número(Lista_1) + " x " + Traducir_Número(Lista_2) + " = " + Traducir_Número(Lista_Números) + "\r\n";
            }
            return Lista_Números;
        }

        internal static List<int> Operación_Multiplicación_Binaria(List<int> Lista_1, List<int> Lista_2)
        {
            List<int> Lista_Números = new List<int>(new int[Lista_1.Count]);
            for (int Índice_2 = Lista_2.Count - 1/*, Números = 0*/; Índice_2 >= 0; Índice_2--)
            {
                //Números++;
                /*if (Lista_Números.Count < Números) */
                if (Lista_2[Índice_2] != 0)
                {
                    for (int Índice_1 = Lista_1.Count - 1, Índice_Número = 0; Índice_1 >= 0; Índice_1--, Índice_Número++)
                    {
                        //if (Índice_Número)
                        Lista_Números[Índice_Número] += Lista_1[Índice_1];
                    }
                }
                Lista_1.Add(0);
                Lista_Números.Add(0);
            }
            int Resto = 0;
            for (int Índice = 0; Índice < Lista_Números.Count; Índice++)
            {
                if (Resto > 0)
                {
                    Lista_Números[Índice] += Resto;
                    Resto = 0;
                }
                if (Lista_Números[Índice] > 1)
                {
                    Resto = Lista_Números[Índice] / 2;
                    Lista_Números[Índice] = Lista_Números[Índice] % 2;
                }
            }
            /*while (Resto > 0)
            {
                Lista_Números[Índice] += Resto;
                Resto = 0;
            }*/

            if (Resto > 0)
            {
                Resto %= 2;
                Lista_Números.Add(Resto % 2);
            }
            while (Lista_Números[Lista_Números.Count - 1] == 0) Lista_Números.RemoveAt(Lista_Números.Count - 1);
            Lista_Números.Reverse();
            return Lista_Números;
        }

        /// <summary>
        /// Si devuelve -1, A es menor que B.
        /// Si devuelve +1, A es mayor que B.
        /// Si devuelve 0, A es igual que B.
        /// </summary>
        /// <param name="Lista_1">Número A</param>
        /// <param name="Lista_2">Número B</param>
        /// <returns>Resultado de la comparación</returns>
        internal static int Operación_Obtener_Mayor(List<int> Lista_1, List<int> Lista_2)
        {
            if (Lista_1.Count > 0 || Lista_2.Count > 0)
            {
                if (Lista_1.Count == Lista_2.Count)
                {
                    for (int Índice = 0; Índice < Lista_1.Count; Índice++)
                    {
                        if (Lista_1[Índice] > Lista_2[Índice]) return -1;
                        else if (Lista_2[Índice] > Lista_1[Índice]) return 1;
                    }
                }
                else
                {
                    List<int> Lista_Temporal_1 = Lista_1.GetRange(0, Lista_1.Count);
                    List<int> Lista_Temporal_2 = Lista_2.GetRange(0, Lista_2.Count);
                    Operación_Igualar_Longitudes(ref Lista_Temporal_1, ref Lista_Temporal_2);
                    for (int Índice = 0; Índice < Lista_Temporal_1.Count; Índice++)
                    {
                        if (Lista_Temporal_1[Índice] > Lista_Temporal_2[Índice]) return -1;
                        else if (Lista_Temporal_2[Índice] > Lista_Temporal_1[Índice]) return 1;
                    }
                }
            }
            return 0;
        }

        internal static List<int> Operación_Potencia(List<int> Lista_1, List<int> Lista_2)
        {
            List<int> Lista_Resultado = new List<int>(new int[] { 1 });
            Lista_1 = Operación_Convertir_a_Binario(Lista_1);
            Lista_2 = Operación_Convertir_a_Binario(Lista_2);
            for (int Índice = Lista_2.Count - 1; Índice >= 0; Índice--)
            {
                if (Lista_2[Índice] != 0) Lista_Resultado = Operación_Multiplicación_Binaria(Lista_Resultado, Lista_1);
                Lista_1 = Operación_Multiplicación_Binaria(Lista_1, Lista_1);
            }
            return Operación_Convertir_desde_Binario(Lista_Resultado);
        }

        internal static List<int> Operación_Potencia_Lenta(List<int> Lista_1, List<int> Lista_2)
        {
            List<int> Lista_Números = Lista_1.GetRange(0, Lista_1.Count);
            if (Lista_1.Count > 0 && Lista_2.Count > 0)
            {
                if (!Operación_Es_Cero(Lista_2))
                {
                    List<int> Lista_Índice = new List<int>(new int[] { 1 });
                    while (Operación_Obtener_Mayor(Lista_Índice, Lista_2) > 0)
                    {
                        Lista_Números = Operación_Multiplicación(Lista_Números, Lista_1);
                        Lista_Índice = Operación_Sumar_1(Lista_Índice);
                    }
                }
                else Lista_Números = new List<int>(new int[] { 1 });
            }
            return Lista_Números;
        }

        internal static List<int> Operación_Resta(List<int> Lista_1, List<int> Lista_2)
        {
            List<int> Lista_Números = new List<int>();
            if (Lista_1.Count > 0 && Lista_2.Count > 0)
            {
                Operación_Igualar_Longitudes(ref Lista_1, ref Lista_2);
                int Comparación = Operación_Obtener_Mayor(Lista_1, Lista_2);
                if (Comparación < 0)
                {
                    int Resto = 0;
                    for (int Índice = Lista_1.Count - 1, Valor = 0; Índice >= 0; Índice--)
                    {
                        if (Lista_1[Índice] < (Lista_2[Índice] + Resto))
                        {
                            Valor = (Lista_1[Índice] + 10) - (Lista_2[Índice] + Resto);
                            Resto = 1;
                        }
                        else
                        {
                            Valor = Lista_1[Índice] - (Lista_2[Índice] + Resto);
                            Resto = 0;
                        }
                        Lista_Números.Insert(0, Valor);
                    }
                    if (Resto != 0) Lista_Números.Insert(0, Resto);
                }
                else if (Comparación > 0)
                {
                    int Resto = 0;
                    for (int Índice = Lista_2.Count - 1, Valor = 0; Índice >= 0; Índice--)
                    {
                        if (Lista_2[Índice] < (Lista_1[Índice] + Resto))
                        {
                            Valor = (Lista_2[Índice] + 10) - (Lista_1[Índice] + Resto);
                            Resto = 1;
                        }
                        else
                        {
                            Valor = Lista_2[Índice] - (Lista_1[Índice] + Resto);
                            Resto = 0;
                        }
                        Lista_Números.Insert(0, Valor);
                    }
                    if (Resto != 0) Lista_Números.Insert(0, Resto);
                    for (int Índice = 0; Índice < Lista_Números.Count; Índice++) Lista_Números[Índice] = -Lista_Números[Índice];
                }
                else Lista_Números.Add(0);
                //while (Lista_1.Count > 1 && Lista_1[0] == 0) Lista_1.RemoveAt(0);
                //while (Lista_2.Count > 1 && Lista_2[0] == 0) Lista_2.RemoveAt(0);
                while (Lista_Números.Count > 1 && Lista_Números[0] == 0) Lista_Números.RemoveAt(0);
                //Editor_RTF.Text += Traducir_Número(Lista_1) + " - " + Traducir_Número(Lista_2) + " = " + Traducir_Número(Lista_Números) + "\r\n";
            }
            else Lista_Números.Add(0);
            return Lista_Números;
        }

        internal static List<int> Operación_Suma(List<int> Lista_1, List<int> Lista_2)
        {
            List<int> Lista_Números = new List<int>();
            if (Lista_1.Count > 0 && Lista_2.Count > 0)
            {
                Operación_Igualar_Longitudes(ref Lista_1, ref Lista_2);
                int Resto = 0;
                for (int Índice = Lista_1.Count - 1, Valor = 0; Índice >= 0; Índice--)
                {
                    Valor = (Lista_1[Índice] + Lista_2[Índice]) + Resto;
                    if (Valor < -9 || Valor > 9)
                    {
                        Resto = Valor / 10;
                        Valor -= Resto * 10;
                    }
                    else Resto = 0;
                    Lista_Números.Insert(0, Valor);
                }
                if (Resto != 0) Lista_Números.Insert(0, Resto);
                //while (Lista_1.Count > 1 && Lista_1[0] == 0) Lista_1.RemoveAt(0);
                //while (Lista_2.Count > 1 && Lista_2[0] == 0) Lista_2.RemoveAt(0);
                while (Lista_Números.Count > 1 && Lista_Números[0] == 0) Lista_Números.RemoveAt(0);
                //Editor_RTF.Text += Traducir_Número(Lista_1) + " + " + Traducir_Número(Lista_2) + " = " + Traducir_Número(Lista_Números) + "\r\n";
            }
            else Lista_Números.Add(0);
            return Lista_Números;
        }

        internal static List<int> Operación_Sumar_1(List<int> Lista_Números)
        {
            if (Lista_Números.Count > 0)
            {
                int Índice = Lista_Números.Contains(Int32.MinValue) == false ? Lista_Números.Count - 1 : Lista_Números.IndexOf(Int32.MinValue) - 1, Resto = 0;
                //Lista_Números[Índice] += Lista_Números[Índice] >= 0 ? 1 : -1;
                Lista_Números[Índice]++;
                for (; Índice >= 0; Índice--)
                {
                    Lista_Números[Índice] += Resto;
                    if (Lista_Números[Índice] < -9 || Lista_Números[Índice] > 9)
                    {
                        Resto = Lista_Números[Índice] / 10;
                        Lista_Números[Índice] -= Resto * 10;
                    }
                    else
                    {
                        Resto = 0;
                        break;
                    }
                }
                if (Resto != 0) Lista_Números.Insert(0, Resto);
            }
            return Lista_Números;
        }

        internal static string Traducir_Número(List<int> Lista_Números)
        {
            string Texto = string.Empty;
            if (Lista_Números.Count > 0)
            {
                foreach (int Valor in Lista_Números)
                {
                    if (Valor >= 0 && Valor <= 9) Texto += Valor.ToString();
                    else if (Valor >= -9 && Valor <= -1)
                    {
                        if (!Texto.StartsWith("-")) Texto = Texto.Insert(0, "-");
                        Texto += (-Valor).ToString();
                    }
                    else if (Valor <= int.MinValue) Texto += ',';
                }
                if (Texto.Length > 3)
                {
                    int Índice_Decimal = Texto.IndexOf(',');
                    for (int Índice = Índice_Decimal < 0 ? Texto.Length - 3 : Índice_Decimal - 3; Índice > (Texto[0] != '-' ? 0 : 1); Índice -= 3) Texto = Texto.Insert(Índice, ".");
                }
            }
            return Texto;
        }

        internal static List<int> Traducir_Número(string Texto)
        {
            List<int> Lista_Números = new List<int>();
            if (string.IsNullOrEmpty(Texto) == false)
            {
                bool Número_Negativo = Texto.Contains("-");
                foreach (char Caracter in Texto)
                {
                    if (char.IsDigit(Caracter)) Lista_Números.Add(!Número_Negativo ? ((int)Caracter - 48) : -((int)Caracter - 48));
                    else if (Caracter == ',' && !Lista_Números.Contains(int.MinValue))
                    {
                        if (Lista_Números.Count <= 0) Lista_Números.Add(0);
                        Lista_Números.Add(int.MinValue);
                    }
                }
                if (Lista_Números.Count > 0 && Lista_Números[Lista_Números.Count - 1] <= int.MinValue) Lista_Números.RemoveAt(Lista_Números.Count - 1);
            }
            return Lista_Números;
        }

        internal static string Traducir_Número_Sin_Puntuación(List<int> Lista_Números)
        {
            string Texto = string.Empty;
            if (Lista_Números.Count > 0)
            {
                foreach (int Valor in Lista_Números)
                {
                    if (Valor >= 0 && Valor <= 9) Texto += Valor.ToString();
                    else if (Valor >= -9 && Valor <= -1)
                    {
                        if (!Texto.StartsWith("-")) Texto = Texto.Insert(0, "-");
                        Texto += (-Valor).ToString();
                    }
                    else if (Valor <= int.MinValue) Texto += ',';
                }
                /*if (Texto.Length > 3)
                {
                    int Índice_Decimal = Texto.IndexOf(',');
                    for (int Índice = Índice_Decimal < 0 ? Texto.Length - 3 : Índice_Decimal - 3; Índice > (Texto[0] != '-' ? 0 : 1); Índice -= 3) Texto = Texto.Insert(Índice, ".");
                }*/
            }
            return Texto;
        }

        internal static readonly List<char> Lista_Caracteres_Base_36 = new List<char>(new char[36] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' });

        internal static string Traducir_Número_Base(List<List<int>> Lista_Números, List<int> Lista_Base)
        {
            string Texto = string.Empty;
            if (Operación_Obtener_Mayor(Lista_Base, new List<int>(new int[] { 1, 0 })) != 0)
            {
                if (Operación_Obtener_Mayor(new List<int>(new int[] { 3, 6 }), Lista_Base) <= 0)
                {
                    for (int Índice = 0; Índice < Lista_Números.Count; Índice++)
                    {
                        int Número = 0;
                        for (int Subíndice = Lista_Números[Índice].Count - 1, Multiplicador = 1; Subíndice >= 0; Subíndice--, Multiplicador *= 10) Número += Lista_Números[Índice][Subíndice] * Multiplicador;
                        Texto += Lista_Caracteres_Base_36[Número];
                    }
                }
                else
                {
                    for (int Índice = 0; Índice < Lista_Números.Count; Índice++)
                    {
                        for (int Subíndice = 0; Subíndice < Lista_Números[Índice].Count; Subíndice++) Texto += Lista_Números[Índice][Subíndice].ToString();
                        Texto += "~";
                    }
                    Texto = Texto.TrimEnd("~".ToCharArray());
                }
            }
            else
            {
                for (int Índice = 0; Índice < Lista_Números.Count; Índice++)
                {
                    //int Número = 0;
                    for (int Subíndice = 0; Subíndice < Lista_Números[Índice].Count; Subíndice++) Texto += Lista_Caracteres_Base_36[Lista_Números[Índice][Subíndice]];
                }
            }
            return Texto;
        }

        internal static List<List<int>> Traducir_Texto_Base(string Texto, List<int> Lista_Base)
        {
            List<List<int>> Lista_Números = new List<List<int>>();
            if (string.IsNullOrEmpty(Texto) == false)
            {
                Texto = Texto.Replace("-", null).Replace(".", null).Replace(",", null);
                bool Separador = false;
                for (int Índice = 0; Índice < Texto.Length; Índice++)
                {
                    if (!Lista_Caracteres_Base_36.Contains(Texto[Índice]))
                    {
                        Separador = true;
                        break;
                    }
                }
                if (!Separador) for (int Índice = 0; Índice < Texto.Length; Índice++)
                    {
                        List<int> Lista_Temporal = Traducir_Número(Lista_Caracteres_Base_36.IndexOf(Texto[Índice]).ToString());
                        if (Operación_Obtener_Mayor(Lista_Base, Lista_Temporal) < 0) Lista_Números.Add(Lista_Temporal);
                    }
                else
                {
                    List<int> Lista_Temporal = new List<int>();
                    for (int Índice = Texto.Length - 1; Índice >= 0; Índice--)
                    {
                        if (!Lista_Caracteres_Base_36.Contains(Texto[Índice]))
                        {
                            Lista_Números.Add(Lista_Temporal.GetRange(0, Lista_Temporal.Count));
                            Lista_Temporal.Clear();
                        }
                        else Lista_Temporal.Add(Texto[Índice]);
                    }
                }
                //if (Lista_Números.Count <= 0) Lista_Números.Add(new List<int>(new int[] { 0 }));
                /*if (Lista_Números.Count > 0)
                {
                    for (int Índice = 0; Índice < Lista_Números.Count; Índice++)
                    {

                    }
                }*/
            }
            //else Lista_Números.Add(new List<int>(new int[] { 0 }));
            return Lista_Números;
        }

        internal static sbyte Traducir_Número(char Caracter)
        {
            try
            {
                if (Caracter == '1') return 1;
                else if (Caracter == '2') return 2;
                else if (Caracter == '3') return 3;
                else if (Caracter == '4') return 4;
                else if (Caracter == '5') return 5;
                else if (Caracter == '6') return 6;
                else if (Caracter == '7') return 7;
                else if (Caracter == '8') return 8;
                else if (Caracter == '9') return 9;
                else return 0;
            }
            catch { }
            return 0;
        }
    }
}
