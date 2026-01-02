using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{
    public class VS_Inversion_et_reparation_de_la_resolution_trajectoire
    {
        public double[,] points_repare, points_repare_2;
        public double[,] matrice_param;
        public double[] x;
        public double[] y;
        double max_x, max_y;
        int choix;

        public double[,] points_total;
        public double[,] points_total_filtre;
        public double[,] DAT_par_pseudo_mot_total;
        public double[,] nbr_points_par_pseudo_mot;
        public double[,] Ensemble_max_min_x_y;
        public double[,] segment_pseudo_mot, segment_pseudo_mot_2;
        public int c, l, taille_point_repare, taille_segment_pseudo_mot, size_point_repare,ind;

        public VS_Re_Echantillonnage_suite_de_points_2D_en_suite_de_taille_fix Re_Echantillonnage;
        public static VS_normalisation_H_L_hauteur_largeur normalisation;


        public int Method_size_matrix(double[,] data, int taille_minimale_pseudo_mot)
        {
            Re_Echantillonnage = new VS_Re_Echantillonnage_suite_de_points_2D_en_suite_de_taille_fix();
            normalisation = new VS_normalisation_H_L_hauteur_largeur();
            int drapeau_parcour_pseudo_mot_2 = 0;
            c = 0;
            l = 0;
            
            taille_point_repare = 0;
            x = new double[data.GetLength(0)];
            y = new double[data.GetLength(0)];
            int n = data.GetLength(0);
            for (int i = 0; i < data.GetLength(0); i++)
                x[i] = data[i, 0];
            for (int i = 0; i < data.GetLength(0); i++)
                y[i] = data[i, 1];


            if ((data.GetLength(0) > 0) && (data.GetLength(0) >= 2))
            {
                max_x = max_matrice(x);
                max_y = max_matrice(y);
            }
            else
            {
                max_x = 1;
                max_y = 1;
            }
            int size_segment_pseudo_mot;
            //double[,] segment_pseudo_mot = new double[data.GetLength(0), data.GetLength(1)];
            //double[,] points_repare = new double[data.GetLength(0), data.GetLength(1)];

            for (int k = 0; k < data.GetLength(0); k++)
            {

                if (data[k, 0] != 0 || data[k, 1] != 0)
                {
                    c += 1;
                    //segment_pseudo_mot = [segment_pseudo_mot; data(k, 1), max_y_data + 2 - data(k, 2)];
                    segment_pseudo_mot = new double[c, data.GetLength(1)];
                    //segment_pseudo_mot_2 = new double[c, data.GetLength(1)];
                    for (int j = 0; j < c; j++)
                    {
                        //segment_pseudo_mot = segment_pseudo_mot_2;
                        segment_pseudo_mot[j, 0] = data[l + j, 0]; //data[j, 0];
                        segment_pseudo_mot[j, 1] = max_y + 2 - data[l + j, 1];
                        //l += 1;
                    }
                    //l += 1;
                    //segment_pseudo_mot_2 = segment_pseudo_mot;
                    drapeau_parcour_pseudo_mot_2 = 1;

                }
                else
                {
                    l = k + 1;
                    c = 0;
                  
                    if (drapeau_parcour_pseudo_mot_2 == 1)
                    {
                        segment_pseudo_mot_2 = new double[segment_pseudo_mot.GetLength(0), segment_pseudo_mot.GetLength(1)];
                        segment_pseudo_mot_2 = segment_pseudo_mot;

                        taille_segment_pseudo_mot = 0;
                        size_segment_pseudo_mot = segment_pseudo_mot_2.GetLength(0);   // ??
                        ////Console.WriteLine(size_segment_pseudo_mot);
                        if (size_segment_pseudo_mot < taille_minimale_pseudo_mot )
                        {
                            segment_pseudo_mot_2 = new double[segment_pseudo_mot.GetLength(0) + taille_minimale_pseudo_mot + 2, segment_pseudo_mot.GetLength(1)];
                            segment_pseudo_mot_2 = Re_Echantillonnage.method_Vect_points_de_re_echantionnage(segment_pseudo_mot, (taille_minimale_pseudo_mot + size_segment_pseudo_mot + 2));
                            //segment_pseudo_mot = VS_Re_Echantillonnage_suite_de_points_2D_en_suite_de_taille_fix(segment_pseudo_mot, (taille_minimale_pseudo_mot + size_segment_pseudo_mot + 2));
                            taille_segment_pseudo_mot = segment_pseudo_mot_2.GetLength(0) - size_segment_pseudo_mot;
                        }

                        taille_point_repare += size_segment_pseudo_mot + taille_segment_pseudo_mot;
                        //points_repare = [points_repare; segment_pseudo_mot]


                    }
                    taille_point_repare += 1;
                    segment_pseudo_mot = new double[data.GetLength(0), data.GetLength(1)]; //  segment_pseudo_mot = [];
                    drapeau_parcour_pseudo_mot_2 = 0;
                }

            }



            if (drapeau_parcour_pseudo_mot_2 == 1)
            {
                taille_segment_pseudo_mot = 0;
                size_segment_pseudo_mot = segment_pseudo_mot.GetLength(0);
                if (size_segment_pseudo_mot < taille_minimale_pseudo_mot)
                {
                    segment_pseudo_mot_2 = new double[segment_pseudo_mot.GetLength(0) + taille_minimale_pseudo_mot + 2, segment_pseudo_mot.GetLength(1)];
                    segment_pseudo_mot_2 = Re_Echantillonnage.method_Vect_points_de_re_echantionnage(segment_pseudo_mot, (taille_minimale_pseudo_mot + size_segment_pseudo_mot + 2));
                    //segment_pseudo_mot = VS_Re_Echantillonnage_suite_de_points_2D_en_suite_de_taille_fix(segment_pseudo_mot, (taille_minimale_pseudo_mot + size_segment_pseudo_mot + 2));
                    taille_segment_pseudo_mot = segment_pseudo_mot_2.GetLength(0) - size_segment_pseudo_mot;
                }

                taille_point_repare += size_segment_pseudo_mot + taille_segment_pseudo_mot;
            }

            
            return taille_point_repare;



        }




        public double[,] inversion(double[,] data, int taille_minimale_pseudo_mot, string label_du_script_choisi)
        {


            Re_Echantillonnage = new VS_Re_Echantillonnage_suite_de_points_2D_en_suite_de_taille_fix();
            normalisation = new VS_normalisation_H_L_hauteur_largeur();
            size_point_repare = Method_size_matrix(data, taille_minimale_pseudo_mot);

            points_repare = new double[Method_size_matrix(data, taille_minimale_pseudo_mot), 2];
            //points_repare_2 =
            int drapeau_parcour_pseudo_mot = 0;
            //System.Console.ReadKey(true);
            c = 0;
            l = 0;
            int n = data.GetLength(0);
            for (int i = 0; i < data.GetLength(0); i++)
                x[i] = data[i, 0];
            for (int i = 0; i < data.GetLength(0); i++)
                y[i] = data[i, 1];
            //double max_x = max_matrice(x);
            //double max_y = max_matrice(y);
            //System.//Console.WriteLine(size_point_repare);
            int size_segment_pseudo_mot;
            if ((data.GetLength(0) > 0) && (data.GetLength(0) >= 2))
            {
                max_x = max_matrice(x);
                max_y = max_matrice(y);
            }
            else
            {
                max_x = 1;
                max_y = 1;
            }
            points_repare_2 = new double[size_point_repare, data.GetLength(0)];
            //double[,] segment_pseudo_mot = new double[data.GetLength(0), data.GetLength(1)];
            //double[,] points_repare = new double[data.GetLength(0), data.GetLength(1)];
            ind = 0;
            for (int k = 0; k < data.GetLength(0); k++)
            {

                if (data[k, 0] != 0 || data[k, 1] != 0)
                {
                    c += 1;
                    //segment_pseudo_mot = [segment_pseudo_mot; data(k, 1), max_y_data + 2 - data(k, 2)];
                    segment_pseudo_mot = new double[c, data.GetLength(1)];
                    //segment_pseudo_mot_2 = new double[c, data.GetLength(1)];
                    for (int j = 0; j < c; j++)
                    {
                        //segment_pseudo_mot = segment_pseudo_mot_2;
                        segment_pseudo_mot[j, 0] = data[l + j, 0]; //data[j, 0];
                        segment_pseudo_mot[j, 1] = max_y + 2 - data[l + j, 1];
                        //l += 1;
                    }
                    //l += 1;
                    //segment_pseudo_mot_2 = segment_pseudo_mot;
                    drapeau_parcour_pseudo_mot = 1;

                }
                else
                {
                    l = k + 1;
                    c = 0;


                    if (drapeau_parcour_pseudo_mot == 1)
                    {
                        segment_pseudo_mot_2 = new double[segment_pseudo_mot.GetLength(0), segment_pseudo_mot.GetLength(1)];
                        segment_pseudo_mot_2 = segment_pseudo_mot;

                        size_segment_pseudo_mot = segment_pseudo_mot_2.GetLength(0);   // ??
                        //System.//Console.WriteLine(size_segment_pseudo_mot);
                        if (size_segment_pseudo_mot < taille_minimale_pseudo_mot)
                        {
                            segment_pseudo_mot_2 = new double[segment_pseudo_mot_2.GetLength(0) + taille_minimale_pseudo_mot + 2, segment_pseudo_mot_2.GetLength(1)];
                            segment_pseudo_mot_2 = Re_Echantillonnage.method_Vect_points_de_re_echantionnage(segment_pseudo_mot, (taille_minimale_pseudo_mot + size_segment_pseudo_mot + 2));
                            //segment_pseudo_mot = VS_Re_Echantillonnage_suite_de_points_2D_en_suite_de_taille_fix(segment_pseudo_mot, (taille_minimale_pseudo_mot + size_segment_pseudo_mot + 2));
                            /*for (int h = 0; h < segment_pseudo_mot_2.GetLength(0); h++)
                            {
                                System.//Console.WriteLine(segment_pseudo_mot_2[h, 0]);
                            }*/
                        }

                        //points_repare = [points_repare; segment_pseudo_mot]

                        //points_repare_2 = new double[segment_pseudo_mot_2.GetLength(0), segment_pseudo_mot_2.GetLength(1)];
                        for (int nb = 0; nb < segment_pseudo_mot_2.GetLength(0); nb++)
                        {
                            points_repare[ind, 0] = segment_pseudo_mot_2[nb, 0];
                            points_repare[ind, 1] = segment_pseudo_mot_2[nb, 1];
                            ind++;
                        }
                        //points_repare[ind, 0] = segment_pseudo_mot[k, 0]; //points_repare = [points_segment_pseudo_mot];
                        //points_repare[ind, 1] = segment_pseudo_mot[k, 1]; 


                    }

                    points_repare[ind, 0] = data[k, 0];// segment_pseudo_mot_2[k, 0];
                    points_repare[ind, 1] = data[k, 1]; //segment_pseudo_mot_2[k, 1];
                    ind++;
                    //points_repare[k, 0] = data[k, 0]; // points_repare = [points_repare; data(k, 1), data(k, 2)];
                    //points_repare[k, 1] = data[k, 1];
                    segment_pseudo_mot = new double[data.GetLength(0), data.GetLength(1)]; //  segment_pseudo_mot = [];
                    drapeau_parcour_pseudo_mot = 0;
                }

            }



            if (drapeau_parcour_pseudo_mot == 1)
            {
                segment_pseudo_mot_2 = new double[segment_pseudo_mot.GetLength(0), segment_pseudo_mot.GetLength(1)];
                segment_pseudo_mot_2 = segment_pseudo_mot;
                size_segment_pseudo_mot = segment_pseudo_mot.GetLength(0);
                if (size_segment_pseudo_mot < taille_minimale_pseudo_mot)
                {
                    segment_pseudo_mot_2 = new double[segment_pseudo_mot.GetLength(0) + taille_minimale_pseudo_mot + 2, segment_pseudo_mot.GetLength(1)];

                    segment_pseudo_mot_2 = Re_Echantillonnage.method_Vect_points_de_re_echantionnage(segment_pseudo_mot, (taille_minimale_pseudo_mot + size_segment_pseudo_mot + 2));
                    // segment_pseudo_mot = VS_Re_Echantillonnage_suite_de_points_2D_en_suite_de_taille_fix(segment_pseudo_mot, (taille_minimale_pseudo_mot + size_segment_pseudo_mot + 2));
                }
                for (int j = 0; j < segment_pseudo_mot_2.GetLength(0); j++)
                {
                    points_repare[ind, 0] = segment_pseudo_mot_2[j, 0];
                    points_repare[ind, 1] = segment_pseudo_mot_2[j, 1];
                    ind += 1;
                }

            }
            if (label_du_script_choisi.Equals("Symbole1"))
            {
               /* Console.WriteLine("Taille Points réparé" + points_repare.GetLength(0));
                for (int i = 0; i < points_repare.GetLength(0); i++)
                    for (int j = 0; j < points_repare.GetLength(1); j++)
                        Console.WriteLine("Points réparé" + points_repare[i,j]);*/
                
               return points_repare;
            }
            else
            {
                choix = 1;
                points_repare_2 = normalisation.method_normalisation_hauteur_Largeur(points_repare, points_repare.GetLength(0),choix);
                return points_repare_2;
            }



        }


        public double min_matrice(double[] tab)
        {
            double minimum = tab[0];
            for (int i = 1; i < tab.Length; i++)
            {
                if (tab[i] < minimum)
                    minimum = tab[i];
            }
            return minimum;
        }
        public double max_matrice(double[] tab)
        {
            double maximum = tab[0];
            for (int i = 1; i < tab.Length; i++)
            {
                if (tab[i] > maximum)
                    maximum = tab[i];
            }
            return maximum;
        }


    }
}

