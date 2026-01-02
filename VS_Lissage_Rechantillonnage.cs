//using Beta_elliptic_model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{
   public class VS_Lissage_Rechantillonnage
   {

        double[,] points_equidistants_lisse;
        double[,] segment_points, segment_lisse, segment_points_2, segment_lisse_2;
        int drapeau_levee, drapeau_baissee, indice_debut_segment_courant, indice_fin_segment_courant, taille_segment_reechantionne;
        double x_points_trace_numero_k, y_points_trace_numero_k;
       public  double[,] Method_VS_Lissage_reechantionnage(double[,] points_equidistants, int rayon, double sigma_p, int nombre_fixe_de_points)
       {
            /* Calibrage_automatique_des_parametres_du_lissage calibrage_automatique = new Calibrage_automatique_des_parametres_du_lissage();
             double[] calibrage = calibrage_automatique.Method_Calibrage_Automatique(points_equidistants);
             rayon = (int) calibrage[0];
             sigma_p = calibrage[1];
             */

            VS_Re_Echantillonnage_suite_de_points_2D_en_suite_de_taille_fix Reechantillonage = new VS_Re_Echantillonnage_suite_de_points_2D_en_suite_de_taille_fix();


            int nombre_minimal_de_points_par_segment = 2;
            //////////////////
            drapeau_levee = 1;
            drapeau_baissee = 0;
            indice_debut_segment_courant = 0;
            indice_fin_segment_courant = 0;
            int size_points_reechantionne = 0;
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
                        taille_segment_reechantionne = (taille_segment * nombre_fixe_de_points )/ Math.Max(points_equidistants.GetLength(0), 1);
                        taille_segment_reechantionne = Math.Max(taille_segment_reechantionne, nombre_minimal_de_points_par_segment);
                        size_points_reechantionne += taille_segment_reechantionne;
                        segment_points = new double[taille_segment, 2];

                    }

                    drapeau_levee = 1;
                    drapeau_baissee = 0;
                    size_points_reechantionne += 1;
                }

            }

            /////////////////
            double[,] points_equidistants_reechantionne = new double[size_points_reechantionne, 2];

            drapeau_levee = 1;
            drapeau_baissee = 0;
            indice_debut_segment_courant = 0;
            indice_fin_segment_courant = 0;
            int indice_debut_remplissage = 0;
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
                        taille_segment_reechantionne = (taille_segment * nombre_fixe_de_points) / Math.Max(points_equidistants.GetLength(0), 1);
                        taille_segment_reechantionne = Math.Max(taille_segment_reechantionne, nombre_minimal_de_points_par_segment);
                        segment_points = new double[taille_segment, 2];

                        double[,] points_segment_reechantionne = new double[size_points_reechantionne, 2];

                        for (int ijk = indice_debut_segment_courant; ijk <= indice_fin_segment_courant; ijk++)
                        {
                            segment_points[(ijk - indice_debut_segment_courant), 0] = points_equidistants[ijk, 0];
                            segment_points[(ijk - indice_debut_segment_courant), 1] = points_equidistants[ijk, 1];
                        }


                        if (segment_points.GetLength(0) >= 2)
                        {
                            //for (int i = 0; i < segment_points.GetLength(0); i++)
                            ////Console.WriteLine(segment_points[i, 0]);

                            points_segment_reechantionne = Reechantillonage.method_Vect_points_de_re_echantionnage(segment_points, taille_segment_reechantionne);

                            for (int j = 0; j < points_segment_reechantionne.GetLength(0); j++)
                            {
                                for (int i = 0; i < points_segment_reechantionne.GetLength(1); i++)
                                    points_equidistants_reechantionne[j + indice_debut_remplissage, i] = points_segment_reechantionne[j, i];

                            }
                            indice_debut_remplissage += points_segment_reechantionne.GetLength(0);


                        }
                        else
                        {
                            points_segment_reechantionne = segment_points;

                            for (int j = 0; j < points_segment_reechantionne.GetLength(0); j++)
                            {
                                for (int i = 0; i < points_segment_reechantionne.GetLength(1); i++)
                                    points_equidistants_reechantionne[j + indice_debut_remplissage, i] = points_segment_reechantionne[j, i];

                            }
                            indice_debut_remplissage += points_segment_reechantionne.GetLength(0);
                        }


                    }
                    drapeau_levee = 1;
                    drapeau_baissee = 0;
                    points_equidistants_reechantionne[indice_debut_remplissage , 0] = 0;
                    points_equidistants_reechantionne[indice_debut_remplissage, 1] = 0;
                    indice_debut_remplissage ++;
                    //segment_lisse = null;
                    //segment_points = null;
                }

            }
            ////////////////////
            points_equidistants = points_equidistants_reechantionne;
            ///////////////////
            drapeau_levee = 1;
            drapeau_baissee = 0;
            indice_debut_segment_courant = 0;
            indice_fin_segment_courant = 0;

            VS_filtre_lineaire_1 filtre = new VS_filtre_lineaire_1();

            points_equidistants_lisse  = new double[points_equidistants.GetLength(0),2];
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
                            segment_points[(ijk - indice_debut_segment_courant  ), 0] = points_equidistants[ijk, 0];
                            segment_points[(ijk - indice_debut_segment_courant  ), 1] = points_equidistants[ijk, 1];
                        }


                        if (segment_points.GetLength(0) >= 2)
                        {
                            //for (int i = 0; i < segment_points.GetLength(0); i++)
                                ////Console.WriteLine(segment_points[i, 0]);

                            double[,] segment_point_p = new double[segment_points.GetLength(0),2];
                            for (int j = 0; j < segment_point_p.GetLength(0); j++)
                            {
                                for (int i = 0; i < segment_point_p.GetLength(1); i++)
                                    segment_point_p[j, i] = segment_points[j, i];

                            }

                            segment_lisse = filtre.Method_VS_filtre_lineaire_1_points(rayon, sigma_p, 0, segment_point_p.GetLength(0), segment_point_p);

                            // Console.ReadKey();
                            //XYpoint = filtre.Method_VS_filtre_lineaire_1_XYpoint(rayon, sigma_p, 1, segment_points.GetLength(0), segment_points);

                            segment_points_2 = new double[segment_lisse.GetLength(0), 2];
                              for (int j = 0; j < segment_lisse.GetLength(0); j++)
                              {
                                  for (int i = 0; i < segment_lisse.GetLength(1); i++)
                                      segment_points_2[j, i] = segment_lisse[j, i];

                              }

                            //for (int i = 0; i < segment_lisse.GetLength(0); i++)
                               ////Console.WriteLine(segment_lisse[i, 0]);

                            //segment_points = segment_lisse;

                            segment_lisse_2 = filtre.Method_VS_filtre_lineaire_1_points(rayon, sigma_p, 0, segment_points_2.GetLength(0), segment_points_2);

                            // XYpoint = filtre.Method_VS_filtre_lineaire_1_XYpoint(rayon, sigma_p, 1, segment_points_lisse.GetLength(0), segment_points_lisse);
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
                       */

                        for (int ijk = indice_debut_segment_courant; ijk <= indice_fin_segment_courant; ijk++)
                        {
                            points_equidistants_lisse[ijk, 0] = segment_lisse_2[(ijk - indice_debut_segment_courant ), 0];
                            points_equidistants_lisse[ijk, 1] = segment_lisse_2[(ijk - indice_debut_segment_courant ), 1];
                        }


                    }
                    drapeau_levee = 1;
                    drapeau_baissee = 0;

                    //segment_lisse = null;
                    //segment_points = null;
                }

            }



            return points_equidistants_lisse;
       }
    }
}
