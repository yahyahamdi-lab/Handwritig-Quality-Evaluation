using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{
    class VS_new_beta_ellipse_stretegy_Beta_chevauchees_s_L
    {
        /*
          la fonction new_beta_ellipse_stretegy_Beta_chevauchees permet de décomposer la trajectoire enligne en segements séparés par une levée suivie d'une posée du                             %%%%%
          stylo. Chaque segment sera ensuite traité dans la phase de modélisation dans ses aspects : vitesse (dynamique)et trajectoire (statique)                 %%%%%
          Cette fonction permet d'adapter le niveau de sensibilité de détection des points de double inflexion de la vitesse suivant la précision de la  reconstrction elliptique. Elle permet aussi suivant le choix de l'utilisateur d'eliminer les traits excédentaires.                                                         %%%%%
          Entrées : signal en ligne, choix de l'utilisateur, Sorties : matrice de paramètres de modélisation, nombre de traits, Erreurs de reconstruction
         */

        int M, nbr_trait, n, num_fig,size,size_vect, size_param_trajectoire;
        double tf, pas, Rap_lim1, Rap_lim2,pas_t;
        public double[] T,TT, X, Y, RAP_lim,V,DPV,t,X_init,Y_init,T_init;
        public double[,] data_kk, data_pseudo_mot_t, matrice_param, points, fonctions_beta, tableau_affectation_intervalles, vect_T0_TC_T1_i, param_trajectoire, modele_sortie, vecteur_param,mat, mat1;
        VS_Pre_traitement_H_p pre_traitement;
        VS_Extraction_impulses_beta_chevauchees extraction_impulses;
        VS_construction_par_arc_ellipse_adapt_beta_chevauchees_1 construction_par_arc_ellipse;
        VS_collecte_donnees_Beta_chevauchees collecte_donnees;

        public double[,] Method_vecteur_param(double[,] data_k, int rayon_filtre_traject, double sigma_p_filtre_traject, int rayon_filtre_V, double sigma_p_filtre_V)
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
            for (double ih = 0; ih <= tf; ih = ih + 0.01) // t = 0 : pas: tf;
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
            V = pre_traitement.VS_Pre_traitement_H_p_V(X, Y, T, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);                                                                              // -------------------La dérivée première de la vitesse V

            DPV = pre_traitement.VS_Pre_traitement_H_p_DPV(X, Y, T, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
            TT = pre_traitement.VS_Pre_traitement_H_p_T(T);
            t = pre_traitement.VS_Pre_traitement_H_p_t(T);
            X_init = pre_traitement.VS_Pre_traitement_H_p_X_init(X, Y, T, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
            Y_init = pre_traitement.VS_Pre_traitement_H_p_Y_init(X, Y, T, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
            T_init = pre_traitement.VS_Pre_traitement_H_p_T_init(T);
            pas_t = pre_traitement.VS_Pre_traitement_H_p_pas_t(T);



            Rap_lim1 = 0.35;              // seuil de detection des points d'inflexion en montée 
            Rap_lim2 = 0.35;              // seuil de detection des points d'inflexion en descente
            RAP_lim[0] = Rap_lim1;
            RAP_lim[1] = Rap_lim2;
            extraction_impulses = new VS_Extraction_impulses_beta_chevauchees(); // VS_Extraction_impulses_beta_chevauchees(V, DPV, points, T, t, RAP_lim, pas_t);
            size = extraction_impulses.Method_size_vecteur(V, DPV, points, TT, t, RAP_lim, pas_t);
            size_vect = extraction_impulses.Method_size_vect_Tx0TxCTx1(V, DPV, points, TT, t, RAP_lim, pas_t);

            fonctions_beta = new double[size, 7];
            modele_sortie = new double[size, 12];
            matrice_param = new double[size, 12];
            vecteur_param = new double[size, 12];
            mat = new double[12, size];
            tableau_affectation_intervalles = new double[size, 8];
            vect_T0_TC_T1_i = new double[size_vect, 12];
            fonctions_beta = extraction_impulses.Method_fonctions_beta_sortie(V, DPV, points, TT, t, RAP_lim, pas_t);
           
            tableau_affectation_intervalles = extraction_impulses.Method_tableau_affectation_intervalles_sortie(V, DPV, points, TT, t, RAP_lim, pas_t);
            vect_T0_TC_T1_i = extraction_impulses.Method_vect_Tx0TxCTx1(V, DPV, points, TT, t, RAP_lim, pas_t);
            construction_par_arc_ellipse = new VS_construction_par_arc_ellipse_adapt_beta_chevauchees_1();
            num_fig = 4;
            size_param_trajectoire = construction_par_arc_ellipse.Methode_size_param_trajectoire(fonctions_beta, tableau_affectation_intervalles, vect_T0_TC_T1_i, points, TT, num_fig, V);
            param_trajectoire = new double[size_param_trajectoire, 11];
            param_trajectoire = construction_par_arc_ellipse.Method_param_trajectoire(fonctions_beta, tableau_affectation_intervalles, vect_T0_TC_T1_i, points, TT, num_fig, V);
            //VS_construction_par_arc_ellipse_adapt_beta_chevauchees_1(fonctions_beta, tableau_affectation_intervalles, vect_T0_TC_T1_i, points, TT, num_fig, V);


            collecte_donnees = new VS_collecte_donnees_Beta_chevauchees(); //[modele_sortie] = VS_collecte_donnees_Beta_chevauchees(fonctions_beta, param_trajectoire);
            modele_sortie = collecte_donnees.Method_VS_collecte_donnees_Beta_chevauchees(fonctions_beta, param_trajectoire);

            matrice_param = modele_sortie;
            vecteur_param = modele_sortie;
            nbr_trait = matrice_param.GetLength(0);

            mat = vecteur_param;
            mat1 = new double[mat.GetLength(1), mat.GetLength(0)];
            mat1 = Transpose(mat);
            

            return mat;
        }


        public int Method_ligne_vecteur_param(double[,] data_k, int rayon_filtre_traject, double sigma_p_filtre_traject, int rayon_filtre_V, double sigma_p_filtre_V)
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
            for (double ih = 0; ih <= tf; ih = ih + 0.01) // t = 0 : pas: tf;
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
           // V = new double[X.Length ]; 
          //  DPV = new double[X.Length];
            //TT = new double[X.Length];
           // t = new double[X.Length];
           // X_init = new double[X.Length];
           // Y_init = new double[X.Length];
            //T_init = new double[X.Length];
           
            points = pre_traitement.VS_Pre_traitement_H_p_points(X, Y, T, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
            V = pre_traitement.VS_Pre_traitement_H_p_V(X, Y, T, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);                                                                              // -------------------La dérivée première de la vitesse V

            
           
            DPV = pre_traitement.VS_Pre_traitement_H_p_DPV(X, Y, T, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
            TT = pre_traitement.VS_Pre_traitement_H_p_T(T);
            t = pre_traitement.VS_Pre_traitement_H_p_t(T);
            X_init = pre_traitement.VS_Pre_traitement_H_p_X_init(X, Y, T, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
            Y_init = pre_traitement.VS_Pre_traitement_H_p_Y_init(X, Y, T, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
            T_init = pre_traitement.VS_Pre_traitement_H_p_T_init(T);
            pas_t = pre_traitement.VS_Pre_traitement_H_p_pas_t(T);

            Rap_lim1 = 0.35;              // seuil de detection des points d'inflexion en montée 
            Rap_lim2 = 0.35;              // seuil de detection des points d'inflexion en descente
            RAP_lim[0] = Rap_lim1;
            RAP_lim[1] = Rap_lim2;
            extraction_impulses = new VS_Extraction_impulses_beta_chevauchees(); // VS_Extraction_impulses_beta_chevauchees(V, DPV, points, T, t, RAP_lim, pas_t);
            size = extraction_impulses.Method_size_vecteur(V, DPV, points, TT, t, RAP_lim, pas_t);
            size_vect = extraction_impulses.Method_size_vect_Tx0TxCTx1(V, DPV, points, TT, t, RAP_lim, pas_t);

          
            //fonctions_beta = new double[7, size];  //[size, 7];
            //modele_sortie = new double[12, size];  // [size, 12];
            //matrice_param = new double[12, size];
            // vecteur_param = new double[12, size];
            mat = new double[12, size];
            tableau_affectation_intervalles = new double[8, size];
            vect_T0_TC_T1_i = new double[size_vect, 12];
            fonctions_beta = extraction_impulses.Method_fonctions_beta_sortie(V, DPV, points, TT, t, RAP_lim, pas_t);

            tableau_affectation_intervalles = extraction_impulses.Method_tableau_affectation_intervalles_sortie(V, DPV, points, TT, t, RAP_lim, pas_t);
            vect_T0_TC_T1_i = extraction_impulses.Method_vect_Tx0TxCTx1(V, DPV, points, TT, t, RAP_lim, pas_t);
            construction_par_arc_ellipse = new VS_construction_par_arc_ellipse_adapt_beta_chevauchees_1();
            num_fig = 4;
            size_param_trajectoire = construction_par_arc_ellipse.Methode_size_param_trajectoire(fonctions_beta, tableau_affectation_intervalles, vect_T0_TC_T1_i, points, TT, num_fig, V);
            param_trajectoire = new double[size_param_trajectoire, 11];
            param_trajectoire = construction_par_arc_ellipse.Method_param_trajectoire(fonctions_beta, tableau_affectation_intervalles, vect_T0_TC_T1_i, points, TT, num_fig, V);
            //VS_construction_par_arc_ellipse_adapt_beta_chevauchees_1(fonctions_beta, tableau_affectation_intervalles, vect_T0_TC_T1_i, points, TT, num_fig, V);

            collecte_donnees = new VS_collecte_donnees_Beta_chevauchees(); //[modele_sortie] = VS_collecte_donnees_Beta_chevauchees(fonctions_beta, param_trajectoire);
            modele_sortie = collecte_donnees.Method_VS_collecte_donnees_Beta_chevauchees(fonctions_beta, param_trajectoire);
           
            matrice_param = modele_sortie;
             vecteur_param = modele_sortie;
             nbr_trait = matrice_param.GetLength(0);

             mat = vecteur_param;
             mat1 = new double[mat.GetLength(1), mat.GetLength(0)];
             mat1 = Transpose(mat);


            return mat1.GetLength(0);
        }




        public int Method_colonne_vecteur_param(double[,] data_k, int rayon_filtre_traject, double sigma_p_filtre_traject, int rayon_filtre_V, double sigma_p_filtre_V)
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
            for (double ih = 0; ih <= tf; ih = ih + 0.01) // t = 0 : pas: tf;
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
            V = pre_traitement.VS_Pre_traitement_H_p_V(X, Y, T, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);                                                                              // -------------------La dérivée première de la vitesse V

            DPV = pre_traitement.VS_Pre_traitement_H_p_DPV(X, Y, T, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
            TT = pre_traitement.VS_Pre_traitement_H_p_T(T);
            t = pre_traitement.VS_Pre_traitement_H_p_t(T);
            X_init = pre_traitement.VS_Pre_traitement_H_p_X_init(X, Y, T, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
            Y_init = pre_traitement.VS_Pre_traitement_H_p_Y_init(X, Y, T, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
            T_init = pre_traitement.VS_Pre_traitement_H_p_T_init(T);
            pas_t = pre_traitement.VS_Pre_traitement_H_p_pas_t(T);

         

            Rap_lim1 = 0.35;              // seuil de detection des points d'inflexion en montée 
            Rap_lim2 = 0.35;              // seuil de detection des points d'inflexion en descente
            RAP_lim[0] = Rap_lim1;
            RAP_lim[1] = Rap_lim2;
            extraction_impulses = new VS_Extraction_impulses_beta_chevauchees(); // VS_Extraction_impulses_beta_chevauchees(V, DPV, points, T, t, RAP_lim, pas_t);
            size = extraction_impulses.Method_size_vecteur(V, DPV, points, TT, t, RAP_lim, pas_t);
            size_vect = extraction_impulses.Method_size_vect_Tx0TxCTx1(V, DPV, points, TT, t, RAP_lim, pas_t);

            fonctions_beta = new double[7, size];  //[size, 7];
            modele_sortie = new double[12, size];  // [size, 12];
            matrice_param = new double[12, size];
            vecteur_param = new double[12, size];
            mat = new double[12, size];
            tableau_affectation_intervalles = new double[ 8,size];
            vect_T0_TC_T1_i = new double[size_vect, 12];
            fonctions_beta = extraction_impulses.Method_fonctions_beta_sortie(V, DPV, points, TT, t, RAP_lim, pas_t);

            tableau_affectation_intervalles = extraction_impulses.Method_tableau_affectation_intervalles_sortie(V, DPV, points, TT, t, RAP_lim, pas_t);
            vect_T0_TC_T1_i = extraction_impulses.Method_vect_Tx0TxCTx1(V, DPV, points, TT, t, RAP_lim, pas_t);
            construction_par_arc_ellipse = new VS_construction_par_arc_ellipse_adapt_beta_chevauchees_1();
            num_fig = 4;
            size_param_trajectoire = construction_par_arc_ellipse.Methode_size_param_trajectoire(fonctions_beta, tableau_affectation_intervalles, vect_T0_TC_T1_i, points, TT, num_fig, V);
            //param_trajectoire = new double[size_param_trajectoire, 11];
            param_trajectoire = construction_par_arc_ellipse.Method_param_trajectoire(fonctions_beta, tableau_affectation_intervalles, vect_T0_TC_T1_i, points, TT, num_fig, V);
            //VS_construction_par_arc_ellipse_adapt_beta_chevauchees_1(fonctions_beta, tableau_affectation_intervalles, vect_T0_TC_T1_i, points, TT, num_fig, V);




            collecte_donnees = new VS_collecte_donnees_Beta_chevauchees(); //[modele_sortie] = VS_collecte_donnees_Beta_chevauchees(fonctions_beta, param_trajectoire);
            modele_sortie = collecte_donnees.Method_VS_collecte_donnees_Beta_chevauchees(fonctions_beta, param_trajectoire);

            matrice_param = modele_sortie;
            vecteur_param = modele_sortie;
            nbr_trait = matrice_param.GetLength(0);

            mat = vecteur_param;
            mat1 = new double[mat.GetLength(1), mat.GetLength(0)];
            mat1 = Transpose(mat);


            return mat1.GetLength(1);
        }

        //
        public double[,] Transpose(double[,] matrix)
        {
            int w = matrix.GetLength(0);
            int h = matrix.GetLength(1);

            double[,] result = new double[h, w];

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    result[j, i] = matrix[i, j];
                }
            }

            return result;
        }








        // matrice param
        public double[,] Method_matrice_param(double[,] data_k, int rayon_filtre_traject, double sigma_p_filtre_traject, int rayon_filtre_V, double sigma_p_filtre_V)
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
            for (double ih = 0; ih <= tf; ih = ih + 0.01) // t = 0 : pas: tf;
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
            V = pre_traitement.VS_Pre_traitement_H_p_V(X, Y, T, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);                                                                              // -------------------La dérivée première de la vitesse V

            DPV = pre_traitement.VS_Pre_traitement_H_p_DPV(X, Y, T, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
            TT = pre_traitement.VS_Pre_traitement_H_p_T(T);
            t = pre_traitement.VS_Pre_traitement_H_p_t(T);
            X_init = pre_traitement.VS_Pre_traitement_H_p_X_init(X, Y, T, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
            Y_init = pre_traitement.VS_Pre_traitement_H_p_Y_init(X, Y, T, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
            T_init = pre_traitement.VS_Pre_traitement_H_p_T_init(T);
            pas_t = pre_traitement.VS_Pre_traitement_H_p_pas_t(T);



            Rap_lim1 = 0.35;              // seuil de detection des points d'inflexion en montée 
            Rap_lim2 = 0.35;              // seuil de detection des points d'inflexion en descente
            RAP_lim[0] = Rap_lim1;
            RAP_lim[1] = Rap_lim2;
            extraction_impulses = new VS_Extraction_impulses_beta_chevauchees(); // VS_Extraction_impulses_beta_chevauchees(V, DPV, points, T, t, RAP_lim, pas_t);
            

            size = extraction_impulses.Method_size_vecteur(V, DPV, points, TT, t, RAP_lim, pas_t);
            size_vect = extraction_impulses.Method_size_vect_Tx0TxCTx1(V, DPV, points, TT, t, RAP_lim, pas_t);

            fonctions_beta = new double[7, size];  //[size, 7];
            //modele_sortie = new double[12, size];  // [size, 12];
            //matrice_param = new double[12, size];
           // vecteur_param = new double[12, size];
            //mat = new double[12,size];
            tableau_affectation_intervalles = new double[ 8, size];
            vect_T0_TC_T1_i = new double[size_vect, 12];
            fonctions_beta = extraction_impulses.Method_fonctions_beta_sortie(V, DPV, points, TT, t, RAP_lim, pas_t);
 
            tableau_affectation_intervalles = extraction_impulses.Method_tableau_affectation_intervalles_sortie(V, DPV, points, TT, t, RAP_lim, pas_t);
            vect_T0_TC_T1_i = extraction_impulses.Method_vect_Tx0TxCTx1(V, DPV, points, TT, t, RAP_lim, pas_t);
            construction_par_arc_ellipse = new VS_construction_par_arc_ellipse_adapt_beta_chevauchees_1();
            num_fig = 4;
            size_param_trajectoire = construction_par_arc_ellipse.Methode_size_param_trajectoire(fonctions_beta, tableau_affectation_intervalles, vect_T0_TC_T1_i, points, TT, num_fig, V);
            //param_trajectoire = new double[size_param_trajectoire, 11];
            param_trajectoire = construction_par_arc_ellipse.Method_param_trajectoire(fonctions_beta, tableau_affectation_intervalles, vect_T0_TC_T1_i, points, TT, num_fig, V);
            //VS_construction_par_arc_ellipse_adapt_beta_chevauchees_1(fonctions_beta, tableau_affectation_intervalles, vect_T0_TC_T1_i, points, TT, num_fig, V);
          
            
            collecte_donnees = new VS_collecte_donnees_Beta_chevauchees(); //[modele_sortie] = VS_collecte_donnees_Beta_chevauchees(fonctions_beta, param_trajectoire);
            modele_sortie = collecte_donnees.Method_VS_collecte_donnees_Beta_chevauchees(fonctions_beta, param_trajectoire);
             
           // matrice_param = modele_sortie;
             matrice_param =  Transpose(modele_sortie);

            
            return matrice_param;
        }



        public double[,] Method_points(double[,] data_k, int rayon_filtre_traject, double sigma_p_filtre_traject, int rayon_filtre_V, double sigma_p_filtre_V)
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
            for (double ih = 0; ih <= tf; ih = ih + 0.01) // t = 0 : pas: tf;
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

           
            return points;
        }




        public int Method_nbr_trait(double[,] data_k, int rayon_filtre_traject, double sigma_p_filtre_traject, int rayon_filtre_V, double sigma_p_filtre_V)
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
            for (double ih = 0; ih <= tf; ih = ih + 0.01) // t = 0 : pas: tf;
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
            V = pre_traitement.VS_Pre_traitement_H_p_V(X, Y, T, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
            DPV = pre_traitement.VS_Pre_traitement_H_p_DPV(X, Y, T, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
            TT = pre_traitement.VS_Pre_traitement_H_p_T(T);
            t = pre_traitement.VS_Pre_traitement_H_p_t(T);
            X_init = pre_traitement.VS_Pre_traitement_H_p_X_init(X, Y, T, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
            Y_init = pre_traitement.VS_Pre_traitement_H_p_Y_init(X, Y, T, rayon_filtre_traject, sigma_p_filtre_traject, rayon_filtre_V, sigma_p_filtre_V);
            T_init = pre_traitement.VS_Pre_traitement_H_p_T_init(T);
            pas_t = pre_traitement.VS_Pre_traitement_H_p_pas_t(T);


            Rap_lim1 = 0.35;              // seuil de detection des points d'inflexion en montée 
            Rap_lim2 = 0.35;              // seuil de detection des points d'inflexion en descente
            RAP_lim[0] = Rap_lim1;
            RAP_lim[1] = Rap_lim2;
            extraction_impulses = new VS_Extraction_impulses_beta_chevauchees(); // VS_Extraction_impulses_beta_chevauchees(V, DPV, points, T, t, RAP_lim, pas_t);
            size = extraction_impulses.Method_size_vecteur(V, DPV, points, TT, t, RAP_lim, pas_t);
            size_vect = extraction_impulses.Method_size_vect_Tx0TxCTx1(V, DPV, points, TT, t, RAP_lim, pas_t);
            fonctions_beta = new double[size, 7];
            modele_sortie = new double[size, 12];
            matrice_param = new double[size, 12];
            vecteur_param = new double[size, 12];
            mat = new double[size, 12];
            tableau_affectation_intervalles = new double[size, 8];
            vect_T0_TC_T1_i = new double[size_vect, 12];
            fonctions_beta = extraction_impulses.Method_fonctions_beta_sortie(V, DPV, points, TT, t, RAP_lim, pas_t);
            tableau_affectation_intervalles = extraction_impulses.Method_tableau_affectation_intervalles_sortie(V, DPV, points, TT, t, RAP_lim, pas_t);
            vect_T0_TC_T1_i = extraction_impulses.Method_vect_Tx0TxCTx1(V, DPV, points, TT, t, RAP_lim, pas_t);

            num_fig = 4;
            size_param_trajectoire = construction_par_arc_ellipse.Methode_size_param_trajectoire(fonctions_beta, tableau_affectation_intervalles, vect_T0_TC_T1_i, points, TT, num_fig, V);
            param_trajectoire = new double[size_param_trajectoire, 11];
            param_trajectoire = construction_par_arc_ellipse.Method_param_trajectoire(fonctions_beta, tableau_affectation_intervalles, vect_T0_TC_T1_i, points, TT, num_fig, V);
            //VS_construction_par_arc_ellipse_adapt_beta_chevauchees_1(fonctions_beta, tableau_affectation_intervalles, vect_T0_TC_T1_i, points, T, num_fig, V);

            collecte_donnees = new VS_collecte_donnees_Beta_chevauchees(); //[modele_sortie] = VS_collecte_donnees_Beta_chevauchees(fonctions_beta, param_trajectoire);
            modele_sortie = collecte_donnees.Method_VS_collecte_donnees_Beta_chevauchees(fonctions_beta, param_trajectoire);

            matrice_param = modele_sortie;
            vecteur_param = modele_sortie;
            nbr_trait = matrice_param.GetLength(0);


            return nbr_trait;


        }
       
    }
 }

