using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{
   public class Traitement_Evaluation_DFcar
    {
        public int poids_score_forme = 4;
        public int poids_score_direction = 1;
        public double poids_score_ordre = 0.5;
        public double distance_AS_direction, distance_AS_ordre, distance_AS_forme, distance_AP_direction, distance_AP_ordre, distance_AP_forme;
        public string type_de_script, type_de_script_p, type_de_script_q;
        public static string label_du_script_choisi_p, label_du_script_choisi_s, appreciation_1, appreciation_2;
        public static double Pourcetange_appreciation_1, Pourcetange_appreciation_2;
        public static string Pourcetange_appreciation_11, Pourcetange_appreciation_22;
        public double[] tab_inverse, tab_reordonne, tab;
        public double[] score_DF_norm;
        public double[,] point_sequence_inverse, points_segments_reordonnes;
        public double[] score_direction_ordre_forme, mmatrice_echantillon_test, mmatrice_echantillon_modele_1, mmatrice_echantillon_modele_2, mmatrice_echantillon_modele_3;
        public int[] mat_cod_test, mat_cod_1, mat_cod_2, mat_cod_3;
        public double a, b, c, d, e, f, u, g, h, i;
        public string a1, b1, c1, d1, e1, f1, u1;
        public double a_final, b_final, c_final, d_final, e_final, f_final;
        public double poids_AP = 0.5, poids_AS = 0.5, somme_poids, somme_poids_score;
        public double score_direction_global, score_ordre_global, score_forme_global, score_global;

        public string classe_choisie;
        public string label_du_script_choisi;
        public double[,] mmatrice_param_DFcar_echantillon_test;

        WRDFile WRDfile = new WRDFile();

        public double[] Method_Traitement_Evaluation_car(double[,] xy_echantillon_test, string label_du_script_choisi, string type_de_script, string path, int autorisation_distance_test_faux)
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

            label_du_script_choisi_p = label_du_script_choisi + "_p";
            label_du_script_choisi_s = label_du_script_choisi + "_s";

            VS_Lecture_traitement_save_param_online_beta_ellipt_DFAnCa lecturecar = new VS_Lecture_traitement_save_param_online_beta_ellipt_DFAnCa();
            
            
            mmatrice_param_DFcar_echantillon_test = lecturecar.method_mmatrice_param_DF_car(xy_echantillon_test, label_du_script_choisi);
          string chemin_acces_dossier_examiner1 = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DFcar/";
           string chemin_acces_dossier_examiner11 = chemin_acces_dossier_examiner1 + label_du_script_choisi + "_testbaacar.bin";
            //WRDfile.Method_WriteFile(mmatrice_param_DFcar_echantillon_test, chemin_acces_dossier_examiner11);
            //WRDfile.Method_WriteFile_Bin(chemin_acces_dossier_examiner11, mmatrice_param_DFcar_echantillon_test);





            Evaluation_DFcar Eva_dfcar = new Evaluation_DFcar();
            double[] df = Eva_dfcar.Method_Evaluation_DFcar(mmatrice_param_DFcar_echantillon_test, label_du_script_choisi, type_de_script, path, autorisation_distance_test_faux);
            double[] df_p = Eva_dfcar.Method_Evaluation_DFcar(mmatrice_param_DFcar_echantillon_test, label_du_script_choisi_p, type_de_script, path, autorisation_distance_test_faux);
            double[] df_s = Eva_dfcar.Method_Evaluation_DFcar(mmatrice_param_DFcar_echantillon_test, label_du_script_choisi_s, type_de_script, path, autorisation_distance_test_faux);


            g = max_matrice(df[0], df_p[0], df_s[0], 0);
            h = max_matrice(df[1], df_p[1], df_s[1], 0);
            i = max_matrice(df[2], df_p[2], df_s[2], 0);

            if (g == df[0])
            {
                h = df[1];
                i = df[2];
            }
            else if (g == df_p[0])
            {
                h = df_p[1];
                i = df_p[2];
            }
            else if (g == df_s[0])
            {
                h = df_s[1];
                i = df_s[2];
            }


            tab = new double[3];
            tab[0] = g;
            tab[1] = h;
            tab[2] = i;

            //}
            return tab;
        }

        public double max_matrice(double w, double x, double y, double z)
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

