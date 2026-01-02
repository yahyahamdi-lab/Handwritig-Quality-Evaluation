using Beta_elliptic_model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaligo_Beta_elliptic
{
    public class VS_reechantionnage_temporel_p
    {
        public int e_indice = 1;
        public double maxi_x, maxi_y, mini_x, mini_y, dimension_Max, pas_echatillonnage_temps;
        public double[] Dernier_point_courant, tab1, tab2, Vitesse_loi_2_tiers, DAT, memDAT1, Points_reordonnees, V_r, VX_r, VY_r;
        public double[,] data_k_filtree, Distance_Entre_Deux_Pts_Pixels;
        public int[,] Limites_Intervalles_Reechantillonnage;
        double[,] points;
        public double X_point_i, Y_point_i, V_loi_2sur3_point_courant, distance_point_courant_point_indice_i;
        public int k1, k2, indice_debut_intervalle, temps_H, indice_fin_intervalle, nbr_intervalles_Reechantillonnage, indice_fin_dernier_int;
        public int rayon_filtre_traject;
        public double x_depart_distance_a_completer, y_depart_distance_a_completer;
        public double facteur_diminution_amplitude_vitesse, facteur_accord, distance_moyenne_entre_deux_pts_pixels;
        public double sigma_p_filtre_traject, distance_entre_deux_pts_pixels, vitesse_moyenne_loi_2_tiers;
        public int coeff_HB, nombre_points_depasses;
        public double X_courant, Y_courant, delta_longueur;
        public int[] Indices_Points_Depasses;
        public double enregistreur_distance, enregistreur_distance_precedente, distance_a_completer, Xi_Xi_1, Yi_Yi_1;
        public int  indice_debut_int,indice_fin_int, iiiii, indice_dernier_pt_proche_du_pt_courant_preced, indice_dernier_pt_proche_du_pt_courant;
        VS_Direction_Angulaire_continue_de_Tangente_DAT_Abd_ALKARIM Direction;
        VS_filtre_lineaire filtre1 = new VS_filtre_lineaire();
        public double distance_Mi_Mi_1, a, X_nouveau_pt_courant, Y_nouveau_pt_courant, distance_pt_courant_pt_indice_i,
             X_point_i_1, Y_point_i_1,distance_pt_courant_pt_indice_i_1;
        public double[,] reechantionnage_temporel_p(double[,] points_equidistants, int rayon, double sigma_p)
        {

            tab1 = new double[points_equidistants.GetLength(0)];
            tab2 = new double[points_equidistants.GetLength(0)];
            for (int i = 0; i < points_equidistants.GetLength(0); i++)
            { tab1[i] = points_equidistants[i, 0]; }
            for (int j = 0; j < points_equidistants.GetLength(0); j++)
            { tab2[j] = points_equidistants[j, 1]; }
            maxi_x = max_matrice(tab1);//maxi_x est le maximum des abscisses
            maxi_y = max_matrice(tab2);       // maxi_y est le maximum des ordonnées
            mini_x = min_matrice(tab1);      //mini_x est le minimum des abscisses
            mini_y = min_matrice(tab2);       // mini_y est le minimum des ordonnées
            dimension_Max = Math.Max(maxi_x - mini_x, maxi_y - mini_y);

            points = new double[points_equidistants.GetLength(0), points_equidistants.GetLength(1)];
            for (int ijk = 0; ijk < points_equidistants.GetLength(0); ijk++)
            {
                for (int k = 0; k < points_equidistants.GetLength(1); k++)
                    points[ijk, k] = points_equidistants[ijk, k];
            }
            pas_echatillonnage_temps = 0.01;

            k1 = 1;
            k2 = points_equidistants.GetLength(0);
            for (int i = 0; i < 6; i++)
            {
                if (((rayon_filtre_traject * 2) + 1) > k2)

                { rayon_filtre_traject = k2; }

                VS_filtre_lineaire_1 filtre = new VS_filtre_lineaire_1();
                filtre.Method_VS_filtre_lineaire_1_points(rayon_filtre_traject, sigma_p_filtre_traject, 1, points.GetLength(0), points);
            }

            data_k_filtree = new double[points_equidistants.GetLength(0), points_equidistants.GetLength(1)];
            for (int ijk = 0; ijk < points_equidistants.GetLength(0); ijk++)
            {
                for (int k = 0; k < points_equidistants.GetLength(1); k++)
                    data_k_filtree[ijk, k] = points[ijk, k];
            }
            //
            //[DAT, memDAT, type_point, Ttemps] = Direction_Angulaire_continue_de_la_Tangente_DAT_Abd_AL_KARIM(k1, k2, points);

            Direction = new VS_Direction_Angulaire_continue_de_Tangente_DAT_Abd_ALKARIM();
            DAT = Direction.MethodsDat(k1, k2, points);
            memDAT1 = new double[DAT.GetLength(0)];
            for (int jjj = 0; jjj < DAT.GetLength(0); jjj++)
            {

                memDAT1[jjj] = DAT[jjj];
            }

            //memDAT1 = DAT;
            //[RAYON_COURBURE, Vitesse_loi_2_tiers] = Rayon_Courbure_et_Vitesse_loi_2_tiers_Abd_AL_KARIM_alpha(DAT, memDAT, points, Ttemps);
            rayon = 5;
            sigma_p = 3.5;


            Vitesse_loi_2_tiers = filtre1.method_VS_filtre_lineaire(rayon, sigma_p, Vitesse_loi_2_tiers);

            indice_debut_intervalle = k1 + 2;
            indice_fin_intervalle = k2 - 2;
            // Limites_Intervalles_Reechantillonnage = [indice_debut_intervalle, indice_fin_intervalle];
            nbr_intervalles_Reechantillonnage = Limites_Intervalles_Reechantillonnage.GetLength(0);


            temps_H = 0;
            indice_fin_dernier_int = 0;
            pas_echatillonnage_temps = 0.01;
            int vitesse_moyenne_main = 500;
            int hauteur_norm = 128;
            int distance_totale_optimale_en_hauteur = 100;
            int nbr_points_optimale_en_hauteur = 35;
            int nbr_points_init = points.GetLength(0);
            for (int iiiii = 2; iiiii < points.GetLength(0); iiiii++)
            {
                distance_entre_deux_pts_pixels = Math.Sqrt(Math.Pow((points[iiiii, 1] - points[iiiii - 1, 1]), 2) + (Math.Pow((points[iiiii, 2] - points[iiiii - 1, 2]), 2)));
                //Distance_Entre_Deux_Pts_Pixels = [Distance_Entre_Deux_Pts_Pixels, distance_entre_deux_pts_pixels];
            }
            double distance_totale = 0;
            for (int ii = 2; ii < Distance_Entre_Deux_Pts_Pixels.GetLength(0); ii++)
            {
                for (int ij = 2; ij < Distance_Entre_Deux_Pts_Pixels.GetLength(1); ij++)

                    distance_totale = distance_totale + Distance_Entre_Deux_Pts_Pixels[ii, ij];
            }
            if (distance_totale > (hauteur_norm / 5))
            {
                vitesse_moyenne_loi_2_tiers = moyenne_tableau(Vitesse_loi_2_tiers); // max(Vitesse_loi_2_tiers) / 2;
                distance_moyenne_entre_deux_pts_pixels = distance_totale_optimale_en_hauteur / nbr_points_optimale_en_hauteur;
                coeff_HB = 1; //vous pouvez si necessaire faire un reglage ici sur la valeur de coeff_HB
                facteur_accord = distance_moyenne_entre_deux_pts_pixels * (1 / (pas_echatillonnage_temps * vitesse_moyenne_loi_2_tiers)) * coeff_HB;
            }
            else if ((distance_totale < (hauteur_norm / 5)) && (distance_totale >= (hauteur_norm / 10)))
            { facteur_diminution_amplitude_vitesse = 4 * (distance_totale / hauteur_norm);
                //Vitesse_loi_2_tiers = Vitesse_loi_2_tiers * facteur_diminution_amplitude_vitesse;
                for (int jk = 0; jk < Vitesse_loi_2_tiers.GetLength(0); jk++)
                {

                    Vitesse_loi_2_tiers[jk] = Vitesse_loi_2_tiers[jk] * facteur_diminution_amplitude_vitesse; ;
                }
                vitesse_moyenne_loi_2_tiers = moyenne_tableau(Vitesse_loi_2_tiers); //max(Vitesse_loi_2_tiers) / 2;
                distance_moyenne_entre_deux_pts_pixels = (distance_totale_optimale_en_hauteur / nbr_points_optimale_en_hauteur) * facteur_diminution_amplitude_vitesse;
                coeff_HB = 1; //vous pouvez si necessaire faire un reglage ici sur la valeur de coeff_HB
                facteur_accord = distance_moyenne_entre_deux_pts_pixels * (1 / (pas_echatillonnage_temps * vitesse_moyenne_loi_2_tiers)) * coeff_HB;

            }
            else
            { facteur_diminution_amplitude_vitesse = (distance_totale / hauteur_norm);
                // Vitesse_loi_2_tiers = Vitesse_loi_2_tiers * facteur_diminution_amplitude_vitesse;
                for (int jk = 0; jk < Vitesse_loi_2_tiers.GetLength(0); jk++)
                {

                    Vitesse_loi_2_tiers[jk] = Vitesse_loi_2_tiers[jk] * facteur_diminution_amplitude_vitesse; ;
                }
                vitesse_moyenne_loi_2_tiers = moyenne_tableau(Vitesse_loi_2_tiers); // max(Vitesse_loi_2_tiers) / 2;
                distance_moyenne_entre_deux_pts_pixels = (distance_totale_optimale_en_hauteur / nbr_points_optimale_en_hauteur) * facteur_diminution_amplitude_vitesse;
                coeff_HB = 1; //vous pouvez si necessaire faire un reglage ici sur la valeur de coeff_HB
                facteur_accord = distance_moyenne_entre_deux_pts_pixels * (1 / (pas_echatillonnage_temps * vitesse_moyenne_loi_2_tiers)) * coeff_HB;

            }
         for(int  j =0;j <nbr_intervalles_Reechantillonnage;j++)
         {
            indice_debut_int = Limites_Intervalles_Reechantillonnage[j, 0] - 1;
            indice_fin_int = Limites_Intervalles_Reechantillonnage[j, 1] + 1;
            iiiii = indice_debut_int;
           /*  for (int i = indice_fin_dernier_int + 1; i < indice_debut_int - 1; i++)
              {

                    //Nouveau_Points[i] =  points[i, : ];
                    //temps_H = temps_H + pas_echatillonnage_temps;
                    //Nouveau_Ttemps = [Nouveau_Ttemps; temps_H];
                }*/

              //Dernier_point_courant = points(indice_debut_int - 1, :);
              indice_dernier_pt_proche_du_pt_courant = indice_debut_int - 1;
              indice_dernier_pt_proche_du_pt_courant_preced = indice_dernier_pt_proche_du_pt_courant;

            int i = indice_dernier_pt_proche_du_pt_courant + 1;
        while (i <= indice_fin_int)
        {   V_loi_2sur3_point_courant = Vitesse_loi_2_tiers[indice_dernier_pt_proche_du_pt_courant];
               delta_longueur = V_loi_2sur3_point_courant * pas_echatillonnage_temps * (facteur_accord);

                X_courant = Dernier_point_courant[1];
                Y_courant = Dernier_point_courant[2];
                 X_point_i = points[i, 0];
                 Y_point_i = points[i, 1];

            if (delta_longueur == 0)
               delta_longueur = 0.01;
            

            distance_point_courant_point_indice_i = Math.Sqrt(Math.Pow((X_point_i - X_courant), 2) + (Math.Pow((Y_point_i - Y_courant) ,2)));
            enregistreur_distance = distance_point_courant_point_indice_i;
            enregistreur_distance_precedente = 0;

            //Indices_Points_Depasses = [];
            //Indices_Points_Depasses = [Indices_Points_Depasses; indice_dernier_pt_proche_du_pt_courant];


        while ((enregistreur_distance < delta_longueur) &(i <= indice_fin_int))
         {
            enregistreur_distance_precedente = enregistreur_distance;
            i = i + 1;
            X_point_i = points[i, 0];
            Y_point_i = points[i,1];
            distance_point_courant_point_indice_i = Math.Sqrt(Math.Pow((X_point_i - X_courant), 2) + Math.Pow((Y_point_i - Y_courant) , 2));
            enregistreur_distance = enregistreur_distance + Math.Sqrt(Math.Pow((X_point_i - points[i - 1, 1]), 2) + Math.Pow((Y_point_i - points[i - 1, 2]) , 2));
            //Indices_Points_Depasses = [Indices_Points_Depasses; i - 1];
            nombre_points_depasses = Indices_Points_Depasses.Length;
            delta_longueur = 0;
            for (int fh = 1; fh< nombre_points_depasses;fh++)
                {  int   indice_depasse_numero_fh = Indices_Points_Depasses[fh];
                   delta_longueur = delta_longueur + (Vitesse_loi_2_tiers[indice_depasse_numero_fh] * (facteur_accord) * (pas_echatillonnage_temps / nombre_points_depasses));
                }
           }

               distance_a_completer = delta_longueur - enregistreur_distance_precedente;
                    if (enregistreur_distance_precedente == 0)
                    {   x_depart_distance_a_completer = X_courant;
                        y_depart_distance_a_completer = Y_courant;
                    }
                    else
                    { x_depart_distance_a_completer = points[i - 1, 0];
                        y_depart_distance_a_completer = points[i - 1, 1];
                    }

            Xi_Xi_1 = points[i, 0] - x_depart_distance_a_completer;
            Yi_Yi_1 = points[i, 1] - y_depart_distance_a_completer;
            distance_Mi_Mi_1 = Math.Sqrt(Math.Pow(Xi_Xi_1 , 2) + Math.Pow(Yi_Yi_1 , 2));
            a = distance_a_completer / distance_Mi_Mi_1;

            X_nouveau_pt_courant = x_depart_distance_a_completer + (a * Xi_Xi_1);
            Y_nouveau_pt_courant = y_depart_distance_a_completer + (a * Yi_Yi_1);



      
           //Dernier_point_courant = [X_nouveau_pt_courant, Y_nouveau_pt_courant];
             X_courant = X_nouveau_pt_courant;
             Y_courant = Y_nouveau_pt_courant;

             distance_pt_courant_pt_indice_i = Math.Sqrt(Math.Pow((X_point_i - X_courant) , 2) + Math.Pow((X_point_i - X_courant) , 2));
             X_point_i_1 = points[i - 1, 0];
             Y_point_i_1 = points[i - 1, 1];
             distance_pt_courant_pt_indice_i_1 = Math.Sqrt(Math.Pow((X_point_i_1 - X_courant) , 2) + Math.Pow((X_point_i_1 - X_courant) ,2));

            if (distance_pt_courant_pt_indice_i <= distance_pt_courant_pt_indice_i_1)
            { indice_dernier_pt_proche_du_pt_courant = i; }
            else
             {   indice_dernier_pt_proche_du_pt_courant = i - 1;
             }

            indice_dernier_pt_proche_du_pt_courant_preced = indice_dernier_pt_proche_du_pt_courant;
            //Nouveau_Points = [Nouveau_Points; Dernier_point_courant];
            //temps_H = temps_H + pas_echatillonnage_temps;
           // Nouveau_Ttemps = [Nouveau_Ttemps; temps_H];

         }

            indice_fin_dernier_int = indice_fin_int;
       }

      for(int kk = indice_fin_dernier_int + 1; kk< points.GetLength(0);kk++)
               
      {     //  Nouveau_Points = [Nouveau_Points; points(i, : )];
           // temps_H = temps_H + pas_echatillonnage_temps;
            //Nouveau_Ttemps = [Nouveau_Ttemps; temps_H];
        }

           // Points_reordonnees = Nouveau_Points;
/////////////////////////////////////////
            /*dNouveau_Points = [Nouveau_Points(1,:); Nouveau_Points; Nouveau_Points(size(Nouveau_Points, 1), :)];
            Nouveau_Ttemps = [Nouveau_Ttemps(1); Nouveau_Ttemps + pas_echatillonnage_temps; Nouveau_Ttemps(length(Nouveau_Ttemps)) + (2 * pas_echatillonnage_temps)];
            Nouveau_Points = [Nouveau_Points(1,:); Nouveau_Points; Nouveau_Points(size(Nouveau_Points, 1), :)];
            Nouveau_Ttemps = [Nouveau_Ttemps(1); Nouveau_Ttemps + pas_echatillonnage_temps; Nouveau_Ttemps(length(Nouveau_Ttemps)) + (2 * pas_echatillonnage_temps)];
            Nouveau_Points = [Nouveau_Points(1,:); Nouveau_Points];
            Nouveau_Ttemps = [Nouveau_Ttemps(1); Nouveau_Ttemps + pas_echatillonnage_temps];
            Nouveau_Points = [Nouveau_Points(1,:); Nouveau_Points];
            Nouveau_Ttemps = [Nouveau_Ttemps(1); Nouveau_Ttemps + pas_echatillonnage_temps];

            */




            /////Callcul de vitesse curviline
            //VX_r = diff(Nouveau_Points(:, 1)) / pas_echatillonnage_temps;
            //VX_r =[VX_r; 0];
            //VY_r = diff(Nouveau_Points(:, 2)) / pas_echatillonnage_temps;
            //VY_r =[VY_r; 0];
            //N = 6; Fs = 100; Fc = 12; Flag = 0; beta = 6; ripple = 35;


            //[B] = firpb(N, Fs, Fc, Wind, Flag);
            // A = 1;

            rayon = 5;
            sigma_p = 1.8;

            if (((rayon * 2) + 1) > VX_r.Length)
            { rayon = VX_r.Length; }
            
             VX_r = filtre1.method_VS_filtre_lineaire(rayon, sigma_p, VX_r);
             VY_r = filtre1.method_VS_filtre_lineaire(rayon, sigma_p, VY_r);

            // V_r = sqrt(VX_r.^2+VY_r.^2);






            int max_Vsigma = 650;
            double max_V_r = max_matrice(V_r);
            double Gain_vitesse = max_Vsigma / max_V_r;
            for (int k = 0; k < V_r.Length; k++)
            {

                V_r[k] = V_r[k] * Gain_vitesse;
            }
            


            int M = Points_reordonnees.GetLength(0);
            int pression_baissee = 1;
            int pression_levee = 0;
            for (int i = 0; i<M ;i++)
            {
               // points_reechantillones = [points_reechantillones; Points_reordonnees(i,:), pression_baissee];
            }

            //points_reechantillones = [points_reechantillones(1, 1) points_reechantillones(1, 2) pression_levee; points_reechantillones; points_reechantillones(M, 1) points_reechantillones(M, 2) pression_levee];
            M = M + 2;


            return points_equidistants;
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
        public double moyenne_tableau(double[] matrice)
        {
            double acc = 0;
            for (int i = 0; i < matrice.Length; i++)
            {
                acc += matrice[i];
            }

            return acc / matrice.Length;
        }

    }
}
