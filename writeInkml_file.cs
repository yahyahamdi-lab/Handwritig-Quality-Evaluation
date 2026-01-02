using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Beta_elliptic_model
{
    public class writeInkml_file
    {
        public void method_writeInkml_file(double[,] point_Trajectoire)
        {
            double val, x_point_m, y_point_m, x_iiih, y_iiih;
            string id_2, x_point_m_string, y_point_m_string;
            double abc;
            int nombre_de_pseudo_mots, taille_pseudo_mot_courant, numero_pseudo_mot_courant, taille_du_pseudo_mot_fini, drapeau_debut_passage_levee, indice_point_courant_dans_pseudo_mot_courant;

            //corp_inkml[9] = "  <trace duration=" + '"' + "1872" + " " + "id=" + "id001" + " " + "type=" + '"' + "penDown" + '"' + ">" + "\r\n"; 
            //corp_inkml[10] = "</ink>";

            drapeau_debut_passage_levee = 0;
            abc = point_Trajectoire.GetLength(0);
            nombre_de_pseudo_mots = 0;
            taille_pseudo_mot_courant = 0;
            for (int iiih = 0; iiih < abc; iiih++)
            {
                x_iiih = point_Trajectoire[iiih, 0];
                y_iiih = point_Trajectoire[iiih, 1];
                if ((x_iiih != 0) || (y_iiih != 0))
                {
                    taille_pseudo_mot_courant = taille_pseudo_mot_courant + 1;
                    drapeau_debut_passage_levee = 0;
                }
                else if (drapeau_debut_passage_levee == 0)
                {
                    if (taille_pseudo_mot_courant >= 1)
                        nombre_de_pseudo_mots = nombre_de_pseudo_mots + 1;

                    drapeau_debut_passage_levee = 1;
                    taille_pseudo_mot_courant = 0;
                }

            }
            // %%%% comptage de la taille des pseudos mots %%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            drapeau_debut_passage_levee = 0;
            int[] vect_taille_pseudos_mots = new int[nombre_de_pseudo_mots];  //zeros(nombre_de_pseudo_mots, 1);
            for (int t = 0; t < nombre_de_pseudo_mots; t++)
                vect_taille_pseudos_mots[t] = 0;

            taille_pseudo_mot_courant = 0;
            numero_pseudo_mot_courant = -1;
            for (int iiih = 0; iiih < abc; iiih++)
            {
                x_iiih = point_Trajectoire[iiih, 0];
                y_iiih = point_Trajectoire[iiih, 1];
                if ((x_iiih != 0) || (y_iiih != 0))
                {
                    taille_pseudo_mot_courant = taille_pseudo_mot_courant + 1; /// !!!!!!!!!
                    drapeau_debut_passage_levee = 0;
                }
                else if (drapeau_debut_passage_levee == 0)
                {
                    if (taille_pseudo_mot_courant >= 1)
                    {
                        numero_pseudo_mot_courant = numero_pseudo_mot_courant + 1;
                        vect_taille_pseudos_mots[numero_pseudo_mot_courant] = taille_pseudo_mot_courant;
                    }

                    drapeau_debut_passage_levee = 1;
                    taille_pseudo_mot_courant = 0;
                }

            }

            // préparation de vecteur de matrice mat_pseudo_mot

            string chemin = "/mnt/sdcard/KALIGO/test_inkml.ikml";
            string[] corp_inkml = new string[10 + nombre_de_pseudo_mots];
            corp_inkml[0] = "<?xml version=" + '"' + "1.0" + '"' + " encoding=" + '"' + "ASCII" + '"' + "?" + ">" + "\r\n";
            corp_inkml[1] = "<ink xmlns=" + '"' + "http://www.w3.org/2003/InkML" + '"' + ">" + "\r\n";
            corp_inkml[2] = "  <context inkSourceRef=" + '"' + "Tablet PC" + '"' + "/>" + "\r\n";
            corp_inkml[3] = "  <captureDevice sampleRate=" + '"' + "125" + '"' + ">" + "\r\n";
            corp_inkml[4] = "    <channelList>" + "\r\n";
            corp_inkml[5] = "     <channelDef name=" + '"' + "X" + '"' + "/>" + "\r\n";
            corp_inkml[6] = "     <channelDef name=" + '"' + "Y" + '"' + "/>" + "\r\n";
            corp_inkml[7] = "    </channelList>" + "\r\n";
            corp_inkml[8] = "  </captureDevice>" + "\r\n";

            drapeau_debut_passage_levee = 0;
            numero_pseudo_mot_courant = 0;
            indice_point_courant_dans_pseudo_mot_courant = 0;
            double[][,] mat_pseudo_mot = new double[nombre_de_pseudo_mots][,];
            //Console.WriteLine(nombre_de_pseudo_mots);
            for (int ji = 0; ji < nombre_de_pseudo_mots; ji++)
            {
                mat_pseudo_mot[ji] = new double[vect_taille_pseudos_mots[ji], 2];
                //Console.WriteLine(vect_taille_pseudos_mots[ji]);
            }

            if (nombre_de_pseudo_mots < 10)
                id_2 = "00";
            else
                id_2 = "0";


            drapeau_debut_passage_levee = 0;
            indice_point_courant_dans_pseudo_mot_courant = 0;
            for (int iiih = 0; iiih < abc; iiih++)
            {
                x_iiih = point_Trajectoire[iiih, 0];
                y_iiih = point_Trajectoire[iiih, 1];
                if ((x_iiih != 0) || (y_iiih != 0))
                {
                    indice_point_courant_dans_pseudo_mot_courant = indice_point_courant_dans_pseudo_mot_courant + 1;
                    //Console.WriteLine(indice_point_courant_dans_pseudo_mot_courant -1);
                    mat_pseudo_mot[numero_pseudo_mot_courant][indice_point_courant_dans_pseudo_mot_courant - 1, 0] = x_iiih;
                    mat_pseudo_mot[numero_pseudo_mot_courant][indice_point_courant_dans_pseudo_mot_courant - 1, 1] = y_iiih;

                    drapeau_debut_passage_levee = 0;

                }
                else if (drapeau_debut_passage_levee == 0)
                {
                    taille_du_pseudo_mot_fini = indice_point_courant_dans_pseudo_mot_courant;
                    if (taille_du_pseudo_mot_fini >= 1)
                    {
                        numero_pseudo_mot_courant = numero_pseudo_mot_courant + 1;
                        val = 125;
                        double duration = Math.Round((1000 / val) * (indice_point_courant_dans_pseudo_mot_courant + 1));
                        string duration_sting = duration.ToString();
                        double id = (numero_pseudo_mot_courant * 2) - 1;
                        string id_string = id.ToString();

                        corp_inkml[8 + numero_pseudo_mot_courant] = "  <trace duration=" + '"' + duration_sting + '"' + " id=" + '"' + "id" + id_2 + id_string + '"' + " type=" + '"' + "penDown" + '"' + ">"; 
                        for (int indice_point_m = 0; indice_point_m < indice_point_courant_dans_pseudo_mot_courant; indice_point_m++)
                        {
                            x_point_m = Math.Truncate(mat_pseudo_mot[numero_pseudo_mot_courant - 1][indice_point_m, 0]);
                            y_point_m = Math.Truncate(mat_pseudo_mot[numero_pseudo_mot_courant - 1][indice_point_m, 1]);
                            //Console.WriteLine(x_point_m);
                            x_point_m_string = x_point_m.ToString();
                            y_point_m_string = y_point_m.ToString();
                            if(indice_point_m != indice_point_courant_dans_pseudo_mot_courant-1)
                              corp_inkml[8 + numero_pseudo_mot_courant] = corp_inkml[8 + numero_pseudo_mot_courant] + x_point_m + " " + y_point_m + ",";
                            else
                                corp_inkml[8 + numero_pseudo_mot_courant] = corp_inkml[8 + numero_pseudo_mot_courant] + x_point_m + " " + y_point_m;
                        }

                        corp_inkml[8 + numero_pseudo_mot_courant] = corp_inkml[8 + numero_pseudo_mot_courant] + "</trace>" + "\r\n";  

                        indice_point_courant_dans_pseudo_mot_courant = 0;
                    }

                    drapeau_debut_passage_levee = 1;

                }

            }

            corp_inkml[8 + numero_pseudo_mot_courant + 1] = "</ink>";


            // gestion et enregistrement du fichier
            string chemin_fichier = "/mnt/sdcard/KALIGO/test_inkml.inkml";
            List<string> linesToWrite = new List<string>();
            StringBuilder line = new StringBuilder();
            if (!File.Exists(chemin_fichier))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(chemin_fichier))
                {
                   
                }

                using (StreamWriter sw = File.AppendText(chemin_fichier))
                {
                    // sw.WriteLine(txt);
                    for (int rowIndex = 0; rowIndex < corp_inkml.GetLength(0); rowIndex++)
                        line.Append(corp_inkml[rowIndex]).Append("");
                    linesToWrite.Add(line.ToString());
                    //Console.WriteLine(linesToWrite);
                    //System.IO.File.WriteAllLines(path, linesToWrite.ToArray());
                    sw.WriteLine(line);

                }


            }


            //return corp_inkml;
        }
    }
}
