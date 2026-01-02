using System;
using System.Collections.Generic;
using System.IO;

using System.Text;

using static System.Net.WebRequestMethods;
using System.Security.AccessControl;
namespace Beta_elliptic_model
{
    public class Traitement_Apprentissage_origine
    {
        public int num_echa, num_echantillon_modele_1_AP, drapeau_autorisation_apprentissage_partiel, size_label_choisi, nombre_de_caractere, num_echantillon, numero_echantillon_modele_1_en_caractere, numero_echantillon_modele_2_en_caractere, numero_echantillon_modele_3_en_caractere;
        public int num_echantillon_modele_2_AP, num_echantillon_modele_3_AP, num_echantillon_modele_1_AS, num_echantillon_modele_2_AS, num_echantillon_modele_3_AS;
        public int[] mat_cod_test, mat_cod_1, mat_cod_2, mat_cod_3, Rangs_ordonnes_des_echantillons_modeles_AP, Rangs_ordonnes_des_echantillons_modeles_AS;
        private double[] mmatrice_echantillon_modele_3;
        private double[] mmatrice_echantillon_modele_2;
        private double[] mmatrice_echantillon_modele_1;
        List<double> cor_11 = new List<double>();
        List<double> cor_12 = new List<double>();
        public double distance_DFcar_direction, distance_DFcar_ordre, distance_DFcar_forme, distance_AS_direction_0, distance_DF_ordre, distance_DF_direction, distance_AS_direction, distance_AS_ordre_0, distance_AS_ordre, distance_AS_forme_0, distance_AP_forme_0, distance_AS_forme, distance_AP_direction, distance_AP_ordre, distance_AP_forme, distance_DF_forme, distance_DF_forme_0, distance_DF_ordre_0, distance_DF_direction_0;
        public string chemin_acces_param_DF_echantillon_modele_1_AP, chemin_acces_param_DF_echantillon_modele_2_AP, chemin_acces_param_DF_echantillon_modele_3_AP, chemin_acces_fichier_destination_matrice_param_DFcar, chemin_acces_fichier_param_echantillon_courant, chemin_acces_param_echantillon_modele_1_AP, chemin_acces_param_echantillon_modele_2_AP, chemin_acces_param_echantillon_modele_3_AP, chemin_acces_param_echantillon_modele_1_AS, chemin_acces_param_echantillon_modele_2_AS, chemin_acces_param_echantillon_modele_3_AS, chemin_acces_dossier_label_modele, classe_de_l_ensemble_des_echantillons_references, classe_des_echantillons_candidats, type_de_script_p, type_de_script_q, label_choisi, label_choisi_sans_lettre, label;
        public string chemin_acces_sous_dossier_param_DFcar, chemin_acces_sous_dossier_param_DF, chemin_acces_dossier_matrices_seuils, chemin_acces_dossier_matrices_distance, chemin_acces_param_echantillon_modele, type_de_script, chemin_acces_fichier_echantillon_courant, chemin_acces_xy_echantillon_courant, chemin_acces_dossier_label, chemin_acces_sous_dossier_courant, chemin_acces_sous_dossier_param, chemin_acces_dossier_examiner, chemin_acces_dossier_distance, chemin_acces_fichier_distances_parametriques, chemin_acces_fichier_distances_parametriques_0, chemin_acces_fichier_distances_structurelles, chemin_acces_fichier_distances_structurelles_0, chemin_acces_param_echantillon_modele_1, chemin_acces_param_echantillon_modele_2, chemin_acces_param_echantillon_modele_3, chemin_acces_dossier_destination_xy_traject, chemin_acces_fichier_destination_matrice_param, chemin_acces_dossier_destination_matrice_param, chemin_acces_fichier_destination_xy_traject;
        private double[,] mmatrice_param_DFcar_echantillon_modele_1_AP, mmatrice_param_DFcar_echantillon_modele_2_AP, mmatrice_param_DFcar_echantillon_modele_3_AP, mmatrice_param_DF_echantillon_modele_1_AP, mmatrice_param_DF_echantillon_modele_2_AP, mmatrice_param_DF_echantillon_modele_3_AP, mmatrice_param_DF_echantillon_courant, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_1, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, mmatrice_param_echantillon_modele_1_AS, mmatrice_param_echantillon_modele_2_AS, mmatrice_param_echantillon_modele_3_AS;
        private double[,] xy_echantillon_courant;
        public char premier_caractere, deuxieme_caractere, troisieme_caractere;
        public double[,] mmatrice_param_DFcar_echantillon_courant, mmatrice_param_echantillon_test, mm_distance_AP, mm_distance_AS, mm_distance_DFcar;
        public double[] mmatrice_echantillon_test, mmatrice_echantillon_modele_1_AS, mmatrice_echantillon_modele_2_AS, mmatrice_echantillon_modele_3_AS;
        public double[,] mmatrice_param_echantillon_courant, mm_distance_DF;
        public class_codage_p codage_p1 = new class_codage_p(); 
        Module_Comparaison_Structurelle comparaison_structurelle = new Module_Comparaison_Structurelle();
        Module_estimation_direction_score direction_score = new Module_estimation_direction_score();
        Module_estimation_ordre_score ordre_score = new Module_estimation_ordre_score();
        Module_estimation_forme_score forme_score = new Module_estimation_forme_score();
        Module_estimation_forme_score_Df forme_score_DF = new Module_estimation_forme_score_Df();
        Module_estimation_ordre_score_DF ordre_score_DF = new Module_estimation_ordre_score_DF();
        Module_estimation_direction_score_DF direction_score_DF = new Module_estimation_direction_score_DF();
        Domaine_de_variation_DF Dvariation = new Domaine_de_variation_DF();
        Domaine_de_variation_DFcar Dvariationcar = new Domaine_de_variation_DFcar();
        Module_estimation_forme_score_Dfcar forme_score_DFcar = new Module_estimation_forme_score_Dfcar();
        Module_estimation_ordre_score_DFcar ordre_score_DFcar = new Module_estimation_ordre_score_DFcar();
        Module_estimation_direction_score_DFcar direction_score_DFcar = new Module_estimation_direction_score_DFcar();
        

        VS_Lecture_traitement_save_param_phrase_online_beta_elliptique lecture = new VS_Lecture_traitement_save_param_phrase_online_beta_elliptique();
        VS_Lecture_traitement_save_param_online_beta_ellipt_DFAnCa lecturecar = new VS_Lecture_traitement_save_param_online_beta_ellipt_DFAnCa();

        Selection_des_Echantillons_Modeles Selection_echantillon = new Selection_des_Echantillons_Modeles();
        Estimation_et_Enregistrement_des_Seuils_V1 estimation_seuil = new Estimation_et_Enregistrement_des_Seuils_V1();
        WRDFile WRDfile = new WRDFile();
        double[] vect_distance_AP, vect_distance_AP_0, vect_distance_AS, vect_distance_AS_0, vect_distance_DF, vect_distance_DF_0;
        //double[] seuil_parametrique1, seuil_structurelle1;
        private double[,] mm1, mm1_0, mm1_1, mm1_1_0, mm2_2_0, mm3_3_0;
        private double[,] mm2, mm2_0, mm2_2, mm3_3, mm3, mm3_0;
        public double[] seuil_parametrique1;
        public double[] seuil_structurelle1 = new double[6];
        public WRDFile RDF = new WRDFile();
        public double[,] points_delta_fixe2;
        public void Method_Traitement_apprentissage(double[,] tes, string label_du_script_choisi, string classe_choisie, int mise_a_jour_ou_apprentissage, int type_procedure_apprentissage, int type_extracteur, string type_de_script, string path, int type_approche, int autorisation_distance_test_faux)
        {

            Console.WriteLine("Pathh  " + path);
            vect_distance_AP = new double[3];
            vect_distance_AS = new double[3];
            vect_distance_DF = new double[3];
            //type_de_script = "Lettre";
            if (type_de_script.Equals("Mot___"))
            {
                type_de_script_p = "Mot";
                type_de_script_q = "mots";
            }
            else if (type_de_script.Equals("Lettre"))
            {
                type_de_script_p = "Lettre";
                type_de_script_q = "lettres";
            }
            if (type_de_script.Equals("Lettre"))
            {
                premier_caractere = 'L';
                deuxieme_caractere = 'e';
                troisieme_caractere = 't';
                nombre_de_caractere = 6;
            }
            else
            {
                premier_caractere = 'M';
                deuxieme_caractere = 'o';
                troisieme_caractere = 't';
                nombre_de_caractere = 3;

            }

            /*       string chemin_acces_dossier_destination_rang = path + "/base_de_lettres/echantillons_modeles/Lettre_Ba_isole/";

                   int[] rang = new int[3];
                   rang[0] = 1;
                   rang[1] = 2;
                   rang[2] = 3;
                   // rang[3] = 4;
                   // rang[4] = 5;
                   // rang[5] = 6;

                   RDF.Method_WriteFile_Int(rang, chemin_acces_dossier_destination_rang + "Rangs_ordonnes_des_echantillons_modeles_AP.txt");
                   RDF.Method_WriteFile_Int(rang, chemin_acces_dossier_destination_rang + "Rangs_ordonnes_des_echantillons_modeles_AS.txt");
                 */
            // *************** enregistrement de la trajectoire de l'échantillon choisi  **********
            // ************************************************************************************
            //chemin_acces_dossier_destination_xy_traject = new string[]{ @"C:\yahia\yahia\base_de_", type_de_script_q ,@"\echantillons_", classe_choisie , @"\", type_de_script_p,"_", label_du_script_choisi , "_isole",@"\xy_traject\" };

            chemin_acces_dossier_destination_xy_traject = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole" + "/xy_traject/";

            //Console.WriteLine(chemin_acces_dossier_destination_xy_traject);
            chemin_acces_dossier_destination_matrice_param = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole" + "/matrice_param/";
            string chemin_acces_dossier_destination_matrice_param_DF = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole" + "/matrice_param_DF/";

            if (!Directory.Exists(chemin_acces_dossier_destination_xy_traject))
            { Directory.CreateDirectory(chemin_acces_dossier_destination_xy_traject); }
            if (!Directory.Exists(chemin_acces_dossier_destination_matrice_param))
            { Directory.CreateDirectory(chemin_acces_dossier_destination_matrice_param); }
            if (!Directory.Exists(chemin_acces_dossier_destination_matrice_param_DF))
            { Directory.CreateDirectory(chemin_acces_dossier_destination_matrice_param_DF); }
            /*  chemin_acces_dossier_destination_xy_traject1 = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole" + "/xy_traject/";
              chemin_acces_dossier_destination_matrice_param1 = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole" + "/matrice_param/";
              if (!Directory.Exists(chemin_acces_dossier_destination_xy_traject1))
              { Directory.CreateDirectory(chemin_acces_dossier_destination_xy_traject1); }
              if (!Directory.Exists(chemin_acces_dossier_destination_matrice_param1))
              { Directory.CreateDirectory(chemin_acces_dossier_destination_matrice_param1); }*/
            Re_echantillonnage_de_la_trajectoire_a_Delta_T_fixe delta_fixe = new Re_echantillonnage_de_la_trajectoire_a_Delta_T_fixe();
            Raffinage_de_la_quantification_spatiale raffinage = new Raffinage_de_la_quantification_spatiale();
            calibrage_de_l_echantillonnage_V1 calib_ech = new calibrage_de_l_echantillonnage_V1();
            double[,] points_delta_fixe = delta_fixe.method_Re_echantillonnage_de_la_trajectoire_a_Delta_T_fixe(tes);
            points_delta_fixe2 = new double[points_delta_fixe.GetLength(0), 2];


            for (int j = 0; j < points_delta_fixe.GetLength(0); j++)
            {

                points_delta_fixe2[j, 0] = points_delta_fixe[j, 0];
                points_delta_fixe2[j, 1] = points_delta_fixe[j, 1];
            }
            double[,] point_Trajectoire_avec_quantification_spatiale_rafinnee = raffinage.method_Raffinage_de_la_quantification_spatiale(points_delta_fixe2);
            double[,] point_TrajecStoire_reechantillonnee = calib_ech.method_calibrage_de_l_echantillonnage_V1(point_Trajectoire_avec_quantification_spatiale_rafinnee, label_du_script_choisi, type_de_script, path);

            var num_echantillon = System.IO.Directory.GetFiles(chemin_acces_dossier_destination_xy_traject, "*.bin").Length + 1;

            chemin_acces_fichier_destination_xy_traject = chemin_acces_dossier_destination_xy_traject + label_du_script_choisi + "_" + classe_choisie + "_" + num_echantillon + ".bin";
            //string chemin_acces_fichier_destination_xy_traject1 = chemin_acces_dossier_destination_xy_traject + label_du_script_choisi + "_" + "modele_tes" + "_" + num_echantillon + ".txt";

           WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_destination_xy_traject, point_TrajecStoire_reechantillonnee);
           //WRDfile.Method_WriteFile(tes, chemin_acces_fichier_destination_xy_traject1);
            chemin_acces_fichier_destination_matrice_param = chemin_acces_dossier_destination_matrice_param + label_du_script_choisi + "_" + classe_choisie + "_" + num_echantillon + ".bin";
            string chemin_acces_fichier_destination_matrice_param_DF = chemin_acces_dossier_destination_matrice_param_DF + label_du_script_choisi + "_" + classe_choisie + "_" + num_echantillon + ".bin";

            // chemin_acces_fichier_destination_matrice_param1 = chemin_acces_dossier_destination_matrice_param1 + label_du_script_choisi + "_" + "modele" + "_" + num_echantillon + ".txt";
            /* Console.WriteLine("Sequenceeeeeeeee pointsssssssssss");
             for (int i = 0; i < tes.GetLength(0); i++)
                 for (int j = 0; j < tes.GetLength(1); j++)
                     Console.WriteLine(tes[i,j]);
             */
          
            mmatrice_param_echantillon_test = lecture.method_mmatrice_param_1(point_TrajecStoire_reechantillonnee, label_du_script_choisi);
           WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_destination_matrice_param, mmatrice_param_echantillon_test);
          
            double[,] mmatrice_param_DF = lecture.method_mmatrice_param_DF(point_TrajecStoire_reechantillonnee, label_du_script_choisi);
           WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_destination_matrice_param_DF, mmatrice_param_DF);
            // //WRDfile.Method_WriteFile(mmatrice_param_echantillon_test, chemin_acces_fichier_destination_matrice_param1);


            if (mise_a_jour_ou_apprentissage == 1)
            {

                chemin_acces_param_echantillon_modele = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/";
                //Console.WriteLine(chemin_acces_param_echantillon_modele);
                var av_file_10 = System.IO.Directory.GetFiles(chemin_acces_param_echantillon_modele, "*.bin");
                var size_av_file_10 = System.IO.Directory.GetFiles(chemin_acces_param_echantillon_modele, "*.bin").Length;
                chemin_acces_dossier_label_modele = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/";
                System.IO.DirectoryInfo info = new System.IO.DirectoryInfo(chemin_acces_dossier_label_modele);

                if (info.Exists)
                {
                    /* Rangs_ordonnes_des_echantillons_modeles_AP = WRDfile.Method_ReadFile_Int(chemin_acces_dossier_label_modele + "Rangs_ordonnes_des_echantillons_modeles_AP.txt");
                     Rangs_ordonnes_des_echantillons_modeles_AS = WRDfile.Method_ReadFile_Int(chemin_acces_dossier_label_modele + "Rangs_ordonnes_des_echantillons_modeles_AS.txt");

                     num_echantillon_modele_1_AP = Rangs_ordonnes_des_echantillons_modeles_AP[0];
                     num_echantillon_modele_2_AP = Rangs_ordonnes_des_echantillons_modeles_AP[1];
                     num_echantillon_modele_3_AP = Rangs_ordonnes_des_echantillons_modeles_AP[2];
                     //num_echantillon_modele_3_AP = 2;
                     num_echantillon_modele_1_AS = Rangs_ordonnes_des_echantillons_modeles_AS[0];
                     num_echantillon_modele_2_AS = Rangs_ordonnes_des_echantillons_modeles_AS[1];
                     num_echantillon_modele_3_AS = Rangs_ordonnes_des_echantillons_modeles_AS[2];*/
                    //num_echantillon_modele_3_AS = 2;
                    num_echantillon_modele_1_AP = 1;
                    num_echantillon_modele_2_AP = 2;
                    num_echantillon_modele_3_AP = 3;
                    num_echantillon_modele_1_AS = 1;
                    num_echantillon_modele_2_AS = 2;
                    num_echantillon_modele_3_AS = 3;

                }
                else if (size_av_file_10 >= 3)
                {
                    num_echantillon_modele_1_AP = 1;
                    num_echantillon_modele_2_AP = 2;
                    num_echantillon_modele_3_AP = 3;
                    num_echantillon_modele_1_AS = 1;
                    num_echantillon_modele_2_AS = 2;
                    num_echantillon_modele_3_AS = 3;
                }


                /* chemin_acces_xy_echantillon_modele_1_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/xy_traject/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_1_AP + ".txt";
                 chemin_acces_xy_echantillon_modele_2_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_2_AP + ".txt";
                 chemin_acces_xy_echantillon_modele_3_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_3_AP + ".txt";

                 chemin_acces_xy_echantillon_modele_1_AS = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/xy_traject/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_1_AS + ".txt";
                 chemin_acces_xy_echantillon_modele_2_AS = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_2_AS + ".txt";
                 chemin_acces_xy_echantillon_modele_3_AS = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_3_AS + ".txt";
                 */
                chemin_acces_param_echantillon_modele_1_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_1_AP + ".bin";
                chemin_acces_param_echantillon_modele_2_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_2_AP + ".bin";
                chemin_acces_param_echantillon_modele_3_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_3_AP + ".bin";

                chemin_acces_param_echantillon_modele_1_AS = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_1_AS + ".bin";
                chemin_acces_param_echantillon_modele_2_AS = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_2_AS + ".bin";
                chemin_acces_param_echantillon_modele_3_AS = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_3_AS + ".bin";

                string chemin_acces_param_DF_echantillon_modele_1_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DF/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_1_AP + ".bin";
                string chemin_acces_param_DF_echantillon_modele_2_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DF/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_2_AP + ".bin";
                string chemin_acces_param_DF_echantillon_modele_3_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DF/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_3_AP + ".bin";

                mmatrice_param_echantillon_modele_1_AP = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_1_AP);
                mmatrice_param_echantillon_modele_2_AP = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_2_AP);
                mmatrice_param_echantillon_modele_3_AP = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_3_AP);
                mmatrice_param_echantillon_modele_1_AS = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_1_AS);
                mmatrice_param_echantillon_modele_2_AS = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_2_AS);
                mmatrice_param_echantillon_modele_3_AS = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_3_AS);

                double[,] mmatrice_param_DF_echantillon_modele_1_AP = WRDfile.Method_ReadFile_Bin_Param_DF(chemin_acces_param_DF_echantillon_modele_1_AP);
                double[,] mmatrice_param_DF_echantillon_modele_2_AP = WRDfile.Method_ReadFile_Bin_Param_DF(chemin_acces_param_DF_echantillon_modele_2_AP);
                double[,] mmatrice_param_DF_echantillon_modele_3_AP = WRDfile.Method_ReadFile_Bin_Param_DF(chemin_acces_param_DF_echantillon_modele_3_AP);

                mmatrice_echantillon_test = new double[mmatrice_param_echantillon_test.GetLength(1)];
                for (var ii = 0; ii < mmatrice_param_echantillon_test.GetLength(1); ii++)
                    mmatrice_echantillon_test[ii] = mmatrice_param_echantillon_test[6, ii];

                mat_cod_test = codage_p1.method_codage_p(mmatrice_echantillon_test, mmatrice_param_echantillon_test);

                mmatrice_echantillon_modele_1 = new double[mmatrice_param_echantillon_modele_1_AS.GetLength(1)];
                for (var ii = 0; ii < mmatrice_param_echantillon_modele_1_AS.GetLength(1); ii++)
                    mmatrice_echantillon_modele_1[ii] = mmatrice_param_echantillon_modele_1_AS[6, ii];

                mat_cod_1 = codage_p1.method_codage_p(mmatrice_echantillon_modele_1, mmatrice_param_echantillon_modele_1_AS);

                mmatrice_echantillon_modele_2 = new double[mmatrice_param_echantillon_modele_2_AS.GetLength(1)];
                for (var ii = 0; ii < mmatrice_param_echantillon_modele_2_AS.GetLength(1); ii++)
                    mmatrice_echantillon_modele_2[ii] = mmatrice_param_echantillon_modele_2_AS[6, ii];

                mat_cod_2 = codage_p1.method_codage_p(mmatrice_echantillon_modele_2, mmatrice_param_echantillon_modele_2_AS);

                mmatrice_echantillon_modele_3 = new double[mmatrice_param_echantillon_modele_3_AS.GetLength(1)];
                for (var ii = 0; ii < mmatrice_param_echantillon_modele_3_AS.GetLength(1); ii++)
                    mmatrice_echantillon_modele_3[ii] = mmatrice_param_echantillon_modele_3_AS[6, ii];

                mat_cod_3 = codage_p1.method_codage_p(mmatrice_echantillon_modele_3, mmatrice_param_echantillon_modele_3_AS);



                chemin_acces_dossier_distance = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/";
                chemin_acces_fichier_distances_parametriques = chemin_acces_dossier_distance + "distance_parametrique.bin";
                chemin_acces_fichier_distances_parametriques_0 = chemin_acces_dossier_distance + "distance_parametrique_0.bin";
                chemin_acces_fichier_distances_structurelles = chemin_acces_dossier_distance + "distance_structurelle.bin";
                chemin_acces_fichier_distances_structurelles_0 = chemin_acces_dossier_distance + "distance_structurelle_0.bin";
                string chemin_acces_fichier_distances_DF = chemin_acces_dossier_distance + "distance_DF.bin";
                string chemin_acces_fichier_distances_DF_0 = chemin_acces_dossier_distance + "distance_DF_0.bin";

                if (!Directory.Exists(chemin_acces_dossier_distance))
                { Directory.CreateDirectory(chemin_acces_dossier_distance); }

                int av_files_n = System.IO.Directory.GetFiles(chemin_acces_dossier_distance, "*.bin").Length;
                if (av_files_n >= 1)
                {
                    mm1_1 = WRDfile.Method_ReadFile_Bin_Distance(chemin_acces_fichier_distances_parametriques);
                    mm1_1_0 = WRDfile.Method_ReadFile_Bin_Distance(chemin_acces_fichier_distances_parametriques_0);
                    mm2_2 = WRDfile.Method_ReadFile_Bin_Distance(chemin_acces_fichier_distances_structurelles);
                    mm2_2_0 = WRDfile.Method_ReadFile_Bin_Distance(chemin_acces_fichier_distances_structurelles_0);
                    mm3_3 = WRDfile.Method_ReadFile_Bin_Distance(chemin_acces_fichier_distances_DF);
                    mm3_3_0 = WRDfile.Method_ReadFile_Bin_Distance(chemin_acces_fichier_distances_DF_0);

                    if ((mm1_1.GetLength(0) > 5) && (mm2_2.GetLength(0) > 5) && (mm3_3.GetLength(0) > 5))
                        autorisation_distance_test_faux = 1;
                    else
                        autorisation_distance_test_faux = 0;

                    distance_AS_direction = comparaison_structurelle.method_Calcul_score_CS_direction(mat_cod_test, mat_cod_1, mat_cod_2, mat_cod_3);
                    distance_AS_ordre = comparaison_structurelle.method_Calcul_score_CS_ordre(mat_cod_test, mat_cod_1, mat_cod_2, mat_cod_3);
                    distance_AS_forme = comparaison_structurelle.method_Calcul_score_CS_forme(mat_cod_test, mat_cod_1, mat_cod_2, mat_cod_3, label_du_script_choisi, path, autorisation_distance_test_faux);

                    distance_AS_direction_0 = comparaison_structurelle.method_Calcul_score_CS_direction(mat_cod_test, mat_cod_1, mat_cod_2, mat_cod_3);
                    distance_AS_ordre_0 = comparaison_structurelle.method_Calcul_score_CS_ordre(mat_cod_test, mat_cod_1, mat_cod_2, mat_cod_3);
                    distance_AS_forme_0 = comparaison_structurelle.method_Calcul_score_CS_forme(mat_cod_test, mat_cod_1, mat_cod_2, mat_cod_3, label_du_script_choisi, path, 0);

                    distance_AP_direction = direction_score.method_estimation_direction_score(mmatrice_param_echantillon_test, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_du_script_choisi);
                    distance_AP_ordre = ordre_score.method_estimation_ordre_score(mmatrice_param_echantillon_test, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_du_script_choisi);
                    distance_AP_forme = forme_score.method_Module_estimation_forme_score(mmatrice_param_echantillon_test, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_du_script_choisi, path, autorisation_distance_test_faux);
                    distance_AP_forme_0 = forme_score.method_Module_estimation_forme_score(mmatrice_param_echantillon_test, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_du_script_choisi, path, 0);

                    distance_DF_forme = forme_score_DF.method_Module_estimation_forme_score_DF(mmatrice_param_echantillon_test, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, autorisation_distance_test_faux);
                    distance_DF_ordre = ordre_score_DF.method_Module_estimation_ordre_score_DF(mmatrice_param_echantillon_test, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, autorisation_distance_test_faux);
                    distance_DF_direction = direction_score_DF.method_Module_estimation_direction_score_DF(mmatrice_param_echantillon_test, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, autorisation_distance_test_faux);

                    distance_DF_forme_0 = forme_score_DF.method_Module_estimation_forme_score_DF(mmatrice_param_echantillon_test, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, 0);
                    distance_DF_ordre_0 = ordre_score_DF.method_Module_estimation_ordre_score_DF(mmatrice_param_echantillon_test, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, 0);
                    distance_DF_direction_0 = direction_score_DF.method_Module_estimation_direction_score_DF(mmatrice_param_echantillon_test, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, 0);

                    vect_distance_AP[0] = distance_AP_direction;
                    vect_distance_AP[1] = distance_AP_ordre;
                    vect_distance_AP[2] = distance_AP_forme;
                    vect_distance_AP_0[0] = distance_AP_direction;
                    vect_distance_AP_0[1] = distance_AP_ordre;
                    vect_distance_AP_0[2] = distance_AP_forme_0;

                    vect_distance_AS[0] = distance_AS_direction;
                    vect_distance_AS[1] = distance_AS_ordre;
                    vect_distance_AS[2] = distance_AS_forme;
                    vect_distance_AS_0[0] = distance_AS_direction_0;
                    vect_distance_AS_0[1] = distance_AS_ordre_0;
                    vect_distance_AS_0[2] = distance_AS_forme_0;

                    vect_distance_DF[0] = distance_DF_direction;
                    vect_distance_DF[1] = distance_DF_ordre;
                    vect_distance_DF[2] = distance_DF_forme;
                    vect_distance_DF_0[0] = distance_DF_direction_0;
                    vect_distance_DF_0[1] = distance_DF_ordre_0;
                    vect_distance_DF_0[2] = distance_DF_forme_0;


                    //mm1 = xlsread(chemin_acces_fichier_distances_parametriques);
                    int l = mm1_1.GetLength(0) + 1;
                    mm1 = new double[l, 3];
                    for (int i = 0; i < l - 1; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            mm1[i, j] = mm1_1[i, j];
                        }
                    }
                    mm1[l - 1, 0] = vect_distance_AP[0];
                    mm1[l - 1, 1] = vect_distance_AP[1];
                    mm1[l - 1, 2] = vect_distance_AP[2];
                    WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_parametriques);
                   WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_parametriques, mm1);

                    int l_0 = mm1_1_0.GetLength(0) + 1;
                    mm1_0 = new double[l_0, 3];
                    for (int i = 0; i < l_0 - 1; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            mm1_0[i, j] = mm1_1_0[i, j];
                        }
                    }
                    mm1_0[l - 1, 0] = vect_distance_AP_0[0];
                    mm1_0[l - 1, 1] = vect_distance_AP_0[1];
                    mm1_0[l - 1, 2] = vect_distance_AP_0[2];
                    WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_parametriques_0);
                   WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_parametriques_0, mm1_0);

                    mm2_2 = WRDfile.Method_ReadFile_Bin_Distance(chemin_acces_fichier_distances_structurelles);
                    // mm2_2_0 = WRDfile.Method_ReadFile_Bin_Distance(chemin_acces_fichier_distances_structurelles_0);
                    int o = mm2_2.GetLength(0) + 1;
                    mm2 = new double[o, 3];
                    for (int i = 0; i < o - 1; i++)
                        for (int j = 0; j < 3; j++)
                        {
                            { mm2[i, j] = mm2_2[i, j]; }
                        }
                    mm2[o - 1, 0] = vect_distance_AS[0];
                    mm2[o - 1, 1] = vect_distance_AS[1];
                    mm2[o - 1, 2] = vect_distance_AS[2];
                    WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_structurelles);
                   WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_structurelles, mm2);

                    // mm2_2_0 = WRDfile.Method_ReadFile_Bin_Distance(chemin_acces_fichier_distances_structurelles_0);
                    int o_0 = mm2_2_0.GetLength(0) + 1;
                    mm2_0 = new double[o_0, 3];
                    for (int i = 0; i < o_0 - 1; i++)
                        for (int j = 0; j < 3; j++)
                        {
                            { mm2_0[i, j] = mm2_2_0[i, j]; }
                        }
                    mm2_0[o_0 - 1, 0] = vect_distance_AS_0[0];
                    mm2_0[o_0 - 1, 1] = vect_distance_AS_0[1];
                    mm2_0[o_0 - 1, 2] = vect_distance_AS_0[2];
                    WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_structurelles_0);
                   WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_structurelles_0, mm2_0);

                    int o1 = mm3_3.GetLength(0) + 1;
                    mm3 = new double[o1, 3];
                    for (int i = 0; i < o1 - 1; i++)
                        for (int j = 0; j < 3; j++)
                        {
                            { mm3[i, j] = mm3_3[i, j]; }
                        }
                    mm3[o1 - 1, 0] = vect_distance_DF[0];
                    mm3[o1 - 1, 1] = vect_distance_DF[1];
                    mm3[o1 - 1, 2] = vect_distance_DF[2];

                    WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DF);
                   WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_DF, mm3);


                    int o1_0 = mm3_3_0.GetLength(0) + 1;
                    mm3_0 = new double[o1_0, 3];
                    for (int i = 0; i < o1_0 - 1; i++)
                        for (int j = 0; j < 3; j++)
                        {
                            { mm3_0[i, j] = mm3_3_0[i, j]; }
                        }
                    mm3_0[o1_0 - 1, 0] = vect_distance_DF_0[0];
                    mm3_0[o1_0 - 1, 1] = vect_distance_DF_0[1];
                    mm3_0[o1_0 - 1, 2] = vect_distance_DF_0[2];

                    WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DF_0);
                   WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_DF_0, mm3_0);

                }

                else if (av_files_n == 0)
                {
                    autorisation_distance_test_faux = 0;

                    distance_AS_direction = comparaison_structurelle.method_Calcul_score_CS_direction(mat_cod_test, mat_cod_1, mat_cod_2, mat_cod_3);
                    distance_AS_ordre = comparaison_structurelle.method_Calcul_score_CS_ordre(mat_cod_test, mat_cod_1, mat_cod_2, mat_cod_3);
                    distance_AS_forme = comparaison_structurelle.method_Calcul_score_CS_forme(mat_cod_test, mat_cod_1, mat_cod_2, mat_cod_3, label_du_script_choisi, path, autorisation_distance_test_faux);

                    distance_AP_direction = direction_score.method_estimation_direction_score(mmatrice_param_echantillon_test, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_du_script_choisi);
                    distance_AP_ordre = ordre_score.method_estimation_ordre_score(mmatrice_param_echantillon_test, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_du_script_choisi);
                    distance_AP_forme = forme_score.method_Module_estimation_forme_score(mmatrice_param_echantillon_test, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_du_script_choisi, path, autorisation_distance_test_faux);

                    distance_DF_forme = forme_score_DF.method_Module_estimation_forme_score_DF(mmatrice_param_echantillon_test, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, autorisation_distance_test_faux);
                    distance_DF_ordre = ordre_score_DF.method_Module_estimation_ordre_score_DF(mmatrice_param_echantillon_test, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, autorisation_distance_test_faux);
                    distance_DF_direction = direction_score_DF.method_Module_estimation_direction_score_DF(mmatrice_param_echantillon_test, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, autorisation_distance_test_faux);


                    vect_distance_AP[0] = distance_AP_direction;
                    vect_distance_AP[1] = distance_AP_ordre;
                    vect_distance_AP[2] = distance_AP_forme;

                    vect_distance_AS[0] = distance_AS_direction;
                    vect_distance_AS[1] = distance_AS_ordre;
                    vect_distance_AS[2] = distance_AS_forme;

                    vect_distance_DF[0] = distance_DF_direction;
                    vect_distance_DF[1] = distance_DF_ordre;
                    vect_distance_DF[2] = distance_DF_forme;


                    // mm1 = vect_distance_AP;
                    mm1 = new double[1, 3];
                    mm1[0, 0] = vect_distance_AP[0];
                    mm1[0, 1] = vect_distance_AP[1];
                    mm1[0, 2] = vect_distance_AP[2];
                   WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_parametriques, mm1);
                   WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_parametriques_0, mm1);

                    //mm2 = vect_distance_AS;
                    mm2 = new double[1, 3];
                    mm2[0, 0] = vect_distance_AS[0];
                    mm2[0, 1] = vect_distance_AS[1];
                    mm2[0, 2] = vect_distance_AS[2];
                    // xlswrite(chemin_acces_fichier_distances_structurelles, mm2);
                   WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_structurelles, mm2);
                   WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_structurelles_0, mm2);

                    mm3 = new double[1, 3];
                    mm3[0, 0] = vect_distance_DF[0];
                    mm3[0, 1] = vect_distance_DF[1];
                    mm3[0, 2] = vect_distance_DF[2];
                    // xlswrite(chemin_acces_fichier_distances_structurelles, mm2);
                   WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_DF, mm3);
                   WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_DF_0, mm3);


                }

                // %%%%%%%Mise à jour des valeurs des seuils%%%%%%%%%%%%
                chemin_acces_dossier_matrices_distance = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/";
                chemin_acces_dossier_matrices_seuils = path + "/Record_des_Valeurs_des_Seuils_Appris/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/";
                estimation_seuil.Method_Estimation_et_Enregistrement_des_Seuils_V1(label_du_script_choisi, type_de_script_p, path);
                estimation_seuil.Method_Estimation_et_Enregistrement_des_Seuils_DF(label_du_script_choisi, type_de_script_p, path);

            }

            else if (mise_a_jour_ou_apprentissage == 2)
            {
                //Console.WriteLine("label choisi" + label_du_script_choisi);
                if ((type_procedure_apprentissage == 1) || ((type_procedure_apprentissage == 2) && (type_extracteur == 1)))
                {
                    classe_choisie = "modeles";
                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie;
                    var number_av_files_n2 = System.IO.Directory.GetDirectories(chemin_acces_dossier_examiner).Length;
                    var av_files_n2 = System.IO.Directory.GetDirectories(chemin_acces_dossier_examiner);

                    //System.//Console.WriteLine(av_files_n2[3]);

                    for (int num_label = 0; num_label < number_av_files_n2; num_label++)
                    {
                        chemin_acces_dossier_label = av_files_n2[num_label];
                        chemin_acces_sous_dossier_courant = chemin_acces_dossier_label + "/xy_traject/";
                        chemin_acces_sous_dossier_param = chemin_acces_dossier_label + "/matrice_param/";
                        var size_dossier_label = chemin_acces_dossier_label.Length;
                        for (int ijk = 0; ijk < size_dossier_label - 2; ijk++)  ////
                        {
                            if ((chemin_acces_dossier_label[ijk] == premier_caractere) && (chemin_acces_dossier_label[ijk + 1] == deuxieme_caractere) && (chemin_acces_dossier_label[ijk + 2] == troisieme_caractere))
                            {
                                label_choisi = chemin_acces_dossier_label.Substring(ijk);
                                size_label_choisi = label_choisi.Length;
                                label_choisi_sans_lettre = label_choisi.Substring(nombre_de_caractere + 1, (size_label_choisi - 7 - nombre_de_caractere));
                            }
                        }
                        if ((type_procedure_apprentissage == 2) && (type_extracteur == 1))
                        {
                            if (label_choisi_sans_lettre.Length == label_du_script_choisi.Length)
                            {
                                if (label_choisi_sans_lettre == label_du_script_choisi)
                                {
                                    var av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin");
                                    int size_av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin").Length;
                                    // Console.WriteLine("size "+ size_av_files_n13);
                                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n13; numero_echantillon++)
                                    {
                                        string chemin = av_files_n13[numero_echantillon];
                                        WRDfile.Method_DeleteFiles(chemin);
                                    }

                                    var av_files_n3 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin");
                                    int size_av_files_n3 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin").Length;
                                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n3; numero_echantillon++)
                                    {
                                        string chemin = av_files_n3[numero_echantillon];
                                        chemin_acces_xy_echantillon_courant = chemin;
                                        // xy_echantillon_courant = null;
                                        xy_echantillon_courant = WRDfile.Method_ReadFile_Bin_XY(chemin_acces_xy_echantillon_courant);

                                        // for (int h = 0; h < xy_echantillon_courant.GetLength(0); h++)
                                        //for (int hh = 0; hh < xy_echantillon_courant.GetLength(1); hh++)
                                        // Console.WriteLine(xy_echantillon_courant[h,hh]);
                                        int num_echa = numero_echantillon + 1;
                                        mmatrice_param_echantillon_courant = lecture.method_mmatrice_param_1(xy_echantillon_courant, label_du_script_choisi);
                                        chemin_acces_fichier_destination_matrice_param = chemin_acces_sous_dossier_param + label_choisi_sans_lettre + "_" + classe_choisie.Substring(0, ((classe_choisie.Length - 1))) + "_" + num_echa + ".bin";
                                        // xlswrite(chemin_acces_fichier_destination_matrice_param, mmatrice_param_echantillon_courant);
                                       WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_destination_matrice_param, mmatrice_param_echantillon_courant);
                                    }
                                }
                            }
                        }
                        else
                        {
                            //delete(fullfile(chemin_acces_sous_dossier_param, '*.xls'));
                            // WRDfile.Method_DeleteFiles(chemin_acces_sous_dossier_param);
                            var av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin");
                            int size_av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin").Length;
                            // Console.WriteLine("size "+ size_av_files_n13);
                            for (int numero_echantillon = 0; numero_echantillon < size_av_files_n13; numero_echantillon++)
                            {
                                string chemin = av_files_n13[numero_echantillon];
                                WRDfile.Method_DeleteFiles(chemin);
                            }

                            var av_files_n3 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin");
                            int size_av_files_n3 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin").Length;
                            for (int numero_echantillon = 0; numero_echantillon < size_av_files_n3; numero_echantillon++)
                            {
                                string chemin = av_files_n3[numero_echantillon];
                                chemin_acces_xy_echantillon_courant = chemin;
                                // xy_echantillon_courant = null;
                                xy_echantillon_courant = WRDfile.Method_ReadFile_Bin_XY(chemin_acces_xy_echantillon_courant);

                                // for (int h = 0; h < xy_echantillon_courant.GetLength(0); h++)
                                //for (int hh = 0; hh < xy_echantillon_courant.GetLength(1); hh++)
                                // Console.WriteLine(xy_echantillon_courant[h,hh]);
                                int num_echa = numero_echantillon + 1;
                                mmatrice_param_echantillon_courant = lecture.method_mmatrice_param_1(xy_echantillon_courant, label_du_script_choisi);
                                chemin_acces_fichier_destination_matrice_param = chemin_acces_sous_dossier_param + label_choisi_sans_lettre + "_" + classe_choisie.Substring(0, ((classe_choisie.Length))) + "_" + num_echa + ".bin";
                                // xlswrite(chemin_acces_fichier_destination_matrice_param, mmatrice_param_echantillon_courant);
                                // //WRDfile.Method_WriteFile(mmatrice_param_echantillon_courant, chemin_acces_fichier_destination_matrice_param);
                               WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_destination_matrice_param, mmatrice_param_echantillon_courant);

                            }
                        }
                    }

                    // %%%classe choisie correcte
                    classe_choisie = "correcte";
                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie;
                    number_av_files_n2 = System.IO.Directory.GetDirectories(chemin_acces_dossier_examiner).Length;
                    av_files_n2 = System.IO.Directory.GetDirectories(chemin_acces_dossier_examiner);

                    ///!!!!!




                    for (int num_label = 0; num_label < number_av_files_n2; num_label++)
                    {
                        chemin_acces_dossier_label = av_files_n2[num_label];
                        chemin_acces_sous_dossier_courant = chemin_acces_dossier_label + "/xy_traject/";
                        chemin_acces_sous_dossier_param = chemin_acces_dossier_label + "/matrice_param/";
                        var size_dossier_label = chemin_acces_dossier_label.Length;
                        for (int ijk = 0; ijk < size_dossier_label - 2; ijk++)  ////
                        {
                            if ((chemin_acces_dossier_label[ijk] == premier_caractere) && (chemin_acces_dossier_label[ijk + 1] == deuxieme_caractere) && (chemin_acces_dossier_label[ijk + 2] == troisieme_caractere))
                            {
                                label_choisi = chemin_acces_dossier_label.Substring(ijk);
                                size_label_choisi = label_choisi.Length;
                                label_choisi_sans_lettre = label_choisi.Substring(nombre_de_caractere + 1, (size_label_choisi - 7 - nombre_de_caractere));
                            }
                        }
                        if ((type_procedure_apprentissage == 2) && (type_extracteur == 1))

                        {

                            if (label_choisi_sans_lettre.Length == label_du_script_choisi.Length)
                            {
                                if (label_choisi_sans_lettre == label_du_script_choisi)
                                {
                                    var av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin");
                                    int size_av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin").Length;
                                    // Console.WriteLine("size "+ size_av_files_n13);
                                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n13; numero_echantillon++)
                                    {
                                        string chemin = av_files_n13[numero_echantillon];
                                        WRDfile.Method_DeleteFiles(chemin);
                                    }

                                    var av_files_n3 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin");
                                    int size_av_files_n3 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin").Length;
                                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n3; numero_echantillon++)
                                    {
                                        string chemin = av_files_n3[numero_echantillon];
                                        chemin_acces_xy_echantillon_courant = chemin;
                                        // xy_echantillon_courant = null;
                                        xy_echantillon_courant = WRDfile.Method_ReadFile_Bin_XY(chemin_acces_xy_echantillon_courant);

                                        // for (int h = 0; h < xy_echantillon_courant.GetLength(0); h++)
                                        //for (int hh = 0; hh < xy_echantillon_courant.GetLength(1); hh++)
                                        // Console.WriteLine(xy_echantillon_courant[h,hh]);
                                        int num_echa = numero_echantillon + 1;
                                        mmatrice_param_echantillon_courant = lecture.method_mmatrice_param_1(xy_echantillon_courant, label_du_script_choisi);
                                        chemin_acces_fichier_destination_matrice_param = chemin_acces_sous_dossier_param + label_choisi_sans_lettre + "_" + classe_choisie.Substring(0, ((classe_choisie.Length))) + "_" + num_echa + ".bin";
                                        // xlswrite(chemin_acces_fichier_destination_matrice_param, mmatrice_param_echantillon_courant);
                                        ////WRDfile.Method_WriteFile(mmatrice_param_echantillon_courant, chemin_acces_fichier_destination_matrice_param);
                                       WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_destination_matrice_param, mmatrice_param_echantillon_courant);

                                    }
                                }
                            }
                        }
                        else
                        {
                            //delete(fullfile(chemin_acces_sous_dossier_param, '*.xls'));
                            var av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin");
                            int size_av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin").Length;
                            // Console.WriteLine("size "+ size_av_files_n13);
                            for (int numero_echantillon = 0; numero_echantillon < size_av_files_n13; numero_echantillon++)
                            {
                                string chemin = av_files_n13[numero_echantillon];
                                WRDfile.Method_DeleteFiles(chemin);
                            }

                            var av_files_n3 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin");
                            int size_av_files_n3 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin").Length;
                            for (int numero_echantillon = 0; numero_echantillon < size_av_files_n3; numero_echantillon++)
                            {
                                string chemin = av_files_n3[numero_echantillon];
                                chemin_acces_xy_echantillon_courant = chemin;
                                // xy_echantillon_courant = null;
                                xy_echantillon_courant = WRDfile.Method_ReadFile_Bin_XY(chemin_acces_xy_echantillon_courant);

                                // for (int h = 0; h < xy_echantillon_courant.GetLength(0); h++)
                                //for (int hh = 0; hh < xy_echantillon_courant.GetLength(1); hh++)
                                // Console.WriteLine(xy_echantillon_courant[h,hh]);
                                int num_echa = numero_echantillon + 1;
                                mmatrice_param_echantillon_courant = lecture.method_mmatrice_param_1(xy_echantillon_courant, label_du_script_choisi);
                                chemin_acces_fichier_destination_matrice_param = chemin_acces_sous_dossier_param + label_choisi_sans_lettre + "_" + classe_choisie.Substring(0, ((classe_choisie.Length))) + "_" + num_echa + ".bin";
                                // xlswrite(chemin_acces_fichier_destination_matrice_param, mmatrice_param_echantillon_courant);
                                ////WRDfile.Method_WriteFile(mmatrice_param_echantillon_courant, chemin_acces_fichier_destination_matrice_param);
                               WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_destination_matrice_param, mmatrice_param_echantillon_courant);

                            }


                        }

                    }
                    /////*///////////
                    classe_choisie = "fausse_direction";
                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie;
                    number_av_files_n2 = System.IO.Directory.GetDirectories(chemin_acces_dossier_examiner).Length;
                    av_files_n2 = System.IO.Directory.GetDirectories(chemin_acces_dossier_examiner);

                    //System.//Console.WriteLine(av_files_n2[3]);

                    for (int num_label = 0; num_label < number_av_files_n2; num_label++)
                    {
                        chemin_acces_dossier_label = av_files_n2[num_label];
                        chemin_acces_sous_dossier_courant = chemin_acces_dossier_label + "/xy_traject/";
                        chemin_acces_sous_dossier_param = chemin_acces_dossier_label + "/matrice_param/";
                        var size_dossier_label = chemin_acces_dossier_label.Length;
                        for (int ijk = 0; ijk < size_dossier_label - 2; ijk++)  ////
                        {
                            if ((chemin_acces_dossier_label[ijk] == premier_caractere) && (chemin_acces_dossier_label[ijk + 1] == deuxieme_caractere) && (chemin_acces_dossier_label[ijk + 2] == troisieme_caractere))
                            {
                                label_choisi = chemin_acces_dossier_label.Substring(ijk);
                                size_label_choisi = label_choisi.Length;
                                label_choisi_sans_lettre = label_choisi.Substring(nombre_de_caractere + 1, (size_label_choisi - 7 - nombre_de_caractere));
                            }
                        }
                        if ((type_procedure_apprentissage == 2) && (type_extracteur == 1))
                        {
                            if (label_choisi_sans_lettre.Length == label_du_script_choisi.Length)
                            {
                                if (label_choisi_sans_lettre == label_du_script_choisi)
                                {
                                    var av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin");
                                    int size_av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin").Length;
                                    // Console.WriteLine("size "+ size_av_files_n13);
                                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n13; numero_echantillon++)
                                    {
                                        string chemin = av_files_n13[numero_echantillon];
                                        WRDfile.Method_DeleteFiles(chemin);
                                    }

                                    var av_files_n3 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin");
                                    int size_av_files_n3 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin").Length;
                                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n3; numero_echantillon++)
                                    {
                                        string chemin = av_files_n3[numero_echantillon];
                                        chemin_acces_xy_echantillon_courant = chemin;
                                        // xy_echantillon_courant = null;
                                        xy_echantillon_courant = WRDfile.Method_ReadFile_Bin_XY(chemin_acces_xy_echantillon_courant);

                                        // for (int h = 0; h < xy_echantillon_courant.GetLength(0); h++)
                                        //for (int hh = 0; hh < xy_echantillon_courant.GetLength(1); hh++)
                                        // Console.WriteLine(xy_echantillon_courant[h,hh]);
                                        int num_echa = numero_echantillon + 1;
                                        mmatrice_param_echantillon_courant = lecture.method_mmatrice_param_1(xy_echantillon_courant, label_du_script_choisi);
                                        chemin_acces_fichier_destination_matrice_param = chemin_acces_sous_dossier_param + label_choisi_sans_lettre + "_" + classe_choisie.Substring(0, ((classe_choisie.Length))) + "_" + num_echa + ".bin";
                                        // xlswrite(chemin_acces_fichier_destination_matrice_param, mmatrice_param_echantillon_courant);
                                        // //WRDfile.Method_WriteFile(mmatrice_param_echantillon_courant, chemin_acces_fichier_destination_matrice_param);
                                       WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_destination_matrice_param, mmatrice_param_echantillon_courant);

                                    }
                                }
                            }
                        }
                        else
                        {
                            //delete(fullfile(chemin_acces_sous_dossier_param, '*.xls'));
                            var av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin");
                            int size_av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin").Length;
                            // Console.WriteLine("size "+ size_av_files_n13);
                            for (int numero_echantillon = 0; numero_echantillon < size_av_files_n13; numero_echantillon++)
                            {
                                string chemin = av_files_n13[numero_echantillon];
                                WRDfile.Method_DeleteFiles(chemin);
                            }

                            var av_files_n3 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin");
                            int size_av_files_n3 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin").Length;
                            for (int numero_echantillon = 0; numero_echantillon < size_av_files_n3; numero_echantillon++)
                            {
                                string chemin = av_files_n3[numero_echantillon];
                                chemin_acces_xy_echantillon_courant = chemin;
                                // xy_echantillon_courant = null;
                                xy_echantillon_courant = WRDfile.Method_ReadFile_Bin_XY(chemin_acces_xy_echantillon_courant);

                                // for (int h = 0; h < xy_echantillon_courant.GetLength(0); h++)
                                //for (int hh = 0; hh < xy_echantillon_courant.GetLength(1); hh++)
                                // Console.WriteLine(xy_echantillon_courant[h,hh]);
                                int num_echa = numero_echantillon + 1;
                                mmatrice_param_echantillon_courant = lecture.method_mmatrice_param_1(xy_echantillon_courant, label_du_script_choisi);
                                chemin_acces_fichier_destination_matrice_param = chemin_acces_sous_dossier_param + label_choisi_sans_lettre + "_" + classe_choisie.Substring(0, ((classe_choisie.Length))) + "_" + num_echa + ".bin";
                                // xlswrite(chemin_acces_fichier_destination_matrice_param, mmatrice_param_echantillon_courant);
                                ////WRDfile.Method_WriteFile(mmatrice_param_echantillon_courant, chemin_acces_fichier_destination_matrice_param);
                               WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_destination_matrice_param, mmatrice_param_echantillon_courant);

                            }

                        }

                    }


                    ////////*///////////
                    classe_choisie = "faux_ordre";
                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie;
                    number_av_files_n2 = System.IO.Directory.GetDirectories(chemin_acces_dossier_examiner).Length;
                    av_files_n2 = System.IO.Directory.GetDirectories(chemin_acces_dossier_examiner);

                    //System.//Console.WriteLine(av_files_n2[3]);

                    for (int num_label = 0; num_label < number_av_files_n2; num_label++)
                    {
                        chemin_acces_dossier_label = av_files_n2[num_label];
                        chemin_acces_sous_dossier_courant = chemin_acces_dossier_label + "/xy_traject/";
                        chemin_acces_sous_dossier_param = chemin_acces_dossier_label + "/matrice_param/";
                        var size_dossier_label = chemin_acces_dossier_label.Length;
                        for (int ijk = 0; ijk < size_dossier_label - 2; ijk++)  ////
                        {
                            if ((chemin_acces_dossier_label[ijk] == premier_caractere) && (chemin_acces_dossier_label[ijk + 1] == deuxieme_caractere) && (chemin_acces_dossier_label[ijk + 2] == troisieme_caractere))
                            {
                                label_choisi = chemin_acces_dossier_label.Substring(ijk);
                                size_label_choisi = label_choisi.Length;
                                label_choisi_sans_lettre = label_choisi.Substring(nombre_de_caractere + 1, (size_label_choisi - 7 - nombre_de_caractere));
                            }
                        }
                        if ((type_procedure_apprentissage == 2) && (type_extracteur == 1))
                        {
                            if (label_choisi_sans_lettre.Length == label_du_script_choisi.Length)
                            {
                                if (label_choisi_sans_lettre == label_du_script_choisi)
                                {
                                    var av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin");
                                    int size_av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin").Length;
                                    // Console.WriteLine("size "+ size_av_files_n13);
                                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n13; numero_echantillon++)
                                    {
                                        string chemin = av_files_n13[numero_echantillon];
                                        WRDfile.Method_DeleteFiles(chemin);
                                    }

                                    var av_files_n3 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin");
                                    int size_av_files_n3 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin").Length;
                                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n3; numero_echantillon++)
                                    {
                                        string chemin = av_files_n3[numero_echantillon];
                                        chemin_acces_xy_echantillon_courant = chemin;
                                        // xy_echantillon_courant = null;
                                        xy_echantillon_courant = WRDfile.Method_ReadFile_Bin_XY(chemin_acces_xy_echantillon_courant);

                                        // for (int h = 0; h < xy_echantillon_courant.GetLength(0); h++)
                                        //for (int hh = 0; hh < xy_echantillon_courant.GetLength(1); hh++)
                                        // Console.WriteLine(xy_echantillon_courant[h,hh]);
                                        int num_echa = numero_echantillon + 1;
                                        mmatrice_param_echantillon_courant = lecture.method_mmatrice_param_1(xy_echantillon_courant, label_du_script_choisi);
                                        chemin_acces_fichier_destination_matrice_param = chemin_acces_sous_dossier_param + label_choisi_sans_lettre + "_" + classe_choisie.Substring(0, ((classe_choisie.Length))) + "_" + num_echa + ".bin";
                                        // xlswrite(chemin_acces_fichier_destination_matrice_param, mmatrice_param_echantillon_courant);
                                       WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_destination_matrice_param, mmatrice_param_echantillon_courant);
                                    }
                                }
                            }
                        }
                        else
                        {
                            //delete(fullfile(chemin_acces_sous_dossier_param, '*.xls'));
                            var av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin");
                            int size_av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin").Length;
                            // Console.WriteLine("size "+ size_av_files_n13);
                            for (int numero_echantillon = 0; numero_echantillon < size_av_files_n13; numero_echantillon++)
                            {
                                string chemin = av_files_n13[numero_echantillon];
                                WRDfile.Method_DeleteFiles(chemin);
                            }

                            var av_files_n3 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin");
                            int size_av_files_n3 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin").Length;
                            for (int numero_echantillon = 0; numero_echantillon < size_av_files_n3; numero_echantillon++)
                            {
                                string chemin = av_files_n3[numero_echantillon];
                                chemin_acces_xy_echantillon_courant = chemin;
                                // xy_echantillon_courant = null;
                                xy_echantillon_courant = WRDfile.Method_ReadFile_Bin_XY(chemin_acces_xy_echantillon_courant);

                                // for (int h = 0; h < xy_echantillon_courant.GetLength(0); h++)
                                //for (int hh = 0; hh < xy_echantillon_courant.GetLength(1); hh++)
                                // Console.WriteLine(xy_echantillon_courant[h,hh]);
                                int num_echa = numero_echantillon + 1;
                                mmatrice_param_echantillon_courant = lecture.method_mmatrice_param_1(xy_echantillon_courant, label_du_script_choisi);
                                chemin_acces_fichier_destination_matrice_param = chemin_acces_sous_dossier_param + label_choisi_sans_lettre + "_" + classe_choisie.Substring(0, ((classe_choisie.Length))) + "_" + num_echa + ".bin";
                                // xlswrite(chemin_acces_fichier_destination_matrice_param, mmatrice_param_echantillon_courant);
                               WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_destination_matrice_param, mmatrice_param_echantillon_courant);
                            }


                        }

                    }

                    ////////*///////////
                    classe_choisie = "fausse_forme";
                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie;
                    number_av_files_n2 = System.IO.Directory.GetDirectories(chemin_acces_dossier_examiner).Length;
                    av_files_n2 = System.IO.Directory.GetDirectories(chemin_acces_dossier_examiner);

                    //System.//Console.WriteLine(av_files_n2[3]);

                    for (int num_label = 0; num_label < number_av_files_n2; num_label++)
                    {
                        chemin_acces_dossier_label = av_files_n2[num_label];
                        chemin_acces_sous_dossier_courant = chemin_acces_dossier_label + "/xy_traject/";
                        chemin_acces_sous_dossier_param = chemin_acces_dossier_label + "/matrice_param/";
                        var size_dossier_label = chemin_acces_dossier_label.Length;
                        for (int ijk = 0; ijk < size_dossier_label - 2; ijk++)  ////
                        {
                            if ((chemin_acces_dossier_label[ijk] == premier_caractere) && (chemin_acces_dossier_label[ijk + 1] == deuxieme_caractere) && (chemin_acces_dossier_label[ijk + 2] == troisieme_caractere))
                            {
                                label_choisi = chemin_acces_dossier_label.Substring(ijk);
                                size_label_choisi = label_choisi.Length;
                                label_choisi_sans_lettre = label_choisi.Substring(nombre_de_caractere + 1, (size_label_choisi - 7 - nombre_de_caractere));
                            }
                        }
                        if ((type_procedure_apprentissage == 2) && (type_extracteur == 1))
                        {
                            if (label_choisi_sans_lettre.Length == label_du_script_choisi.Length)
                            {
                                if (label_choisi_sans_lettre == label_du_script_choisi)
                                {
                                    var av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin");
                                    int size_av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin").Length;
                                    // Console.WriteLine("size "+ size_av_files_n13);
                                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n13; numero_echantillon++)
                                    {
                                        string chemin = av_files_n13[numero_echantillon];
                                        WRDfile.Method_DeleteFiles(chemin);
                                    }

                                    var av_files_n3 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin");
                                    int size_av_files_n3 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin").Length;
                                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n3; numero_echantillon++)
                                    {
                                        string chemin = av_files_n3[numero_echantillon];
                                        chemin_acces_xy_echantillon_courant = chemin;
                                        // xy_echantillon_courant = null;
                                        xy_echantillon_courant = WRDfile.Method_ReadFile_Bin_XY(chemin_acces_xy_echantillon_courant);

                                        // for (int h = 0; h < xy_echantillon_courant.GetLength(0); h++)
                                        //for (int hh = 0; hh < xy_echantillon_courant.GetLength(1); hh++)
                                        // Console.WriteLine(xy_echantillon_courant[h,hh]);
                                        int num_echa = numero_echantillon + 1;
                                        mmatrice_param_echantillon_courant = lecture.method_mmatrice_param_1(xy_echantillon_courant, label_du_script_choisi);
                                        chemin_acces_fichier_destination_matrice_param = chemin_acces_sous_dossier_param + label_choisi_sans_lettre + "_" + classe_choisie.Substring(0, ((classe_choisie.Length))) + "_" + num_echa + ".bin";
                                        // xlswrite(chemin_acces_fichier_destination_matrice_param, mmatrice_param_echantillon_courant);
                                       WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_destination_matrice_param, mmatrice_param_echantillon_courant);
                                    }
                                }
                            }
                        }
                        else
                        {
                            //delete(fullfile(chemin_acces_sous_dossier_param, '*.xls'));
                            //WRDfile.Method_DeleteFiles(chemin_acces_sous_dossier_param);
                            var av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin");
                            int size_av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin").Length;
                            // Console.WriteLine("size "+ size_av_files_n13);
                            for (int numero_echantillon = 0; numero_echantillon < size_av_files_n13; numero_echantillon++)
                            {
                                string chemin = av_files_n13[numero_echantillon];
                                WRDfile.Method_DeleteFiles(chemin);
                            }

                            var av_files_n3 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin");
                            int size_av_files_n3 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin").Length;
                            for (int numero_echantillon = 0; numero_echantillon < size_av_files_n3; numero_echantillon++)
                            {
                                string chemin = av_files_n3[numero_echantillon];
                                chemin_acces_xy_echantillon_courant = chemin;
                                // xy_echantillon_courant = null;
                                xy_echantillon_courant = WRDfile.Method_ReadFile_Bin_XY(chemin_acces_xy_echantillon_courant);

                                // for (int h = 0; h < xy_echantillon_courant.GetLength(0); h++)
                                //for (int hh = 0; hh < xy_echantillon_courant.GetLength(1); hh++)
                                // Console.WriteLine(xy_echantillon_courant[h,hh]);
                                int num_echa = numero_echantillon + 1;
                                mmatrice_param_echantillon_courant = lecture.method_mmatrice_param_1(xy_echantillon_courant, label_du_script_choisi);
                                chemin_acces_fichier_destination_matrice_param = chemin_acces_sous_dossier_param + label_choisi_sans_lettre + "_" + classe_choisie.Substring(0, ((classe_choisie.Length))) + "_" + num_echa + ".bin";
                                // xlswrite(chemin_acces_fichier_destination_matrice_param, mmatrice_param_echantillon_courant);
                               WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_destination_matrice_param, mmatrice_param_echantillon_courant);
                            }
                        }

                    }

                    // ***************************** selection des echontillons modeles ************

                    classe_choisie = "modeles";
                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie;
                    number_av_files_n2 = System.IO.Directory.GetDirectories(chemin_acces_dossier_examiner).Length;
                    av_files_n2 = System.IO.Directory.GetDirectories(chemin_acces_dossier_examiner);
                    for (int numero_label = 0; numero_label < number_av_files_n2; numero_label++)
                    {
                        chemin_acces_dossier_label = av_files_n2[numero_label];
                        for (int ijk = 0; ijk < number_av_files_n2 - 2; ijk++)  ////
                        {
                            if ((chemin_acces_dossier_label[ijk] == premier_caractere) && (chemin_acces_dossier_label[ijk + 1] == deuxieme_caractere) && (chemin_acces_dossier_label[ijk + 2] == troisieme_caractere))
                            {
                                label_choisi = chemin_acces_dossier_label.Substring(ijk);
                                size_label_choisi = label_choisi.Length;
                                label_choisi_sans_lettre = label_choisi.Substring(nombre_de_caractere + 1, (size_label_choisi - 7 - nombre_de_caractere));
                            }
                        }
                        label = label_choisi_sans_lettre;
                        classe_des_echantillons_candidats = "correcte";
                        classe_des_echantillons_candidats = "modeles";
                        classe_de_l_ensemble_des_echantillons_references = "correcte";
                        if (label.Length == label_du_script_choisi.Length)
                        {
                            if ((label == label_du_script_choisi) || (type_procedure_apprentissage == 1))
                            {
                                /*  WRDfile.Method_DeleteFiles(chemin_acces_dossier_label + "/" + "Rangs_ordonnes_des_echantillons_modeles_AP.txt");
                                  WRDfile.Method_DeleteFiles(chemin_acces_dossier_label + "/" + "Rangs_ordonnes_des_echantillons_modeles_AS.txt");
                                  Rangs_ordonnes_des_echantillons_modeles_AP = Selection_echantillon.Method_Rangs_ordonnes_des_echantillons_modeles_AP(label, type_de_script, classe_des_echantillons_candidats, classe_de_l_ensemble_des_echantillons_references);
                                  Rangs_ordonnes_des_echantillons_modeles_AS = Selection_echantillon.Method_Rangs_ordonnes_des_echantillons_modeles_AS(label, type_de_script, classe_des_echantillons_candidats, classe_de_l_ensemble_des_echantillons_references);
                                  //WRDfile.Method_WriteFile_Int(Rangs_ordonnes_des_echantillons_modeles_AP, chemin_acces_dossier_label + "/" + "Rangs_ordonnes_des_echantillons_modeles_AP.txt");
                                  //WRDfile.Method_WriteFile_Int(Rangs_ordonnes_des_echantillons_modeles_AS, chemin_acces_dossier_label + "/" + "Rangs_ordonnes_des_echantillons_modeles_AS.txt");
                                  */
                            }

                        }
                        else if (type_procedure_apprentissage == 1)
                        {
                            /*  WRDfile.Method_DeleteFiles(chemin_acces_dossier_label + "/" + "Rangs_ordonnes_des_echantillons_modeles_AP.txt");
                               WRDfile.Method_DeleteFiles(chemin_acces_dossier_label + "/" + "Rangs_ordonnes_des_echantillons_modeles_AS.txt");
                               Rangs_ordonnes_des_echantillons_modeles_AP = Selection_echantillon.Method_Rangs_ordonnes_des_echantillons_modeles_AP(label, type_de_script, classe_des_echantillons_candidats, classe_de_l_ensemble_des_echantillons_references);
                               Rangs_ordonnes_des_echantillons_modeles_AS = Selection_echantillon.Method_Rangs_ordonnes_des_echantillons_modeles_AS(label, type_de_script, classe_des_echantillons_candidats, classe_de_l_ensemble_des_echantillons_references);
                               //WRDfile.Method_WriteFile_Int(Rangs_ordonnes_des_echantillons_modeles_AP, chemin_acces_dossier_label + "/" + "Rangs_ordonnes_des_echantillons_modeles_AP.txt");
                               //WRDfile.Method_WriteFile_Int(Rangs_ordonnes_des_echantillons_modeles_AS, chemin_acces_dossier_label + "/" + "Rangs_ordonnes_des_echantillons_modeles_AS.txt");
                           */
                        }
                    }

                }
                // *****************************************************************************
                // ***************************** calcul des distances BEm **************************

                if ((type_procedure_apprentissage == 1) || ((type_procedure_apprentissage == 2) && (type_extracteur == 1)) || ((type_procedure_apprentissage == 3) && (type_approche == 1)))
                {
                    classe_choisie = "correcte";
                    //Console.WriteLine("label11111");
                    //Console.WriteLine(label_du_script_choisi);

                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/";//+ type_de_script_p + "_" + label_du_script_choisi + "_isole/"; 
                    var av_files_n2 = System.IO.Directory.GetDirectories(chemin_acces_dossier_examiner);
                    //Console.WriteLine("av_files_n2");
                    //Console.WriteLine(av_files_n2.Length);
                    string[] dirs = System.IO.Directory.GetDirectories(chemin_acces_dossier_examiner);
                    //Console.WriteLine("The number of directories starting with p is {0}.", dirs.Length);
                    foreach (string dir in dirs)
                    {
                        //Console.WriteLine(dir);
                    }
                    for (int numero_label = 0; numero_label < (av_files_n2.Length); numero_label++)
                    {

                        chemin_acces_dossier_label = av_files_n2[numero_label];
                        //Console.WriteLine("chemin acces_dossier label");
                        //Console.WriteLine(chemin_acces_dossier_label);


                        chemin_acces_sous_dossier_param = chemin_acces_dossier_label + "/matrice_param/";
                        int size_dossier_label = chemin_acces_dossier_label.Length;
                        for (int ijk = 0; ijk < size_dossier_label - 2; ijk++)  ////
                        {
                            if ((chemin_acces_dossier_label[ijk] == premier_caractere) && (chemin_acces_dossier_label[ijk + 1] == deuxieme_caractere) && (chemin_acces_dossier_label[ijk + 2] == troisieme_caractere))
                            {
                                label_choisi = chemin_acces_dossier_label.Substring(ijk);
                                size_label_choisi = label_choisi.Length;
                                label_choisi_sans_lettre = label_choisi.Substring(nombre_de_caractere + 1, (size_label_choisi - 7 - nombre_de_caractere));
                                //Console.WriteLine("label choisi"+ label_choisi +"taille" + size_label_choisi+"label sans lettre"+ label_choisi_sans_lettre);
                            }
                        }

                        drapeau_autorisation_apprentissage_partiel = 1;
                        if ((type_procedure_apprentissage == 2) && (type_extracteur == 1))
                        {
                            drapeau_autorisation_apprentissage_partiel = 0;
                            if (label_choisi_sans_lettre.Length == label_du_script_choisi.Length)
                            {
                                if (label_choisi_sans_lettre == label_du_script_choisi)
                                    drapeau_autorisation_apprentissage_partiel = 1;
                            }
                        }
                        if (drapeau_autorisation_apprentissage_partiel == 1)
                        {
                            chemin_acces_dossier_label_modele = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole";
                            //  Rangs_ordonnes_des_echantillons_modeles_AP = WRDfile.Method_ReadFile_Int(chemin_acces_dossier_label_modele + "/" + "Rangs_ordonnes_des_echantillons_modeles_AP.txt");
                            //  Rangs_ordonnes_des_echantillons_modeles_AS = WRDfile.Method_ReadFile_Int(chemin_acces_dossier_label_modele + "/" + "Rangs_ordonnes_des_echantillons_modeles_AS.txt");

                            /* num_echantillon_modele_1_AP = Rangs_ordonnes_des_echantillons_modeles_AP[0];
                             num_echantillon_modele_2_AP = Rangs_ordonnes_des_echantillons_modeles_AP[1];
                             num_echantillon_modele_3_AP = Rangs_ordonnes_des_echantillons_modeles_AP[2];
                             //num_echantillon_modele_3_AP = 2;
                             num_echantillon_modele_1_AS = Rangs_ordonnes_des_echantillons_modeles_AS[0];
                             num_echantillon_modele_2_AS = Rangs_ordonnes_des_echantillons_modeles_AS[1];
                             num_echantillon_modele_3_AS = Rangs_ordonnes_des_echantillons_modeles_AS[2];*/
                            num_echantillon_modele_1_AP = 1;
                            num_echantillon_modele_2_AP = 2;
                            num_echantillon_modele_3_AP = 3;
                            num_echantillon_modele_1_AS = 1;
                            num_echantillon_modele_2_AS = 2;
                            num_echantillon_modele_3_AS = 3;
                            //num_echantillon_modele_3_AS = 2;
                            chemin_acces_param_echantillon_modele_1_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/matrice_param/" + label_choisi_sans_lettre + "_modele_" + num_echantillon_modele_1_AP + ".bin";
                            chemin_acces_param_echantillon_modele_2_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/matrice_param/" + label_choisi_sans_lettre + "_modele_" + num_echantillon_modele_2_AP + ".bin";
                            chemin_acces_param_echantillon_modele_3_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/matrice_param/" + label_choisi_sans_lettre + "_modele_" + num_echantillon_modele_3_AP + ".bin";
                            chemin_acces_param_echantillon_modele_1_AS = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/matrice_param/" + label_choisi_sans_lettre + "_modele_" + num_echantillon_modele_1_AS + ".bin";
                            chemin_acces_param_echantillon_modele_2_AS = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/matrice_param/" + label_choisi_sans_lettre + "_modele_" + num_echantillon_modele_2_AS + ".bin";
                            chemin_acces_param_echantillon_modele_3_AS = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/matrice_param/" + label_choisi_sans_lettre + "_modele_" + num_echantillon_modele_3_AS + ".bin";

                            mmatrice_param_echantillon_modele_1_AP = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_1_AP);
                            mmatrice_param_echantillon_modele_2_AP = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_2_AP);
                            mmatrice_param_echantillon_modele_3_AP = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_3_AP);

                            mmatrice_param_echantillon_modele_1_AS = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_1_AS);
                            mmatrice_param_echantillon_modele_2_AS = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_2_AS);
                            mmatrice_param_echantillon_modele_3_AS = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_3_AS);


                            var size_av_files_n4 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin").Length;
                            var av_files_n4 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin");
                            mm_distance_AP = new double[size_av_files_n4, 3];
                            mm_distance_AS = new double[size_av_files_n4, 3];
                            for (int numero_echantillon = 0; numero_echantillon < size_av_files_n4; numero_echantillon++)
                            {
                                chemin_acces_fichier_param_echantillon_courant = av_files_n4[numero_echantillon];
                                mmatrice_param_echantillon_courant = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_fichier_param_echantillon_courant);

                                double[] mmatrice_echantillon_test1 = new double[mmatrice_param_echantillon_courant.GetLength(1)];

                                for (var ii = 0; ii < mmatrice_param_echantillon_courant.GetLength(1); ii++)
                                    mmatrice_echantillon_test1[ii] = mmatrice_param_echantillon_courant[6, ii];

                                mat_cod_test = codage_p1.method_codage_p(mmatrice_echantillon_test1, mmatrice_param_echantillon_courant);
                                mmatrice_echantillon_modele_1_AS = new double[mmatrice_param_echantillon_modele_1_AS.GetLength(1)];
                                mmatrice_echantillon_modele_2_AS = new double[mmatrice_param_echantillon_modele_2_AS.GetLength(1)];
                                mmatrice_echantillon_modele_3_AS = new double[mmatrice_param_echantillon_modele_3_AS.GetLength(1)];

                                for (var ii = 0; ii < mmatrice_param_echantillon_modele_1_AS.GetLength(1); ii++)
                                    mmatrice_echantillon_modele_1_AS[ii] = mmatrice_param_echantillon_modele_1_AS[6, ii];
                                mat_cod_1 = codage_p1.method_codage_p(mmatrice_echantillon_modele_1_AS, mmatrice_param_echantillon_modele_1_AS);

                                for (var ii = 0; ii < mmatrice_param_echantillon_modele_2_AS.GetLength(1); ii++)
                                    mmatrice_echantillon_modele_2_AS[ii] = mmatrice_param_echantillon_modele_2_AS[6, ii];
                                mat_cod_2 = codage_p1.method_codage_p(mmatrice_echantillon_modele_2_AS, mmatrice_param_echantillon_modele_2_AS);

                                for (var ii = 0; ii < mmatrice_param_echantillon_modele_3_AS.GetLength(1); ii++)
                                    mmatrice_echantillon_modele_3_AS[ii] = mmatrice_param_echantillon_modele_3_AS[6, ii];
                                mat_cod_3 = codage_p1.method_codage_p(mmatrice_echantillon_modele_3_AS, mmatrice_param_echantillon_modele_3_AS);

                                distance_AS_direction = comparaison_structurelle.method_Calcul_score_CS_direction(mat_cod_test, mat_cod_1, mat_cod_2, mat_cod_3);
                                distance_AS_ordre = comparaison_structurelle.method_Calcul_score_CS_ordre(mat_cod_test, mat_cod_1, mat_cod_2, mat_cod_3);
                                distance_AS_forme = comparaison_structurelle.method_Calcul_score_CS_forme(mat_cod_test, mat_cod_1, mat_cod_2, mat_cod_3, label_du_script_choisi, path, autorisation_distance_test_faux);

                                distance_AP_direction = direction_score.method_estimation_direction_score(mmatrice_param_echantillon_courant, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_choisi_sans_lettre);
                                distance_AP_ordre = ordre_score.method_estimation_ordre_score(mmatrice_param_echantillon_courant, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_choisi_sans_lettre);
                                distance_AP_forme = forme_score.method_Module_estimation_forme_score(mmatrice_param_echantillon_courant, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_choisi_sans_lettre, path, autorisation_distance_test_faux);

                                vect_distance_AP[0] = distance_AP_direction;
                                vect_distance_AP[1] = distance_AP_ordre;
                                vect_distance_AP[2] = distance_AP_forme;

                                vect_distance_AS[0] = distance_AS_direction;
                                vect_distance_AS[1] = distance_AS_ordre;
                                vect_distance_AS[2] = distance_AS_forme;
                                mm_distance_AP[numero_echantillon, 0] = vect_distance_AP[0];
                                mm_distance_AP[numero_echantillon, 1] = vect_distance_AP[1];
                                mm_distance_AP[numero_echantillon, 2] = vect_distance_AP[2];

                                mm_distance_AS[numero_echantillon, 0] = vect_distance_AS[0];
                                mm_distance_AS[numero_echantillon, 1] = vect_distance_AS[1];
                                mm_distance_AS[numero_echantillon, 2] = vect_distance_AS[2];

                            }

                            chemin_acces_dossier_distance = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie;
                            chemin_acces_fichier_distances_parametriques = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie + "/distance_parametrique.bin";
                            chemin_acces_fichier_distances_parametriques_0 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie + "/distance_parametrique_0.bin";

                            string chemin_acces_fichier_distances_parametriques2 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie + "/distance_parametrique.txt";
                            string chemin_acces_fichier_distances_structurelles2 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie + "/distance_structurelle.txt";

                            chemin_acces_fichier_distances_structurelles = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie + "/distance_structurelle.bin";
                            chemin_acces_fichier_distances_structurelles_0 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie + "/distance_structurelle_0.bin";

                            if (!Directory.Exists(chemin_acces_dossier_distance))
                            { Directory.CreateDirectory(chemin_acces_dossier_distance); }
                            var av_files_n = System.IO.Directory.GetFiles(chemin_acces_dossier_distance, "*.bin").Length;

                            if (autorisation_distance_test_faux == 0)
                            {
                                WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_parametriques_0);
                               WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_parametriques_0, mm_distance_AP);

                                //WRDfile.Method_WriteFile(mm_distance_AP, chemin_acces_fichier_distances_parametriques2);

                                WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_structurelles_0);
                               WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_structurelles_0, mm_distance_AS);
                                //WRDfile.Method_WriteFile(mm_distance_AS, chemin_acces_fichier_distances_structurelles2);

                            }


                            WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_parametriques);
                           WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_parametriques, mm_distance_AP);

                            //WRDfile.Method_WriteFile(mm_distance_AP, chemin_acces_fichier_distances_parametriques2);

                            WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_structurelles);
                           WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_structurelles, mm_distance_AS);
                            //WRDfile.Method_WriteFile(mm_distance_AS, chemin_acces_fichier_distances_structurelles2);

                        }
                    }

                    /*******************///
                    classe_choisie = "fausse_direction";
                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie;
                    av_files_n2 = System.IO.Directory.GetDirectories(chemin_acces_dossier_examiner);
                    for (int numero_label = 0; numero_label < (av_files_n2.Length); numero_label++)
                    {
                        chemin_acces_dossier_label = av_files_n2[numero_label];
                        chemin_acces_sous_dossier_param = chemin_acces_dossier_label + "/matrice_param/";
                        int size_dossier_label = chemin_acces_dossier_label.Length;
                        for (int ijk = 0; ijk < size_dossier_label - 2; ijk++)  ////
                        {
                            if ((chemin_acces_dossier_label[ijk] == premier_caractere) && (chemin_acces_dossier_label[ijk + 1] == deuxieme_caractere) && (chemin_acces_dossier_label[ijk + 2] == troisieme_caractere))
                            {
                                label_choisi = chemin_acces_dossier_label.Substring(ijk);
                                size_label_choisi = label_choisi.Length;
                                label_choisi_sans_lettre = label_choisi.Substring(nombre_de_caractere + 1, (size_label_choisi - 7 - nombre_de_caractere));
                            }
                        }

                        drapeau_autorisation_apprentissage_partiel = 1;
                        if ((type_procedure_apprentissage == 2) && (type_extracteur == 1))
                        {
                            drapeau_autorisation_apprentissage_partiel = 0;
                            if (label_choisi_sans_lettre.Length == label_du_script_choisi.Length)
                            {
                                if (label_choisi_sans_lettre == label_du_script_choisi)
                                    drapeau_autorisation_apprentissage_partiel = 1;
                            }
                        }
                        if (drapeau_autorisation_apprentissage_partiel == 1)
                        {

                            /* chemin_acces_dossier_label_modele = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole";
                             Rangs_ordonnes_des_echantillons_modeles_AP = WRDfile.Method_ReadFile_Int(chemin_acces_dossier_label_modele + "/" + "Rangs_ordonnes_des_echantillons_modeles_AP.txt");
                             Rangs_ordonnes_des_echantillons_modeles_AS = WRDfile.Method_ReadFile_Int(chemin_acces_dossier_label_modele + "/" + "Rangs_ordonnes_des_echantillons_modeles_AS.txt");

                             num_echantillon_modele_1_AP = Rangs_ordonnes_des_echantillons_modeles_AP[0];
                             num_echantillon_modele_2_AP = Rangs_ordonnes_des_echantillons_modeles_AP[1];
                             //num_echantillon_modele_3_AP = Rangs_ordonnes_des_echantillons_modeles_AP[2];
                             num_echantillon_modele_3_AP = 2;
                             num_echantillon_modele_1_AS = Rangs_ordonnes_des_echantillons_modeles_AS[0];
                             num_echantillon_modele_2_AS = Rangs_ordonnes_des_echantillons_modeles_AS[1];
                             //num_echantillon_modele_3_AS = Rangs_ordonnes_des_echantillons_modeles_AS[2];
                             num_echantillon_modele_3_AS = 2;*/
                            num_echantillon_modele_1_AP = 1;
                            num_echantillon_modele_2_AP = 2;
                            num_echantillon_modele_3_AP = 3;
                            num_echantillon_modele_1_AS = 1;
                            num_echantillon_modele_2_AS = 2;
                            num_echantillon_modele_3_AS = 3;
                            chemin_acces_param_echantillon_modele_1_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/matrice_param/" + label_choisi_sans_lettre + "_modele_" + num_echantillon_modele_1_AP + ".bin";
                            chemin_acces_param_echantillon_modele_2_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/matrice_param/" + label_choisi_sans_lettre + "_modele_" + num_echantillon_modele_2_AP + ".bin";
                            chemin_acces_param_echantillon_modele_3_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/matrice_param/" + label_choisi_sans_lettre + "_modele_" + num_echantillon_modele_3_AP + ".bin";
                            chemin_acces_param_echantillon_modele_1_AS = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/matrice_param/" + label_choisi_sans_lettre + "_modele_" + num_echantillon_modele_1_AS + ".bin";
                            chemin_acces_param_echantillon_modele_2_AS = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/matrice_param/" + label_choisi_sans_lettre + "_modele_" + num_echantillon_modele_2_AS + ".bin";
                            chemin_acces_param_echantillon_modele_3_AS = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/matrice_param/" + label_choisi_sans_lettre + "_modele_" + num_echantillon_modele_3_AS + ".bin";

                            mmatrice_param_echantillon_modele_1_AP = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_1_AP);
                            mmatrice_param_echantillon_modele_2_AP = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_2_AP);
                            mmatrice_param_echantillon_modele_3_AP = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_3_AP);

                            mmatrice_param_echantillon_modele_1_AS = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_1_AS);
                            mmatrice_param_echantillon_modele_2_AS = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_2_AS);
                            mmatrice_param_echantillon_modele_3_AS = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_3_AS);

                            var size_av_files_n4 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin").Length;
                            var av_files_n4 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin");
                            mm_distance_AP = new double[size_av_files_n4, 3];
                            mm_distance_AS = new double[size_av_files_n4, 3];
                            double[] mmatrice_echantillon_modele_11_AS = new double[mmatrice_param_echantillon_modele_1_AS.GetLength(1)];
                            double[] mmatrice_echantillon_modele_12_AS = new double[mmatrice_param_echantillon_modele_2_AS.GetLength(1)];
                            double[] mmatrice_echantillon_modele_13_AS = new double[mmatrice_param_echantillon_modele_3_AS.GetLength(1)];
                            for (int numero_echantillon = 0; numero_echantillon < size_av_files_n4; numero_echantillon++)
                            {
                                chemin_acces_fichier_param_echantillon_courant = av_files_n4[numero_echantillon];
                                mmatrice_param_echantillon_courant = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_fichier_param_echantillon_courant);
                                int t_m = mmatrice_param_echantillon_courant.GetLength(1);
                                double[] yy = new double[t_m];
                                for (var ii1 = 0; ii1 < yy.Length; ii1++)
                                    yy[ii1] = mmatrice_param_echantillon_courant[6, ii1];

                                mat_cod_test = codage_p1.method_codage_p(yy, mmatrice_param_echantillon_courant);
                                for (var ii = 0; ii < mmatrice_param_echantillon_modele_1_AS.GetLength(1); ii++)
                                    mmatrice_echantillon_modele_11_AS[ii] = mmatrice_param_echantillon_modele_1_AS[6, ii];
                                mat_cod_1 = codage_p1.method_codage_p(mmatrice_echantillon_modele_11_AS, mmatrice_param_echantillon_modele_1_AS);

                                for (var ii = 0; ii < mmatrice_param_echantillon_modele_2_AS.GetLength(1); ii++)
                                    mmatrice_echantillon_modele_12_AS[ii] = mmatrice_param_echantillon_modele_2_AS[6, ii];
                                mat_cod_2 = codage_p1.method_codage_p(mmatrice_echantillon_modele_12_AS, mmatrice_param_echantillon_modele_2_AS);

                                for (var ii = 0; ii < mmatrice_param_echantillon_modele_3_AS.GetLength(1); ii++)
                                    mmatrice_echantillon_modele_13_AS[ii] = mmatrice_param_echantillon_modele_3_AS[6, ii];
                                mat_cod_3 = codage_p1.method_codage_p(mmatrice_echantillon_modele_13_AS, mmatrice_param_echantillon_modele_3_AS);

                                distance_AS_direction = comparaison_structurelle.method_Calcul_score_CS_direction(mat_cod_test, mat_cod_1, mat_cod_2, mat_cod_3);
                                distance_AS_ordre = comparaison_structurelle.method_Calcul_score_CS_ordre(mat_cod_test, mat_cod_1, mat_cod_2, mat_cod_3);
                                distance_AS_forme = comparaison_structurelle.method_Calcul_score_CS_forme(mat_cod_test, mat_cod_1, mat_cod_2, mat_cod_3, label_du_script_choisi, path, autorisation_distance_test_faux);

                                distance_AP_direction = direction_score.method_estimation_direction_score(mmatrice_param_echantillon_courant, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_choisi_sans_lettre);
                                distance_AP_ordre = ordre_score.method_estimation_ordre_score(mmatrice_param_echantillon_courant, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_choisi_sans_lettre);
                                distance_AP_forme = forme_score.method_Module_estimation_forme_score(mmatrice_param_echantillon_courant, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_choisi_sans_lettre, path, autorisation_distance_test_faux);



                                vect_distance_AP[0] = distance_AP_direction;
                                Console.WriteLine("Distance AP direction" + vect_distance_AP[0]);
                                vect_distance_AP[1] = distance_AP_ordre;
                                Console.WriteLine("Distance AP ordre" + vect_distance_AP[1]);
                                vect_distance_AP[2] = distance_AP_forme;
                                Console.WriteLine("Distance AP forme" + vect_distance_AP[2]);

                                vect_distance_AS[0] = distance_AS_direction;
                                vect_distance_AS[1] = distance_AS_ordre;
                                vect_distance_AS[2] = distance_AS_forme;
                                mm_distance_AP[numero_echantillon, 0] = vect_distance_AP[0];
                                mm_distance_AP[numero_echantillon, 1] = vect_distance_AP[1];
                                mm_distance_AP[numero_echantillon, 2] = vect_distance_AP[2];

                                mm_distance_AS[numero_echantillon, 0] = vect_distance_AS[0];
                                mm_distance_AS[numero_echantillon, 1] = vect_distance_AS[1];
                                mm_distance_AS[numero_echantillon, 2] = vect_distance_AS[2];

                            }

                            chemin_acces_dossier_distance = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie;
                            if (!Directory.Exists(chemin_acces_dossier_distance))
                            { Directory.CreateDirectory(chemin_acces_dossier_distance); }

                            chemin_acces_fichier_distances_parametriques = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie + "/distance_parametrique.bin";
                            chemin_acces_fichier_distances_structurelles = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie + "/distance_structurelle.bin";
                            string chemin_acces_fichier_distances_parametriques1 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie + "/distance_parametrique.txt";
                            string chemin_acces_fichier_distances_structurelles1 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie + "/distance_structurelle.txt";

                            chemin_acces_fichier_distances_parametriques_0 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie + "/distance_parametrique_0.bin";
                            chemin_acces_fichier_distances_structurelles_0 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie + "/distance_structurelle_0.bin";


                            if (autorisation_distance_test_faux == 0)
                            {
                                WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_parametriques_0);
                               WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_parametriques_0, mm_distance_AP);

                                //WRDfile.Method_WriteFile(mm_distance_AP, chemin_acces_fichier_distances_parametriques1);

                                WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_structurelles_0);
                               WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_structurelles_0, mm_distance_AS);
                               //WRDfile.Method_WriteFile(mm_distance_AS, chemin_acces_fichier_distances_structurelles1);

                            }


                            WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_parametriques);
                           WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_parametriques, mm_distance_AP);

                            WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_structurelles);
                           WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_structurelles, mm_distance_AS);
                            string chemin_acces_fichier_distances_parametriques2 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie + "/distance_parametrique.txt";
                            //WRDfile.Method_WriteFile(mm_distance_AP, chemin_acces_fichier_distances_parametriques1);
                            //WRDfile.Method_WriteFile(mm_distance_AS, chemin_acces_fichier_distances_structurelles1);

                        }
                    }

                    ////////////////
                    classe_choisie = "fausse_forme";
                    //mm_distance_AP =[];
                    //mm_distance_AS = [];
                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie;
                    av_files_n2 = System.IO.Directory.GetDirectories(chemin_acces_dossier_examiner);
                    for (int numero_label = 0; numero_label < (av_files_n2.Length); numero_label++)
                    {
                        chemin_acces_dossier_label = av_files_n2[numero_label];
                        chemin_acces_sous_dossier_param = chemin_acces_dossier_label + "/matrice_param/";
                        int size_dossier_label = chemin_acces_dossier_label.Length;
                        for (int ijk = 0; ijk < size_dossier_label - 2; ijk++)  ////
                        {
                            if ((chemin_acces_dossier_label[ijk] == premier_caractere) && (chemin_acces_dossier_label[ijk + 1] == deuxieme_caractere) && (chemin_acces_dossier_label[ijk + 2] == troisieme_caractere))
                            {
                                label_choisi = chemin_acces_dossier_label.Substring(ijk);
                                size_label_choisi = label_choisi.Length;
                                label_choisi_sans_lettre = label_choisi.Substring(nombre_de_caractere + 1, (size_label_choisi - 7 - nombre_de_caractere));
                            }
                        }

                        drapeau_autorisation_apprentissage_partiel = 1;
                        if ((type_procedure_apprentissage == 2) && (type_extracteur == 1))
                        {
                            drapeau_autorisation_apprentissage_partiel = 0;
                            if (label_choisi_sans_lettre.Length == label_du_script_choisi.Length)
                            {
                                if (label_choisi_sans_lettre == label_du_script_choisi)
                                    drapeau_autorisation_apprentissage_partiel = 1;
                            }
                        }
                        if (drapeau_autorisation_apprentissage_partiel == 1)
                        {
                            /* chemin_acces_dossier_label_modele = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole";
                             Rangs_ordonnes_des_echantillons_modeles_AP = WRDfile.Method_ReadFile_Int(chemin_acces_dossier_label_modele + "/" + "Rangs_ordonnes_des_echantillons_modeles_AP.txt");
                             Rangs_ordonnes_des_echantillons_modeles_AS = WRDfile.Method_ReadFile_Int(chemin_acces_dossier_label_modele + "/" + "Rangs_ordonnes_des_echantillons_modeles_AS.txt");

                             num_echantillon_modele_1_AP = Rangs_ordonnes_des_echantillons_modeles_AP[0];
                             num_echantillon_modele_2_AP = Rangs_ordonnes_des_echantillons_modeles_AP[1];
                             num_echantillon_modele_3_AP = Rangs_ordonnes_des_echantillons_modeles_AP[2];
                             //num_echantillon_modele_3_AP = 2;
                             num_echantillon_modele_1_AS = Rangs_ordonnes_des_echantillons_modeles_AS[0];
                             num_echantillon_modele_2_AS = Rangs_ordonnes_des_echantillons_modeles_AS[1];
                             num_echantillon_modele_3_AS = Rangs_ordonnes_des_echantillons_modeles_AS[2];
                             //num_echantillon_modele_3_AS =2;*/
                            num_echantillon_modele_1_AP = 1;
                            num_echantillon_modele_2_AP = 2;
                            num_echantillon_modele_3_AP = 3;
                            num_echantillon_modele_1_AS = 1;
                            num_echantillon_modele_2_AS = 2;
                            num_echantillon_modele_3_AS = 3;
                            chemin_acces_param_echantillon_modele_1_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/matrice_param/" + label_choisi_sans_lettre + "_modele_" + num_echantillon_modele_1_AP + ".bin";
                            chemin_acces_param_echantillon_modele_2_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/matrice_param/" + label_choisi_sans_lettre + "_modele_" + num_echantillon_modele_2_AP + ".bin";
                            chemin_acces_param_echantillon_modele_3_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/matrice_param/" + label_choisi_sans_lettre + "_modele_" + num_echantillon_modele_3_AP + ".bin";
                            chemin_acces_param_echantillon_modele_1_AS = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/matrice_param/" + label_choisi_sans_lettre + "_modele_" + num_echantillon_modele_1_AS + ".bin";
                            chemin_acces_param_echantillon_modele_2_AS = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/matrice_param/" + label_choisi_sans_lettre + "_modele_" + num_echantillon_modele_2_AS + ".bin";
                            chemin_acces_param_echantillon_modele_3_AS = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/matrice_param/" + label_choisi_sans_lettre + "_modele_" + num_echantillon_modele_3_AS + ".bin";

                            mmatrice_param_echantillon_modele_1_AP = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_1_AP);
                            mmatrice_param_echantillon_modele_2_AP = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_2_AP);
                            mmatrice_param_echantillon_modele_3_AP = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_3_AP);

                            mmatrice_param_echantillon_modele_1_AS = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_1_AS);
                            mmatrice_param_echantillon_modele_2_AS = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_2_AS);
                            mmatrice_param_echantillon_modele_3_AS = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_3_AS);

                            var size_av_files_n4 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin").Length;
                            var av_files_n4 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin");
                            mm_distance_AP = new double[size_av_files_n4, 3];
                            mm_distance_AS = new double[size_av_files_n4, 3];
                            double[] mmatrice_echantillon_modele_111_AS = new double[mmatrice_param_echantillon_modele_1_AS.GetLength(1)];
                            double[] mmatrice_echantillon_modele_122_AS = new double[mmatrice_param_echantillon_modele_2_AS.GetLength(1)];
                            double[] mmatrice_echantillon_modele_133_AS = new double[mmatrice_param_echantillon_modele_3_AS.GetLength(1)];
                            for (int numero_echantillon = 0; numero_echantillon < size_av_files_n4; numero_echantillon++)
                            {
                                chemin_acces_fichier_param_echantillon_courant = av_files_n4[numero_echantillon];
                                mmatrice_param_echantillon_courant = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_fichier_param_echantillon_courant);
                                int t_m1 = mmatrice_param_echantillon_courant.GetLength(1);
                                double[] yy2 = new double[t_m1];
                                for (var ii = 0; ii < mmatrice_param_echantillon_courant.GetLength(1); ii++)
                                    yy2[ii] = mmatrice_param_echantillon_courant[6, ii];

                                mat_cod_test = codage_p1.method_codage_p(yy2, mmatrice_param_echantillon_courant);
                                // mmatrice_echantillon_modele_1 = new double[mmatrice_param_echantillon_modele_1.GetLength(1)];

                                for (var ii = 0; ii < mmatrice_param_echantillon_modele_1_AS.GetLength(1); ii++)
                                    mmatrice_echantillon_modele_111_AS[ii] = mmatrice_param_echantillon_modele_1_AS[6, ii];
                                mat_cod_1 = codage_p1.method_codage_p(mmatrice_echantillon_modele_111_AS, mmatrice_param_echantillon_modele_1_AS);

                                for (var ii = 0; ii < mmatrice_param_echantillon_modele_2_AS.GetLength(1); ii++)
                                    mmatrice_echantillon_modele_122_AS[ii] = mmatrice_param_echantillon_modele_2_AS[6, ii];
                                mat_cod_2 = codage_p1.method_codage_p(mmatrice_echantillon_modele_122_AS, mmatrice_param_echantillon_modele_2_AS);

                                for (var ii = 0; ii < mmatrice_param_echantillon_modele_3_AS.GetLength(1); ii++)
                                    mmatrice_echantillon_modele_133_AS[ii] = mmatrice_param_echantillon_modele_3_AS[6, ii];
                                mat_cod_3 = codage_p1.method_codage_p(mmatrice_echantillon_modele_133_AS, mmatrice_param_echantillon_modele_3_AS);

                                distance_AS_direction = comparaison_structurelle.method_Calcul_score_CS_direction(mat_cod_test, mat_cod_1, mat_cod_2, mat_cod_3);
                                distance_AS_ordre = comparaison_structurelle.method_Calcul_score_CS_ordre(mat_cod_test, mat_cod_1, mat_cod_2, mat_cod_3);
                                distance_AS_forme = comparaison_structurelle.method_Calcul_score_CS_forme(mat_cod_test, mat_cod_1, mat_cod_2, mat_cod_3, label_du_script_choisi, path, autorisation_distance_test_faux);

                                distance_AP_direction = direction_score.method_estimation_direction_score(mmatrice_param_echantillon_courant, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_choisi_sans_lettre);
                                distance_AP_ordre = ordre_score.method_estimation_ordre_score(mmatrice_param_echantillon_courant, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_choisi_sans_lettre);
                                distance_AP_forme = forme_score.method_Module_estimation_forme_score(mmatrice_param_echantillon_courant, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_choisi_sans_lettre, path, autorisation_distance_test_faux);

                                vect_distance_AP[0] = distance_AP_direction;
                                vect_distance_AP[1] = distance_AP_ordre;
                                vect_distance_AP[2] = distance_AP_forme;

                                vect_distance_AS[0] = distance_AS_direction;
                                vect_distance_AS[1] = distance_AS_ordre;
                                vect_distance_AS[2] = distance_AS_forme;
                                mm_distance_AP[numero_echantillon, 0] = vect_distance_AP[0];
                                mm_distance_AP[numero_echantillon, 1] = vect_distance_AP[1];
                                mm_distance_AP[numero_echantillon, 2] = vect_distance_AP[2];

                                mm_distance_AS[numero_echantillon, 0] = vect_distance_AS[0];
                                mm_distance_AS[numero_echantillon, 1] = vect_distance_AS[1];
                                mm_distance_AS[numero_echantillon, 2] = vect_distance_AS[2];

                            }

                            chemin_acces_dossier_distance = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie;
                            if (!Directory.Exists(chemin_acces_dossier_distance))
                            { Directory.CreateDirectory(chemin_acces_dossier_distance); }
                            chemin_acces_fichier_distances_parametriques = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie + "/distance_parametrique.bin";
                            chemin_acces_fichier_distances_structurelles = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie + "/distance_structurelle.bin";
                            string chemin_acces_fichier_distances_parametriques1 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie + "/distance_parametrique.txt";
                            string chemin_acces_fichier_distances_structurelles1 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie + "/distance_structurelle.txt";

                            //    var av_files_n = System.IO.Directory.GetFiles(chemin_acces_dossier_distance, "*.txt").Length;
                            chemin_acces_fichier_distances_parametriques_0 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie + "/distance_parametrique_0.bin";
                            chemin_acces_fichier_distances_structurelles_0 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie + "/distance_structurelle_0.bin";


                            if (autorisation_distance_test_faux == 0)
                            {
                                WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_parametriques_0);
                               WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_parametriques_0, mm_distance_AP);

                                //WRDfile.Method_WriteFile(mm_distance_AP, chemin_acces_fichier_distances_parametriques1);

                                WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_structurelles_0);
                               WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_structurelles_0, mm_distance_AS);
                               //WRDfile.Method_WriteFile(mm_distance_AS, chemin_acces_fichier_distances_structurelles1);

                            }

                            WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_parametriques);
                           WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_parametriques, mm_distance_AP);
                           //WRDfile.Method_WriteFile(mm_distance_AP, chemin_acces_fichier_distances_parametriques1);

                            WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_structurelles);
                           WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_structurelles, mm_distance_AS);
                           //WRDfile.Method_WriteFile(mm_distance_AS, chemin_acces_fichier_distances_structurelles1);

                            string chemin_acces_fichier_distances_parametriques2 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie + "/distance_parametrique.txt";
                            //WRDfile.Method_WriteFile(mm_distance_AP, chemin_acces_fichier_distances_parametriques2);
                        }
                    }

                    ///////////
                    classe_choisie = "faux_ordre";

                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie;
                    av_files_n2 = System.IO.Directory.GetDirectories(chemin_acces_dossier_examiner);
                    for (int numero_label = 0; numero_label < (av_files_n2.Length); numero_label++)
                    {

                        chemin_acces_dossier_label = av_files_n2[numero_label];
                        chemin_acces_sous_dossier_param = chemin_acces_dossier_label + "/matrice_param/";
                        int size_dossier_label = chemin_acces_dossier_label.Length;
                        for (int ijk = 0; ijk < size_dossier_label - 2; ijk++)  ////
                        {
                            if ((chemin_acces_dossier_label[ijk] == premier_caractere) && (chemin_acces_dossier_label[ijk + 1] == deuxieme_caractere) && (chemin_acces_dossier_label[ijk + 2] == troisieme_caractere))
                            {
                                label_choisi = chemin_acces_dossier_label.Substring(ijk);
                                size_label_choisi = label_choisi.Length;
                                label_choisi_sans_lettre = label_choisi.Substring(nombre_de_caractere + 1, (size_label_choisi - 7 - nombre_de_caractere));
                            }
                        }

                        int drapeau_autorisation_apprentissage_partiel = 1;
                        if ((type_procedure_apprentissage == 2) && (type_extracteur == 1))
                        {
                            drapeau_autorisation_apprentissage_partiel = 0;
                            if (label_choisi_sans_lettre.Length == label_du_script_choisi.Length)
                            {
                                if (label_choisi_sans_lettre == label_du_script_choisi)
                                    drapeau_autorisation_apprentissage_partiel = 1;
                            }
                        }
                        if (drapeau_autorisation_apprentissage_partiel == 1)
                        {
                            /* chemin_acces_dossier_label_modele = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole";
                             Rangs_ordonnes_des_echantillons_modeles_AP = WRDfile.Method_ReadFile_Int(chemin_acces_dossier_label_modele + "/" + "Rangs_ordonnes_des_echantillons_modeles_AP.txt");
                             Rangs_ordonnes_des_echantillons_modeles_AS = WRDfile.Method_ReadFile_Int(chemin_acces_dossier_label_modele + "/" + "Rangs_ordonnes_des_echantillons_modeles_AS.txt");

                             num_echantillon_modele_1_AP = Rangs_ordonnes_des_echantillons_modeles_AP[0];
                             num_echantillon_modele_2_AP = Rangs_ordonnes_des_echantillons_modeles_AP[1];
                             num_echantillon_modele_3_AP = Rangs_ordonnes_des_echantillons_modeles_AP[2];
                             //num_echantillon_modele_3_AP = 2;
                             num_echantillon_modele_1_AS = Rangs_ordonnes_des_echantillons_modeles_AS[0];
                             num_echantillon_modele_2_AS = Rangs_ordonnes_des_echantillons_modeles_AS[1];
                             num_echantillon_modele_3_AS = Rangs_ordonnes_des_echantillons_modeles_AS[2];
                             //num_echantillon_modele_3_AS = 2;*/
                            num_echantillon_modele_1_AP = 1;
                            num_echantillon_modele_2_AP = 2;
                            num_echantillon_modele_3_AP = 3;
                            num_echantillon_modele_1_AS = 1;
                            num_echantillon_modele_2_AS = 2;
                            num_echantillon_modele_3_AS = 3;
                            chemin_acces_param_echantillon_modele_1_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/matrice_param/" + label_choisi_sans_lettre + "_modele_" + num_echantillon_modele_1_AP + ".bin";
                            chemin_acces_param_echantillon_modele_2_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/matrice_param/" + label_choisi_sans_lettre + "_modele_" + num_echantillon_modele_2_AP + ".bin";
                            chemin_acces_param_echantillon_modele_3_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/matrice_param/" + label_choisi_sans_lettre + "_modele_" + num_echantillon_modele_3_AP + ".bin";
                            chemin_acces_param_echantillon_modele_1_AS = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/matrice_param/" + label_choisi_sans_lettre + "_modele_" + num_echantillon_modele_1_AS + ".bin";
                            chemin_acces_param_echantillon_modele_2_AS = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/matrice_param/" + label_choisi_sans_lettre + "_modele_" + num_echantillon_modele_2_AS + ".bin";
                            chemin_acces_param_echantillon_modele_3_AS = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/matrice_param/" + label_choisi_sans_lettre + "_modele_" + num_echantillon_modele_3_AS + ".bin";

                            mmatrice_param_echantillon_modele_1_AP = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_1_AP);
                            mmatrice_param_echantillon_modele_2_AP = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_2_AP);
                            mmatrice_param_echantillon_modele_3_AP = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_3_AP);

                            mmatrice_param_echantillon_modele_1_AS = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_1_AS);
                            mmatrice_param_echantillon_modele_2_AS = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_2_AS);
                            mmatrice_param_echantillon_modele_3_AS = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_param_echantillon_modele_3_AS);

                            int size_av_files_n4 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin").Length;
                            var av_files_n4 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin");
                            mm_distance_AP = new double[size_av_files_n4, 3];
                            mm_distance_AS = new double[size_av_files_n4, 3];
                            // double[] yy1 = new double[mmatrice_param_echantillon_courant.GetLength(1)];
                            double[] mmatrice_echantillon_modele_1111_AS = new double[mmatrice_param_echantillon_modele_1_AS.GetLength(1)];
                            double[] mmatrice_echantillon_modele_1222_AS = new double[mmatrice_param_echantillon_modele_2_AS.GetLength(1)];
                            double[] mmatrice_echantillon_modele_1333_AS = new double[mmatrice_param_echantillon_modele_3_AS.GetLength(1)];
                            for (int numero_echantillon = 0; numero_echantillon < size_av_files_n4; numero_echantillon++)
                            {
                                chemin_acces_fichier_param_echantillon_courant = av_files_n4[numero_echantillon];
                                mmatrice_param_echantillon_courant = WRDfile.Method_ReadFile_Bin_Param(chemin_acces_fichier_param_echantillon_courant);
                                int t_m2 = mmatrice_param_echantillon_courant.GetLength(1);
                                double[] yy3 = new double[t_m2];
                                for (var ii = 0; ii < yy3.Length; ii++)
                                    yy3[ii] = mmatrice_param_echantillon_courant[6, ii];

                                mat_cod_test = codage_p1.method_codage_p(yy3, mmatrice_param_echantillon_courant);
                                // mmatrice_echantillon_modele_1 = new double[mmatrice_param_echantillon_modele_1.GetLength(1)];

                                for (var ii = 0; ii < mmatrice_param_echantillon_modele_1_AS.GetLength(1); ii++)
                                    mmatrice_echantillon_modele_1111_AS[ii] = mmatrice_param_echantillon_modele_1_AS[6, ii];
                                mat_cod_1 = codage_p1.method_codage_p(mmatrice_echantillon_modele_1111_AS, mmatrice_param_echantillon_modele_1_AS);

                                for (var ii = 0; ii < mmatrice_param_echantillon_modele_2_AS.GetLength(1); ii++)
                                    mmatrice_echantillon_modele_1222_AS[ii] = mmatrice_param_echantillon_modele_2_AS[6, ii];
                                mat_cod_2 = codage_p1.method_codage_p(mmatrice_echantillon_modele_1222_AS, mmatrice_param_echantillon_modele_2_AS);

                                for (var ii = 0; ii < mmatrice_param_echantillon_modele_3_AS.GetLength(1); ii++)
                                    mmatrice_echantillon_modele_1333_AS[ii] = mmatrice_param_echantillon_modele_3_AS[6, ii];
                                mat_cod_3 = codage_p1.method_codage_p(mmatrice_echantillon_modele_1333_AS, mmatrice_param_echantillon_modele_3_AS);

                                distance_AS_direction = comparaison_structurelle.method_Calcul_score_CS_direction(mat_cod_test, mat_cod_1, mat_cod_2, mat_cod_3);
                                distance_AS_ordre = comparaison_structurelle.method_Calcul_score_CS_ordre(mat_cod_test, mat_cod_1, mat_cod_2, mat_cod_3);
                                distance_AS_forme = comparaison_structurelle.method_Calcul_score_CS_forme(mat_cod_test, mat_cod_1, mat_cod_2, mat_cod_3, label_du_script_choisi, path, autorisation_distance_test_faux);

                                distance_AP_direction = direction_score.method_estimation_direction_score(mmatrice_param_echantillon_courant, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_choisi_sans_lettre);
                                distance_AP_ordre = ordre_score.method_estimation_ordre_score(mmatrice_param_echantillon_courant, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_choisi_sans_lettre);
                                distance_AP_forme = forme_score.method_Module_estimation_forme_score(mmatrice_param_echantillon_courant, mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, label_choisi_sans_lettre, path, autorisation_distance_test_faux);

                                vect_distance_AP[0] = distance_AP_direction;
                                vect_distance_AP[1] = distance_AP_ordre;
                                vect_distance_AP[2] = distance_AP_forme;

                                vect_distance_AS[0] = distance_AS_direction;
                                vect_distance_AS[1] = distance_AS_ordre;
                                vect_distance_AS[2] = distance_AS_forme;
                                mm_distance_AP[numero_echantillon, 0] = vect_distance_AP[0];
                                mm_distance_AP[numero_echantillon, 1] = vect_distance_AP[1];
                                mm_distance_AP[numero_echantillon, 2] = vect_distance_AP[2];

                                mm_distance_AS[numero_echantillon, 0] = vect_distance_AS[0];
                                mm_distance_AS[numero_echantillon, 1] = vect_distance_AS[1];
                                mm_distance_AS[numero_echantillon, 2] = vect_distance_AS[2];

                            }

                            chemin_acces_dossier_distance = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie;
                            if (!Directory.Exists(chemin_acces_dossier_distance))
                            { Directory.CreateDirectory(chemin_acces_dossier_distance); }
                            chemin_acces_fichier_distances_parametriques = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie + "/distance_parametrique.bin";
                            chemin_acces_fichier_distances_structurelles = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie + "/distance_structurelle.bin";
                            // var av_files_n = System.IO.Directory.GetFiles(chemin_acces_dossier_distance, "*.txt").Length;
                            chemin_acces_fichier_distances_parametriques_0 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie + "/distance_parametrique_0.bin";
                            chemin_acces_fichier_distances_structurelles_0 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie + "/distance_structurelle_0.bin";
                            string chemin_acces_fichier_distances_parametriques1 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie + "/distance_parametrique.txt";
                            string chemin_acces_fichier_distances_structurelles1 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/" + classe_choisie + "/distance_structurelle.txt";

                            if (autorisation_distance_test_faux == 0)
                            {
                                WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_parametriques_0);
                               WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_parametriques_0, mm_distance_AP);

                                //WRDfile.Method_WriteFile(mm_distance_AP, chemin_acces_fichier_distances_parametriques1);

                                WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_structurelles_0);
                               WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_structurelles_0, mm_distance_AS);
                               //WRDfile.Method_WriteFile(mm_distance_AS, chemin_acces_fichier_distances_structurelles1);

                            }

                            WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_parametriques);
                           WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_parametriques, mm_distance_AP);
                            //WRDfile.Method_WriteFile(mm_distance_AP, chemin_acces_fichier_distances_parametriques1);

                            WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_structurelles);
                           WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_structurelles, mm_distance_AS);

                            //WRDfile.Method_WriteFile(mm_distance_AS, chemin_acces_fichier_distances_structurelles1);
                        }
                    }
                }

                /// ******************************************************************//
                // *****************************calcul des seuils ******************//
                if ((type_procedure_apprentissage == 1) || (type_procedure_apprentissage == 2) || (type_procedure_apprentissage == 3) || (type_procedure_apprentissage == 4))
                {
                    string chemin_acces_dossier_distance_a_examiner = path + "/Base_apprentissage_distance/" + type_de_script_p + "/";
                    var av_files_n8 = System.IO.Directory.GetDirectories(chemin_acces_dossier_distance_a_examiner);
                    for (int numero_label = 0; numero_label < av_files_n8.Length; numero_label++)
                    {
                        string chemin_acces_dossier_distance_label = av_files_n8[numero_label];

                        int size_dossier_label = chemin_acces_dossier_distance_label.Length;
                        for (int ijk = 0; ijk < size_dossier_label - 2; ijk++)  ////
                        {
                            if ((chemin_acces_dossier_distance_label[ijk] == premier_caractere) && (chemin_acces_dossier_distance_label[ijk + 1] == deuxieme_caractere) && (chemin_acces_dossier_distance_label[ijk + 2] == troisieme_caractere))
                            {
                                label_choisi = chemin_acces_dossier_distance_label.Substring(ijk);
                                size_label_choisi = label_choisi.Length;
                                label_choisi_sans_lettre = label_choisi.Substring(nombre_de_caractere + 1, (size_label_choisi - 7 - nombre_de_caractere));
                            }
                        }
                        drapeau_autorisation_apprentissage_partiel = 1;
                        if ((type_procedure_apprentissage == 2) && (type_extracteur == 1))
                        {
                            drapeau_autorisation_apprentissage_partiel = 0;
                            if (label_choisi_sans_lettre.Length == label_du_script_choisi.Length)
                            {
                                if (label_choisi_sans_lettre == label_du_script_choisi)
                                { drapeau_autorisation_apprentissage_partiel = 1; }
                            }
                        }
                        if (drapeau_autorisation_apprentissage_partiel == 1)
                        {
                            chemin_acces_dossier_matrices_distance = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/";


                            chemin_acces_dossier_matrices_seuils = path + "/Record_des_Valeurs_des_Seuils_Appris/" + type_de_script_p + "/" + type_de_script_p + "_" + label_choisi_sans_lettre + "_isole/";


                            /// %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%//
                            //  %%%%%%% Mise à jour des valeurs des seuils %%%%%%%%%%%%///
                            // %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%//
                            estimation_seuil.Method_Estimation_et_Enregistrement_des_Seuils_V1(label_du_script_choisi, type_de_script_p, path);
                        }

                    }
                }


                //%%%%%%%%%%%%%%%%%%%%%%% enregistrement des paramètres DF %%%%%%%%%%%%%%%%%
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                if ((type_procedure_apprentissage == 2) && (type_extracteur == 2))
                ///////////////////////// Calcul des matrices param //////////////
                {
                    classe_choisie = "modeles";
                   
                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/";
                    chemin_acces_sous_dossier_courant = chemin_acces_dossier_examiner + "/xy_traject/";

                    chemin_acces_sous_dossier_param_DF = chemin_acces_dossier_examiner + "/matrice_param_DF/";
                    var av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_DF, "*.bin");
                    int size_av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_DF, "*.bin").Length;
                    // Console.WriteLine("size "+ size_av_files_n13);
                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n13; numero_echantillon++)
                    {
                        string chemin = av_files_n13[numero_echantillon];
                        WRDfile.Method_DeleteFiles(chemin);
                    }

                    var number_av_files_n2 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin").Length;
                    var av_files_n2 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin");

                    for (int num_label = 0; num_label < number_av_files_n2; num_label++)
                    {
                        chemin_acces_fichier_echantillon_courant = av_files_n2[num_label];
                        xy_echantillon_courant = WRDfile.Method_ReadFile_Bin_XY(chemin_acces_fichier_echantillon_courant);

                        mmatrice_param_DF_echantillon_courant = lecture.method_mmatrice_param_DF(xy_echantillon_courant, label_du_script_choisi);
                        int num_echa = num_label + 1;
                        chemin_acces_fichier_destination_matrice_param_DF = chemin_acces_sous_dossier_param_DF + label_du_script_choisi + "_" + classe_choisie.Substring(0, ((classe_choisie.Length - 1))) + "_" + num_echa + ".bin";
                        WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_destination_matrice_param_DF, mmatrice_param_DF_echantillon_courant);
                        string chemin_acces_fichier_destination_matrice_param_DF1 = chemin_acces_sous_dossier_param_DF + label_du_script_choisi + "_" + classe_choisie.Substring(0, ((classe_choisie.Length - 1))) + "_" + num_echa + ".txt";
                        //WRDfile.Method_WriteFile(mmatrice_param_DF_echantillon_courant, chemin_acces_fichier_destination_matrice_param_DF1);

                    }

                    ////////////////////////////
                    classe_choisie = "correcte";

                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/";
                    chemin_acces_sous_dossier_courant = chemin_acces_dossier_examiner + "/xy_traject/";
                    chemin_acces_sous_dossier_param_DF = chemin_acces_dossier_examiner + "/matrice_param_DF/";
                    var av_files_n113 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_DF, "*.bin");
                    int size_av_files_n113 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_DF, "*.bin").Length;
                    // Console.WriteLine("size "+ size_av_files_n13);
                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n113; numero_echantillon++)
                    {
                        string chemin = av_files_n113[numero_echantillon];
                        WRDfile.Method_DeleteFiles(chemin);
                    }

                    var number_av_files_n12 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin").Length;
                    var av_files_n12 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin");

                    for (int num_label = 0; num_label < number_av_files_n12; num_label++)
                    {
                        chemin_acces_fichier_echantillon_courant = av_files_n12[num_label];
                        xy_echantillon_courant = WRDfile.Method_ReadFile_Bin_XY(chemin_acces_fichier_echantillon_courant);

                        mmatrice_param_DF_echantillon_courant = lecture.method_mmatrice_param_DF(xy_echantillon_courant, label_du_script_choisi);
                        int num_echa = num_label + 1;
                        chemin_acces_fichier_destination_matrice_param_DF = chemin_acces_sous_dossier_param_DF + label_du_script_choisi + "_" + classe_choisie.Substring(0, ((classe_choisie.Length - 1))) + "_" + num_echa + ".bin";
                        WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_destination_matrice_param_DF, mmatrice_param_DF_echantillon_courant);
                        string chemin_acces_fichier_destination_matrice_param_DF1 = chemin_acces_sous_dossier_param_DF + label_du_script_choisi + "_" + classe_choisie.Substring(0, ((classe_choisie.Length - 1))) + "_" + num_echa + ".txt";
                        //WRDfile.Method_WriteFile(mmatrice_param_DF_echantillon_courant, chemin_acces_fichier_destination_matrice_param_DF1);

                    }

                    // Methode domaine de variation DF
                    Dvariation.Method_Domaine_de_variation_DF(path, label_du_script_choisi, type_de_script_p, type_de_script_q, classe_choisie);


                    ///////////////////////////
                    classe_choisie = "fausse_forme";

                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/";
                    chemin_acces_sous_dossier_courant = chemin_acces_dossier_examiner + "/xy_traject/";
                    chemin_acces_sous_dossier_param_DF = chemin_acces_dossier_examiner + "/matrice_param_DF/";
                    var av_files_n133 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_DF, "*.bin");
                    int size_av_files_n133 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_DF, "*.bin").Length;
                    // Console.WriteLine("size "+ size_av_files_n13);
                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n133; numero_echantillon++)
                    {
                        string chemin = av_files_n133[numero_echantillon];
                        WRDfile.Method_DeleteFiles(chemin);
                    }

                    var number_av_files_n32 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin").Length;
                    var av_files_n32 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin");

                    for (int num_label = 0; num_label < number_av_files_n32; num_label++)
                    {
                        chemin_acces_fichier_echantillon_courant = av_files_n32[num_label];
                        xy_echantillon_courant = WRDfile.Method_ReadFile_Bin_XY(chemin_acces_fichier_echantillon_courant);

                        mmatrice_param_DF_echantillon_courant = lecture.method_mmatrice_param_DF(xy_echantillon_courant, label_du_script_choisi);
                        int num_echa = num_label + 1;
                        chemin_acces_fichier_destination_matrice_param_DF = chemin_acces_sous_dossier_param_DF + label_du_script_choisi + "_" + classe_choisie.Substring(0, ((classe_choisie.Length - 1))) + "_" + num_echa + ".bin";
                        WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_destination_matrice_param_DF, mmatrice_param_DF_echantillon_courant);
                        string chemin_acces_fichier_destination_matrice_param_DF1 = chemin_acces_sous_dossier_param_DF + label_du_script_choisi + "_" + classe_choisie.Substring(0, ((classe_choisie.Length - 1))) + "_" + num_echa + ".txt";
                        //WRDfile.Method_WriteFile(mmatrice_param_DF_echantillon_courant, chemin_acces_fichier_destination_matrice_param_DF1);

                    }
                    ///////////////////////////////////
                    classe_choisie = "faux_ordre";

                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/";
                    chemin_acces_sous_dossier_courant = chemin_acces_dossier_examiner + "/xy_traject/";
                    chemin_acces_sous_dossier_param_DF = chemin_acces_dossier_examiner + "/matrice_param_DF/";
                    var av_files_n131 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_DF, "*.bin");
                    int size_av_files_n131 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_DF, "*.bin").Length;
                    // Console.WriteLine("size "+ size_av_files_n13);
                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n131; numero_echantillon++)
                    {
                        string chemin = av_files_n131[numero_echantillon];
                        WRDfile.Method_DeleteFiles(chemin);
                    }

                    var number_av_files_n21 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin").Length;
                    var av_files_n21 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin");

                    for (int num_label = 0; num_label < number_av_files_n21; num_label++)
                    {
                        chemin_acces_fichier_echantillon_courant = av_files_n21[num_label];
                        xy_echantillon_courant = WRDfile.Method_ReadFile_Bin_XY(chemin_acces_fichier_echantillon_courant);

                        mmatrice_param_DF_echantillon_courant = lecture.method_mmatrice_param_DF(xy_echantillon_courant, label_du_script_choisi);
                        int num_echa = num_label + 1;
                        chemin_acces_fichier_destination_matrice_param_DF = chemin_acces_sous_dossier_param_DF + label_du_script_choisi + "_" + classe_choisie.Substring(0, ((classe_choisie.Length - 1))) + "_" + num_echa + ".bin";
                        WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_destination_matrice_param_DF, mmatrice_param_DF_echantillon_courant);
                        string chemin_acces_fichier_destination_matrice_param_DF1 = chemin_acces_sous_dossier_param_DF + label_du_script_choisi + "_" + classe_choisie.Substring(0, ((classe_choisie.Length - 1))) + "_" + num_echa + ".txt";
                        //WRDfile.Method_WriteFile(mmatrice_param_DF_echantillon_courant, chemin_acces_fichier_destination_matrice_param_DF1);

                    }

                    ////////////////////////////////////
                    classe_choisie = "fausse_direction";
                    if (!Directory.Exists(chemin_acces_sous_dossier_param_DF))
                    { Directory.CreateDirectory(chemin_acces_sous_dossier_param_DF); }
                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/";
                    chemin_acces_sous_dossier_courant = chemin_acces_dossier_examiner + "/xy_traject/";

                    chemin_acces_sous_dossier_param_DF = chemin_acces_dossier_examiner + "/matrice_param_DF/";
                    var av_files_n123 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_DF, "*.bin");
                    int size_av_files_n123 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_DF, "*.bin").Length;
                    // Console.WriteLine("size "+ size_av_files_n13);
                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n123; numero_echantillon++)
                    {
                        string chemin = av_files_n123[numero_echantillon];
                        WRDfile.Method_DeleteFiles(chemin);
                    }

                    var number_av_files_n22 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin").Length;
                    var av_files_n22 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin");

                    for (int num_label = 0; num_label < number_av_files_n22; num_label++)
                    {
                        chemin_acces_fichier_echantillon_courant = av_files_n22[num_label];
                        xy_echantillon_courant = WRDfile.Method_ReadFile_Bin_XY(chemin_acces_fichier_echantillon_courant);

                        mmatrice_param_DF_echantillon_courant = lecture.method_mmatrice_param_DF(xy_echantillon_courant, label_du_script_choisi);
                        int num_echa = num_label + 1;
                        chemin_acces_fichier_destination_matrice_param_DF = chemin_acces_sous_dossier_param_DF + label_du_script_choisi + "_" + classe_choisie.Substring(0, ((classe_choisie.Length - 1))) + "_" + num_echa + ".bin";
                        WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_destination_matrice_param_DF, mmatrice_param_DF_echantillon_courant);
                        string chemin_acces_fichier_destination_matrice_param_DF1 = chemin_acces_sous_dossier_param_DF + label_du_script_choisi + "_" + classe_choisie.Substring(0, ((classe_choisie.Length - 1))) + "_" + num_echa + ".txt";
                        //WRDfile.Method_WriteFile(mmatrice_param_DF_echantillon_courant, chemin_acces_fichier_destination_matrice_param_DF1);

                    }

                }


                //%%%%%%%%%%%%%%%%%%%%%%% enregistrement des paramètres DF cartesienne %%%%%%%%%%%%%%%%%
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                if ((type_procedure_apprentissage == 2) && (type_extracteur == 3))
                ///////////////////////// Calcul des matrices param //////////////
                {
                    classe_choisie = "modeles";
                    
                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/";
                    chemin_acces_sous_dossier_courant = chemin_acces_dossier_examiner + "/xy_traject/";                    
                    chemin_acces_sous_dossier_param_DFcar = chemin_acces_dossier_examiner + "/matrice_param_DFcar/";
                    if (!Directory.Exists(chemin_acces_sous_dossier_param_DFcar))
                    { Directory.CreateDirectory(chemin_acces_sous_dossier_param_DFcar); }
                    var av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_DFcar, "*.bin");
                    int size_av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_DFcar, "*.bin").Length;
                    // Console.WriteLine("size "+ size_av_files_n13);
                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n13; numero_echantillon++)
                    {
                        string chemin = av_files_n13[numero_echantillon];
                        WRDfile.Method_DeleteFiles(chemin);
                    }

                    var number_av_files_n2 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin").Length;
                    var av_files_n2 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin");

                    for (int num_label = 0; num_label < number_av_files_n2; num_label++)
                    {
                        chemin_acces_fichier_echantillon_courant = av_files_n2[num_label];
                        xy_echantillon_courant = WRDfile.Method_ReadFile_Bin_XY(chemin_acces_fichier_echantillon_courant);

                        mmatrice_param_DFcar_echantillon_courant = lecturecar.method_mmatrice_param_DF_car(xy_echantillon_courant, label_du_script_choisi);
                        num_echa = num_label + 1;
                        chemin_acces_fichier_destination_matrice_param_DFcar = chemin_acces_sous_dossier_param_DFcar + label_du_script_choisi + "_" + classe_choisie.Substring(0, ((classe_choisie.Length - 1))) + "_" + num_echa + ".bin";
                        WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_destination_matrice_param_DFcar, mmatrice_param_DFcar_echantillon_courant);
                        string chemin_acces_fichier_destination_matrice_param_DFcar1 = chemin_acces_sous_dossier_param_DFcar + label_du_script_choisi + "_" + classe_choisie.Substring(0, ((classe_choisie.Length - 1))) + "_" + num_echa + ".txt";
                        WRDfile.Method_WriteFile(mmatrice_param_DFcar_echantillon_courant, chemin_acces_fichier_destination_matrice_param_DFcar1);
                        
                    }

                    ////////////////////////////
                    classe_choisie = "correcte";
                    
                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/";
                    chemin_acces_sous_dossier_courant = chemin_acces_dossier_examiner + "/xy_traject/";
                    chemin_acces_sous_dossier_param_DFcar = chemin_acces_dossier_examiner + "/matrice_param_DFcar/";
                    if (!Directory.Exists(chemin_acces_sous_dossier_param_DFcar))
                    { Directory.CreateDirectory(chemin_acces_sous_dossier_param_DFcar); }
                    var av_files_n113 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_DFcar, "*.bin");
                    int size_av_files_n113 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_DFcar, "*.bin").Length;
                    // Console.WriteLine("size "+ size_av_files_n13);
                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n113; numero_echantillon++)
                    {
                        string chemin = av_files_n113[numero_echantillon];
                        WRDfile.Method_DeleteFiles(chemin);
                    }

                    var number_av_files_n12 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin").Length;
                    var av_files_n12 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin");

                    for (int num_label = 0; num_label < number_av_files_n12; num_label++)
                    {
                        chemin_acces_fichier_echantillon_courant = av_files_n12[num_label];
                        xy_echantillon_courant = WRDfile.Method_ReadFile_Bin_XY(chemin_acces_fichier_echantillon_courant);

                        mmatrice_param_DFcar_echantillon_courant = lecturecar.method_mmatrice_param_DF_car(xy_echantillon_courant, label_du_script_choisi);
                        num_echa = num_label + 1;
                        chemin_acces_fichier_destination_matrice_param_DFcar = chemin_acces_sous_dossier_param_DFcar + label_du_script_choisi + "_" + classe_choisie.Substring(0, ((classe_choisie.Length - 1))) + "_" + num_echa + ".bin";
                        WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_destination_matrice_param_DFcar, mmatrice_param_DFcar_echantillon_courant);
                        string chemin_acces_fichier_destination_matrice_param_DFcar1 = chemin_acces_sous_dossier_param_DFcar + label_du_script_choisi + "_" + classe_choisie.Substring(0, ((classe_choisie.Length - 1))) + "_" + num_echa + ".txt";
                        WRDfile.Method_WriteFile(mmatrice_param_DFcar_echantillon_courant, chemin_acces_fichier_destination_matrice_param_DFcar1);

                    }

                    // Methode domaine de variation DF
                    Dvariationcar.Method_Domaine_de_variation_DFcar(path, label_du_script_choisi, type_de_script_p, type_de_script_q, classe_choisie);


                    ///////////////////////////
                    classe_choisie = "fausse_forme";
                   
                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/";
                    chemin_acces_sous_dossier_courant = chemin_acces_dossier_examiner + "/xy_traject/";
                    chemin_acces_sous_dossier_param_DFcar = chemin_acces_dossier_examiner + "/matrice_param_DFcar/";
                    if (!Directory.Exists(chemin_acces_sous_dossier_param_DFcar))
                    { Directory.CreateDirectory(chemin_acces_sous_dossier_param_DFcar); }
                    var av_files_n133 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_DFcar, "*.bin");
                    int size_av_files_n133 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_DFcar, "*.bin").Length;
                    // Console.WriteLine("size "+ size_av_files_n13);
                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n133; numero_echantillon++)
                    {
                        string chemin = av_files_n133[numero_echantillon];
                        WRDfile.Method_DeleteFiles(chemin);
                    }

                    var number_av_files_n32 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin").Length;
                    var av_files_n32 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin");

                    for (int num_label = 0; num_label < number_av_files_n32; num_label++)
                    {
                        chemin_acces_fichier_echantillon_courant = av_files_n32[num_label];
                        xy_echantillon_courant = WRDfile.Method_ReadFile_Bin_XY(chemin_acces_fichier_echantillon_courant);

                        mmatrice_param_DFcar_echantillon_courant = lecturecar.method_mmatrice_param_DF_car(xy_echantillon_courant, label_du_script_choisi);
                        num_echa = num_label + 1;
                        chemin_acces_fichier_destination_matrice_param_DFcar = chemin_acces_sous_dossier_param_DFcar + label_du_script_choisi + "_" + classe_choisie.Substring(0, ((classe_choisie.Length - 1))) + "_" + num_echa + ".bin";
                        WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_destination_matrice_param_DFcar, mmatrice_param_DFcar_echantillon_courant);
                        string chemin_acces_fichier_destination_matrice_param_DFcar1 = chemin_acces_sous_dossier_param_DFcar + label_du_script_choisi + "_" + classe_choisie.Substring(0, ((classe_choisie.Length - 1))) + "_" + num_echa + ".txt";
                        WRDfile.Method_WriteFile(mmatrice_param_DFcar_echantillon_courant, chemin_acces_fichier_destination_matrice_param_DFcar1);

                    }
                    ///////////////////////////////////
                    classe_choisie = "faux_ordre";
                   
                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/";
                    chemin_acces_sous_dossier_courant = chemin_acces_dossier_examiner + "/xy_traject/";
                    chemin_acces_sous_dossier_param_DFcar = chemin_acces_dossier_examiner + "/matrice_param_DFcar/";
                    if (!Directory.Exists(chemin_acces_sous_dossier_param_DFcar))
                    { Directory.CreateDirectory(chemin_acces_sous_dossier_param_DFcar); }
                    var av_files_n131 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_DFcar, "*.bin");
                    int size_av_files_n131 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_DFcar, "*.bin").Length;
                    // Console.WriteLine("size "+ size_av_files_n13);
                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n131; numero_echantillon++)
                    {
                        string chemin = av_files_n131[numero_echantillon];
                        WRDfile.Method_DeleteFiles(chemin);
                    }

                    var number_av_files_n21 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin").Length;
                    var av_files_n21 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin");

                    for (int num_label = 0; num_label < number_av_files_n21; num_label++)
                    {
                        chemin_acces_fichier_echantillon_courant = av_files_n21[num_label];
                        xy_echantillon_courant = WRDfile.Method_ReadFile_Bin_XY(chemin_acces_fichier_echantillon_courant);

                        mmatrice_param_DFcar_echantillon_courant = lecturecar.method_mmatrice_param_DF_car(xy_echantillon_courant, label_du_script_choisi);
                        num_echa = num_label + 1;
                        chemin_acces_fichier_destination_matrice_param_DFcar = chemin_acces_sous_dossier_param_DFcar + label_du_script_choisi + "_" + classe_choisie.Substring(0, ((classe_choisie.Length - 1))) + "_" + num_echa + ".bin";
                        WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_destination_matrice_param_DFcar, mmatrice_param_DFcar_echantillon_courant);
                        string chemin_acces_fichier_destination_matrice_param_DFcar1 = chemin_acces_sous_dossier_param_DFcar + label_du_script_choisi + "_" + classe_choisie.Substring(0, ((classe_choisie.Length - 1))) + "_" + num_echa + ".txt";
                        WRDfile.Method_WriteFile(mmatrice_param_DFcar_echantillon_courant, chemin_acces_fichier_destination_matrice_param_DFcar1);

                    }

                    ////////////////////////////////////
                    classe_choisie = "fausse_direction";
                 
                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/";
                    chemin_acces_sous_dossier_courant = chemin_acces_dossier_examiner + "/xy_traject/";

                    chemin_acces_sous_dossier_param_DFcar = chemin_acces_dossier_examiner + "/matrice_param_DFcar/";
                    if (!Directory.Exists(chemin_acces_sous_dossier_param_DFcar))
                    { Directory.CreateDirectory(chemin_acces_sous_dossier_param_DFcar); }
                    var av_files_n123 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_DFcar, "*.bin");
                    int size_av_files_n123 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_DFcar, "*.bin").Length;
                    // Console.WriteLine("size "+ size_av_files_n13);
                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n123; numero_echantillon++)
                    {
                        string chemin = av_files_n123[numero_echantillon];
                        WRDfile.Method_DeleteFiles(chemin);
                    }

                    var number_av_files_n22 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin").Length;
                    var av_files_n22 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_courant, "*.bin");

                    for (int num_label = 0; num_label < number_av_files_n22; num_label++)
                    {
                        chemin_acces_fichier_echantillon_courant = av_files_n22[num_label];
                        xy_echantillon_courant = WRDfile.Method_ReadFile_Bin_XY(chemin_acces_fichier_echantillon_courant);

                        mmatrice_param_DFcar_echantillon_courant = lecturecar.method_mmatrice_param_DF_car(xy_echantillon_courant, label_du_script_choisi);
                        num_echa = num_label + 1;
                        chemin_acces_fichier_destination_matrice_param_DFcar = chemin_acces_sous_dossier_param_DFcar + label_du_script_choisi + "_" + classe_choisie.Substring(0, ((classe_choisie.Length - 1))) + "_" + num_echa + ".bin";
                        WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_destination_matrice_param_DFcar, mmatrice_param_DFcar_echantillon_courant);
                        string chemin_acces_fichier_destination_matrice_param_DFcar1 = chemin_acces_sous_dossier_param_DFcar + label_du_script_choisi + "_" + classe_choisie.Substring(0, ((classe_choisie.Length - 1))) + "_" + num_echa + ".txt";
                        WRDfile.Method_WriteFile(mmatrice_param_DFcar_echantillon_courant, chemin_acces_fichier_destination_matrice_param_DFcar1);

                    }

                }
                
                //%% *****************************calcul des distances DF * ************************************%%
                /////////////////////////////////////////////////////////////////////////////////////////////////
                if (((type_procedure_apprentissage == 3) && (type_approche == 2)) || ((type_procedure_apprentissage == 2) && (type_approche == 2)))
              
                {
                    classe_choisie = "correcte";

                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/";
                    chemin_acces_sous_dossier_param = chemin_acces_dossier_examiner + type_de_script_p + "_" + label_du_script_choisi + "_isole" + "/matrice_param_DF/";

                    num_echantillon_modele_1_AP = 1;
                    num_echantillon_modele_2_AP = 2;
                    num_echantillon_modele_3_AP = 3;

                    //num_echantillon_modele_3_AS = 2;
                    string chemin_acces_param_DF_echantillon_modele_1_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DF/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_1_AP + ".bin";
                    string chemin_acces_param_DF_echantillon_modele_2_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DF/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_2_AP + ".bin";
                    string chemin_acces_param_DF_echantillon_modele_3_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DF/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_3_AP + ".bin";

                    mmatrice_param_DF_echantillon_modele_1_AP = WRDfile.Method_ReadFile_Bin_Param_DF(chemin_acces_param_DF_echantillon_modele_1_AP);
                    mmatrice_param_DF_echantillon_modele_2_AP = WRDfile.Method_ReadFile_Bin_Param_DF(chemin_acces_param_DF_echantillon_modele_2_AP);
                    mmatrice_param_DF_echantillon_modele_3_AP = WRDfile.Method_ReadFile_Bin_Param_DF(chemin_acces_param_DF_echantillon_modele_3_AP);

                    var av_files_n2 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin");
                    int size_av_files_n2 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin").Length;
                    mm_distance_DF = new double[size_av_files_n2, 3];

                    for (int numero_label = 0; numero_label < size_av_files_n2; numero_label++)
                    {

                        chemin_acces_fichier_param_echantillon_courant = av_files_n2[numero_label];

                        mmatrice_param_DF_echantillon_courant = WRDfile.Method_ReadFile_Bin_Param_DF(chemin_acces_fichier_param_echantillon_courant);
                        //[distance_DF_direction]= Module_estimation_direction_score_DF(mmatrice_param_DF_echantillon_courant, mmatrice_param_DF_echantillon_modele_1_AP, mmatrice_param_DF_echantillon_modele_2_AP, mmatrice_param_DF_echantillon_modele_3_AP, label_du_script_choisi, type_de_script_p, type_de_script_q, autorisation_consideration_distance_test_faux)

                        distance_DF_direction = direction_score_DF.method_Module_estimation_direction_score_DF(mmatrice_param_DF_echantillon_courant, mmatrice_param_DF_echantillon_modele_1_AP, mmatrice_param_DF_echantillon_modele_2_AP, mmatrice_param_DF_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, 0);
                        distance_DF_ordre = ordre_score_DF.method_Module_estimation_ordre_score_DF(mmatrice_param_DF_echantillon_courant, mmatrice_param_DF_echantillon_modele_1_AP, mmatrice_param_DF_echantillon_modele_2_AP, mmatrice_param_DF_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, 0);
                        distance_DF_forme = forme_score_DF.method_Module_estimation_forme_score_DF(mmatrice_param_DF_echantillon_courant, mmatrice_param_DF_echantillon_modele_1_AP, mmatrice_param_DF_echantillon_modele_2_AP, mmatrice_param_DF_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, 0);

                        mm_distance_DF[numero_label, 0] = distance_DF_direction;
                        mm_distance_DF[numero_label, 1] = distance_DF_ordre;
                        mm_distance_DF[numero_label, 2] = distance_DF_forme;

                    }

                    string chemin_acces_fichier_distances_DF = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DF.bin";
                    string chemin_acces_fichier_distances_DF_0 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DF_0.bin";
                   
                    if (autorisation_distance_test_faux == 0)
                    {
                        WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DF_0);
                       WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_DF_0, mm_distance_DF);
                    }
                    WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DF);
                   WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_DF, mm_distance_DF);
                    string chemin_acces_fichier_distances_DF1 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DF.txt";
                    string chemin_acces_fichier_distances_DF_01 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DF_0.txt";
                    if (autorisation_distance_test_faux == 0)
                    {
                        WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DF_01);
                        WRDfile.Method_WriteFile(mm_distance_DF, chemin_acces_fichier_distances_DF_01);
                    }
                    WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DF1);
                    WRDfile.Method_WriteFile(mm_distance_DF, chemin_acces_fichier_distances_DF1);
                    ////////////////
                    classe_choisie = "fausse_forme";

                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/";
                    chemin_acces_sous_dossier_param = chemin_acces_dossier_examiner + type_de_script_p + "_" + label_du_script_choisi + "_isole" + "/matrice_param_DF/";

                    num_echantillon_modele_1_AP = 1;
                    num_echantillon_modele_2_AP = 2;
                    num_echantillon_modele_3_AP = 3;

                    //num_echantillon_modele_3_AS = 2;
                    chemin_acces_param_DF_echantillon_modele_1_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DF/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_1_AP + ".bin";
                    chemin_acces_param_DF_echantillon_modele_2_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DF/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_2_AP + ".bin";
                    chemin_acces_param_DF_echantillon_modele_3_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DF/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_3_AP + ".bin";

                    mmatrice_param_DF_echantillon_modele_1_AP = WRDfile.Method_ReadFile_Bin_Param_DF(chemin_acces_param_DF_echantillon_modele_1_AP);
                    mmatrice_param_DF_echantillon_modele_2_AP = WRDfile.Method_ReadFile_Bin_Param_DF(chemin_acces_param_DF_echantillon_modele_2_AP);
                    mmatrice_param_DF_echantillon_modele_3_AP = WRDfile.Method_ReadFile_Bin_Param_DF(chemin_acces_param_DF_echantillon_modele_3_AP);

                    var av_files_n12 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin");
                    size_av_files_n2 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin").Length;
                    mm_distance_DF = new double[size_av_files_n2, 3];

                    for (int numero_label = 0; numero_label < size_av_files_n2; numero_label++)
                    {

                        chemin_acces_fichier_param_echantillon_courant = av_files_n12[numero_label];

                        mmatrice_param_DF_echantillon_courant = WRDfile.Method_ReadFile_Bin_Param_DF(chemin_acces_fichier_param_echantillon_courant);
                        //[distance_DF_direction]= Module_estimation_direction_score_DF(mmatrice_param_DF_echantillon_courant, mmatrice_param_DF_echantillon_modele_1_AP, mmatrice_param_DF_echantillon_modele_2_AP, mmatrice_param_DF_echantillon_modele_3_AP, label_du_script_choisi, type_de_script_p, type_de_script_q, autorisation_consideration_distance_test_faux)

                        distance_DF_direction = direction_score_DF.method_Module_estimation_direction_score_DF(mmatrice_param_DF_echantillon_courant, mmatrice_param_DF_echantillon_modele_1_AP, mmatrice_param_DF_echantillon_modele_2_AP, mmatrice_param_DF_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, 0);
                        distance_DF_ordre = ordre_score_DF.method_Module_estimation_ordre_score_DF(mmatrice_param_DF_echantillon_courant, mmatrice_param_DF_echantillon_modele_1_AP, mmatrice_param_DF_echantillon_modele_2_AP, mmatrice_param_DF_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, 0);
                        distance_DF_forme = forme_score_DF.method_Module_estimation_forme_score_DF(mmatrice_param_DF_echantillon_courant, mmatrice_param_DF_echantillon_modele_1_AP, mmatrice_param_DF_echantillon_modele_2_AP, mmatrice_param_DF_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, 0);

                        mm_distance_DF[numero_label, 0] = distance_DF_direction;
                        mm_distance_DF[numero_label, 1] = distance_DF_ordre;
                        mm_distance_DF[numero_label, 2] = distance_DF_forme;


                    }
                    chemin_acces_fichier_distances_DF = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DF.bin";
                    chemin_acces_fichier_distances_DF_0 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DF_0.bin";
                    chemin_acces_fichier_distances_DF1 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DF.txt";
                    
                    if (autorisation_distance_test_faux == 0)
                    {
                        WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DF_0);
                        WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_DF_0, mm_distance_DF);
                    }
                    WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DF);
                    WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_DF, mm_distance_DF);
                    chemin_acces_fichier_distances_DF1 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DF.txt";
                    chemin_acces_fichier_distances_DF_01 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DF_0.txt";
                    if (autorisation_distance_test_faux == 0)
                    {
                        WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DF_01);
                        WRDfile.Method_WriteFile(mm_distance_DF, chemin_acces_fichier_distances_DF_01);
                    }
                    WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DF1);
                    WRDfile.Method_WriteFile(mm_distance_DF, chemin_acces_fichier_distances_DF1);
                    /////////////
                    classe_choisie = "fausse_direction";

                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/";
                    chemin_acces_sous_dossier_param = chemin_acces_dossier_examiner + type_de_script_p + "_" + label_du_script_choisi + "_isole" + "/matrice_param_DF/";

                    num_echantillon_modele_1_AP = 1;
                    num_echantillon_modele_2_AP = 2;
                    num_echantillon_modele_3_AP = 3;

                    //num_echantillon_modele_3_AS = 2;
                    chemin_acces_param_DF_echantillon_modele_1_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DF/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_1_AP + ".bin";
                    chemin_acces_param_DF_echantillon_modele_2_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DF/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_2_AP + ".bin";
                    chemin_acces_param_DF_echantillon_modele_3_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DF/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_3_AP + ".bin";

                    mmatrice_param_DF_echantillon_modele_1_AP = WRDfile.Method_ReadFile_Bin_Param_DF(chemin_acces_param_DF_echantillon_modele_1_AP);
                    mmatrice_param_DF_echantillon_modele_2_AP = WRDfile.Method_ReadFile_Bin_Param_DF(chemin_acces_param_DF_echantillon_modele_2_AP);
                    mmatrice_param_DF_echantillon_modele_3_AP = WRDfile.Method_ReadFile_Bin_Param_DF(chemin_acces_param_DF_echantillon_modele_3_AP);

                    var av_files_n112 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin");
                    size_av_files_n2 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin").Length;
                    mm_distance_DF = new double[size_av_files_n2, 3];

                    for (int numero_label = 0; numero_label < size_av_files_n2; numero_label++)
                    {

                        chemin_acces_fichier_param_echantillon_courant = av_files_n112[numero_label];

                        mmatrice_param_DF_echantillon_courant = WRDfile.Method_ReadFile_Bin_Param_DF(chemin_acces_fichier_param_echantillon_courant);
                        //[distance_DF_direction]= Module_estimation_direction_score_DF(mmatrice_param_DF_echantillon_courant, mmatrice_param_DF_echantillon_modele_1_AP, mmatrice_param_DF_echantillon_modele_2_AP, mmatrice_param_DF_echantillon_modele_3_AP, label_du_script_choisi, type_de_script_p, type_de_script_q, autorisation_consideration_distance_test_faux)

                        distance_DF_direction = direction_score_DF.method_Module_estimation_direction_score_DF(mmatrice_param_DF_echantillon_courant, mmatrice_param_DF_echantillon_modele_1_AP, mmatrice_param_DF_echantillon_modele_2_AP, mmatrice_param_DF_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, 0);
                        distance_DF_ordre = ordre_score_DF.method_Module_estimation_ordre_score_DF(mmatrice_param_DF_echantillon_courant, mmatrice_param_DF_echantillon_modele_1_AP, mmatrice_param_DF_echantillon_modele_2_AP, mmatrice_param_DF_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, 0);
                        distance_DF_forme = forme_score_DF.method_Module_estimation_forme_score_DF(mmatrice_param_DF_echantillon_courant, mmatrice_param_DF_echantillon_modele_1_AP, mmatrice_param_DF_echantillon_modele_2_AP, mmatrice_param_DF_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, 0);

                        mm_distance_DF[numero_label, 0] = distance_DF_direction;
                        mm_distance_DF[numero_label, 1] = distance_DF_ordre;
                        mm_distance_DF[numero_label, 2] = distance_DF_forme;



                    }
                    chemin_acces_fichier_distances_DF = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DF.bin";
                    chemin_acces_fichier_distances_DF_0 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DF_0.bin";

                    if (autorisation_distance_test_faux == 0)
                    {
                        WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DF_0);
                       WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_DF_0, mm_distance_DF);
                    }
                    WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DF);
                   WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_DF, mm_distance_DF);
                    chemin_acces_fichier_distances_DF1 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DF.txt";
                    chemin_acces_fichier_distances_DF_01 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DF_0.txt";
                    if (autorisation_distance_test_faux == 0)
                    {
                        WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DF_01);
                        WRDfile.Method_WriteFile(mm_distance_DF, chemin_acces_fichier_distances_DF_01);
                    }
                    WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DF1);
                    WRDfile.Method_WriteFile(mm_distance_DF, chemin_acces_fichier_distances_DF1);
                    ///////////
                    classe_choisie = "faux_ordre";

                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/";
                    chemin_acces_sous_dossier_param = chemin_acces_dossier_examiner + type_de_script_p + "_" + label_du_script_choisi + "_isole" + "/matrice_param_DF/";

                    num_echantillon_modele_1_AP = 1;
                    num_echantillon_modele_2_AP = 2;
                    num_echantillon_modele_3_AP = 3;

                    //num_echantillon_modele_3_AS = 2;
                    chemin_acces_param_DF_echantillon_modele_1_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DF/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_1_AP + ".bin";
                    chemin_acces_param_DF_echantillon_modele_2_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DF/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_2_AP + ".bin";
                    chemin_acces_param_DF_echantillon_modele_3_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DF/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_3_AP + ".bin";

                    mmatrice_param_DF_echantillon_modele_1_AP = WRDfile.Method_ReadFile_Bin_Param_DF(chemin_acces_param_DF_echantillon_modele_1_AP);
                    mmatrice_param_DF_echantillon_modele_2_AP = WRDfile.Method_ReadFile_Bin_Param_DF(chemin_acces_param_DF_echantillon_modele_2_AP);
                    mmatrice_param_DF_echantillon_modele_3_AP = WRDfile.Method_ReadFile_Bin_Param_DF(chemin_acces_param_DF_echantillon_modele_3_AP);

                    var av_files_n122 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin");
                    size_av_files_n2 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin").Length;
                    mm_distance_DF = new double[size_av_files_n2, 3];

                    for (int numero_label = 0; numero_label < size_av_files_n2; numero_label++)
                    {

                        chemin_acces_fichier_param_echantillon_courant = av_files_n122[numero_label];

                        mmatrice_param_DF_echantillon_courant = WRDfile.Method_ReadFile_Bin_Param_DF(chemin_acces_fichier_param_echantillon_courant);
                        //[distance_DF_direction]= Module_estimation_direction_score_DF(mmatrice_param_DF_echantillon_courant, mmatrice_param_DF_echantillon_modele_1_AP, mmatrice_param_DF_echantillon_modele_2_AP, mmatrice_param_DF_echantillon_modele_3_AP, label_du_script_choisi, type_de_script_p, type_de_script_q, autorisation_consideration_distance_test_faux)

                        distance_DF_direction = direction_score_DF.method_Module_estimation_direction_score_DF(mmatrice_param_DF_echantillon_courant, mmatrice_param_DF_echantillon_modele_1_AP, mmatrice_param_DF_echantillon_modele_2_AP, mmatrice_param_DF_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, 0);
                        distance_DF_ordre = ordre_score_DF.method_Module_estimation_ordre_score_DF(mmatrice_param_DF_echantillon_courant, mmatrice_param_DF_echantillon_modele_1_AP, mmatrice_param_DF_echantillon_modele_2_AP, mmatrice_param_DF_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, 0);
                        distance_DF_forme = forme_score_DF.method_Module_estimation_forme_score_DF(mmatrice_param_DF_echantillon_courant, mmatrice_param_DF_echantillon_modele_1_AP, mmatrice_param_DF_echantillon_modele_2_AP, mmatrice_param_DF_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, 0);

                        mm_distance_DF[numero_label, 0] = distance_DF_direction;
                        mm_distance_DF[numero_label, 1] = distance_DF_ordre;
                        mm_distance_DF[numero_label, 2] = distance_DF_forme;


                    }
                    chemin_acces_fichier_distances_DF = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DF.bin";
                    chemin_acces_fichier_distances_DF_0 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DF_0.bin";
                    
                    if (autorisation_distance_test_faux == 0)
                    {
                        WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DF_0);
                       WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_DF_0, mm_distance_DF);
                    }
                    WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DF);
                   WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_DF, mm_distance_DF);
                    chemin_acces_fichier_distances_DF1 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DF.txt";
                    chemin_acces_fichier_distances_DF_01 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DF_0.txt";
                    if (autorisation_distance_test_faux == 0)
                    {
                        WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DF_01);
                        WRDfile.Method_WriteFile(mm_distance_DF, chemin_acces_fichier_distances_DF_01);
                    }
                    WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DF1);
                    WRDfile.Method_WriteFile(mm_distance_DF, chemin_acces_fichier_distances_DF1);

                }



                //%% *****************************calcul des distances DF cartédienne * ************************************%%
                /////////////////////////////////////////////////////////////////////////////////////////////////
                if (((type_procedure_apprentissage == 3) && (type_approche == 3)) || ((type_procedure_apprentissage == 2) && (type_approche == 3)))

                {
                    classe_choisie = "correcte";

                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/";
                    chemin_acces_sous_dossier_param = chemin_acces_dossier_examiner + type_de_script_p + "_" + label_du_script_choisi + "_isole" + "/matrice_param_DFcar/";

                    num_echantillon_modele_1_AP = 1;
                    num_echantillon_modele_2_AP = 2;
                    num_echantillon_modele_3_AP = 3;

                    //num_echantillon_modele_3_AS = 2;
                    string chemin_acces_param_DFcar_echantillon_modele_1_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DFcar/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_1_AP + ".bin";
                    string chemin_acces_param_DFcar_echantillon_modele_2_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DFcar/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_2_AP + ".bin";
                    string chemin_acces_param_DFcar_echantillon_modele_3_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DFcar/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_3_AP + ".bin";

                    mmatrice_param_DFcar_echantillon_modele_1_AP = WRDfile.Method_ReadFile_Bin_Param_DFcar(chemin_acces_param_DFcar_echantillon_modele_1_AP);
                    mmatrice_param_DFcar_echantillon_modele_2_AP = WRDfile.Method_ReadFile_Bin_Param_DFcar(chemin_acces_param_DFcar_echantillon_modele_2_AP);
                    mmatrice_param_DFcar_echantillon_modele_3_AP = WRDfile.Method_ReadFile_Bin_Param_DFcar(chemin_acces_param_DFcar_echantillon_modele_3_AP);

                    var av_files_n2 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin");
                    int size_av_files_n2 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin").Length;
                    mm_distance_DFcar = new double[size_av_files_n2, 3];

                    for (int numero_label = 0; numero_label < size_av_files_n2; numero_label++)
                    {

                        chemin_acces_fichier_param_echantillon_courant = av_files_n2[numero_label];

                        mmatrice_param_DFcar_echantillon_courant = WRDfile.Method_ReadFile_Bin_Param_DFcar(chemin_acces_fichier_param_echantillon_courant);
                        //[distance_DF_direction]= Module_estimation_direction_score_DF(mmatrice_param_DF_echantillon_courant, mmatrice_param_DF_echantillon_modele_1_AP, mmatrice_param_DF_echantillon_modele_2_AP, mmatrice_param_DF_echantillon_modele_3_AP, label_du_script_choisi, type_de_script_p, type_de_script_q, autorisation_consideration_distance_test_faux)

                        distance_DFcar_direction = direction_score_DFcar.method_Module_estimation_direction_score_DFcar(mmatrice_param_DFcar_echantillon_courant, mmatrice_param_DFcar_echantillon_modele_1_AP, mmatrice_param_DFcar_echantillon_modele_2_AP, mmatrice_param_DFcar_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, 0);
                        distance_DFcar_ordre = ordre_score_DFcar.method_Module_estimation_ordre_score_DFcar(mmatrice_param_DFcar_echantillon_courant, mmatrice_param_DFcar_echantillon_modele_1_AP, mmatrice_param_DFcar_echantillon_modele_2_AP, mmatrice_param_DFcar_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, 0);
                        distance_DFcar_forme = forme_score_DFcar.method_Module_estimation_forme_score_DFcar(mmatrice_param_DFcar_echantillon_courant, mmatrice_param_DFcar_echantillon_modele_1_AP, mmatrice_param_DFcar_echantillon_modele_2_AP, mmatrice_param_DFcar_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, 0);

                        mm_distance_DFcar[numero_label, 0] = distance_DFcar_direction;
                        mm_distance_DFcar[numero_label, 1] = distance_DFcar_ordre;
                        mm_distance_DFcar[numero_label, 2] = distance_DFcar_forme;

                    }

                    string chemin_acces_fichier_distances_DFcar = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DFcar.bin";
                    string chemin_acces_fichier_distances_DFcar_0 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DFcar_0.bin";
                    
                    if (autorisation_distance_test_faux == 0)
                    {
                        WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DFcar_0);
                        WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_DFcar_0, mm_distance_DFcar);
                    }
                    WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DFcar);
                    WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_DFcar, mm_distance_DFcar);
                    string chemin_acces_fichier_distances_DFcar1 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DFcar.txt";
                    string chemin_acces_fichier_distances_DFcar_01 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DFcar_0.txt";
                    if (autorisation_distance_test_faux == 0)
                    {
                        WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DFcar_01);
                        WRDfile.Method_WriteFile(mm_distance_DFcar, chemin_acces_fichier_distances_DFcar_01);
                    }
                    WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DFcar1);
                    WRDfile.Method_WriteFile(mm_distance_DFcar, chemin_acces_fichier_distances_DFcar1);
                    ////////////////
                    classe_choisie = "fausse_forme";

                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/";
                    chemin_acces_sous_dossier_param = chemin_acces_dossier_examiner + type_de_script_p + "_" + label_du_script_choisi + "_isole" + "/matrice_param_DFcar/";

                    num_echantillon_modele_1_AP = 1;
                    num_echantillon_modele_2_AP = 2;
                    num_echantillon_modele_3_AP = 3;

                    //num_echantillon_modele_3_AS = 2;
                    chemin_acces_param_DFcar_echantillon_modele_1_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DFcar/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_1_AP + ".bin";
                    chemin_acces_param_DFcar_echantillon_modele_2_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DFcar/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_2_AP + ".bin";
                    chemin_acces_param_DFcar_echantillon_modele_3_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DFcar/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_3_AP + ".bin";

                    mmatrice_param_DFcar_echantillon_modele_1_AP = WRDfile.Method_ReadFile_Bin_Param_DFcar(chemin_acces_param_DFcar_echantillon_modele_1_AP);
                    mmatrice_param_DFcar_echantillon_modele_2_AP = WRDfile.Method_ReadFile_Bin_Param_DFcar(chemin_acces_param_DFcar_echantillon_modele_2_AP);
                    mmatrice_param_DFcar_echantillon_modele_3_AP = WRDfile.Method_ReadFile_Bin_Param_DFcar(chemin_acces_param_DFcar_echantillon_modele_3_AP);

                    var av_files_n12 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin");
                    size_av_files_n2 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin").Length;
                    mm_distance_DFcar = new double[size_av_files_n2, 3];

                    for (int numero_label = 0; numero_label < size_av_files_n2; numero_label++)
                    {

                        chemin_acces_fichier_param_echantillon_courant = av_files_n12[numero_label];

                        mmatrice_param_DFcar_echantillon_courant = WRDfile.Method_ReadFile_Bin_Param_DFcar(chemin_acces_fichier_param_echantillon_courant);
                        //[distance_DF_direction]= Module_estimation_direction_score_DF(mmatrice_param_DF_echantillon_courant, mmatrice_param_DF_echantillon_modele_1_AP, mmatrice_param_DF_echantillon_modele_2_AP, mmatrice_param_DF_echantillon_modele_3_AP, label_du_script_choisi, type_de_script_p, type_de_script_q, autorisation_consideration_distance_test_faux)

                        distance_DFcar_direction = direction_score_DFcar.method_Module_estimation_direction_score_DFcar(mmatrice_param_DFcar_echantillon_courant, mmatrice_param_DFcar_echantillon_modele_1_AP, mmatrice_param_DFcar_echantillon_modele_2_AP, mmatrice_param_DFcar_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, 0);
                        distance_DFcar_ordre = ordre_score_DFcar.method_Module_estimation_ordre_score_DFcar(mmatrice_param_DFcar_echantillon_courant, mmatrice_param_DFcar_echantillon_modele_1_AP, mmatrice_param_DFcar_echantillon_modele_2_AP, mmatrice_param_DFcar_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, 0);
                        distance_DFcar_forme = forme_score_DFcar.method_Module_estimation_forme_score_DFcar(mmatrice_param_DFcar_echantillon_courant, mmatrice_param_DFcar_echantillon_modele_1_AP, mmatrice_param_DFcar_echantillon_modele_2_AP, mmatrice_param_DFcar_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, 0);

                        mm_distance_DFcar[numero_label, 0] = distance_DFcar_direction;
                        mm_distance_DFcar[numero_label, 1] = distance_DFcar_ordre;
                        mm_distance_DFcar[numero_label, 2] = distance_DFcar_forme;


                    }
                    chemin_acces_fichier_distances_DFcar = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DFcar.bin";
                    chemin_acces_fichier_distances_DFcar_0 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DFcar_0.bin";
                    chemin_acces_fichier_distances_DFcar1 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DFcar.txt";
                   
                    if (autorisation_distance_test_faux == 0)
                    {
                        WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DFcar_0);
                        WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_DFcar_0, mm_distance_DFcar);
                    }
                    WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DFcar);
                    WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_DFcar, mm_distance_DFcar);
                    chemin_acces_fichier_distances_DFcar1 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DFcar.txt";
                    chemin_acces_fichier_distances_DFcar_01 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DFcar_0.txt";
                    if (autorisation_distance_test_faux == 0)
                    {
                        WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DFcar_01);
                        WRDfile.Method_WriteFile(mm_distance_DFcar, chemin_acces_fichier_distances_DFcar_01);
                    }
                    WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DFcar1);
                    WRDfile.Method_WriteFile(mm_distance_DFcar, chemin_acces_fichier_distances_DFcar1);

                    /////////////
                    classe_choisie = "fausse_direction";

                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/";
                    chemin_acces_sous_dossier_param = chemin_acces_dossier_examiner + type_de_script_p + "_" + label_du_script_choisi + "_isole" + "/matrice_param_DFcar/";

                    num_echantillon_modele_1_AP = 1;
                    num_echantillon_modele_2_AP = 2;
                    num_echantillon_modele_3_AP = 3;

                    //num_echantillon_modele_3_AS = 2;
                    chemin_acces_param_DFcar_echantillon_modele_1_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DFcar/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_1_AP + ".bin";
                    chemin_acces_param_DFcar_echantillon_modele_2_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DFcar/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_2_AP + ".bin";
                    chemin_acces_param_DFcar_echantillon_modele_3_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DFcar/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_3_AP + ".bin";

                    mmatrice_param_DFcar_echantillon_modele_1_AP = WRDfile.Method_ReadFile_Bin_Param_DFcar(chemin_acces_param_DFcar_echantillon_modele_1_AP);
                    mmatrice_param_DFcar_echantillon_modele_2_AP = WRDfile.Method_ReadFile_Bin_Param_DFcar(chemin_acces_param_DFcar_echantillon_modele_2_AP);
                    mmatrice_param_DFcar_echantillon_modele_3_AP = WRDfile.Method_ReadFile_Bin_Param_DFcar(chemin_acces_param_DFcar_echantillon_modele_3_AP);

                    var av_files_n112 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin");
                    size_av_files_n2 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin").Length;
                    mm_distance_DFcar = new double[size_av_files_n2, 3];

                    for (int numero_label = 0; numero_label < size_av_files_n2; numero_label++)
                    {

                        chemin_acces_fichier_param_echantillon_courant = av_files_n112[numero_label];

                        mmatrice_param_DFcar_echantillon_courant = WRDfile.Method_ReadFile_Bin_Param_DFcar(chemin_acces_fichier_param_echantillon_courant);
                        //[distance_DF_direction]= Module_estimation_direction_score_DF(mmatrice_param_DF_echantillon_courant, mmatrice_param_DF_echantillon_modele_1_AP, mmatrice_param_DF_echantillon_modele_2_AP, mmatrice_param_DF_echantillon_modele_3_AP, label_du_script_choisi, type_de_script_p, type_de_script_q, autorisation_consideration_distance_test_faux)

                        distance_DFcar_direction = direction_score_DFcar.method_Module_estimation_direction_score_DFcar(mmatrice_param_DFcar_echantillon_courant, mmatrice_param_DFcar_echantillon_modele_1_AP, mmatrice_param_DFcar_echantillon_modele_2_AP, mmatrice_param_DFcar_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, 0);
                        distance_DFcar_ordre = ordre_score_DFcar.method_Module_estimation_ordre_score_DFcar(mmatrice_param_DFcar_echantillon_courant, mmatrice_param_DFcar_echantillon_modele_1_AP, mmatrice_param_DFcar_echantillon_modele_2_AP, mmatrice_param_DFcar_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, 0);
                        distance_DFcar_forme = forme_score_DFcar.method_Module_estimation_forme_score_DFcar(mmatrice_param_DFcar_echantillon_courant, mmatrice_param_DFcar_echantillon_modele_1_AP, mmatrice_param_DFcar_echantillon_modele_2_AP, mmatrice_param_DFcar_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, 0);

                        mm_distance_DFcar[numero_label, 0] = distance_DFcar_direction;
                        mm_distance_DFcar[numero_label, 1] = distance_DFcar_ordre;
                        mm_distance_DFcar[numero_label, 2] = distance_DFcar_forme;



                    }
                    chemin_acces_fichier_distances_DFcar = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DFcar.bin";
                    chemin_acces_fichier_distances_DFcar_0 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DFcar_0.bin";
                    
                    if (autorisation_distance_test_faux == 0)
                    {
                        WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DFcar_0);
                        WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_DFcar_0, mm_distance_DFcar);
                    }
                    WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DFcar);
                    WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_DFcar, mm_distance_DFcar);
                    chemin_acces_fichier_distances_DFcar1 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DFcar.txt";
                    chemin_acces_fichier_distances_DFcar_01 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DFcar_0.txt";
                    if (autorisation_distance_test_faux == 0)
                    {
                        WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DFcar_01);
                        WRDfile.Method_WriteFile(mm_distance_DFcar, chemin_acces_fichier_distances_DFcar_01);
                    }
                    WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DFcar1);
                    WRDfile.Method_WriteFile(mm_distance_DFcar, chemin_acces_fichier_distances_DFcar1);
                    ///////////
                    classe_choisie = "faux_ordre";

                    chemin_acces_dossier_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/";
                    chemin_acces_sous_dossier_param = chemin_acces_dossier_examiner + type_de_script_p + "_" + label_du_script_choisi + "_isole" + "/matrice_param_DFcar/";

                    num_echantillon_modele_1_AP = 1;
                    num_echantillon_modele_2_AP = 2;
                    num_echantillon_modele_3_AP = 3;

                    //num_echantillon_modele_3_AS = 2;
                    chemin_acces_param_DFcar_echantillon_modele_1_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DFcar/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_1_AP + ".bin";
                    chemin_acces_param_DFcar_echantillon_modele_2_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DFcar/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_2_AP + ".bin";
                    chemin_acces_param_DFcar_echantillon_modele_3_AP = path + "/base_de_" + type_de_script_q + "/echantillons_modeles/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/matrice_param_DFcar/" + label_du_script_choisi + "_modele_" + num_echantillon_modele_3_AP + ".bin";

                    mmatrice_param_DFcar_echantillon_modele_1_AP = WRDfile.Method_ReadFile_Bin_Param_DFcar(chemin_acces_param_DFcar_echantillon_modele_1_AP);
                    mmatrice_param_DFcar_echantillon_modele_2_AP = WRDfile.Method_ReadFile_Bin_Param_DFcar(chemin_acces_param_DFcar_echantillon_modele_2_AP);
                    mmatrice_param_DFcar_echantillon_modele_3_AP = WRDfile.Method_ReadFile_Bin_Param_DFcar(chemin_acces_param_DFcar_echantillon_modele_3_AP);

                    var av_files_n122 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin");
                    size_av_files_n2 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param, "*.bin").Length;
                    mm_distance_DFcar = new double[size_av_files_n2, 3];

                    for (int numero_label = 0; numero_label < size_av_files_n2; numero_label++)
                    {

                        chemin_acces_fichier_param_echantillon_courant = av_files_n122[numero_label];

                        mmatrice_param_DFcar_echantillon_courant = WRDfile.Method_ReadFile_Bin_Param_DFcar(chemin_acces_fichier_param_echantillon_courant);
                        //[distance_DF_direction]= Module_estimation_direction_score_DF(mmatrice_param_DF_echantillon_courant, mmatrice_param_DF_echantillon_modele_1_AP, mmatrice_param_DF_echantillon_modele_2_AP, mmatrice_param_DF_echantillon_modele_3_AP, label_du_script_choisi, type_de_script_p, type_de_script_q, autorisation_consideration_distance_test_faux)

                        distance_DFcar_direction = direction_score_DFcar.method_Module_estimation_direction_score_DFcar(mmatrice_param_DFcar_echantillon_courant, mmatrice_param_DFcar_echantillon_modele_1_AP, mmatrice_param_DFcar_echantillon_modele_2_AP, mmatrice_param_DFcar_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, 0);
                        distance_DFcar_ordre = ordre_score_DFcar.method_Module_estimation_ordre_score_DFcar(mmatrice_param_DFcar_echantillon_courant, mmatrice_param_DFcar_echantillon_modele_1_AP, mmatrice_param_DFcar_echantillon_modele_2_AP, mmatrice_param_DFcar_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, 0);
                        distance_DFcar_forme = forme_score_DFcar.method_Module_estimation_forme_score_DFcar(mmatrice_param_DFcar_echantillon_courant, mmatrice_param_DFcar_echantillon_modele_1_AP, mmatrice_param_DFcar_echantillon_modele_2_AP, mmatrice_param_DFcar_echantillon_modele_3_AP, label_du_script_choisi, path, type_de_script_p, type_de_script_q, 0);

                        mm_distance_DFcar[numero_label, 0] = distance_DFcar_direction;
                        mm_distance_DFcar[numero_label, 1] = distance_DFcar_ordre;
                        mm_distance_DFcar[numero_label, 2] = distance_DFcar_forme;


                    }
                    chemin_acces_fichier_distances_DFcar = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DFcar.bin";
                   
                    if (autorisation_distance_test_faux == 0)
                    {
                        WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DFcar_0);
                        WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_DFcar_0, mm_distance_DFcar);
                    }
                    WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DFcar);
                    WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_distances_DFcar, mm_distance_DFcar);
                    chemin_acces_fichier_distances_DFcar1 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DFcar.txt";
                    chemin_acces_fichier_distances_DFcar_01 = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/" + classe_choisie + "/distance_DFcar_0.txt";
                    if (autorisation_distance_test_faux == 0)
                    {
                        WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DFcar_01);
                        WRDfile.Method_WriteFile(mm_distance_DFcar, chemin_acces_fichier_distances_DFcar_01);
                    }
                    WRDfile.Method_DeleteFiles(chemin_acces_fichier_distances_DFcar1);
                    WRDfile.Method_WriteFile(mm_distance_DFcar, chemin_acces_fichier_distances_DFcar1);

                }

                // %%%%%%%%%%%%%%%%%%%% calcul des seuils DF %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                if ((type_procedure_apprentissage == 1) || ((type_procedure_apprentissage == 2) && (type_extracteur == 2)) || ((type_procedure_apprentissage == 3) && (type_approche == 2)) || ((type_procedure_apprentissage == 4) && (type_extracteur == 2)))
                {
                    string chemin_acces_dossier_distance_a_examiner = path + "/Base_apprentissage_distance/" + type_de_script_p + "/";

                    chemin_acces_dossier_matrices_distance = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/";
                    chemin_acces_dossier_matrices_seuils = path + "/Record_des_Valeurs_des_Seuils_Appris/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/";

                    estimation_seuil.Method_Estimation_et_Enregistrement_des_Seuils_DF(label_du_script_choisi, type_de_script_p, path);

                }

                // %%%%%%%%%%%%%%%%%%%% calcul des seuils DF cartesienne %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                if ((type_procedure_apprentissage == 1) || ((type_procedure_apprentissage == 2) && (type_extracteur == 3)) || ((type_procedure_apprentissage == 3) && (type_approche == 3)) || ((type_procedure_apprentissage == 4) && (type_extracteur == 3)))
                {
                    string chemin_acces_dossier_distance_a_examiner = path + "/Base_apprentissage_distance/" + type_de_script_p + "/";

                    chemin_acces_dossier_matrices_distance = path + "/Base_apprentissage_distance/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/";
                    chemin_acces_dossier_matrices_seuils = path + "/Record_des_Valeurs_des_Seuils_Appris/" + type_de_script_p + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole/";

                    estimation_seuil.Method_Estimation_et_Enregistrement_des_Seuils_DFcar(label_du_script_choisi, type_de_script_p, path);

                }


            }
        }


        public void Method_Traitement_apprentissage1(double[,] tes, string label_du_script_choisi, string classe_choisie, string type_de_script, string path)
        {

            if (type_de_script.Equals("Mot___"))
            {
                type_de_script_p = "Mot";
                type_de_script_q = "mots";
            }
            else if (type_de_script.Equals("Lettre"))
            {
                type_de_script_p = "Lettre";
                type_de_script_q = "lettres";
            }
            if (type_de_script.Equals("Lettre"))
            {
                premier_caractere = 'L';
                deuxieme_caractere = 'e';
                troisieme_caractere = 't';
                nombre_de_caractere = 6;
            }
            else
            {
                premier_caractere = 'M';
                deuxieme_caractere = 'o';
                troisieme_caractere = 't';
                nombre_de_caractere = 3;

            }
            //type_de_script = "Lettre";
            if (classe_choisie.Equals("modeles"))
            {

                classe_choisie = "modeles";
                chemin_acces_dossier_destination_xy_traject = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole" + "/xy_traject/";
                //Console.WriteLine(chemin_acces_dossier_destination_xy_traject);
                chemin_acces_dossier_destination_matrice_param = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole" + "/matrice_param/";
                string chemin_acces_dossier_destination_matrice_param_DFcar = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole" + "/matrice_param_DFcar/";
                string chemin_acces_dossier_destination_matrice_param_DFcar1= chemin_acces_dossier_destination_matrice_param_DFcar+"matrice_modele_globale.bin";
                if (!Directory.Exists(chemin_acces_dossier_destination_matrice_param_DFcar))
                { Directory.CreateDirectory(chemin_acces_dossier_destination_matrice_param_DFcar); }
                if (!Directory.Exists(chemin_acces_dossier_destination_xy_traject))
                { Directory.CreateDirectory(chemin_acces_dossier_destination_xy_traject); }
                if (!Directory.Exists(chemin_acces_dossier_destination_matrice_param))
                { Directory.CreateDirectory(chemin_acces_dossier_destination_matrice_param); }
                int number_av_files_n2 = System.IO.Directory.GetFiles(chemin_acces_dossier_destination_xy_traject, "*.bin").Length;
                num_echantillon = number_av_files_n2 + 1;
                classe_choisie = "modele";
                chemin_acces_fichier_destination_xy_traject = chemin_acces_dossier_destination_xy_traject + label_du_script_choisi + "_" + classe_choisie + "_" + num_echantillon + ".bin";
               WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_destination_xy_traject, tes);
                string chemin_acces_fichier_destination_xy_traject1 = chemin_acces_dossier_destination_xy_traject + label_du_script_choisi + "_" + classe_choisie + "_" + num_echantillon + ".txt";
                //WRDfile.Method_WriteFile(tes, chemin_acces_fichier_destination_xy_traject1);

                chemin_acces_fichier_destination_matrice_param = chemin_acces_dossier_destination_matrice_param + label_du_script_choisi + "_" + classe_choisie + "_" + num_echantillon + ".bin";
                // string chemin_acces_fichier_destination_matrice_param2 = chemin_acces_dossier_destination_matrice_param + label_du_script_choisi + "_" + classe_choisie + "_" + num_echantillon + ".txt";
                Raffinage_de_la_quantification_spatiale raffinage = new Raffinage_de_la_quantification_spatiale();
                calibrage_de_l_echantillonnage_V1 calib_ech = new calibrage_de_l_echantillonnage_V1();
               

              //  double[,] point_Trajectoire_avec_quantification_spatiale_rafinnee = raffinage.method_Raffinage_de_la_quantification_spatiale(tes);
             //   double[,] point_Trajectoire_reechantillonnee = calib_ech.method_calibrage_de_l_echantillonnage_V1(point_Trajectoire_avec_quantification_spatiale_rafinnee, label_du_script_choisi, type_de_script, path);


                mmatrice_param_echantillon_test = lecture.method_mmatrice_param_1(tes, label_du_script_choisi);
                mmatrice_param_DFcar_echantillon_courant = lecturecar.method_mmatrice_param_DF_car(tes, label_du_script_choisi);
                ////WRDfile.Method_WriteFile(mmatrice_param_echantillon_test, chemin_acces_fichier_destination_matrice_param2);
               write_matrice_globale matrice_globale = new write_matrice_globale();
                matrice_globale.method_write_matrice_globale(chemin_acces_dossier_destination_matrice_param_DFcar1, mmatrice_param_DFcar_echantillon_courant);
               WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_destination_matrice_param, mmatrice_param_echantillon_test);
                ////WRDfile.Method_WriteFile_Bin_DF(chemin_acces_fichier_destination_matrice_param, mmatrice_param_echantillon_test);


            }
            else
            {


                // *************** enregistrement de la trajectoire de l'échantillon choisi  **********
                // ************************************************************************************

                chemin_acces_dossier_destination_xy_traject = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole" + "/xy_traject/";
                //Console.WriteLine(chemin_acces_dossier_destination_xy_traject);
                chemin_acces_dossier_destination_matrice_param = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isole" + "/matrice_param/";
                if (!Directory.Exists(chemin_acces_dossier_destination_xy_traject))
                { Directory.CreateDirectory(chemin_acces_dossier_destination_xy_traject); }
                if (!Directory.Exists(chemin_acces_dossier_destination_matrice_param))
                { Directory.CreateDirectory(chemin_acces_dossier_destination_matrice_param); }
                int number_av_files_n2 = System.IO.Directory.GetFiles(chemin_acces_dossier_destination_xy_traject, "*.bin").Length;
                num_echantillon = number_av_files_n2 + 1;
                chemin_acces_fichier_destination_xy_traject = chemin_acces_dossier_destination_xy_traject + label_du_script_choisi + "_" + classe_choisie + "_" + num_echantillon + ".bin";
               WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_destination_xy_traject, tes);
                chemin_acces_fichier_destination_matrice_param = chemin_acces_dossier_destination_matrice_param + label_du_script_choisi + "_" + classe_choisie + "_" + num_echantillon + ".bin";
                string chemin_acces_fichier_destination_xy_traject2 = chemin_acces_dossier_destination_xy_traject + label_du_script_choisi + "_" + classe_choisie + "_" + num_echantillon + ".txt";
                Raffinage_de_la_quantification_spatiale raffinage = new Raffinage_de_la_quantification_spatiale();
                calibrage_de_l_echantillonnage_V1 calib_ech = new calibrage_de_l_echantillonnage_V1();
                double[,] point_Trajectoire_avec_quantification_spatiale_rafinnee = raffinage.method_Raffinage_de_la_quantification_spatiale(tes);
                double[,] point_Trajectoire_reechantillonnee = calib_ech.method_calibrage_de_l_echantillonnage_V1(point_Trajectoire_avec_quantification_spatiale_rafinnee, label_du_script_choisi, type_de_script, path);
                mmatrice_param_echantillon_test = lecture.method_mmatrice_param_1(point_Trajectoire_reechantillonnee, label_du_script_choisi);
                //mmatrice_param_echantillon_test = lecture.method_mmatrice_param_1(tes, label_du_script_choisi);
                //WRDfile.Method_WriteFile(tes, chemin_acces_fichier_destination_xy_traject2);
                WRDfile.Method_WriteFile_Bin(chemin_acces_fichier_destination_matrice_param, mmatrice_param_echantillon_test);
            }
        }





        public void Addseuil_rang()
        {
            ////Lettre Ba
            /* string path = path + "/Record_des_Valeurs_des_Seuils_Appris/Lettre/Lettre_Ba_isole/";
             string chemin_acces_seuils_parametrique = path + "seuil_parametrique.txt";
             if (!Directory.Exists(path))
             { Directory.CreateDirectory(path); }
             double[] seuil_parametrique1 = new double[6];
             seuil_parametrique1[0] = 1.5; //2.33;//0.7;
             seuil_parametrique1[1] = 0.8;  //1.21; //0.5;
             seuil_parametrique1[2] = 1;//0.28;//2.73; //1;
             seuil_parametrique1[3] = 0.9; //0.2;//1.65; //0.8;
             seuil_parametrique1[4] = 1;//1.06;//0.43;
             seuil_parametrique1[5] = 0.4;// 1; //0.64;
             RDF.Method_WriteFile_double(seuil_parametrique1, chemin_acces_seuils_parametrique);
             string chemin_acces_seuils_structurelle = path + "seuil_structurelle.txt";
             double[] seuil_structurelle1 = new double[6];
             seuil_structurelle1[0] = 0.44;
             seuil_structurelle1[1] = 0.28;
             seuil_structurelle1[2] = 0.85;
             seuil_structurelle1[3] = 0.2;
             seuil_structurelle1[4] = 0.65;
             seuil_structurelle1[5] = 0.4;// 1; //0.64;
             RDF.Method_WriteFile_double(seuil_structurelle1, chemin_acces_seuils_structurelle);
             ////Lettre Tad
             string path1 = path + "/Record_des_Valeurs_des_Seuils_Appris/Lettre/Lettre_Tad_isole/";
             string chemin_acces_seuils_parametrique1 = path1 + "seuil_parametrique.txt";
             if (!Directory.Exists(path1))
             { Directory.CreateDirectory(path1); }
             double[] seuil_parametrique2 = new double[6];
             seuil_parametrique2[0] = 1.547157106;
             seuil_parametrique2[1] = 1.378364876;
             seuil_parametrique2[2] = 1.703754584;
             seuil_parametrique2[3] = 1.468147503;
             seuil_parametrique2[4] = 0.845406864;
             seuil_parametrique2[5] = 0.703157569;
             RDF.Method_WriteFile_double(seuil_parametrique2, chemin_acces_seuils_parametrique1);
             string chemin_acces_seuils_structurelle1 = path1 + "seuil_structurelle.txt";
             double[] seuil_structurelle2 = new double[6];
             seuil_structurelle2[0] = 0.302702969;
             seuil_structurelle2[1] = 0.201368284;
             seuil_structurelle2[2] = 0.47910464;
             seuil_structurelle2[3] = 0.451157727;
             seuil_structurelle2[4] = 0.358290062;
             seuil_structurelle2[5] = 0.335684999;
             RDF.Method_WriteFile_double(seuil_structurelle2, chemin_acces_seuils_structurelle1);
             //rang   
             int[] rang = new int[3];
             rang[0] = 1;
             rang[1] = 3;
             rang[2] = 2;
             string chemin_acces_dossier_destination_rang = path + "/base_de_lettres/echantillons_modeles/Lettre_Ba_isole/ ";
             if (!Directory.Exists(chemin_acces_dossier_destination_rang))
             { Directory.CreateDirectory(chemin_acces_dossier_destination_rang); }
             RDF.Method_WriteFile_Int(rang, chemin_acces_dossier_destination_rang + "Rangs_ordonnes_des_echantillons_modeles_AP.txt");
             RDF.Method_WriteFile_Int(rang, chemin_acces_dossier_destination_rang + "Rangs_ordonnes_des_echantillons_modeles_AS.txt");
             string chemin_acces_dossier_destination_rang1 = path + "/base_de_lettres/echantillons_modeles/Lettre_Tad_isole/ ";
             if (!Directory.Exists(chemin_acces_dossier_destination_rang1))
             { Directory.CreateDirectory(chemin_acces_dossier_destination_rang1); }
             RDF.Method_WriteFile_Int(rang, chemin_acces_dossier_destination_rang1 + "Rangs_ordonnes_des_echantillons_modeles_AP.txt");
             RDF.Method_WriteFile_Int(rang, chemin_acces_dossier_destination_rang1 + "Rangs_ordonnes_des_echantillons_modeles_AS.txt");

             string chemin_acces_dossier_destination_rang2 = path + "/base_de_mots/echantillons_modeles/Mot_Koraton_isole/ ";
             if (!Directory.Exists(chemin_acces_dossier_destination_rang2))
             { Directory.CreateDirectory(chemin_acces_dossier_destination_rang2); }
             RDF.Method_WriteFile_Int(rang, chemin_acces_dossier_destination_rang2 + "Rangs_ordonnes_des_echantillons_modeles_AP.txt");
             RDF.Method_WriteFile_Int(rang, chemin_acces_dossier_destination_rang2 + "Rangs_ordonnes_des_echantillons_modeles_AS.txt");



                string path3 = path + "/Record_des_Valeurs_des_Seuils_Appris/Mot/Mot_Koraton_isole/";
             string chemin_acces_seuils_structurelle3 = path3 + "seuil_structurelle.txt";
             if (!Directory.Exists(path3))
             { Directory.CreateDirectory(path3); }
             double[] seuil_structurelle3 = new double[6];
             seuil_structurelle1[0] = 0.302693264;
             seuil_structurelle1[1] = 0.191614685;
             seuil_structurelle1[2] = 0.282585336;
             seuil_structurelle1[3] = 0.269652487;
             seuil_structurelle1[4] = 0.246405387;
             seuil_structurelle1[5] = 0.203513643;
             //WRDfile.Method_WriteFile_double(seuil_structurelle3, chemin_acces_seuils_structurelle3);
             string chemin_acces_seuils_parametrique3 = path3 + "seuil_parametrique.txt";

             double[] seuil_parametrique3 = new double[6];
             seuil_parametrique1[0] = 0.618067522;
             seuil_parametrique1[1] = 0.585945328;
             seuil_parametrique1[2] = 1.595908685;
             seuil_parametrique1[3] = 1.299572199;
             seuil_parametrique1[4] = 0.521304069;
             seuil_parametrique1[5] = 0.4883037;
             //WRDfile.Method_WriteFile_double(seuil_parametrique3, chemin_acces_seuils_parametrique3);
             */
            ////Lettre Qaf
            /*string path11 = path + "/Record_des_Valeurs_des_Seuils_Appris/Lettre/Lettre_Qaf_isole/";
            string chemin_acces_seuils_parametrique11 = path11 + "seuil_parametrique.txt";
            if (!Directory.Exists(path11))
            { Directory.CreateDirectory(path11); }
            double[] seuil_parametrique11 = new double[6];          

            seuil_parametrique11[0] = 2.318566059; //2.33;//0.7;
            seuil_parametrique11[1] = 1.630553602;  //1.21; //0.5;
            seuil_parametrique11[2] = 3.203156546;//0.28;//2.73; //1;
            seuil_parametrique11[3] = 1.787625512; //0.2;//1.65; //0.8;
            seuil_parametrique11[4] = 1.382889337;//1.06;//0.43;
            seuil_parametrique11[5] = 1.330705321;// 1; //0.64;
            RDF.Method_WriteFile_double(seuil_parametrique11, chemin_acces_seuils_parametrique11);
            string chemin_acces_seuils_structurelle11 = path11 + "seuil_structurelle.txt";
            double[] seuil_structurelle11 = new double[6];
            seuil_structurelle11[0] = 0.373085349;
            seuil_structurelle11[1] = 0.120116422;
            seuil_structurelle11[2] = 0.481954905;
            seuil_structurelle11[3] = 0.26223399;
            seuil_structurelle11[4] = 0.381441311;
            seuil_structurelle11[5] = 0.300270005;// 1; //0.64;
            RDF.Method_WriteFile_double(seuil_structurelle11, chemin_acces_seuils_structurelle11);*/
            /// Lettre Ha

            /* string path = path + "/Record_des_Valeurs_des_Seuils_Appris/Lettre/Lettre_Ha_isole/";
             string chemin_acces_seuils_parametrique = path + "seuil_parametrique.txt";
             if (!Directory.Exists(path))
             { Directory.CreateDirectory(path); }
             double[] seuil_parametrique1 = new double[6];
             seuil_parametrique1[0] = 2.047791482; //2.33;//0.7;
             seuil_parametrique1[1] = 1.505935024;  //1.21; //0.5;
             seuil_parametrique1[2] = 2.214634455;//0.28;//2.73; //1;
             seuil_parametrique1[3] = 2.092909115; //0.2;//1.65; //0.8;
             seuil_parametrique1[4] = 1.855614198;//1.06;//0.43;
             seuil_parametrique1[5] = 1.070087503;// 1; //0.64;
             RDF.Method_WriteFile_double(seuil_parametrique1, chemin_acces_seuils_parametrique);
             string chemin_acces_seuils_structurelle = path + "seuil_structurelle.txt";
             double[] seuil_structurelle1 = new double[6];
             seuil_structurelle1[0] = 0.520487094;
             seuil_structurelle1[1] = 0.256599342;
             seuil_structurelle1[2] = 0.559870954;
             seuil_structurelle1[3] = 0.361038876;
             seuil_structurelle1[4] = 0.578822174;
             seuil_structurelle1[5] = 0.474117601;// 1; //0.64;
             RDF.Method_WriteFile_double(seuil_structurelle1, chemin_acces_seuils_structurelle);
             // Lettre Seen

             string path1 = path + "/Record_des_Valeurs_des_Seuils_Appris/Lettre/Lettre_Dal_isole/";
             string chemin_acces_seuils_parametrique1 = path1 + "seuil_parametrique.txt";
             if (!Directory.Exists(path1))
             { Directory.CreateDirectory(path1); }
             double[] seuil_parametrique2 = new double[6];

             seuil_parametrique2[0] = 2.318566059; //2.33;//0.7;
             seuil_parametrique2[1] = 1.630553602;  //1.21; //0.5;
             seuil_parametrique2[2] = 3.203156546;//0.28;//2.73; //1;
             seuil_parametrique2[3] = 1.787625512; //0.2;//1.65; //0.8;
             seuil_parametrique2[4] = 1.382889337;//1.06;//0.43;
             seuil_parametrique2[5] = 1.330705321;// 1; //0.64;
             RDF.Method_WriteFile_double(seuil_parametrique2, chemin_acces_seuils_parametrique1);
             string chemin_acces_seuils_structurelle1 = path1 + "seuil_structurelle.txt";
             double[] seuil_structurelle2 = new double[6];
             seuil_structurelle2[0] = 0.373085349;
             seuil_structurelle2[1] = 0.120116422;
             seuil_structurelle2[2] = 0.481954905;
             seuil_structurelle2[3] = 0.26223399;
             seuil_structurelle2[4] = 0.381441311;
             seuil_structurelle1[5] = 0.300270005;// 1; //0.64;
             RDF.Method_WriteFile_double(seuil_structurelle2, chemin_acces_seuils_structurelle1);
             //Lettre Dal

             string path2 = path + "/Record_des_Valeurs_des_Seuils_Appris/Lettre/Lettre_Seen_isole/";
             string chemin_acces_seuils_parametrique2 = path2 + "seuil_parametrique.txt";
             if (!Directory.Exists(path2))
             { Directory.CreateDirectory(path2); }
             double[] seuil_parametrique3 = new double[6];
             seuil_parametrique3[0] = 1.525577892; //2.33;//0.7;
             seuil_parametrique3[1] = 1.463495989;  //1.21; //0.5;
             seuil_parametrique3[2] = 1.875362004;//0.28;//2.73; //1;
             seuil_parametrique3[3] = 1.420954054; //0.2;//1.65; //0.8;
             seuil_parametrique3[4] = 1.375193615;//1.06;//0.43;
             seuil_parametrique3[5] = 0.997060357;// 1; //0.64;
             RDF.Method_WriteFile_double(seuil_parametrique3, chemin_acces_seuils_parametrique2);
             string chemin_acces_seuils_structurelle2 = path2 + "seuil_structurelle.txt";
             double[] seuil_structurelle3 = new double[6];
             seuil_structurelle3[0] = 0.313979227;
             seuil_structurelle3[1] = 0.132405979;
             seuil_structurelle3[2] = 0.365257777;
             seuil_structurelle3[3] = 0.177925828;
             seuil_structurelle3[4] = 0.464223966;
             seuil_structurelle3[5] = 0.273887146;// 1; //0.64;
             RDF.Method_WriteFile_double(seuil_structurelle3, chemin_acces_seuils_structurelle2);*/
            // Lettre Waw
            /*  string path20 = path + "/Record_des_Valeurs_des_Seuils_Appris/Lettre/Lettre_Waw_isole/";
              string chemin_acces_seuils_parametrique20 = path20 + "seuil_parametrique.txt";
              if (!Directory.Exists(path20))
              { Directory.CreateDirectory(path20); }

              double[] seuil_parametrique20 = new double[6];
              seuil_parametrique20[0] = 2.259666248; //2.33;//0.7;
              seuil_parametrique20[1] = 1.7385805;  //1.21; //0.5;
              seuil_parametrique20[2] = 2.449080769;//0.28;//2.73; //1;
              seuil_parametrique20[3] = 2.145557225; //0.2;//1.65; //0.8;
              seuil_parametrique20[4] = 1.469922488;//1.06;//0.43;
              seuil_parametrique20[5] = 1.441342476;// 1; //0.64;
              RDF.Method_WriteFile_double(seuil_parametrique20, chemin_acces_seuils_parametrique20);
              string chemin_acces_seuils_structurelle20 = path20 + "seuil_structurelle.txt";
              double[] seuil_structurelle20 = new double[6];
              seuil_structurelle20[0] = 0.502812476;
              seuil_structurelle20[1] = 0.187752609;
              seuil_structurelle20[2] = 0.289314287;
              seuil_structurelle20[3] = 0.245313523;
              seuil_structurelle20[4] = 0.524962748;
              seuil_structurelle20[5] = 0.519084241;// 1; //0.64;
              RDF.Method_WriteFile_double(seuil_structurelle20, chemin_acces_seuils_structurelle20);

              //Lettre Sad
              string path30 = path + "/Record_des_Valeurs_des_Seuils_Appris/Lettre/Lettre_Sad_isole/";
              string chemin_acces_seuils_parametrique30 = path30 + "seuil_parametrique.txt";
              if (!Directory.Exists(path30))
              { Directory.CreateDirectory(path30); }
              double[] seuil_parametrique30 = new double[6];
              seuil_parametrique30[0] = 1.455925926; //2.33;//0.7;
              seuil_parametrique30[1] = 1.28235746;  //1.21; //0.5;
              seuil_parametrique30[2] = 2.463714807;//0.28;//2.73; //1;
              seuil_parametrique30[3] = 2.050593564; //0.2;//1.65; //0.8;
              seuil_parametrique30[4] = 1.404512576;//1.06;//0.43;
              seuil_parametrique30[5] = 0.697366478;// 1; //0.64;
              RDF.Method_WriteFile_double(seuil_parametrique30, chemin_acces_seuils_parametrique30);
              string chemin_acces_seuils_structurelle30 = path20 + "seuil_structurelle.txt";
              double[] seuil_structurelle30 = new double[6];
              seuil_structurelle30[0] = 0.379503349;
              seuil_structurelle30[1] = 0.359008621;
              seuil_structurelle30[2] = 0.476626061;
              seuil_structurelle30[3] = 0.322791627;
              seuil_structurelle30[4] = 0.44535691;
              seuil_structurelle30[5] = 0.38628585;// 1; //0.64;
              RDF.Method_WriteFile_double(seuil_structurelle30, chemin_acces_seuils_structurelle30);
              //Lettre Kaf
              string path40 = path + "/Record_des_Valeurs_des_Seuils_Appris/Lettre/Lettre_Kaf_isole/";
              string chemin_acces_seuils_parametrique40 = path40 + "seuil_parametrique.txt";
              if (!Directory.Exists(path40))
              { Directory.CreateDirectory(path40); }
              double[] seuil_parametrique40 = new double[6];

              seuil_parametrique40[0] = 0.93; //2.33;//0.7;
              seuil_parametrique40[1] = 0.58;  //1.21; //0.5;
              seuil_parametrique40[2] = 0.9;//0.28;//2.73; //1;
              seuil_parametrique40[3] = 0.7; //0.2;//1.65; //0.8;
              seuil_parametrique40[4] = 0.6;//1.06;//0.43;
              seuil_parametrique40[5] = 0.4;// 1; //0.64;
              RDF.Method_WriteFile_double(seuil_parametrique40, chemin_acces_seuils_parametrique40);
              string chemin_acces_seuils_structurelle40 = path40 + "seuil_structurelle.txt";
              double[] seuil_structurelle40 = new double[6];
              seuil_structurelle40[0] = 0.379503349;
              seuil_structurelle40[1] = 0.359008621;
              seuil_structurelle40[2] = 0.476626061;
              seuil_structurelle40[3] = 0.322791627;
              seuil_structurelle40[4] = 0.44535691;
              seuil_structurelle40[5] = 0.38628585;// 1; //0.64;
              RDF.Method_WriteFile_double(seuil_structurelle40, chemin_acces_seuils_structurelle40);*/

        }
    }
}
