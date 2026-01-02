using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{
    public class VS_Re_Echantillonnage_suite_de_points_2D_en_suite_de_taille_fix
    {
        public double[,] Vect_points_de_re_echantionnage, points_segment_filtre, XYpoints_segment_filtre, Vect_points_de_re_echantionnage_filtre;
        public int rayon_filtre_traject, size_points_segment_original,size_Vect_points_de_re_echantionnage, i_hb_2;
        public double val, val2, sigma_p_filtre_traject , Longueur_elementaire_courante, Longueur_curviligne_segment_trace, facteur_vitesse_ou_rapport_distance_parcourue, facteur_vitesse_ou_rapport_dist_parcourue_dist_moy_reechant, x_dernier_point_re_echantillonne, y_dernier_point_re_echantillonne, inverse_facteur_vitesse_ou_rapport_distance_parcourue;
        public double x_point_deb_intervalle_courant, y_point_deb_intervalle_courant, x_point_fin_intervalle_courant, y_point_fin_intervalle_courant, x_nouveau_point_re_echantillonne, y_nouveau_point_re_echantillonne, Rapport_de_re_echantillonnage, Reste, somme_rap_pas_intervalle, somme_preced_rap_pas_intervalle, nombre_de_points;
        public double[] vect_position_points, Vect_facteur_vitesse_ou_rapport_distance_parcourue;
        VS_filtre_lineaire_1 filtre;

        public double[,] method_Vect_points_de_re_echantionnage(double[,] points_segment, int nombre_fixe_de_points)
        {
            filtre = new VS_filtre_lineaire_1();
            //Vect_points_de_re_echantionnage_filtre = new VS_filtre_lineaire_1();
            points_segment_filtre = new double[points_segment.GetLength(0), points_segment.GetLength(1)];/////////////
            XYpoints_segment_filtre = new double[points_segment.GetLength(0), points_segment.GetLength(1)];/////
            vect_position_points = new double[points_segment.Length - 1]; /////////////
            Vect_facteur_vitesse_ou_rapport_distance_parcourue = new double[points_segment.GetLength(0) - 1];
            rayon_filtre_traject = 2;
            sigma_p_filtre_traject = 0.4;
            size_points_segment_original = points_segment.GetLength(0);
            Vect_points_de_re_echantionnage = new double[nombre_fixe_de_points, 2];                // à verifier 

           
            if (((rayon_filtre_traject * 2) + 1) > size_points_segment_original)
                rayon_filtre_traject = size_points_segment_original;

            points_segment_filtre = filtre.Method_VS_filtre_lineaire_1_points(rayon_filtre_traject, sigma_p_filtre_traject, 0, size_points_segment_original, points_segment);
            //XYpoints_segment_filtre = filtre.Method_VS_filtre_lineaire_1_XYpoint(rayon_filtre_traject, sigma_p_filtre_traject, 1, size_points_segment_original, points_segment);
            //[points_segment_filtre, XYpoints_segment_filtre] = VS_filtre_lineaire_1(rayon_filtre_traject, sigma_p_filtre_traject, 1, size_points_segment_original, points_segment);
            //points_segment_original = points_segment;
            points_segment = points_segment_filtre;
            


            nombre_de_points = points_segment.GetLength(0);
            val = (nombre_de_points - 1);
            val2 = (nombre_fixe_de_points - 1);
            Rapport_de_re_echantillonnage = val2 / val;
            Longueur_curviligne_segment_trace = 0;


            for (int i_hb = 1; i_hb < points_segment.GetLength(0); i_hb++)
            {
                
                Longueur_elementaire_courante = Math.Sqrt(Math.Pow((points_segment[i_hb, 0] - points_segment[i_hb - 1, 0]), 2) + Math.Pow((points_segment[i_hb, 1] - points_segment[i_hb - 1, 1]), 2));
                Longueur_curviligne_segment_trace += Longueur_elementaire_courante;
                vect_position_points[i_hb] = Longueur_curviligne_segment_trace;
                //vect_position_points = [vect_position_points; Longueur_curviligne_segment_trace];
                
                    //System.//Console.WriteLine(Longueur_elementaire_courante);
                
            }
            //System.//Console.WriteLine(Longueur_curviligne_segment_trace);
            //Longueur_distance_moyenne_entre_points_reechantillones = Longueur_curviligne_segment_trace / (max(1, (nombre_fixe_de_points - 1)));
            //Vect_facteur_vitesse_ou_rapport_distance_parcourue = [];
            i_hb_2 = 0;
            for (int i_hb = 1; i_hb < points_segment.GetLength(0) ; i_hb++)
            {
                Longueur_elementaire_courante = Math.Sqrt(Math.Pow((points_segment[i_hb, 0] - points_segment[i_hb - 1, 0]), 2) + Math.Pow((points_segment[i_hb, 1] - points_segment[i_hb - 1, 1]), 2));
                facteur_vitesse_ou_rapport_distance_parcourue = Longueur_elementaire_courante / Longueur_curviligne_segment_trace;
                Vect_facteur_vitesse_ou_rapport_distance_parcourue[i_hb_2] = facteur_vitesse_ou_rapport_distance_parcourue; 
                //Vect_facteur_vitesse_ou_rapport_distance_parcourue = [Vect_facteur_vitesse_ou_rapport_distance_parcourue ; facteur_vitesse_ou_rapport_distance_parcourue];

                facteur_vitesse_ou_rapport_dist_parcourue_dist_moy_reechant = Longueur_elementaire_courante;
                // Vect_facteur_vitesse_ou_rapport_dist_parcourue_dist_moy_reechant = [Vect_facteur_vitesse_ou_rapport_dist_parcourue_dist_moy_reechant ; facteur_vitesse_ou_rapport_dist_parcourue_dist_moy_reechant];
                i_hb_2++;
            }
            
            Vect_points_de_re_echantionnage[0, 0] = points_segment[0, 0]; 
            Vect_points_de_re_echantionnage[0, 1] = points_segment[0, 1];
            Reste = 0;
            x_dernier_point_re_echantillonne = points_segment[0, 0];
            y_dernier_point_re_echantillonne = points_segment[0, 1];
            int i_hb_3 = 1;
            for (int i_hb = 1; i_hb < points_segment.GetLength(0); i_hb++)
            {
                facteur_vitesse_ou_rapport_distance_parcourue = Vect_facteur_vitesse_ou_rapport_distance_parcourue[i_hb - 1];
                if (Rapport_de_re_echantillonnage >= 1)
                {
                    inverse_facteur_vitesse_ou_rapport_distance_parcourue = 1 / facteur_vitesse_ou_rapport_distance_parcourue;
                    Longueur_elementaire_courante = Math.Sqrt(Math.Pow((points_segment[i_hb, 0] - points_segment[i_hb - 1, 0]), 2) + Math.Pow((points_segment[i_hb, 1] - points_segment[i_hb - 1, 1]), 2));

                    x_point_deb_intervalle_courant = points_segment[i_hb - 1, 0];
                    y_point_deb_intervalle_courant = points_segment[i_hb - 1, 1];

                    x_point_fin_intervalle_courant = points_segment[i_hb, 0];
                    y_point_fin_intervalle_courant = points_segment[i_hb, 1];
                    if (Reste == 0)
                    {
                        somme_rap_pas_intervalle = 1;
                        somme_preced_rap_pas_intervalle = 0;
                    }
                    else
                    {
                        somme_rap_pas_intervalle = Reste;
                        somme_preced_rap_pas_intervalle = 1 - Reste;

                    }
                    while (somme_rap_pas_intervalle <= Rapport_de_re_echantillonnage)
                    {
                        x_point_deb_intervalle_courant = points_segment[i_hb - 1, 0];
                        y_point_deb_intervalle_courant = points_segment[i_hb - 1, 1];

                        x_point_fin_intervalle_courant = points_segment[i_hb, 0];
                        y_point_fin_intervalle_courant = points_segment[i_hb, 1];
                        x_nouveau_point_re_echantillonne = ((x_point_deb_intervalle_courant * (Rapport_de_re_echantillonnage - somme_rap_pas_intervalle)) + (x_point_fin_intervalle_courant * (somme_rap_pas_intervalle))) / Rapport_de_re_echantillonnage;
                        y_nouveau_point_re_echantillonne = ((y_point_deb_intervalle_courant * (Rapport_de_re_echantillonnage - somme_rap_pas_intervalle)) + (y_point_fin_intervalle_courant * (somme_rap_pas_intervalle))) / Rapport_de_re_echantillonnage;

                        if (((x_dernier_point_re_echantillonne != x_nouveau_point_re_echantillonne) || (y_dernier_point_re_echantillonne != y_nouveau_point_re_echantillonne)) || (Longueur_elementaire_courante == 0))
                        {
                            Vect_points_de_re_echantionnage[i_hb_3, 0] = x_nouveau_point_re_echantillonne;
                            Vect_points_de_re_echantionnage[i_hb_3, 1] = y_nouveau_point_re_echantillonne; ////// à verifier 
                            i_hb_3++;
                        }
                        //Vect_points_de_re_echantionnage = [Vect_points_de_re_echantionnage ; x_nouveau_point_re_echantillonne , y_nouveau_point_re_echantillonne];

                        x_dernier_point_re_echantillonne = x_nouveau_point_re_echantillonne;
                        y_dernier_point_re_echantillonne = y_nouveau_point_re_echantillonne;


                        somme_preced_rap_pas_intervalle = somme_rap_pas_intervalle;
                        somme_rap_pas_intervalle = somme_rap_pas_intervalle + 1;

                    }
                    if (somme_rap_pas_intervalle > Rapport_de_re_echantillonnage)
                    {
                        Reste = somme_rap_pas_intervalle - Rapport_de_re_echantillonnage;
                        somme_rap_pas_intervalle = Reste;
                    }

                }
                else
                {
                    inverse_facteur_vitesse_ou_rapport_distance_parcourue = 1 / facteur_vitesse_ou_rapport_distance_parcourue;
                    Longueur_elementaire_courante = Math.Sqrt(Math.Pow((points_segment[i_hb, 0] - points_segment[i_hb - 1, 0]), 2) + Math.Pow((points_segment[i_hb, 1] - points_segment[i_hb - 1, 1]), 2));
                    if (Reste == 0)
                        somme_rap_pas_intervalle = Rapport_de_re_echantillonnage;
                    else
                        somme_rap_pas_intervalle = somme_rap_pas_intervalle + Rapport_de_re_echantillonnage;
                    if (somme_rap_pas_intervalle < 1)

                        Reste = 1 - somme_rap_pas_intervalle;
                    else if (somme_rap_pas_intervalle > 1)
                    {
                        Reste = Rapport_de_re_echantillonnage - (somme_rap_pas_intervalle - 1);
                        x_point_deb_intervalle_courant = points_segment[i_hb - 1, 0];
                        y_point_deb_intervalle_courant = points_segment[i_hb - 1, 1];
                        x_point_fin_intervalle_courant = points_segment[i_hb, 0];
                        y_point_fin_intervalle_courant = points_segment[i_hb, 1];

                        x_nouveau_point_re_echantillonne = ((x_point_deb_intervalle_courant * (somme_rap_pas_intervalle - 1)) + (x_point_fin_intervalle_courant * Reste)) / Rapport_de_re_echantillonnage;
                        y_nouveau_point_re_echantillonne = ((y_point_deb_intervalle_courant * (somme_rap_pas_intervalle - 1)) + (y_point_fin_intervalle_courant * Reste)) / Rapport_de_re_echantillonnage;
                        Vect_points_de_re_echantionnage[i_hb_3, 0] = x_nouveau_point_re_echantillonne;
                        Vect_points_de_re_echantionnage[i_hb_3, 1] = y_nouveau_point_re_echantillonne;
                        // Vect_points_de_re_echantionnage = [Vect_points_de_re_echantionnage ; x_nouveau_point_re_echantillonne , y_nouveau_point_re_echantillonne];
                        i_hb_3++;

                        x_dernier_point_re_echantillonne = x_nouveau_point_re_echantillonne;
                        y_dernier_point_re_echantillonne = y_nouveau_point_re_echantillonne;

                        somme_rap_pas_intervalle = somme_rap_pas_intervalle - 1;

                    }



                }
                //System.//Console.WriteLine(Longueur_elementaire_courante);
                
            }
            
            if (i_hb_3< nombre_fixe_de_points)
            {
                Vect_points_de_re_echantionnage[Vect_points_de_re_echantionnage.GetLength(0) - 1, 0] = points_segment[points_segment.GetLength(0) - 1, 0];
                Vect_points_de_re_echantionnage[Vect_points_de_re_echantionnage.GetLength(0) - 1, 1] = points_segment[points_segment.GetLength(0) - 1, 1];
            }
            // Vect_points_de_re_echantionnage = [Vect_points_de_re_echantionnage; points_segment(end,:)];
            rayon_filtre_traject = 2;
            sigma_p_filtre_traject = 0.4;
            size_Vect_points_de_re_echantionnage = Vect_points_de_re_echantionnage.GetLength(0);
            //if (((rayon_filtre_traject * 2) + 1) > size_Vect_points_de_re_echantionnage)
            //rayon_filtre_traject = size_Vect_points_de_re_echantionnage;

            //Vect_points_de_re_echantionnage_filtre = filtre.Method_VS_filtre_lineaire_1_points(rayon_filtre_traject, sigma_p_filtre_traject, 0, size_Vect_points_de_re_echantionnage, Vect_points_de_re_echantionnage);
            //Vect_points_de_re_echantionnage = Vect_points_de_re_echantionnage_filtre;
          /*for (int h = 0; h < Vect_points_de_re_echantionnage.Length; h++)
            {
                 System.//Console.WriteLine(Vect_points_de_re_echantionnage[h,0]);
            }
           */  
            return Vect_points_de_re_echantionnage;

        }
    }
}
