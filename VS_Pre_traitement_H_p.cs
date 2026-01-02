using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{
   public class VS_Pre_traitement_H_p
   {
        public double[,] points,p;
        public double[] t, T_init, X_init, Y_init,TT, dt,XX, dDx, VX,VX1,YY, V, dDy,VY1, VY, dV, DPV, VXX, VYY, DPV1;
        public int longt, rayon;
        public double pas_t, temp_depart, sigma_p;
        VS_filtre_lineaire_1 filtre1;
        VS_filtre_lineaire filtre;
        public double[,] VS_Pre_traitement_H_p_points(double[] X, double[] Y, double[] T, int rayon_filtre_traject, double sigma_p_filtre_traject, int rayon_filtre_V, double sigma_p_filtre_V)
        {
            longt = T.Length;
            t = new double[T.Length];
            TT = new double[T.Length];
            //T_init = new double[T.Length];

            points = new double[X.Length,2];

            pas_t = T[1] - T[0];
            temp_depart = T[0];
            for(int i=0;i<longt;i++)
                TT[i] = T[i] - temp_depart;
            for (int i = 0; i < X.Length; i++)  //points = [X, Y];
            {
                points[i, 0] = X[i];
                points[i, 1] = Y[i];
            }
               
            t = TT;
            rayon = rayon_filtre_traject;
            //sigma_p = sigma_p_filtre_traject - 1;
            if (sigma_p_filtre_traject > 1)
                sigma_p = sigma_p_filtre_traject - 1;
            else
                sigma_p = sigma_p_filtre_traject;
            

            if (((rayon * 2) + 1) > points.GetLength(0))
                rayon = points.GetLength(0);

           /* Console.WriteLine("points XXXXXXXXXXXXXXXXXXX avant");
             for (int i = 0; i < points.GetLength(0); i++)
               Console.WriteLine(points[i, 0]);

             Console.WriteLine("points YYYYYYYYYYYYYYYYYYY avnant");
             for (int i = 0; i < points.GetLength(0); i++)
              Console.WriteLine(points[i, 1]);
              */
            filtre1 = new VS_filtre_lineaire_1();
            points = filtre1.Method_VS_filtre_lineaire_1_points(rayon, sigma_p, 0, points.GetLength(0), points); //  

           
            points = filtre1.Method_VS_filtre_lineaire_1_points(rayon, sigma_p, 0, points.GetLength(0), points);
            points = filtre1.Method_VS_filtre_lineaire_1_points(rayon, sigma_p, 0, points.GetLength(0), points);
            points = filtre1.Method_VS_filtre_lineaire_1_points(rayon, sigma_p, 0, points.GetLength(0), points);
            // [points,XYpoint] = VS_filtre_lineaire_1(rayon, sigma_p, 1, size(points,1), points);
            //T_init = T;

            return points;

        }

        public double[] VS_Pre_traitement_H_p_V(double[] X, double[] Y, double[] T, int rayon_filtre_traject, double sigma_p_filtre_traject, int rayon_filtre_V, double sigma_p_filtre_V)
        {
            longt = T.Length;
            t = new double[T.Length];
            TT = new double[T.Length];
            //T_init = new double[T.Length];

            points = new double[X.Length, 2];

            pas_t = T[1] - T[0];
            temp_depart = T[0];
            for (int i = 0; i < longt; i++)
                TT[i] = T[i] - temp_depart;
            for (int i = 0; i < X.Length; i++)  //points = [X, Y];
            {
                points[i, 0] = X[i];
                points[i, 1] = Y[i];
            }

            t = TT;


            rayon = rayon_filtre_traject;
            //sigma_p = sigma_p_filtre_traject - 1;
            if (sigma_p_filtre_traject > 1)
                sigma_p = sigma_p_filtre_traject - 1;
            else
                sigma_p = sigma_p_filtre_traject;

            if (((rayon * 2) + 1) > points.GetLength(0))
                rayon = points.GetLength(0);

            filtre1 = new VS_filtre_lineaire_1();
            points = filtre1.Method_VS_filtre_lineaire_1_points(rayon, sigma_p, 0, points.GetLength(0), points);
            points = filtre1.Method_VS_filtre_lineaire_1_points(rayon, sigma_p, 0, points.GetLength(0), points);
            points = filtre1.Method_VS_filtre_lineaire_1_points(rayon, sigma_p, 0, points.GetLength(0), points);
            points = filtre1.Method_VS_filtre_lineaire_1_points(rayon, sigma_p, 0, points.GetLength(0), points);
            filtre = new VS_filtre_lineaire();

            dt = new double[t.Length - 1];
            XX = new double[points.GetLength(0)];
            YY = new double[points.GetLength(0)];
            for (int i = 0; i < t.Length - 1; i++) // dt = diff(t);
                dt[i] = t[i + 1] - t[i];

            //for (int j = 0; j < t.Length-1; j++)
            // System.//Console.WriteLine(dt[j]);
            // --------Calcul de la composante de vitesse suivant X, VX

            for (int i = 0; i < points.GetLength(0); i++) //XX = points(:,1);
                XX[i] = points[i, 0];

            dDx = new double[XX.Length - 1];

            VX1 = new double[XX.Length - 1];
            VX = new double[XX.Length];                        ///// ??
            for (int i = 0; i < dDx.Length; i++) // dDx = diff(XX);
                dDx[i] = XX[i + 1] - XX[i];


            for (int i = 0; i < VX1.Length; i++)
                VX1[i] = dDx[i] / dt[i];


            VX[0] = 0;
            for (int i = 0; i < VX1.Length; i++) //VX =[0; VX];
            {
                VX[i + 1] = VX1[i];
            }
            VX[VX.Length - 1] = 0; //VX =[VX(1:length(VX) - 1); 0];

///
            VXX = new double[VX.Length];

            // --------Calcul de la composante de vitesse suivant Y, VY
            for (int i = 0; i < points.GetLength(0); i++) //XX = points(:,1);
                YY[i] = points[i, 1];   

            dDy = new double[YY.Length - 1];
            VY1 = new double[YY.Length - 1];
            for (int i = 0; i < dDy.Length; i++) // dDy = diff(Y);
                dDy[i] = YY[i + 1] - YY[i];

            for (int i = 0; i < VY1.Length; i++) // VY = (dDy)./ (dt);
                VY1[i] = dDy[i] / dt[i];


            VY = new double[YY.Length];
            VY[0] = 0;
            for (int i = 0; i < VY1.Length; i++) //VX =[0; VX];
            {
                VY[i + 1] = VY1[i];
            }
            VY[VY.Length - 1] = 0;
            VYY = new double[VX.Length];


            VXX = filtre.method_VS_filtre_lineaire(rayon_filtre_V, sigma_p_filtre_V, VX); // [VX] = VS_filtre_lineaire(rayon_filtre_V, sigma_p_filtre_V, VX);
            
            VYY = filtre.method_VS_filtre_lineaire(rayon_filtre_V, sigma_p_filtre_V, VY);

            V = new double[VXX.Length];
            for (int i = 0; i < V.Length; i++)   // V = sqrt((VX.^ 2) + (VY.^ 2));
                V[i] = Math.Sqrt(Math.Pow(VXX[i], 2) + Math.Pow(VYY[i], 2));
            
            V = filtre.method_VS_filtre_lineaire(rayon_filtre_V, sigma_p_filtre_V, V);     // ??

            V = filtre.method_VS_filtre_lineaire(rayon_filtre_V, sigma_p_filtre_V, V);     // ??


            return V;
        }



        public double[] VS_Pre_traitement_H_p_DPV(double[] X, double[] Y, double[] T, int rayon_filtre_traject, double sigma_p_filtre_traject, int rayon_filtre_V, double sigma_p_filtre_V)
        {
            longt = T.Length;
            t = new double[T.Length];
            TT = new double[T.Length];
            //T_init = new double[T.Length];

            points = new double[X.Length, 2];

            pas_t = T[1] - T[0];
            temp_depart = T[0];
            for (int i = 0; i < longt; i++)
                TT[i] = T[i] - temp_depart;
            for (int i = 0; i < X.Length; i++)  //points = [X, Y];
            {
                points[i, 0] = X[i];
                points[i, 1] = Y[i];
            }

            t = TT;


            rayon = rayon_filtre_traject;
            // sigma_p = sigma_p_filtre_traject - 1;
            if (sigma_p_filtre_traject > 1)
                sigma_p = sigma_p_filtre_traject - 1;
            else
                sigma_p = sigma_p_filtre_traject;
            if (((rayon * 2) + 1) > points.GetLength(0))
                rayon = points.GetLength(0);

            filtre1 = new VS_filtre_lineaire_1();
            points = filtre1.Method_VS_filtre_lineaire_1_points(rayon, sigma_p, 0, points.GetLength(0), points);
            points = filtre1.Method_VS_filtre_lineaire_1_points(rayon, sigma_p, 0, points.GetLength(0), points);
            points = filtre1.Method_VS_filtre_lineaire_1_points(rayon, sigma_p, 0, points.GetLength(0), points);
            points = filtre1.Method_VS_filtre_lineaire_1_points(rayon, sigma_p, 0, points.GetLength(0), points);

            filtre = new VS_filtre_lineaire();

            dt = new double[t.Length - 1];
            XX = new double[points.GetLength(0)];
            YY = new double[points.GetLength(0)];
            for (int i = 0; i < t.Length - 1; i++) // dt = diff(t);
                dt[i] = t[i + 1] - t[i];

            // --------Calcul de la composante de vitesse suivant X, VX

            for (int i = 0; i < points.GetLength(0); i++) //XX = points(:,1);
                XX[i] = points[i, 0];
            /* for (int j = 0; j < XX.Length; j++)
                 System.//Console.WriteLine(XX[j]);*/
            dDx = new double[XX.Length - 1];

            VX1 = new double[XX.Length - 1];
            VX = new double[XX.Length];                        
            for (int i = 0; i < dDx.Length; i++) // dDx = diff(XX);
                dDx[i] = XX[i + 1] - XX[i];


            for (int i = 0; i < VX1.Length; i++)
                VX1[i] = dDx[i] / dt[i];


            VX[0] = 0;
            for (int i = 0; i < VX1.Length; i++) //VX =[0; VX];
            {
                VX[i + 1] = VX1[i];
            }
            VX[VX.Length - 1] = 0; //VX =[VX(1:length(VX) - 1); 0];

            ///
            VXX = new double[VX.Length];

            // --------Calcul de la composante de vitesse suivant Y, VY
            for (int i = 0; i < points.GetLength(0); i++) //XX = points(:,1);
                YY[i] = points[i, 1];

            dDy = new double[YY.Length - 1];
            VY1 = new double[YY.Length - 1];
            for (int i = 0; i < dDy.Length; i++) // dDy = diff(Y);
                dDy[i] = YY[i + 1] - YY[i];

            for (int i = 0; i < VY1.Length; i++) // VY = (dDy)./ (dt);
                VY1[i] = dDy[i] / dt[i];


            VY = new double[YY.Length];
            VY[0] = 0;
            for (int i = 0; i < VY1.Length; i++) //VX =[0; VX];
            {
                VY[i + 1] = VY1[i];
            }
            VY[VY.Length - 1] = 0;
            VYY = new double[VX.Length];


            VXX = filtre.method_VS_filtre_lineaire(rayon_filtre_V, sigma_p_filtre_V, VX); // [VX] = VS_filtre_lineaire(rayon_filtre_V, sigma_p_filtre_V, VX);

            VYY = filtre.method_VS_filtre_lineaire(rayon_filtre_V, sigma_p_filtre_V, VY);

            V = new double[VXX.Length];
            for (int i = 0; i < V.Length; i++)   // V = sqrt((VX.^ 2) + (VY.^ 2));
                V[i] = Math.Sqrt(Math.Pow(VXX[i], 2) + Math.Pow(VYY[i], 2));

            V = filtre.method_VS_filtre_lineaire(rayon_filtre_V, sigma_p_filtre_V, V);     

            V = filtre.method_VS_filtre_lineaire(rayon_filtre_V, sigma_p_filtre_V, V);     


            dV = new double[V.Length - 1];
            DPV = new double[V.Length - 1];
            for (int i = 0; i < dV.Length; i++) // dV = diff(V);    // ??
                dV[i] = V[i + 1] - V[i];
           
            for (int i = 0; i < DPV.Length; i++) // DPV = (dV)./ (dt);
                DPV[i] = dV[i] / dt[i];

            DPV1 = new double[DPV.Length + 1];
            DPV1[0] = 0;
            for (int i = 1; i < DPV1.Length; i++)   // DPV = [0; DPV];
                DPV1[i] = DPV[i - 1];

            DPV1 = filtre.method_VS_filtre_lineaire(rayon_filtre_V, sigma_p_filtre_V, DPV1); // //[DPV] = VS_filtre_lineaire(rayon_filtre_V, sigma_p_filtre_V, DPV);

            return DPV1;
        }
        public double [] VS_Pre_traitement_H_p_T(double[] T)
        {
            longt = T.Length;
            TT = new double[T.Length];
            //T_init = new double[T.Length];
            temp_depart = T[0];
            for (int i = 0; i < longt; i++)
                TT[i] = T[i] - temp_depart;
            return TT;
        }

        public double [] VS_Pre_traitement_H_p_t( double[] T)
        {
            longt = T.Length;
            TT = new double[T.Length];
            t = new double[T.Length];
            //T_init = new double[T.Length];
            temp_depart = T[0];
            for (int i = 0; i < longt; i++)
                TT[i] = T[i] - temp_depart;
            t = TT;
            return t;
        }
        public double[] VS_Pre_traitement_H_p_X_init(double[] X, double[] Y, double[] T, int rayon_filtre_traject, double sigma_p_filtre_traject, int rayon_filtre_V, double sigma_p_filtre_V)
        {
            longt = T.Length;
            t = new double[T.Length];
            //T_init = new double[T.Length];

            points = new double[X.Length,2];

            pas_t = T[1] - T[0];
            temp_depart = T[0];
            for (int i = 0; i < longt; i++)
                T[i] -= temp_depart;

            for (int i = 0; i < X.Length; i++)  //points = [X, Y];
            {
                points[i, 0] = X[i];
                points[i, 1] = Y[i];
            }

            t = T;
            rayon = rayon_filtre_traject;
            // sigma_p = sigma_p_filtre_traject - 1;
            if (sigma_p_filtre_traject > 1)
                sigma_p = sigma_p_filtre_traject - 1;
            else
                sigma_p = sigma_p_filtre_traject;

            if (((rayon * 2) + 1) > points.GetLength(0))
                rayon = points.GetLength(0);

            filtre1 = new VS_filtre_lineaire_1();
            points = filtre1.Method_VS_filtre_lineaire_1_points(rayon, sigma_p, 0, points.GetLength(0), points);  // ???
            points = filtre1.Method_VS_filtre_lineaire_1_points(rayon, sigma_p, 0, points.GetLength(0), points);
            points = filtre1.Method_VS_filtre_lineaire_1_points(rayon, sigma_p, 0, points.GetLength(0), points);
            points = filtre1.Method_VS_filtre_lineaire_1_points(rayon, sigma_p, 0, points.GetLength(0), points);
            X_init = new double[points.GetLength(0)];
            for (int i = 0; i < points.GetLength(0); i++)
            {
                X_init[i] = points[i, 0]; //points(:, 1);
                //Y_init[i] = points[i, 1]; //points(:, 2);
            }
            return X_init;

        }
        public double[] VS_Pre_traitement_H_p_Y_init(double[] X, double[] Y, double[] T, int rayon_filtre_traject, double sigma_p_filtre_traject, int rayon_filtre_V, double sigma_p_filtre_V)
        {
            longt = T.Length;
            t = new double[T.Length];
            //T_init = new double[T.Length];

            points = new double[X.Length, 2];

            pas_t = T[1] - T[0];
            temp_depart = T[0];
            for (int i = 0; i < longt; i++)
                T[i] -= temp_depart;

            for (int i = 0; i < X.Length; i++)  //points = [X, Y];
            {
                points[i, 0] = X[i];
                points[i, 1] = Y[i];
            }

            t = T;
            rayon = rayon_filtre_traject;
            // sigma_p = sigma_p_filtre_traject - 1;
            if (sigma_p_filtre_traject > 1)
                sigma_p = sigma_p_filtre_traject - 1;
            else
                sigma_p = sigma_p_filtre_traject;

            if (((rayon * 2) + 1) > points.GetLength(0))
                rayon = points.GetLength(0);

            filtre1 = new VS_filtre_lineaire_1();
            points = filtre1.Method_VS_filtre_lineaire_1_points(rayon, sigma_p, 0 , points.GetLength(0), points);
            points = filtre1.Method_VS_filtre_lineaire_1_points(rayon, sigma_p, 0, points.GetLength(0), points);
            points = filtre1.Method_VS_filtre_lineaire_1_points(rayon, sigma_p, 0, points.GetLength(0), points);
            points = filtre1.Method_VS_filtre_lineaire_1_points(rayon, sigma_p, 0, points.GetLength(0), points);
            Y_init = new double[points.GetLength(0)];
            for (int i = 0; i < points.GetLength(0); i++)
            {
                //X_init[i] = points[i, 0]; //points(:, 1);
                Y_init[i] = points[i, 1]; //points(:, 2);
            }
            return Y_init;

        }
        public double[] VS_Pre_traitement_H_p_T_init(double[] T)
        {
            longt = T.Length;
            TT = new double[T.Length];
            //T_init = new double[T.Length];
            temp_depart = T[0];
            for (int i = 0; i < longt; i++)
                TT[i] = T[i] - temp_depart;
            return TT;
        }
        public double VS_Pre_traitement_H_p_pas_t(double[] T)
        {
            pas_t = T[1] - T[0];
            return pas_t;
        }
    }
}
