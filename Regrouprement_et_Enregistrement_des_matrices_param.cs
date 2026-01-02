using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Beta_elliptic_model
{
    class Regrouprement_et_Enregistrement_des_matrices_param
    {
        public int num_echantillon_modele_1_AP, drapeau_autorisation_apprentissage_partiel, size_label_choisi, nombre_de_caractere, num_echantillon, numero_echantillon_modele_1_en_caractere, numero_echantillon_modele_2_en_caractere, numero_echantillon_modele_3_en_caractere;
        public int num_echantillon_modele_2_AP, num_echantillon_modele_3_AP, num_echantillon_modele_1_AS, num_echantillon_modele_2_AS, num_echantillon_modele_3_AS;
        public int[] mat_cod_test, mat_cod_1, mat_cod_2, mat_cod_3, Rangs_ordonnes_des_echantillons_modeles_AP, Rangs_ordonnes_des_echantillons_modeles_AS;
        private double[] mmatrice_echantillon_modele_3;
        public string chemin_acces_sous_dossier_param_DF;
        private double[] mmatrice_echantillon_modele_2;
        private double[] mmatrice_echantillon_modele_1;
        List<double> cor_11 = new List<double>();
        List<double> cor_12 = new List<double>();
        public double distance_AS_direction, distance_AS_ordre, distance_AS_forme, distance_AP_direction, distance_AP_ordre, distance_AP_forme;
        public string chemin_acces_fichier_param_echantillon_courant, chemin_acces_param_echantillon_modele_1_AP, chemin_acces_param_echantillon_modele_2_AP, chemin_acces_param_echantillon_modele_3_AP, chemin_acces_param_echantillon_modele_1_AS, chemin_acces_param_echantillon_modele_2_AS, chemin_acces_param_echantillon_modele_3_AS, chemin_acces_dossier_label_modele, classe_de_l_ensemble_des_echantillons_references, classe_des_echantillons_candidats, type_de_script_p, type_de_script_q, label_choisi, label_choisi_sans_lettre, label;
        public string chemin_acces_dossier_matrices_seuils, chemin_acces_dossier_matrices_distance, chemin_acces_param_echantillon_modele, type_de_script, chemin_acces_fichier_echantillon_courant, chemin_acces_xy_echantillon_courant, chemin_acces_dossier_label, chemin_acces_sous_dossier_courant, chemin_acces_sous_dossier_param, chemin_acces_dossier_examiner, chemin_acces_dossier_distance, chemin_acces_fichier_distances_parametriques, chemin_acces_fichier_distances_structurelles, chemin_acces_param_echantillon_modele_1, chemin_acces_param_echantillon_modele_2, chemin_acces_param_echantillon_modele_3, chemin_acces_dossier_destination_xy_traject, chemin_acces_fichier_destination_matrice_param, chemin_acces_dossier_destination_matrice_param, chemin_acces_fichier_destination_xy_traject;
        private double[,] mmatrice_param_echantillon_modele_1_AP, mmatrice_param_echantillon_modele_1, mmatrice_param_echantillon_modele_2_AP, mmatrice_param_echantillon_modele_3_AP, mmatrice_param_echantillon_modele_1_AS, mmatrice_param_echantillon_modele_2_AS, mmatrice_param_echantillon_modele_3_AS;
        private double[,] xy_echantillon_courant;
        public int nbr_segment_echantillon;
        public WRDFile RDF = new WRDFile();
        public int size_av_files_n13;
        public double[,] matrice_globale_des_parametres_des_echantillons, matrice_param_echantillon;
        public string chemin_acces_echantillon_courant,chemin_acces_dossier_param_echantillons, chemin_acces_fichier_matrice_globale_param_DF;
        public void Method_Regroupement_Mat_Param(string label_du_script_choisi, string classe_choisie, string type_de_script_q, string  type_de_script_p, int type_approche, string path)
        {
            if (type_approche == 2)
            {
                ///////////////////////Modele//////////////////////////////////////////
                classe_choisie = "modeles";

                chemin_acces_dossier_param_echantillons = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie+"/" + type_de_script_p + "_" + label_du_script_choisi + "_isolé" + "/" + "matrice_param_DF/";
                chemin_acces_fichier_matrice_globale_param_DF = path + "/base_de_"+ type_de_script_q+"/echantillons_"+ classe_choisie+"/"+type_de_script_p+"_"+label_du_script_choisi+"_isolé"+"/"+"matrice_globale_param_DF_des_echantillons_"+classe_choisie+".bin";
                var av_files_n2 = System.IO.Directory.GetFiles(chemin_acces_dossier_param_echantillons);
                for (int numero_label = 0; numero_label < (av_files_n2.Length); numero_label++)
                {
                    chemin_acces_echantillon_courant = av_files_n2[numero_label];
                    matrice_param_echantillon = RDF.Method_ReadFile_Bin_Param(chemin_acces_echantillon_courant);
                    nbr_segment_echantillon = matrice_param_echantillon.GetLength(1);
                    matrice_param_echantillon = new double[matrice_param_echantillon.GetLength(0)+1, nbr_segment_echantillon];
                    //matrice_globale_des_parametres_des_echantillons = [matrice_globale_des_parametres_des_echantillons, matrice_param_echantillon];
                }
                size_av_files_n13=System.IO.Directory.GetFiles(chemin_acces_fichier_matrice_globale_param_DF).Length;
                if (!Directory.Exists(chemin_acces_fichier_matrice_globale_param_DF))
                {
                    var av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_fichier_matrice_globale_param_DF);
                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n13; numero_echantillon++)
                    {
                        string chemin = av_files_n13[numero_echantillon];
                        RDF.Method_DeleteFiles(chemin);
                    }
                }
                RDF.Method_WriteFile_Bin(chemin_acces_fichier_matrice_globale_param_DF, matrice_globale_des_parametres_des_echantillons);

                ///////////////////////Correcte//////////////////////////////////////////
                classe_choisie = "correcte";

                chemin_acces_dossier_param_echantillons = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isolé" + "/" + "matrice_param_DF/";
                chemin_acces_fichier_matrice_globale_param_DF = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isolé" + "/" + "matrice_globale_param_DF_des_echantillons_" + classe_choisie + ".bin";
                av_files_n2 = System.IO.Directory.GetFiles(chemin_acces_dossier_param_echantillons);
                for (int numero_label = 0; numero_label < (av_files_n2.Length); numero_label++)
                {
                    chemin_acces_echantillon_courant = av_files_n2[numero_label];
                    matrice_param_echantillon = RDF.Method_ReadFile_Bin_Param(chemin_acces_echantillon_courant);
                    nbr_segment_echantillon = matrice_param_echantillon.GetLength(1);
                    matrice_param_echantillon = new double[matrice_param_echantillon.GetLength(0) + 1, nbr_segment_echantillon];
                    //matrice_globale_des_parametres_des_echantillons = [matrice_globale_des_parametres_des_echantillons, matrice_param_echantillon];
                }
                size_av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_fichier_matrice_globale_param_DF).Length;
                if (!Directory.Exists(chemin_acces_fichier_matrice_globale_param_DF))
                {
                    var av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_fichier_matrice_globale_param_DF);
                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n13; numero_echantillon++)
                    {
                        string chemin = av_files_n13[numero_echantillon];
                        RDF.Method_DeleteFiles(chemin);
                    }
                }
                RDF.Method_WriteFile_Bin(chemin_acces_fichier_matrice_globale_param_DF, matrice_globale_des_parametres_des_echantillons);

                /// ///////////////////////fausse forme//////////////////////////////////////////
                classe_choisie = "fausse_forme";

                chemin_acces_dossier_param_echantillons = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isolé" + "/" + "matrice_param_DF/";
                chemin_acces_fichier_matrice_globale_param_DF = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isolé" + "/" + "matrice_globale_param_DF_des_echantillons_" + classe_choisie + ".bin";
                av_files_n2 = System.IO.Directory.GetFiles(chemin_acces_dossier_param_echantillons);
                for (int numero_label = 0; numero_label < (av_files_n2.Length); numero_label++)
                {
                    chemin_acces_echantillon_courant = av_files_n2[numero_label];
                    matrice_param_echantillon = RDF.Method_ReadFile_Bin_Param(chemin_acces_echantillon_courant);
                    nbr_segment_echantillon = matrice_param_echantillon.GetLength(1);
                    matrice_param_echantillon = new double[matrice_param_echantillon.GetLength(0) + 1, nbr_segment_echantillon];
                    //matrice_globale_des_parametres_des_echantillons = [matrice_globale_des_parametres_des_echantillons, matrice_param_echantillon];
                }
                size_av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_fichier_matrice_globale_param_DF).Length;
                if (!Directory.Exists(chemin_acces_fichier_matrice_globale_param_DF))
                {
                    var av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_fichier_matrice_globale_param_DF);
                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n13; numero_echantillon++)
                    {
                        string chemin = av_files_n13[numero_echantillon];
                        RDF.Method_DeleteFiles(chemin);
                    }
                }
                RDF.Method_WriteFile_Bin(chemin_acces_fichier_matrice_globale_param_DF, matrice_globale_des_parametres_des_echantillons);

                //////////////////////////////faux ordre /////////////////////////////////////
                classe_choisie = "faux_ordre";

                chemin_acces_dossier_param_echantillons = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isolé" + "/" + "matrice_param_DF/";
                chemin_acces_fichier_matrice_globale_param_DF = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isolé" + "/" + "matrice_globale_param_DF_des_echantillons_" + classe_choisie + ".bin";
                av_files_n2 = System.IO.Directory.GetFiles(chemin_acces_dossier_param_echantillons);
                for (int numero_label = 0; numero_label < (av_files_n2.Length); numero_label++)
                {
                    chemin_acces_echantillon_courant = av_files_n2[numero_label];
                    matrice_param_echantillon = RDF.Method_ReadFile_Bin_Param(chemin_acces_echantillon_courant);
                    nbr_segment_echantillon = matrice_param_echantillon.GetLength(1);
                    matrice_param_echantillon = new double[matrice_param_echantillon.GetLength(0) + 1, nbr_segment_echantillon];
                    //matrice_globale_des_parametres_des_echantillons = [matrice_globale_des_parametres_des_echantillons, matrice_param_echantillon];
                }
                size_av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_fichier_matrice_globale_param_DF).Length;
                if (!Directory.Exists(chemin_acces_fichier_matrice_globale_param_DF))
                {
                    var av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_fichier_matrice_globale_param_DF);
                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n13; numero_echantillon++)
                    {
                        string chemin = av_files_n13[numero_echantillon];
                        RDF.Method_DeleteFiles(chemin);
                    }
                }
                RDF.Method_WriteFile_Bin(chemin_acces_fichier_matrice_globale_param_DF, matrice_globale_des_parametres_des_echantillons);

                //////////////////////////fausse direction ///////////////////////////////
                classe_choisie = "fausse_direction";

                chemin_acces_dossier_param_echantillons = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isolé" + "/" + "matrice_param_DF/";
                chemin_acces_fichier_matrice_globale_param_DF = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isolé" + "/" + "matrice_globale_param_DF_des_echantillons_" + classe_choisie + ".bin";
                av_files_n2 = System.IO.Directory.GetFiles(chemin_acces_dossier_param_echantillons);
                for (int numero_label = 0; numero_label < (av_files_n2.Length); numero_label++)
                {
                    chemin_acces_echantillon_courant = av_files_n2[numero_label];
                    matrice_param_echantillon = RDF.Method_ReadFile_Bin_Param(chemin_acces_echantillon_courant);
                    nbr_segment_echantillon = matrice_param_echantillon.GetLength(1);
                    matrice_param_echantillon = new double[matrice_param_echantillon.GetLength(0) + 1, nbr_segment_echantillon];
                    //matrice_globale_des_parametres_des_echantillons = [matrice_globale_des_parametres_des_echantillons, matrice_param_echantillon];
                }
                size_av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_fichier_matrice_globale_param_DF).Length;
                if (!Directory.Exists(chemin_acces_fichier_matrice_globale_param_DF))
                {
                    var av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_fichier_matrice_globale_param_DF);
                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n13; numero_echantillon++)
                    {
                        string chemin = av_files_n13[numero_echantillon];
                        RDF.Method_DeleteFiles(chemin);
                    }
                }
                RDF.Method_WriteFile_Bin(chemin_acces_fichier_matrice_globale_param_DF, matrice_globale_des_parametres_des_echantillons);



            }
            if (type_approche == 1)
            {
                ///////////////////////Modele//////////////////////////////////////////
                classe_choisie = "modeles";

                chemin_acces_dossier_param_echantillons = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isolé" + "/" + "matrice_param_DF/";
                chemin_acces_fichier_matrice_globale_param_DF = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isolé" + "/" + "matrice_globale_param_DF_des_echantillons_" + classe_choisie + ".bin";
                var av_files_n2 = System.IO.Directory.GetFiles(chemin_acces_dossier_param_echantillons);
                for (int numero_label = 0; numero_label < (av_files_n2.Length); numero_label++)
                {
                    chemin_acces_echantillon_courant = av_files_n2[numero_label];
                    matrice_param_echantillon = RDF.Method_ReadFile_Bin_Param(chemin_acces_echantillon_courant);
                    nbr_segment_echantillon = matrice_param_echantillon.GetLength(1);
                    matrice_param_echantillon = new double[matrice_param_echantillon.GetLength(0) + 1, nbr_segment_echantillon];
                    //matrice_globale_des_parametres_des_echantillons = [matrice_globale
                   //_des_parametres_des_echantillons, matrice_param_echantillon];
                    for (int i = 0; i < matrice_param_echantillon.GetLength(0); i++)
                        for (int j = 0; j < matrice_param_echantillon.GetLength(1); j++)
                            matrice_globale_des_parametres_des_echantillons[i, j] = matrice_param_echantillon[i, j];


                }
                size_av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_fichier_matrice_globale_param_DF).Length;
                if (!Directory.Exists(chemin_acces_fichier_matrice_globale_param_DF))
                {
                    var av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_fichier_matrice_globale_param_DF);
                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n13; numero_echantillon++)
                    {
                        string chemin = av_files_n13[numero_echantillon];
                        RDF.Method_DeleteFiles(chemin);
                    }
                }
                RDF.Method_WriteFile_Bin(chemin_acces_fichier_matrice_globale_param_DF, matrice_globale_des_parametres_des_echantillons);

                ///////////////////////Correcte//////////////////////////////////////////
                classe_choisie = "correcte";

                chemin_acces_dossier_param_echantillons = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isolé" + "/" + "matrice_param_DF/";
                chemin_acces_fichier_matrice_globale_param_DF = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isolé" + "/" + "matrice_globale_param_DF_des_echantillons_" + classe_choisie + ".bin";
                av_files_n2 = System.IO.Directory.GetFiles(chemin_acces_dossier_param_echantillons);
                for (int numero_label = 0; numero_label < (av_files_n2.Length); numero_label++)
                {
                    chemin_acces_echantillon_courant = av_files_n2[numero_label];
                    matrice_param_echantillon = RDF.Method_ReadFile_Bin_Param(chemin_acces_echantillon_courant);
                    nbr_segment_echantillon = matrice_param_echantillon.GetLength(1);
                    matrice_param_echantillon = new double[matrice_param_echantillon.GetLength(0) + 1, nbr_segment_echantillon];
                    //matrice_globale_des_parametres_des_echantillons = [matrice_globale_des_parametres_des_echantillons, matrice_param_echantillon];
                }
                size_av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_fichier_matrice_globale_param_DF).Length;
                if (!Directory.Exists(chemin_acces_fichier_matrice_globale_param_DF))
                {
                    var av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_fichier_matrice_globale_param_DF);
                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n13; numero_echantillon++)
                    {
                        string chemin = av_files_n13[numero_echantillon];
                        RDF.Method_DeleteFiles(chemin);
                    }
                }
                RDF.Method_WriteFile_Bin(chemin_acces_fichier_matrice_globale_param_DF, matrice_globale_des_parametres_des_echantillons);

                /// ///////////////////////fausse forme//////////////////////////////////////////
                classe_choisie = "fausse_forme";

                chemin_acces_dossier_param_echantillons = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isolé" + "/" + "matrice_param_DF/";
                chemin_acces_fichier_matrice_globale_param_DF = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isolé" + "/" + "matrice_globale_param_DF_des_echantillons_" + classe_choisie + ".bin";
                av_files_n2 = System.IO.Directory.GetFiles(chemin_acces_dossier_param_echantillons);
                for (int numero_label = 0; numero_label < (av_files_n2.Length); numero_label++)
                {
                    chemin_acces_echantillon_courant = av_files_n2[numero_label];
                    matrice_param_echantillon = RDF.Method_ReadFile_Bin_Param(chemin_acces_echantillon_courant);
                    nbr_segment_echantillon = matrice_param_echantillon.GetLength(1);
                    matrice_param_echantillon = new double[matrice_param_echantillon.GetLength(0) + 1, nbr_segment_echantillon];
                    //matrice_globale_des_parametres_des_echantillons = [matrice_globale_des_parametres_des_echantillons, matrice_param_echantillon];
                }
                size_av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_fichier_matrice_globale_param_DF).Length;
                if (!Directory.Exists(chemin_acces_fichier_matrice_globale_param_DF))
                {
                    var av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_fichier_matrice_globale_param_DF);
                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n13; numero_echantillon++)
                    {
                        string chemin = av_files_n13[numero_echantillon];
                        RDF.Method_DeleteFiles(chemin);
                    }
                }
                RDF.Method_WriteFile_Bin(chemin_acces_fichier_matrice_globale_param_DF, matrice_globale_des_parametres_des_echantillons);

                //////////////////////////////faux ordre /////////////////////////////////////
                classe_choisie = "faux_ordre";

                chemin_acces_dossier_param_echantillons = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isolé" + "/" + "matrice_param_DF/";
                chemin_acces_fichier_matrice_globale_param_DF = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isolé" + "/" + "matrice_globale_param_DF_des_echantillons_" + classe_choisie + ".bin";
                av_files_n2 = System.IO.Directory.GetFiles(chemin_acces_dossier_param_echantillons);
                for (int numero_label = 0; numero_label < (av_files_n2.Length); numero_label++)
                {
                    chemin_acces_echantillon_courant = av_files_n2[numero_label];
                    matrice_param_echantillon = RDF.Method_ReadFile_Bin_Param(chemin_acces_echantillon_courant);
                    nbr_segment_echantillon = matrice_param_echantillon.GetLength(1);
                    matrice_param_echantillon = new double[matrice_param_echantillon.GetLength(0) + 1, nbr_segment_echantillon];
                    //matrice_globale_des_parametres_des_echantillons = [matrice_globale_des_parametres_des_echantillons, matrice_param_echantillon];
                }
                size_av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_fichier_matrice_globale_param_DF).Length;
                if (!Directory.Exists(chemin_acces_fichier_matrice_globale_param_DF))
                {
                    var av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_fichier_matrice_globale_param_DF);
                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n13; numero_echantillon++)
                    {
                        string chemin = av_files_n13[numero_echantillon];
                        RDF.Method_DeleteFiles(chemin);
                    }
                }
                RDF.Method_WriteFile_Bin(chemin_acces_fichier_matrice_globale_param_DF, matrice_globale_des_parametres_des_echantillons);

                //////////////////////////fausse direction ///////////////////////////////
                classe_choisie = "fausse_direction";

                chemin_acces_dossier_param_echantillons = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isolé" + "/" + "matrice_param_DF/";
                chemin_acces_fichier_matrice_globale_param_DF = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_choisie + "/" + type_de_script_p + "_" + label_du_script_choisi + "_isolé" + "/" + "matrice_globale_param_DF_des_echantillons_" + classe_choisie + ".bin";
                av_files_n2 = System.IO.Directory.GetFiles(chemin_acces_dossier_param_echantillons);
                for (int numero_label = 0; numero_label < (av_files_n2.Length); numero_label++)
                {
                    chemin_acces_echantillon_courant = av_files_n2[numero_label];
                    matrice_param_echantillon = RDF.Method_ReadFile_Bin_Param(chemin_acces_echantillon_courant);
                    nbr_segment_echantillon = matrice_param_echantillon.GetLength(1);
                    matrice_param_echantillon = new double[matrice_param_echantillon.GetLength(0) + 1, nbr_segment_echantillon];
                    //matrice_globale_des_parametres_des_echantillons = [matrice_globale_des_parametres_des_echantillons, matrice_param_echantillon];
                }
                size_av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_fichier_matrice_globale_param_DF).Length;
                if (!Directory.Exists(chemin_acces_fichier_matrice_globale_param_DF))
                {
                    var av_files_n13 = System.IO.Directory.GetFiles(chemin_acces_fichier_matrice_globale_param_DF);
                    for (int numero_echantillon = 0; numero_echantillon < size_av_files_n13; numero_echantillon++)
                    {
                        string chemin = av_files_n13[numero_echantillon];
                        RDF.Method_DeleteFiles(chemin);
                    }
                }
                RDF.Method_WriteFile_Bin(chemin_acces_fichier_matrice_globale_param_DF, matrice_globale_des_parametres_des_echantillons);



            }

        }
    }
    }
