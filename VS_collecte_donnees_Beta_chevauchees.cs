using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{
    class VS_collecte_donnees_Beta_chevauchees
    {
        /*
         La fonction collecte_donnees_Beta_chevauchees permet de collecter les paramètre des impulsions Bêta chevauchées ainsi que ceux et des arcs d'ellipse 
         modélisant le signal en ligne dans une seule matrice qui représente le modèle de sortie 
         */

        public int nbr_seg;
        public double position_trait,delta_T, rap_Tc, K_i, teta_deb_DAT, teta_deb, x_centre, y_centre, x_pt_depart_arc, y_pt_depart_arc, x_pt_arrivee_arc, y_pt_arrivee_arc, dist_centre_pt_deb, dist_centre_pt_arr, teta_i;
        public double[] T0, Tc, T1, P, K, DELTA_T, RAP_Tc, P0SITION_TRAIT,A,B, TETA, TETA_P, X_deb_arc, Y_deb_arc, X_fin_arc, Y_fin_arc;
        public double[,] modele_sortie;
        double X_deb_i;
        double Y_deb_i;
        double X_fin_i;
        double Y_fin_i;
        public double[,] Method_VS_collecte_donnees_Beta_chevauchees(double[,] fonctions_beta, double[,] param_trajectoire)
        {

            double autorisation_correctif_3 = 0;

            nbr_seg = fonctions_beta.GetLength(0);
            DELTA_T = new double[nbr_seg];
            RAP_Tc = new double[nbr_seg];
            P0SITION_TRAIT = new double[nbr_seg];
            T0 = new double[nbr_seg];
            for (int i=0;i<nbr_seg;i++)  // T0 = fonctions_beta(:, 1);
                T0[i]= fonctions_beta[i, 0];

            Tc = new double[nbr_seg];
            for (int i = 0; i < nbr_seg; i++)  // Tc = fonctions_beta(:, 2);
                Tc[i] = fonctions_beta[i, 1];

            T1 = new double[nbr_seg];
            for (int i = 0; i < nbr_seg; i++)  // T1 = fonctions_beta(:, 3);
                T1[i] = fonctions_beta[i, 2];

            P = new double[nbr_seg];
            for (int i = 0; i < nbr_seg; i++)  // P = fonctions_beta(:, 4);
                P[i] = fonctions_beta[i, 3];

           /* for (int h = 0; h < P.Length; h++)
            {
                System.Console.WriteLine(P[h]); // 12
            }*/
            K = new double[nbr_seg];
            for (int i = 0; i < nbr_seg; i++)  //  K = fonctions_beta(:, 6);
                K[i] = fonctions_beta[i, 5];

            // % Rapport_Amplitude_Impulsions_Successives_Vitesse = [];
            for (int i = 0; i < nbr_seg; i++)
            {
                delta_T = T1[i] - T0[i];
                DELTA_T[i] = delta_T;
                rap_Tc = (Tc[i] - T0[i]) / delta_T;
                RAP_Tc[i] =  rap_Tc;

                position_trait = 1;

                if ((i == 0) && (nbr_seg >= 2))
                        position_trait = 1;         // premier trait au début de la trajectoire
                else if((i == 1) && (nbr_seg >= 3))
                        position_trait = 2;         // trait au milieu de la trajectoire
                else if((i == 2) && (nbr_seg >= 6))
                        position_trait = 3;
                else if((i == 3) && (nbr_seg >= 8))
                        position_trait = 4;
                else if((i > 3) && (i < (nbr_seg - 4)) && (nbr_seg >= 9))
                        position_trait = 5;
                else if((i > 3) && (i == (nbr_seg - 4)))
                        position_trait = 6;
                else if((i > 2) && (i == (nbr_seg - 3)))
                        position_trait = 7;
                else if((i > 1) && (i == (nbr_seg - 2)))
                        position_trait = 8;
                else if((i > 0) && (i == nbr_seg - 1))
                        position_trait = 9;
                else if((i == 0) & (i == nbr_seg - 1))
                        position_trait = 0.1;

                P0SITION_TRAIT[i] = position_trait;
                K_i = fonctions_beta[i, 5];


            }

            A = new double[param_trajectoire.GetLength(0)];
            for (int i = 0; i < nbr_seg; i++)  // A = param_trajectoire(:, 1);
                A[i] = param_trajectoire[i, 0];

            B = new double[param_trajectoire.GetLength(0)];
            for (int i = 0; i < nbr_seg; i++)  // A = param_trajectoire(:, 1);
                B[i] = param_trajectoire[i, 1];
            
            // TETA_P = atan( tan( TETA_P ) );
            teta_deb_DAT = param_trajectoire[0, 2];
            teta_deb = Math.Atan(Math.Tan(teta_deb_DAT));

            TETA = new double[param_trajectoire.GetLength(0)];
            for (int i =0;i< param_trajectoire.GetLength(0); i++)  
            {
                TETA[i] = param_trajectoire[i, 2] - (teta_deb_DAT - teta_deb); // TETA = atan(tan(TETA)); 
                //TETA[i] = Math.Atan(Math.Tan(TETA[i]));
            }

            TETA_P = new double[param_trajectoire.GetLength(0)];
            for (int i = 0; i < param_trajectoire.GetLength(0); i++) // TETA_P = param_trajectoire(:, 4) - (teta_deb_DAT - teta_deb);
            {
                TETA_P[i] = param_trajectoire[i, 3] - (teta_deb_DAT - teta_deb);  // TETA_P = atan(tan(TETA_P));
                TETA_P[i] = Math.Atan(Math.Tan(TETA_P[i]));
            }

            X_deb_arc = new double[param_trajectoire.GetLength(0)];
            for (int i = 0; i < param_trajectoire.GetLength(0); i++) // X_deb_arc = param_trajectoire(:, 7);
                X_deb_arc[i] = param_trajectoire[i, 6];
            
            Y_deb_arc = new double[param_trajectoire.GetLength(0)];
            for (int i = 0; i < param_trajectoire.GetLength(0); i++) //Y_deb_arc = param_trajectoire(:, 8);
                Y_deb_arc[i] = param_trajectoire[i, 7];

            X_fin_arc = new double[param_trajectoire.GetLength(0)];
            for (int i = 0; i < param_trajectoire.GetLength(0); i++) //X_fin_arc = param_trajectoire(:, 9);
                X_fin_arc[i] = param_trajectoire[i, 8];

            Y_fin_arc = new double[param_trajectoire.GetLength(0)];
            for (int i = 0; i < param_trajectoire.GetLength(0); i++) //Y_fin_arc = param_trajectoire(:, 10);
                Y_fin_arc[i] = param_trajectoire[i, 9];

            
            for(int i=0; i<TETA.Length;i++)
            {
                x_centre = param_trajectoire[i, 4];
                y_centre = param_trajectoire[i, 5];
                x_pt_depart_arc = param_trajectoire[i, 6];
                y_pt_depart_arc = param_trajectoire[i, 7];
                x_pt_arrivee_arc = param_trajectoire[i, 8];
                y_pt_arrivee_arc = param_trajectoire[i, 9];
                dist_centre_pt_deb = Math.Sqrt(Math.Pow((x_pt_depart_arc - x_centre), 2) + Math.Pow((y_pt_depart_arc - y_centre), 2));
                dist_centre_pt_arr = Math.Sqrt(Math.Pow((x_pt_arrivee_arc - x_centre), 2) + Math.Pow((y_pt_arrivee_arc - y_centre), 2));
                teta_i = TETA[i];
                if (dist_centre_pt_deb > dist_centre_pt_arr)
                {
                    if (x_centre > x_pt_depart_arc)
                        TETA[i] = TETA[i];
                    else
                        TETA[i] = TETA[i] + Math.PI;
                }
                    
                else
                {
                    if (x_centre < x_pt_arrivee_arc)
                        TETA[i] = TETA[i];
                    else
                        TETA[i] = TETA[i] + Math.PI;
                }

                 if (TETA[i] > Math.PI)
                     TETA[i] = TETA[i] - (2 * Math.PI);
                 else if(TETA[i] < -Math.PI)
                  TETA[i] = TETA[i] + (2 * Math.PI);
               /* if (TETA[i] > (4 * Math.PI)/3)
                    TETA[i] = TETA[i] - (2 * Math.PI);
                else if (TETA[i] < - (4*Math.PI)/3)
                    TETA[i] = TETA[i] + (2 * Math.PI);
                */
            }

            ///////////////////////////////// CORRECTIF ANGLE TETA /////////////////////
            double valeur_PI = Math.PI;
            double valeur_PI_sur_2 = Math.PI/ 2;
            double fraction_PI = 0; // Math.PI / 9;
            if (autorisation_correctif_3 == 1)
            {
                for (int i = 0; i < TETA.Length; i++)
                {
                    X_deb_i = X_deb_arc[i];
                    Y_deb_i = Y_deb_arc[i];
                    X_fin_i = X_fin_arc[i];
                    Y_fin_i = Y_fin_arc[i];
                    teta_i = TETA[i];
                    if ((teta_i > 0) && (Math.Abs(teta_i - valeur_PI_sur_2) < fraction_PI))
                    {
                        if (Y_fin_i < Y_deb_i)
                            teta_i -= valeur_PI;

                    }
                    else if ((teta_i < 0) && (Math.Abs(teta_i + valeur_PI_sur_2) < fraction_PI))
                    {
                        if (Y_fin_i > Y_deb_i)
                            teta_i += valeur_PI;
                    }
                    else if ((teta_i > 0) && (Math.Abs(valeur_PI - teta_i) < fraction_PI))
                    {
                        if (X_fin_i > X_deb_i)
                            teta_i -= valeur_PI;
                    }
                    else if ((teta_i < 0) && (Math.Abs(valeur_PI + teta_i) < fraction_PI))
                    {
                        if (X_fin_i > X_deb_i)
                            //teta_i = Math.Abs(teta_i + valeur_PI);
                            teta_i += valeur_PI;
                    }
                    TETA[i] = teta_i;
                }
            }
            
            ////////////////////////////////////////////////////////////////////////////

            modele_sortie = new double[TETA.Length,12];
            for(int i=0; i< modele_sortie.GetLength(0); i++)  //  modele_sortie = [P, RAP_Tc, DELTA_T, K, A, B, TETA, X_deb_arc, Y_deb_arc, X_fin_arc, Y_fin_arc, P0SITION_TRAIT];
            {
                modele_sortie[i,0] = P[i];
                modele_sortie[i, 1] = RAP_Tc[i];
                modele_sortie[i, 2] = DELTA_T[i];
                modele_sortie[i, 3] = K[i];
                modele_sortie[i, 4] = A[i];
                modele_sortie[i, 5] = B[i];
                modele_sortie[i, 6] = TETA[i];
                modele_sortie[i, 7] = X_deb_arc[i];
                modele_sortie[i, 8] = Y_deb_arc[i];
                modele_sortie[i, 9] = X_fin_arc[i];
                modele_sortie[i, 10] = Y_fin_arc[i];
                modele_sortie[i, 11] = P0SITION_TRAIT[i];
            }
           
            //modele_sortie = real(modele_sortie);
            

            return modele_sortie;
        }
    }
}
