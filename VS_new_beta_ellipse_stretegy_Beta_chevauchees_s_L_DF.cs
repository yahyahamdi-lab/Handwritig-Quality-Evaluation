using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{
    class VS_new_beta_ellipse_stretegy_Beta_chevauchees_s_L_DF
    {
        /*
          la fonction new_beta_ellipse_stretegy_Beta_chevauchees permet de décomposer la trajectoire enligne en segements séparés par une levée suivie d'une posée du                             %%%%%
          stylo. Chaque segment sera ensuite traité dans la phase de modélisation dans ses aspects : vitesse (dynamique)et trajectoire (statique)                 %%%%%
          Cette fonction permet d'adapter le niveau de sensibilité de détection des points de double inflexion de la vitesse suivant la précision de la  reconstrction elliptique. Elle permet aussi suivant le choix de l'utilisateur d'eliminer les traits excédentaires.                                                         %%%%%
          Entrées : signal en ligne, choix de l'utilisateur, Sorties : matrice de paramètres de modélisation, nombre de traits, Erreurs de reconstruction
         */

        int M, nbr_trait, n, num_fig, size, size_vect, size_param_trajectoire;
        double tf, pas, Rap_lim1, Rap_lim2, pas_t;
        public double[] T, TT, X, Y, RAP_lim, V, DPV, t, X_init, Y_init, T_init;
        public double[,] data_kk, data_pseudo_mot_t, matrice_param, points, fonctions_beta, tableau_affectation_intervalles, vect_T0_TC_T1_i, param_trajectoire, modele_sortie, vecteur_param, mat, mat1;
        VS_Pre_traitement_H_p pre_traitement;


        public double[,] Method_points_DF(double[,] data_k, int rayon_filtre_traject, double sigma_p_filtre_traject, int rayon_filtre_V, double sigma_p_filtre_V)
        {
            
            nbr_trait = 0;

            RAP_lim = new double[2];
            M = data_k.GetLength(0);
            T = new double[M];
            data_kk = new double[data_k.GetLength(0), data_k.GetLength(1) + 1];
            double s = 100;
            tf = (M - 1) / s;
            
            pas = 0.01;
            //T[0] = 0;
            int ii = 0;
            for (double ih = 0; ih <= tf; ih=ih+ 0.01) // t = 0 : pas: tf;
            {
                T[ii++] = ih;
                
            }
            
            //data_k2    
            for (int i = 0; i < M; i++)
            {
                data_kk[i, 0] = data_k[i, 0];        //data_k = [data_k, T];    ////// a verifier 
                data_kk[i, 1] = data_k[i, 1];
                data_kk[i, 2] = data_k[i, 2];
                data_kk[i, 3] = T[i];

            }
            

            data_pseudo_mot_t = new double[data_kk.GetLength(0), data_kk.GetLength(1) + 1];     //??
                                                                                                ///n = data_kk.Length / data_kk.Rank;
           
            points = new double[data_kk.GetLength(0), data_kk.GetLength(1) - 2];
            // size X,Y,T
            int size_X = 0;
            for (int i_th = 0; i_th < M; i_th++)
            {
                if (data_kk[i_th, 2] != 0)
                {
                    size_X++;
                }

            }
            X = new double[size_X];
            Y = new double[size_X];
            T = new double[size_X];
            int i_th_1 = 0;
            
            for (int i_th = 0; i_th < M; i_th++)
            {
                if (data_kk[i_th, 2] != 0)
                {
                    data_pseudo_mot_t[i_th, 0] = 1;   //data_pseudo_mot_t = [data_pseudo_mot_t; 1, data_k(i_th,:)];
                    data_pseudo_mot_t[i_th, 1] = data_kk[i_th, 0];
                    data_pseudo_mot_t[i_th, 2] = data_kk[i_th, 1];
                    data_pseudo_mot_t[i_th, 3] = data_kk[i_th, 2];


                }
                else
                {
                    // data_pseudo_mot_t = [data_pseudo_mot_t; 0, data_k(i_th,:)];
                    data_pseudo_mot_t[i_th, 0] = 0;
                    data_pseudo_mot_t[i_th, 1] = data_kk[i_th, 0];
                    data_pseudo_mot_t[i_th, 2] = data_kk[i_th, 1];
                    data_pseudo_mot_t[i_th, 3] = data_kk[i_th, 2];
                    ///
                }

            }
            for (int i_th = 0; i_th < M; i_th++)
            {
                if (data_kk[i_th, 2] != 0)
                {
                    X[i_th_1] = data_kk[i_th, 0];
                    Y[i_th_1] = data_kk[i_th, 1];
                    T[i_th_1] = data_kk[i_th, 3];
                    i_th_1++;
                }
            }
                

            pre_traitement = new VS_Pre_traitement_H_p();   // //VS_Pre_traitement_H_p(X, Y, T, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);


            points = pre_traitement.VS_Pre_traitement_H_p_points(X, Y, T, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);

            //for (int jk = 0; jk < points.GetLength(0); jk++)
            //System.Console.WriteLine(points[jk,0]);
            //System.Console.ReadKey();
            return points;
        }





    }
}

