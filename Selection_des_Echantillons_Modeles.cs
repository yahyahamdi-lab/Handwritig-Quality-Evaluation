using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{
   public class Selection_des_Echantillons_Modeles
   {
        public string chemin_acces_fichier_param_echantillon_reference_courant, chemin_acces_dossier_label_reference, chemin_acces_sous_dossier_param_reference, chemin_acces_fichier_param_echantillon_candidat_courant, label_choisi_p, label_choisi, label_choisi_sans_lettre, label_choisi_sans_lettre_p, chemin_acces_dossier_label, chemin_acces_sous_dossier_param_candidat, type_de_script_p, type_de_script_q, chemin_acces_dossier_candidat_a_examiner, chemin_acces_dossier_ensemble_reference;
        public char premier_caractere, deuxieme_caractere, troisieme_caractere;
        public int nombre_de_caractere_a_sauter, size_label_choisi, size_label_choisi_p;
        public int[] indice_min, mat_cod_candidat_courant, vect_cv_candidat_courant, mat_cod_reference_courant, vect_cv_reference_courant, Rangs_ordonnes_des_echantillons_modeles_AP, Rangs_ordonnes_des_echantillons_modeles_AS;
        public double[,] arr2, mmatrice_param_echantillon_candidat_courant, mmatrice_param_echantillon_reference_courant, matrice_distances_AP_candidat_courant, matrice_distances_AS_candidat_courant;
        public double somme_distances_AP_echant_candidat_courant_echants_refs, somme_distances_AS_echant_candidat_courant_echants_refs, distance_AS_direction, distance_AS_ordre, distance_AS_forme, distance_AP_direction, distance_AP_ordre, distance_AP_forme;
        public double[] matrice_distances_moyennes_AP_des_echantillons_candidats, matrice_distances_moyennes_AS_des_echantillons_candidats, mm_param_echantillon_candidat_courant, mma_param_echantillon_reference_courant, vect_distance_AP, vect_distance_AS, matrice_dist_AS_candidat_courant, matrice_dist_AP_candidat_courant;
        
        class_codage_p p = new class_codage_p();
        Module_Comparaison_Structurelle comparaison_structurelle = new Module_Comparaison_Structurelle();
        Module_estimation_direction_score direction_score = new Module_estimation_direction_score();
        Module_estimation_ordre_score ordre_score = new Module_estimation_ordre_score();
        Module_estimation_forme_score_1 forme_score = new Module_estimation_forme_score_1();
        public int[] Method_Rangs_ordonnes_des_echantillons_modeles_AP(string Label, string type_de_script, string classe_des_echantillons_candidats, string classe_de_l_ensemble_des_echantillons_references, string path)
        {
            Console.WriteLine("Hello11");
            if (type_de_script.Equals("Lettre"))
            {
                type_de_script_p = "Lettre";
                type_de_script_q = "lettres";
            }
            else if(type_de_script.Equals( "Mot___"))
            {
                type_de_script_p = "Mot";
                type_de_script_q = "mots";
            }

            if (type_de_script.Equals("Lettre"))
            {
                premier_caractere = 'L';
                deuxieme_caractere = 'e';
                troisieme_caractere = 't';
                nombre_de_caractere_a_sauter = 6;
            }
            else if (type_de_script.Equals("Mot___"))
            {
                premier_caractere = 'M';
                deuxieme_caractere = 'o';
                troisieme_caractere = 't';
                nombre_de_caractere_a_sauter = 3;
            }

            //matrice_distances_AP_candidat_courant = new double[numero_echantillon_candidat, 3];

            chemin_acces_dossier_candidat_a_examiner = path + "/base_de_"+ type_de_script_q + "/echantillons_"+ classe_des_echantillons_candidats+"/";
            chemin_acces_dossier_ensemble_reference = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_de_l_ensemble_des_echantillons_references + "/";

            var number_av_files_n2 = System.IO.Directory.GetDirectories(chemin_acces_dossier_candidat_a_examiner).Length;
            var av_files_n2 = System.IO.Directory.GetDirectories(chemin_acces_dossier_candidat_a_examiner);
            for ( int numero_label = 0; numero_label< number_av_files_n2; numero_label++)
            {
                chemin_acces_dossier_label = av_files_n2[numero_label];
                chemin_acces_sous_dossier_param_candidat = chemin_acces_dossier_label + "/matrice_param/";
                var size_dossier_label = chemin_acces_dossier_label.Length;
                Console.WriteLine("Hello12"+ chemin_acces_sous_dossier_param_candidat);
                for (int ijk = 0; ijk < size_dossier_label - 2; ijk++)  ////
                {
                    if ((chemin_acces_dossier_label[ijk] == premier_caractere) && (chemin_acces_dossier_label[ijk + 1] == deuxieme_caractere) && (chemin_acces_dossier_label[ijk + 2] == troisieme_caractere))
                    {
                        label_choisi = chemin_acces_dossier_label.Substring(ijk);
                        size_label_choisi = label_choisi.Length;
                        label_choisi_sans_lettre = label_choisi.Substring(nombre_de_caractere_a_sauter + 1, (size_label_choisi - 7 - nombre_de_caractere_a_sauter));
                    }
                }
                Console.WriteLine("Hello13");
                if (label_choisi_sans_lettre.Length == Label.Length)
                {
                    if (label_choisi_sans_lettre == Label)
                    {
                        var av_files_n4 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_candidat, "*.txt");
                        int size_av_files_n4 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_candidat, "*.txt").Length;
                        Console.WriteLine("Hello14AP"+ size_av_files_n4);
                        Console.WriteLine("Hello141AP" + chemin_acces_sous_dossier_param_candidat);
                        matrice_distances_moyennes_AP_des_echantillons_candidats = new double[size_av_files_n4];
                        matrice_distances_moyennes_AS_des_echantillons_candidats = new double[size_av_files_n4];
                        for (int numero_echantillon_candidat = 0; numero_echantillon_candidat < size_av_files_n4; numero_echantillon_candidat++)
                        {
                            chemin_acces_fichier_param_echantillon_candidat_courant = av_files_n4[numero_echantillon_candidat];
                            mmatrice_param_echantillon_candidat_courant = Method_ReadFile(chemin_acces_fichier_param_echantillon_candidat_courant);
                            for (int i = 0; i < mmatrice_param_echantillon_candidat_courant.GetLength(0); i++)
                            {
                                for (int j = 0; j < mmatrice_param_echantillon_candidat_courant.GetLength(1); j++)
                                {    double s1 = mmatrice_param_echantillon_candidat_courant[i, j];
                                    double s2 = mmatrice_param_echantillon_candidat_courant[i, j];
                                   Console.WriteLine("matrice param" + s1 + " " + s2);
                                }
                            }
                            Console.WriteLine("Hello14AP");
                            mm_param_echantillon_candidat_courant = new double[mmatrice_param_echantillon_candidat_courant.GetLength(1)];
                            for (int i = 0; i < mmatrice_param_echantillon_candidat_courant.GetLength(1); i++)
                            mm_param_echantillon_candidat_courant[i] = mmatrice_param_echantillon_candidat_courant[6, i];
                            Console.WriteLine("Hello15AP");
                            mat_cod_candidat_courant = p.method_codage_p(mm_param_echantillon_candidat_courant, mmatrice_param_echantillon_candidat_courant);
                            vect_cv_candidat_courant = mat_cod_candidat_courant;

                            var av_files_n5 = System.IO.Directory.GetFiles(chemin_acces_dossier_ensemble_reference);
                            int size_av_files_n5 = System.IO.Directory.GetFiles(chemin_acces_dossier_ensemble_reference).Length;
                            for (int numero_label_reference= 0; numero_label_reference < size_av_files_n5; numero_label_reference++)
                            {
                                chemin_acces_dossier_label_reference = av_files_n5[numero_label_reference];
                                chemin_acces_sous_dossier_param_reference = chemin_acces_dossier_label_reference + "/matrice_param/";
                                Console.WriteLine("Hello16AP");
                                var size_dossier_label_ref = chemin_acces_dossier_label_reference.Length;
                                for (int ijk = 0; ijk < size_dossier_label_ref - 2; ijk++)  ////
                                {
                                    if ((chemin_acces_dossier_label_reference[ijk] == premier_caractere) && (chemin_acces_dossier_label_reference[ijk + 1] == deuxieme_caractere) && (chemin_acces_dossier_label_reference[ijk + 2] == troisieme_caractere))
                                    {
                                        label_choisi_p = chemin_acces_dossier_label_reference.Substring(ijk);
                                        size_label_choisi_p = label_choisi.Length; 
                                        label_choisi_sans_lettre_p = label_choisi.Substring(nombre_de_caractere_a_sauter + 1, (size_label_choisi - 7 - nombre_de_caractere_a_sauter)); //// !!
                                    }
                                }

                                if (label_choisi_sans_lettre_p.Length == Label.Length)
                                {
                                    if (label_choisi_sans_lettre_p == Label)
                                    {
                                        var av_files_n6 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_reference, "*.txt");
                                        var size_av_files_n6 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_reference, "*.txt").Length;
                                        matrice_distances_AP_candidat_courant = new double[size_av_files_n6, 3];
                                        matrice_distances_AS_candidat_courant = new double[size_av_files_n6, 3];

                                        for (int numero_echantillon_reference = 0; numero_echantillon_reference < size_av_files_n6; numero_echantillon_reference++)
                                        {
                                            chemin_acces_fichier_param_echantillon_reference_courant = av_files_n6[numero_label_reference];
                                            mmatrice_param_echantillon_reference_courant = Method_ReadFile(chemin_acces_fichier_param_echantillon_reference_courant);
                                            mma_param_echantillon_reference_courant = new double[mmatrice_param_echantillon_reference_courant.GetLength(1)];
                                            for (int i = 0; i < mmatrice_param_echantillon_reference_courant.GetLength(1); i++)
                                                mma_param_echantillon_reference_courant[i] = mmatrice_param_echantillon_reference_courant[6, i];

                                            mat_cod_reference_courant = p.method_codage_p(mma_param_echantillon_reference_courant, mmatrice_param_echantillon_reference_courant);
                                            vect_cv_reference_courant = mat_cod_reference_courant;

                                            /// ***** calcul des distances *****************
                                            distance_AP_direction = direction_score.method_estimation_direction_score(mmatrice_param_echantillon_candidat_courant, mmatrice_param_echantillon_reference_courant, mmatrice_param_echantillon_reference_courant, mmatrice_param_echantillon_reference_courant, label_choisi_sans_lettre);
                                            distance_AP_ordre = ordre_score.method_estimation_ordre_score(mmatrice_param_echantillon_candidat_courant, mmatrice_param_echantillon_reference_courant, mmatrice_param_echantillon_reference_courant, mmatrice_param_echantillon_reference_courant, label_choisi_sans_lettre);
                                            distance_AP_forme = forme_score.method_Module_estimation_forme_score(mmatrice_param_echantillon_candidat_courant, mmatrice_param_echantillon_reference_courant, mmatrice_param_echantillon_reference_courant, mmatrice_param_echantillon_reference_courant, label_choisi_sans_lettre);

                                            vect_distance_AP[0] = distance_AP_direction;
                                            vect_distance_AP[1] = distance_AP_ordre;
                                            vect_distance_AP[2] = distance_AP_forme;

                                            matrice_distances_AP_candidat_courant[numero_echantillon_reference, 0] = vect_distance_AP[0];
                                            matrice_distances_AP_candidat_courant[numero_echantillon_reference, 1] = vect_distance_AP[1];
                                            matrice_distances_AP_candidat_courant[numero_echantillon_reference, 2] = vect_distance_AP[2];

                                        } // fin boucle des echantillons référence

                                        for (int j = 0; j < matrice_distances_AP_candidat_courant.GetLength(0); j++)
                                            matrice_dist_AP_candidat_courant[j] = matrice_distances_AP_candidat_courant[j, 2];

                                        somme_distances_AP_echant_candidat_courant_echants_refs = somme(matrice_dist_AP_candidat_courant);
                                        Console.WriteLine("Hello18AP");
                                    }
                                }
                            }
                            Console.WriteLine("Hello19AP");
                            matrice_distances_moyennes_AP_des_echantillons_candidats[numero_echantillon_candidat] = somme_distances_AP_echant_candidat_courant_echants_refs;
                        }
                    }
                }
            }

            Rangs_ordonnes_des_echantillons_modeles_AP = sortBySelection(matrice_distances_moyennes_AP_des_echantillons_candidats, matrice_distances_moyennes_AP_des_echantillons_candidats.GetLength(0));
            return Rangs_ordonnes_des_echantillons_modeles_AP;
        }

        public double somme(double[] tab)
        {
            double s = 0;
            for (int i = 0; i < tab.Length; i++)
                s  += tab[i];
            return s;
        }

        public int[] sortBySelection(double[] t, int sizeOfTheCollection)
        {

            indice_min = new int[sizeOfTheCollection];
            double max_mat = max_matrice(t);
            int pos_min;
            for (int i = 0; i < sizeOfTheCollection; i++)
            {
                pos_min = pos_matrice(t);
                indice_min[i] = pos_min+1;
                t[pos_min] = max_mat + 1;
            }
            return indice_min;
        }

        public int pos_matrice(double[] tab)
        {
            //int minimum = 0;
            double minimum = tab[0];
            int pos = 0;
            for (int i = 1; i < tab.Length; i++)
            {
                if (tab[i] < minimum)
                {
                    minimum = tab[i];
                    pos = i;
                }


            }
            return pos;
        }
        public static double max_matrice(double[] tab)
        {
            double mmaximum = tab[0];
            for (int i = 1; i < tab.Length; i++)
            {
                if (tab[i] > mmaximum)
                    mmaximum = tab[i];
            }
            return mmaximum;
        }
        public int[] Method_Rangs_ordonnes_des_echantillons_modeles_AS(string Label, string type_de_script, string classe_des_echantillons_candidats, string classe_de_l_ensemble_des_echantillons_references, string path,int autorisation_distance_test_faux)
        {
            if (type_de_script.Equals("Lettre"))
            {
                type_de_script_p = "Lettre";
                type_de_script_q = "lettres";
            }
            else if (type_de_script.Equals("Mot___"))
            {
                type_de_script_p = "Mot";
                type_de_script_q = "mots";
            }

            if (type_de_script.Equals("Lettre"))
            {
                premier_caractere = 'L';
                deuxieme_caractere = 'e';
                troisieme_caractere = 't';
                nombre_de_caractere_a_sauter = 6;
            }
            else if (type_de_script.Equals("Mot___"))
            {
                premier_caractere = 'M';
                deuxieme_caractere = 'o';
                troisieme_caractere = 't';
                nombre_de_caractere_a_sauter = 3;
            }

            //matrice_distances_AP_candidat_courant = new double[numero_echantillon_candidat, 3];

            //chemin_acces_dossier_candidat_a_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_des_echantillons_candidats + "s";
            chemin_acces_dossier_candidat_a_examiner = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_des_echantillons_candidats;

            chemin_acces_dossier_ensemble_reference = path + "/base_de_" + type_de_script_q + "/echantillons_" + classe_de_l_ensemble_des_echantillons_references;

            var number_av_files_n2 = System.IO.Directory.GetDirectories(chemin_acces_dossier_candidat_a_examiner).Length;
            var av_files_n2 = System.IO.Directory.GetDirectories(chemin_acces_dossier_candidat_a_examiner);
            for (int numero_label = 0; numero_label < number_av_files_n2; numero_label++)
            {
                chemin_acces_dossier_label = av_files_n2[numero_label];
                chemin_acces_sous_dossier_param_candidat = chemin_acces_dossier_label + "/matrice_param/";
                var size_dossier_label = chemin_acces_dossier_label.Length;
                for (int ijk = 0; ijk < size_dossier_label - 2; ijk++)  ////
                {
                    if ((chemin_acces_dossier_label[ijk] == premier_caractere) && (chemin_acces_dossier_label[ijk + 1] == deuxieme_caractere) && (chemin_acces_dossier_label[ijk + 2] == troisieme_caractere))
                    {
                        label_choisi = chemin_acces_dossier_label.Substring(ijk);
                        size_label_choisi = label_choisi.Length;
                        label_choisi_sans_lettre = label_choisi.Substring(nombre_de_caractere_a_sauter + 1, (size_label_choisi - 7 - nombre_de_caractere_a_sauter));
                    }
                }
                if (label_choisi_sans_lettre.Length == Label.Length)
                {
                    if (label_choisi_sans_lettre == Label)
                    {
                        var av_files_n4 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_candidat, "*.txt");
                        var size_av_files_n4 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_candidat, "*.txt").Length;
                        matrice_distances_moyennes_AS_des_echantillons_candidats = new double[size_av_files_n4];
                        for (int numero_echantillon_candidat = 0; numero_echantillon_candidat < size_av_files_n4; numero_echantillon_candidat++)
                        {
                            chemin_acces_fichier_param_echantillon_candidat_courant = av_files_n4[numero_echantillon_candidat];
                            mmatrice_param_echantillon_candidat_courant = Method_ReadFile(chemin_acces_fichier_param_echantillon_candidat_courant);
                            mm_param_echantillon_candidat_courant = new double[mmatrice_param_echantillon_candidat_courant.GetLength(1)];
                            for (int i = 0; i < mmatrice_param_echantillon_candidat_courant.GetLength(1); i++)
                                mm_param_echantillon_candidat_courant[i] = mmatrice_param_echantillon_candidat_courant[6, i];

                            mat_cod_candidat_courant = p.method_codage_p(mm_param_echantillon_candidat_courant, mmatrice_param_echantillon_candidat_courant);
                            vect_cv_candidat_courant = mat_cod_candidat_courant;

                            var av_files_n5 = System.IO.Directory.GetFiles(chemin_acces_dossier_ensemble_reference);
                            var size_av_files_n5 = System.IO.Directory.GetFiles(chemin_acces_dossier_ensemble_reference).Length;
                            for (int numero_label_reference = 0; numero_label_reference < size_av_files_n5; numero_label_reference++)
                            {
                                chemin_acces_dossier_label_reference = av_files_n5[numero_label_reference];
                                chemin_acces_sous_dossier_param_reference = chemin_acces_dossier_label_reference + "/matrice_param/";

                                var size_dossier_label_ref = chemin_acces_dossier_label_reference.Length;
                                for (int ijk = 0; ijk < size_dossier_label_ref - 2; ijk++)  ////
                                {
                                    if ((chemin_acces_dossier_label_reference[ijk] == premier_caractere) && (chemin_acces_dossier_label_reference[ijk + 1] == deuxieme_caractere) && (chemin_acces_dossier_label_reference[ijk + 2] == troisieme_caractere))
                                    {
                                        label_choisi_p = chemin_acces_dossier_label_reference.Substring(ijk);
                                        size_label_choisi_p = label_choisi.Length;
                                        label_choisi_sans_lettre_p = label_choisi.Substring(nombre_de_caractere_a_sauter + 1, (size_label_choisi - 7 - nombre_de_caractere_a_sauter)); //// !!
                                    }
                                }

                                if (label_choisi_sans_lettre_p.Length == Label.Length)
                                {
                                    if (label_choisi_sans_lettre_p == Label)
                                    {
                                        var av_files_n6 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_reference, "*.txt");
                                        var size_av_files_n6 = System.IO.Directory.GetFiles(chemin_acces_sous_dossier_param_reference, "*.txt").Length;
                                        matrice_distances_AP_candidat_courant = new double[size_av_files_n6, 3];
                                        matrice_distances_AS_candidat_courant = new double[size_av_files_n6, 3];

                                        for (int numero_echantillon_reference = 0; numero_echantillon_reference < size_av_files_n6; numero_echantillon_reference++)
                                        {
                                            chemin_acces_fichier_param_echantillon_reference_courant = av_files_n5[numero_label_reference];
                                            mmatrice_param_echantillon_reference_courant = Method_ReadFile(chemin_acces_fichier_param_echantillon_reference_courant);
                                            mma_param_echantillon_reference_courant = new double[mmatrice_param_echantillon_reference_courant.GetLength(1)];
                                            for (int i = 0; i < mmatrice_param_echantillon_reference_courant.GetLength(1); i++)
                                                mma_param_echantillon_reference_courant[i] = mmatrice_param_echantillon_reference_courant[6, i];

                                            mat_cod_reference_courant = p.method_codage_p(mma_param_echantillon_reference_courant, mmatrice_param_echantillon_reference_courant);
                                            vect_cv_reference_courant = mat_cod_reference_courant;

                                            /// ***** calcul des distances *****************
                                            distance_AS_direction = comparaison_structurelle.method_Calcul_score_CS_direction(vect_cv_candidat_courant, vect_cv_reference_courant, vect_cv_reference_courant, vect_cv_reference_courant);
                                            distance_AS_ordre = comparaison_structurelle.method_Calcul_score_CS_ordre(vect_cv_candidat_courant, vect_cv_reference_courant, vect_cv_reference_courant, vect_cv_reference_courant);
                                            distance_AS_forme = comparaison_structurelle.method_Calcul_score_CS_forme(vect_cv_candidat_courant, vect_cv_reference_courant, vect_cv_reference_courant, vect_cv_reference_courant,Label, path,autorisation_distance_test_faux);

                                            vect_distance_AS[0] = distance_AS_direction;
                                            vect_distance_AS[1] = distance_AS_ordre;
                                            vect_distance_AS[2] = distance_AS_forme;
                                            Console.WriteLine("Hello12");
                                            matrice_distances_AS_candidat_courant[numero_echantillon_reference, 0] = vect_distance_AS[0];
                                            matrice_distances_AS_candidat_courant[numero_echantillon_reference, 1] = vect_distance_AS[1];
                                            matrice_distances_AS_candidat_courant[numero_echantillon_reference, 2] = vect_distance_AS[2];
                                            Console.WriteLine("Hello131");
                                        } // fin boucle des echantillons référence

                                        for (int j = 0; j < matrice_distances_AS_candidat_courant.GetLength(0); j++)
                                            matrice_dist_AS_candidat_courant[j] = matrice_distances_AS_candidat_courant[j, 2];
                                        Console.WriteLine("Hello15");
                                        somme_distances_AP_echant_candidat_courant_echants_refs = somme(matrice_dist_AP_candidat_courant);
                                        somme_distances_AS_echant_candidat_courant_echants_refs = somme(matrice_dist_AS_candidat_courant);
                                    }
                                }
                            }
                            matrice_distances_moyennes_AS_des_echantillons_candidats[numero_echantillon_candidat] = somme_distances_AS_echant_candidat_courant_echants_refs;
                            for (int i = 0; i < matrice_distances_moyennes_AS_des_echantillons_candidats.Length; i++)
                                Console.WriteLine("matrice" + matrice_distances_moyennes_AS_des_echantillons_candidats[i]);

                        }
                    }
                }
            }

            Rangs_ordonnes_des_echantillons_modeles_AS = sortBySelection(matrice_distances_moyennes_AS_des_echantillons_candidats, matrice_distances_moyennes_AS_des_echantillons_candidats.GetLength(0));
            return Rangs_ordonnes_des_echantillons_modeles_AS;
        }

        double[,] Method_ReadFile(string file)
        {
            string cor1 = "";
            List<string> cor14 = new List<string>();
            string[] lines = System.IO.File.ReadAllLines(file);
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                Console.WriteLine("\t" + line);
                char[] t = { '\r', ' ' };
                string[] multiArray = line.Split(t);

                arr2 = new double[lines.Length, multiArray.Length - 1];
                for (int i = 0; i < lines.Length; i++)
                {
                    for (int j = 0; j < multiArray.Length - 1; j++)
                    {
                        Console.WriteLine("arr value");
                        double arr = Convert.ToDouble(multiArray[j]);
                        arr2[i, j] = arr;
                        //Console.WriteLine(arr2[i, j]);
                    }
                }

            }

            return arr2;
        }
    }
}
