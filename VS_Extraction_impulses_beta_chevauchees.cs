using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{
    class VS_Extraction_impulses_beta_chevauchees
    {
        /*
         * la fonction Extract_impulses calcule les  5 parametres caracteristiques des differentes beta chevauchées  %%%%%
            contenues dans un signal de vitesse défini                                                                %%%%%
            les parametres d'entree etant le signal de vitesse curviligne  filtré                                     %%%%%
            les paramètres de sortie sont les caractéristiques des Betas et de la composante d'entrainement           %%%%%
            constituant le signal de vitesse curviligne à savoir le t0, tc, t1, K_hauteur, p et q 
         */
        public string ajout_impulsion_finale_nulle = "oui";
        public int maximum_inscrit;
        public double[,] extremum, Tx0, TxC, Tx1, vect_Tx0TxCTx1, Suite_Extremums_Ordonnee, Suite_Extremums_Ordonnee_2, Suite_Extremums_Ordonnee_reduite, tableau_affectation_methode_2;
        public double[] vxmax, vymax, ind_vmin, ind_vmax, vxmin, vymin, Nbre_max, maximum, debut_intervalle, maximum_intervalle, fin_intervalle;
        public double[] YY, XX, ZZ, fct_beta_immobile;
        public int[] indice_extremum_a_ellimier; //!!!!
        public double temp_depart, dernier_DPV_min_local, Rap_lim1, Rap_lim2, dernier_DPV_max_local, premier_suivant_DPV_max_local, indice_t_extremum_j_moins_, t_extremum_j_moins_1, t_extremum_j, indice_t_extremum_j, indice_t_extremum_j_moins_1, t0, tc, t1, K_hauteur, T1_beta_act, Valeur_de_beta_preced_a_t_ajustement1, t_ajustement, Vcurv_t_ajustement1, Derivee_Vsig_t_ajustement, Valeur_de_beta_actuel_a_t_ajustement1, p_v1,q, q_v1,p, t_ajustement_actuel, Vcurv_t_ajustement1_actuel, Tc_beta_act, t0_suiv, tc_suiv, t1_suiv, K_hauteur_suiv, q_suiv, p_suiv, beta_suivante_estimee_t_ajustement1, beta_actuelle_estimee_pour_nouveau_t_ajustement1, p_v2, q_v2, q_preced, p_preced, t0_preced, tc_preced, t1_preced, K_hauteur_preced, temps1, temps2;
        public double K_amplitude1, K_amplitude2, indice_data_1, indice_data_2, t0_derniere_fct_beta, tc_derniere_fct_beta, t1_derniere_fct_beta, p_derniere_fct_beta, q_derniere_fct_beta, K_derniere_fct_beta, rong_extremum_derniere_fct_beta, t0_fct_beta_immobile, tc_fct_beta_immobile, t1_fct_beta_immobile, K_fct_beta_immobile, rong_extremum_fct_beta_immobile, q_fct_beta_immobile;
        public double [,] v_min, extrema, v_max, fonctions_beta_sortie, tableau_affectation_intervalles_sortie, memoire_points_passage_beta_methode2, fonctions_beta;
        public int taille_Suite_Extremums_Ordonnee,l, i,j, uv, uvp,oui,non, nbr_intervalles_Sensibilite_plus, delta_i, delta_i2, nbr_int_fin, nbr_int_init, l_DPV, taille,indice,ind,ind2,c,b, ihp, marque_ihp, L_SEO, nombre_total_impulsions_beta,  rong_extremum, rong_extremum_preced, nbr_fb, rong_fonction1, rong_fonction2, nbr_fct_beta_exacte, p_fct_beta_immobile, indice_vect;
        public double [][,] Suite_Intervalles_Beta;
        definition_de_l_instant_t_ajustement_sur_profil_vitesse profil_vitesse;

        public double[,] Method_fonctions_beta_sortie(double[] V, double[] DPV, double[,] points, double[] T, double[] t, double[] RAP_lim, double pas) // verifier
        {


            temp_depart = T[0];
            for (int i = 0; i < T.Length; i++)
                T[i] -= temp_depart;
            XX = new double[T.Length];
            YY = new double[T.Length];
            maximum = new double[2];
            //vymin 
            XX = T;
            YY = V;
            //ZZ = DPV;
            //-------------------------------------longueur du vecteur vitesse
            l = YY.Length;
            // -----------------------------------initialisation des paramètres
            //vymin[0] = YY[0];
            //vxmin[0] = XX[0];
            //ind_vmin[0] =  (XX[0] / pas + 1);
            //v_min[0,0]= max(Math.Round(XX[0] / pas), 1);
            //v_min[0,1]= XX[0];
            //v_min[0, 2] = YY[0];

            //v_min = [v_min; (max(round(XX(1) / pas), 1)) XX(1) YY(1)] ;

            taille = 0;
            // calculer la taille de la matrice extrema
            i = 2;
            while (i < l)
            {
                // -----------------------------------detection des maxima
                if (YY[i - 1] >= YY[i - 2])   // ???
                {

                    if (YY[i - 1] >= YY[i])
                    {
                        taille += 1;
                    }
                }
                // ----------------------------------detection des minima
                if (YY[i - 1] <= YY[i - 2])
                {

                    if (YY[i - 1] <= YY[i])
                    {
                        taille += 1;
                    }

                }

                i += 1;
            }

            extrema = new double[taille + 2, 4];
            maximum[0] = Math.Round(XX[0] / pas);
            maximum[1] = 1;
            extrema[0, 0] = max_matrice(maximum);
            extrema[0, 1] = XX[0];
            extrema[0, 2] = YY[0];
            extrema[0, 3] = 0;
            indice = 1;
            
            i = 2; // à verifier 
            while (i < l)
            {
                // -----------------------------------detection des maxima
                if (YY[i - 1] >= YY[i - 2])   // ???
                {

                    if (YY[i - 1] >= YY[i])
                    {

                        extrema[indice, 0] = Math.Round(XX[i - 1] / pas) + 1;    // extrema = [extrema;  XX(i - 1) YY(i - 1) 2];
                        extrema[indice, 1] = XX[i - 1];
                        extrema[indice, 2] = YY[i - 1];
                        extrema[indice, 3] = 2;
                        indice += 1;
                    }
                }
                // ----------------------------------detection des minima
                if (YY[i - 1] <= YY[i - 2])
                {

                    if (YY[i - 1] <= YY[i])
                    {
                        extrema[indice, 0] = Math.Round(XX[i - 1] / pas) + 1;
                        extrema[indice, 1] = XX[i - 1];
                        extrema[indice, 2] = YY[i - 1];
                        extrema[indice, 3] = 0;
                        indice += 1;
                    }

                }

                i += 1;
            }
            // --------------------------------------le dernier points

            //v_min[] = Math.Round(XX[i - 1] / pas) + 1;     // v_min = [v_min; (round(XX(i - 1) / pas) + 1) XX(l) YY(l)];
            //v_min[] = XX[l];
            //v_min[] = YY[l];

            extrema[indice, 0] = Math.Round(XX[i - 1] / pas) + 1;// extrema = [extrema; (round(XX(i - 1) / pas) + 1) XX(l) YY(l) 0];
            extrema[indice, 1] = XX[l - 1];
            extrema[indice, 2] = YY[l - 1];
            extrema[indice, 3] = 0;
          
            // --------------------------------------------------------------------------
            p = 2;
            i = 1;
            // -------------------------------------Elimination des points voisins(bruit)

            /*for (int i = 0; i<extrema.Length/extrema.Rank;i++)
            {
                extremum[,0] = extrema[i,0];
                extremum[, 1] = extrema[i, 1];
                extremum[, 2] = extrema[i, 2];
                extremum[, 3] = extrema[i, 3];
                //hh = extremum;
            }*/
            extremum = new double[extrema.GetLength(0), extrema.GetLength(1)];
            extremum = extrema;

            // calcul taille Tx0
            ind = 0;
            j = 0;    //  1
            while (j <= extremum.GetLength(0) - 3) //verifier condition
            {
                if (extremum[j, 3] == 0)
                {
                    uv = 1;
                    while ((extremum[j + uv, 3] != 2) && (j + uv <= extremum.GetLength(0) - 2)) // verfifier codition
                        uv += 1;
                    if ((extremum[j + uv, 3] == 2) && (j + uv <= extremum.GetLength(0) - 2 ))
                    {
                        uvp = 1;
                        while ((extremum[j + uv + uvp, 3] != 0) && (j + uv + uvp <= extremum.GetLength(0) - 1 ))
                            uvp += 1;
                        if ((extremum[j + uv + uvp, 3] == 0) && (j + uv + uvp <= extremum.GetLength(0) - 1))
                        {
                            ind += 1;
                        }
                    }
                    j = j + uv + uvp - 1;
                }
                j += 1;

            }

            Tx0 = new double[ind, extremum.GetLength(1)];
            TxC = new double[ind, extremum.GetLength(1)];
            Tx1 = new double[ind, extremum.GetLength(1)];
            j = 0;    //  1
            ind2 = 0;
            while (j <= extremum.GetLength(0) - 3) 
            {
                if (extremum[j, 3] == 0)
                {
                    uv = 1;
                    while ((extremum[j + uv, 3] != 2) && (j + uv <= extremum.GetLength(0) - 2)) // verfifier codition
                        uv += 1;
                    if ((extremum[j + uv, 3] == 2) && (j + uv <= extremum.GetLength(0) - 2))
                    {
                        uvp = 1;
                        while ((extremum[j + uv + uvp, 3] != 0) && (j + uv + uvp <= extremum.GetLength(0) - 1))
                            uvp += 1;
                        if ((extremum[j + uv + uvp, 3] == 0) && (j + uv + uvp <= extremum.GetLength(0) - 1))
                        {

                            Tx0[ind2, 0] = extremum[j, 0];   //[Tx0; extremum(j,:)];
                            Tx0[ind2, 1] = extremum[j, 1];
                            Tx0[ind2, 2] = extremum[j, 2];
                            Tx0[ind2, 3] = extremum[j, 3];


                            TxC[ind2, 0] = extremum[j + uv, 0]; // [TxC; extremum(j + uv,:)];
                            TxC[ind2, 1] = extremum[j + uv, 1];
                            TxC[ind2, 2] = extremum[j + uv, 2];
                            TxC[ind2, 3] = extremum[j + uv, 3];

                            Tx1[ind2, 0] = extremum[j + uv + uvp, 0]; //[Tx1; extremum(j + uv + uvp,:)];
                            Tx1[ind2, 1] = extremum[j + uv + uvp, 1];
                            Tx1[ind2, 2] = extremum[j + uv + uvp, 2];
                            Tx1[ind2, 3] = extremum[j + uv + uvp, 3];

                            ind2 += 1;
                        }
                    }
                    j = j + uv + uvp - 1;
                }
                j += 1;

            }


            vect_Tx0TxCTx1 = new double[Tx0.GetLength(0), Tx0.GetLength(1) * 3];

            for (int cf = 0; cf < Tx0.GetLength(0); cf++) //vect_Tx0TxCTx1 = [Tx0, TxC, Tx1];
            {
                c = 0;
                for (int lf = 0; lf < 4; lf++)
                {
                    vect_Tx0TxCTx1[cf, c] = Tx0[cf, lf];
                    c++;
                }
                for (int lf = 0; lf < 4; lf++)
                {
                    vect_Tx0TxCTx1[cf, c] = TxC[cf, lf];
                    c++;
                }
                for (int lf = 0; lf < 4; lf++)
                {
                    vect_Tx0TxCTx1[cf, c] = Tx1[cf, lf];
                    c++;
                }
            }
            // mettre dans l'ordre les indices des nouveaux points détectés %
            //for (int y = 0; y < vect_Tx0TxCTx1.GetLength(0); y++)
            // System.////Console.WriteLine(vect_Tx0TxCTx1[y,10]);
            
            //nbr_intervalles_Sensibilite_plus = Sensibilite_plus.Length/3; ////////

            Rap_lim1 = RAP_lim[0];     //seuil de detection grossiere des points de double inflexion en montée
            Rap_lim2 = RAP_lim[1];      // seuil de detection grossiere des points de double inflexion en descente

            // taille Suite_Extremums_Ordonnee
            taille_Suite_Extremums_Ordonnee = 0;
            j = 0;
            nbr_int_init = TxC.GetLength(0); // length(TxC(:, 1));
            nbr_int_fin = nbr_int_init;
            l = YY.Length;
            l_DPV = DPV.Length;
            delta_i = 1; //% 2;
            delta_i2 = 2; // 4;  1;

            while (j < nbr_int_fin)
            {
                taille_Suite_Extremums_Ordonnee += 1;
                maximum_inscrit = 0;

                for (int ih = (int)(Tx0[j, 0] - Tx0[0, 0]); ih < Tx1[j, 0] - Tx0[0, 0]; ih++)   // ???????????????????????
                {

                    if ((ih >= TxC[j, 0] - Tx0[0, 0]) && (maximum_inscrit == 0))
                    {
                        taille_Suite_Extremums_Ordonnee += 1;
                        //Suite_Extremums_Ordonnee = [Suite_Extremums_Ordonnee; TxC(j,:)];
                        maximum_inscrit = 1;

                    }

                    if ((ih - delta_i > 0) && (ih + delta_i2 < l_DPV))
                    {

                        if (DPV[ih] >= 0)
                        {
                            if ((DPV[ih] > DPV[ih - delta_i]) && (DPV[ih] > DPV[ih + delta_i]))
                            {
                                dernier_DPV_max_local = DPV[ih];

                            }
                            if ((DPV[ih] < DPV[ih - delta_i]) && (DPV[ih] < DPV[ih + delta_i]))
                            {
                                marque_ihp = 0;
                                premier_suivant_DPV_max_local = dernier_DPV_max_local;
                                ihp = ih + 1;
                                while ((marque_ihp == 0) && (ihp < TxC[j, 0] - Tx0[0, 0]))
                                {
                                    if ((DPV[ihp] > DPV[ihp - delta_i]) && (DPV[ihp] > DPV[ihp + delta_i]))
                                    {
                                        premier_suivant_DPV_max_local = DPV[ihp];
                                        marque_ihp = 1;
                                    }
                                    ihp += 1;
                                }
                                //if (((DPV[ih] / dernier_DPV_max_local) < Rap_lim1) && ((ih + Tx0[0, 0] - 1 > Tx0[j, 0]) && (ih + Tx0[0, 0] - 1 < Tx1[j, 0]) && (ih + Tx0[0, 0] - 1 != TxC[j, 0])))
                                if (((DPV[ih] / dernier_DPV_max_local) < Rap_lim1) && (((ih + Tx0[0, 0] - 1) > Tx0[j, 0]) && ((ih + Tx0[0, 0] - 1) < Tx1[j, 0]) && ((ih + Tx0[0, 0] - 1) != TxC[j, 0])))
                                {
                                    taille_Suite_Extremums_Ordonnee += 1;

                                }

                            }


                        }

                        if (DPV[ih] <= 0)
                        {
                            if ((DPV[ih] <= DPV[ih - delta_i]) && (DPV[ih] <= DPV[ih + delta_i]))
                            {
                                dernier_DPV_min_local = DPV[ih];
                            }
                            if ((DPV[ih] >= DPV[ih - delta_i]) && (DPV[ih] >= DPV[ih + delta_i]))
                            {
                                if (((DPV[ih] / dernier_DPV_min_local) < Rap_lim2) && ((ih + Tx0[0, 0] - 1 > Tx0[j, 0]) && (ih + Tx0[0, 0] - 1 < Tx1[j, 0]) && (ih + Tx0[0, 0] - 1 != TxC[j, 0])))
                                {
                                    taille_Suite_Extremums_Ordonnee += 1;

                                }
                            }
                        }
                    }
                }
                j += 1;

            }

            //  Fin taille Suite_Extremums_ordonnee

            // mettre dans l'ordre les indices des nouveaux points détectés %
            //for (int y = 0; y < vect_Tx0TxCTx1.GetLength(0); y++)
            // System.////Console.WriteLine(vect_Tx0TxCTx1[y,10]);
            Suite_Extremums_Ordonnee = new double[taille_Suite_Extremums_Ordonnee + 1, Tx0.GetLength(1)];
            oui = 1;
            non = 0;
            //nbr_intervalles_Sensibilite_plus = Sensibilite_plus.Length/3; ////////

            j = 0;
            nbr_int_init = TxC.GetLength(0); // length(TxC(:, 1));
            nbr_int_fin = nbr_int_init;
            l = YY.Length;
            l_DPV = DPV.Length;

            delta_i = 1; //% 2;
            delta_i2 = 2; // 4;  1;
            b = 0;
            while (j < nbr_int_fin)
            {
                //panier_X5 = [];
                Suite_Extremums_Ordonnee[b, 0] = Tx0[j, 0];  // Suite_Extremums_Ordonnee = [Suite_Extremums_Ordonnee; Tx0(j,:)];
                Suite_Extremums_Ordonnee[b, 1] = Tx0[j, 1];
                Suite_Extremums_Ordonnee[b, 2] = Tx0[j, 2];
                Suite_Extremums_Ordonnee[b, 3] = Tx0[j, 3];
                b += 1;
                maximum_inscrit = 0;

                for (int ih = (int)(Tx0[j, 0] - Tx0[0, 0]); ih < Tx1[j, 0] - Tx0[0, 0]; ih++)
                {
                    if ((ih >= TxC[j, 0] - Tx0[0, 0]) && (maximum_inscrit == 0))
                    {
                        Suite_Extremums_Ordonnee[b, 0] = TxC[j, 0];  // Suite_Extremums_Ordonnee = [Suite_Extremums_Ordonnee; Tx0(j,:)];
                        Suite_Extremums_Ordonnee[b, 1] = TxC[j, 1];
                        Suite_Extremums_Ordonnee[b, 2] = TxC[j, 2];
                        Suite_Extremums_Ordonnee[b, 3] = TxC[j, 3];
                        //Suite_Extremums_Ordonnee = [Suite_Extremums_Ordonnee; TxC(j,:)];
                        maximum_inscrit = 1;
                        b += 1;
                    }

                    if ((ih - delta_i > 0) && (ih + delta_i2 < l_DPV))
                    {
                        if (DPV[ih] >= 0)
                        {
                            if ((DPV[ih] > DPV[ih - delta_i]) && (DPV[ih] > DPV[ih + delta_i]))
                            {
                                dernier_DPV_max_local = DPV[ih];

                            }
                            if ((DPV[ih] < DPV[ih - delta_i]) && (DPV[ih] < DPV[ih + delta_i]))
                            {
                                marque_ihp = 0;
                                premier_suivant_DPV_max_local = dernier_DPV_max_local;
                                ihp = ih + 1;
                                while ((marque_ihp == 0) && (ihp < TxC[j, 0] - Tx0[0, 0]))
                                {
                                    if ((DPV[ihp] > DPV[ihp - delta_i]) && (DPV[ihp] > DPV[ihp + delta_i]))
                                    {
                                        premier_suivant_DPV_max_local = DPV[ihp];
                                        marque_ihp = 1;
                                    }
                                    ihp += 1;
                                }
                                if (((DPV[ih] / dernier_DPV_max_local) < Rap_lim1) && ((ih + Tx0[0, 0] - 1 > Tx0[j, 0]) && (ih + Tx0[0, 0] - 1 < Tx1[j, 0]) && (ih + Tx0[0, 0] - 1 != TxC[j, 0])))
                                {
                                    Suite_Extremums_Ordonnee[b, 0] = ih + Tx0[0, 0] ;  // // Suite_Extremums_Ordonnee = [Suite_Extremums_Ordonnee; ih+Tx0(1,1)  XX(ih) YY(ih) 5];
                                    Suite_Extremums_Ordonnee[b, 1] = XX[ih];
                                    Suite_Extremums_Ordonnee[b, 2] = YY[ih];
                                    Suite_Extremums_Ordonnee[b, 3] = 5;
                                    b += 1;
                                }

                            }
                        }

                        if (DPV[ih] <= 0)
                        {
                            if ((DPV[ih] <= DPV[ih - delta_i]) && (DPV[ih] <= DPV[ih + delta_i]))
                            {
                                dernier_DPV_min_local = DPV[ih];
                            }
                            if ((DPV[ih] >= DPV[ih - delta_i]) && (DPV[ih] >= DPV[ih + delta_i]))
                            {
                                if (((DPV[ih] / dernier_DPV_min_local) < Rap_lim2) && ((ih + Tx0[0, 0] - 1 > Tx0[j, 0]) && (ih + Tx0[0, 0] - 1 < Tx1[j, 0]) & (ih + Tx0[0, 0] - 1 != TxC[j, 0])))
                                {
                                    Suite_Extremums_Ordonnee[b, 0] = ih + Tx0[0, 0] ;  //// Suite_Extremums_Ordonnee = [Suite_Extremums_Ordonnee; ih+Tx0(1,1)  XX(ih) YY(ih) 5];
                                    Suite_Extremums_Ordonnee[b, 1] = XX[ih];
                                    Suite_Extremums_Ordonnee[b, 2] = YY[ih];
                                    Suite_Extremums_Ordonnee[b, 3] = 5;

                                    b += 1;
                                }
                            }
                        }
                    }
                }

                j += 1;
            }


            //Suite_Extremums_Ordonnee = [Suite_Extremums_Ordonnee; Tx1(size(Tx1, 1),:)]
            b = Math.Min(b, (Suite_Extremums_Ordonnee.GetLength(0) - 1));
            Suite_Extremums_Ordonnee[b, 0] = Tx1[Tx1.GetLength(0) - 1, 0];
            Suite_Extremums_Ordonnee[b, 1] = Tx1[Tx1.GetLength(0) - 1, 1];
            Suite_Extremums_Ordonnee[b, 2] = Tx1[Tx1.GetLength(0) - 1, 2];
            Suite_Extremums_Ordonnee[b, 3] = Tx1[Tx1.GetLength(0) - 1, 3];

            ///////////// ellimination des extremums confondus
            L_SEO = Suite_Extremums_Ordonnee.GetLength(0);
            indice_extremum_a_ellimier = new int[L_SEO];
            Suite_Extremums_Ordonnee_reduite = new double[L_SEO, Suite_Extremums_Ordonnee.GetLength(1)];
            Suite_Extremums_Ordonnee_2 = new double[L_SEO, Suite_Extremums_Ordonnee.GetLength(1)];
            Suite_Extremums_Ordonnee_reduite[0, 0] = Suite_Extremums_Ordonnee[0, 0];
            Suite_Extremums_Ordonnee_reduite[0, 1] = Suite_Extremums_Ordonnee[0, 1];
            Suite_Extremums_Ordonnee_reduite[0, 2] = Suite_Extremums_Ordonnee[0, 2];
            Suite_Extremums_Ordonnee_reduite[0, 3] = Suite_Extremums_Ordonnee[0, 3];

            for (int j = 1; j < L_SEO; j++)
            {
                indice_t_extremum_j_moins_1 = Suite_Extremums_Ordonnee[j - 1, 0];
                indice_t_extremum_j = Suite_Extremums_Ordonnee[j, 0];
                t_extremum_j_moins_1 = Suite_Extremums_Ordonnee[j - 1, 1];
                t_extremum_j = Suite_Extremums_Ordonnee[j, 1];
                if ((t_extremum_j == t_extremum_j_moins_1) || (indice_t_extremum_j == indice_t_extremum_j_moins_1))
                    indice_extremum_a_ellimier[j] = j;
                else
                {
                    Suite_Extremums_Ordonnee_reduite[j, 0] = Suite_Extremums_Ordonnee[j, 0];
                    Suite_Extremums_Ordonnee_reduite[j, 1] = Suite_Extremums_Ordonnee[j, 1];
                    Suite_Extremums_Ordonnee_reduite[j, 2] = Suite_Extremums_Ordonnee[j, 2];
                    Suite_Extremums_Ordonnee_reduite[j, 3] = Suite_Extremums_Ordonnee[j, 3];
                    //Suite_Extremums_Ordonnee_reduite = [Suite_Extremums_Ordonnee_reduite; Suite_Extremums_Ordonnee(j,:)]
                }
            }
            //Suite_Extremums_Ordonnee = Suite_Extremums_Ordonnee_reduite;
            Suite_Extremums_Ordonnee_2 = Suite_Extremums_Ordonnee_reduite;

            // Définition des instants t0, Tc, et t1 pour chaque intervalle Bêta
            L_SEO = Suite_Extremums_Ordonnee_2.GetLength(0);
            debut_intervalle = new double[4];
            maximum_intervalle = new double[4];
            fin_intervalle = new double[4];
            Suite_Intervalles_Beta = new double[4][,];
            Suite_Intervalles_Beta[0] = new double[L_SEO - 2, 3];
            Suite_Intervalles_Beta[1] = new double[L_SEO - 2, 3];
            Suite_Intervalles_Beta[2] = new double[L_SEO - 2, 3];
            Suite_Intervalles_Beta[3] = new double[L_SEO - 2, 3];
            indice_vect = 0;
            for (int j = 1; j <= L_SEO - 2; j++)
            {

                debut_intervalle[0] = Suite_Extremums_Ordonnee_2[j - 1, 0];
                debut_intervalle[1] = Suite_Extremums_Ordonnee_2[j - 1, 1];
                debut_intervalle[2] = Suite_Extremums_Ordonnee_2[j - 1, 2];
                debut_intervalle[3] = Suite_Extremums_Ordonnee_2[j - 1, 3];

                maximum_intervalle[0] = Suite_Extremums_Ordonnee_2[j, 0];
                maximum_intervalle[1] = Suite_Extremums_Ordonnee_2[j, 1];
                maximum_intervalle[2] = Suite_Extremums_Ordonnee_2[j, 2];
                maximum_intervalle[3] = Suite_Extremums_Ordonnee_2[j, 3];

                fin_intervalle[0] = Suite_Extremums_Ordonnee_2[j + 1, 0];
                fin_intervalle[1] = Suite_Extremums_Ordonnee_2[j + 1, 1];
                fin_intervalle[2] = Suite_Extremums_Ordonnee_2[j + 1, 2];
                fin_intervalle[3] = Suite_Extremums_Ordonnee_2[j + 1, 3];
                
                //Suite_Intervalles_Beta[j] = new double[L_SEO-2,3];
                Suite_Intervalles_Beta[0][indice_vect, 0] = debut_intervalle[0];
                Suite_Intervalles_Beta[0][indice_vect, 1] = maximum_intervalle[0];
                Suite_Intervalles_Beta[0][indice_vect, 2] = fin_intervalle[0];

                Suite_Intervalles_Beta[1][indice_vect, 0] = debut_intervalle[1];
                Suite_Intervalles_Beta[1][indice_vect, 1] = maximum_intervalle[1];
                Suite_Intervalles_Beta[1][indice_vect, 2] = fin_intervalle[1];

                Suite_Intervalles_Beta[2][indice_vect, 0] = debut_intervalle[2];
                Suite_Intervalles_Beta[2][indice_vect, 1] = maximum_intervalle[2];
                Suite_Intervalles_Beta[2][indice_vect, 2] = fin_intervalle[2];

                Suite_Intervalles_Beta[3][indice_vect, 0] = debut_intervalle[3];
                Suite_Intervalles_Beta[3][indice_vect, 1] = maximum_intervalle[3];
                Suite_Intervalles_Beta[3][indice_vect, 2] = fin_intervalle[3];

                indice_vect += 1;

            }

            // Définir les paramettres de l'impulsion beta declanchee %%%%%%%%%%%%%%%%%%%

            nombre_total_impulsions_beta = Suite_Intervalles_Beta[0].GetLength(0); /////////////////////////  Suite_Intervalles_Beta.GetLength(1)
                                                                                   //Suite_Intervalles_Beta = Suite_Intervalles_Beta;

            K_hauteur_preced = 50;     // facultatif
            q_preced = 20;
            p_preced = 20;
            t0_preced = 1;
            tc_preced = 10;
            t1_preced = 20;
            Valeur_de_beta_preced_a_t_ajustement1 = 0;
            Tc_beta_act = Suite_Intervalles_Beta[1][0, 0];  //(1, 1, 2);
            T1_beta_act = Suite_Intervalles_Beta[1][0, 1]; // (1,2,2); 

            profil_vitesse = new definition_de_l_instant_t_ajustement_sur_profil_vitesse();
            t_ajustement = profil_vitesse.Method_retour_t_ajustement(Tc_beta_act, T1_beta_act);
            Vcurv_t_ajustement1 = profil_vitesse.Method_Vcurv_t_ajustement1(Tc_beta_act, T1_beta_act, XX, YY, pas);
            Derivee_Vsig_t_ajustement = profil_vitesse.Method_Derivee_Vsig_t_ajustement(Tc_beta_act, T1_beta_act, XX, YY, pas);
            // [t_ajustement , Vcurv_t_ajustement1 , Derivee_Vsig_t_ajustement] = definition_de_l_instant_t_ajustement_sur_profil_vitesse(Tc_beta_act , T1_beta_act , XX , YY , pas);
            
            fonctions_beta = new double[nombre_total_impulsions_beta, 7];
            nombre_total_impulsions_beta = Suite_Intervalles_Beta[0].GetLength(0); //// 

            memoire_points_passage_beta_methode2 = new double[nombre_total_impulsions_beta + 1, 4];
            for (int j = 0; j < nombre_total_impulsions_beta; j++)
            {
                t0 = Suite_Intervalles_Beta[1][j, 0];
                tc = Suite_Intervalles_Beta[1][j, 1];
                t1 = Suite_Intervalles_Beta[1][j, 2];
                K_hauteur = Suite_Intervalles_Beta[2][j, 1];
                
                Valeur_de_beta_actuel_a_t_ajustement1 = (Vcurv_t_ajustement1) - (Valeur_de_beta_preced_a_t_ajustement1);
                if (Valeur_de_beta_actuel_a_t_ajustement1 > 0)
                    Valeur_de_beta_actuel_a_t_ajustement1 = Valeur_de_beta_actuel_a_t_ajustement1;
                else
                    Valeur_de_beta_actuel_a_t_ajustement1 = K_hauteur / 1000;

                if (Valeur_de_beta_actuel_a_t_ajustement1 > K_hauteur)
                {
                    p = 2.5;
                    p_v1 = p;
                    q = (p * (((t1 - tc) / (tc - t0))));
                    q_v1 = q;
                }
                else
                {
                    p = Math.Abs((Math.Log(Valeur_de_beta_actuel_a_t_ajustement1 / K_hauteur)) / ((Math.Log((t_ajustement - t0) / (tc - t0))) + (((t1 - tc) / (tc - t0)) * Math.Log((t1 - t_ajustement) / (t1 - tc)))));
                    p_v1 = p;
                    //////Console.WriteLine(p);
                    q = (p * (((t1 - tc) / (tc - t0))));
                    q_v1 = q;
                    
                }

                //mémorisation de l'instant t_ajustement actuel
                t_ajustement_actuel = t_ajustement;
                Vcurv_t_ajustement1_actuel = Vcurv_t_ajustement1;

                Tc_beta_act = tc;
                T1_beta_act = t1;

                t_ajustement = profil_vitesse.Method_retour_t_ajustement(Tc_beta_act, T1_beta_act);
                Vcurv_t_ajustement1 = profil_vitesse.Method_Vcurv_t_ajustement1(Tc_beta_act, T1_beta_act, XX, YY, pas);
                Derivee_Vsig_t_ajustement = profil_vitesse.Method_Derivee_Vsig_t_ajustement(Tc_beta_act, T1_beta_act, XX, YY, pas);
                //      [t_ajustement , Vcurv_t_ajustement1, Derivee_Vsig_t_ajustement] = definition_de_l_instant_t_ajustement_sur_profil_vitesse(Tc_beta_act , T1_beta_act , XX , YY , pas);

                if (j < nombre_total_impulsions_beta - 1)
                {
                    t0_suiv = Suite_Intervalles_Beta[1][j + 1, 0];
                    tc_suiv = Suite_Intervalles_Beta[1][j + 1, 1];
                    t1_suiv = Suite_Intervalles_Beta[1][j + 1, 2];
                    K_hauteur_suiv = Suite_Intervalles_Beta[2][j + 1, 1];

                    if ((tc_suiv - t0_suiv) > (t1_suiv - tc_suiv))
                    {
                        q_suiv = 2;
                        p_suiv = (q_suiv * (((tc_suiv - t0_suiv) / (t1_suiv - tc_suiv))));
                        if (p_suiv > 4)
                        {
                            p_suiv = 4;
                            q_suiv = (p_suiv * (((t1_suiv - tc_suiv) / (tc_suiv - t0_suiv))));
                        }

                    }
                    else
                    {
                        p_suiv = 2;
                        q_suiv = (p_suiv * (((t1_suiv - tc_suiv) / (tc_suiv - t0_suiv))));
                        if (q_suiv > 4)
                        {
                            q_suiv = 4;
                            p_suiv = (q_suiv * (((tc_suiv - t0_suiv) / (t1_suiv - tc_suiv))));
                        }
                    }
                    //Calcul de la valeur estimee beta_suivante_t_ajustement1
                    beta_suivante_estimee_t_ajustement1 = K_hauteur_suiv * ((Math.Pow(((t_ajustement - t0_suiv) / (tc_suiv - t0_suiv)), p_suiv) * Math.Pow(((t1_suiv - t_ajustement) / (t1_suiv - tc_suiv)), q_suiv)));

                    //Calcul d'une autre estimation de beta_act_ajustement1
                    beta_actuelle_estimee_pour_nouveau_t_ajustement1 = Vcurv_t_ajustement1 - beta_suivante_estimee_t_ajustement1;
                    p_v2 = Math.Abs((Math.Log(beta_actuelle_estimee_pour_nouveau_t_ajustement1 / K_hauteur)) / ((Math.Log((t_ajustement - t0) / (tc - t0))) + (((t1 - tc) / (tc - t0)) * Math.Log((t1 - t_ajustement) / (t1 - tc)))));
                    
                    q_v2 = (p_v2 * (((t1 - tc) / (tc - t0))));
                    p = ((1 * p_v1) + (1 * p_v2)) / 2;
                    q = ((1 * q_v1) + (1 * q_v2)) / 2;
                }
                else
                {
                    p = p;
                    q = (p * (((t1 - tc) / (tc - t0))));
                }
                // memorisation des valeurs des impulsions Beta et de la vitesse  a l'instant t_ajustement@ contrôle des resultats obtenus pour p et q
                Valeur_de_beta_preced_a_t_ajustement1 = K_hauteur_preced * ((Math.Pow(((t_ajustement_actuel - t0_preced) / (tc_preced - t0_preced)), p_preced) * Math.Pow(((t1_preced - t_ajustement_actuel) / (t1_preced - tc_preced)), q_preced)));
                Valeur_de_beta_actuel_a_t_ajustement1 = (Vcurv_t_ajustement1_actuel) - (Valeur_de_beta_preced_a_t_ajustement1);

                // memoire_points_passage_beta_methode2 = [memoire_points_passage_beta_methode2; t_ajustement_actuel, Vcurv_t_ajustement1_actuel, Valeur_de_beta_preced_a_t_ajustement1, Valeur_de_beta_actuel_a_t_ajustement1];
                memoire_points_passage_beta_methode2[j, 0] = t_ajustement_actuel;
                memoire_points_passage_beta_methode2[j, 1] = Vcurv_t_ajustement1_actuel;
                memoire_points_passage_beta_methode2[j, 2] = Valeur_de_beta_preced_a_t_ajustement1;
                memoire_points_passage_beta_methode2[j, 3] = Valeur_de_beta_actuel_a_t_ajustement1;
                if (p > 7)
                    p = 7;
                else if  ((p < 1) || (Double.IsNaN(p)))
                        p = 1;

                q = (p * (((t1 - tc) / (tc - t0))));
                if (q > 8)
                {
                    q = 8;
                    p = (q * (((tc - t0) / (t1 - tc))));
                }
                else if (q < 0.8)
                {
                    q = 0.8;
                    p = (q * (((tc - t0) / (t1 - tc))));
                }
                Valeur_de_beta_preced_a_t_ajustement1 = K_hauteur * (Math.Pow(((t_ajustement - t0) / (tc - t0)), p) * Math.Pow(((t1 - t_ajustement) / (t1 - tc)), q));
                K_hauteur_preced = K_hauteur;
                
                q_preced = q;
                p_preced = p;
                t0_preced = t0;
                tc_preced = tc;
                t1_preced = t1;
                // enregistrement des fonctions beta et de leurs parametres
                rong_extremum = j + 2;
                rong_extremum_preced = rong_extremum;
                // fonctions_beta = [fonctions_beta; t0, tc, t1, p, q, K_hauteur, rong_extremum];
                fonctions_beta[j, 0] = t0;
                fonctions_beta[j, 1] = tc;
                fonctions_beta[j, 2] = t1;
                fonctions_beta[j, 3] = p;
                fonctions_beta[j, 4] = q;
                fonctions_beta[j, 5] = K_hauteur;
                fonctions_beta[j, 6] = rong_extremum;
                

            }

            Valeur_de_beta_actuel_a_t_ajustement1 = 0;
            //  memoire_points_passage_beta_methode2 = [memoire_points_passage_beta_methode2; t_ajustement, Vcurv_t_ajustement1, Valeur_de_beta_preced_a_t_ajustement1, Valeur_de_beta_actuel_a_t_ajustement1];
            memoire_points_passage_beta_methode2[memoire_points_passage_beta_methode2.GetLength(0) - 1, 0] = t_ajustement;
            memoire_points_passage_beta_methode2[memoire_points_passage_beta_methode2.GetLength(0) - 1, 1] = Vcurv_t_ajustement1;
            memoire_points_passage_beta_methode2[memoire_points_passage_beta_methode2.GetLength(0) - 1, 2] = Valeur_de_beta_preced_a_t_ajustement1;
            memoire_points_passage_beta_methode2[memoire_points_passage_beta_methode2.GetLength(0) - 1, 3] = Valeur_de_beta_actuel_a_t_ajustement1;
            // repartition des fonctions beta chevauchées sur les intervalles de temps 

            nbr_fb = fonctions_beta.GetLength(0);

            temps1 = fonctions_beta[0, 0];
            temps2 = fonctions_beta[0, 1];
            rong_fonction1 = 0;
            rong_fonction2 = 1;

            K_amplitude1 = 0;
            K_amplitude2 = fonctions_beta[0, 5];
            indice_data_1 = Suite_Extremums_Ordonnee_2[0, 0];
            indice_data_2 = Suite_Extremums_Ordonnee_2[1, 0];
            tableau_affectation_methode_2 = new double[nbr_fb + 1, 8];
            // tableau_affectation_methode_2 = [temps1, temps2, rong_fonction1, rong_fonction2, K_amplitude1, K_amplitude2, indice_data_1, indice_data_2];
            tableau_affectation_methode_2[0, 0] = temps1;
            tableau_affectation_methode_2[0, 1] = temps2;
            tableau_affectation_methode_2[0, 2] = rong_fonction1;
            tableau_affectation_methode_2[0, 3] = rong_fonction2;
            tableau_affectation_methode_2[0, 4] = K_amplitude1;
            tableau_affectation_methode_2[0, 5] = K_amplitude2;
            tableau_affectation_methode_2[0, 6] = indice_data_1;
            tableau_affectation_methode_2[0, 7] = indice_data_2;

            for (int j = 1; j < nbr_fb; j++)
            {
                temps1 = fonctions_beta[j, 0];
                temps2 = fonctions_beta[j, 1];
                rong_fonction1 = j;
                rong_fonction2 = j + 1;
                //rong_extremum1_liste = fonctions_beta[rong_fonction1, 6];
                //rong_extremum2_liste = fonctions_beta[rong_fonction2, 6];
                indice_data_1 = Suite_Extremums_Ordonnee_2[j, 0];
                indice_data_2 = Suite_Extremums_Ordonnee_2[j + 1, 0];
                K_amplitude1 = K_amplitude2;
                K_amplitude2 = fonctions_beta[j, 5];
                //    tableau_affectation_methode_2 = [tableau_affectation_methode_2; temps1, temps2, rong_fonction1, rong_fonction2, K_amplitude1, K_amplitude2, indice_data_1, indice_data_2];
                tableau_affectation_methode_2[j, 0] = temps1; //Math.Round(temps1,1);
                tableau_affectation_methode_2[j, 1] = temps2;
                tableau_affectation_methode_2[j, 2] = rong_fonction1;
                tableau_affectation_methode_2[j, 3] = rong_fonction2;
                tableau_affectation_methode_2[j, 4] = K_amplitude1;
                tableau_affectation_methode_2[j, 5] = K_amplitude2;
                tableau_affectation_methode_2[j, 6] = indice_data_1;
                tableau_affectation_methode_2[j, 7] = indice_data_2;

            }

            temps1 = fonctions_beta[nbr_fb - 1, 1];
            temps2 = fonctions_beta[nbr_fb - 1, 2];
            rong_fonction1 = nbr_fb;
            rong_fonction2 = 0;
            K_amplitude1 = K_amplitude2;
            K_amplitude2 = 0;
            indice_data_1 = Suite_Extremums_Ordonnee_2[nbr_fb, 0];
            indice_data_2 = Suite_Extremums_Ordonnee_2[nbr_fb + 1, 0];

            // tableau_affectation_methode_2 = [tableau_affectation_methode_2; temps1, temps2, rong_fonction1, rong_fonction2, K_amplitude1, K_amplitude2, indice_data_1, indice_data_2];
            tableau_affectation_methode_2[nbr_fb, 0] = temps1;
            tableau_affectation_methode_2[nbr_fb, 1] = temps2;
            tableau_affectation_methode_2[nbr_fb, 2] = rong_fonction1;
            tableau_affectation_methode_2[nbr_fb, 3] = rong_fonction2;
            tableau_affectation_methode_2[nbr_fb, 4] = K_amplitude1;
            tableau_affectation_methode_2[nbr_fb, 5] = K_amplitude2;
            tableau_affectation_methode_2[nbr_fb, 6] = indice_data_1;
            tableau_affectation_methode_2[nbr_fb, 7] = indice_data_2;

            fonctions_beta_sortie = new double[fonctions_beta.GetLength(0), fonctions_beta.GetLength(1)];
            fonctions_beta_sortie = fonctions_beta;
            
            tableau_affectation_intervalles_sortie = tableau_affectation_methode_2;
            ////////////////////////////////////////////////////////////////////////////////////

           
            if (ajout_impulsion_finale_nulle == "oui")
            {
                fonctions_beta_sortie = new double[fonctions_beta.GetLength(0) + 1, fonctions_beta.GetLength(1)];
                //fonctions_beta_sortie = fonctions_beta;
                for (int i = 0; i < fonctions_beta.GetLength(0); i++)
                    for (int j = 0; j < fonctions_beta.GetLength(1); j++)
                        fonctions_beta_sortie[i, j] = fonctions_beta[i, j];

                nbr_fct_beta_exacte = fonctions_beta.GetLength(0) - 1;

                t0_derniere_fct_beta = fonctions_beta[nbr_fct_beta_exacte, 0];
                tc_derniere_fct_beta = fonctions_beta[nbr_fct_beta_exacte, 1];
                t1_derniere_fct_beta = fonctions_beta[nbr_fct_beta_exacte, 2];
                p_derniere_fct_beta = fonctions_beta[nbr_fct_beta_exacte, 3];
                q_derniere_fct_beta = fonctions_beta[nbr_fct_beta_exacte, 4];
                K_derniere_fct_beta = fonctions_beta[nbr_fct_beta_exacte, 5];
                rong_extremum_derniere_fct_beta = fonctions_beta[nbr_fct_beta_exacte, 6];

                t0_fct_beta_immobile = tc_derniere_fct_beta;
                tc_fct_beta_immobile = t1_derniere_fct_beta;
                t1_fct_beta_immobile = tc_fct_beta_immobile + ((tc_fct_beta_immobile - t0_fct_beta_immobile) / 3);
                p_fct_beta_immobile = 2;
                q_fct_beta_immobile = (p_fct_beta_immobile * (((t1_fct_beta_immobile - tc_fct_beta_immobile) / (tc_fct_beta_immobile - t0_fct_beta_immobile))));
                K_fct_beta_immobile = 0.000001;
                rong_extremum_fct_beta_immobile = rong_extremum_derniere_fct_beta + 1;
                // System.////Console.WriteLine(fonctions_beta.GetLength(1));
                //fct_beta_immobile = [t0_fct_beta_immobile, tc_fct_beta_immobile, t1_fct_beta_immobile, p_fct_beta_immobile, q_fct_beta_immobile, K_fct_beta_immobile, rong_extremum_fct_beta_immobile];
                fonctions_beta_sortie[fonctions_beta.GetLength(0), 0] = t0_fct_beta_immobile;
                fonctions_beta_sortie[fonctions_beta.GetLength(0), 1] = tc_fct_beta_immobile;
                fonctions_beta_sortie[fonctions_beta.GetLength(0), 2] = t1_fct_beta_immobile;
                fonctions_beta_sortie[fonctions_beta.GetLength(0), 3] = p_fct_beta_immobile;
                fonctions_beta_sortie[fonctions_beta.GetLength(0), 4] = q_fct_beta_immobile;
                fonctions_beta_sortie[fonctions_beta.GetLength(0), 5] = K_fct_beta_immobile;
                fonctions_beta_sortie[fonctions_beta.GetLength(0), 6] = rong_extremum_fct_beta_immobile;

            }
           
            return fonctions_beta_sortie;

        }








        public int Method_size_vecteur (double[] V, double[] DPV, double[,] points, double[] T, double[] t, double[] RAP_lim, double pas)
        {
            temp_depart = T[0];
            for (int i = 0; i < T.Length; i++)
                T[i] -= temp_depart;
            XX = new double[T.Length];
            YY = new double[T.Length];
            maximum = new double[2];
            //vymin 
            XX = T;
            YY = V;
            //ZZ = DPV;
            //-------------------------------------longueur du vecteur vitesse
            l = YY.Length;
            // -----------------------------------initialisation des paramètres
            //vymin[0] = YY[0];
            //vxmin[0] = XX[0];
            //ind_vmin[0] =  (XX[0] / pas + 1);
            //v_min[0,0]= max(Math.Round(XX[0] / pas), 1);
            //v_min[0,1]= XX[0];
            //v_min[0, 2] = YY[0];

            //v_min = [v_min; (max(round(XX(1) / pas), 1)) XX(1) YY(1)] ;

            taille = 0;
            // calculer la taille de la matrice extrema
            i = 2;
            while (i < l) 
            {
                // -----------------------------------detection des maxima
                if (YY[i - 1] >= YY[i - 2])   // ???
                {

                    if (YY[i - 1] >= YY[i])
                    {
                        taille += 1;
                    }
                }
                // ----------------------------------detection des minima
                if (YY[i - 1] <= YY[i - 2])
                {

                    if (YY[i - 1] <= YY[i])   
                    {
                        taille += 1;
                    }

                }

                i += 1;
            }
            
            extrema = new double[taille + 2, 4];
            maximum[0] = Math.Round(XX[0] / pas);
            maximum[1] = 1;
            extrema[0, 0] = max_matrice(maximum);
            extrema[0, 1] = XX[0];
            extrema[0, 2] = YY[0];
            extrema[0, 3] = 0;
            indice = 1;
            i = 2; // à verifier 
            while (i < l)
            {
                // -----------------------------------detection des maxima
                if (YY[i - 1] >= YY[i - 2])   // ???
                {

                    if (YY[i - 1] >= YY[i])
                    {

                        extrema[indice, 0] = Math.Round(XX[i - 1] / pas) + 1;    // extrema = [extrema;  XX(i - 1) YY(i - 1) 2];
                        extrema[indice, 1] = XX[i - 1];
                        extrema[indice, 2] = YY[i - 1];
                        extrema[indice, 3] = 2;
                        indice += 1;
                    }
                }
                // ----------------------------------detection des minima
                if (YY[i - 1] <= YY[i - 2])
                {

                    if (YY[i - 1] <= YY[i])  
                    {


                        extrema[indice, 0] = Math.Round(XX[i - 1] / pas) + 1;
                        extrema[indice, 1] = XX[i - 1];
                        extrema[indice, 2] = YY[i - 1];
                        extrema[indice, 3] = 0;
                        indice += 1;
                    }

                }

                i += 1;
            }
            // --------------------------------------le dernier points


            extrema[indice, 0] = Math.Round(XX[i - 1] / pas) + 1;// extrema = [extrema; (round(XX(i - 1) / pas) + 1) XX(l) YY(l) 0];
            extrema[indice, 1] = XX[l-1];
            extrema[indice, 2] = YY[l-1];
            extrema[indice, 3] = 0;

            // --------------------------------------------------------------------------
            p = 2;
            i = 1;
            // -------------------------------------Elimination des points voisins(bruit)

            /*for (int i = 0; i<extrema.Length/extrema.Rank;i++)
            {
                extremum[,0] = extrema[i,0];
                extremum[, 1] = extrema[i, 1];
                extremum[, 2] = extrema[i, 2];
                extremum[, 3] = extrema[i, 3];
                //hh = extremum;
            }*/
            extremum = new double[extrema.GetLength(0), extrema.GetLength(1)];
            extremum = extrema;                                                  

            // calcul taille Tx0
            ind = 0;
            j = 0;    //  1
            while (j <= extremum.GetLength(0) - 3) //verifier condition
            {
                if (extremum[j, 3] == 0)
                {
                    uv = 1;
                    while ((extremum[j + uv, 3] != 2) && (j + uv <= extremum.GetLength(0) - 2)) // verfifier codition
                        uv += 1;
                    if ((extremum[j + uv, 3] == 2) && (j + uv <= extremum.GetLength(0) -2))
                    {
                        uvp = 1;
                        while ((extremum[j + uv + uvp, 3] != 0) && (j + uv + uvp <= extremum.GetLength(0) - 1))
                            uvp += 1;
                        if ((extremum[j + uv + uvp, 3] == 0) && (j + uv + uvp <= extremum.GetLength(0) - 1))
                        {
                            ind += 1;
                        }
                    }
                    j = j + uv + uvp - 1;
                }
                j += 1;

            }
            int val_ind = ind;
            ////Console.WriteLine("ind" + val_ind);
            Tx0 = new double[val_ind, extremum.GetLength(1)];
            TxC = new double[val_ind, extremum.GetLength(1)];
            Tx1 = new double[val_ind, extremum.GetLength(1)];
          
            j = 0;    //  1
            ind2 = 0;
            while (j <= extremum.GetLength(0) - 3) //verifier condition
            {
                if (extremum[j, 3] == 0)
                {
                    uv = 1;
                    while ((extremum[j + uv, 3] != 2) && (j + uv <= extremum.GetLength(0) - 2)) // verfifier codition
                        uv += 1;
                    if ((extremum[j + uv, 3] == 2) && (j + uv <= extremum.GetLength(0) - 2))
                    {
                        uvp = 1;
                        while ((extremum[j + uv + uvp, 3] != 0) && (j + uv + uvp <= extremum.GetLength(0) - 1))
                            uvp += 1;
                        if ((extremum[j + uv + uvp, 3] == 0) && (j + uv + uvp <= extremum.GetLength(0) - 1))
                        {

                            Tx0[ind2, 0] = extremum[j, 0];   //[Tx0; extremum(j,:)];
                            Tx0[ind2, 1] = extremum[j, 1];
                            Tx0[ind2, 2] = extremum[j, 2];
                            Tx0[ind2, 3] = extremum[j, 3];   


                            TxC[ind2, 0] = extremum[j + uv, 0]; // [TxC; extremum(j + uv,:)];
                            TxC[ind2, 1] = extremum[j + uv, 1];
                            TxC[ind2, 2] = extremum[j + uv, 2];
                            TxC[ind2, 3] = extremum[j + uv, 3];
                            
                            Tx1[ind2, 0] = extremum[j + uv + uvp, 0]; //[Tx1; extremum(j + uv + uvp,:)];
                            Tx1[ind2, 1] = extremum[j + uv + uvp, 1];
                            Tx1[ind2, 2] = extremum[j + uv + uvp, 2];
                            Tx1[ind2, 3] = extremum[j + uv + uvp, 3];

                            ind2 += 1;
                        }
                    }
                    j = j + uv + uvp - 1;
                }
                j += 1;

            }


            vect_Tx0TxCTx1 = new double[Tx0.GetLength(0), Tx0.GetLength(1) * 3];

            for (int cf = 0; cf < Tx0.GetLength(0); cf++) //vect_Tx0TxCTx1 = [Tx0, TxC, Tx1];
            {
                c = 0;
                for (int lf = 0; lf < 4; lf++)
                {
                    vect_Tx0TxCTx1[cf, c] = Tx0[cf, lf];
                    c++;
                }
                for (int lf = 0; lf < 4; lf++)
                {
                    vect_Tx0TxCTx1[cf, c] = TxC[cf, lf];
                    c++;
                }
                for (int lf = 0; lf < 4; lf++)
                {
                    vect_Tx0TxCTx1[cf, c] = Tx1[cf, lf];
                    c++;
                }
            }

            Rap_lim1 = RAP_lim[0];     //seuil de detection grossiere des points de double inflexion en montée
            Rap_lim2 = RAP_lim[1];      // seuil de detection grossiere des points de double inflexion en descente
            
            // taille Suite_Extremums_Ordonnee
            taille_Suite_Extremums_Ordonnee = 0;
            j = 0;
            nbr_int_init = TxC.GetLength(0); // length(TxC(:, 1));
            nbr_int_fin = nbr_int_init;
            l = YY.Length;
            l_DPV = DPV.Length;
            delta_i = 1; //% 2;
            delta_i2 = 2; // 4;  1;

            while (j < nbr_int_fin)
            {
                taille_Suite_Extremums_Ordonnee += 1;
                maximum_inscrit =0;
               
                for (int ih = (int)(Tx0[j, 0] - Tx0[0, 0] ) ; ih < Tx1[j, 0] - Tx0[0, 0]; ih++)   // ???????????????????????
                {
                   
                    if ((ih >= TxC[j, 0] - Tx0[0, 0] ) && (maximum_inscrit == 0))
                    {
                        taille_Suite_Extremums_Ordonnee += 1;
                        //Suite_Extremums_Ordonnee = [Suite_Extremums_Ordonnee; TxC(j,:)];
                        maximum_inscrit = 1;
                        
                    }

                    if ((ih - delta_i > 0) && (ih + delta_i2 < l_DPV))
                    {
                        
                        if (DPV[ih] >= 0)
                        {
                            if ((DPV[ih] > DPV[ih - delta_i]) && (DPV[ih] > DPV[ih + delta_i]))
                            {
                                dernier_DPV_max_local = DPV[ih];

                            }
                            if ((DPV[ih] < DPV[ih - delta_i]) && (DPV[ih] < DPV[ih + delta_i]))
                            {
                                marque_ihp = 0;
                                premier_suivant_DPV_max_local = dernier_DPV_max_local;
                                ihp = ih + 1;
                                while ((marque_ihp == 0) && (ihp < TxC[j, 0] - Tx0[0, 0]))
                                {
                                    if ((DPV[ihp] > DPV[ihp - delta_i]) && (DPV[ihp] > DPV[ihp + delta_i]))
                                    {
                                        premier_suivant_DPV_max_local = DPV[ihp];
                                        marque_ihp = 1;
                                    }
                                    ihp += 1;
                                }
                                //if (((DPV[ih] / dernier_DPV_max_local) < Rap_lim1) && ((ih + Tx0[0, 0] - 1 > Tx0[j, 0]) && (ih + Tx0[0, 0] - 1 < Tx1[j, 0]) && (ih + Tx0[0, 0] - 1 != TxC[j, 0])))
                                if (((DPV[ih] / dernier_DPV_max_local) < Rap_lim1) && (((ih + Tx0[0, 0] -1) > Tx0[j, 0]) && ((ih + Tx0[0, 0]  -1) < Tx1[j, 0]) && ((ih + Tx0[0, 0] -1) != TxC[j, 0])))
                                {
                                    taille_Suite_Extremums_Ordonnee += 1;
                                  
                                }

                            }


                        }

                        if (DPV[ih] <= 0)
                        {
                            if ((DPV[ih] <= DPV[ih - delta_i]) && (DPV[ih] <= DPV[ih + delta_i]))
                            {
                                dernier_DPV_min_local = DPV[ih];
                            }
                            if ((DPV[ih] >= DPV[ih - delta_i]) && (DPV[ih] >= DPV[ih + delta_i]))
                            {
                                if (((DPV[ih] / dernier_DPV_min_local) < Rap_lim2) && ((ih + Tx0[0, 0] -1 > Tx0[j, 0]) && (ih + Tx0[0, 0] -1 < Tx1[j, 0]) && (ih + Tx0[0, 0] -1 != TxC[j, 0])))
                                {
                                    taille_Suite_Extremums_Ordonnee += 1;
                                    

                                }
                            }
                        }
                    }
                }
                j += 1;

            }

            //  Fin taille Suite_Extremums_ordonnee

           // mettre dans l'ordre les indices des nouveaux points détectés %
            //for (int y = 0; y < vect_Tx0TxCTx1.GetLength(0); y++)
            // System.////Console.WriteLine(vect_Tx0TxCTx1[y,10]);
            Suite_Extremums_Ordonnee = new double[taille_Suite_Extremums_Ordonnee + 1 , Tx0.GetLength(1)];
            oui = 1;
            non = 0;
            //nbr_intervalles_Sensibilite_plus = Sensibilite_plus.Length/3; ////////

           

            j = 0;
            nbr_int_init = TxC.GetLength(0); // length(TxC(:, 1));
            nbr_int_fin = nbr_int_init;
            l = YY.Length;
            l_DPV = DPV.Length;
           
            delta_i = 1; //% 2;
            delta_i2 = 2; // 4;  1;
            b = 0;
            while (j < nbr_int_fin)
            {
                //panier_X5 = [];
                b = Math.Min(b, Suite_Extremums_Ordonnee.GetLength(0)-1);
                Suite_Extremums_Ordonnee[b, 0] = Tx0[j, 0];  // Suite_Extremums_Ordonnee = [Suite_Extremums_Ordonnee; Tx0(j,:)];
                Suite_Extremums_Ordonnee[b, 1] = Tx0[j, 1];
                Suite_Extremums_Ordonnee[b, 2] = Tx0[j, 2];
                Suite_Extremums_Ordonnee[b, 3] = Tx0[j, 3];
                b += 1;
                maximum_inscrit = 0;

                for (int ih = (int)(Tx0[j, 0] - Tx0[0, 0] ); ih < Tx1[j, 0] - Tx0[0, 0]; ih++)   
                {
                    if ((ih >= TxC[j, 0] - Tx0[0, 0] ) && (maximum_inscrit == 0))
                    {
                        Suite_Extremums_Ordonnee[b, 0] = TxC[j, 0];  // Suite_Extremums_Ordonnee = [Suite_Extremums_Ordonnee; Tx0(j,:)];
                        Suite_Extremums_Ordonnee[b, 1] = TxC[j, 1];
                        Suite_Extremums_Ordonnee[b, 2] = TxC[j, 2];
                        Suite_Extremums_Ordonnee[b, 3] = TxC[j, 3];
                        //Suite_Extremums_Ordonnee = [Suite_Extremums_Ordonnee; TxC(j,:)];
                        maximum_inscrit = 1;
                        b += 1;
                    }

                    if ((ih - delta_i > 0) && (ih + delta_i2 < l_DPV))
                    {
                        if (DPV[ih] >= 0)
                        {
                            if ((DPV[ih] > DPV[ih - delta_i]) && (DPV[ih] > DPV[ih + delta_i]))
                            {
                                dernier_DPV_max_local = DPV[ih];

                            }
                            if ((DPV[ih] < DPV[ih - delta_i]) && (DPV[ih] < DPV[ih + delta_i]))
                            {
                                marque_ihp = 0;
                                premier_suivant_DPV_max_local = dernier_DPV_max_local;
                                ihp = ih + 1;
                                while ((marque_ihp == 0) && (ihp < TxC[j, 0] - Tx0[0, 0]))
                                {
                                    if ((DPV[ihp] > DPV[ihp - delta_i]) && (DPV[ihp] > DPV[ihp + delta_i]))
                                    {
                                        premier_suivant_DPV_max_local = DPV[ihp];
                                        marque_ihp = 1;
                                    }
                                    ihp += 1;
                                }
                                if (((DPV[ih] / dernier_DPV_max_local) < Rap_lim1) && ((ih + Tx0[0, 0] - 1 > Tx0[j, 0]) && (ih + Tx0[0, 0] - 1 < Tx1[j, 0]) && (ih + Tx0[0, 0] - 1 != TxC[j, 0])))
                                {
                                    Suite_Extremums_Ordonnee[b, 0] = ih + Tx0[0, 0];  // // Suite_Extremums_Ordonnee = [Suite_Extremums_Ordonnee; ih+Tx0(1,1)  XX(ih) YY(ih) 5];
                                    Suite_Extremums_Ordonnee[b, 1] = XX[ih];
                                    Suite_Extremums_Ordonnee[b, 2] = YY[ih];
                                    Suite_Extremums_Ordonnee[b, 3] = 5;

                                    b += 1;

                                }

                            }


                        }

                        if (DPV[ih] <= 0)
                        {
                            if ((DPV[ih] <= DPV[ih - delta_i]) && (DPV[ih] <= DPV[ih + delta_i]))
                            {
                                dernier_DPV_min_local = DPV[ih];
                            }
                            if ((DPV[ih] >= DPV[ih - delta_i]) && (DPV[ih] >= DPV[ih + delta_i]))
                            {
                                if (((DPV[ih] / dernier_DPV_min_local) < Rap_lim2) && ((ih + Tx0[0, 0] - 1 > Tx0[j, 0]) && (ih + Tx0[0, 0] - 1 < Tx1[j, 0]) && (ih + Tx0[0, 0] - 1 != TxC[j, 0])))
                                {
                                    Suite_Extremums_Ordonnee[b, 0] = ih + Tx0[0, 0] ;  //// Suite_Extremums_Ordonnee = [Suite_Extremums_Ordonnee; ih+Tx0(1,1)  XX(ih) YY(ih) 5];
                                    Suite_Extremums_Ordonnee[b, 1] = XX[ih];
                                    Suite_Extremums_Ordonnee[b, 2] = YY[ih];
                                    Suite_Extremums_Ordonnee[b, 3] = 5;

                                    b += 1;
                                }
                            }
                        }
                    }
                }
                j += 1;

            }
            // ////Console.WriteLine(Suite_Extremums_Ordonnee.GetLength(0));
            //////Console.WriteLine(b);

            //Suite_Extremums_Ordonnee = [Suite_Extremums_Ordonnee; Tx1(size(Tx1, 1),:)]
            b = Math.Min(b, (Suite_Extremums_Ordonnee.GetLength(0) - 1)) ;
            Suite_Extremums_Ordonnee[b, 0] = Tx1[Tx1.GetLength(0) - 1, 0];
            Suite_Extremums_Ordonnee[b, 1] = Tx1[Tx1.GetLength(0) - 1, 1];
            Suite_Extremums_Ordonnee[b, 2] = Tx1[Tx1.GetLength(0) - 1, 2];
            Suite_Extremums_Ordonnee[b, 3] = Tx1[Tx1.GetLength(0) - 1, 3];
            
                ///////////// ellimination des extremums confondus
                L_SEO = Suite_Extremums_Ordonnee.GetLength(0);
            indice_extremum_a_ellimier = new int[L_SEO];
            Suite_Extremums_Ordonnee_reduite = new double[L_SEO, Suite_Extremums_Ordonnee.GetLength(1)];
            Suite_Extremums_Ordonnee_2 = new double[L_SEO, Suite_Extremums_Ordonnee.GetLength(1)];
            Suite_Extremums_Ordonnee_reduite[0, 0] = Suite_Extremums_Ordonnee[0, 0];
            Suite_Extremums_Ordonnee_reduite[0, 1] = Suite_Extremums_Ordonnee[0, 1];
            Suite_Extremums_Ordonnee_reduite[0, 2] = Suite_Extremums_Ordonnee[0, 2];
            Suite_Extremums_Ordonnee_reduite[0, 3] = Suite_Extremums_Ordonnee[0, 3];

            for (int j = 1; j < L_SEO; j++)
            {
                indice_t_extremum_j_moins_1 = Suite_Extremums_Ordonnee[j - 1, 0];
                indice_t_extremum_j = Suite_Extremums_Ordonnee[j, 0];
                t_extremum_j_moins_1 = Suite_Extremums_Ordonnee[j - 1, 1];
                t_extremum_j = Suite_Extremums_Ordonnee[j, 1];
                if ((t_extremum_j == t_extremum_j_moins_1) || (indice_t_extremum_j == indice_t_extremum_j_moins_1))
                    indice_extremum_a_ellimier[j] = j;
                else
                {
                    Suite_Extremums_Ordonnee_reduite[j, 0] = Suite_Extremums_Ordonnee[j, 0];
                    Suite_Extremums_Ordonnee_reduite[j, 1] = Suite_Extremums_Ordonnee[j, 1];
                    Suite_Extremums_Ordonnee_reduite[j, 2] = Suite_Extremums_Ordonnee[j, 2];
                    Suite_Extremums_Ordonnee_reduite[j, 3] = Suite_Extremums_Ordonnee[j, 3];
                    //Suite_Extremums_Ordonnee_reduite = [Suite_Extremums_Ordonnee_reduite; Suite_Extremums_Ordonnee(j,:)]
                }
            }
            //Suite_Extremums_Ordonnee = Suite_Extremums_Ordonnee_reduite;
            Suite_Extremums_Ordonnee_2 = Suite_Extremums_Ordonnee_reduite;

            // Définition des instants t0, Tc, et t1 pour chaque intervalle Bêta
            L_SEO = Suite_Extremums_Ordonnee_2.GetLength(0);
            debut_intervalle = new double[4];
            maximum_intervalle = new double[4];
            fin_intervalle = new double[4];
            Suite_Intervalles_Beta = new double[4][,];
            Suite_Intervalles_Beta[0] = new double[L_SEO - 2, 3];
            Suite_Intervalles_Beta[1] = new double[L_SEO - 2, 3];
            Suite_Intervalles_Beta[2] = new double[L_SEO - 2, 3];
            Suite_Intervalles_Beta[3] = new double[L_SEO - 2, 3];
            indice_vect = 0;
            for (int j = 1; j <= L_SEO - 2; j++)
            {

                debut_intervalle[0] = Suite_Extremums_Ordonnee_2[j - 1, 0];
                debut_intervalle[1] = Suite_Extremums_Ordonnee_2[j - 1, 1];
                debut_intervalle[2] = Suite_Extremums_Ordonnee_2[j - 1, 2];
                debut_intervalle[3] = Suite_Extremums_Ordonnee_2[j - 1, 3];

                maximum_intervalle[0] = Suite_Extremums_Ordonnee_2[j, 0];
                maximum_intervalle[1] = Suite_Extremums_Ordonnee_2[j, 1];
                maximum_intervalle[2] = Suite_Extremums_Ordonnee_2[j, 2];
                maximum_intervalle[3] = Suite_Extremums_Ordonnee_2[j, 3];

                fin_intervalle[0] = Suite_Extremums_Ordonnee_2[j + 1, 0];
                fin_intervalle[1] = Suite_Extremums_Ordonnee_2[j + 1, 1];
                fin_intervalle[2] = Suite_Extremums_Ordonnee_2[j + 1, 2];
                fin_intervalle[3] = Suite_Extremums_Ordonnee_2[j + 1, 3];

                //Suite_Intervalles_Beta[j] = new double[L_SEO-2,3];
                Suite_Intervalles_Beta[0][indice_vect, 0] = debut_intervalle[0];
                Suite_Intervalles_Beta[0][indice_vect, 1] = maximum_intervalle[0];
                Suite_Intervalles_Beta[0][indice_vect, 2] = fin_intervalle[0];

                Suite_Intervalles_Beta[1][indice_vect, 0] = debut_intervalle[1];
                Suite_Intervalles_Beta[1][indice_vect, 1] = maximum_intervalle[1];
                Suite_Intervalles_Beta[1][indice_vect, 2] = fin_intervalle[1];

                Suite_Intervalles_Beta[2][indice_vect, 0] = debut_intervalle[2];
                Suite_Intervalles_Beta[2][indice_vect, 1] = maximum_intervalle[2];
                Suite_Intervalles_Beta[2][indice_vect, 2] = fin_intervalle[2];

                Suite_Intervalles_Beta[3][indice_vect, 0] = debut_intervalle[3];
                Suite_Intervalles_Beta[3][indice_vect, 1] = maximum_intervalle[3];
                Suite_Intervalles_Beta[3][indice_vect, 2] = fin_intervalle[3];

                indice_vect += 1;

            }

            // Définir les paramettres de l'impulsion beta declanchee %%%%%%%%%%%%%%%%%%%

            nombre_total_impulsions_beta = Suite_Intervalles_Beta[0].GetLength(0); /////////////////////////  Suite_Intervalles_Beta.GetLength(1)
            //Suite_Intervalles_Beta = Suite_Intervalles_Beta;
            
            K_hauteur_preced = 50;     // facultatif
            q_preced = 20;
            p_preced = 20;
            t0_preced = 1;
            tc_preced = 10;
            t1_preced = 20;
            Valeur_de_beta_preced_a_t_ajustement1 = 0;
            Tc_beta_act = Suite_Intervalles_Beta[1][0, 0];  //(1, 1, 2);
            T1_beta_act = Suite_Intervalles_Beta[1][0, 1]; // (1,2,2); 
            profil_vitesse = new definition_de_l_instant_t_ajustement_sur_profil_vitesse();
            t_ajustement = profil_vitesse.Method_retour_t_ajustement(Tc_beta_act, T1_beta_act);
            Vcurv_t_ajustement1 = profil_vitesse.Method_Vcurv_t_ajustement1(Tc_beta_act, T1_beta_act, XX, YY, pas);

            Derivee_Vsig_t_ajustement = profil_vitesse.Method_Derivee_Vsig_t_ajustement(Tc_beta_act, T1_beta_act, XX, YY, pas);
            // [t_ajustement , Vcurv_t_ajustement1 , Derivee_Vsig_t_ajustement] = definition_de_l_instant_t_ajustement_sur_profil_vitesse(Tc_beta_act , T1_beta_act , XX , YY , pas);
            //for(int h=0;h<YY.Length;h++)           

            fonctions_beta = new double[nombre_total_impulsions_beta, 7];
            nombre_total_impulsions_beta = Suite_Intervalles_Beta[0].GetLength(0); //// 
            
            memoire_points_passage_beta_methode2 = new double[nombre_total_impulsions_beta + 1, 4];
            for (int j = 0; j < nombre_total_impulsions_beta; j++)
            {
                t0 = Suite_Intervalles_Beta[1][j, 0];    
                tc = Suite_Intervalles_Beta[1][j, 1];
                t1 = Suite_Intervalles_Beta[1][j, 2];
                K_hauteur = Suite_Intervalles_Beta[2][j, 1];

                Valeur_de_beta_actuel_a_t_ajustement1 = (Vcurv_t_ajustement1) - (Valeur_de_beta_preced_a_t_ajustement1);
                if (Valeur_de_beta_actuel_a_t_ajustement1 > 0)
                    Valeur_de_beta_actuel_a_t_ajustement1 = Valeur_de_beta_actuel_a_t_ajustement1;
                else
                    Valeur_de_beta_actuel_a_t_ajustement1 = K_hauteur / 1000;

                if (Valeur_de_beta_actuel_a_t_ajustement1 > K_hauteur)
                {
                    p = 2.5;
                    p_v1 = p;
                    q = (p * (((t1 - tc) / (tc - t0))));
                    q_v1 = q;
                }
                else
                {
                    p = Math.Abs((Math.Log(Valeur_de_beta_actuel_a_t_ajustement1 / K_hauteur)) / ((Math.Log((t_ajustement - t0) / (tc - t0))) + (((t1 - tc) / (tc - t0)) * Math.Log((t1 - t_ajustement) / (t1 - tc)))));
                    p_v1 = p;
                    q = (p * (((t1 - tc) / (tc - t0))));
                    q_v1 = q;
                }

                //mémorisation de l'instant t_ajustement actuel
                t_ajustement_actuel = t_ajustement;
                Vcurv_t_ajustement1_actuel = Vcurv_t_ajustement1;

                Tc_beta_act = tc;
                T1_beta_act = t1;
                
                t_ajustement = profil_vitesse.Method_retour_t_ajustement(Tc_beta_act, T1_beta_act);
                Vcurv_t_ajustement1 = profil_vitesse.Method_Vcurv_t_ajustement1(Tc_beta_act, T1_beta_act, XX, YY, pas);
                Derivee_Vsig_t_ajustement = profil_vitesse.Method_Derivee_Vsig_t_ajustement(Tc_beta_act, T1_beta_act, XX, YY, pas);
                // [t_ajustement , Vcurv_t_ajustement1, Derivee_Vsig_t_ajustement] = definition_de_l_instant_t_ajustement_sur_profil_vitesse(Tc_beta_act , T1_beta_act , XX , YY , pas);

                if (j < nombre_total_impulsions_beta-1) 
                {
                    t0_suiv = Suite_Intervalles_Beta[1][j+1, 0];
                    tc_suiv = Suite_Intervalles_Beta[1][j+1, 1];
                    t1_suiv = Suite_Intervalles_Beta[1][j+1, 2];
                    K_hauteur_suiv = Suite_Intervalles_Beta[2][j+1, 1];

                    if ((tc_suiv - t0_suiv) > (t1_suiv - tc_suiv))
                    {
                        q_suiv = 2;
                        p_suiv = (q_suiv * (((tc_suiv - t0_suiv) / (t1_suiv - tc_suiv))));
                        if (p_suiv > 4)
                        {
                            p_suiv = 4;
                            q_suiv = (p_suiv * (((t1_suiv - tc_suiv) / (tc_suiv - t0_suiv))));
                        }

                    }
                    else
                    {
                        p_suiv = 2;
                        q_suiv = (p_suiv * (((t1_suiv - tc_suiv) / (tc_suiv - t0_suiv))));
                        if (q_suiv > 4)
                        {
                            q_suiv = 4;
                            p_suiv = (q_suiv * (((tc_suiv - t0_suiv) / (t1_suiv - tc_suiv))));
                        }
                    }
                    //Calcul de la valeur estimee beta_suivante_t_ajustement1
                    beta_suivante_estimee_t_ajustement1 = K_hauteur_suiv * ((Math.Pow(((t_ajustement - t0_suiv) / (tc_suiv - t0_suiv)), p_suiv) * Math.Pow(((t1_suiv - t_ajustement) / (t1_suiv - tc_suiv)), q_suiv)));
                    
                    //Calcul d'une autre estimation de beta_act_ajustement1
                    beta_actuelle_estimee_pour_nouveau_t_ajustement1 = Vcurv_t_ajustement1 - beta_suivante_estimee_t_ajustement1;
                    p_v2 = Math.Abs((Math.Log(beta_actuelle_estimee_pour_nouveau_t_ajustement1 / K_hauteur)) / ((Math.Log((t_ajustement - t0) / (tc - t0))) + (((t1 - tc) / (tc - t0)) * Math.Log((t1 - t_ajustement) / (t1 - tc)))));
                    q_v2 = (p_v2 * (((t1 - tc) / (tc - t0))));
                    p = ((1 * p_v1) + (1 * p_v2)) / 2;
                    q = ((1 * q_v1) + (1 * q_v2)) / 2;
                }
                else
                {
                    p = p;
                    q = (p * (((t1 - tc) / (tc - t0))));
                }
                // memorisation des valeurs des impulsions Beta et de la vitesse  a l'instant t_ajustement@ contrôle des resultats obtenus pour p et q
                Valeur_de_beta_preced_a_t_ajustement1 = K_hauteur_preced * ((Math.Pow(((t_ajustement_actuel - t0_preced) / (tc_preced - t0_preced)), p_preced) * Math.Pow(((t1_preced - t_ajustement_actuel) / (t1_preced - tc_preced)), q_preced)));
                Valeur_de_beta_actuel_a_t_ajustement1 = (Vcurv_t_ajustement1_actuel) - (Valeur_de_beta_preced_a_t_ajustement1);
                
                // memoire_points_passage_beta_methode2 = [memoire_points_passage_beta_methode2; t_ajustement_actuel, Vcurv_t_ajustement1_actuel, Valeur_de_beta_preced_a_t_ajustement1, Valeur_de_beta_actuel_a_t_ajustement1];
                memoire_points_passage_beta_methode2[j, 0] = t_ajustement_actuel;
                memoire_points_passage_beta_methode2[j, 1] = Vcurv_t_ajustement1_actuel;
                memoire_points_passage_beta_methode2[j, 2] = Valeur_de_beta_preced_a_t_ajustement1;
                memoire_points_passage_beta_methode2[j, 3] = Valeur_de_beta_actuel_a_t_ajustement1;
                if (p > 7)
                    p = 7;
                else if ((p < 1) || (Double.IsNaN(p)))
                    p = 1;
                q = (p * (((t1 - tc) / (tc - t0))));
                if (q > 8)
                {
                    q = 8;
                    p = (q * (((tc - t0) / (t1 - tc))));
                }
                else if (q < 0.8)
                {
                    q = 0.8;
                    p = (q * (((tc - t0) / (t1 - tc))));
                }
                Valeur_de_beta_preced_a_t_ajustement1 = K_hauteur * (Math.Pow(((t_ajustement - t0) / (tc - t0)), p) * Math.Pow(((t1 - t_ajustement) / (t1 - tc)), q));
                K_hauteur_preced = K_hauteur;

                q_preced = q;
                p_preced = p;
                t0_preced = t0;
                tc_preced = tc;
                t1_preced = t1;
                // enregistrement des fonctions beta et de leurs parametres
                rong_extremum = j + 2;
                rong_extremum_preced = rong_extremum;
                // fonctions_beta = [fonctions_beta; t0, tc, t1, p, q, K_hauteur, rong_extremum];
                fonctions_beta[j, 0] = t0;
                fonctions_beta[j, 1] = tc;
                fonctions_beta[j, 2] = t1;
                fonctions_beta[j, 3] = p;
                fonctions_beta[j, 4] = q;
                fonctions_beta[j, 5] = K_hauteur;
                fonctions_beta[j, 6] = rong_extremum;

            }
            //Console.ReadKey();
            Valeur_de_beta_actuel_a_t_ajustement1 = 0;
            //  memoire_points_passage_beta_methode2 = [memoire_points_passage_beta_methode2; t_ajustement, Vcurv_t_ajustement1, Valeur_de_beta_preced_a_t_ajustement1, Valeur_de_beta_actuel_a_t_ajustement1];
            memoire_points_passage_beta_methode2[memoire_points_passage_beta_methode2.GetLength(0) - 1, 0] = t_ajustement;
            memoire_points_passage_beta_methode2[memoire_points_passage_beta_methode2.GetLength(0) - 1, 1] = Vcurv_t_ajustement1;
            memoire_points_passage_beta_methode2[memoire_points_passage_beta_methode2.GetLength(0) - 1, 2] = Valeur_de_beta_preced_a_t_ajustement1;
            memoire_points_passage_beta_methode2[memoire_points_passage_beta_methode2.GetLength(0) - 1, 3] = Valeur_de_beta_actuel_a_t_ajustement1;
            // repartition des fonctions beta chevauchées sur les intervalles de temps 

            nbr_fb = fonctions_beta.GetLength(0);

            temps1 = fonctions_beta[0, 0];
            temps2 = fonctions_beta[0, 1];
            rong_fonction1 = 0;
            rong_fonction2 = 1;

            K_amplitude1 = 0;
            K_amplitude2 = fonctions_beta[0, 5];
            indice_data_1 = Suite_Extremums_Ordonnee_2[0, 0];
            indice_data_2 = Suite_Extremums_Ordonnee_2[1, 0];
            tableau_affectation_methode_2 = new double[nbr_fb+1, 8];
            // tableau_affectation_methode_2 = [temps1, temps2, rong_fonction1, rong_fonction2, K_amplitude1, K_amplitude2, indice_data_1, indice_data_2];
            tableau_affectation_methode_2[0, 0] = temps1;
            tableau_affectation_methode_2[0, 1] = temps2;
            tableau_affectation_methode_2[0, 2] = rong_fonction1;
            tableau_affectation_methode_2[0, 3] = rong_fonction2;
            tableau_affectation_methode_2[0, 4] = K_amplitude1;
            tableau_affectation_methode_2[0, 5] = K_amplitude2;
            tableau_affectation_methode_2[0, 6] = indice_data_1;
            tableau_affectation_methode_2[0, 7] = indice_data_2;

            for (int j = 1; j < nbr_fb; j++)
            {
                temps1 = fonctions_beta[j, 0];
                temps2 = fonctions_beta[j, 1];
                rong_fonction1 = j ;
                rong_fonction2 = j+1;
                //rong_extremum1_liste = fonctions_beta[rong_fonction1, 6];
                //rong_extremum2_liste = fonctions_beta[rong_fonction2, 6];
                indice_data_1 = Suite_Extremums_Ordonnee_2[j, 0];
                indice_data_2 = Suite_Extremums_Ordonnee_2[j + 1, 0];
                K_amplitude1 = K_amplitude2;
                K_amplitude2 = fonctions_beta[j, 5];
                //    tableau_affectation_methode_2 = [tableau_affectation_methode_2; temps1, temps2, rong_fonction1, rong_fonction2, K_amplitude1, K_amplitude2, indice_data_1, indice_data_2];
                tableau_affectation_methode_2[j, 0] = temps1; //Math.Round(temps1,1);
                tableau_affectation_methode_2[j, 1] = temps2;
                tableau_affectation_methode_2[j, 2] = rong_fonction1;
                tableau_affectation_methode_2[j, 3] = rong_fonction2;
                tableau_affectation_methode_2[j, 4] = K_amplitude1;
                tableau_affectation_methode_2[j, 5] = K_amplitude2;
                tableau_affectation_methode_2[j, 6] = indice_data_1;
                tableau_affectation_methode_2[j, 7] = indice_data_2;

            }

            temps1 = fonctions_beta[nbr_fb-1, 1];
            temps2 = fonctions_beta[nbr_fb-1, 2];
            rong_fonction1 = nbr_fb;
            rong_fonction2 = 0;
            K_amplitude1 = K_amplitude2;
            K_amplitude2 = 0;
            indice_data_1 = Suite_Extremums_Ordonnee_2[nbr_fb , 0];
            indice_data_2 = Suite_Extremums_Ordonnee_2[nbr_fb + 1, 0];

            // tableau_affectation_methode_2 = [tableau_affectation_methode_2; temps1, temps2, rong_fonction1, rong_fonction2, K_amplitude1, K_amplitude2, indice_data_1, indice_data_2];
            tableau_affectation_methode_2[nbr_fb, 0] = temps1;
            tableau_affectation_methode_2[nbr_fb , 1] = temps2;
            tableau_affectation_methode_2[nbr_fb , 2] = rong_fonction1;
            tableau_affectation_methode_2[nbr_fb , 3] = rong_fonction2;
            tableau_affectation_methode_2[nbr_fb , 4] = K_amplitude1;
            tableau_affectation_methode_2[nbr_fb , 5] = K_amplitude2;
            tableau_affectation_methode_2[nbr_fb , 6] = indice_data_1;
            tableau_affectation_methode_2[nbr_fb , 7] = indice_data_2;

            fonctions_beta_sortie = new double[fonctions_beta.GetLength(0), fonctions_beta.GetLength(1)];
            fonctions_beta_sortie = fonctions_beta;
            
            tableau_affectation_intervalles_sortie = tableau_affectation_methode_2;
            ////////////////////////////////////////////////////////////////////////////////////
            if (ajout_impulsion_finale_nulle == "oui")
            {
                fonctions_beta_sortie = new double[fonctions_beta.GetLength(0)+1, fonctions_beta.GetLength(1)];
                //fonctions_beta_sortie = fonctions_beta;
                for (int i = 0; i < fonctions_beta.GetLength(0); i++)
                    for (int j = 0; j< fonctions_beta.GetLength(1); j++)
                        fonctions_beta_sortie[i, j] = fonctions_beta[i, j];

                nbr_fct_beta_exacte = fonctions_beta.GetLength(0) -1;

                t0_derniere_fct_beta = fonctions_beta[nbr_fct_beta_exacte, 0];
                tc_derniere_fct_beta = fonctions_beta[nbr_fct_beta_exacte, 1];
                t1_derniere_fct_beta = fonctions_beta[nbr_fct_beta_exacte, 2];
                p_derniere_fct_beta = fonctions_beta[nbr_fct_beta_exacte, 3];
                q_derniere_fct_beta = fonctions_beta[nbr_fct_beta_exacte, 4];
                K_derniere_fct_beta = fonctions_beta[nbr_fct_beta_exacte, 5];
                rong_extremum_derniere_fct_beta = fonctions_beta[nbr_fct_beta_exacte, 6];

                t0_fct_beta_immobile = tc_derniere_fct_beta;
                tc_fct_beta_immobile = t1_derniere_fct_beta;
                t1_fct_beta_immobile = tc_fct_beta_immobile + ((tc_fct_beta_immobile - t0_fct_beta_immobile) / 3);
                p_fct_beta_immobile = 2;
                q_fct_beta_immobile = (p_fct_beta_immobile * (((t1_fct_beta_immobile - tc_fct_beta_immobile) / (tc_fct_beta_immobile - t0_fct_beta_immobile))));
                K_fct_beta_immobile = 0.000001;
                rong_extremum_fct_beta_immobile = rong_extremum_derniere_fct_beta + 1;
               // System.////Console.WriteLine(fonctions_beta.GetLength(1));
                //fct_beta_immobile = [t0_fct_beta_immobile, tc_fct_beta_immobile, t1_fct_beta_immobile, p_fct_beta_immobile, q_fct_beta_immobile, K_fct_beta_immobile, rong_extremum_fct_beta_immobile];
                fonctions_beta_sortie[fonctions_beta.GetLength(0), 0] = t0_fct_beta_immobile;
                fonctions_beta_sortie[fonctions_beta.GetLength(0), 1] = tc_fct_beta_immobile;
                fonctions_beta_sortie[fonctions_beta.GetLength(0), 2] = t1_fct_beta_immobile;
                fonctions_beta_sortie[fonctions_beta.GetLength(0), 3] = p_fct_beta_immobile;
                fonctions_beta_sortie[fonctions_beta.GetLength(0), 4] = q_fct_beta_immobile;
                fonctions_beta_sortie[fonctions_beta.GetLength(0), 5] = K_fct_beta_immobile;
                fonctions_beta_sortie[fonctions_beta.GetLength(0), 6] = rong_extremum_fct_beta_immobile;

               

            }
           

            return fonctions_beta_sortie.GetLength(0);

        }
    




        public double[,] Method_tableau_affectation_intervalles_sortie(double[] V, double[] DPV, double[,] points, double[] T, double[] t, double[] RAP_lim, double pas)
        {
            temp_depart = T[0];
            for (int i = 0; i < T.Length; i++)
                T[i] -= temp_depart;
            XX = new double[T.Length];
            YY = new double[T.Length];
            maximum = new double[2];
            //vymin 
            XX = T;
            YY = V;
            //ZZ = DPV;
            //-------------------------------------longueur du vecteur vitesse
            l = YY.Length;
            // -----------------------------------initialisation des paramètres
            //vymin[0] = YY[0];
            //vxmin[0] = XX[0];
            //ind_vmin[0] =  (XX[0] / pas + 1);
            //v_min[0,0]= max(Math.Round(XX[0] / pas), 1);
            //v_min[0,1]= XX[0];
            //v_min[0, 2] = YY[0];

            //v_min = [v_min; (max(round(XX(1) / pas), 1)) XX(1) YY(1)] ;

            taille = 0;
            // calculer la taille de la matrice extrema
            i = 2;
            while (i < l)
            {
                // -----------------------------------detection des maxima
                if (YY[i - 1] >= YY[i - 2])   // ???
                {

                    if (YY[i - 1] >= YY[i])
                    {
                        taille += 1;
                    }
                }
                // ----------------------------------detection des minima
                if (YY[i - 1] <= YY[i - 2])
                {

                    if (YY[i - 1] <= YY[i])
                    {
                        taille += 1;
                    }

                }

                i += 1;
            }

            extrema = new double[taille + 2, 4];
            maximum[0] = Math.Round(XX[0] / pas);
            maximum[1] = 1;
            extrema[0, 0] = max_matrice(maximum);
            extrema[0, 1] = XX[0];
            extrema[0, 2] = YY[0];
            extrema[0, 3] = 0;
            indice = 1;
            i = 2; // à verifier 
            while (i < l)
            {
                // -----------------------------------detection des maxima
                if (YY[i - 1] >= YY[i - 2])   // ???
                {

                    if (YY[i - 1] >= YY[i])
                    {

                        extrema[indice, 0] = Math.Round(XX[i - 1] / pas) + 1;    // extrema = [extrema;  XX(i - 1) YY(i - 1) 2];
                        extrema[indice, 1] = XX[i - 1];
                        extrema[indice, 2] = YY[i - 1];
                        extrema[indice, 3] = 2;
                        indice += 1;
                    }
                }
                // ----------------------------------detection des minima
                if (YY[i - 1] <= YY[i - 2])
                {

                    if (YY[i - 1] <= YY[i])
                    {


                        extrema[indice, 0] = Math.Round(XX[i - 1] / pas) + 1;
                        extrema[indice, 1] = XX[i - 1];
                        extrema[indice, 2] = YY[i - 1];
                        extrema[indice, 3] = 0;
                        indice += 1;
                    }

                }

                i += 1;
            }
            // --------------------------------------le dernier points

            //v_min[] = Math.Round(XX[i - 1] / pas) + 1;     // v_min = [v_min; (round(XX(i - 1) / pas) + 1) XX(l) YY(l)];
            //v_min[] = XX[l];
            //v_min[] = YY[l];

            extrema[indice, 0] = Math.Round(XX[i - 1] / pas) + 1;// extrema = [extrema; (round(XX(i - 1) / pas) + 1) XX(l) YY(l) 0];
            extrema[indice, 1] = XX[l-1];
            extrema[indice, 2] = YY[l-1];
            extrema[indice, 3] = 0;

            // --------------------------------------------------------------------------
            p = 2;
            i = 1;
            // -------------------------------------Elimination des points voisins(bruit)

            /*for (int i = 0; i<extrema.Length/extrema.Rank;i++)
            {
                extremum[,0] = extrema[i,0];
                extremum[, 1] = extrema[i, 1];
                extremum[, 2] = extrema[i, 2];
                extremum[, 3] = extrema[i, 3];
                //hh = extremum;
            }*/
            extremum = new double[extrema.GetLength(0), extrema.GetLength(1)];
            extremum = extrema;                                                  // ?????  

            // calcul taille Tx0
            ind = 0;
            j = 0;    //  1
            while (j <= extremum.GetLength(0) - 3) //verifier condition
            {
                if (extremum[j, 3] == 0)
                {
                    uv = 1;
                    while ((extremum[j + uv, 3] != 2) && (j + uv <= extremum.GetLength(0) - 2)) // verfifier codition
                        uv += 1;
                    if ((extremum[j + uv, 3] == 2) && (j + uv <= extremum.GetLength(0) - 2))
                    {
                        uvp = 1;
                        while ((extremum[j + uv + uvp, 3] != 0) && (j + uv + uvp <= extremum.GetLength(0) - 1))
                            uvp += 1;
                        if ((extremum[j + uv + uvp, 3] == 0) && (j + uv + uvp <= extremum.GetLength(0) - 1))
                        {
                            ind += 1;
                        }
                    }
                    j = j + uv + uvp - 1;
                }
                j += 1;

            }

            Tx0 = new double[ind, extremum.GetLength(1)];
            TxC = new double[ind, extremum.GetLength(1)];
            Tx1 = new double[ind, extremum.GetLength(1)];
            j = 0;    //  1
            ind2 = 0;
            while (j <= extremum.GetLength(0) - 3) //verifier condition
            {
                if (extremum[j, 3] == 0)
                {
                    uv = 1;
                    while ((extremum[j + uv, 3] != 2) && (j + uv <= extremum.GetLength(0) - 2)) // verfifier codition
                        uv += 1;
                    if ((extremum[j + uv, 3] == 2) && (j + uv <= extremum.GetLength(0) - 2))
                    {
                        uvp = 1;
                        while ((extremum[j + uv + uvp, 3] != 0) && (j + uv + uvp <= extremum.GetLength(0) - 1))
                            uvp += 1;
                        if ((extremum[j + uv + uvp, 3] == 0) && (j + uv + uvp <= extremum.GetLength(0) - 1))
                        {

                            Tx0[ind2, 0] = extremum[j, 0];   //[Tx0; extremum(j,:)];
                            Tx0[ind2, 1] = extremum[j, 1];
                            Tx0[ind2, 2] = extremum[j, 2];
                            Tx0[ind2, 3] = extremum[j, 3];


                            TxC[ind2, 0] = extremum[j + uv, 0]; // [TxC; extremum(j + uv,:)];
                            TxC[ind2, 1] = extremum[j + uv, 1];
                            TxC[ind2, 2] = extremum[j + uv, 2];
                            TxC[ind2, 3] = extremum[j + uv, 3];

                            Tx1[ind2, 0] = extremum[j + uv + uvp, 0]; //[Tx1; extremum(j + uv + uvp,:)];
                            Tx1[ind2, 1] = extremum[j + uv + uvp, 1];
                            Tx1[ind2, 2] = extremum[j + uv + uvp, 2];
                            Tx1[ind2, 3] = extremum[j + uv + uvp, 3];

                            ind2 += 1;
                        }
                    }
                    j = j + uv + uvp - 1;
                }
                j += 1;

            }

            vect_Tx0TxCTx1 = new double[Tx0.GetLength(0), Tx0.GetLength(1) * 3];

            for (int cf = 0; cf < Tx0.GetLength(0); cf++) //vect_Tx0TxCTx1 = [Tx0, TxC, Tx1];
            {
                c = 0;
                for (int lf = 0; lf < 4; lf++)
                {
                    vect_Tx0TxCTx1[cf, c] = Tx0[cf, lf];
                    c++;
                }
                for (int lf = 0; lf < 4; lf++)
                {
                    vect_Tx0TxCTx1[cf, c] = TxC[cf, lf];
                    c++;
                }
                for (int lf = 0; lf < 4; lf++)
                {
                    vect_Tx0TxCTx1[cf, c] = Tx1[cf, lf];
                    c++;
                }
            }
            // mettre dans l'ordre les indices des nouveaux points détectés %

            Rap_lim1 = RAP_lim[0];     //seuil de detection grossiere des points de double inflexion en montée
            Rap_lim2 = RAP_lim[1];      // seuil de detection grossiere des points de double inflexion en descente

            // taille Suite_Extremums_Ordonnee
            taille_Suite_Extremums_Ordonnee = 0;
            j = 0;
            nbr_int_init = TxC.GetLength(0); // length(TxC(:, 1));
            nbr_int_fin = nbr_int_init;
            l = YY.Length;
            l_DPV = DPV.Length;
            delta_i = 1; //% 2;
            delta_i2 = 2; // 4;  1;

            while (j < nbr_int_fin)
            {
                taille_Suite_Extremums_Ordonnee += 1;
                maximum_inscrit = 0;

                for (int ih = (int)(Tx0[j, 0] - Tx0[0, 0]); ih < Tx1[j, 0] - Tx0[0, 0]; ih++)   // ???????????????????????
                {

                    if ((ih >= TxC[j, 0] - Tx0[0, 0]) && (maximum_inscrit == 0))
                    {
                        taille_Suite_Extremums_Ordonnee += 1;
                        //Suite_Extremums_Ordonnee = [Suite_Extremums_Ordonnee; TxC(j,:)];
                        maximum_inscrit = 1;

                    }

                    if ((ih - delta_i > 0) && (ih + delta_i2 < l_DPV))
                    {

                        if (DPV[ih] >= 0)
                        {
                            if ((DPV[ih] > DPV[ih - delta_i]) && (DPV[ih] > DPV[ih + delta_i]))
                            {
                                dernier_DPV_max_local = DPV[ih];

                            }
                            if ((DPV[ih] < DPV[ih - delta_i]) && (DPV[ih] < DPV[ih + delta_i]))
                            {
                                marque_ihp = 0;
                                premier_suivant_DPV_max_local = dernier_DPV_max_local;
                                ihp = ih + 1;
                                while ((marque_ihp == 0) && (ihp < TxC[j, 0] - Tx0[0, 0]))
                                {
                                    if ((DPV[ihp] > DPV[ihp - delta_i]) && (DPV[ihp] > DPV[ihp + delta_i]))
                                    {
                                        premier_suivant_DPV_max_local = DPV[ihp];
                                        marque_ihp = 1;
                                    }
                                    ihp += 1;
                                }
                                //if (((DPV[ih] / dernier_DPV_max_local) < Rap_lim1) && ((ih + Tx0[0, 0] - 1 > Tx0[j, 0]) && (ih + Tx0[0, 0] - 1 < Tx1[j, 0]) && (ih + Tx0[0, 0] - 1 != TxC[j, 0])))
                                if (((DPV[ih] / dernier_DPV_max_local) < Rap_lim1) && (((ih + Tx0[0, 0] - 1) > Tx0[j, 0]) && ((ih + Tx0[0, 0] - 1) < Tx1[j, 0]) && ((ih + Tx0[0, 0] - 1) != TxC[j, 0])))
                                {
                                    taille_Suite_Extremums_Ordonnee += 1;

                                }

                            }


                        }

                        if (DPV[ih] <= 0)
                        {
                            if ((DPV[ih] <= DPV[ih - delta_i]) && (DPV[ih] <= DPV[ih + delta_i]))
                            {
                                dernier_DPV_min_local = DPV[ih];
                            }
                            if ((DPV[ih] >= DPV[ih - delta_i]) && (DPV[ih] >= DPV[ih + delta_i]))
                            {
                                if (((DPV[ih] / dernier_DPV_min_local) < Rap_lim2) && ((ih + Tx0[0, 0] - 1 > Tx0[j, 0]) && (ih + Tx0[0, 0] - 1 < Tx1[j, 0]) && (ih + Tx0[0, 0] - 1 != TxC[j, 0])))
                                {
                                    taille_Suite_Extremums_Ordonnee += 1;


                                }
                            }
                        }
                    }
                }
                j += 1;

            }

            //  Fin taille Suite_Extremums_ordonnee

            // mettre dans l'ordre les indices des nouveaux points détectés %
            //for (int y = 0; y < vect_Tx0TxCTx1.GetLength(0); y++)
            // System.////Console.WriteLine(vect_Tx0TxCTx1[y,10]);
            Suite_Extremums_Ordonnee = new double[taille_Suite_Extremums_Ordonnee + 1, Tx0.GetLength(1)];
            oui = 1;
            non = 0;
            //nbr_intervalles_Sensibilite_plus = Sensibilite_plus.Length/3; ////////



            j = 0;
            nbr_int_init = TxC.GetLength(0); // length(TxC(:, 1));
            nbr_int_fin = nbr_int_init;
            l = YY.Length;
            l_DPV = DPV.Length;

            delta_i = 1; //% 2;
            delta_i2 = 2; // 4;  1;
            b = 0;
            while (j < nbr_int_fin)
            {
                //panier_X5 = [];
                Suite_Extremums_Ordonnee[b, 0] = Tx0[j, 0];  // Suite_Extremums_Ordonnee = [Suite_Extremums_Ordonnee; Tx0(j,:)];
                Suite_Extremums_Ordonnee[b, 1] = Tx0[j, 1];
                Suite_Extremums_Ordonnee[b, 2] = Tx0[j, 2];
                Suite_Extremums_Ordonnee[b, 3] = Tx0[j, 3];
                b += 1;
                maximum_inscrit = 0;

                for (int ih = (int)(Tx0[j, 0] - Tx0[0, 0]); ih < Tx1[j, 0] - Tx0[0, 0]; ih++)
                {
                    if ((ih >= TxC[j, 0] - Tx0[0, 0]) && (maximum_inscrit == 0))
                    {
                        Suite_Extremums_Ordonnee[b, 0] = TxC[j, 0];  // Suite_Extremums_Ordonnee = [Suite_Extremums_Ordonnee; Tx0(j,:)];
                        Suite_Extremums_Ordonnee[b, 1] = TxC[j, 1];
                        Suite_Extremums_Ordonnee[b, 2] = TxC[j, 2];
                        Suite_Extremums_Ordonnee[b, 3] = TxC[j, 3];
                        //Suite_Extremums_Ordonnee = [Suite_Extremums_Ordonnee; TxC(j,:)];
                        maximum_inscrit = 1;
                        b += 1;
                    }

                    if ((ih - delta_i > 0) && (ih + delta_i2 < l_DPV))
                    {
                        if (DPV[ih] >= 0)
                        {
                            if ((DPV[ih] > DPV[ih - delta_i]) && (DPV[ih] > DPV[ih + delta_i]))
                            {
                                dernier_DPV_max_local = DPV[ih];

                            }
                            if ((DPV[ih] < DPV[ih - delta_i]) && (DPV[ih] < DPV[ih + delta_i]))
                            {
                                marque_ihp = 0;
                                premier_suivant_DPV_max_local = dernier_DPV_max_local;
                                ihp = ih + 1;
                                while ((marque_ihp == 0) && (ihp < TxC[j, 0] - Tx0[0, 0]))
                                {
                                    if ((DPV[ihp] > DPV[ihp - delta_i]) && (DPV[ihp] > DPV[ihp + delta_i]))
                                    {
                                        premier_suivant_DPV_max_local = DPV[ihp];
                                        marque_ihp = 1;
                                    }
                                    ihp += 1;
                                }
                                if (((DPV[ih] / dernier_DPV_max_local) < Rap_lim1) && ((ih + Tx0[0, 0] - 1 > Tx0[j, 0]) && (ih + Tx0[0, 0] - 1 < Tx1[j, 0]) && (ih + Tx0[0, 0] - 1 != TxC[j, 0])))
                                {
                                    Suite_Extremums_Ordonnee[b, 0] = ih + Tx0[0, 0] ;  // // Suite_Extremums_Ordonnee = [Suite_Extremums_Ordonnee; ih+Tx0(1,1)  XX(ih) YY(ih) 5];
                                    Suite_Extremums_Ordonnee[b, 1] = XX[ih];
                                    Suite_Extremums_Ordonnee[b, 2] = YY[ih];
                                    Suite_Extremums_Ordonnee[b, 3] = 5;

                                    b += 1;

                                }

                            }


                        }

                        if (DPV[ih] <= 0)
                        {
                            if ((DPV[ih] <= DPV[ih - delta_i]) && (DPV[ih] <= DPV[ih + delta_i]))
                            {
                                dernier_DPV_min_local = DPV[ih];
                            }
                            if ((DPV[ih] >= DPV[ih - delta_i]) && (DPV[ih] >= DPV[ih + delta_i]))
                            {
                                if (((DPV[ih] / dernier_DPV_min_local) < Rap_lim2) && ((ih + Tx0[0, 0] - 1 > Tx0[j, 0]) && (ih + Tx0[0, 0] - 1 < Tx1[j, 0]) & (ih + Tx0[0, 0] - 1 != TxC[j, 0])))
                                {
                                    Suite_Extremums_Ordonnee[b, 0] = ih + Tx0[0, 0] ;  //// Suite_Extremums_Ordonnee = [Suite_Extremums_Ordonnee; ih+Tx0(1,1)  XX(ih) YY(ih) 5];
                                    Suite_Extremums_Ordonnee[b, 1] = XX[ih];
                                    Suite_Extremums_Ordonnee[b, 2] = YY[ih];
                                    Suite_Extremums_Ordonnee[b, 3] = 5;

                                    b += 1;
                                }
                            }
                        }
                    }
                }
                j += 1;

            }

            //Suite_Extremums_Ordonnee = [Suite_Extremums_Ordonnee; Tx1(size(Tx1, 1),:)]
            b = Math.Min(b, (Suite_Extremums_Ordonnee.GetLength(0) - 1));
            Suite_Extremums_Ordonnee[b, 0] = Tx1[Tx1.GetLength(0) - 1, 0];
            Suite_Extremums_Ordonnee[b, 1] = Tx1[Tx1.GetLength(0) - 1, 1];
            Suite_Extremums_Ordonnee[b, 2] = Tx1[Tx1.GetLength(0) - 1, 2];
            Suite_Extremums_Ordonnee[b, 3] = Tx1[Tx1.GetLength(0) - 1, 3];

            ///////////// ellimination des extremums confondus
            L_SEO = Suite_Extremums_Ordonnee.GetLength(0);
            indice_extremum_a_ellimier = new int[L_SEO];
            Suite_Extremums_Ordonnee_reduite = new double[L_SEO, Suite_Extremums_Ordonnee.GetLength(1)];
            Suite_Extremums_Ordonnee_2 = new double[L_SEO, Suite_Extremums_Ordonnee.GetLength(1)];
            Suite_Extremums_Ordonnee_reduite[0, 0] = Suite_Extremums_Ordonnee[0, 0];
            Suite_Extremums_Ordonnee_reduite[0, 1] = Suite_Extremums_Ordonnee[0, 1];
            Suite_Extremums_Ordonnee_reduite[0, 2] = Suite_Extremums_Ordonnee[0, 2];
            Suite_Extremums_Ordonnee_reduite[0, 3] = Suite_Extremums_Ordonnee[0, 3];

            for (int j = 1; j < L_SEO; j++)
            {
                indice_t_extremum_j_moins_1 = Suite_Extremums_Ordonnee[j - 1, 0];
                indice_t_extremum_j = Suite_Extremums_Ordonnee[j, 0];
                t_extremum_j_moins_1 = Suite_Extremums_Ordonnee[j - 1, 1];
                t_extremum_j = Suite_Extremums_Ordonnee[j, 1];
                if ((t_extremum_j == t_extremum_j_moins_1) || (indice_t_extremum_j == indice_t_extremum_j_moins_1))
                    indice_extremum_a_ellimier[j] = j;
                else
                {
                    Suite_Extremums_Ordonnee_reduite[j, 0] = Suite_Extremums_Ordonnee[j, 0];
                    Suite_Extremums_Ordonnee_reduite[j, 1] = Suite_Extremums_Ordonnee[j, 1];
                    Suite_Extremums_Ordonnee_reduite[j, 2] = Suite_Extremums_Ordonnee[j, 2];
                    Suite_Extremums_Ordonnee_reduite[j, 3] = Suite_Extremums_Ordonnee[j, 3];
                    //Suite_Extremums_Ordonnee_reduite = [Suite_Extremums_Ordonnee_reduite; Suite_Extremums_Ordonnee(j,:)]
                }
            }
            //Suite_Extremums_Ordonnee = Suite_Extremums_Ordonnee_reduite;
            Suite_Extremums_Ordonnee_2 = Suite_Extremums_Ordonnee_reduite;
            // Définition des instants t0, Tc, et t1 pour chaque intervalle Bêta
            L_SEO = Suite_Extremums_Ordonnee_2.GetLength(0);
            debut_intervalle = new double[4];
            maximum_intervalle = new double[4];
            fin_intervalle = new double[4];
            Suite_Intervalles_Beta = new double[4][,];
            Suite_Intervalles_Beta[0] = new double[L_SEO - 2, 3];
            Suite_Intervalles_Beta[1] = new double[L_SEO - 2, 3];
            Suite_Intervalles_Beta[2] = new double[L_SEO - 2, 3];
            Suite_Intervalles_Beta[3] = new double[L_SEO - 2, 3];
            indice_vect = 0;
            for (int j = 1; j <= L_SEO - 2; j++)
            {

                debut_intervalle[0] = Suite_Extremums_Ordonnee_2[j - 1, 0];
                debut_intervalle[1] = Suite_Extremums_Ordonnee_2[j - 1, 1];
                debut_intervalle[2] = Suite_Extremums_Ordonnee_2[j - 1, 2];
                debut_intervalle[3] = Suite_Extremums_Ordonnee_2[j - 1, 3];

                maximum_intervalle[0] = Suite_Extremums_Ordonnee_2[j, 0];
                maximum_intervalle[1] = Suite_Extremums_Ordonnee_2[j, 1];
                maximum_intervalle[2] = Suite_Extremums_Ordonnee_2[j, 2];
                maximum_intervalle[3] = Suite_Extremums_Ordonnee_2[j, 3];

                fin_intervalle[0] = Suite_Extremums_Ordonnee_2[j + 1, 0];
                fin_intervalle[1] = Suite_Extremums_Ordonnee_2[j + 1, 1];
                fin_intervalle[2] = Suite_Extremums_Ordonnee_2[j + 1, 2];
                fin_intervalle[3] = Suite_Extremums_Ordonnee_2[j + 1, 3];

                //Suite_Intervalles_Beta[j] = new double[L_SEO-2,3];
                Suite_Intervalles_Beta[0][indice_vect, 0] = debut_intervalle[0];
                Suite_Intervalles_Beta[0][indice_vect, 1] = maximum_intervalle[0];
                Suite_Intervalles_Beta[0][indice_vect, 2] = fin_intervalle[0];

                Suite_Intervalles_Beta[1][indice_vect, 0] = debut_intervalle[1];
                Suite_Intervalles_Beta[1][indice_vect, 1] = maximum_intervalle[1];
                Suite_Intervalles_Beta[1][indice_vect, 2] = fin_intervalle[1];

                Suite_Intervalles_Beta[2][indice_vect, 0] = debut_intervalle[2];
                Suite_Intervalles_Beta[2][indice_vect, 1] = maximum_intervalle[2];
                Suite_Intervalles_Beta[2][indice_vect, 2] = fin_intervalle[2];

                Suite_Intervalles_Beta[3][indice_vect, 0] = debut_intervalle[3];
                Suite_Intervalles_Beta[3][indice_vect, 1] = maximum_intervalle[3];
                Suite_Intervalles_Beta[3][indice_vect, 2] = fin_intervalle[3];

                indice_vect += 1;

            }

            // Définir les paramettres de l'impulsion beta declanchee %%%%%%%%%%%%%%%%%%%

            nombre_total_impulsions_beta = Suite_Intervalles_Beta[0].GetLength(0); /////////////////////////  Suite_Intervalles_Beta.GetLength(1)
            //Suite_Intervalles_Beta = Suite_Intervalles_Beta;

            K_hauteur_preced = 50;     // facultatif
            q_preced = 20;
            p_preced = 20;
            t0_preced = 1;
            tc_preced = 10;
            t1_preced = 20;
            Valeur_de_beta_preced_a_t_ajustement1 = 0;
            Tc_beta_act = Suite_Intervalles_Beta[1][0, 0];  //(1, 1, 2);
            T1_beta_act = Suite_Intervalles_Beta[1][0, 1]; // (1,2,2); 
            profil_vitesse = new definition_de_l_instant_t_ajustement_sur_profil_vitesse();
            t_ajustement = profil_vitesse.Method_retour_t_ajustement(Tc_beta_act, T1_beta_act);
            Vcurv_t_ajustement1 = profil_vitesse.Method_Vcurv_t_ajustement1(Tc_beta_act, T1_beta_act, XX, YY, pas);
            Derivee_Vsig_t_ajustement = profil_vitesse.Method_Derivee_Vsig_t_ajustement(Tc_beta_act, T1_beta_act, XX, YY, pas);
            // [t_ajustement , Vcurv_t_ajustement1 , Derivee_Vsig_t_ajustement] = definition_de_l_instant_t_ajustement_sur_profil_vitesse(Tc_beta_act , T1_beta_act , XX , YY , pas);
            fonctions_beta = new double[nombre_total_impulsions_beta, 7];
            nombre_total_impulsions_beta = Suite_Intervalles_Beta[0].GetLength(0);  

            memoire_points_passage_beta_methode2 = new double[nombre_total_impulsions_beta + 1, 4];
            for (int j = 0; j < nombre_total_impulsions_beta; j++)
            {
                t0 = Suite_Intervalles_Beta[1][j, 0];
                tc = Suite_Intervalles_Beta[1][j, 1];
                t1 = Suite_Intervalles_Beta[1][j, 2];
                K_hauteur = Suite_Intervalles_Beta[2][j, 1];

                Valeur_de_beta_actuel_a_t_ajustement1 = (Vcurv_t_ajustement1) - (Valeur_de_beta_preced_a_t_ajustement1);
                if (Valeur_de_beta_actuel_a_t_ajustement1 > 0)
                    Valeur_de_beta_actuel_a_t_ajustement1 = Valeur_de_beta_actuel_a_t_ajustement1;
                else
                    Valeur_de_beta_actuel_a_t_ajustement1 = K_hauteur / 1000;

                if (Valeur_de_beta_actuel_a_t_ajustement1 > K_hauteur)
                {
                    p = 2.5;
                    p_v1 = p;
                    q = (p * (((t1 - tc) / (tc - t0))));
                    q_v1 = q;
                }
                else
                {
                    p = Math.Abs((Math.Log(Valeur_de_beta_actuel_a_t_ajustement1 / K_hauteur)) / ((Math.Log((t_ajustement - t0) / (tc - t0))) + (((t1 - tc) / (tc - t0)) * Math.Log((t1 - t_ajustement) / (t1 - tc)))));
                    p_v1 = p;
                    q = (p * (((t1 - tc) / (tc - t0))));
                    q_v1 = q;
                }

                //mémorisation de l'instant t_ajustement actuel
                t_ajustement_actuel = t_ajustement;
                Vcurv_t_ajustement1_actuel = Vcurv_t_ajustement1;

                Tc_beta_act = tc;
                T1_beta_act = t1;

                t_ajustement = profil_vitesse.Method_retour_t_ajustement(Tc_beta_act, T1_beta_act);
                Vcurv_t_ajustement1 = profil_vitesse.Method_Vcurv_t_ajustement1(Tc_beta_act, T1_beta_act, XX, YY, pas);
                Derivee_Vsig_t_ajustement = profil_vitesse.Method_Derivee_Vsig_t_ajustement(Tc_beta_act, T1_beta_act, XX, YY, pas);
                if (j < nombre_total_impulsions_beta - 1)
                {
                    t0_suiv = Suite_Intervalles_Beta[1][j + 1, 0];
                    tc_suiv = Suite_Intervalles_Beta[1][j + 1, 1];
                    t1_suiv = Suite_Intervalles_Beta[1][j + 1, 2];
                    K_hauteur_suiv = Suite_Intervalles_Beta[2][j + 1, 1];

                    if ((tc_suiv - t0_suiv) > (t1_suiv - tc_suiv))
                    {
                        q_suiv = 2;
                        p_suiv = (q_suiv * (((tc_suiv - t0_suiv) / (t1_suiv - tc_suiv))));
                        if (p_suiv > 4)
                        {
                            p_suiv = 4;
                            q_suiv = (p_suiv * (((t1_suiv - tc_suiv) / (tc_suiv - t0_suiv))));
                        }

                    }
                    else
                    {
                        p_suiv = 2;
                        q_suiv = (p_suiv * (((t1_suiv - tc_suiv) / (tc_suiv - t0_suiv))));
                        if (q_suiv > 4)
                        {
                            q_suiv = 4;
                            p_suiv = (q_suiv * (((tc_suiv - t0_suiv) / (t1_suiv - tc_suiv))));
                        }
                    }
                    //Calcul de la valeur estimee beta_suivante_t_ajustement1
                    beta_suivante_estimee_t_ajustement1 = K_hauteur_suiv * ((Math.Pow(((t_ajustement - t0_suiv) / (tc_suiv - t0_suiv)), p_suiv) * Math.Pow(((t1_suiv - t_ajustement) / (t1_suiv - tc_suiv)), q_suiv)));

                    //Calcul d'une autre estimation de beta_act_ajustement1
                    beta_actuelle_estimee_pour_nouveau_t_ajustement1 = Vcurv_t_ajustement1 - beta_suivante_estimee_t_ajustement1;
                    p_v2 = Math.Abs((Math.Log(beta_actuelle_estimee_pour_nouveau_t_ajustement1 / K_hauteur)) / ((Math.Log((t_ajustement - t0) / (tc - t0))) + (((t1 - tc) / (tc - t0)) * Math.Log((t1 - t_ajustement) / (t1 - tc)))));
                    q_v2 = (p_v2 * (((t1 - tc) / (tc - t0))));
                    p = ((1 * p_v1) + (1 * p_v2)) / 2;
                    q = ((1 * q_v1) + (1 * q_v2)) / 2;
                }
                else
                {
                    p = p;
                    q = (p * (((t1 - tc) / (tc - t0))));
                }
                // memorisation des valeurs des impulsions Beta et de la vitesse  a l'instant t_ajustement@ contrôle des resultats obtenus pour p et q
                Valeur_de_beta_preced_a_t_ajustement1 = K_hauteur_preced * ((Math.Pow(((t_ajustement_actuel - t0_preced) / (tc_preced - t0_preced)), p_preced) * Math.Pow(((t1_preced - t_ajustement_actuel) / (t1_preced - tc_preced)), q_preced)));
                Valeur_de_beta_actuel_a_t_ajustement1 = (Vcurv_t_ajustement1_actuel) - (Valeur_de_beta_preced_a_t_ajustement1);

                // memoire_points_passage_beta_methode2 = [memoire_points_passage_beta_methode2; t_ajustement_actuel, Vcurv_t_ajustement1_actuel, Valeur_de_beta_preced_a_t_ajustement1, Valeur_de_beta_actuel_a_t_ajustement1];
                memoire_points_passage_beta_methode2[j, 0] = t_ajustement_actuel;
                memoire_points_passage_beta_methode2[j, 1] = Vcurv_t_ajustement1_actuel;
                memoire_points_passage_beta_methode2[j, 2] = Valeur_de_beta_preced_a_t_ajustement1;
                memoire_points_passage_beta_methode2[j, 3] = Valeur_de_beta_actuel_a_t_ajustement1;
                if (p > 7)
                    p = 7;
                else if ((p < 1) || (Double.IsNaN(p)))
                        p = 1;
                q = (p * (((t1 - tc) / (tc - t0))));
                if (q > 8)
                {
                    q = 8;
                    p = (q * (((tc - t0) / (t1 - tc))));
                }
                else if (q < 0.8)
                {
                    q = 0.8;
                    p = (q * (((tc - t0) / (t1 - tc))));
                }
                Valeur_de_beta_preced_a_t_ajustement1 = K_hauteur * (Math.Pow(((t_ajustement - t0) / (tc - t0)), p) * Math.Pow(((t1 - t_ajustement) / (t1 - tc)), q));
                K_hauteur_preced = K_hauteur;

                q_preced = q;
                p_preced = p;
                t0_preced = t0;
                tc_preced = tc;
                t1_preced = t1;
                // enregistrement des fonctions beta et de leurs parametres
                rong_extremum = j + 2;
                rong_extremum_preced = rong_extremum;
                // fonctions_beta = [fonctions_beta; t0, tc, t1, p, q, K_hauteur, rong_extremum];
                fonctions_beta[j, 0] = t0;
                fonctions_beta[j, 1] = tc;
                fonctions_beta[j, 2] = t1;
                fonctions_beta[j, 3] = p;
                fonctions_beta[j, 4] = q;
                fonctions_beta[j, 5] = K_hauteur;
                fonctions_beta[j, 6] = rong_extremum;

                // System.////Console.WriteLine(Math.Round(t0,1));
            }

            Valeur_de_beta_actuel_a_t_ajustement1 = 0;
            //  memoire_points_passage_beta_methode2 = [memoire_points_passage_beta_methode2; t_ajustement, Vcurv_t_ajustement1, Valeur_de_beta_preced_a_t_ajustement1, Valeur_de_beta_actuel_a_t_ajustement1];
            memoire_points_passage_beta_methode2[memoire_points_passage_beta_methode2.GetLength(0) - 1, 0] = t_ajustement;
            memoire_points_passage_beta_methode2[memoire_points_passage_beta_methode2.GetLength(0) - 1, 1] = Vcurv_t_ajustement1;
            memoire_points_passage_beta_methode2[memoire_points_passage_beta_methode2.GetLength(0) - 1, 2] = Valeur_de_beta_preced_a_t_ajustement1;
            memoire_points_passage_beta_methode2[memoire_points_passage_beta_methode2.GetLength(0) - 1, 3] = Valeur_de_beta_actuel_a_t_ajustement1;
            // repartition des fonctions beta chevauchées sur les intervalles de temps 

            nbr_fb = fonctions_beta.GetLength(0);

            temps1 = fonctions_beta[0, 0];
            temps2 = fonctions_beta[0, 1];
            rong_fonction1 = 0;
            rong_fonction2 = 1;

            K_amplitude1 = 0;
            K_amplitude2 = fonctions_beta[0, 5];
            indice_data_1 = Suite_Extremums_Ordonnee_2[0, 0];
            indice_data_2 = Suite_Extremums_Ordonnee_2[1, 0];
            tableau_affectation_methode_2 = new double[nbr_fb + 1, 8];
            // tableau_affectation_methode_2 = [temps1, temps2, rong_fonction1, rong_fonction2, K_amplitude1, K_amplitude2, indice_data_1, indice_data_2];
            tableau_affectation_methode_2[0, 0] = temps1;
            tableau_affectation_methode_2[0, 1] = temps2;
            tableau_affectation_methode_2[0, 2] = rong_fonction1;
            tableau_affectation_methode_2[0, 3] = rong_fonction2;
            tableau_affectation_methode_2[0, 4] = K_amplitude1;
            tableau_affectation_methode_2[0, 5] = K_amplitude2;
            tableau_affectation_methode_2[0, 6] = indice_data_1;
            tableau_affectation_methode_2[0, 7] = indice_data_2;

            for (int j = 1; j < nbr_fb; j++)
            {
                temps1 = fonctions_beta[j, 0];
                temps2 = fonctions_beta[j, 1];
                rong_fonction1 = j;
                rong_fonction2 = j + 1;
                //rong_extremum1_liste = fonctions_beta[rong_fonction1, 6];
                //rong_extremum2_liste = fonctions_beta[rong_fonction2, 6];
                indice_data_1 = Suite_Extremums_Ordonnee_2[j, 0];
                indice_data_2 = Suite_Extremums_Ordonnee_2[j + 1, 0];
                K_amplitude1 = K_amplitude2;
                K_amplitude2 = fonctions_beta[j, 5];
                //    tableau_affectation_methode_2 = [tableau_affectation_methode_2; temps1, temps2, rong_fonction1, rong_fonction2, K_amplitude1, K_amplitude2, indice_data_1, indice_data_2];
                tableau_affectation_methode_2[j, 0] = temps1; //Math.Round(temps1,1);
                tableau_affectation_methode_2[j, 1] = temps2;
                tableau_affectation_methode_2[j, 2] = rong_fonction1;
                tableau_affectation_methode_2[j, 3] = rong_fonction2;
                tableau_affectation_methode_2[j, 4] = K_amplitude1;
                tableau_affectation_methode_2[j, 5] = K_amplitude2;
                tableau_affectation_methode_2[j, 6] = indice_data_1;
                tableau_affectation_methode_2[j, 7] = indice_data_2;

            }

            temps1 = fonctions_beta[nbr_fb - 1, 1];
            temps2 = fonctions_beta[nbr_fb - 1, 2];
            rong_fonction1 = nbr_fb;
            rong_fonction2 = 0;
            K_amplitude1 = K_amplitude2;
            K_amplitude2 = 0;
            indice_data_1 = Suite_Extremums_Ordonnee_2[nbr_fb, 0];
            indice_data_2 = Suite_Extremums_Ordonnee_2[nbr_fb + 1, 0];

            // tableau_affectation_methode_2 = [tableau_affectation_methode_2; temps1, temps2, rong_fonction1, rong_fonction2, K_amplitude1, K_amplitude2, indice_data_1, indice_data_2];
            tableau_affectation_methode_2[nbr_fb, 0] = temps1;
            tableau_affectation_methode_2[nbr_fb, 1] = temps2;
            tableau_affectation_methode_2[nbr_fb, 2] = rong_fonction1;
            tableau_affectation_methode_2[nbr_fb, 3] = rong_fonction2;
            tableau_affectation_methode_2[nbr_fb, 4] = K_amplitude1;
            tableau_affectation_methode_2[nbr_fb, 5] = K_amplitude2;
            tableau_affectation_methode_2[nbr_fb, 6] = indice_data_1;
            tableau_affectation_methode_2[nbr_fb, 7] = indice_data_2;

            fonctions_beta_sortie = new double[fonctions_beta.GetLength(0), fonctions_beta.GetLength(1)];
            fonctions_beta_sortie = fonctions_beta;

            tableau_affectation_intervalles_sortie = tableau_affectation_methode_2;
            ////////////////////////////////////////////////////////////////////////////////////


            return tableau_affectation_intervalles_sortie;
        }






        public double[,] Method_vect_Tx0TxCTx1(double[] V, double[] DPV, double[,] points, double[] T, double[] t, double[] RAP_lim, double pas)
        {

            temp_depart = T[0];
            for (int i = 0; i < T.Length; i++)
                T[i] -= temp_depart;
            XX = new double[T.Length];
            YY = new double[T.Length];
            maximum = new double[2];
            //vymin 
            XX = T;
            YY = V;
            //ZZ = DPV;
            //-------------------------------------longueur du vecteur vitesse
            l = YY.Length;
            // -----------------------------------initialisation des paramètres
            //vymin[0] = YY[0];
            //vxmin[0] = XX[0];
            //ind_vmin[0] =  (XX[0] / pas + 1);
            //v_min[0,0]= max(Math.Round(XX[0] / pas), 1);
            //v_min[0,1]= XX[0];
            //v_min[0, 2] = YY[0];

            //v_min = [v_min; (max(round(XX(1) / pas), 1)) XX(1) YY(1)] ;

            taille = 0;
            // calculer la taille de la matrice extrema
            i = 2;
            while (i < l)
            {
                // -----------------------------------detection des maxima
                if (YY[i - 1] >= YY[i - 2])   // ???
                {

                    if (YY[i - 1] >= YY[i])
                    {
                        taille += 1;
                    }
                }
                // ----------------------------------detection des minima
                if (YY[i - 1] <= YY[i - 2])
                {

                    if (YY[i - 1] <= YY[i])
                    {
                        taille += 1;
                    }

                }

                i += 1;
            }

            extrema = new double[taille + 2, 4];
            maximum[0] = Math.Round(XX[0] / pas);
            maximum[1] = 1;
            extrema[0, 0] = max_matrice(maximum);
            extrema[0, 1] = XX[0];
            extrema[0, 2] = YY[0];
            extrema[0, 3] = 0;
            indice = 1;
            i = 2; // à verifier 
            while (i < l)
            {
                // -----------------------------------detection des maxima
                if (YY[i - 1] >= YY[i - 2])   // ???
                {

                    if (YY[i - 1] >= YY[i])
                    {

                        extrema[indice, 0] = Math.Round(XX[i - 1] / pas) + 1;    // extrema = [extrema;  XX(i - 1) YY(i - 1) 2];
                        extrema[indice, 1] = XX[i - 1];
                        extrema[indice, 2] = YY[i - 1];
                        extrema[indice, 3] = 2;
                        indice += 1;
                    }
                }
                // ----------------------------------detection des minima
                if (YY[i - 1] <= YY[i - 2])
                {

                    if (YY[i - 1] <= YY[i])
                    {


                        extrema[indice, 0] = Math.Round(XX[i - 1] / pas) + 1;
                        extrema[indice, 1] = XX[i - 1];
                        extrema[indice, 2] = YY[i - 1];
                        extrema[indice, 3] = 0;
                        indice += 1;
                    }

                }

                i += 1;
            }
            // --------------------------------------le dernier points

            //v_min[] = Math.Round(XX[i - 1] / pas) + 1;     // v_min = [v_min; (round(XX(i - 1) / pas) + 1) XX(l) YY(l)];
            //v_min[] = XX[l];
            //v_min[] = YY[l];

            extrema[indice, 0] = Math.Round(XX[i - 1] / pas) + 1;// extrema = [extrema; (round(XX(i - 1) / pas) + 1) XX(l) YY(l) 0];
            extrema[indice, 1] = XX[l-1];
            extrema[indice, 2] = YY[l-1];
            extrema[indice, 3] = 0;

            // --------------------------------------------------------------------------
            p = 2;
            i = 1;
            // -------------------------------------Elimination des points voisins(bruit)

            /*for (int i = 0; i<extrema.Length/extrema.Rank;i++)
            {
                extremum[,0] = extrema[i,0];
                extremum[, 1] = extrema[i, 1];
                extremum[, 2] = extrema[i, 2];
                extremum[, 3] = extrema[i, 3];
                //hh = extremum;
            }*/
            extremum = new double[extrema.GetLength(0), extrema.GetLength(1)];
            extremum = extrema;                                                    

            // calcul taille Tx0
            ind = 0;
            j = 0;    //  1
            while (j <= extremum.GetLength(0) - 3) //verifier condition
            {
                if (extremum[j, 3] == 0)
                {
                    uv = 1;
                    while ((extremum[j + uv, 3] != 2) && (j + uv <= extremum.GetLength(0) - 2)) // verfifier codition
                        uv += 1;
                    if ((extremum[j + uv, 3] == 2) && (j + uv <= extremum.GetLength(0) - 2))
                    {
                        uvp = 1;
                        while ((extremum[j + uv + uvp, 3] != 0) && (j + uv + uvp <= extremum.GetLength(0) - 1))
                            uvp += 1;
                        if ((extremum[j + uv + uvp, 3] == 0) && (j + uv + uvp <= extremum.GetLength(0) - 1))
                        {
                            ind += 1;
                        }
                    }
                    j = j + uv + uvp - 1;
                }
                j += 1;

            }

            Tx0 = new double[ind, extremum.GetLength(1)];
            TxC = new double[ind, extremum.GetLength(1)];
            Tx1 = new double[ind, extremum.GetLength(1)];
            j = 0;    //  1
            ind2 = 0;
            while (j <= extremum.GetLength(0) - 3) //verifier condition
            {
                if (extremum[j, 3] == 0)
                {
                    uv = 1;
                    while ((extremum[j + uv, 3] != 2) && (j + uv <= extremum.GetLength(0) - 2)) // verfifier codition
                        uv += 1;
                    if ((extremum[j + uv, 3] == 2) && (j + uv <= extremum.GetLength(0) - 2))
                    {
                        uvp = 1;
                        while ((extremum[j + uv + uvp, 3] != 0) && (j + uv + uvp <= extremum.GetLength(0) -1))
                            uvp += 1;
                        if ((extremum[j + uv + uvp, 3] == 0) && (j + uv + uvp <= extremum.GetLength(0) - 1))
                        {

                            Tx0[ind2, 0] = extremum[j, 0];   //[Tx0; extremum(j,:)];
                            Tx0[ind2, 1] = extremum[j, 1];
                            Tx0[ind2, 2] = extremum[j, 2];
                            Tx0[ind2, 3] = extremum[j, 3];


                            TxC[ind2, 0] = extremum[j + uv, 0]; // [TxC; extremum(j + uv,:)];
                            TxC[ind2, 1] = extremum[j + uv, 1];
                            TxC[ind2, 2] = extremum[j + uv, 2];
                            TxC[ind2, 3] = extremum[j + uv, 3];

                            Tx1[ind2, 0] = extremum[j + uv + uvp, 0]; //[Tx1; extremum(j + uv + uvp,:)];
                            Tx1[ind2, 1] = extremum[j + uv + uvp, 1];
                            Tx1[ind2, 2] = extremum[j + uv + uvp, 2];
                            Tx1[ind2, 3] = extremum[j + uv + uvp, 3];

                            ind2 += 1;
                        }
                    }
                    j = j + uv + uvp - 1;
                }
                j += 1;

            }

            vect_Tx0TxCTx1 = new double[Tx0.GetLength(0), Tx0.GetLength(1) * 3];

            for (int cf = 0; cf < Tx0.GetLength(0); cf++) //vect_Tx0TxCTx1 = [Tx0, TxC, Tx1];
            {
                c = 0;
                for (int lf = 0; lf < 4; lf++)
                {
                    vect_Tx0TxCTx1[cf, c] = Tx0[cf, lf];
                    c++;
                }
                for (int lf = 0; lf < 4; lf++)
                {
                    vect_Tx0TxCTx1[cf, c] = TxC[cf, lf];
                    c++;
                }
                for (int lf = 0; lf < 4; lf++)
                {
                    vect_Tx0TxCTx1[cf, c] = Tx1[cf, lf];
                    c++;
                }
            }
            return vect_Tx0TxCTx1;
        }



        public int Method_size_vect_Tx0TxCTx1(double[] V, double[] DPV, double[,] points, double[] T, double[] t, double[] RAP_lim, double pas)
        {

            temp_depart = T[0];
            for (int i = 0; i < T.Length; i++)
                T[i] -= temp_depart;
            XX = new double[T.Length];
            YY = new double[T.Length];
            maximum = new double[2];
            //vymin 
            XX = T;
            YY = V;
            //ZZ = DPV;
            //-------------------------------------longueur du vecteur vitesse
            l = YY.Length;
            // -----------------------------------initialisation des paramètres
            //vymin[0] = YY[0];
            //vxmin[0] = XX[0];
            //ind_vmin[0] =  (XX[0] / pas + 1);
            //v_min[0,0]= max(Math.Round(XX[0] / pas), 1);
            //v_min[0,1]= XX[0];
            //v_min[0, 2] = YY[0];

            //v_min = [v_min; (max(round(XX(1) / pas), 1)) XX(1) YY(1)] ;

            taille = 0;
            // calculer la taille de la matrice extrema
            i = 2;
            while (i < l)
            {
                // -----------------------------------detection des maxima
                if (YY[i - 1] >= YY[i - 2])   // ???
                {

                    if (YY[i - 1] >= YY[i])
                    {
                        taille += 1;
                    }
                }
                // ----------------------------------detection des minima
                if (YY[i - 1] <= YY[i - 2])
                {

                    if (YY[i - 1] <= YY[i])
                    {
                        taille += 1;
                    }

                }

                i += 1;
            }

            extrema = new double[taille + 2, 4];
            maximum[0] = Math.Round(XX[0] / pas);
            maximum[1] = 1;
            extrema[0, 0] = max_matrice(maximum);
            extrema[0, 1] = XX[0];
            extrema[0, 2] = YY[0];
            extrema[0, 3] = 0;
            indice = 1;
            i = 2; // à verifier 
            while (i < l)
            {
                // -----------------------------------detection des maxima
                if (YY[i - 1] >= YY[i - 2])   // ???
                {

                    if (YY[i - 1] >= YY[i])
                    {

                        extrema[indice, 0] = Math.Round(XX[i - 1] / pas) + 1;    // extrema = [extrema;  XX(i - 1) YY(i - 1) 2];
                        extrema[indice, 1] = XX[i - 1];
                        extrema[indice, 2] = YY[i - 1];
                        extrema[indice, 3] = 2;
                        indice += 1;
                    }
                }
                // ----------------------------------detection des minima
                if (YY[i - 1] <= YY[i - 2])
                {

                    if (YY[i - 1] <= YY[i])
                    {


                        extrema[indice, 0] = Math.Round(XX[i - 1] / pas) + 1;
                        extrema[indice, 1] = XX[i - 1];
                        extrema[indice, 2] = YY[i - 1];
                        extrema[indice, 3] = 0;
                        indice += 1;
                    }

                }

                i += 1;
            }
            // --------------------------------------le dernier points

            //v_min[] = Math.Round(XX[i - 1] / pas) + 1;     // v_min = [v_min; (round(XX(i - 1) / pas) + 1) XX(l) YY(l)];
            //v_min[] = XX[l];
            //v_min[] = YY[l];

            extrema[indice, 0] = Math.Round(XX[i - 1] / pas) + 1;// extrema = [extrema; (round(XX(i - 1) / pas) + 1) XX(l) YY(l) 0];
            extrema[indice, 1] = XX[l-1];
            extrema[indice, 2] = YY[l-1];
            extrema[indice, 3] = 0;

            // --------------------------------------------------------------------------
            p = 2;
            i = 1;
            // -------------------------------------Elimination des points voisins(bruit)

            /*for (int i = 0; i<extrema.Length/extrema.Rank;i++)
            {
                extremum[,0] = extrema[i,0];
                extremum[, 1] = extrema[i, 1];
                extremum[, 2] = extrema[i, 2];
                extremum[, 3] = extrema[i, 3];
                //hh = extremum;
            }*/
            extremum = new double[extrema.Length / extrema.Rank, extrema.Rank];
            extremum = extrema;                                                  // ?????  

            // calcul taille Tx0
            ind = 0;
            j = 0;    //  1
            while (j <= extremum.GetLength(0) - 3) //verifier condition
            {
                if (extremum[j, 3] == 0)
                {
                    uv = 1;
                    while ((extremum[j + uv, 3] != 2) && (j + uv < extremum.GetLength(0) -2)) // verfifier codition
                        uv += 1;
                    if ((extremum[j + uv, 3] == 2) && (j + uv <= extremum.GetLength(0) - 2))
                    {
                        uvp = 1;
                        while ((extremum[j + uv + uvp, 3] != 0) && (j + uv + uvp <= extremum.GetLength(0)-1))
                            uvp += 1;
                        if ((extremum[j + uv + uvp, 3] == 0) && (j + uv + uvp <= extremum.GetLength(0)-1))
                        {
                            ind += 1;
                        }
                    }
                    j = j + uv + uvp - 1;
                }
                j += 1;

            }

            Tx0 = new double[ind, extremum.GetLength(1)];
            TxC = new double[ind, extremum.GetLength(1)];
            Tx1 = new double[ind, extremum.GetLength(1)];
            j = 0;    //  1
            ind2 = 0;
            while (j <= extremum.GetLength(0) - 3) //verifier condition
            {
                if (extremum[j, 3] == 0)
                {
                    uv = 1;
                    while ((extremum[j + uv, 3] != 2) && (j + uv <= extremum.GetLength(0) - 2)) // verfifier codition
                        uv += 1;
                    if ((extremum[j + uv, 3] == 2) && (j + uv <= extremum.GetLength(0) - 2))
                    {
                        uvp = 1;
                        while ((extremum[j + uv + uvp, 3] != 0) && (j + uv + uvp <= extremum.GetLength(0) - 1))
                            uvp += 1;
                        if ((extremum[j + uv + uvp, 3] == 0) && (j + uv + uvp <= extremum.GetLength(0) - 1))
                        {

                            Tx0[ind2, 0] = extremum[j, 0];   //[Tx0; extremum(j,:)];
                            Tx0[ind2, 1] = extremum[j, 1];
                            Tx0[ind2, 2] = extremum[j, 2];
                            Tx0[ind2, 3] = extremum[j, 3];


                            TxC[ind2, 0] = extremum[j + uv, 0]; // [TxC; extremum(j + uv,:)];
                            TxC[ind2, 1] = extremum[j + uv, 1];
                            TxC[ind2, 2] = extremum[j + uv, 2];
                            TxC[ind2, 3] = extremum[j + uv, 3];

                            Tx1[ind2, 0] = extremum[j + uv + uvp, 0]; //[Tx1; extremum(j + uv + uvp,:)];
                            Tx1[ind2, 1] = extremum[j + uv + uvp, 1];
                            Tx1[ind2, 2] = extremum[j + uv + uvp, 2];
                            Tx1[ind2, 3] = extremum[j + uv + uvp, 3];

                            ind2 += 1;
                        }
                    }
                    j = j + uv + uvp - 1;
                }
                j += 1;

            }

            vect_Tx0TxCTx1 = new double[Tx0.GetLength(0), Tx0.GetLength(1) * 3];

            for (int cf = 0; cf < Tx0.GetLength(0); cf++) //vect_Tx0TxCTx1 = [Tx0, TxC, Tx1];
            {
                c = 0;
                for (int lf = 0; lf < 4; lf++)
                {
                    vect_Tx0TxCTx1[cf, c] = Tx0[cf, lf];
                    c++;
                }
                for (int lf = 0; lf < 4; lf++)
                {
                    vect_Tx0TxCTx1[cf, c] = TxC[cf, lf];
                    c++;
                }
                for (int lf = 0; lf < 4; lf++)
                {
                    vect_Tx0TxCTx1[cf, c] = Tx1[cf, lf];
                    c++;
                }
            }
            return vect_Tx0TxCTx1.GetLength(0);
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

    }


}

