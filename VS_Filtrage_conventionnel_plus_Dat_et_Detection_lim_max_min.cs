using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Beta_elliptic_model
{
    
    class VS_Filtrage_conventionnel_plus_Dat_et_Detection_lim_max_min
    {
        /*La fonction Filtrage_conventionnel_plus_Detection_lim_max_min permet de filtrer 
        et calculer l'angle d'inclinaison continu de la trajectoire d'une ecriture      
        manuscrite Arabe ou Latine, en-ligne ou off-ligne squeletisée */

        public int k1,k2;
        public double max_x_filtre, max_y_filtre, min_x_filtre, min_y_filtre;
        public double[,] points_filtre, max_min_x_y;
        public double[]  points_filtre_x, points_filtre_y, DAT;
        VS_filtre_lineaire_1 p;
        VS_Direction_Angulaire_continue_de_Tangente_DAT_Abd_ALKARIM Direction;
        public double[,] Method_VS_Filtrage_conventionnel_plus_Dat_et_Detection_lim_max_min_max_min_x_y(double[,] points, int rayon_filtre_traject, double sigma_p_filtre_traject)
        {

            k1 = 1;                  
            k2 = points.GetLength(0);
            points_filtre = new double[points.GetLength(0), points.GetLength(1)]; /////
            max_min_x_y = new double[1,4];
            points_filtre_x = new double[points.GetLength(0)];
            points_filtre_y = new double[points.GetLength(0)];
            p = new VS_filtre_lineaire_1();

            for (int i = 0; i < 7; i++)
            {
                if (((rayon_filtre_traject * 2) + 1) > k2)
                    rayon_filtre_traject = k2;
                //ggg = VS_filtre_lineaire_1(rayon_filtre_traject, sigma_p_filtre_traject, 1, size(points, 1), points);
               
                points =  p.Method_VS_filtre_lineaire_1_points(rayon_filtre_traject, sigma_p_filtre_traject,1,points.Length,points);
            }

            points_filtre = points;
            for (int i = 0; i < points_filtre.GetLength(0); i++)
                points_filtre_x[i] = points_filtre[i,0];
            for (int i = 0; i < points_filtre.GetLength(0); i++)
                points_filtre_y[i] = points_filtre[i, 1];

            max_x_filtre = max_matrice(points_filtre_x);//points_filtre.Max();
            min_x_filtre = min_matrice(points_filtre_x);
            max_y_filtre = max_matrice(points_filtre_y);
            min_y_filtre = min_matrice(points_filtre_y);
            max_min_x_y [0,0]= max_x_filtre;
            max_min_x_y[0,1] = min_x_filtre;
            max_min_x_y[0,2] = max_y_filtre;
            max_min_x_y[0,3] = min_y_filtre;
            //hhh = VS_Direction_Angulaire_continue_de_Tangente_DAT_Abd_ALKARIM(k1, k2, points);
            return max_min_x_y;
        }

        public double[,] Method_VS_Filtrage_conventionnel_plus_Dat_et_Detection_lim_max_min_points_filtre(double[,] points, int rayon_filtre_traject, double sigma_p_filtre_traject)
        {

            k1 = 1;                      
            k2 = points.Length/2;
            points_filtre = new double[points.GetLength(0), points.GetLength(1)]; /////
            p = new VS_filtre_lineaire_1();

            for (int i = 0; i < 7; i++)
            {
                if (((rayon_filtre_traject * 2) + 1) > k2)
                    rayon_filtre_traject = k2;
                //ggg = VS_filtre_lineaire_1(rayon_filtre_traject, sigma_p_filtre_traject, 1, size(points, 1), points);

                points = p.Method_VS_filtre_lineaire_1_points(rayon_filtre_traject, sigma_p_filtre_traject, 1, points.Length, points);
            }

            points_filtre = points;
            
            return points_filtre;
        }

        public double[] Method_VS_Filtrage_conventionnel_plus_Dat_et_Detection_lim_max_min_DAT(double[,] points, int rayon_filtre_traject, double sigma_p_filtre_traject)
        {

            k1 = 1;
            k2 = points.Length/2;

            DAT = new double[k2];
            p = new VS_filtre_lineaire_1();

            for (int i = 0; i < 7; i++)
            {
                if (((rayon_filtre_traject * 2) + 1) > k2)
                    rayon_filtre_traject = k2;
                //ggg = VS_filtre_lineaire_1(rayon_filtre_traject, sigma_p_filtre_traject, 1, size(points, 1), points);

                points = p.Method_VS_filtre_lineaire_1_points(rayon_filtre_traject, sigma_p_filtre_traject, 1, points.Length, points);
            }

            //[DAT, memDAT, type_point, Ttemps] = VS_Direction_Angulaire_continue_de_Tangente_DAT_Abd_ALKARIM(k1, k2, points);
            Direction = new VS_Direction_Angulaire_continue_de_Tangente_DAT_Abd_ALKARIM();
            DAT = Direction.MethodsDat(k1, k2, points);
            return DAT;
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
