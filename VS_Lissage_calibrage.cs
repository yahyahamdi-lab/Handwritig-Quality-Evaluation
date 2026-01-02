//using Beta_elliptic_model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{
    public class VS_Lissage_calibrage
    {

        double[,] points_equidistants_lisse, points_equidistants_lisse_i;
        double[,] segment_points, segment_lisse, segment_points_2, segment_lisse_2;
        int drapeau_levee, drapeau_baissee, indice_debut_segment_courant, indice_fin_segment_courant;
        double x_points_trace_numero_k, y_points_trace_numero_k, max_nbr_stroke, min_nbr_stroke, rayon_2, sigma_p_2;
        VS_Lecture_traitement_save_param_phrase_online_beta_elliptique lecture = new VS_Lecture_traitement_save_param_phrase_online_beta_elliptique();

      /*  public double[,] Method_VS_Lissage(double[,] points_equidistants, double[] max_min_nbr_stroke_ech_modele, int rayon, double sigma_p)
        {
            Calibrage_automatique_des_parametres_du_lissage calibrage_automatique = new Calibrage_automatique_des_parametres_du_lissage();
            double[] calibrage = calibrage_automatique.Method_Calibrage_Automatique(points_equidistants);

            double[,] points_equidistants_lisse_1 = method_lissage_points_equidistant(points_equidistants, calibrage[0], calibrage[1]);

            double pas_glissement_iterative_sigma_p = 1;
            int pas_glissement_iterative_rayon = 1;
            int nbr_iteration = 1;
            rayon_2 = calibrage[0];
            sigma_p_2 = calibrage[1];

            double[,] mmatrice_param_echantillon_test_lisse = lecture.method_mmatrice_param_1(points_equidistants_lisse_1);
            max_nbr_stroke = max_min_nbr_stroke_ech_modele[0];
            min_nbr_stroke = max_min_nbr_stroke_ech_modele[1];
            int nbr_stroke_test_lisse = mmatrice_param_echantillon_test_lisse.GetLength(1);
            int nbr_stroke_test_lisse_i = nbr_stroke_test_lisse;
            points_equidistants_lisse_i = points_equidistants_lisse;
            if (nbr_stroke_test_lisse > max_nbr_stroke)
            {
                while ((nbr_stroke_test_lisse_i > max_nbr_stroke) && (nbr_iteration < 20))
                {
                    rayon_2 += pas_glissement_iterative_rayon;
                    sigma_p_2 += pas_glissement_iterative_sigma_p;
                    points_equidistants_lisse_i = method_lissage_points_equidistant(points_equidistants_lisse_1, rayon_2, sigma_p_2);
                    double[,] mmatrice_param_echantillon_test_lisse_i = lecture.method_mmatrice_param_1(points_equidistants_lisse_i);
                    nbr_stroke_test_lisse_i = mmatrice_param_echantillon_test_lisse_i.GetLength(1);
                    nbr_iteration++;
                }
            }
            else if (nbr_stroke_test_lisse < min_nbr_stroke)
            {

                while ((nbr_stroke_test_lisse_i < min_nbr_stroke) && (nbr_iteration < 20) && ((rayon_2 - pas_glissement_iterative_rayon) >= 1) && ((sigma_p - pas_glissement_iterative_sigma_p) >= 0.5))
                {
                    rayon_2 -= pas_glissement_iterative_rayon;
                    sigma_p_2 -= pas_glissement_iterative_sigma_p;
                    points_equidistants_lisse_i = method_lissage_points_equidistant(points_equidistants_lisse_1, rayon_2, sigma_p_2);
                    double[,] mmatrice_param_echantillon_test_lisse_i = lecture.method_mmatrice_param_1(points_equidistants_lisse_i);
                    nbr_stroke_test_lisse_i = mmatrice_param_echantillon_test_lisse_i.GetLength(1);
                }
            }

            points_equidistants_lisse = points_equidistants_lisse_i;
            return points_equidistants_lisse;
        }*/
        /*
        public double[,] method_lissage_points_equidistant(double[,] points_equidistants, double rayon, double sigma_p)
        {

            drapeau_levee = 1;
            drapeau_baissee = 0;
            indice_debut_segment_courant = 0;
            indice_fin_segment_courant = 0;


            VS_filtre_lineaire_1 filtre = new VS_filtre_lineaire_1();

            points_equidistants_lisse = new double[points_equidistants.GetLength(0), 2];
            for (int k = 0; k < points_equidistants.GetLength(0); k++)
            {
                x_points_trace_numero_k = points_equidistants[k, 0];
                y_points_trace_numero_k = points_equidistants[k, 1];

                if ((x_points_trace_numero_k != 0) || (y_points_trace_numero_k != 0))
                {
                    if (drapeau_levee == 1)
                    {
                        indice_debut_segment_courant = k;
                    }

                    drapeau_levee = 0;
                    drapeau_baissee = 1;
                }
                else
                {
                    if (drapeau_baissee == 1)
                    {
                        indice_fin_segment_courant = k - 1;

                        int taille_segment = (indice_fin_segment_courant - indice_debut_segment_courant) + 1;

                        segment_points = new double[taille_segment, 2];

                        for (int ijk = indice_debut_segment_courant; ijk <= indice_fin_segment_courant; ijk++)
                        {
                            segment_points[(ijk - indice_debut_segment_courant), 0] = points_equidistants[ijk, 0];
                            segment_points[(ijk - indice_debut_segment_courant), 1] = points_equidistants[ijk, 1];
                        }


                        if (segment_points.GetLength(0) >= 2)
                        {
                            //for (int i = 0; i < segment_points.GetLength(0); i++)
                            ////Console.WriteLine(segment_points[i, 0]);

                            double[,] segment_point_p = new double[segment_points.GetLength(0), 2];
                            for (int j = 0; j < segment_point_p.GetLength(0); j++)
                            {
                                for (int i = 0; i < segment_point_p.GetLength(1); i++)
                                    segment_point_p[j, i] = segment_points[j, i];

                            }
                            int rayon_r = (int)Math.Round(rayon);
                            segment_lisse = filtre.Method_VS_filtre_lineaire_1_points(rayon_r, sigma_p, 0, segment_point_p.GetLength(0), segment_point_p);

                            // Console.ReadKey();
                            //XYpoint = filtre.Method_VS_filtre_lineaire_1_XYpoint(rayon_r, sigma_p, 1, segment_points.GetLength(0), segment_points);

                            segment_points_2 = new double[segment_lisse.GetLength(0), 2];
                            for (int j = 0; j < segment_lisse.GetLength(0); j++)
                            {
                                for (int i = 0; i < segment_lisse.GetLength(1); i++)
                                    segment_points_2[j, i] = segment_lisse[j, i];

                            }

                            //for (int i = 0; i < segment_lisse.GetLength(0); i++)
                            ////Console.WriteLine(segment_lisse[i, 0]);

                            //segment_points = segment_lisse;

                            segment_lisse_2 = filtre.Method_VS_filtre_lineaire_1_points(rayon_r, sigma_p, 0, segment_points_2.GetLength(0), segment_points_2);

                            // XYpoint = filtre.Method_VS_filtre_lineaire_1_XYpoint(rayon_r, sigma_p, 1, segment_points_lisse.GetLength(0), segment_points_lisse);
                            //for (int i = 0; i < segment_point_p.GetLength(0); i++)
                            // //Console.WriteLine(segment_point_p[i, 0]);
                        }

                        /*  else
                         {
                             segment_lisse = segment_points;
                             for (int j = 0; j < segment_points.GetLength(0); j++)
                              {
                                  for (int i = 0; i < segment_points.GetLength(1); i++)
                                      segment_points_lisse[j, i] = segment_points[j, i];

                         }
                        

                        for (int ijk = indice_debut_segment_courant; ijk <= indice_fin_segment_courant; ijk++)
                        {
                            points_equidistants_lisse[ijk, 0] = segment_lisse_2[(ijk - indice_debut_segment_courant), 0];
                            points_equidistants_lisse[ijk, 1] = segment_lisse_2[(ijk - indice_debut_segment_courant), 1];
                        }


                    }
                    drapeau_levee = 1;
                    drapeau_baissee = 0;

                    //segment_lisse = null;
                    //segment_points = null;
                }


            }
            return points_equidistants_lisse;
        }*/
    }
}
