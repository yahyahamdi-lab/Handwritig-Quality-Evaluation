using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{
   public class Re_echantillonnage_de_la_trajectoire_a_Delta_T_fixe_1
    {
        public double[,] point_Trajectoire_matrice_xytz_colonnes_1, points_segment_echantillonne_et_tronque_des_traits_excedent, point_Trajectoire_segment_filtre, point_Trajectoire_Re_echantillonne_matrice_3_colonnes, point_Trajectoire, point_Trajectoire_segment_Re_echantillonne_a_Delta_T_fixe;
        double position_en_pourcentage_par_rapport_au_point_arrivee, position_en_pourcentage_par_rapport_au_point_depart, abscisse_curviligne_k, Diff_signe_diff_delta_deplacement_k, Delta_deplacement_avant_preced, Delta_deplacement_preced, Delta_deplacement_suivant, Delta_deplacement_apres_suivant, x_apres_suivant, x_suivant, y_suivant, y_apres_suivant, longueur_curviligne, Delta_deplacement, x_courant, y_courant, x_preced, y_preced, longueur_bout_segment_en_pourcent, Delta_T;

        int size_xytz_colonnes, l, taille_point_Trajectoire_3colonnes, indice_point, indice_fin_apres_elimination_traits_excedentaires, indice_deb_apres_elimination_traits_excedentaires, size_segment_Re_echantillonne, ind, taille_segment_Re_echantillonne_a_Delta_T_fixe, taille_du_pseudo_mot_fini, indice_point_courant_dans_pseudo_mot_courant, numero_pseudo_mot_courant, nombre_de_pseudo_mots, taille_pseudo_mot_courant, jjjh, taille_point_Trajectoire_segment, taille_Trajectoire_segment_avec_quantification, taille_du_pseudo_mot_avec_quantification_raffinee, KMH_segm_fin_stroke_strictement_vertical, KMH_segm_deb_stroke_strictement_vertical, jjjh_reprise, abc, drapeau_debut_passage_levee, size_point_Trajectoire_segment, KMH_segm_deb_stroke_strictement_horizontal, KMH_segm_fin_stroke_strictement_horizontal, j_prospection, drapeau_fin_segment;
        double signe_diff_delta_deplacement, signe_diff_delta_deplacement_precede, Diff_signe_diff_delta_deplacement, Diff_Delta_deplacement, x_temps_present, y_temps_present, temps_present, t_k_passe, temps_passe, t_k, x_iiih, t_iiih, y_iiih,  x_k_moins_1, y_k_moins_1, x_k, y_k;
        int[] Re_echantillonne_a_Delta_T_fixe;
        double[] vect_Diff_signe_diff_Delta_deplacement, vect_abscisse_curviligne;


        VS_filtre_lineaire_1 filtre = new VS_filtre_lineaire_1();
        public double[,] method_Re_echantillonnage_de_la_trajectoire_a_Delta_T_fixe( double[,] point_Trajectoire_matrice_xytz_colonnes)
        {
            //point_Trajectoire_Re_echantillonne_matrice_3_colonnes = [0, 0, 0; 0, 0, 0];
            Delta_T = 10;
            size_xytz_colonnes = point_Trajectoire_matrice_xytz_colonnes.GetLength(0);
            point_Trajectoire_matrice_xytz_colonnes_1 = new double[size_xytz_colonnes, point_Trajectoire_matrice_xytz_colonnes.GetLength(1)+1];
            for (int i = 0; i < point_Trajectoire_matrice_xytz_colonnes.GetLength(0); i++)
            {
                for (int j = 0; j < point_Trajectoire_matrice_xytz_colonnes.GetLength(1); j++)
                    point_Trajectoire_matrice_xytz_colonnes_1[i, j] = point_Trajectoire_matrice_xytz_colonnes[i, j];

                point_Trajectoire_matrice_xytz_colonnes_1[i, point_Trajectoire_matrice_xytz_colonnes_1.GetLength(1) - 1] = (size_xytz_colonnes - 1) * Delta_T;
            }

            point_Trajectoire = new double[point_Trajectoire_matrice_xytz_colonnes_1.GetLength(0), point_Trajectoire_matrice_xytz_colonnes_1.GetLength(1)];
            if (point_Trajectoire_matrice_xytz_colonnes_1.GetLength(1) >= 3)
            {
                for (int i = 0; i < point_Trajectoire_matrice_xytz_colonnes_1.GetLength(0); i++)
                {
                    for (int j = 0; j < point_Trajectoire_matrice_xytz_colonnes_1.GetLength(1); j++)
                        point_Trajectoire[i, j] = point_Trajectoire_matrice_xytz_colonnes_1[i, j];
                }




                // comptage 

                drapeau_debut_passage_levee = 0;
                abc = point_Trajectoire.GetLength(0);
                nombre_de_pseudo_mots = 0;
                taille_pseudo_mot_courant = 0;
                for (int iiih = 0; iiih < abc; iiih++)
                {
                    x_iiih = point_Trajectoire[iiih, 0];
                    y_iiih = point_Trajectoire[iiih, 1];
                    if ((x_iiih != 0) || (y_iiih != 0))
                    {
                        taille_pseudo_mot_courant = taille_pseudo_mot_courant + 1;
                        drapeau_debut_passage_levee = 0;
                    }
                    else if (drapeau_debut_passage_levee == 0)
                    {
                        if (taille_pseudo_mot_courant >= 1)
                            nombre_de_pseudo_mots = nombre_de_pseudo_mots + 1;

                        drapeau_debut_passage_levee = 1;
                        taille_pseudo_mot_courant = 0;
                    }

                }
                // %%%% comptage de la taille des pseudos mots %%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                drapeau_debut_passage_levee = 0;
                int[] vect_taille_pseudos_mots = new int[nombre_de_pseudo_mots];  //zeros(nombre_de_pseudo_mots, 1);
                Re_echantillonne_a_Delta_T_fixe = new int[nombre_de_pseudo_mots];

                for (int t = 0; t < nombre_de_pseudo_mots; t++)
                    vect_taille_pseudos_mots[t] = 0;

                taille_pseudo_mot_courant = 0;
                numero_pseudo_mot_courant = -1;
                for (int iiih = 0; iiih < abc; iiih++)
                {
                    x_iiih = point_Trajectoire[iiih, 0];
                    y_iiih = point_Trajectoire[iiih, 1];
                    if ((x_iiih != 0) || (y_iiih != 0))
                    {
                        taille_pseudo_mot_courant = taille_pseudo_mot_courant + 1; /// !!!!!!!!!
                        drapeau_debut_passage_levee = 0;
                    }
                    else if (drapeau_debut_passage_levee == 0)
                    {
                        if (taille_pseudo_mot_courant >= 1)
                        {
                            numero_pseudo_mot_courant = numero_pseudo_mot_courant + 1;
                            vect_taille_pseudos_mots[numero_pseudo_mot_courant] = taille_pseudo_mot_courant;
                            
                        }

                        drapeau_debut_passage_levee = 1;
                        taille_pseudo_mot_courant = 0;
                    }

                }

                // préparation de vecteur de matrice mat_pseudo_mot
                drapeau_debut_passage_levee = 0;
                numero_pseudo_mot_courant = 0;
                indice_point_courant_dans_pseudo_mot_courant = 0;
                double[][,] mat_pseudo_mot = new double[nombre_de_pseudo_mots][,];
                double[][,] point_Trajectoire_segment_Re_echantillonne_a_Delta_T = new double[nombre_de_pseudo_mots][,];
                double[][,] mat_pseudo_mot_2 = new double[nombre_de_pseudo_mots][,];
                //Console.WriteLine(nombre_de_pseudo_mots);
                for (int ji = 0; ji < nombre_de_pseudo_mots; ji++)
                {
                    mat_pseudo_mot[ji] = new double[vect_taille_pseudos_mots[ji], 3];
                    //Console.WriteLine(vect_taille_pseudos_mots[ji]);
                }


                // comptage taille  point_Trajectoire_segment_Re_echantillonne_a_Delta_T_fixe
                taille_segment_Re_echantillonne_a_Delta_T_fixe = 0;
                for (int iiih = 0; iiih < abc; iiih++)
                {
                    x_iiih = point_Trajectoire[iiih, 0];
                    y_iiih = point_Trajectoire[iiih, 1];
                    t_iiih = point_Trajectoire[iiih, 2];

                    if ((x_iiih != 0) || (y_iiih != 0))
                    {
                        indice_point_courant_dans_pseudo_mot_courant = indice_point_courant_dans_pseudo_mot_courant + 1;
                        //Console.WriteLine(indice_point_courant_dans_pseudo_mot_courant -1);
                        mat_pseudo_mot[numero_pseudo_mot_courant][indice_point_courant_dans_pseudo_mot_courant - 1, 0] = x_iiih;
                        mat_pseudo_mot[numero_pseudo_mot_courant][indice_point_courant_dans_pseudo_mot_courant - 1, 1] = y_iiih;
                        mat_pseudo_mot[numero_pseudo_mot_courant][indice_point_courant_dans_pseudo_mot_courant - 1, 2] = t_iiih;

                        drapeau_debut_passage_levee = 0;

                    }
                    else if (drapeau_debut_passage_levee == 0)
                    {
                        taille_du_pseudo_mot_fini = indice_point_courant_dans_pseudo_mot_courant;
                        int taille_segment_rech_delta_fixe = 1;
                        if (taille_du_pseudo_mot_fini >= 1)
                        {
                            //%%%%%%%%%%%% Re echantillonnage de la_trajectoire a Delta_T fixe %%%%%%%%%%%%
                            // %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

                            taille_segment_Re_echantillonne_a_Delta_T_fixe ++;
                            t_k = mat_pseudo_mot[numero_pseudo_mot_courant][0, 2];
                            t_k_passe = t_k;
                            temps_passe = t_k;
                            temps_present = temps_passe + Delta_T;

                            for (int k = 1; k < taille_du_pseudo_mot_fini; k++)
                            {
                                x_k = mat_pseudo_mot[numero_pseudo_mot_courant][k, 0];
                                y_k = mat_pseudo_mot[numero_pseudo_mot_courant][k, 1];
                                t_k = mat_pseudo_mot[numero_pseudo_mot_courant][k, 2];

                                while ((t_k_passe < temps_present) && (temps_present <= t_k))
                                {
                                    x_k_moins_1 = mat_pseudo_mot[numero_pseudo_mot_courant][Math.Max((k - 1), 0), 0];//point_Trajectoire_segment(max((k - 1), 1), 1);
                                    y_k_moins_1 = mat_pseudo_mot[numero_pseudo_mot_courant][Math.Max((k - 1), 0), 1];

                                    x_temps_present = x_k_moins_1 + ((x_k - x_k_moins_1) * ((temps_present - t_k_passe) / (t_k - t_k_passe)));
                                    y_temps_present = y_k_moins_1 + ((y_k - y_k_moins_1) * ((temps_present - t_k_passe) / (t_k - t_k_passe)));

                                    taille_segment_Re_echantillonne_a_Delta_T_fixe ++;

                                    temps_passe = temps_present;
                                    temps_present = temps_passe + Delta_T;
                                    taille_segment_rech_delta_fixe++;

                                }
                                t_k_passe = t_k;

                            }

                            x_temps_present = mat_pseudo_mot[numero_pseudo_mot_courant][mat_pseudo_mot[numero_pseudo_mot_courant].GetLength(0)-1, 0]; //point_Trajectoire_segment(size_point_Trajectoire_segment, 1);
                            y_temps_present = mat_pseudo_mot[numero_pseudo_mot_courant][mat_pseudo_mot[numero_pseudo_mot_courant].GetLength(0) - 1, 1];
                            temps_present = mat_pseudo_mot[numero_pseudo_mot_courant][mat_pseudo_mot[numero_pseudo_mot_courant].GetLength(0) - 1, 2];

                            Re_echantillonne_a_Delta_T_fixe[numero_pseudo_mot_courant] = taille_segment_rech_delta_fixe;
                            numero_pseudo_mot_courant = numero_pseudo_mot_courant + 1;
                            indice_point_courant_dans_pseudo_mot_courant = 0;
                            
                        }

                        drapeau_debut_passage_levee = 1;

                    }
                    

                }

                //taille_a_Delta_T_fixe = 4 + taille_segment_Re_echantillonne_a_Delta_T_fixe + (2 * (nombre_de_pseudo_mots -1));
                for (int ji = 0; ji < nombre_de_pseudo_mots; ji++)
                {
                    point_Trajectoire_segment_Re_echantillonne_a_Delta_T[ji] = new double[Re_echantillonne_a_Delta_T_fixe[ji], 3];
                    //Console.WriteLine(vect_taille_pseudos_mots[ji]);
                }


                drapeau_debut_passage_levee = 0;
                numero_pseudo_mot_courant = 0;
                indice_point_courant_dans_pseudo_mot_courant = 0;


                //point_Trajectoire_segment_Re_echantillonne_a_Delta_T_fixe = new double[taille_a_Delta_T_fixe, 3];
                //point_Trajectoire_Re_echantillonne_matrice_3_colonnes = new double[taille_a_Delta_T_fixe, 3];
                //point_Trajectoire_Re_echantillonne_matrice_3_colonnes[0, 0] = 0;
                //point_Trajectoire_Re_echantillonne_matrice_3_colonnes[0, 1] = 0;
                //point_Trajectoire_Re_echantillonne_matrice_3_colonnes[0, 2] = 0;
                taille_point_Trajectoire_3colonnes = 0;
                for (int iiih = 0; iiih < abc; iiih++)
                {
                    x_iiih = point_Trajectoire[iiih, 0];
                    y_iiih = point_Trajectoire[iiih, 1];
                    t_iiih = point_Trajectoire[iiih, 2];

                    if ((x_iiih != 0) || (y_iiih != 0))
                    {
                        indice_point_courant_dans_pseudo_mot_courant = indice_point_courant_dans_pseudo_mot_courant + 1;
                        //Console.WriteLine(indice_point_courant_dans_pseudo_mot_courant -1);
                        mat_pseudo_mot[numero_pseudo_mot_courant][indice_point_courant_dans_pseudo_mot_courant - 1, 0] = x_iiih;
                        mat_pseudo_mot[numero_pseudo_mot_courant][indice_point_courant_dans_pseudo_mot_courant - 1, 1] = y_iiih;
                        mat_pseudo_mot[numero_pseudo_mot_courant][indice_point_courant_dans_pseudo_mot_courant - 1, 2] = t_iiih;

                        drapeau_debut_passage_levee = 0;

                    }
                    else if (drapeau_debut_passage_levee == 0)
                    {
                        taille_du_pseudo_mot_fini = indice_point_courant_dans_pseudo_mot_courant;
                        ind = 0;
                        if (taille_du_pseudo_mot_fini >= 1)
                        {
                            //%%%%%%%%%%%% Re echantillonnage de la_trajectoire a Delta_T fixe %%%%%%%%%%%%
                            // %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                            point_Trajectoire_segment_Re_echantillonne_a_Delta_T[numero_pseudo_mot_courant][ind, 0] = mat_pseudo_mot[numero_pseudo_mot_courant][0, 0];
                            point_Trajectoire_segment_Re_echantillonne_a_Delta_T[numero_pseudo_mot_courant][ind, 1] = mat_pseudo_mot[numero_pseudo_mot_courant][0, 1];
                            point_Trajectoire_segment_Re_echantillonne_a_Delta_T[numero_pseudo_mot_courant][ind, 2] = mat_pseudo_mot[numero_pseudo_mot_courant][0, 2];
                            ind++;
                            t_k = mat_pseudo_mot[numero_pseudo_mot_courant][0, 2];
                            t_k_passe = t_k;
                            temps_passe = t_k;
                            temps_present = temps_passe + Delta_T;

                            for (int k = 1; k < taille_du_pseudo_mot_fini; k++)
                            {
                                x_k = mat_pseudo_mot[numero_pseudo_mot_courant][k, 0];
                                y_k = mat_pseudo_mot[numero_pseudo_mot_courant][k, 1];
                                t_k = mat_pseudo_mot[numero_pseudo_mot_courant][k, 2];

                                while ((t_k_passe < temps_present) && (temps_present <= t_k))
                                {
                                    x_k_moins_1 = mat_pseudo_mot[numero_pseudo_mot_courant][Math.Max((k - 1), 0), 0];//point_Trajectoire_segment(max((k - 1), 1), 1);
                                    y_k_moins_1 = mat_pseudo_mot[numero_pseudo_mot_courant][Math.Max((k - 1), 0), 1];

                                    x_temps_present = x_k_moins_1 + ((x_k - x_k_moins_1) * ((temps_present - t_k_passe) / (t_k - t_k_passe)));
                                    y_temps_present = y_k_moins_1 + ((y_k - y_k_moins_1) * ((temps_present - t_k_passe) / (t_k - t_k_passe)));


                                    point_Trajectoire_segment_Re_echantillonne_a_Delta_T[numero_pseudo_mot_courant][ind, 0] = x_temps_present;
                                    point_Trajectoire_segment_Re_echantillonne_a_Delta_T[numero_pseudo_mot_courant][ind, 1] = y_temps_present;
                                    point_Trajectoire_segment_Re_echantillonne_a_Delta_T[numero_pseudo_mot_courant][ind, 2] = temps_present;
                                    ind++;
                                    temps_passe = temps_present;
                                    temps_present = temps_passe + Delta_T;

                                }
                                t_k_passe = t_k;

                            }

                            x_temps_present = mat_pseudo_mot[numero_pseudo_mot_courant][mat_pseudo_mot[numero_pseudo_mot_courant].GetLength(0) - 1, 0]; //point_Trajectoire_segment(size_point_Trajectoire_segment, 1);
                            y_temps_present = mat_pseudo_mot[numero_pseudo_mot_courant][mat_pseudo_mot[numero_pseudo_mot_courant].GetLength(0) - 1, 1];
                            temps_present = mat_pseudo_mot[numero_pseudo_mot_courant][mat_pseudo_mot[numero_pseudo_mot_courant].GetLength(0) - 1, 2];

                            // point_Trajectoire_segment = point_Trajectoire_segment_Re_echantillonne_a_Delta_T_fixe;

                            // Elimination des traits excédentaires dus aux actions de frainage des muscles antagonistes//////

                            longueur_bout_segment_en_pourcent = 2.7; // 5; % 7;
                            size_segment_Re_echantillonne = point_Trajectoire_segment_Re_echantillonne_a_Delta_T[numero_pseudo_mot_courant].GetLength(0);

                            // Console.WriteLine(ind);
                            //size_segment_Re_echantillonne = point_Trajectoire_segment_Re_echantillonne_a_Delta_T_fixe.GetLength(0);
                            //[point_Trajectoire_segment_filtre, XYpoint] = VS_filtre_lineaire_1(0, 0.5, 1, size_segment_Re_echantillonne, point_Trajectoire_segment_Re_echantillonne_a_Delta_T_fixe)
                             point_Trajectoire_segment_filtre = filtre.Method_VS_filtre_lineaire_1_points(1, 0.5, 0, size_segment_Re_echantillonne, point_Trajectoire_segment_Re_echantillonne_a_Delta_T[numero_pseudo_mot_courant]);
                             // Calcul des abscisses curvilignes des points
                             
                            //for(int t= 0; t < point_Trajectoire_segment_filtre.GetLength(0); t++)
                             //   Console.WriteLine(point_Trajectoire_segment_filtre[t, 0]);

                             longueur_curviligne = 0;
                             vect_abscisse_curviligne = new double[size_segment_Re_echantillonne];
                             vect_abscisse_curviligne[0] = longueur_curviligne;
                             x_preced = point_Trajectoire_segment_filtre[0, 0];
                             y_preced = point_Trajectoire_segment_filtre[0, 1];

                             for (int k = 1; k < size_segment_Re_echantillonne; k++)
                             {
                                 x_courant = point_Trajectoire_segment_filtre[k, 0];
                                 y_courant = point_Trajectoire_segment_filtre[k, 1];

                                 Delta_deplacement = Math.Sqrt(Math.Pow((x_courant - x_preced), 2) + Math.Pow((y_courant - y_preced), 2));
                                 longueur_curviligne = longueur_curviligne + Delta_deplacement;
                                 vect_abscisse_curviligne[k] = longueur_curviligne;
                                 x_preced = x_courant;
                                 y_preced = y_courant;
                             }

                             // identification des points correspondants à des minimums locaux de vitesse 
                             x_preced = point_Trajectoire_segment_filtre[0, 0];
                             y_preced = point_Trajectoire_segment_filtre[0, 1];
                             x_courant = point_Trajectoire_segment_filtre[0, 0];
                             y_courant = point_Trajectoire_segment_filtre[0, 1];
                             x_suivant = point_Trajectoire_segment_filtre[Math.Min(1, size_segment_Re_echantillonne), 0]; // !!!
                             y_suivant = point_Trajectoire_segment_filtre[Math.Min(1, size_segment_Re_echantillonne), 1];
                             x_apres_suivant = point_Trajectoire_segment_filtre[Math.Min(2, size_segment_Re_echantillonne), 0];
                             y_apres_suivant = point_Trajectoire_segment_filtre[Math.Min(2, size_segment_Re_echantillonne), 1];

                             Delta_deplacement_avant_preced = 0;
                             Delta_deplacement_preced = 0;
                             Delta_deplacement_suivant = Math.Sqrt(Math.Pow((x_suivant - x_courant), 2) + Math.Pow((y_suivant - y_courant), 2));
                             Delta_deplacement_apres_suivant = Math.Sqrt(Math.Pow((x_apres_suivant - x_suivant), 2) + Math.Pow((y_apres_suivant - y_suivant), 2));
                             Diff_Delta_deplacement = Delta_deplacement_apres_suivant + Delta_deplacement_suivant - Delta_deplacement_preced - Delta_deplacement_avant_preced;
                             signe_diff_delta_deplacement = 0;
                             signe_diff_delta_deplacement_precede = 0;
                             Diff_signe_diff_delta_deplacement = 0;

                             //vect_Delta_deplacement = [Diff_Delta_deplacement];
                             vect_Diff_signe_diff_Delta_deplacement = new double[size_segment_Re_echantillonne];
                             vect_Diff_signe_diff_Delta_deplacement[0] = Diff_signe_diff_delta_deplacement;


                             for (int k = 1; k < size_segment_Re_echantillonne; k++)
                             {
                                 Delta_deplacement_avant_preced = Delta_deplacement_preced;
                                 Delta_deplacement_preced = Delta_deplacement_suivant;
                                 Delta_deplacement_suivant = Delta_deplacement_apres_suivant;
                                 x_suivant = x_apres_suivant;
                                 y_suivant = y_apres_suivant;
                                 x_apres_suivant = point_Trajectoire_segment_filtre[Math.Min(k + 2, size_segment_Re_echantillonne-1), 0]; 
                                 y_apres_suivant = point_Trajectoire_segment_filtre[Math.Min(k + 2, size_segment_Re_echantillonne-1), 1];
                                 Delta_deplacement_apres_suivant = Math.Sqrt(Math.Pow((x_apres_suivant - x_suivant), 2) + Math.Pow((y_apres_suivant - y_suivant), 2));

                                 Diff_Delta_deplacement = Delta_deplacement_apres_suivant + Delta_deplacement_suivant - Delta_deplacement_preced - Delta_deplacement_avant_preced;
                                 signe_diff_delta_deplacement = Math.Sign(Diff_Delta_deplacement);
                                 //vect_Delta_deplacement = [vect_Delta_deplacement; Diff_Delta_deplacement];
                                 // vect_signe_diff_Delta_deplacement = [vect_signe_diff_Delta_deplacement; signe_diff_delta_deplacement];
                                 Diff_signe_diff_delta_deplacement = signe_diff_delta_deplacement - signe_diff_delta_deplacement_precede;
                                 vect_Diff_signe_diff_Delta_deplacement[k] =  Diff_signe_diff_delta_deplacement;
                                 signe_diff_delta_deplacement_precede = signe_diff_delta_deplacement;
                             }
                             indice_deb_apres_elimination_traits_excedentaires = 0;   // !!!!!!!!!!!
                             indice_fin_apres_elimination_traits_excedentaires = size_segment_Re_echantillonne-1;

                             for (int k = 1; k < size_segment_Re_echantillonne; k++)
                             {
                                 Diff_signe_diff_delta_deplacement_k = vect_Diff_signe_diff_Delta_deplacement[k];
                                 // signe_diff_Delta_deplacement_k = vect_Diff_signe_diff_Delta_deplacement(k);
                                 abscisse_curviligne_k = vect_abscisse_curviligne[k];
                                 position_en_pourcentage_par_rapport_au_point_depart = abscisse_curviligne_k * 100 / longueur_curviligne;
                                 if ((Diff_signe_diff_delta_deplacement_k == 2) && (position_en_pourcentage_par_rapport_au_point_depart < longueur_bout_segment_en_pourcent))
                                     indice_deb_apres_elimination_traits_excedentaires = k;

                             }

                             for (int k = size_segment_Re_echantillonne-1; k>= 1; k--)
                             {
                                 Diff_signe_diff_delta_deplacement_k = vect_Diff_signe_diff_Delta_deplacement[k];
                                //signe_diff_Delta_deplacement_k = vect_Diff_signe_diff_Delta_deplacement(k);
                                 abscisse_curviligne_k = vect_abscisse_curviligne[k];
                                 position_en_pourcentage_par_rapport_au_point_arrivee = (longueur_curviligne - abscisse_curviligne_k) * 100 / longueur_curviligne;
                                 if ((Diff_signe_diff_delta_deplacement_k == 2) && (position_en_pourcentage_par_rapport_au_point_arrivee < longueur_bout_segment_en_pourcent))
                                     indice_fin_apres_elimination_traits_excedentaires = k;

                             }
                            
                             
                            
                             //points_segment_echantillonne_et_tronque_des_traits_excedent = point_Trajectoire_segment_Re_echantillonne_a_Delta_T_fixe(indice_deb_apres_elimination_traits_excedentaires: indice_fin_apres_elimination_traits_excedentaires, :);
                             indice_point = 0;
                             mat_pseudo_mot_2[numero_pseudo_mot_courant] = new double[(indice_fin_apres_elimination_traits_excedentaires - indice_deb_apres_elimination_traits_excedentaires )+1, 3];
                             for (int ii = indice_deb_apres_elimination_traits_excedentaires; ii <= indice_fin_apres_elimination_traits_excedentaires; ii++)
                             {
                                 mat_pseudo_mot_2[numero_pseudo_mot_courant][indice_point, 0] = point_Trajectoire_segment_filtre[ii, 0];
                                 mat_pseudo_mot_2[numero_pseudo_mot_courant][indice_point, 1] = point_Trajectoire_segment_filtre[ii, 1];
                                 mat_pseudo_mot_2[numero_pseudo_mot_courant][indice_point, 2] = point_Trajectoire_segment_filtre[ii, 2];
                                indice_point++;
                                
                             }
                             taille_point_Trajectoire_3colonnes += mat_pseudo_mot_2[numero_pseudo_mot_courant].GetLength(0);
                            

                            numero_pseudo_mot_courant = numero_pseudo_mot_courant + 1;
                             indice_point_courant_dans_pseudo_mot_courant = 0;
                             
                             
                        }

                        drapeau_debut_passage_levee = 1;

                    }
                    

                }
                //Console.WriteLine(taille_point_Trajectoire_3colonnes);
                taille_point_Trajectoire_3colonnes += 4 + 2 * (nombre_de_pseudo_mots - 1);
                //Console.WriteLine(taille_point_Trajectoire_3colonnes);
                l = 2;
                 point_Trajectoire_Re_echantillonne_matrice_3_colonnes = new double[taille_point_Trajectoire_3colonnes, 3];
                for (int h = 0; h< 2; h++)
                {
                    for (int c = 0; c < 3; c++)
                        point_Trajectoire_Re_echantillonne_matrice_3_colonnes[h, c] = 0;
                }

                for (int ij = 0; ij < nombre_de_pseudo_mots; ij++)
                {
                    for (int j = 0; j < mat_pseudo_mot_2[ij].GetLength(0); j++) // ligne 
                    {
                        for (int ijk = 0; ijk < mat_pseudo_mot_2[ij].GetLength(1); ijk++)  // colonne 
                            point_Trajectoire_Re_echantillonne_matrice_3_colonnes[l, ijk] = mat_pseudo_mot_2[ij][j, ijk];
                        l++;

                    }
                    point_Trajectoire_Re_echantillonne_matrice_3_colonnes[l, 0] = 0;
                    point_Trajectoire_Re_echantillonne_matrice_3_colonnes[l, 1] = 0;
                    point_Trajectoire_Re_echantillonne_matrice_3_colonnes[l++, 2] = 0;
                    point_Trajectoire_Re_echantillonne_matrice_3_colonnes[l, 0] = 0;
                    point_Trajectoire_Re_echantillonne_matrice_3_colonnes[l, 1] = 0;
                    point_Trajectoire_Re_echantillonne_matrice_3_colonnes[l++, 2] = 0;

                }
                
            }
            else
            {
                point_Trajectoire_Re_echantillonne_matrice_3_colonnes = new double[point_Trajectoire_matrice_xytz_colonnes.GetLength(0), point_Trajectoire_matrice_xytz_colonnes.GetLength(1)];
                for (int i = 0; i < point_Trajectoire_matrice_xytz_colonnes.GetLength(0); i++)
                {
                    for (int j = 0; j < point_Trajectoire_matrice_xytz_colonnes.GetLength(1); j++)
                        point_Trajectoire_Re_echantillonne_matrice_3_colonnes[i, j] = point_Trajectoire_matrice_xytz_colonnes[i, j];
                }
            }
                

            return point_Trajectoire_Re_echantillonne_matrice_3_colonnes;
        }
    }
}
