using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{
    public class Traitement_Evaluation_Verification
    {

        public int poids_score_forme = 4;
        public int poids_score_direction = 1, drapeau_inversion_direction, drapeau_inversion_ordre;
        public double poids_score_ordre = 0.5;
        public double distance_AS_direction, distance_AS_ordre, distance_AS_forme, distance_AP_direction, distance_AP_ordre, distance_AP_forme;
        public string type_de_script, type_de_script_p, type_de_script_q;
        public static string appreciation_1, appreciation_2;
        public static double Pourcetange_appreciation_1, Pourcetange_appreciation_2;
        public static string Pourcetange_appreciation_11, Pourcetange_appreciation_22;
        public double[] tab_inverse, tab_reordonne, tab, tab_DF_inverse, tab_DF_reordonne;
        public double[,] point_sequence_inverse, points_segments_reordonnes, point_sequence_inverse_DF, points_segments_reordonnes_DF;
        public double[] score_direction_ordre_forme, mmatrice_echantillon_test, mmatrice_echantillon_modele_1, mmatrice_echantillon_modele_2, mmatrice_echantillon_modele_3;
        public int[] mat_cod_test, mat_cod_1, mat_cod_2, mat_cod_3;
        public double a, b, c, d, e, f, u, g, h, i;
        public string a1, b1, c1, d1, e1, f1, u1, g1, h1, i1;
        public string[] tab3, tab1;
        double poids_DF;
        public double a_final, b_final, c_final, d_final, e_final, f_final, g_final, h_final, i_final;
        public double poids_AP, poids_AS, somme_poids, somme_poids_score;
        public double score_direction_global, score_ordre_global, score_forme_global, score_global;
        public double[] tab_DF;
        public string classe_choisie;
        public string label_du_script_choisi;
        public double[,] points_delta_fixe2, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_test,
            mmatrice_param_echantillon_modele_3_AP, mmatrice_param_echantillon_modele_1_AS,
            mmatrice_param_echantillon_modele_2_AS, mmatrice_param_echantillon_modele_3_AS;
        Traitement_Evaluation t1 = new Traitement_Evaluation();
        Traitement_Evaluation_DF t2 = new Traitement_Evaluation_DF();
        WRDFile WRDfile = new WRDFile();
        inversion_sequence_points_des_segments_trajectoire inverse_sens = new inversion_sequence_points_des_segments_trajectoire();
        inversion_ordre_segment_trajectoire inverse_ordre = new inversion_ordre_segment_trajectoire();
        public string[] Traitement_Evaluation_verification(double[,] xy_echantillon_test1, double[,] xy_echantillon_test, string label_du_script_choisi, string type_de_script, string path, int autorisation_distance_test_faux,string lg)
        {
            autorisation_distance_test_faux = 1;

            Re_echantillonnage_de_la_trajectoire_a_Delta_T_fixe delta_fixe = new Re_echantillonnage_de_la_trajectoire_a_Delta_T_fixe();
            Raffinage_de_la_quantification_spatiale raffinage = new Raffinage_de_la_quantification_spatiale();
            calibrage_de_l_echantillonnage_V1 calib_ech = new calibrage_de_l_echantillonnage_V1();
            double[,] points_delta_fixe = delta_fixe.method_Re_echantillonnage_de_la_trajectoire_a_Delta_T_fixe(xy_echantillon_test1);
         

            points_delta_fixe2 = new double[points_delta_fixe.GetLength(0), 2];


            for (int j = 0; j < points_delta_fixe.GetLength(0); j++)
            {

                points_delta_fixe2[j, 0] = points_delta_fixe[j, 0];
                points_delta_fixe2[j, 1] = points_delta_fixe[j, 1];
            }
            double[,]  point_Trajectoire_avec_quantification_spatiale_rafinnee = raffinage.method_Raffinage_de_la_quantification_spatiale(points_delta_fixe2);
            Console.WriteLine("taille apres quantification_spatiale_rafinnee" + point_Trajectoire_avec_quantification_spatiale_rafinnee.GetLength(0));
            
            double[,] point_TrajecStoire_reechantillonnee = calib_ech.method_calibrage_de_l_echantillonnage_V1(point_Trajectoire_avec_quantification_spatiale_rafinnee, label_du_script_choisi, type_de_script, path);

            Console.WriteLine("taille apres rechantillonnage" + point_TrajecStoire_reechantillonnee.GetLength(0));

            string chemin_acces_dossier_examiner1 = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DFcar/";
            string chemin_acces_dossier_examiner11 = chemin_acces_dossier_examiner1 + label_du_script_choisi + "_test.bin";
            //WRDfile.Method_WriteFile(mmatrice_param_DFcar_echantillon_test, chemin_acces_dossier_examiner11);
            WRDfile.Method_WriteFile_Bin(chemin_acces_dossier_examiner11, point_TrajecStoire_reechantillonnee);


            tab = t1.Method_Traitement_Evaluation_1(point_TrajecStoire_reechantillonnee, label_du_script_choisi, type_de_script, path, autorisation_distance_test_faux);
            tab_DF=t2.Method_Traitement_Evaluation_1(point_TrajecStoire_reechantillonnee, label_du_script_choisi, type_de_script, path, autorisation_distance_test_faux);
            //Console.WriteLine("DF param forme normal" + tab_DF[0]);
            //Inversion
            point_sequence_inverse = inverse_sens.Method_inversion_sequence_points_des_segments_trajectoire(point_TrajecStoire_reechantillonnee);
            point_sequence_inverse_DF = inverse_sens.Method_inversion_sequence_points_des_segments_trajectoire(point_TrajecStoire_reechantillonnee);
            tab_inverse = t1.Method_Traitement_Evaluation_1(point_sequence_inverse, label_du_script_choisi, type_de_script, path, autorisation_distance_test_faux);
            tab_DF_inverse = t2.Method_Traitement_Evaluation_1(point_sequence_inverse_DF, label_du_script_choisi, type_de_script, path, autorisation_distance_test_faux);
            ///////inversion_ordre  ////////////
            ////Console.WriteLine("DF param forme inversé" + tab_DF_inverse[0]);
            //Reordonner 
            points_segments_reordonnes = inverse_ordre.Method_inversion_ordre_trajectoire(point_TrajecStoire_reechantillonnee);
            points_segments_reordonnes_DF = inverse_ordre.Method_inversion_ordre_trajectoire(point_TrajecStoire_reechantillonnee);
            tab_reordonne = t1.Method_Traitement_Evaluation_1(points_segments_reordonnes, label_du_script_choisi, type_de_script, path, autorisation_distance_test_faux);
            tab_DF_reordonne = t2.Method_Traitement_Evaluation_1(points_segments_reordonnes_DF, label_du_script_choisi, type_de_script, path, autorisation_distance_test_faux);
            ////Console.WriteLine("DF param forme reordonne" + tab_DF_reordonne[0]);
            
            //////////////////////////Estimation des scores finaux/////////////////////
            a = tab[0];
            b = tab[1];
            c = tab[2];
            d = tab[3];
            e = tab[4];
            f = tab[5];
            g = tab_DF[0];
            h = tab_DF[1];
            i = tab_DF[2];
            /* a_final = a ;
             b_final = b;
             c_final = c;
             d_final = d;
             e_final = e;
             f_final = f;
             */
            // paramétrique
            a_final = Math.Max(Math.Max(a, tab_inverse[0]), tab_reordonne[0]);
            if ((a_final == a) && (a_final > 0.5))
            {
                b_final = b;
                c_final = c;
            }

            else if ((a_final == tab_inverse[0]) && (a_final > 0.5))
            {
                b_final = 0;
                c_final = tab_inverse[2];
                drapeau_inversion_direction = 1;
            }

            else if ((a_final == tab_reordonne[0]) && (a_final > 0.5))
            {
                b_final = tab_reordonne[1];
                c_final = 0;
                drapeau_inversion_ordre = 1;
            }
            else
            {
                b_final = b;
                c_final = c;
            }
            // structurelle
            d_final = Math.Max(Math.Max(d, tab_inverse[3]), tab_reordonne[3]);
            if ((d_final == d) && (d_final > 0.5))
            {
                e_final = e;
                f_final = f;
            }
            else if ((d_final == tab_inverse[3]) && (d_final > 0.5))
            {
                e_final = 0;
                f_final = tab_inverse[5];
                drapeau_inversion_direction = 1;
            }
            else if ((d_final == tab_reordonne[3]) && (d_final > 0.5))
            {
                e_final = tab_reordonne[4];
                f_final = 0;
                drapeau_inversion_ordre = 1;
            }
            else
            {
                e_final = e;
                f_final = f;
            }
            ///DF
            g_final = Math.Max(Math.Max(g, tab_DF_inverse[0]), tab_DF_reordonne[0]);
            if ((g_final == g) && (g_final > 0.5))
            {
                h_final = h;
                i_final = i;
            }

            else if ((g_final == tab_DF_inverse[0]) && (g_final > 0.5))
            {
                h_final = 0;
                i_final = tab_DF_inverse[2];
                drapeau_inversion_ordre = 1;
            }

            else if ((g_final == tab_DF_reordonne[0]) && (g_final > 0.5))
            {
                h_final = tab_DF_reordonne[1];
                i_final = 0;
                drapeau_inversion_direction = 1;
            }
            else
            {
                h_final = h;
                i_final = i;
            }
            /////////////////
            nombre_segments nb_segments = new nombre_segments();
            int nb = nb_segments.method_nombre_segment(xy_echantillon_test);
            if (nb < 2)
            {
                c_final = 1;
                f_final = 1;
                i_final = 1;
            }
            else
            {
                c_final = c_final;
                f_final = f_final;
                i_final = i_final;
            }


                a1 = Convert.ToString(Math.Round(a_final * 100));
                b1 = Convert.ToString(Math.Round(b_final * 100));
                c1 = Convert.ToString(Math.Round(c_final * 100));
                d1 = Convert.ToString(Math.Round(d_final * 100));
                e1 = Convert.ToString(Math.Round(e_final * 100));
                f1 = Convert.ToString(Math.Round(f_final * 100));
                g1 = Convert.ToString(Math.Round(g_final * 100));
                h1 = Convert.ToString(Math.Round(h_final * 100));
                i1 = Convert.ToString(Math.Round(i_final * 100));

          
            ///
            poids_AP = 0.1;
            poids_AS = 0.1;
            poids_DF = 0.8;
           // c_final = 1.0;
           // f_final = 1.0;
           // i_final = 1.0;
            somme_poids = poids_AP + poids_AS + poids_DF;

            score_direction_global = ((poids_AP * e_final) + (poids_AS * b_final) + (poids_DF * h_final)) / somme_poids;
            score_forme_global = ((poids_AP * d_final) + (poids_AS * a_final) + (poids_DF * g_final)) / somme_poids;
            score_ordre_global = ((poids_AP * f_final) + (poids_AS * c_final) + (poids_DF * i_final)) / somme_poids;
            somme_poids_score = poids_score_forme + poids_score_direction + poids_score_ordre;
            if ((score_direction_global < 0.3))
            {
                poids_score_forme = 1;
                poids_score_direction = 2;
                poids_score_ordre = 1;
                somme_poids_score = poids_score_forme + poids_score_direction + poids_score_ordre;
            }
            if ((score_ordre_global < 0.3))
            {
                poids_score_forme = 1;
                poids_score_direction = 1;
                poids_score_ordre = 2;
                somme_poids_score = poids_score_forme + poids_score_direction + poids_score_ordre;
            }
            score_global = ((poids_score_forme * score_forme_global) + (poids_score_direction * score_direction_global) + (poids_score_ordre * score_ordre_global)) / somme_poids_score;

            u1 = Convert.ToString(Math.Round(score_global * 100)) + "%";
            if(lg.Equals("arabe"))
            { 
            if ((score_global > 0.75) && (score_global <= 1))
            {
                
                appreciation_1 = "جيد جدا";
                appreciation_2 = "جيد";
                Pourcetange_appreciation_1 = (1 - ((score_global - 0.75) / 0.25)) * 100;
                Pourcetange_appreciation_2 = ((score_global - 0.75) / 0.25) * 100;
                Pourcetange_appreciation_11 = Convert.ToString(Math.Round(Pourcetange_appreciation_1)) + "%";
                Pourcetange_appreciation_22 = Convert.ToString(Math.Round(Pourcetange_appreciation_2)) + "%";

            }
            else if ((score_global > 0.5) && (score_global <= 0.75))
            {
                
                appreciation_1 = "مقبول";
                appreciation_2 = "جيد جدا";
                Pourcetange_appreciation_1 = (1 - ((score_global - 0.5) / 0.25)) * 100;
                Pourcetange_appreciation_2 = ((score_global - 0.5) / 0.25) * 100;
                Pourcetange_appreciation_11 = Convert.ToString(Math.Round(Pourcetange_appreciation_1)) + "%";
                Pourcetange_appreciation_22 = Convert.ToString(Math.Round(Pourcetange_appreciation_2)) + "%";

            }
            else if ((score_global > 0.25) && (score_global <= 0.5))
            {
              
                appreciation_1 = "سيء";
                appreciation_2 = "متوسط";
                Pourcetange_appreciation_1 = (1 - ((score_global - 0.25) / 0.25)) * 100;
                Pourcetange_appreciation_2 = ((score_global - 0.25) / 0.25) * 100;
                Pourcetange_appreciation_11 = Convert.ToString(Math.Round(Pourcetange_appreciation_1)) + "%";
                Pourcetange_appreciation_22 = Convert.ToString(Math.Round(Pourcetange_appreciation_2)) + "%";
            }
            else
            {
                
                appreciation_1 = "سيء جدا";
                appreciation_2 = "سيء";
                Pourcetange_appreciation_1 = (1 - ((score_global - 0) / 0.25)) * 100;
                Pourcetange_appreciation_2 = ((score_global - 0) / 0.25) * 100;
                Pourcetange_appreciation_11 = Convert.ToString(Math.Round(Pourcetange_appreciation_1)) + "%";
                Pourcetange_appreciation_22 = Convert.ToString(Math.Round(Pourcetange_appreciation_2)) + "%";

            }
            if ((a_final == -1) && (b_final == -1) && (c_final == -1) && (d_final == -1) && (e_final == -1) && (f_final == -1))
            {
                appreciation_1 = "#";
                appreciation_2 = "#";
                Pourcetange_appreciation_11 = "#";
                Pourcetange_appreciation_22 = "#";

            }
            }
            else if(lg.Equals("francais"))
            {
                if ((score_global > 0.75) && (score_global <= 1))
                {
                    appreciation_1 = "Très Bien Fait";
                    appreciation_2 = "Parfait";                   
                    Pourcetange_appreciation_1 = (1 - ((score_global - 0.75) / 0.25)) * 100;
                    Pourcetange_appreciation_2 = ((score_global - 0.75) / 0.25) * 100;
                    Pourcetange_appreciation_11 = Convert.ToString(Math.Round(Pourcetange_appreciation_1)) + "%";
                    Pourcetange_appreciation_22 = Convert.ToString(Math.Round(Pourcetange_appreciation_2)) + "%";

                }
                else if ((score_global > 0.5) && (score_global <= 0.75))
                {
                    appreciation_1 = "Acceptable";
                    appreciation_2 = "Très Bien Fait";
                    Pourcetange_appreciation_1 = (1 - ((score_global - 0.5) / 0.25)) * 100;
                    Pourcetange_appreciation_2 = ((score_global - 0.5) / 0.25) * 100;
                    Pourcetange_appreciation_11 = Convert.ToString(Math.Round(Pourcetange_appreciation_1)) + "%";
                    Pourcetange_appreciation_22 = Convert.ToString(Math.Round(Pourcetange_appreciation_2)) + "%";

                }
                else if ((score_global > 0.25) && (score_global <= 0.5))
                {
                    appreciation_1 = "Mauvais";
                    appreciation_2 = "Moyen";
                    Pourcetange_appreciation_1 = (1 - ((score_global - 0.25) / 0.25)) * 100;
                    Pourcetange_appreciation_2 = ((score_global - 0.25) / 0.25) * 100;
                    Pourcetange_appreciation_11 = Convert.ToString(Math.Round(Pourcetange_appreciation_1)) + "%";
                    Pourcetange_appreciation_22 = Convert.ToString(Math.Round(Pourcetange_appreciation_2)) + "%";
                }
                else
                {
                    appreciation_1 = "Très Mal Fait";
                    appreciation_2 = "Mauvais";
                    Pourcetange_appreciation_1 = (1 - ((score_global - 0) / 0.25)) * 100;
                    Pourcetange_appreciation_2 = ((score_global - 0) / 0.25) * 100;
                    Pourcetange_appreciation_11 = Convert.ToString(Math.Round(Pourcetange_appreciation_1)) + "%";
                    Pourcetange_appreciation_22 = Convert.ToString(Math.Round(Pourcetange_appreciation_2)) + "%";

                }
                if ((a_final == -1) && (b_final == -1) && (c_final == -1) && (d_final == -1) && (e_final == -1) && (f_final == -1))
                {
                    appreciation_1 = "#";
                    appreciation_2 = "#";
                    Pourcetange_appreciation_11 = "#";
                    Pourcetange_appreciation_22 = "#";

                }
            }
           else
            {
                if ((score_global > 0.75) && (score_global <= 1))
                {
                   
                    appreciation_1 = "Very Well";
                    appreciation_2 = "Well";
                    Pourcetange_appreciation_1 = (1 - ((score_global - 0.75) / 0.25)) * 100;
                    Pourcetange_appreciation_2 = ((score_global - 0.75) / 0.25) * 100;
                    Pourcetange_appreciation_11 = Convert.ToString(Math.Round(Pourcetange_appreciation_1)) + "%";
                    Pourcetange_appreciation_22 = Convert.ToString(Math.Round(Pourcetange_appreciation_2)) + "%";

                }
                else if ((score_global > 0.5) && (score_global <= 0.75))
                {
                    appreciation_1 = "Acceptable";
                    appreciation_2 = "Très Bien Fait";
                    Pourcetange_appreciation_1 = (1 - ((score_global - 0.5) / 0.25)) * 100;
                    Pourcetange_appreciation_2 = ((score_global - 0.5) / 0.25) * 100;
                    Pourcetange_appreciation_11 = Convert.ToString(Math.Round(Pourcetange_appreciation_1)) + "%";
                    Pourcetange_appreciation_22 = Convert.ToString(Math.Round(Pourcetange_appreciation_2)) + "%";

                }
                else if ((score_global > 0.25) && (score_global <= 0.5))
                {
                   
                    appreciation_1 = "Bad";
                    appreciation_2 = "Medium";
                    Pourcetange_appreciation_1 = (1 - ((score_global - 0.25) / 0.25)) * 100;
                    Pourcetange_appreciation_2 = ((score_global - 0.25) / 0.25) * 100;
                    Pourcetange_appreciation_11 = Convert.ToString(Math.Round(Pourcetange_appreciation_1)) + "%";
                    Pourcetange_appreciation_22 = Convert.ToString(Math.Round(Pourcetange_appreciation_2)) + "%";
                }
                else
                {
                    
                    appreciation_1 = "Very Bad";
                    appreciation_2 = "Bad";
                    Pourcetange_appreciation_1 = (1 - ((score_global - 0) / 0.25)) * 100;
                    Pourcetange_appreciation_2 = ((score_global - 0) / 0.25) * 100;
                    Pourcetange_appreciation_11 = Convert.ToString(Math.Round(Pourcetange_appreciation_1)) + "%";
                    Pourcetange_appreciation_22 = Convert.ToString(Math.Round(Pourcetange_appreciation_2)) + "%";

                }
                if ((a_final == -1) && (b_final == -1) && (c_final == -1) && (d_final == -1) && (e_final == -1) && (f_final == -1))
                {
                    appreciation_1 = "#";
                    appreciation_2 = "#";
                    Pourcetange_appreciation_11 = "#";
                    Pourcetange_appreciation_22 = "#";

                }
            }

            string score_global1 = Convert.ToString(Math.Round(score_global * 100));
            string Pourcetange_appreciation_1_1 = Convert.ToString(Math.Round(Pourcetange_appreciation_1));
            string Pourcetange_appreciation_2_1 = Convert.ToString(Math.Round(Pourcetange_appreciation_2));
            tab1 = new string[15];
            tab1[0] = a1;
            tab1[1] = b1;
            tab1[2] = c1;
            tab1[3] = d1;
            tab1[4] = e1;
            tab1[5] = f1;
            tab1[6] = g1;
            tab1[7] = h1;
            tab1[8] = i1;
            tab1[9] = score_global1;
            tab1[10] = Pourcetange_appreciation_1_1;
            tab1[11] = Pourcetange_appreciation_2_1;
            tab1[12] = Convert.ToString(Math.Round(score_forme_global * 100));
            tab1[13] = Convert.ToString(Math.Round(score_direction_global * 100));
            tab1[14] = Convert.ToString(Math.Round(score_ordre_global * 100));
           /* if (label_du_script_choisi == "symbole1" || label_du_script_choisi == "symbole2" || label_du_script_choisi == "symbole3" || label_du_script_choisi == "symbole4" || label_du_script_choisi == "symbole5" ||
            label_du_script_choisi == "symbole6" || label_du_script_choisi == "symbole7" || label_du_script_choisi == "symbole8" || label_du_script_choisi == "symbole9" || label_du_script_choisi == "symbole10" || label_du_script_choisi == "symbole11" ||
            label_du_script_choisi == "symbole12" || label_du_script_choisi == "symbole13" || label_du_script_choisi == "symbole14" || label_du_script_choisi == "symbole15" || label_du_script_choisi == "symbole16" || label_du_script_choisi == "symbole17" ||
            label_du_script_choisi == "symbole18" || label_du_script_choisi == "symbole19" || label_du_script_choisi == "symbole20" || label_du_script_choisi == "symbole21" || label_du_script_choisi == "symbole22" || label_du_script_choisi == "symbole23")
            {
                tab1[14] = Convert.ToString(100);
                tab1[2] = Convert.ToString(100);
                tab1[5] = Convert.ToString(100);
                tab1[8] = Convert.ToString(100);
            }*/
            
            return tab1;
        }


        public string[] Traitement_Evaluation_verification_pourcentage_appreciation(double[,] xy_echantillon_test1, double[,] xy_echantillon_test, string label_du_script_choisi, string type_de_script, string path, int autorisation_distance_test_faux, string lg)
        {
            Re_echantillonnage_de_la_trajectoire_a_Delta_T_fixe delta_fixe = new Re_echantillonnage_de_la_trajectoire_a_Delta_T_fixe();
            Raffinage_de_la_quantification_spatiale raffinage = new Raffinage_de_la_quantification_spatiale();
            calibrage_de_l_echantillonnage_V1 calib_ech = new calibrage_de_l_echantillonnage_V1();
            double[,] points_delta_fixe = delta_fixe.method_Re_echantillonnage_de_la_trajectoire_a_Delta_T_fixe(xy_echantillon_test1);
            points_delta_fixe2 = new double[points_delta_fixe.GetLength(0), 2];


            for (int j = 0; j < points_delta_fixe.GetLength(0); j++)
            {

                points_delta_fixe2[j, 0] = points_delta_fixe[j, 0];
                points_delta_fixe2[j, 1] = points_delta_fixe[j, 1];
            }
            double[,] point_Trajectoire_avec_quantification_spatiale_rafinnee = raffinage.method_Raffinage_de_la_quantification_spatiale(points_delta_fixe2);
            double[,] point_TrajecStoire_reechantillonnee = calib_ech.method_calibrage_de_l_echantillonnage_V1(point_Trajectoire_avec_quantification_spatiale_rafinnee, label_du_script_choisi, type_de_script, path);

            tab = t1.Method_Traitement_Evaluation_1(point_TrajecStoire_reechantillonnee, label_du_script_choisi, type_de_script, path, autorisation_distance_test_faux);
            tab_DF = t2.Method_Traitement_Evaluation_1(point_TrajecStoire_reechantillonnee, label_du_script_choisi, type_de_script, path, autorisation_distance_test_faux);
            //Console.WriteLine("DF param forme normal" + tab_DF[0]);
            //Inversion
            point_sequence_inverse = inverse_sens.Method_inversion_sequence_points_des_segments_trajectoire(point_TrajecStoire_reechantillonnee);
            point_sequence_inverse_DF = inverse_sens.Method_inversion_sequence_points_des_segments_trajectoire(point_TrajecStoire_reechantillonnee);
            tab_inverse = t1.Method_Traitement_Evaluation_1(point_sequence_inverse, label_du_script_choisi, type_de_script, path, autorisation_distance_test_faux);
            tab_DF_inverse = t2.Method_Traitement_Evaluation_1(point_sequence_inverse_DF, label_du_script_choisi, type_de_script, path, autorisation_distance_test_faux);
            ///////inversion_ordre  ////////////
            ////Console.WriteLine("DF param forme inversé" + tab_DF_inverse[0]);
            //Reordonner 
            points_segments_reordonnes = inverse_ordre.Method_inversion_ordre_trajectoire(point_TrajecStoire_reechantillonnee);
            points_segments_reordonnes_DF = inverse_ordre.Method_inversion_ordre_trajectoire(point_TrajecStoire_reechantillonnee);
            tab_reordonne = t1.Method_Traitement_Evaluation_1(points_segments_reordonnes, label_du_script_choisi, type_de_script, path, autorisation_distance_test_faux);
            tab_DF_reordonne = t2.Method_Traitement_Evaluation_1(points_segments_reordonnes_DF, label_du_script_choisi, type_de_script, path, autorisation_distance_test_faux);
            //Console.WriteLine("DF param forme reordonne" + tab_DF_reordonne[0]);
            //////////////////////////Estimation des scores finaux/////////////////////
            a = tab[0];
            b = tab[1];
            c = tab[2];
            d = tab[3];
            e = tab[4];
            f = tab[5];
            g = tab_DF[0];
            h = tab_DF[1];
            i = tab_DF[2];
            /* a_final = a ;
             b_final = b;
             c_final = c;
             d_final = d;
             e_final = e;
             f_final = f;
             */
            // paramétrique
            a_final = Math.Max(Math.Max(a, tab_inverse[0]), tab_reordonne[0]);
            if ((a_final == a) && (a_final > 0.5))
            {
                b_final = b;
                c_final = c;
            }

            else if ((a_final == tab_inverse[0]) && (a_final > 0.5))
            {
                b_final = 0;
                c_final = tab_inverse[2];
                drapeau_inversion_direction = 1;
            }

            else if ((a_final == tab_reordonne[0]) && (a_final > 0.5))
            {
                b_final = tab_reordonne[1];
                c_final = 0;
                drapeau_inversion_ordre = 1;
            }
            else
            {
                b_final = b;
                c_final = c;
            }
            // structurelle
            d_final = Math.Max(Math.Max(d, tab_inverse[3]), tab_reordonne[3]);
            if ((d_final == d) && (d_final > 0.5))
            {
                e_final = e;
                f_final = f;
            }
            else if ((d_final == tab_inverse[3]) && (d_final > 0.5))
            {
                e_final = 0;
                f_final = tab_inverse[5];
                drapeau_inversion_ordre = 1;
            }
            else if ((d_final == tab_reordonne[3]) && (d_final > 0.5))
            {
                e_final = tab_reordonne[4];
                f_final = 0;
                drapeau_inversion_direction = 1;
            }
            else
            {
                e_final = e;
                f_final = f;
            }
            ///DF
            g_final = Math.Max(Math.Max(g, tab_DF_inverse[0]), tab_DF_reordonne[0]);
            if ((g_final == g) && (g_final > 0.5))
            {
                h_final = h;
                i_final = i;
            }

            else if ((g_final == tab_DF_inverse[0]) && (g_final > 0.5))
            {
                h_final = 0;
                i_final = tab_DF_inverse[2];
                drapeau_inversion_ordre = 1;
            }

            else if ((g_final == tab_DF_reordonne[0]) && (g_final > 0.5))
            {
                h_final = tab_DF_reordonne[1];
                i_final = 0;
                drapeau_inversion_direction = 1;
            }
            else
            {
                h_final = h;
                i_final = i;
            }
            /////////////////

            nombre_segments nb_segments = new nombre_segments();
            int nb = nb_segments.method_nombre_segment(xy_echantillon_test);
            if (nb < 2)
            {
                c_final = 1;
                f_final = 1;
                i_final = 1;
            }
            else
            {
                c_final = c_final;
                f_final = f_final;
                i_final = i_final;
            }
                a1 = Convert.ToString(Math.Round(a_final * 100));
                b1 = Convert.ToString(Math.Round(b_final * 100)) ;
                c1 = Convert.ToString(Math.Round(c_final * 100));
                d1 = Convert.ToString(Math.Round(d_final * 100));
                e1 = Convert.ToString(Math.Round(e_final * 100)) ;
                f1 = Convert.ToString(Math.Round(f_final * 100)) ;
                g1 = Convert.ToString(Math.Round(g_final * 100));
                h1 = Convert.ToString(Math.Round(h_final * 100));
                i1 = Convert.ToString(Math.Round(i_final * 100));
           
           
            poids_AP = 0.1;
            poids_AS = 0.1;
            poids_DF = 0.8;
           
            somme_poids = poids_AP + poids_AS + poids_DF;
            score_direction_global = ((poids_AP * e_final) + (poids_AS * b_final) + (poids_DF * h_final)) / somme_poids;
            score_forme_global = ((poids_AP * d_final) + (poids_AS * a_final) + (poids_DF * g_final)) / somme_poids;
            score_ordre_global = ((poids_AP * f_final) + (poids_AS * c_final) + (poids_DF * i_final)) / somme_poids;
            somme_poids_score = poids_score_forme + poids_score_direction + poids_score_ordre;

            if ((score_direction_global < 0.3))
            {
                poids_score_forme = 1;
                poids_score_direction = 2;
                poids_score_ordre = 1; 
                somme_poids_score = poids_score_forme + poids_score_direction + poids_score_ordre;
            }
            if ((score_ordre_global < 0.3))
            {
                poids_score_forme = 1;
                poids_score_direction = 1;
                poids_score_ordre = 2;
                somme_poids_score = poids_score_forme + poids_score_direction + poids_score_ordre;
            }

            score_global = ((poids_score_forme * score_forme_global) + (poids_score_direction * score_direction_global) + (poids_score_ordre * score_ordre_global)) / somme_poids_score;




            u1 = Convert.ToString(Math.Round(score_global * 100)) + "%";
            if (lg.Equals("arabe"))
            {
                if ((score_global > 0.75) && (score_global <= 1))
                {

                    appreciation_1 = "جيد جدا";
                    appreciation_2 = "جيد";
                    Pourcetange_appreciation_1 = (1 - ((score_global - 0.75) / 0.25)) * 100;
                    Pourcetange_appreciation_2 = ((score_global - 0.75) / 0.25) * 100;
                    Pourcetange_appreciation_11 = Convert.ToString(Math.Round(Pourcetange_appreciation_1)) + "%";
                    Pourcetange_appreciation_22 = Convert.ToString(Math.Round(Pourcetange_appreciation_2)) + "%";

                }
                else if ((score_global > 0.5) && (score_global <= 0.75))
                {

                    appreciation_1 = "مقبول";
                    appreciation_2 = "جيد جدا";
                    Pourcetange_appreciation_1 = (1 - ((score_global - 0.5) / 0.25)) * 100;
                    Pourcetange_appreciation_2 = ((score_global - 0.5) / 0.25) * 100;
                    Pourcetange_appreciation_11 = Convert.ToString(Math.Round(Pourcetange_appreciation_1)) + "%";
                    Pourcetange_appreciation_22 = Convert.ToString(Math.Round(Pourcetange_appreciation_2)) + "%";

                }
                else if ((score_global > 0.25) && (score_global <= 0.5))
                {

                    appreciation_1 = "سيء";
                    appreciation_2 = "متوسط";
                    Pourcetange_appreciation_1 = (1 - ((score_global - 0.25) / 0.25)) * 100;
                    Pourcetange_appreciation_2 = ((score_global - 0.25) / 0.25) * 100;
                    Pourcetange_appreciation_11 = Convert.ToString(Math.Round(Pourcetange_appreciation_1)) + "%";
                    Pourcetange_appreciation_22 = Convert.ToString(Math.Round(Pourcetange_appreciation_2)) + "%";
                }
                else
                {

                    appreciation_1 = "سيء جدا";
                    appreciation_2 = "سيء";
                    Pourcetange_appreciation_1 = (1 - ((score_global - 0) / 0.25)) * 100;
                    Pourcetange_appreciation_2 = ((score_global - 0) / 0.25) * 100;
                    Pourcetange_appreciation_11 = Convert.ToString(Math.Round(Pourcetange_appreciation_1)) + "%";
                    Pourcetange_appreciation_22 = Convert.ToString(Math.Round(Pourcetange_appreciation_2)) + "%";

                }
                if ((a_final == -1) && (b_final == -1) && (c_final == -1) && (d_final == -1) && (e_final == -1) && (f_final == -1))
                {
                    appreciation_1 = "#";
                    appreciation_2 = "#";
                    Pourcetange_appreciation_11 = "#";
                    Pourcetange_appreciation_22 = "#";

                }
            }
            else if (lg.Equals("francais"))
            {
                if ((score_global > 0.75) && (score_global <= 1))
                {
                    appreciation_1 = "Très Bien Fait";
                    appreciation_2 = "Parfait";
                    Pourcetange_appreciation_1 = (1 - ((score_global - 0.75) / 0.25)) * 100;
                    Pourcetange_appreciation_2 = ((score_global - 0.75) / 0.25) * 100;
                    Pourcetange_appreciation_11 = Convert.ToString(Math.Round(Pourcetange_appreciation_1)) + "%";
                    Pourcetange_appreciation_22 = Convert.ToString(Math.Round(Pourcetange_appreciation_2)) + "%";

                }
                else if ((score_global > 0.5) && (score_global <= 0.75))
                {
                    appreciation_1 = "Acceptable";
                    appreciation_2 = "Très Bien Fait";
                    Pourcetange_appreciation_1 = (1 - ((score_global - 0.5) / 0.25)) * 100;
                    Pourcetange_appreciation_2 = ((score_global - 0.5) / 0.25) * 100;
                    Pourcetange_appreciation_11 = Convert.ToString(Math.Round(Pourcetange_appreciation_1)) + "%";
                    Pourcetange_appreciation_22 = Convert.ToString(Math.Round(Pourcetange_appreciation_2)) + "%";

                }
                else if ((score_global > 0.25) && (score_global <= 0.5))
                {
                    appreciation_1 = "Mauvais";
                    appreciation_2 = "Moyen";
                    Pourcetange_appreciation_1 = (1 - ((score_global - 0.25) / 0.25)) * 100;
                    Pourcetange_appreciation_2 = ((score_global - 0.25) / 0.25) * 100;
                    Pourcetange_appreciation_11 = Convert.ToString(Math.Round(Pourcetange_appreciation_1)) + "%";
                    Pourcetange_appreciation_22 = Convert.ToString(Math.Round(Pourcetange_appreciation_2)) + "%";
                }
                else
                {
                    appreciation_1 = "Très Mal Fait";
                    appreciation_2 = "Mauvais";
                    Pourcetange_appreciation_1 = (1 - ((score_global - 0) / 0.25)) * 100;
                    Pourcetange_appreciation_2 = ((score_global - 0) / 0.25) * 100;
                    Pourcetange_appreciation_11 = Convert.ToString(Math.Round(Pourcetange_appreciation_1)) + "%";
                    Pourcetange_appreciation_22 = Convert.ToString(Math.Round(Pourcetange_appreciation_2)) + "%";

                }
                if ((a_final == -1) && (b_final == -1) && (c_final == -1) && (d_final == -1) && (e_final == -1) && (f_final == -1))
                {
                    appreciation_1 = "#";
                    appreciation_2 = "#";
                    Pourcetange_appreciation_11 = "#";
                    Pourcetange_appreciation_22 = "#";

                }
            }
            else
            {
                if ((score_global > 0.75) && (score_global <= 1))
                {

                    appreciation_1 = "Very Well";
                    appreciation_2 = "Well";
                    Pourcetange_appreciation_1 = (1 - ((score_global - 0.75) / 0.25)) * 100;
                    Pourcetange_appreciation_2 = ((score_global - 0.75) / 0.25) * 100;
                    Pourcetange_appreciation_11 = Convert.ToString(Math.Round(Pourcetange_appreciation_1)) + "%";
                    Pourcetange_appreciation_22 = Convert.ToString(Math.Round(Pourcetange_appreciation_2)) + "%";

                }
                else if ((score_global > 0.5) && (score_global <= 0.75))
                {
                    appreciation_1 = "Acceptable";
                    appreciation_2 = "Très Bien Fait";
                    Pourcetange_appreciation_1 = (1 - ((score_global - 0.5) / 0.25)) * 100;
                    Pourcetange_appreciation_2 = ((score_global - 0.5) / 0.25) * 100;
                    Pourcetange_appreciation_11 = Convert.ToString(Math.Round(Pourcetange_appreciation_1)) + "%";
                    Pourcetange_appreciation_22 = Convert.ToString(Math.Round(Pourcetange_appreciation_2)) + "%";

                }
                else if ((score_global > 0.25) && (score_global <= 0.5))
                {

                    appreciation_1 = "Bad";
                    appreciation_2 = "Medium";
                    Pourcetange_appreciation_1 = (1 - ((score_global - 0.25) / 0.25)) * 100;
                    Pourcetange_appreciation_2 = ((score_global - 0.25) / 0.25) * 100;
                    Pourcetange_appreciation_11 = Convert.ToString(Math.Round(Pourcetange_appreciation_1)) + "%";
                    Pourcetange_appreciation_22 = Convert.ToString(Math.Round(Pourcetange_appreciation_2)) + "%";
                }
                else
                {

                    appreciation_1 = "Very Bad";
                    appreciation_2 = "Bad";
                    Pourcetange_appreciation_1 = (1 - ((score_global - 0) / 0.25)) * 100;
                    Pourcetange_appreciation_2 = ((score_global - 0) / 0.25) * 100;
                    Pourcetange_appreciation_11 = Convert.ToString(Math.Round(Pourcetange_appreciation_1)) + "%";
                    Pourcetange_appreciation_22 = Convert.ToString(Math.Round(Pourcetange_appreciation_2)) + "%";

                }
                if ((a_final == -1) && (b_final == -1) && (c_final == -1) && (d_final == -1) && (e_final == -1) && (f_final == -1))
                {
                    appreciation_1 = "#";
                    appreciation_2 = "#";
                    Pourcetange_appreciation_11 = "#";
                    Pourcetange_appreciation_22 = "#";

                }
            }
            tab3 = new string[4];
            tab3[0] = appreciation_1;
            tab3[1] = appreciation_2;
            tab3[2] = Pourcetange_appreciation_11;
            tab3[3] = Pourcetange_appreciation_22;
            return tab3;
        }
    }
}
