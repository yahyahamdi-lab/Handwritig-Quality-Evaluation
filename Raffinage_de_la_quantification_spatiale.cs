using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{
    class Raffinage_de_la_quantification_spatiale
    {
        double[,] point_Trajectoire_segment_avec_quantification_rafinnee_2, point_Trajectoire_segment_2, point_Trajectoire, point_Trajectoire_segment_avec_quantification_rafinnee, point_Trajectoire_avec_quantification_spatiale_rafinnee, point_Trajectoire_segment, point_Trajectoire_segment_avec_redondance_eliminee;
        int taille_du_pseudo_mot_fini, indice_point_courant_dans_pseudo_mot_courant, numero_pseudo_mot_courant, nombre_de_pseudo_mots, taille_pseudo_mot_courant, jjjh, taille_point_Trajectoire_segment, taille_Trajectoire_segment_avec_quantification, taille_du_pseudo_mot_avec_quantification_raffinee, KMH_segm_fin_stroke_strictement_vertical, KMH_segm_deb_stroke_strictement_vertical, jjjh_reprise, abc, drapeau_debut_passage_levee, size_point_Trajectoire_segment, KMH_segm_deb_stroke_strictement_horizontal, KMH_segm_fin_stroke_strictement_horizontal, j_prospection, drapeau_fin_segment;
        double x_iiih, y_iiih, y_avec_quantification_rafinnee, x_avec_quantification_rafinnee, x_jjjh, y_jjjh, x_k_moins_1, y_k_moins_1, x_k, y_k, x_jjjh_plus_1, y_jjjh_plus_1, x_jjjh_plus_1_plus_j_prospection, y_jjjh_plus_1_plus_j_prospection;
        int[] taille_pseudo_mot;
        VS_filtre_lineaire_1 filtre_1 = new VS_filtre_lineaire_1();

        public double[,] method_Raffinage_de_la_quantification_spatiale(double[,] point_Trajectoire_2)
        {

            point_Trajectoire = new double[point_Trajectoire_2.GetLength(0) + 2, point_Trajectoire_2.GetLength(1)]; //[point_Trajectoire ; 0,0; 0,0];

            for (int i = 0; i < point_Trajectoire_2.GetLength(0); i++)
            {
                for (int j = 0; j < point_Trajectoire_2.GetLength(1); j++)
                {
                    point_Trajectoire[i, j] = point_Trajectoire_2[i, j];

                }

            }

            point_Trajectoire[point_Trajectoire.GetLength(0) - 2, 0] = 0;
            point_Trajectoire[point_Trajectoire.GetLength(0) - 2, 1] = 0;
            point_Trajectoire[point_Trajectoire.GetLength(0) - 1, 0] = 0;
            point_Trajectoire[point_Trajectoire.GetLength(0) - 1, 1] = 0;

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
            //Console.WriteLine(nombre_de_pseudo_mots);
            for (int ji = 0; ji < nombre_de_pseudo_mots; ji++)
            {
                mat_pseudo_mot[ji] = new double[vect_taille_pseudos_mots[ji], 2];
                //Console.WriteLine(vect_taille_pseudos_mots[ji]);
            }


            for (int iiih = 0; iiih < abc; iiih++)
            {
                x_iiih = point_Trajectoire[iiih, 0];
                y_iiih = point_Trajectoire[iiih, 1];
                if ((x_iiih != 0) || (y_iiih != 0))
                {
                    indice_point_courant_dans_pseudo_mot_courant = indice_point_courant_dans_pseudo_mot_courant + 1;
                    //Console.WriteLine(indice_point_courant_dans_pseudo_mot_courant -1);
                    mat_pseudo_mot[numero_pseudo_mot_courant][indice_point_courant_dans_pseudo_mot_courant - 1, 0] = x_iiih;
                    mat_pseudo_mot[numero_pseudo_mot_courant][indice_point_courant_dans_pseudo_mot_courant - 1, 1] = y_iiih;

                    drapeau_debut_passage_levee = 0;

                }
                else if (drapeau_debut_passage_levee == 0)
                {
                    taille_du_pseudo_mot_fini = indice_point_courant_dans_pseudo_mot_courant;
                    if (taille_du_pseudo_mot_fini >= 1)
                    {
                        numero_pseudo_mot_courant = numero_pseudo_mot_courant + 1;
                        indice_point_courant_dans_pseudo_mot_courant = 0;
                    }

                    drapeau_debut_passage_levee = 1;

                }

            }


            // clacul taille point_Trajectoire_avec_quantification_spatiale_rafinnee
            taille_pseudo_mot = new int[nombre_de_pseudo_mots];
            int taille_point_trajectoire = 0;
            for (int iiih = 0; iiih < nombre_de_pseudo_mots; iiih++)
            {
                point_Trajectoire_segment_2 = mat_pseudo_mot[iiih];
                size_point_Trajectoire_segment = point_Trajectoire_segment_2.GetLength(0);

                // %%%%%%%%%%%% Elimination des points redendants %%%%%%%%%%%%
                // %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                // calcul taille ////
                taille_point_Trajectoire_segment = 0;
                for (int k = 1; k < size_point_Trajectoire_segment; k++)
                {
                    x_k = point_Trajectoire_segment_2[k, 0];
                    y_k = point_Trajectoire_segment_2[k, 1];
                    x_k_moins_1 = point_Trajectoire_segment_2[k - 1, 0];
                    y_k_moins_1 = point_Trajectoire_segment_2[k - 1, 1];
                    if ((x_k != x_k_moins_1) | (y_k != y_k_moins_1))
                    {
                        taille_point_Trajectoire_segment++;

                    }
                }
                ////////////////////
                taille_pseudo_mot[iiih] = taille_point_Trajectoire_segment + 1;
                int indice = 1;
                point_Trajectoire_segment_avec_redondance_eliminee = new double[taille_point_Trajectoire_segment + 1, 2];
                point_Trajectoire_segment_avec_redondance_eliminee[0, 0] = point_Trajectoire_segment_2[0, 0];
                point_Trajectoire_segment_avec_redondance_eliminee[0, 1] = point_Trajectoire_segment_2[0, 1];
                for (int k = 1; k < size_point_Trajectoire_segment; k++)
                {
                    x_k = point_Trajectoire_segment_2[k, 0];
                    y_k = point_Trajectoire_segment_2[k, 1];
                    x_k_moins_1 = point_Trajectoire_segment_2[k - 1, 0];
                    y_k_moins_1 = point_Trajectoire_segment_2[k - 1, 1];
                    if ((x_k != x_k_moins_1) | (y_k != y_k_moins_1))
                    {
                        point_Trajectoire_segment_avec_redondance_eliminee[indice, 0] = x_k;
                        point_Trajectoire_segment_avec_redondance_eliminee[indice, 1] = y_k;
                        indice++;
                    }
                }

                //  point_Trajectoire_segment = point_Trajectoire_segment_avec_redondance_eliminee;  !!!!!!!!!!!!!!!!

                point_Trajectoire_segment = new double[point_Trajectoire_segment_avec_redondance_eliminee.GetLength(0), point_Trajectoire_segment_avec_redondance_eliminee.GetLength(1)];
                for (int i = 0; i < point_Trajectoire_segment_avec_redondance_eliminee.GetLength(0); i++)
                {
                    for (int j = 0; j < point_Trajectoire_segment_avec_redondance_eliminee.GetLength(1); j++)
                    {
                        point_Trajectoire_segment[i, j] = point_Trajectoire_segment_avec_redondance_eliminee[i, j];

                    }

                }



                size_point_Trajectoire_segment = point_Trajectoire_segment.GetLength(0);
                point_Trajectoire_segment_avec_quantification_rafinnee = new double[point_Trajectoire_segment.GetLength(0), point_Trajectoire_segment.GetLength(1)];
                // point_Trajectoire_segment_avec_quantification_rafinnee = point_Trajectoire_segment; 
                for (int i = 0; i < point_Trajectoire_segment.GetLength(0); i++)
                {
                    for (int j = 0; j < point_Trajectoire_segment.GetLength(1); j++)
                    {
                        point_Trajectoire_segment_avec_quantification_rafinnee[i, j] = point_Trajectoire_segment[i, j];

                    }

                }

                jjjh = 0;   // !!!!!!!!!!!!!
                while (jjjh < size_point_Trajectoire_segment - 1)
                {
                    x_jjjh = point_Trajectoire_segment[jjjh, 0];
                    y_jjjh = point_Trajectoire_segment[jjjh, 1];
                    x_jjjh_plus_1 = point_Trajectoire_segment[jjjh + 1, 0];
                    y_jjjh_plus_1 = point_Trajectoire_segment[jjjh + 1, 1];

                    if ((x_jjjh == x_jjjh_plus_1) && (y_jjjh != y_jjjh_plus_1))
                    {
                        KMH_segm_deb_stroke_strictement_horizontal = jjjh;
                        KMH_segm_fin_stroke_strictement_horizontal = jjjh + 1;

                        j_prospection = 1;   // !!!!!!!!!!!!!!!!!!!!!
                        drapeau_fin_segment = 0;

                        if (jjjh + 1 + j_prospection < size_point_Trajectoire_segment)
                        {
                            x_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + 1 + j_prospection, 0];
                            y_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + 1 + j_prospection, 1];
                            drapeau_fin_segment = 0;
                        }
                        else
                        {
                            x_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + j_prospection, 0];
                            y_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + j_prospection, 1];
                            drapeau_fin_segment = 1;
                        }

                        while ((drapeau_fin_segment == 0) && (x_jjjh == x_jjjh_plus_1_plus_j_prospection))
                        {
                            KMH_segm_fin_stroke_strictement_horizontal = KMH_segm_fin_stroke_strictement_horizontal + 1;
                            j_prospection = j_prospection + 1;

                            if (jjjh + 1 + j_prospection < size_point_Trajectoire_segment)
                            {
                                x_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + 1 + j_prospection, 0];
                                y_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + 1 + j_prospection, 1];
                                drapeau_fin_segment = 0;
                            }
                            else
                            {
                                x_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + j_prospection, 0];
                                y_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + j_prospection, 1];
                                drapeau_fin_segment = 1;
                            }

                        }
                        for (int k = KMH_segm_deb_stroke_strictement_horizontal; k < KMH_segm_fin_stroke_strictement_horizontal; k++) // !!!!!!!!
                        {
                            x_avec_quantification_rafinnee = (x_jjjh + ((x_jjjh_plus_1_plus_j_prospection - x_jjjh) * ((k - KMH_segm_deb_stroke_strictement_horizontal) / (KMH_segm_fin_stroke_strictement_horizontal - KMH_segm_deb_stroke_strictement_horizontal))));
                            x_avec_quantification_rafinnee = Math.Round(x_avec_quantification_rafinnee * 100000);
                            x_avec_quantification_rafinnee = x_avec_quantification_rafinnee / 100000;
                            point_Trajectoire_segment_avec_quantification_rafinnee[k, 0] = (x_avec_quantification_rafinnee);
                        }
                        jjjh_reprise = jjjh + 1 + j_prospection;
                    }
                    else if ((y_jjjh == y_jjjh_plus_1) && (x_jjjh != x_jjjh_plus_1))
                    {
                        KMH_segm_deb_stroke_strictement_vertical = jjjh;
                        KMH_segm_fin_stroke_strictement_vertical = jjjh + 1;
                        j_prospection = 1;
                        drapeau_fin_segment = 0;

                        if (jjjh + 1 + j_prospection < size_point_Trajectoire_segment)
                        {
                            x_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + 1 + j_prospection, 0];
                            y_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + 1 + j_prospection, 1];
                            drapeau_fin_segment = 0;
                        }
                        else
                        {
                            x_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + j_prospection, 0];
                            y_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + j_prospection, 1];
                            drapeau_fin_segment = 1;
                        }
                        while ((drapeau_fin_segment == 0) && (y_jjjh == y_jjjh_plus_1_plus_j_prospection))
                        {
                            KMH_segm_fin_stroke_strictement_vertical = KMH_segm_fin_stroke_strictement_vertical + 1;
                            j_prospection = j_prospection + 1;
                            if (jjjh + 1 + j_prospection < size_point_Trajectoire_segment)
                            {
                                x_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + 1 + j_prospection, 0];
                                y_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + 1 + j_prospection, 1];
                                drapeau_fin_segment = 0;
                            }
                            else
                            {
                                x_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + j_prospection, 0];
                                y_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + j_prospection, 1];
                                drapeau_fin_segment = 1;
                            }
                        }
                        for (int k = KMH_segm_deb_stroke_strictement_vertical; k < KMH_segm_fin_stroke_strictement_vertical; k++) // !!!!!!!!
                        {
                            y_avec_quantification_rafinnee = (y_jjjh + ((y_jjjh_plus_1_plus_j_prospection - y_jjjh) * ((k - KMH_segm_deb_stroke_strictement_vertical) / (KMH_segm_fin_stroke_strictement_vertical - KMH_segm_deb_stroke_strictement_vertical))));
                            y_avec_quantification_rafinnee = Math.Round(y_avec_quantification_rafinnee * 100000);
                            y_avec_quantification_rafinnee = y_avec_quantification_rafinnee / 100000;
                            point_Trajectoire_segment_avec_quantification_rafinnee[k, 1] = (y_avec_quantification_rafinnee);
                        }
                        jjjh_reprise = jjjh + 1 + j_prospection;
                    }
                    else
                        jjjh_reprise = jjjh + 1;

                    jjjh = jjjh_reprise;
                }

                taille_du_pseudo_mot_avec_quantification_raffinee = point_Trajectoire_segment_avec_quantification_rafinnee.GetLength(0);
                if (taille_du_pseudo_mot_avec_quantification_raffinee > 20)
                    point_Trajectoire_segment_avec_quantification_rafinnee_2 = filtre_1.Method_VS_filtre_lineaire_1_points(5, 1, 0, point_Trajectoire_segment_avec_quantification_rafinnee.GetLength(0), point_Trajectoire_segment_avec_quantification_rafinnee);
                else
                    point_Trajectoire_segment_avec_quantification_rafinnee_2 = filtre_1.Method_VS_filtre_lineaire_1_points(1, 0.7, 0, point_Trajectoire_segment_avec_quantification_rafinnee.GetLength(0), point_Trajectoire_segment_avec_quantification_rafinnee);

                //point_Trajectoire_avec_quantification_spatiale_rafinnee = [point_Trajectoire_avec_quantification_spatiale_rafinnee; point_Trajectoire_segment_avec_quantification_rafinnee; 0,0; 0,0];
                taille_point_trajectoire += point_Trajectoire_segment_avec_quantification_rafinnee_2.GetLength(0);


            }
            taille_Trajectoire_segment_avec_quantification = 4 + (2 * (nombre_de_pseudo_mots - 1)) + taille_point_trajectoire;

            // Fin calcul ////////////////


            point_Trajectoire_avec_quantification_spatiale_rafinnee = new double[taille_Trajectoire_segment_avec_quantification, 2];
            point_Trajectoire_avec_quantification_spatiale_rafinnee[0, 0] = 0;
            point_Trajectoire_avec_quantification_spatiale_rafinnee[0, 1] = 0;
            point_Trajectoire_avec_quantification_spatiale_rafinnee[1, 0] = 0;
            point_Trajectoire_avec_quantification_spatiale_rafinnee[1, 1] = 0;
            int ind = 2;
            for (int iiih = 0; iiih < nombre_de_pseudo_mots; iiih++)
            {
                point_Trajectoire_segment_2 = mat_pseudo_mot[iiih];
                size_point_Trajectoire_segment = point_Trajectoire_segment_2.GetLength(0);

                // %%%%%%%%%%%% Elimination des points redendants %%%%%%%%%%%%
                // %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                int indice_2 = 1;
                point_Trajectoire_segment_avec_redondance_eliminee = new double[taille_pseudo_mot[iiih], 2];
                point_Trajectoire_segment_avec_redondance_eliminee[0, 0] = point_Trajectoire_segment_2[0, 0];
                point_Trajectoire_segment_avec_redondance_eliminee[0, 1] = point_Trajectoire_segment_2[0, 1];
                for (int k = 1; k < size_point_Trajectoire_segment; k++)
                {
                    x_k = point_Trajectoire_segment_2[k, 0];
                    y_k = point_Trajectoire_segment_2[k, 1];
                    x_k_moins_1 = point_Trajectoire_segment_2[k - 1, 0];
                    y_k_moins_1 = point_Trajectoire_segment_2[k - 1, 1];
                    if ((x_k != x_k_moins_1) | (y_k != y_k_moins_1))
                    {
                        point_Trajectoire_segment_avec_redondance_eliminee[indice_2, 0] = x_k;
                        point_Trajectoire_segment_avec_redondance_eliminee[indice_2, 1] = y_k;
                        indice_2++;
                    }
                }

                //  point_Trajectoire_segment = point_Trajectoire_segment_avec_redondance_eliminee;  !!!!!!!!!!!!!!!!
                point_Trajectoire_segment = new double[point_Trajectoire_segment_avec_redondance_eliminee.GetLength(0), point_Trajectoire_segment_avec_redondance_eliminee.GetLength(1)];

                for (int i = 0; i < point_Trajectoire_segment_avec_redondance_eliminee.GetLength(0); i++)
                {
                    for (int j = 0; j < point_Trajectoire_segment_avec_redondance_eliminee.GetLength(1); j++)
                    {
                        point_Trajectoire_segment[i, j] = point_Trajectoire_segment_avec_redondance_eliminee[i, j];

                    }

                }



                size_point_Trajectoire_segment = point_Trajectoire_segment.GetLength(0);
                point_Trajectoire_segment_avec_quantification_rafinnee = new double[point_Trajectoire_segment.GetLength(0), point_Trajectoire_segment.GetLength(1)];

                for (int i = 0; i < point_Trajectoire_segment.GetLength(0); i++)
                {
                    for (int j = 0; j < point_Trajectoire_segment.GetLength(1); j++)
                    {
                        point_Trajectoire_segment_avec_quantification_rafinnee[i, j] = point_Trajectoire_segment[i, j];

                    }

                }

                jjjh = 0;   // !!!!!!!!!!!!!
                while (jjjh < size_point_Trajectoire_segment - 1)
                {
                    x_jjjh = point_Trajectoire_segment[jjjh, 0];
                    y_jjjh = point_Trajectoire_segment[jjjh, 1];
                    x_jjjh_plus_1 = point_Trajectoire_segment[jjjh + 1, 0];
                    y_jjjh_plus_1 = point_Trajectoire_segment[jjjh + 1, 1];

                    if ((x_jjjh == x_jjjh_plus_1) && (y_jjjh != y_jjjh_plus_1))
                    {
                        KMH_segm_deb_stroke_strictement_horizontal = jjjh;
                        KMH_segm_fin_stroke_strictement_horizontal = jjjh + 1;

                        j_prospection = 1;   // !!!!!!!!!!!!!!!!!!!!!
                        drapeau_fin_segment = 0;

                        if (jjjh + 1 + j_prospection < size_point_Trajectoire_segment)
                        {
                            x_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + 1 + j_prospection, 0];
                            y_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + 1 + j_prospection, 1];
                            drapeau_fin_segment = 0;
                        }
                        else
                        {
                            x_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + j_prospection, 0];
                            y_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + j_prospection, 1];
                            drapeau_fin_segment = 1;
                        }

                        while ((drapeau_fin_segment == 0) && (x_jjjh == x_jjjh_plus_1_plus_j_prospection))
                        {
                            KMH_segm_fin_stroke_strictement_horizontal = KMH_segm_fin_stroke_strictement_horizontal + 1;
                            j_prospection = j_prospection + 1;

                            if (jjjh + 1 + j_prospection < size_point_Trajectoire_segment)
                            {
                                x_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + 1 + j_prospection, 0];
                                y_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + 1 + j_prospection, 1];
                                drapeau_fin_segment = 0;
                            }
                            else
                            {
                                x_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + j_prospection, 0];
                                y_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + j_prospection, 1];
                                drapeau_fin_segment = 1;
                            }

                        }
                        // System.Console.WriteLine(KMH_segm_deb_stroke_strictement_horizontal);
                        //System.Console.WriteLine(KMH_segm_fin_stroke_strictement_horizontal);
                        // Console.ReadKey();
                        for (int k = KMH_segm_deb_stroke_strictement_horizontal; k < KMH_segm_fin_stroke_strictement_horizontal; k++) // !!!!!!!!
                        {
                            x_avec_quantification_rafinnee = (x_jjjh + ((x_jjjh_plus_1_plus_j_prospection - x_jjjh) * ((k - KMH_segm_deb_stroke_strictement_horizontal) / (KMH_segm_fin_stroke_strictement_horizontal - KMH_segm_deb_stroke_strictement_horizontal))));
                            x_avec_quantification_rafinnee = Math.Round(x_avec_quantification_rafinnee * 100000);
                            x_avec_quantification_rafinnee = x_avec_quantification_rafinnee / 100000;
                            point_Trajectoire_segment_avec_quantification_rafinnee[k, 0] = (x_avec_quantification_rafinnee);
                        }
                        jjjh_reprise = jjjh + 1 + j_prospection;
                    }
                    else if ((y_jjjh == y_jjjh_plus_1) && (x_jjjh != x_jjjh_plus_1))
                    {
                        KMH_segm_deb_stroke_strictement_vertical = jjjh;
                        KMH_segm_fin_stroke_strictement_vertical = jjjh + 1;
                        j_prospection = 1;
                        drapeau_fin_segment = 0;

                        if (jjjh + 1 + j_prospection < size_point_Trajectoire_segment)
                        {
                            x_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + 1 + j_prospection, 0];
                            y_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + 1 + j_prospection, 1];
                            drapeau_fin_segment = 0;
                        }
                        else
                        {
                            x_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + j_prospection, 0];
                            y_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + j_prospection, 1];
                            drapeau_fin_segment = 1;
                        }
                        while ((drapeau_fin_segment == 0) && (y_jjjh == y_jjjh_plus_1_plus_j_prospection))
                        {
                            KMH_segm_fin_stroke_strictement_vertical = KMH_segm_fin_stroke_strictement_vertical + 1;
                            j_prospection = j_prospection + 1;
                            if (jjjh + 1 + j_prospection < size_point_Trajectoire_segment)
                            {
                                x_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + 1 + j_prospection, 0];
                                y_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + 1 + j_prospection, 1];
                                drapeau_fin_segment = 0;
                            }
                            else
                            {
                                x_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + j_prospection, 0];
                                y_jjjh_plus_1_plus_j_prospection = point_Trajectoire_segment[jjjh + j_prospection, 1];
                                drapeau_fin_segment = 1;
                            }
                        }
                        for (int k = KMH_segm_deb_stroke_strictement_vertical; k < KMH_segm_fin_stroke_strictement_vertical; k++) // !!!!!!!!
                        {
                            y_avec_quantification_rafinnee = (y_jjjh + ((y_jjjh_plus_1_plus_j_prospection - y_jjjh) * ((k - KMH_segm_deb_stroke_strictement_vertical) / (KMH_segm_fin_stroke_strictement_vertical - KMH_segm_deb_stroke_strictement_vertical))));
                            y_avec_quantification_rafinnee = Math.Round(y_avec_quantification_rafinnee * 100000);
                            y_avec_quantification_rafinnee = y_avec_quantification_rafinnee / 100000;
                            point_Trajectoire_segment_avec_quantification_rafinnee[k, 1] = (y_avec_quantification_rafinnee);
                        }
                        jjjh_reprise = jjjh + 1 + j_prospection;
                    }
                    else
                        jjjh_reprise = jjjh + 1;

                    jjjh = jjjh_reprise;
                }



                taille_du_pseudo_mot_avec_quantification_raffinee = point_Trajectoire_segment_avec_quantification_rafinnee.GetLength(0);
                if (taille_du_pseudo_mot_avec_quantification_raffinee > 20)
                    point_Trajectoire_segment_avec_quantification_rafinnee_2 = filtre_1.Method_VS_filtre_lineaire_1_points(5, 1, 0, point_Trajectoire_segment_avec_quantification_rafinnee.GetLength(0), point_Trajectoire_segment_avec_quantification_rafinnee);
                else
                    point_Trajectoire_segment_avec_quantification_rafinnee_2 = filtre_1.Method_VS_filtre_lineaire_1_points(1, 0.7, 0, point_Trajectoire_segment_avec_quantification_rafinnee.GetLength(0), point_Trajectoire_segment_avec_quantification_rafinnee);




                for (int ii = 0; ii < point_Trajectoire_segment_avec_quantification_rafinnee_2.GetLength(0); ii++)
                {
                    point_Trajectoire_avec_quantification_spatiale_rafinnee[ind, 0] = point_Trajectoire_segment_avec_quantification_rafinnee_2[ii, 0];
                    point_Trajectoire_avec_quantification_spatiale_rafinnee[ind, 1] = point_Trajectoire_segment_avec_quantification_rafinnee_2[ii, 1];
                    ind++;
                }
                point_Trajectoire_avec_quantification_spatiale_rafinnee[ind, 0] = 0;
                point_Trajectoire_avec_quantification_spatiale_rafinnee[ind, 1] = 0;
                ind++;
                point_Trajectoire_avec_quantification_spatiale_rafinnee[ind, 0] = 0;
                point_Trajectoire_avec_quantification_spatiale_rafinnee[ind, 1] = 0;
                ind++;

            }
            point_Trajectoire_avec_quantification_spatiale_rafinnee[point_Trajectoire_avec_quantification_spatiale_rafinnee.GetLength(0) - 2, 0] = 0;
            point_Trajectoire_avec_quantification_spatiale_rafinnee[point_Trajectoire_avec_quantification_spatiale_rafinnee.GetLength(0) - 2, 1] = 0;
            point_Trajectoire_avec_quantification_spatiale_rafinnee[point_Trajectoire_avec_quantification_spatiale_rafinnee.GetLength(0) - 1, 0] = 0;
            point_Trajectoire_avec_quantification_spatiale_rafinnee[point_Trajectoire_avec_quantification_spatiale_rafinnee.GetLength(0) - 1, 1] = 0;
            return point_Trajectoire_avec_quantification_spatiale_rafinnee;
        }
    }
}
