using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{
    class VS_construction_par_arc_ellipse_adapt_beta_chevauchees_1
    {
        /*
         * % La fonction construction_par_quart_ellipse_adapte permet de modéliser la trajectoire           
           par des arcs d'ellipse. trois méthodes sont utilisés pour générer les arcs :                   
           quart d'ellipse, cercle en projection oblique et deux points deux tangentes.                   
           La fonction permet de calculer une variation continue de l'angle d'inclinaison                
           de la tangente au tracé DAT permettant de mieux caractériser la géométrie de la trajectoire.  
           Entrées: instants de segmentation temporelle (approche impuls. Bêta chevauchées), trajectoire  
           Sorties: paramètres elliptiques de modélisation de la trajectoire, Erreurs de reconstruction 
         */

        int nbr_intervalles, indice_point_fin,uu, L_DAT, autorisation_de_representation_graphique_de_l_arc;
        double maxi_x, maxi_y, mini_x, mini_y, dimension_Max, pas, V_M_dep, V_M_arr, teta_M1, teta_M2, KMH1, KMH2, XM1, XM2, teta , YM1, YM2, num_tan_teta_M2_M1, denum_tan_teta_M2_M1, teta_M2_M1, A1, B1, X10, Y10, Teta1, Teta_G, Teta_P, longueur_segment, grand_axe1, petit_axe1;
        public double indiceTemps_deb, indiceTemps_fin, k1, k2, indiceM1, indiceM2;
        double[] points_x,points_y,max_xy, DAT, memDAT, type_point, Ttemps, M_2points, M_2KMH, xy_centre, xy_M_deb_arc, xy_M_fin_arc;
        double[,] XYm1, XYm2, param_trajectoire;
        VS_Direction_Angulaire_continue_de_Tangente_DAT_Abd_ALKARIM direction_angulaire;
        VS_parametres_quart_ellipse quart_ellipse;
        public double[,] Method_param_trajectoire(double[,] fonctions_beta, double[,] tableau_affectation_intervalles, double[,] vect_T0_TC_T1, double[,] points, double[] T, int num_fig, double[] V_SIGMA)
        {
            points_x = new double[points.GetLength(0)];
            points_y = new double[points.GetLength(0)];
            for (int i = 0; i < points.GetLength(0); i++)
                points_x[i] = points[i, 0];
            for (int i = 0; i < points.GetLength(0); i++)
                points_y[i] = points[i, 1];
            max_xy = new double[2];
            maxi_x = max_matrice(points_x);   // maxi_x est le maximum des abscisses
            maxi_y = max_matrice(points_y);         // maxi_y est le maximum des ordonnées
            mini_x = min_matrice(points_x);   // mini_x est le minimum des abscisses
            mini_y = min_matrice(points_y);          //mini_y est le minimum des ordonnées
            max_xy[0] = maxi_x - mini_x;
            max_xy[1] = maxi_y - mini_y;
            dimension_Max = max_matrice(max_xy);
            pas = T[1] - T[0];

            nbr_intervalles = tableau_affectation_intervalles.GetLength(0);
            indiceTemps_deb = tableau_affectation_intervalles[0, 6];          // 1;
            indiceTemps_fin = tableau_affectation_intervalles[nbr_intervalles - 1, 7];
            k1 = indiceTemps_deb;
            k2 = indiceTemps_fin;
            
            direction_angulaire = new VS_Direction_Angulaire_continue_de_Tangente_DAT_Abd_ALKARIM();
            DAT = direction_angulaire.MethodsDat(k1-1, k2, points);
            //memDAT = direction_angulaire.Method_memDAT(k1, k2, points);
            //type_point = direction_angulaire.Method_type_point(k1, k2, points);
            //Ttemps = direction_angulaire.Method_Ttemps(k1, k2, points);   //  à verifier
            //  Angle d'inclinaison de la tangente au tracé, DAT.
            // [DAT, memDAT, type_point, Ttemps] = VS_Direction_Angulaire_continue_de_Tangente_DAT_Abd_ALKARIM(k1, k2, points);
           
            
            if (points.GetLength(0) > DAT.Length)
            {
                int length_DAT_H_A = points.GetLength(0);
                double[] DAT_H_A = new double[length_DAT_H_A];
                for (int indice_point = 0; indice_point < DAT.Length ; indice_point++)
                {
                    DAT_H_A[indice_point] = DAT[indice_point]; // = [DAT; 0];
                }
                indice_point_fin = points.GetLength(0) - 1;
                for (int indice_point = DAT.Length; indice_point <= indice_point_fin; indice_point++)
                {
                    DAT_H_A[indice_point] = 0; // = [DAT; 0];
                }
                DAT = new double[DAT_H_A.Length];
                for (int indice_point = 0; indice_point < DAT_H_A.Length; indice_point++)
                {
                    DAT[indice_point] = DAT_H_A[indice_point];
                }
            }

            ///////////////////////////////////////////////////////////////////////////////////////////

            // constitution des vecteurs caractéristiques des points extrémums   

            // constitution des vecteurs caractéristiques des points extrémums XYm1 et XYm2

            uu = 0;
            L_DAT = DAT.Length;
            XYm1 = new double[nbr_intervalles, 4];
            XYm2 = new double[nbr_intervalles, 4];
            param_trajectoire = new double[nbr_intervalles, 11];

            for (int i = 0; i < nbr_intervalles; i++)
            {
                V_M_dep = tableau_affectation_intervalles[i, 4];
                V_M_arr = tableau_affectation_intervalles[i, 5];

                if (V_M_dep <= V_M_arr)
                {
                    indiceM1 = tableau_affectation_intervalles[i, 6];
                    indiceM2 = tableau_affectation_intervalles[i, 7];
                }
                else
                {
                    indiceM1 = tableau_affectation_intervalles[i, 7];
                    indiceM2 = tableau_affectation_intervalles[i, 6];
                }
                
                teta_M1 = DAT[(int)(Math.Max((indiceM1 - indiceTemps_deb), 0))];  // indiceM1 - indiceTemps_deb + 1
               
                if (((indiceM2 - indiceTemps_deb  - uu) >= 0) && ((indiceM2 - indiceTemps_deb  + uu) < L_DAT))
                    teta_M2 = (DAT[(int)(indiceM2 - indiceTemps_deb + uu)] + DAT[(int)(indiceM2 - indiceTemps_deb  - uu)]) / 2;
                else
                    teta_M2 = DAT[(int)(indiceM2 - indiceTemps_deb )];


                XYm1[i, 0] = Math.Max((indiceM1 ), 0);           // XYm1 = [XYm1; indiceM1 points(indiceM1,1) points(indiceM1,2) teta_M1];
                XYm1[i, 1] = points[(int)Math.Max((indiceM1 - 1), 0), 0];
                XYm1[i, 2] = points[(int)Math.Max((indiceM1 - 1), 0), 1];
                XYm1[i, 3] = teta_M1;

                XYm2[i, 0] = Math.Min((Math.Max((indiceM2 - 1), 0)),L_DAT);           // XYm2 = [XYm2; indiceM2 points(indiceM2,1) points(indiceM2,2) teta_M2];
                XYm2[i, 1] = points[(int)Math.Min((Math.Max((indiceM2 - 1), 0)), L_DAT), 0]; //points[(int)indiceM2, 0];
                XYm2[i, 2] = points[(int)Math.Min((Math.Max((indiceM2 - 1), 0)),L_DAT), 1];
                XYm2[i, 3] = teta_M2;
                //temps_T1_T2 = [temps_T1_T2; tableau_affectation_intervalles(i,1), tableau_affectation_intervalles(i,2)];
               
            }
            
           // for (int h = 0; h < DAT.Length; h++)
                //System.Console.WriteLine(DAT[h]);
            xy_M_deb_arc = new double[2];
            xy_M_fin_arc = new double[2];
            M_2KMH = new double[2];
            M_2points = new double[4];
            xy_centre = new double[2];

            for (int i = 0; i < nbr_intervalles; i++)
            {
                KMH1 = XYm1[i, 0];
                KMH2 = XYm2[i, 0];

                //M_2KMH_HB = [KMH1, KMH2];

                //M_2Donnees = [XYm1(i,:), XYm2(i,:)];

                // Détermination des parmètres du modèle elliptique (de repérage) a, b, teta

                XM1 = XYm1[i, 1];
                YM1 = XYm1[i, 2]; 
                XM2 = XYm2[i, 1]; 
                YM2 = XYm2[i, 2];

                if (Math.Abs(KMH2 - KMH1) >= 1)
                {
                    M_2points[0] = XM2;
                    M_2points[1] = YM2;
                    M_2points[2] = XM1;
                    M_2points[3] = YM1;
                    M_2KMH[0] = KMH2;   //  M_2KMH = [KMH2, KMH1];
                    M_2KMH[0] = KMH1;

                    teta = XYm2[i, 3];
                    
                    // %%% cas particulier de points ou la direction de la tangente au point M2 = M_G est confondue avec la direction (M1 M2)
                    num_tan_teta_M2_M1 = (YM2 - YM1);
                    denum_tan_teta_M2_M1 = (XM2 - XM1);
                    if (denum_tan_teta_M2_M1 == 0)
                        denum_tan_teta_M2_M1 = 1.0e-10;
                    teta_M2_M1 = Math.Atan(num_tan_teta_M2_M1 / denum_tan_teta_M2_M1);
                    if (Math.Atan(Math.Tan(teta)) == teta_M2_M1)
                        teta += (Math.PI / 1000);
                    
                    autorisation_de_representation_graphique_de_l_arc = 0;
                    quart_ellipse = new VS_parametres_quart_ellipse();
                    A1 = quart_ellipse.Method_a(M_2points, M_2KMH, teta, num_fig, autorisation_de_representation_graphique_de_l_arc);
                    B1 = quart_ellipse.Method_b(M_2points, M_2KMH, teta, num_fig, autorisation_de_representation_graphique_de_l_arc);
                    X10 = quart_ellipse.Method_x0(M_2points, M_2KMH, teta, num_fig, autorisation_de_representation_graphique_de_l_arc);
                    Y10 = quart_ellipse.Method_y0(M_2points, M_2KMH, teta, num_fig, autorisation_de_representation_graphique_de_l_arc);
                    Teta1 = quart_ellipse.Method_teta(M_2points, M_2KMH, teta, num_fig, autorisation_de_representation_graphique_de_l_arc);
                    //        [A1,B1,X10,Y10,Teta1,Drap1,HA1] = VS_parametres_quart_ellipse(M_2points,M_2KMH,teta,num_fig, autorisation_de_representation_graphique_de_l_arc);
                    
                    Teta_G = Teta1;
                    if (teta == Teta1)
                        Teta_P = XYm1[i, 3];
                    else
                        Teta_P = XYm2[i, 3];
                    longueur_segment = Math.Sqrt(Math.Pow((XM2 - XM1), 2) + Math.Pow((YM2 - YM1), 2));
                }
                else
                {
                    A1 = 0; B1 = 0; X10 = XYm2[i, 1]; Y10 = XYm2[i, 2]; Teta1 = XYm2[i, 3]; teta = XYm2[i, 3];
                    Teta_P = XYm1[i, 3];

                    longueur_segment = 0.5;
                    Teta_P = XYm1[i, 3];
                }

                // Enregistrement des donnees geometriques
                grand_axe1 = A1;
                petit_axe1 = B1;
                xy_centre[0] = X10;
                xy_centre[1] = Y10;
                if (KMH1 <= KMH2)
                {
                    xy_M_deb_arc[0] = XM1;
                    xy_M_deb_arc[1] = YM1;
                    xy_M_fin_arc[0] = XM2;
                    xy_M_fin_arc[1] = YM2;

                }
                else
                {
                    xy_M_deb_arc[0] = XM2; // xy_M_fin_arc = [XM1, YM1];
                    xy_M_deb_arc[1] = YM2;
                    xy_M_fin_arc[0] = XM1;
                    xy_M_fin_arc[1] = YM1;

                }

                //     param_trajectoire = [param_trajectoire; grand_axe1, petit_axe1, Teta_G, Teta_P, xy_centre, xy_M_deb_arc, xy_M_fin_arc, longueur_segment];
                
                param_trajectoire[i, 0] = grand_axe1;
                param_trajectoire[i, 1] = petit_axe1;
                param_trajectoire[i, 2] = Teta_G;
                param_trajectoire[i, 3] = Teta_P;
                param_trajectoire[i, 4] = xy_centre[0];
                param_trajectoire[i, 5] = xy_centre[1];
                param_trajectoire[i, 6] = xy_M_deb_arc[0];
                param_trajectoire[i, 7] = xy_M_deb_arc[1];
                param_trajectoire[i, 8] = xy_M_fin_arc[0];
                param_trajectoire[i, 9] = xy_M_fin_arc[1];
                param_trajectoire[i, 10] = longueur_segment;
            }
            
            return param_trajectoire;
        }


        public int Methode_size_param_trajectoire(double[,] fonctions_beta, double[,] tableau_affectation_intervalles, double[,] vect_T0_TC_T1, double[,] points, double[] T, int num_fig, double[] V_SIGMA)
        {
            points_x = new double[points.GetLength(0)];
            points_y = new double[points.GetLength(0)];
            for (int i = 0; i < points.GetLength(0); i++)
                points_x[i] = points[i, 0];
            for (int i = 0; i < points.GetLength(0); i++)
                points_y[i] = points[i, 1];
            max_xy = new double[2];
            maxi_x = max_matrice(points_x);   // maxi_x est le maximum des abscisses
            maxi_y = max_matrice(points_y);         // maxi_y est le maximum des ordonnées
            mini_x = min_matrice(points_x);   // mini_x est le minimum des abscisses
            mini_y = min_matrice(points_y);          //mini_y est le minimum des ordonnées
            max_xy[0] = maxi_x - mini_x;
            max_xy[1] = maxi_y - mini_y;
            dimension_Max = max_matrice(max_xy);
            pas = T[1] - T[0];

            nbr_intervalles = tableau_affectation_intervalles.GetLength(0);
            indiceTemps_deb = tableau_affectation_intervalles[0, 6];          // 1;
            indiceTemps_fin = tableau_affectation_intervalles[nbr_intervalles - 1, 7];
            k1 = indiceTemps_deb;
            k2 = indiceTemps_fin;

            direction_angulaire = new VS_Direction_Angulaire_continue_de_Tangente_DAT_Abd_ALKARIM();
            DAT = direction_angulaire.MethodsDat(k1, k2, points);
            //memDAT = direction_angulaire.Method_memDAT(k1, k2, points);
            //type_point = direction_angulaire.Method_type_point(k1, k2, points);
            //Ttemps = direction_angulaire.Method_Ttemps(k1, k2, points);   //  à verifier
            //  Angle d'inclinaison de la tangente au tracé, DAT.
            // [DAT, memDAT, type_point, Ttemps] = VS_Direction_Angulaire_continue_de_Tangente_DAT_Abd_ALKARIM(k1, k2, points);

            if (points.GetLength(0) > DAT.Length)
            {
                int length_DAT_H_A = points.GetLength(0);
                double [] DAT_H_A = new double[length_DAT_H_A];
                for (int indice_point = 0; indice_point <= DAT.Length - 1 ; indice_point++)
                {
                    DAT_H_A[indice_point] = DAT[indice_point]; // = [DAT; 0];
                }
                indice_point_fin = points.GetLength(0) - 1 ;
                for (int indice_point = DAT.Length; indice_point <= indice_point_fin; indice_point++)
                {
                    DAT_H_A[indice_point] = 0; // = [DAT; 0];
                }
                DAT = new double[DAT_H_A.Length];
                for (int indice_point = 0; indice_point < DAT_H_A.Length; indice_point++)
                {
                    DAT[indice_point] = DAT_H_A[indice_point];
                }
            }
            
            ///////////////////////////////////////////////////////////////////////////////////////////

            // constitution des vecteurs caractéristiques des points extrémums   

            // constitution des vecteurs caractéristiques des points extrémums XYm1 et XYm2

            uu = 0;
            L_DAT = DAT.Length;
            XYm1 = new double[nbr_intervalles, 4];
            XYm2 = new double[nbr_intervalles, 4];
            param_trajectoire = new double[nbr_intervalles, 11];

            for (int i = 0; i < nbr_intervalles; i++)
            {
                V_M_dep = tableau_affectation_intervalles[i, 4];
                V_M_arr = tableau_affectation_intervalles[i, 5];

                if (V_M_dep <= V_M_arr)
                {
                    indiceM1 = tableau_affectation_intervalles[i, 6];
                    indiceM2 = tableau_affectation_intervalles[i, 7];
                }
                else
                {
                    indiceM1 = tableau_affectation_intervalles[i, 7];
                    indiceM2 = tableau_affectation_intervalles[i, 6];
                }
                teta_M1 = DAT[(int)(Math.Max((indiceM1 - indiceTemps_deb),0) )];  // indiceM1 - indiceTemps_deb + 1

                if (((indiceM2 - indiceTemps_deb  - uu) >= 0) && ((indiceM2 - indiceTemps_deb  + uu) < L_DAT))
                    teta_M2 = (DAT[(int)(indiceM2 - indiceTemps_deb  + uu)] + DAT[(int)(indiceM2 - indiceTemps_deb - uu)]) / 2;
                else
                    teta_M2 = DAT[(int)(indiceM2 - indiceTemps_deb )];


                XYm1[i, 0] = indiceM1;           // XYm1 = [XYm1; indiceM1 points(indiceM1,1) points(indiceM1,2) teta_M1];
                XYm1[i, 1] = points[(int)Math.Max((indiceM1 - 1),0), 0];
                XYm1[i, 2] = points[(int)Math.Max((indiceM1 - 1), 0), 1];
                XYm1[i, 3] = teta_M1;

                XYm2[i, 0] = indiceM2;           // XYm2 = [XYm2; indiceM2 points(indiceM2,1) points(indiceM2,2) teta_M2];
                XYm2[i, 1] = points[(int)indiceM2 - 1, 0]; //points[(int)indiceM2, 0];
                XYm2[i, 2] = points[(int)indiceM2 - 1, 1];
                XYm2[i, 3] = teta_M2;
                //temps_T1_T2 = [temps_T1_T2; tableau_affectation_intervalles(i,1), tableau_affectation_intervalles(i,2)];

            }
            // for (int h = 0; h < DAT.Length; h++)
            //System.Console.WriteLine(DAT[h]);
            xy_M_deb_arc = new double[2];
            xy_M_fin_arc = new double[2];
            M_2KMH = new double[2];
            M_2points = new double[4];
            xy_centre = new double[2];

            for (int i = 0; i < nbr_intervalles; i++)
            {
                KMH1 = XYm1[i, 0];
                KMH2 = XYm2[i, 0];

                //M_2KMH_HB = [KMH1, KMH2];

                //M_2Donnees = [XYm1(i,:), XYm2(i,:)];

                // Détermination des parmètres du modèle elliptique (de repérage) a, b, teta

                XM1 = XYm1[i, 1];
                YM1 = XYm1[i, 2];
                XM2 = XYm2[i, 1];
                YM2 = XYm2[i, 2];

                if (Math.Abs(KMH2 - KMH1) >= 1)
                {
                    M_2points[0] = XM2;
                    M_2points[1] = YM2;
                    M_2points[2] = XM1;
                    M_2points[3] = YM1;
                    M_2KMH[0] = KMH2;   //  M_2KMH = [KMH2, KMH1];
                    M_2KMH[0] = KMH1;

                    teta = XYm2[i, 3];

                    // %%% cas particulier de points ou la direction de la tangente au point M2 = M_G est confondue avec la direction (M1 M2)
                    num_tan_teta_M2_M1 = (YM2 - YM1);
                    denum_tan_teta_M2_M1 = (XM2 - XM1);
                    if (denum_tan_teta_M2_M1 == 0)
                        denum_tan_teta_M2_M1 = 1.0e-10;
                    teta_M2_M1 = Math.Atan(num_tan_teta_M2_M1 / denum_tan_teta_M2_M1);
                    if (Math.Atan(Math.Tan(teta)) == teta_M2_M1)
                        teta += (Math.PI / 1000);

                    autorisation_de_representation_graphique_de_l_arc = 0;
                    quart_ellipse = new VS_parametres_quart_ellipse();
                    A1 = quart_ellipse.Method_a(M_2points, M_2KMH, teta, num_fig, autorisation_de_representation_graphique_de_l_arc);
                    B1 = quart_ellipse.Method_b(M_2points, M_2KMH, teta, num_fig, autorisation_de_representation_graphique_de_l_arc);
                    X10 = quart_ellipse.Method_x0(M_2points, M_2KMH, teta, num_fig, autorisation_de_representation_graphique_de_l_arc);
                    Y10 = quart_ellipse.Method_y0(M_2points, M_2KMH, teta, num_fig, autorisation_de_representation_graphique_de_l_arc);
                    Teta1 = quart_ellipse.Method_teta(M_2points, M_2KMH, teta, num_fig, autorisation_de_representation_graphique_de_l_arc);
                    //        [A1,B1,X10,Y10,Teta1,Drap1,HA1] = VS_parametres_quart_ellipse(M_2points,M_2KMH,teta,num_fig, autorisation_de_representation_graphique_de_l_arc);

                    Teta_G = Teta1;
                    if (teta == Teta1)
                        Teta_P = XYm1[i, 3];
                    else
                        Teta_P = XYm2[i, 3];
                    longueur_segment = Math.Sqrt(Math.Pow((XM2 - XM1), 2) + Math.Pow((YM2 - YM1), 2));
                }
                else
                {
                    A1 = 0; B1 = 0; X10 = XYm2[i, 1]; Y10 = XYm2[i, 2]; Teta1 = XYm2[i, 3]; teta = XYm2[i, 3];
                    Teta_P = XYm1[i, 3];

                    longueur_segment = 0.5;
                    Teta_P = XYm1[i, 3];
                }

                // Enregistrement des donnees geometriques
                grand_axe1 = A1;
                petit_axe1 = B1;
                xy_centre[0] = X10;
                xy_centre[1] = Y10;
                if (KMH1 <= KMH2)
                {
                    xy_M_deb_arc[0] = XM1;
                    xy_M_deb_arc[1] = YM1;
                    xy_M_fin_arc[0] = XM2;
                    xy_M_fin_arc[1] = YM2;

                }
                else
                {
                    xy_M_deb_arc[0] = XM2; // xy_M_fin_arc = [XM1, YM1];
                    xy_M_deb_arc[1] = YM2;
                    xy_M_fin_arc[0] = XM1;
                    xy_M_fin_arc[1] = YM1;

                }

                //     param_trajectoire = [param_trajectoire; grand_axe1, petit_axe1, Teta_G, Teta_P, xy_centre, xy_M_deb_arc, xy_M_fin_arc, longueur_segment];

                param_trajectoire[i, 0] = grand_axe1;
                param_trajectoire[i, 1] = petit_axe1;
                param_trajectoire[i, 2] = Teta_G;
                param_trajectoire[i, 3] = Teta_P;
                param_trajectoire[i, 4] = xy_centre[0];
                param_trajectoire[i, 5] = xy_centre[1];
                param_trajectoire[i, 6] = xy_M_deb_arc[0];
                param_trajectoire[i, 7] = xy_M_deb_arc[1];
                param_trajectoire[i, 8] = xy_M_fin_arc[0];
                param_trajectoire[i, 9] = xy_M_fin_arc[1];
                param_trajectoire[i, 10] = longueur_segment;
            }


            return param_trajectoire.GetLength(0);
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

       

    }
}
