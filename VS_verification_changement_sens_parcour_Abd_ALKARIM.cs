using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{
    class VS_verification_changement_sens_parcour_Abd_ALKARIM
    {
        public int continuite, inversion,L, k_M1;
        public const int w = 3;
        public const int u = 2;
        public double denum,dat_k, datM1, X_M1, Y_M1, X_M2, Y_M2, X_M3, Y_M3, X_M4, Y_M4, long_segM1M2, long_segM1M3, long_segM1M4, alfat_M1M2, alfat_M1M3, alfat_M1M4, Ang_diff_M1M2, Ang_diff_M1M3, Ang_diff_M1M4, X_projete_M2, X_projete_M3, X_projete_M4;
        public int Method_continuite(int k_Mextremum, double[,] points)
        {
            continuite = 1;
            inversion = 0;
            L = points.Length/2;
            k_M1 = k_Mextremum - w;

            if ((k_M1 > 0) && (k_Mextremum + w - 1 < L))
            {
                if ((k_Mextremum - w) - u < 1)
                {
                    denum = points[1, 0] - points[0, 0];
                    if (denum == 0)
                        denum = 1.0e-8;
                    dat_k = Math.Atan((points[1, 1] - points[0, 1]) / denum);

                }

                else if ((k_Mextremum - w) + u > L)
                {
                    denum = points[L, 0] - points[(k_Mextremum - w) - u, 1];
                    if (denum == 0)
                        denum = 1.0e-8; ////
                    dat_k = Math.Atan((points[L, 1] - points[(k_Mextremum - w) - u, 1]) / denum);

                }
                else
                {
                    denum = (points[(k_Mextremum - w) + u, 0] - points[(k_Mextremum - w) - u, 0]);
                    if (denum == 0)
                        denum = 1.0e-8;
                    dat_k = Math.Atan((points[(k_Mextremum - w) + u, 1] - points[(k_Mextremum - w) - u, 1]) / denum);
                    if ((points[(k_Mextremum - w) + u, 0] - points[(k_Mextremum - w) - u, 0]) < 0)
                        dat_k = dat_k + Math.PI;

                }
                datM1 = dat_k;

                X_M1 = points[k_Mextremum - w, 0];
                Y_M1 = points[k_Mextremum - w, 1];
                X_M2 = points[k_Mextremum - w + 1, 0];              // M2 un point en amont du point extremum Mk
                Y_M2 = points[k_Mextremum - w + 1, 1];
                X_M3 = points[k_Mextremum, 0];                    //M3 le point extremum Mk
                Y_M3 = points[k_Mextremum, 1];
                X_M4 = points[k_Mextremum + w - 1, 0];              //M4 un point en aval du point extremum Mk
                Y_M4 = points[k_Mextremum + w - 1, 1];

                long_segM1M2 = Math.Sqrt(Math.Pow((Y_M2 - Y_M1), 2) + Math.Pow((X_M2 - X_M1), 2));
                long_segM1M3 = Math.Sqrt(Math.Pow((Y_M3 - Y_M1), 2) + Math.Pow((X_M3 - X_M1),2));
                long_segM1M4 = Math.Sqrt(Math.Pow((Y_M4 - Y_M1), 2) + Math.Pow((X_M4 - X_M1), 2));

                alfat_M1M2 = Math.Atan((Y_M2 - Y_M1) / (X_M2 - X_M1));
                if (X_M2 - X_M1 < 0)
                    alfat_M1M2 = alfat_M1M2 + Math.PI;
                alfat_M1M3 = Math.Atan((Y_M3 - Y_M1) / (X_M3 - X_M1));
                if (X_M3 - X_M1 < 0)
                   alfat_M1M3 = alfat_M1M3 + Math.PI;
                 alfat_M1M4 = Math.Atan((Y_M4 - Y_M1) / (X_M4 - X_M1));
                if (X_M4 - X_M1 < 0)
                   alfat_M1M4 = alfat_M1M4 + Math.PI;

                Ang_diff_M1M2 = alfat_M1M2 - datM1;
                Ang_diff_M1M3 = alfat_M1M3 - datM1;
                Ang_diff_M1M4 = alfat_M1M4 - datM1;

                X_projete_M2 = long_segM1M2 * Math.Cos(Ang_diff_M1M2);
                X_projete_M3 = long_segM1M3 * Math.Cos(Ang_diff_M1M3);
                X_projete_M4 = long_segM1M4 * Math.Cos(Ang_diff_M1M4);

                if (((X_projete_M4 > X_projete_M3) && (X_projete_M3 > X_projete_M2) && (X_projete_M2 > 0))|| ((X_projete_M4 < X_projete_M3) && (X_projete_M3 < X_projete_M2) && (X_projete_M2 < 0)))
               {
                    continuite = 1;
                    inversion = 0;
               }
                  
                   else
                {
                    continuite = 0;
                    inversion = 1;
                }
                    
                
            }
            return continuite;
        }



        public int Method_inversion(int k_Mextremum, double[,] points)
        {
            continuite = 1;
            inversion = 0;
            L = points.Length / 2;
            k_M1 = k_Mextremum - w;

            if ((k_M1 > 0) && (k_Mextremum + w - 1 < L))
            {
                if ((k_Mextremum - w) - u < 1)
                {
                    denum = points[1, 0] - points[0, 0];
                    if (denum == 0)
                        denum = 1.0e-8;
                    dat_k = Math.Atan((points[1, 1] - points[0, 1]) / denum);

                }

                else if ((k_Mextremum - w) + u > L)
                {
                    denum = points[L, 0] - points[(k_Mextremum - w) - u, 1];
                    if (denum == 0)
                        denum = 1.0e-8; ////
                    dat_k = Math.Atan((points[L, 1] - points[(k_Mextremum - w) - u, 1]) / denum);

                }
                else
                {
                    denum = (points[(k_Mextremum - w) + u, 0] - points[(k_Mextremum - w) - u, 0]);
                    if (denum == 0)
                        denum = 1.0e-8;
                    dat_k = Math.Atan((points[(k_Mextremum - w) + u, 1] - points[(k_Mextremum - w) - u, 1]) / denum);
                    if ((points[(k_Mextremum - w) + u, 0] - points[(k_Mextremum - w) - u, 0]) < 0)
                        dat_k = dat_k + Math.PI;

                }
                datM1 = dat_k;

                X_M1 = points[k_Mextremum - w, 0];
                Y_M1 = points[k_Mextremum - w, 1];
                X_M2 = points[k_Mextremum - w + 1, 0];              // M2 un point en amont du point extremum Mk
                Y_M2 = points[k_Mextremum - w + 1, 1];
                X_M3 = points[k_Mextremum, 0];                    //M3 le point extremum Mk
                Y_M3 = points[k_Mextremum, 1];
                X_M4 = points[k_Mextremum + w - 1, 0];              //M4 un point en aval du point extremum Mk
                Y_M4 = points[k_Mextremum + w - 1, 1];

                long_segM1M2 = Math.Sqrt(Math.Pow((Y_M2 - Y_M1), 2) + Math.Pow((X_M2 - X_M1), 2));
                long_segM1M3 = Math.Sqrt(Math.Pow((Y_M3 - Y_M1), 2) + Math.Pow((X_M3 - X_M1), 2));
                long_segM1M4 = Math.Sqrt(Math.Pow((Y_M4 - Y_M1), 2) + Math.Pow((X_M4 - X_M1), 2));

                alfat_M1M2 = Math.Atan((Y_M2 - Y_M1) / (X_M2 - X_M1));
                if (X_M2 - X_M1 < 0)
                    alfat_M1M2 = alfat_M1M2 + Math.PI;
                alfat_M1M3 = Math.Atan((Y_M3 - Y_M1) / (X_M3 - X_M1));
                if (X_M3 - X_M1 < 0)
                    alfat_M1M3 = alfat_M1M3 + Math.PI;
                alfat_M1M4 = Math.Atan((Y_M4 - Y_M1) / (X_M4 - X_M1));
                if (X_M4 - X_M1 < 0)
                    alfat_M1M4 = alfat_M1M4 + Math.PI;

                Ang_diff_M1M2 = alfat_M1M2 - datM1;
                Ang_diff_M1M3 = alfat_M1M3 - datM1;
                Ang_diff_M1M4 = alfat_M1M4 - datM1;

                X_projete_M2 = long_segM1M2 * Math.Cos(Ang_diff_M1M2);
                X_projete_M3 = long_segM1M3 * Math.Cos(Ang_diff_M1M3);
                X_projete_M4 = long_segM1M4 * Math.Cos(Ang_diff_M1M4);

                if (((X_projete_M4 > X_projete_M3) && (X_projete_M3 > X_projete_M2) && (X_projete_M2 > 0)) || ((X_projete_M4 < X_projete_M3) && (X_projete_M3 < X_projete_M2) && (X_projete_M2 < 0)))
                {
                    continuite = 1;
                    inversion = 0;
                }

                else
                {
                    continuite = 0;
                    inversion = 1;
                }


            }
            return inversion;
        }
    }
    
}

