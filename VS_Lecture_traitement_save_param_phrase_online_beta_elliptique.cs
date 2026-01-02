using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{
   public class VS_Lecture_traitement_save_param_phrase_online_beta_elliptique
   {
        /*
         * % -SCRIPT    : extraction des caracteristiques par l'approche beta_eliptique             
             : Lecture_traitement_save_param_phrase_online                               
             : chemin d'une suite de fichiers de mots dont l'ordre temporel de leurs     
              trajectoire off line est reconstruit fichier texte contenant les cractéristiques extraites des graphèmes segmentés
              -copyright : REGIM-ENIS
         */

        public double sigma_p_filtre_V, sigma_p_filtre_traject;
        public int rayon_filtre_V, rayon_filtre_traject, taille_minimale_pseudo_mot, taille_minimale_points_filtres_avec_pression, longueur_nbr_points_min, nbr_pseudo_mot, k_p, numero_pseudo_mot, nbr_pseudo_mot_traite,t;
        public double[,] points_new, matrice_param_descripteurs_fourier, points_total_final_filtrer_segment_k, points_total_final_filtrer_3, points_total_final_filtrer, points_repare, data_k, max_min_x_y, points_filtre, points_filtres_pression_pseudo_mot, points_filtres_pression_pseudo_mot_2, data_k_filtree, points_filtres_avec_pression, vect_param, mmatrice_param_1;
        public double[] DAT_par_pseudo_mot;
        public int[] Num_pseudo_mot;
        public int[,] taille_pseudo_mot, taille_de_tous_les_pseudo_mots_en_nbr_de_points;
        public int M, pression_baissee, pression_levee, ligne,colonne, taille_vect_param,ind;
        VS_Inversion_et_reparation_de_la_resolution_trajectoire resolution_trajectoire;
        VS_extract_pseudo_mot_p pseudo_mots;
        VS_Filtrage_conventionnel_plus_Dat_et_Detection_lim_max_min filtrage;
        
        
        //double[,,] tab = new double[2, 3, 4];
        public double[][,] mat_pseudo_mot, mat_data_k_filtree_pseudo_mot;
        public double[][,] mat_pseudo_mot_de_points_filtres_avec_pression;

        public double Maxi_X_P = -100000;
        public double Maxi_Y_P = -100000;
        public double Mini_X_P = 100000;
        public double Mini_Y_P = 100000;


      public double[,] method_mmatrice_param_1(double[,] data, string label_du_script_choisi)
        {

            VS_new_beta_ellipse_stretegy_Beta_chevauchees_s_L new_beta_ellipse = new VS_new_beta_ellipse_stretegy_Beta_chevauchees_s_L();

            resolution_trajectoire = new VS_Inversion_et_reparation_de_la_resolution_trajectoire();
            points_total_final_filtrer = new double[2, 2];
            //rayon_filtre_V = 3;              // Valeur initiale
            //sigma_p_filtre_V = 1.7;          // Valeur initiale

            // rayon_filtre_traject = 4;        //  Valeur initiale
            // sigma_p_filtre_traject = 4.5;    //  Valeur initiale
            rayon_filtre_V = 2;              //Valeur initiale
            sigma_p_filtre_V = 1;          // Valeur initiale

            rayon_filtre_traject = 3;        // Valeur initiale
            sigma_p_filtre_traject = 2.5;
            

            rayon_filtre_V = 2;              //Valeur initiale
            sigma_p_filtre_V = 1;          // Valeur initiale

            rayon_filtre_traject = 2;        // Valeur initiale
            sigma_p_filtre_traject = 2;


            rayon_filtre_V = 2;              //Valeur initiale
            sigma_p_filtre_V = 0.5;          // Valeur initiale

            rayon_filtre_traject = 6;        // Valeur initiale
            sigma_p_filtre_traject = 5;

            // Inversion et reparation de la resolution de la trajectoire
            taille_minimale_pseudo_mot = 7;
            taille_minimale_points_filtres_avec_pression = 4;
            t = resolution_trajectoire.Method_size_matrix(data, taille_minimale_pseudo_mot);
            points_repare = new double[t, data.GetLength(1)];
           /* Console.WriteLine("Taille Data" + data.GetLength(0));
            for (int i = 0; i < data.GetLength(0); i++)
                for (int j = 0; j < data.GetLength(1); j++)
                    Console.WriteLine("Data" + data[i, j]);*/
            points_repare = resolution_trajectoire.inversion(data, taille_minimale_pseudo_mot,label_du_script_choisi);
            //[points_repare]= VS_Inversion_et_reparation_de_la_resolution_trajectoire(data, taille_minimale_pseudo_mot);
            
            // Délimitation des pseudo - mots
            longueur_nbr_points_min = 1;  // 9;
            pseudo_mots = new VS_extract_pseudo_mot_p();

            nbr_pseudo_mot = pseudo_mots.Method_nbr_pseudomots(points_repare, longueur_nbr_points_min);

            taille_pseudo_mot = new int[nbr_pseudo_mot, 2];
            taille_pseudo_mot = pseudo_mots.Method_extract_taille_pseudo_mot_p(points_repare, longueur_nbr_points_min);
            mat_pseudo_mot = new double[nbr_pseudo_mot][,];
            // creation matrice pseudo mot
            for (int i = 0; i < nbr_pseudo_mot; i++)
            {
                mat_pseudo_mot[i] = new double[taille_pseudo_mot[i, 1], 2];
            }

            mat_data_k_filtree_pseudo_mot = new double[nbr_pseudo_mot][,];
            for (int i = 0; i < nbr_pseudo_mot; i++)
            {
                mat_data_k_filtree_pseudo_mot[i] = new double[taille_pseudo_mot[i, 1], 2];
            }

            mat_pseudo_mot_de_points_filtres_avec_pression = new double[nbr_pseudo_mot][,];
            for (int i = 0; i < nbr_pseudo_mot; i++)
            {
                mat_pseudo_mot_de_points_filtres_avec_pression[i] = new double[taille_pseudo_mot[i, 1] + 2, 3];
            }

            mat_pseudo_mot = pseudo_mots.Method_extract_pseudo_mot_p(points_repare, longueur_nbr_points_min);
            //[mat_pseudo_mot, nbr_pseudo_mot, taille_pseudo_mot] = VS_extract_pseudo_mot_p(points_repare , longueur_nbr_points_min);
            // Initialisations

            if (nbr_pseudo_mot > 0)
            {
                // Filtrage et calcul de l'angle d'inclinaison de la tangent DAT

                k_p = 0;
                for (int k = 0; k < nbr_pseudo_mot; k++)
                {
                    data_k = new double[taille_pseudo_mot[k, 1], 2];
                    data_k = mat_pseudo_mot[k]; //data_k = mat_pseudo_mot(:,:, k);
                                                // data_k = data_k(1 : taille_pseudo_mot(k, 2),:)
                    M = data_k.GetLength(0);
                    if (M > taille_minimale_pseudo_mot)
                    {
                        k_p += 1;
                        numero_pseudo_mot = k_p;
                        // max_min_x_y = filtrage.Method_VS_Filtrage_conventionnel_plus_Dat_et_Detection_lim_max_min_max_min_x_y(data_k, rayon_filtre_traject, sigma_p_filtre_traject);
                        // DAT_par_pseudo_mot = filtrage.Method_VS_Filtrage_conventionnel_plus_Dat_et_Detection_lim_max_min_DAT(data_k, rayon_filtre_traject, sigma_p_filtre_traject);
                        // points_filtre = filtrage.Method_VS_Filtrage_conventionnel_plus_Dat_et_Detection_lim_max_min_points_filtre(data_k, rayon_filtre_traject, sigma_p_filtre_traject);

                        //[max_min_x_y, DAT_par_pseudo_mot, points_filtre] = VS_Filtrage_conventionnel_plus_Dat_et_Detection_lim_max_min(data_k, rayon_filtre_traject, sigma_p_filtre_traject);


                        Num_pseudo_mot = new int[M];
                        for (int i = 0; i < M; i++)
                        {
                            Num_pseudo_mot[i] = k_p;
                        }


                        //DAT_par_pseudo_mot_total = [DAT_par_pseudo_mot_total; DAT_par_pseudo_mot, Num_pseudo_mot];
                        //Ensemble_max_min_x_y = [Ensemble_max_min_x_y; max_min_x_y];
                        //points_total = [points_total; data_k, Num_pseudo_mot];
                        //points_total_filtre = [points_total_filtre; points_filtre, Num_pseudo_mot];

                        //nbr_points_par_pseudo_mot = [nbr_points_par_pseudo_mot; numero_pseudo_mot M];
                    }

                }

                /*
                 *  ajout de la pression du stylo et constitution de la matrice des de points pseudo mots à trois indices (numéro pseudomot , x y pression , noméro point) avec un indice de longueur variable : numero points 
                 * */
                taille_de_tous_les_pseudo_mots_en_nbr_de_points = new int[nbr_pseudo_mot, 2];

                pression_baissee = 1;
                pression_levee = 0;

                k_p = 0;
                for (int k = 0; k < nbr_pseudo_mot; k++)
                {
                    data_k = new double[taille_pseudo_mot[k, 1], 2];
                    numero_pseudo_mot = k;
                    data_k = mat_pseudo_mot[k]; //data_k = mat_pseudo_mot(:,:, k);
                                                /* for (int h=0;h< taille_pseudo_mot.GetLength(0); h++)
                                                 { System.Console.WriteLine(taille_pseudo_mot[h, 1]); }*/

                    //data_k = data_k(1 : taille_pseudo_mot(k, 2),:);
                    //mat_pseudo_mot_de_points_filtres_avec_pression = new double[numero_pseudo_mot][,3];
                    M = data_k.GetLength(0);
                    //System.Console.WriteLine(M);
                    points_filtres_pression_pseudo_mot = new double[M, 3];
                    points_filtres_pression_pseudo_mot_2 = new double[M + 2, 3];
                    if (M > taille_minimale_pseudo_mot)
                        k_p += 1;
                    for (int i = 0; i < M; i++)
                    {
                        points_filtres_pression_pseudo_mot[i, 0] = data_k[i, 0]; // points_filtres_pression_pseudo_mot = [points_filtres_pression_pseudo_mot; data_k(i,:), pression_baissee];
                        points_filtres_pression_pseudo_mot[i, 1] = data_k[i, 1];
                        points_filtres_pression_pseudo_mot[i, 2] = pression_baissee;

                    }
                    //points_filtres_pression_pseudo_mot = [points_filtres_pression_pseudo_mot(1,1), points_filtres_pression_pseudo_mot(1,2), pression_levee; points_filtres_pression_pseudo_mot; points_filtres_pression_pseudo_mot(M,1), points_filtres_pression_pseudo_mot(M,2), pression_levee]; 
                    

                    points_filtres_pression_pseudo_mot_2[0, 0] = points_filtres_pression_pseudo_mot[0, 0];
                    points_filtres_pression_pseudo_mot_2[0, 1] = points_filtres_pression_pseudo_mot[0, 1];
                    points_filtres_pression_pseudo_mot_2[0, 2] = pression_levee;
                    for (int i = 1; i <= M; i++)
                    {
                        points_filtres_pression_pseudo_mot_2[i, 0] = points_filtres_pression_pseudo_mot[i - 1, 0];
                        points_filtres_pression_pseudo_mot_2[i, 1] = points_filtres_pression_pseudo_mot[i - 1, 1];
                        points_filtres_pression_pseudo_mot_2[i, 2] = points_filtres_pression_pseudo_mot[i - 1, 2];
                    }
                    points_filtres_pression_pseudo_mot_2[M + 1, 0] = points_filtres_pression_pseudo_mot[M - 1, 0];
                    points_filtres_pression_pseudo_mot_2[M + 1, 1] = points_filtres_pression_pseudo_mot[M - 1, 1];      //????
                    points_filtres_pression_pseudo_mot_2[M + 1, 2] = pression_levee;

                    data_k_filtree = new double[data_k.GetLength(0), data_k.GetLength(1)];
                    data_k_filtree = data_k;
                    for (int indice_point = 0; indice_point < points_filtres_pression_pseudo_mot_2.GetLength(0); indice_point++)
                    {

                        mat_pseudo_mot_de_points_filtres_avec_pression[numero_pseudo_mot][indice_point, 0] = points_filtres_pression_pseudo_mot_2[indice_point, 0];     // à verifier la taille du vecteur
                        mat_pseudo_mot_de_points_filtres_avec_pression[numero_pseudo_mot][indice_point, 1] = points_filtres_pression_pseudo_mot_2[indice_point, 1];
                        mat_pseudo_mot_de_points_filtres_avec_pression[numero_pseudo_mot][indice_point, 2] = points_filtres_pression_pseudo_mot_2[indice_point, 2];
                        //mat_pseudo_mot_de_points_filtres_avec_pression(indice_point,:, numero_pseudo_mot) = points_filtres_pression_pseudo_mot(indice_point,:);

                    }

                    for (int indice_point = 0; indice_point < data_k_filtree.GetLength(0); indice_point++)
                    {
                        mat_data_k_filtree_pseudo_mot[numero_pseudo_mot][indice_point, 0] = data_k_filtree[indice_point, 0];
                        mat_data_k_filtree_pseudo_mot[numero_pseudo_mot][indice_point, 1] = data_k_filtree[indice_point, 1];

                        //mat_data_k_filtree_pseudo_mot(indice_point,:, numero_pseudo_mot) = data_k_filtree(indice_point,:);

                    }
                    taille_de_tous_les_pseudo_mots_en_nbr_de_points[k, 0] = M;
                    taille_de_tous_les_pseudo_mots_en_nbr_de_points[k, 1] = points_filtres_pression_pseudo_mot_2.GetLength(0);

                }

                //Traitement des pseudo - mots pour leurs segmentations en traits beta - elliptiques et l'extraction de leurs paramètres

                // Representation graphique de la trajectoire de la proposition manuscrite à traiter


                // figure (51);
                // hold off;
                for (int k = 0; k < nbr_pseudo_mot; k++)
                {
                    numero_pseudo_mot = k;
                    points_filtres_avec_pression = new double[mat_pseudo_mot_de_points_filtres_avec_pression[k].GetLength(0), mat_pseudo_mot_de_points_filtres_avec_pression[k].GetLength(1)];
                    points_filtres_avec_pression = mat_pseudo_mot_de_points_filtres_avec_pression[k];
                    //figure(51);
                    //axis equal;
                    //plot(points_filtres_avec_pression(:, 1), points_filtres_avec_pression(:, 2), 'Color',[.3 .7 .999], 'lineStyle', ':');
                    //hold on;

                }


                // Traitement des pseudo - mots : segmentation en traits Bêta - elliptique + extraction des caractéristiques
                nbr_pseudo_mot_traite = 0;
                k_p = 0;
                taille_vect_param = 0;
      // Détermination taille matrice param
                int nombre_colonne = 0;
                for (int k = 0; k < nbr_pseudo_mot; k++)
                {
                    numero_pseudo_mot = k;
                    //points_filtres_avec_pression = mat_pseudo_mot_de_points_filtres_avec_pression(:,:, k);
                    //points_filtres_avec_pression = points_filtres_avec_pression(1 : taille_de_tous_les_pseudo_mots_en_nbr_de_points(k, 2), :);
                    points_filtres_avec_pression = new double[mat_pseudo_mot_de_points_filtres_avec_pression[k].GetLength(0), mat_pseudo_mot_de_points_filtres_avec_pression[k].GetLength(1)];
                    points_filtres_avec_pression = mat_pseudo_mot_de_points_filtres_avec_pression[k];

                    data_k_filtree = new double[mat_data_k_filtree_pseudo_mot[k].GetLength(0), mat_data_k_filtree_pseudo_mot[k].GetLength(1)];
                    data_k_filtree = mat_data_k_filtree_pseudo_mot[k];
                    //data_k_filtree = data_k_filtree(1 : taille_de_tous_les_pseudo_mots_en_nbr_de_points(k, 1), :);
                    M = taille_de_tous_les_pseudo_mots_en_nbr_de_points[k, 0];
                    if (M > taille_minimale_pseudo_mot)
                    {
                        k_p += 1;
                        if (points_filtres_avec_pression.GetLength(0) > taille_minimale_points_filtres_avec_pression)
                        {
                            nbr_pseudo_mot_traite += 1;
                            
                            ligne = new_beta_ellipse.Method_ligne_vecteur_param(points_filtres_avec_pression, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
                            colonne = new_beta_ellipse.Method_colonne_vecteur_param(points_filtres_avec_pression, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
                            //vect_param = new double[ligne, colonne];
                            //vect_param = new_beta_ellipse.Method_matrice_param(points_filtres_avec_pression, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
                            

                            nombre_colonne += colonne;


                        }
                    }

                }
                // remplissage matrice param
                mmatrice_param_1 = new double[16, nombre_colonne];
                //points_total_final_filtrer_4 = new double[nbr_pseudo_mot][,];
                int indice_bloc_points = 0;
                ind = 0;
                for (int k = 0; k < nbr_pseudo_mot; k++)
                {
                    numero_pseudo_mot = k;
                    //points_filtres_avec_pression = mat_pseudo_mot_de_points_filtres_avec_pression(:,:, k);
                    //points_filtres_avec_pression = points_filtres_avec_pression(1 : taille_de_tous_les_pseudo_mots_en_nbr_de_points(k, 2), :);
                    points_filtres_avec_pression = new double[mat_pseudo_mot_de_points_filtres_avec_pression[k].GetLength(0), mat_pseudo_mot_de_points_filtres_avec_pression[k].GetLength(1)];
                    points_filtres_avec_pression = mat_pseudo_mot_de_points_filtres_avec_pression[k];

                    data_k_filtree = new double[mat_data_k_filtree_pseudo_mot[k].GetLength(0), mat_data_k_filtree_pseudo_mot[k].GetLength(1)];
                    data_k_filtree = mat_data_k_filtree_pseudo_mot[k];
                    //data_k_filtree = data_k_filtree(1 : taille_de_tous_les_pseudo_mots_en_nbr_de_points(k, 1), :);
                    M = taille_de_tous_les_pseudo_mots_en_nbr_de_points[k, 0];
                    if (M > taille_minimale_pseudo_mot)
                    {
                        k_p += 1;
                        if (points_filtres_avec_pression.GetLength(0) > taille_minimale_points_filtres_avec_pression)
                        {
                            nbr_pseudo_mot_traite += 1;
                            //[vect_param, matrice_param, param_trajectoire_BC, DAT_BC, points, nbr_trait, famille_tg_horiz] = VS_new_beta_ellipse_stretegy_Beta_chevauchees_s_L(points_filtres_avec_pression, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
                            
                            ligne = new_beta_ellipse.Method_ligne_vecteur_param(points_filtres_avec_pression, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
                            //Console.ReadKey();
                            colonne = new_beta_ellipse.Method_colonne_vecteur_param(points_filtres_avec_pression, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
                            vect_param = new double[colonne, ligne];
                            vect_param = new_beta_ellipse.Method_matrice_param(points_filtres_avec_pression, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
                            //points_total_final_filtrer_segment_k = new_beta_ellipse.Method_points(data_k, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
                            points_total_final_filtrer_segment_k = new_beta_ellipse.Method_points(points_filtres_avec_pression, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);

                            indice_bloc_points += 1;

                            //taille_vect_param += colonne; 
                            
                            for (int l = 0; l < colonne; l++)
                            {

                                mmatrice_param_1[0, ind] = vect_param[0, l];
                                mmatrice_param_1[1, ind] = vect_param[1, l];
                                mmatrice_param_1[2, ind] = vect_param[2, l];
                                mmatrice_param_1[3, ind] = vect_param[3, l];
                                mmatrice_param_1[4, ind] = vect_param[4, l];
                                mmatrice_param_1[5, ind] = vect_param[5, l];
                                mmatrice_param_1[6, ind] = vect_param[6, l];
                                mmatrice_param_1[7, ind] = vect_param[7, l];
                                mmatrice_param_1[8, ind] = vect_param[8, l];
                                mmatrice_param_1[9, ind] = vect_param[9, l];
                                mmatrice_param_1[10, ind] = vect_param[10, l];
                                mmatrice_param_1[11, ind] = vect_param[11, l];

                                ind++;

                            }
                            
                        }
                        else
                            points_total_final_filtrer_segment_k = points_filtres_avec_pression;
                    }
                    else
                        points_total_final_filtrer_segment_k = points_filtres_avec_pression;


                    int taille_pure_points_total_final_filtrer_segment_k = 0;
                    for (int i = 0; i < points_total_final_filtrer_segment_k.GetLength(0); i++)
                    {
                        if ((points_total_final_filtrer_segment_k[i, 0] != 0) || (points_total_final_filtrer_segment_k[i, 1] != 0))
                            taille_pure_points_total_final_filtrer_segment_k ++;
                    }

                    double[,] pure_data_filtrer_segment_k = new double[taille_pure_points_total_final_filtrer_segment_k, 2];
                    double[] pure_data_norm_x_segment_k = new double[pure_data_filtrer_segment_k.GetLength(0)];
                    double[] pure_data_norm_y_segment_k = new double[pure_data_filtrer_segment_k.GetLength(0)];
                    int compteur = 0;
                    for (int i = 0; i < points_total_final_filtrer_segment_k.GetLength(0); i++) 
                    {
                        if ((points_total_final_filtrer_segment_k[i, 0] != 0) || (points_total_final_filtrer_segment_k[i, 1] != 0))
                        {
                            pure_data_filtrer_segment_k[compteur, 0] = points_total_final_filtrer_segment_k[i, 0];
                            pure_data_filtrer_segment_k[compteur, 1] = points_total_final_filtrer_segment_k[i, 1];
                            pure_data_norm_x_segment_k[compteur] = pure_data_filtrer_segment_k[compteur, 0];
                            pure_data_norm_y_segment_k[compteur] = pure_data_filtrer_segment_k[compteur, 1];
                            compteur ++;
                        }
                    }
                    
                    Maxi_X_P = Math.Max(max_matrice(pure_data_norm_x_segment_k), Maxi_X_P);
                    Maxi_Y_P = Math.Max(max_matrice(pure_data_norm_y_segment_k), Maxi_Y_P);
                    Mini_X_P = Math.Min(min_matrice(pure_data_norm_x_segment_k), Mini_X_P);
                    Mini_Y_P = Math.Min(min_matrice(pure_data_norm_y_segment_k), Mini_Y_P);
                }

                //Maxi_mini_xy = new double[4];
                 double[] vect_X_deb_arc = new double[mmatrice_param_1.GetLength(1)];
                 double[] vect_Y_deb_arc = new double[mmatrice_param_1.GetLength(1)];
                 double[] vect_X_fin_arc = new double[mmatrice_param_1.GetLength(1)];
                 double[] vect_Y_fin_arc = new double[mmatrice_param_1.GetLength(1)];

                 for (int k = 0; k < mmatrice_param_1.GetLength(1); k++)
                     vect_X_deb_arc[k] = mmatrice_param_1[7, k];

                 for (int k = 0; k < mmatrice_param_1.GetLength(1); k++)
                     vect_Y_deb_arc[k] = mmatrice_param_1[8, k];

                 for (int k = 0; k < mmatrice_param_1.GetLength(1); k++)
                     vect_X_fin_arc[k] = mmatrice_param_1[9, k];

                 for (int k = 0; k < mmatrice_param_1.GetLength(1); k++)
                      vect_Y_fin_arc[k] = mmatrice_param_1[10, k];

                 if ((Maxi_X_P - Mini_X_P) != 0)
                 {
                    for (int m = 0; m < vect_X_deb_arc.Length; m++)
                    {
                            vect_X_deb_arc[m] = Math.Abs(vect_X_deb_arc[m] - Mini_X_P) / (Maxi_X_P - Mini_X_P);
                            vect_X_fin_arc[m] = Math.Abs(vect_X_fin_arc[m] - Mini_X_P) / (Maxi_X_P - Mini_X_P);
                    }

                 }
                 if ((Maxi_Y_P - Mini_Y_P) != 0)
                 {
                    if (label_du_script_choisi != "symbole1")
                    {
                        for (int m = 0; m < vect_X_deb_arc.Length; m++)
                        {
                            vect_Y_deb_arc[m] = Math.Abs(vect_Y_deb_arc[m] - Mini_Y_P) / (Maxi_Y_P - Mini_Y_P);
                            vect_Y_fin_arc[m] = Math.Abs(vect_Y_fin_arc[m] - Mini_Y_P) / (Maxi_Y_P - Mini_Y_P);
                        }
                    }
                    
                 }

                 for (int k = 0; k < mmatrice_param_1.GetLength(1); k++)
                     mmatrice_param_1[12, k] = 204 * vect_X_deb_arc[k];

                 for (int k = 0; k < mmatrice_param_1.GetLength(1); k++)
                     mmatrice_param_1[13, k] = 128 * vect_Y_deb_arc[k];

                 for (int k = 0; k < mmatrice_param_1.GetLength(1); k++)
                     mmatrice_param_1[14, k] = 204 * vect_X_fin_arc[k];

                 for (int k = 0; k < mmatrice_param_1.GetLength(1); k++)
                     mmatrice_param_1[15, k] = 128 * vect_Y_fin_arc[k];
                
               
            }

            return mmatrice_param_1;
        }


        // methode param fourier descriptor 

            
        public double[,] method_mmatrice_param_DF(double[,] data, string label_du_script_choisi) 
        {
            Description_par_Series_de_Fourier_dune_Trajectoire_Online DF = new Description_par_Series_de_Fourier_dune_Trajectoire_Online();
            VS_new_beta_ellipse_stretegy_Beta_chevauchees_s_L_DF new_beta_ellipse_DF = new VS_new_beta_ellipse_stretegy_Beta_chevauchees_s_L_DF();
            resolution_trajectoire = new VS_Inversion_et_reparation_de_la_resolution_trajectoire();
            points_total_final_filtrer = new double[2, 2];
            int nombre_de_harminiques_considerees = 10;
            //rayon_filtre_V = 3;              // Valeur initiale
            //sigma_p_filtre_V = 1.7;          // Valeur initiale

            // rayon_filtre_traject = 4;        //  Valeur initiale
            // sigma_p_filtre_traject = 4.5;    //  Valeur initiale
            rayon_filtre_V = 2;              //Valeur initiale
            sigma_p_filtre_V = 1;          // Valeur initiale

            rayon_filtre_traject = 3;        // Valeur initiale
            sigma_p_filtre_traject = 2.5;


            rayon_filtre_V = 2;              //Valeur initiale
            sigma_p_filtre_V = 1;          // Valeur initiale

            rayon_filtre_traject = 2;        // Valeur initiale
            sigma_p_filtre_traject = 2;


            rayon_filtre_V = 2;
            sigma_p_filtre_V = 0.5;

            rayon_filtre_traject = 4;
            sigma_p_filtre_traject = 2;
            
            // Inversion et reparation de la resolution de la trajectoire
            taille_minimale_pseudo_mot = 7;
            taille_minimale_points_filtres_avec_pression = 4;
            t = resolution_trajectoire.Method_size_matrix(data, taille_minimale_pseudo_mot);
            points_repare = new double[t, data.GetLength(1)];
            points_repare = resolution_trajectoire.inversion(data, taille_minimale_pseudo_mot,label_du_script_choisi);
            //[points_repare]= VS_Inversion_et_reparation_de_la_resolution_trajectoire(data, taille_minimale_pseudo_mot);
          

            // Délimitation des pseudo - mots
            longueur_nbr_points_min = 1;  // 9;
            pseudo_mots = new VS_extract_pseudo_mot_p();

            nbr_pseudo_mot = pseudo_mots.Method_nbr_pseudomots(points_repare, longueur_nbr_points_min);

            taille_pseudo_mot = new int[nbr_pseudo_mot, 2];
            taille_pseudo_mot = pseudo_mots.Method_extract_taille_pseudo_mot_p(points_repare, longueur_nbr_points_min);
            mat_pseudo_mot = new double[nbr_pseudo_mot][,];
            // creation matrice pseudo mot
            for (int i = 0; i < nbr_pseudo_mot; i++)
            {
                mat_pseudo_mot[i] = new double[taille_pseudo_mot[i, 1], 2];
            }

            mat_data_k_filtree_pseudo_mot = new double[nbr_pseudo_mot][,];
            for (int i = 0; i < nbr_pseudo_mot; i++)
            {
                mat_data_k_filtree_pseudo_mot[i] = new double[taille_pseudo_mot[i, 1], 2];
            }

            mat_pseudo_mot_de_points_filtres_avec_pression = new double[nbr_pseudo_mot][,];
            for (int i = 0; i < nbr_pseudo_mot; i++)
            {
                mat_pseudo_mot_de_points_filtres_avec_pression[i] = new double[taille_pseudo_mot[i, 1] + 2, 3];
            }

            mat_pseudo_mot = pseudo_mots.Method_extract_pseudo_mot_p(points_repare, longueur_nbr_points_min);
            //[mat_pseudo_mot, nbr_pseudo_mot, taille_pseudo_mot] = VS_extract_pseudo_mot_p(points_repare , longueur_nbr_points_min);
            // Initialisations

            if (nbr_pseudo_mot > 0)
            {
                // Filtrage et calcul de l'angle d'inclinaison de la tangent DAT

                k_p = 0;
                for (int k = 0; k < nbr_pseudo_mot; k++)
                {
                    data_k = new double[taille_pseudo_mot[k, 1], 2];
                    data_k = mat_pseudo_mot[k]; //data_k = mat_pseudo_mot(:,:, k);
                                                // data_k = data_k(1 : taille_pseudo_mot(k, 2),:)
                    M = data_k.GetLength(0);
                    if (M > taille_minimale_pseudo_mot)
                    {
                        k_p += 1;
                        numero_pseudo_mot = k_p;
                        // max_min_x_y = filtrage.Method_VS_Filtrage_conventionnel_plus_Dat_et_Detection_lim_max_min_max_min_x_y(data_k, rayon_filtre_traject, sigma_p_filtre_traject);
                        // DAT_par_pseudo_mot = filtrage.Method_VS_Filtrage_conventionnel_plus_Dat_et_Detection_lim_max_min_DAT(data_k, rayon_filtre_traject, sigma_p_filtre_traject);
                        // points_filtre = filtrage.Method_VS_Filtrage_conventionnel_plus_Dat_et_Detection_lim_max_min_points_filtre(data_k, rayon_filtre_traject, sigma_p_filtre_traject);

                        //[max_min_x_y, DAT_par_pseudo_mot, points_filtre] = VS_Filtrage_conventionnel_plus_Dat_et_Detection_lim_max_min(data_k, rayon_filtre_traject, sigma_p_filtre_traject);


                        Num_pseudo_mot = new int[M];
                        for (int i = 0; i < M; i++)
                        {
                            Num_pseudo_mot[i] = k_p;
                        }

                    }

                }

                /*
                 *  ajout de la pression du stylo et constitution de la matrice des de points pseudo mots à trois indices (numéro pseudomot , x y pression , noméro point) avec un indice de longueur variable : numero points 
                 * */
                taille_de_tous_les_pseudo_mots_en_nbr_de_points = new int[nbr_pseudo_mot, 2];

                pression_baissee = 1;
                pression_levee = 0;

                k_p = 0;
                for (int k = 0; k < nbr_pseudo_mot; k++)
                {
                    data_k = new double[taille_pseudo_mot[k, 1], 2];
                    numero_pseudo_mot = k;
                    data_k = mat_pseudo_mot[k]; //data_k = mat_pseudo_mot(:,:, k);
                                                /* for (int h=0;h< taille_pseudo_mot.GetLength(0); h++)
                                                 { System.Console.WriteLine(taille_pseudo_mot[h, 1]); }*/

                    //data_k = data_k(1 : taille_pseudo_mot(k, 2),:);
                    //mat_pseudo_mot_de_points_filtres_avec_pression = new double[numero_pseudo_mot][,3];
                    M = data_k.GetLength(0);
                    //System.Console.WriteLine(M);
                    points_filtres_pression_pseudo_mot = new double[M, 3];
                    points_filtres_pression_pseudo_mot_2 = new double[M + 2, 3];
                    if (M > taille_minimale_pseudo_mot)
                        k_p += 1;
                    for (int i = 0; i < M; i++)
                    {
                        points_filtres_pression_pseudo_mot[i, 0] = data_k[i, 0]; // points_filtres_pression_pseudo_mot = [points_filtres_pression_pseudo_mot; data_k(i,:), pression_baissee];
                        points_filtres_pression_pseudo_mot[i, 1] = data_k[i, 1];
                        points_filtres_pression_pseudo_mot[i, 2] = pression_baissee;

                    }
                    //points_filtres_pression_pseudo_mot = [points_filtres_pression_pseudo_mot(1,1), points_filtres_pression_pseudo_mot(1,2), pression_levee; points_filtres_pression_pseudo_mot; points_filtres_pression_pseudo_mot(M,1), points_filtres_pression_pseudo_mot(M,2), pression_levee]; 

                    points_filtres_pression_pseudo_mot_2[0, 0] = points_filtres_pression_pseudo_mot[0, 0];
                    points_filtres_pression_pseudo_mot_2[0, 1] = points_filtres_pression_pseudo_mot[0, 1];
                    points_filtres_pression_pseudo_mot_2[0, 2] = pression_levee;
                    for (int i = 1; i <= M; i++)
                    {
                        points_filtres_pression_pseudo_mot_2[i, 0] = points_filtres_pression_pseudo_mot[i - 1, 0];
                        points_filtres_pression_pseudo_mot_2[i, 1] = points_filtres_pression_pseudo_mot[i - 1, 1];
                        points_filtres_pression_pseudo_mot_2[i, 2] = points_filtres_pression_pseudo_mot[i - 1, 2];
                    }
                    points_filtres_pression_pseudo_mot_2[M + 1, 0] = points_filtres_pression_pseudo_mot[M - 1, 0];
                    points_filtres_pression_pseudo_mot_2[M + 1, 1] = points_filtres_pression_pseudo_mot[M - 1, 1];      //????
                    points_filtres_pression_pseudo_mot_2[M + 1, 2] = pression_levee;

                    data_k_filtree = new double[data_k.GetLength(0), data_k.GetLength(1)];
                    data_k_filtree = data_k;
                    for (int indice_point = 0; indice_point < points_filtres_pression_pseudo_mot_2.GetLength(0); indice_point++)
                    {

                        mat_pseudo_mot_de_points_filtres_avec_pression[numero_pseudo_mot][indice_point, 0] = points_filtres_pression_pseudo_mot_2[indice_point, 0];     // à verifier la taille du vecteur
                        mat_pseudo_mot_de_points_filtres_avec_pression[numero_pseudo_mot][indice_point, 1] = points_filtres_pression_pseudo_mot_2[indice_point, 1];
                        mat_pseudo_mot_de_points_filtres_avec_pression[numero_pseudo_mot][indice_point, 2] = points_filtres_pression_pseudo_mot_2[indice_point, 2];
                        //mat_pseudo_mot_de_points_filtres_avec_pression(indice_point,:, numero_pseudo_mot) = points_filtres_pression_pseudo_mot(indice_point,:);

                    }

                    for (int indice_point = 0; indice_point < data_k_filtree.GetLength(0); indice_point++)
                    {
                        mat_data_k_filtree_pseudo_mot[numero_pseudo_mot][indice_point, 0] = data_k_filtree[indice_point, 0];
                        mat_data_k_filtree_pseudo_mot[numero_pseudo_mot][indice_point, 1] = data_k_filtree[indice_point, 1];

                        //mat_data_k_filtree_pseudo_mot(indice_point,:, numero_pseudo_mot) = data_k_filtree(indice_point,:);

                    }
                    taille_de_tous_les_pseudo_mots_en_nbr_de_points[k, 0] = M;
                    taille_de_tous_les_pseudo_mots_en_nbr_de_points[k, 1] = points_filtres_pression_pseudo_mot_2.GetLength(0);

                }

                //Traitement des pseudo - mots pour leurs segmentations en traits beta - elliptiques et l'extraction de leurs paramètres

                // Representation graphique de la trajectoire de la proposition manuscrite à traiter


                // figure (51);
                // hold off;
                for (int k = 0; k < nbr_pseudo_mot; k++)
                {
                    numero_pseudo_mot = k;
                    points_filtres_avec_pression = new double[mat_pseudo_mot_de_points_filtres_avec_pression[k].GetLength(0), mat_pseudo_mot_de_points_filtres_avec_pression[k].GetLength(1)];
                    points_filtres_avec_pression = mat_pseudo_mot_de_points_filtres_avec_pression[k];
                    //figure(51);
                    //axis equal;
                    //plot(points_filtres_avec_pression(:, 1), points_filtres_avec_pression(:, 2), 'Color',[.3 .7 .999], 'lineStyle', ':');
                    //hold on;

                }

                int taille_points_new = 0;
                // Traitement des pseudo - mots : segmentation en traits Bêta - elliptique + extraction des caractéristiques
                nbr_pseudo_mot_traite = 0;
                k_p = 0;
                taille_vect_param = 0;
                // Détermination taille matrice param
                int nombre_colonne = 0;
                int nombre_ligne_matrice_trajectoire = 0;
                for (int k = 0; k < nbr_pseudo_mot; k++)
                {
                    numero_pseudo_mot = k;
                    //points_filtres_avec_pression = mat_pseudo_mot_de_points_filtres_avec_pression(:,:, k);
                    //points_filtres_avec_pression = points_filtres_avec_pression(1 : taille_de_tous_les_pseudo_mots_en_nbr_de_points(k, 2), :);
                    points_filtres_avec_pression = new double[mat_pseudo_mot_de_points_filtres_avec_pression[k].GetLength(0), mat_pseudo_mot_de_points_filtres_avec_pression[k].GetLength(1)];
                    points_filtres_avec_pression = mat_pseudo_mot_de_points_filtres_avec_pression[k];

                    data_k_filtree = new double[mat_data_k_filtree_pseudo_mot[k].GetLength(0), mat_data_k_filtree_pseudo_mot[k].GetLength(1)];
                    data_k_filtree = mat_data_k_filtree_pseudo_mot[k];
                    //data_k_filtree = data_k_filtree(1 : taille_de_tous_les_pseudo_mots_en_nbr_de_points(k, 1), :);
                    M = taille_de_tous_les_pseudo_mots_en_nbr_de_points[k, 0];
                    if (M > taille_minimale_pseudo_mot)
                    {
                        k_p += 1;
                        if (points_filtres_avec_pression.GetLength(0) > taille_minimale_points_filtres_avec_pression)
                        {
                            nbr_pseudo_mot_traite += 1;
                            //[vect_param, matrice_param, param_trajectoire_BC, DAT_BC, points, nbr_trait, famille_tg_horiz] = VS_new_beta_ellipse_stretegy_Beta_chevauchees_s_L(points_filtres_avec_pression, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
                            //ligne = new_beta_ellipse.Method_ligne_vecteur_param(points_filtres_avec_pression, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
                            //colonne = new_beta_ellipse.Method_colonne_vecteur_param(points_filtres_avec_pression, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
                            //vect_param = new double[ligne, colonne];
                            //vect_param = new_beta_ellipse.Method_matrice_param(points_filtres_avec_pression, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
                            
                            points_new = new_beta_ellipse_DF.Method_points_DF(points_filtres_avec_pression, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
                            taille_points_new += points_new.GetLength(0);

                            nombre_colonne += colonne;
                            nombre_ligne_matrice_trajectoire += mat_pseudo_mot_de_points_filtres_avec_pression[k].GetLength(0);

                        }
                    }

                }
                /////// remplissage matrice param ////////////
                int taille_point_Trajectoire = taille_points_new + 4 + ((nbr_pseudo_mot-1) * 2);
                double[,] point_Trajectoire = new double[taille_point_Trajectoire , 2];
                mmatrice_param_1 = new double[16, nombre_colonne];
                //points_total_final_filtrer_4 = new double[nbr_pseudo_mot][,];
                int indice_bloc_points = 0;
                ind = 0;
                int ind_points_traject = 2;
                point_Trajectoire[0, 0] = 0;
                point_Trajectoire[0, 1] = 0;
                point_Trajectoire[1, 0] = 0;
                point_Trajectoire[1, 1] = 0;
                for (int k = 0; k < nbr_pseudo_mot; k++)
                {
                    numero_pseudo_mot = k;

                    points_filtres_avec_pression = new double[mat_pseudo_mot_de_points_filtres_avec_pression[k].GetLength(0), mat_pseudo_mot_de_points_filtres_avec_pression[k].GetLength(1)];
                    points_filtres_avec_pression = mat_pseudo_mot_de_points_filtres_avec_pression[k];

                    data_k_filtree = new double[mat_data_k_filtree_pseudo_mot[k].GetLength(0), mat_data_k_filtree_pseudo_mot[k].GetLength(1)];
                    data_k_filtree = mat_data_k_filtree_pseudo_mot[k];
                    //data_k_filtree = data_k_filtree(1 : taille_de_tous_les_pseudo_mots_en_nbr_de_points(k, 1), :);
                    M = taille_de_tous_les_pseudo_mots_en_nbr_de_points[k, 0];
                    if (M > taille_minimale_pseudo_mot)
                    {
                        k_p += 1;
                        if (points_filtres_avec_pression.GetLength(0) > taille_minimale_points_filtres_avec_pression)
                        {
                            
                            nbr_pseudo_mot_traite += 1;
                            //[vect_param, matrice_param, param_trajectoire_BC, DAT_BC, points, nbr_trait, famille_tg_horiz] = VS_new_beta_ellipse_stretegy_Beta_chevauchees_s_L(points_filtres_avec_pression, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
                            //ligne = new_beta_ellipse.Method_ligne_vecteur_param(points_filtres_avec_pression, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
                            //colonne = new_beta_ellipse.Method_colonne_vecteur_param(points_filtres_avec_pression, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
                            //vect_param = new double[colonne, ligne];
                            //vect_param = new_beta_ellipse.Method_matrice_param(points_filtres_avec_pression, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
                            //points_total_final_filtrer_segment_k = new_beta_ellipse.Method_points(data_k, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
                            points_total_final_filtrer_segment_k = new_beta_ellipse_DF.Method_points_DF(points_filtres_avec_pression, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
                            indice_bloc_points += 1;
                            
                            for (int i1 = 0; i1 < points_total_final_filtrer_segment_k.GetLength(0); i1++)
                              { //for (int j1 = 0; j1 < points_total_final_filtrer_segment_k.GetLength(1); j1++)
                                 // {
                                      point_Trajectoire[ind_points_traject, 0] = points_total_final_filtrer_segment_k[i1, 0];
                                      point_Trajectoire[ind_points_traject, 1] = points_total_final_filtrer_segment_k[i1, 1];
                                      ind_points_traject++;
                                   
                                //}      
                            }
                            

                            /* for (int i1 = 0; i1 < points_filtres_avec_pression.GetLength(0); i1++)
                             { //for (int j1 = 0; j1 < points_total_final_filtrer_segment_k.GetLength(1); j1++)
                               // {
                                 point_Trajectoire[ind_points_traject, 0] = points_filtres_avec_pression[i1, 0];
                                 point_Trajectoire[ind_points_traject, 1] = points_filtres_avec_pression[i1, 1];
                                 ind_points_traject++;
                                 //}      
                             }
                             */
                            point_Trajectoire[ind_points_traject, 0] = 0;
                            point_Trajectoire[ind_points_traject, 1] = 0;
                            ind_points_traject++;
                            point_Trajectoire[ind_points_traject, 0] = 0;
                            point_Trajectoire[ind_points_traject, 1] = 0;
                            ind_points_traject++;
                            

                            //taille_vect_param += colonne; 
                            //for (int h = 0; h < vect_param.GetLength(0); h++)


                        }
                        else
                            points_total_final_filtrer_segment_k = points_filtres_avec_pression;
                    }
                    else
                        points_total_final_filtrer_segment_k = points_filtres_avec_pression;


                    int taille_pure_points_total_final_filtrer_segment_k = 0;
                    for (int i = 0; i < points_total_final_filtrer_segment_k.GetLength(0); i++)
                    {
                        if ((points_total_final_filtrer_segment_k[i, 0] != 0) || (points_total_final_filtrer_segment_k[i, 1] != 0))
                            taille_pure_points_total_final_filtrer_segment_k++;
                    }

                    double[,] pure_data_filtrer_segment_k = new double[taille_pure_points_total_final_filtrer_segment_k, 2];
                    double[] pure_data_norm_x_segment_k = new double[pure_data_filtrer_segment_k.GetLength(0)];
                    double[] pure_data_norm_y_segment_k = new double[pure_data_filtrer_segment_k.GetLength(0)];
                    int compteur = 0;
                    for (int i = 0; i < points_total_final_filtrer_segment_k.GetLength(0); i++)
                    {
                        if ((points_total_final_filtrer_segment_k[i, 0] != 0) || (points_total_final_filtrer_segment_k[i, 1] != 0))
                        {
                            pure_data_filtrer_segment_k[compteur, 0] = points_total_final_filtrer_segment_k[i, 0];
                            pure_data_filtrer_segment_k[compteur, 1] = points_total_final_filtrer_segment_k[i, 1];
                            pure_data_norm_x_segment_k[compteur] = pure_data_filtrer_segment_k[compteur, 0];
                            pure_data_norm_y_segment_k[compteur] = pure_data_filtrer_segment_k[compteur, 1];
                            compteur++;
                        }
                    }

                    Maxi_X_P = Math.Max(max_matrice(pure_data_norm_x_segment_k), Maxi_X_P);
                    Maxi_Y_P = Math.Max(max_matrice(pure_data_norm_y_segment_k), Maxi_Y_P);
                    Mini_X_P = Math.Min(min_matrice(pure_data_norm_x_segment_k), Mini_X_P);
                    Mini_Y_P = Math.Min(min_matrice(pure_data_norm_y_segment_k), Mini_Y_P);
                }

                /// !!
                
                //Console.ReadKey();

                ///////// extraction des paramètres DF ////////////////
                point_Trajectoire[point_Trajectoire.GetLength(0) - 2 , 0] = 0;
                point_Trajectoire[point_Trajectoire.GetLength(0) - 2, 1] = 0;
                point_Trajectoire[point_Trajectoire.GetLength(0) - 1, 0] = 0;
                point_Trajectoire[point_Trajectoire.GetLength(0) - 1, 1] = 0;
                rayon_filtre_V = 1; //% 2;
                sigma_p_filtre_V = 0.1; //% 0.5;

                rayon_filtre_traject = 1; //% 6;% 4;
                sigma_p_filtre_traject = 0.1; //% 5; % 4.5;
               
                matrice_param_descripteurs_fourier = DF.Method_vecteur_param_etendu(point_Trajectoire, nombre_de_harminiques_considerees, rayon_filtre_V, sigma_p_filtre_V, rayon_filtre_traject, sigma_p_filtre_traject, label_du_script_choisi);
               

            }



            return matrice_param_descripteurs_fourier;
        }

        //////////////////////////////
        
        internal double[,] method_mmatrice_param_1(double[] xy_echantillon_courant)
        {
            throw new NotImplementedException();
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



        // taille matrice param

        

    }
}
