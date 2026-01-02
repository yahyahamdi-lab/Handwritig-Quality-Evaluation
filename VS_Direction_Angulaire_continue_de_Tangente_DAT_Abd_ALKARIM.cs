using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{
    class VS_Direction_Angulaire_continue_de_Tangente_DAT_Abd_ALKARIM
    {
        public int  Lp, L_DAT, type_pnt,  indice_k, continuite, inversion;
        public float Dat;
        public double tour_ajout, diff_angle_min, hu, sens_preced,sens_actuel, k1, k2;
        public const int u = 1;
        public const double pas = 0.01;
        public double denum, cotion, dat_k, num, cotion_preced, diff_angle_1, diff_angle_2, diff_angle_3;
        //public double[,] DAT = new double[10, 2];
        public double[] DAT,Ttemps, memDAT,memDAT2, x, Diff_DAT;
        public int[] type_point ;
        public VS_verification_changement_sens_parcour_Abd_ALKARIM changement_sens;
        public double[] MethodsDat(double indice_deb, double indice_fin, double[,] points)
        {
            k1 = indice_deb;
            k2 = indice_fin;
            Lp = points.GetLength(0);
            DAT = new double[(int)indice_fin];
            x = new double[3];
            memDAT = new double[(int)indice_fin];
            memDAT2 = new double[(int)indice_fin];
            Ttemps = new double[(int)indice_fin];
            type_point = new int[(int)indice_fin];
            Diff_DAT = new double[(int)indice_fin];
            cotion_preced = 1;
            changement_sens = new VS_verification_changement_sens_parcour_Abd_ALKARIM();

            for (double k = k1; k < k2; k++)
            {
                if ((k - u < 0) && (k + u < Lp)) //((k - u < 1) && (k + u < Lp)) 
                {
                    if (points[(int)k + u, 0] - points[0, 0] == 0)
                    {
                        denum = 1.0e-7;
                    }
                    else
                    {
                        denum = (points[(int)k + u, 0] - points[0, 0]);
                    }
                    cotion = (points[(int)k + u, 1] - points[0, 1]) / denum;
                    dat_k = Math.Atan(cotion);
                }
                else if (k + u > Lp-1)
                {
                    if ((points[Lp-1, 0] - points[(int)k - 1, 0]) == 0)
                        denum = 1.0e-7;
                    else
                        denum = (points[Lp-1, 0] - points[(int)k - 1, 0]);

                    cotion = (points[Lp-1, 1] - points[(int)k - 1, 1]) / denum;
                    dat_k = Math.Atan(cotion);
                }
                else if ((k + u < Lp-1) && (k - u > 0)) //((k + u < Lp) && (k - u > 1))
                {
                    num = (points[(int)k + u, 1] - points[(int)k - u, 1]);
                    denum = (points[(int)k + u, 0] - points[(int)k - u, 0]);

                    if (denum == 0)
                    {
                        denum = 1.0e-7;
                        cotion = Math.Abs(num / denum) * Math.Sign(cotion_preced);
                    }
                    else
                    {
                        cotion = num / denum;
                    }
                    cotion_preced = cotion;
                    dat_k = Math.Atan(cotion);
                }

                 DAT[(int)k] = dat_k;        
                 Ttemps[(int)k] = k * pas;
                 //System.//Console.WriteLine(dat_k);

            }

            memDAT = DAT;

            // eviter les discontinuités dans la variation de l'angle d'inclinaison de la tangente
            tour_ajout = 0;
            L_DAT = DAT.Length;
            for (int i = 1; i < L_DAT; i++)
            {
                tour_ajout = Math.Round((DAT[i] - DAT[i - 1]) / Math.PI) * Math.PI; // 
                DAT[i] = DAT[i] - tour_ajout;

                diff_angle_1 = Math.Abs((DAT[i]) - DAT[i - 1]);
                diff_angle_2 = Math.Abs((DAT[i] + Math.PI) - DAT[i - 1]);
                diff_angle_3 = Math.Abs((DAT[i] - Math.PI) - DAT[i - 1]);
                //x = [, diff_angle_2, diff_angle_3];
                x[0] = diff_angle_1;
                x[1] = diff_angle_2;
                x[2] = diff_angle_3;
                diff_angle_min = min_matrice(x);

                if (diff_angle_min == diff_angle_1)
                {
                    DAT[i] = DAT[i];
                    hu = 0;
                }
                else if (diff_angle_min == diff_angle_2)
                {
                    DAT[i] = DAT[i] + Math.PI;
                    hu = Math.PI;
                }

                else if (diff_angle_min == diff_angle_3)
                {
                    DAT[i] = DAT[i] - Math.PI;
                    hu = -Math.PI;
                }
            }
            //memDAT2 = DAT;

            // recherche des minimums locaux de rayon de courbure

            Diff_DAT[0] = 0;
            for (int i = 1; i < L_DAT; i++) // boucle commence de 2
                Diff_DAT[i] = DAT[i] - DAT[i - 1];

            type_point[0] = 0;
            for (int i = 1; i < L_DAT-1; i++)       // L_DAT -1
            {
                if ((Diff_DAT[i] < Diff_DAT[i - 1]) && (Diff_DAT[i] < Diff_DAT[i + 1]))
                    type_pnt = 1;
                else
                    type_pnt = 0;
                type_point[i] = type_pnt; // type_point = [type_point; type_pnt];
            }
            type_point[type_point.Length - 1] = 0;
            // type_point = [0;type_point;0];


            ////////////////
            ////////////////
            sens_preced = 0;

            for (int i = 1; i < L_DAT; i++)
            {
                sens_actuel = Math.Sign(DAT[i] - DAT[i - 1]);
                if ((sens_actuel != sens_preced) && (sens_preced != 0))
                {
                    if ((type_point[i] == 1) || (type_point[i - 1] == 1))
                    {
                        indice_k = (int)(i + k1 - 1);
                        continuite = changement_sens.Method_continuite(indice_k, points);
                        inversion = changement_sens.Method_inversion(indice_k, points);
                      if ((continuite == 0) && (inversion == 1))
                      {
                            DAT[i] = DAT[i] + (sens_preced * (Math.PI));
                            sens_actuel = Math.Sign(DAT[i] - DAT[i-1]);

                      }
                        else
                        {
                            DAT[i] = DAT[i];
                        }

                    }
                }
                sens_preced = sens_actuel;
            }


                return DAT;
        }




       public double[] Method_memDAT(int indice_deb, int indice_fin, double[,] points)
        {
            k1 = indice_deb;
            k2 = indice_fin;
            Lp = points.GetLength(0);
            DAT = new double[(int)indice_fin];
            x = new double[3];
            memDAT = new double[(int)indice_fin];
            memDAT2 = new double[(int)indice_fin];
            Ttemps = new double[(int)indice_fin];
            type_point = new int[(int)indice_fin];
            Diff_DAT = new double[(int)indice_fin];//////////////

            changement_sens = new VS_verification_changement_sens_parcour_Abd_ALKARIM();

            for (double k = k1; k < k2; k++)
            {
                if ((k - u < 1) && (k + u < Lp))
                {
                    if (points[(int)k + 1, 0] - points[0, 0] == 0)
                    {
                        denum = 1.0e-7;
                    }
                    else
                    {
                        denum = (points[(int)k + u, 0] - points[0, 0]);
                    }
                    cotion = (points[(int)k + u, 1] - points[0, 1]) / denum;
                    dat_k = Math.Atan(cotion);
                }
                else if (k + u > Lp)
                {
                    if ((points[Lp, 0] - points[(int)k - 1, 0]) == 0)
                        denum = 1.0e-7;
                    else
                        denum = (points[Lp, 0] - points[(int)k - 1, 0]);
                    cotion = (points[Lp, 1] - points[(int)k - 1, 1]) / denum;
                    dat_k = Math.Atan(cotion);
                }
                else if ((k + u < Lp) && (k - u > 1))
                {
                    num = (points[(int)k + u, 1] - points[(int)k - u, 1]);
                    denum = (points[(int)k + u, 0] - points[(int)k - u, 0]);
                    if (denum == 0)
                    {
                        denum = 1.0e-7;
                        cotion = Math.Abs(num / denum) * Math.Sign(cotion_preced);
                    }
                    else
                    {
                        cotion = num / denum;
                    }
                    cotion_preced = cotion;
                    dat_k = Math.Atan(cotion);
                }

                DAT[(int)k] = dat_k;        // à verifier
                Ttemps[(int)k] = k * pas;


            }

            memDAT = DAT;

            return memDAT;
        }

       public int[] Method_type_point(int indice_deb, int indice_fin, double[,] points)
        {
            k1 = indice_deb;
            k2 = indice_fin;
            Lp = points.GetLength(0);
            DAT = new double[(int)indice_fin];
            x = new double[3];
            memDAT = new double[(int)indice_fin];
            memDAT2 = new double[(int)indice_fin];
            Ttemps = new double[(int)indice_fin];
            type_point = new int[(int)indice_fin];
            Diff_DAT = new double[(int)indice_fin];//////////////
            changement_sens = new VS_verification_changement_sens_parcour_Abd_ALKARIM();

            for (double k = k1; k < k2; k++)
            {
                if ((k - u < 1) && (k + u < Lp))
                {
                    if (points[(int)k + 1, 0] - points[0, 0] == 0)
                    {
                        denum = 1.0e-7;
                    }
                    else
                    {
                        denum = (points[(int)k + u, 0] - points[0, 0]);
                    }
                    cotion = (points[(int)k + u, 1] - points[0, 1]) / denum;
                    dat_k = Math.Atan(cotion);
                }
                else if (k + u > Lp)
                {
                    if ((points[Lp, 0] - points[(int)k - 1, 0]) == 0)
                        denum = 1.0e-7;
                    else
                        denum = (points[Lp, 0] - points[(int)k - 1, 0]);
                    cotion = (points[Lp, 1] - points[(int)k - 1, 1]) / denum;
                    dat_k = Math.Atan(cotion);
                }
                else if ((k + u < Lp) && (k - u > 1))
                {
                    num = (points[(int)k + u, 1] - points[(int)k - u, 1]);
                    denum = (points[(int)k + u, 0] - points[(int)k - u, 0]);
                    if (denum == 0)
                    {
                        denum = 1.0e-7;
                        cotion = Math.Abs(num / denum) * Math.Sign(cotion_preced);
                    }
                    else
                    {
                        cotion = num / denum;
                    }
                    cotion_preced = cotion;
                    dat_k = Math.Atan(cotion);
                }

                DAT[(int)k] = dat_k;        // à verifier
                Ttemps[(int)k] = k * pas;


            }

            memDAT = DAT;

            // eviter les discontinuités dans la variation de l'angle d'inclinaison de la tangente
            tour_ajout = 0;
            L_DAT = DAT.Length;
            for (int i = 1; i < L_DAT; i++)
            {
                tour_ajout = Math.Round((DAT[i] - DAT[i - 1]) / Math.PI) * Math.PI; // 
                DAT[i] = DAT[i] - tour_ajout;

                diff_angle_1 = Math.Abs((DAT[i]) - DAT[i - 1]);
                diff_angle_2 = Math.Abs((DAT[i] + Math.PI) - DAT[i - 1]);
                diff_angle_3 = Math.Abs((DAT[i] - Math.PI) - DAT[i - 1]);
                //x = [, diff_angle_2, diff_angle_3];
                x[0] = diff_angle_1;
                x[1] = diff_angle_2;
                x[2] = diff_angle_3;
                diff_angle_min = min_matrice(x);

                if (diff_angle_min == diff_angle_1)
                {
                    DAT[i] = DAT[i];
                    hu = 0;
                }
                else if (diff_angle_min == diff_angle_2)
                {
                    DAT[i] = DAT[i] + Math.PI;
                    hu = Math.PI;
                }

                else if (diff_angle_min == diff_angle_3)
                {
                    DAT[i] = DAT[i] - Math.PI;
                    hu = -Math.PI;
                }
            }
            //memDAT2 = DAT;

            // recherche des minimums locaux de rayon de courbure

            Diff_DAT[0] = 0;
            for (int i = 1; i < L_DAT; i++) // boucle commence de 2
                Diff_DAT[i] = DAT[i] - DAT[i - 1];

            type_point[0] = 0;
            for (int i = 1; i < L_DAT - 1; i++)       // L_DAT -1
            {
                if ((Diff_DAT[i] < Diff_DAT[i - 1]) && (Diff_DAT[i] < Diff_DAT[i + 1]))
                    type_pnt = 1;
                else
                    type_pnt = 0;
                type_point[i] = type_pnt; // type_point = [type_point; type_pnt];
            }
            type_point[type_point.Length - 1] = 0;

            return type_point;
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

        public double[] Method_Ttemps(int indice_deb, int indice_fin, double[,] points)
        {
            k1 = indice_deb;
            k2 = indice_fin;
            Lp = points.GetLength(0);
            DAT = new double[(int)indice_fin];
            x = new double[3];
            memDAT = new double[(int)indice_fin];
            memDAT2 = new double[(int)indice_fin];
            Ttemps = new double[(int)indice_fin];
            type_point = new int[(int)indice_fin];
            Diff_DAT = new double[(int)indice_fin];//////////////
            changement_sens = new VS_verification_changement_sens_parcour_Abd_ALKARIM();

            for (double k = k1; k < k2; k++)
            {
                if ((k - u < 1) && (k + u < Lp))
                {
                    if (points[(int)k + 1, 0] - points[0, 0] == 0)
                    {
                        denum = 1.0e-7;
                    }
                    else
                    {
                        denum = (points[(int)k + u, 0] - points[0, 0]);
                    }
                    cotion = (points[(int)k + u, 1] - points[0, 1]) / denum;
                    dat_k = Math.Atan(cotion);
                }
                else if (k + u > Lp)
                {
                    if ((points[Lp, 0] - points[(int)k - 1, 0]) == 0)
                        denum = 1.0e-7;
                    else
                        denum = (points[Lp, 0] - points[(int)k - 1, 0]);
                    cotion = (points[Lp, 1] - points[(int)k - 1, 1]) / denum;
                    dat_k = Math.Atan(cotion);
                }
                else if ((k + u < Lp) && (k - u > 1))
                {
                    num = (points[(int)k + u, 1] - points[(int)k - u, 1]);
                    denum = (points[(int)k + u, 0] - points[(int)k - u, 0]);
                    if (denum == 0)
                    {
                        denum = 1.0e-7;
                        cotion = Math.Abs(num / denum) * Math.Sign(cotion_preced);
                    }
                    else
                    {
                        cotion = num / denum;
                    }
                    cotion_preced = cotion;
                    dat_k = Math.Atan(cotion);
                }

                DAT[(int)k] = dat_k;        // à verifier
                Ttemps[(int)k] = k * pas;


            }

            return Ttemps;
        }

    }

}

