using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace  Beta_elliptic_model
{
    public class readInkml_file
    {
        public double[,] matrix_xy;
        int ligne, colonne, nombre_segment;
        string[] multiArray, multiArray_3;
        string multiArray_2, element_liste;
        public double[,] method_readInkml_file(string path_file)
        {
            string[] lines = System.IO.File.ReadAllLines(path_file);
            int i, k, j, indice, drapeau_leve, taille_matrice_xy, ligne;
            string m_line, xy;


            char[] t = { '\r', ',' };
            char[] t3 = { '\r', ' ' };
            i = 0;
            List<string> linesToWrite = new List<string>();
            while (lines[i].Trim(t3) != "</ink>")
            {
                int i_d = lines[i].IndexOf("<trace>");
                int i_d_2 = lines[i].IndexOf("<trace type=" + '"' + "penUp" + '"' + ">");

                if (i_d > 0)
                {
                    j = lines[i].IndexOf(">");
                    k = lines[i].IndexOf("</");
                    m_line = (lines[i].Substring(j + 1, k - j - 1));
                    m_line.Replace(',', ';');
                    multiArray = m_line.Split(t);

                    for (int rowIndex = 0; rowIndex < multiArray.GetLength(0); rowIndex++)
                    {
                        multiArray_2 = multiArray[rowIndex];
                        multiArray_3 = multiArray_2.Split(t3);
                        //linesToWrite.Add(multiArray[rowIndex]);

                        xy = multiArray_3[0] + " " + multiArray_3[1] + " " + "0.00" + " " + multiArray_3[2] + " " + multiArray_3[3];
                        linesToWrite.Add(xy);
                    }

                }
                else if (i_d_2 > 0)
                {
                    j = lines[i].IndexOf(">");
                    k = lines[i].IndexOf("</");
                    m_line = (lines[i].Substring(j + 1, k - j - 1));
                    //m_line.Replace(',', ';');
                    multiArray = m_line.Split(t);

                    for (int rowIndex = 0; rowIndex < multiArray.GetLength(0); rowIndex++)

                        linesToWrite.Add(multiArray[rowIndex]);

                }

                i++;
            }
            indice = 0;
            drapeau_leve = 0;
            nombre_segment = 0;

            for (int h = 0; h < linesToWrite.Count; h++)
            {
                element_liste = linesToWrite[h];
                Console.WriteLine("element" + element_liste);
                multiArray_3 = element_liste.Split(t3);
                if (multiArray_3[2].Equals("0.00"))
                {
                    indice++;
                    drapeau_leve = 1;
                }
                else if (drapeau_leve == 1)
                {
                    nombre_segment += 1;
                    drapeau_leve = 0;
                }

            }

            taille_matrice_xy = indice + 4 + 2 * (nombre_segment - 1);
            matrix_xy = new double[taille_matrice_xy, 3];
            matrix_xy[0, 0] = 0;
            matrix_xy[0, 1] = 0;
            matrix_xy[0, 2] = 0;
            matrix_xy[1, 0] = 0;
            matrix_xy[1, 1] = 0;
            matrix_xy[1, 2] = 0;
            ligne = 2;
            for (int h = 0; h < linesToWrite.Count; h++)
            {
                element_liste = linesToWrite[h];
                multiArray_3 = element_liste.Split(t3);
                Console.WriteLine("multiArray_3" + multiArray_3);
                if (multiArray_3[2].Equals("0.00"))
                {

                    matrix_xy[ligne, 0] = Convert.ToDouble(multiArray_3[0].Replace(".",","));
                    //Console.WriteLine("multiArray_3" + Convert.ToDouble(multiArray_3[0]));
                    matrix_xy[ligne, 1] = Convert.ToDouble(multiArray_3[1].Replace(".", ","));
                    //Console.WriteLine("multiArray_3" + Convert.ToDouble(multiArray_3[1]));
                    matrix_xy[ligne, 2] = Convert.ToDouble(multiArray_3[4].Replace(".", ","));
                    Console.WriteLine("multiArray_3  " + Convert.ToDouble(multiArray_3[0].Replace(".", ","))+" "+ Convert.ToDouble(multiArray_3[1].Replace(".", ",")) + "  "+ Convert.ToDouble(multiArray_3[4].Replace(".", ",")));
                    indice++;
                    drapeau_leve = 1;
                    ligne++;
                }
                else if (drapeau_leve == 1)
                {
                    matrix_xy[ligne, 0] = 0;
                    matrix_xy[ligne, 1] = 0;
                    matrix_xy[ligne, 2] = 0; ligne++;
                    matrix_xy[ligne, 0] = 0;
                    matrix_xy[ligne, 1] = 0;
                    matrix_xy[ligne, 2] = 0; ligne++;
                    drapeau_leve = 0;
                }

            }

            matrix_xy[matrix_xy.GetLength(0) - 2, 0] = 0;
            matrix_xy[matrix_xy.GetLength(0) - 2, 1] = 0;
            matrix_xy[matrix_xy.GetLength(0) - 2, 2] = 0;
            matrix_xy[matrix_xy.GetLength(0) - 1, 0] = 0;
            matrix_xy[matrix_xy.GetLength(0) - 1, 1] = 0;
            matrix_xy[matrix_xy.GetLength(0) - 1, 2] = 0;

            return matrix_xy;




        }
    }
}
