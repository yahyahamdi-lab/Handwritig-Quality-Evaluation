
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Beta_elliptic_model
{
    public class Traitement_Evaluation
    {
        public int poids_score_forme = 4;
        public double[] score_DF_norm;
        public int poids_score_direction = 1;
        public double poids_score_ordre = 0.5;
        public double distance_AS_direction, distance_AS_ordre, distance_AS_forme, distance_AP_direction, distance_AP_ordre, distance_AP_forme;
        public string label_du_script_choisi_p, label_du_script_choisi_s, type_de_script, type_de_script_p, type_de_script_q;
        public static string appreciation_1, appreciation_2;
        public static double Pourcetange_appreciation_1, Pourcetange_appreciation_2;
        public static string Pourcetange_appreciation_11, Pourcetange_appreciation_22;
        public double[] tab_inverse, tab_reordonne, tab;
        public double[,] point_sequence_inverse, points_segments_reordonnes;
        public double[] score_direction_ordre_forme, mmatrice_echantillon_test, mmatrice_echantillon_modele_1, mmatrice_echantillon_modele_2, mmatrice_echantillon_modele_3;
        public int[] mat_cod_test, mat_cod_1, mat_cod_2, mat_cod_3;
        public double a, b, c, d, e, f, u, g,h,i;
        public string a1, b1, c1, d1, e1, f1, u1;
        public double a_final, b_final, c_final, d_final, e_final, f_final;
        public double poids_AP = 0.5, poids_AS = 0.5, somme_poids, somme_poids_score;
        public double score_direction_global, score_ordre_global, score_forme_global, score_global;
        public double[,] point_Trajectoire_avec_quantification_spatiale_rafinnee,  points_delta_fixe2; 
        public  string classe_choisie ;
        public string label_du_script_choisi;
        public double[,] mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_DF_echantillon_test, mmatrice_param_echantillon_test, mmatrice_param_echantillon_modele_3_AP, mmatrice_param_echantillon_modele_1_AS, mmatrice_param_echantillon_modele_2_AS, mmatrice_param_echantillon_modele_3_AS, mmatrice_param_DF_echantillon_modele_1_AP, mmatrice_param_DF_echantillon_modele_2_AP, mmatrice_param_DF_echantillon_modele_3_AP;
        private double[] score_AS_norm;
        private double[] score_AP_norm;
        WRDFile WRDfile = new WRDFile();
        Module_estimation_scores_de_comparaison_approche_structurelle_f estimation_CS = new Module_estimation_scores_de_comparaison_approche_structurelle_f();

        public double[] Method_Traitement_Evaluation_1(double[,] xy_echantillon_test, string label_du_script_choisi, string type_de_script, string path,int autorisation_distance_test_faux)
        {
            

            if (type_de_script == "Mot___")
            {
                type_de_script_p = "Mot";
                type_de_script_q = "mots";
            }
            else if (type_de_script == "Lettre")
            {
                type_de_script_p = "Lettre";
                type_de_script_q = "lettres";
            }


                label_du_script_choisi_p = label_du_script_choisi+ "_p";
                label_du_script_choisi_s = label_du_script_choisi+ "_s";

                class_codage_p co = new class_codage_p();
                VS_Lecture_traitement_save_param_phrase_online_beta_elliptique lecture = new VS_Lecture_traitement_save_param_phrase_online_beta_elliptique();
                
                Evaluation Eva_bem = new Evaluation();
                

                mmatrice_param_echantillon_test = lecture.method_mmatrice_param_1(xy_echantillon_test, label_du_script_choisi); 
                double[] bem = Eva_bem.Method_Evaluation(mmatrice_param_echantillon_test, label_du_script_choisi, type_de_script, path, autorisation_distance_test_faux);
                double[] bem_p = Eva_bem.Method_Evaluation(mmatrice_param_echantillon_test, label_du_script_choisi_p, type_de_script, path, autorisation_distance_test_faux);
                double[] bem_s = Eva_bem.Method_Evaluation(mmatrice_param_echantillon_test, label_du_script_choisi_s, type_de_script, path, autorisation_distance_test_faux);



                a = max_matrice(bem[0], bem_p[0], bem_s[0], 0);
                b = max_matrice(bem[1], bem_p[1], bem_s[1], 0);
                c = max_matrice(bem[2], bem_p[2], bem_s[2], 0);
                d = max_matrice(bem[3], bem_p[3], bem_s[3], 0);
                e = max_matrice(bem[4], bem_p[4], bem_s[4], 0);
                f = max_matrice(bem[5], bem_p[5], bem_s[5], 0);

            if (a == bem[0])
            {
                b = bem[1];
                c = bem[2];
            }
            else if (a == bem_p[0])
            {
                b = bem_p[1];
                c = bem_p[2];
            }
            else if (a == bem_s[0])
            {
                b = bem_s[1];
                c = bem_s[2];
            }

            // structurelle 
            if (d == bem[3])
            {
                e = bem[4];
                f = bem[5];
            }
            else if (d == bem_p[3])
            {
                e = bem_p[4];
                f = bem_p[5];
            }
            else if (d == bem_s[3])
            {
                e = bem_s[4];
                f = bem_s[5];
            }


                tab = new double[6];
                tab[0] = a;
                tab[1] = b;
                tab[2] = c;
                tab[3] = d;
                tab[4] = e;
                tab[5] = f;
            /*tab[6] = g;
            tab[7] = h;
            tab[8] = i;*/
            Console.WriteLine("BEM"+ a + " " + d);

            return tab;

        }

        public double max_matrice( double w, double x, double y, double z)
        {
            double[] tab = new double[4];
            tab[0] = w; tab[1] = x; tab[2] = y; tab[3] = z;
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
