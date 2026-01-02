using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{
    class VS_normalisation_H_L_hauteur_et_largeur
    {
        public int rapport_norm_h, l;
        public double maxi_y, maxi_x, mini_x, mini_y, m_y, m_x, rapport_norm_l;
        public double[,] data_norm, pure_data;
        public double[] X, Y, xs, ys, pure_data_1, pure_data_0, xn, yn;
        double maximum;

        public double[,] method_normalisation_hauteur_et_Largeur(double[,] data, int n)
        {
            rapport_norm_h = 128;
            data_norm = new double[n, 2];
            //  Console.WriteLine("taille n"+ n);
            //  Console.WriteLine("taille data"+data.GetLength(0));
            /* for (int i = 0; i <data.GetLength(0); i++)
             {
                 double s1 = data[i, 0];
                 double s2 = data[i, 1];
                 Console.WriteLine("{0}, {1}"+ s1 + " "+ s2);
             }*/
            X = new double[data.GetLength(0)];
            Y = new double[data.GetLength(0)];
            xs = new double[data.GetLength(0)];
            ys = new double[data.GetLength(0)];

            for (int i = 0; i < data.GetLength(0); i++)
            {
                X[i] = data[i, 0];
                Y[i] = data[i, 1];
                // System.Console.WriteLine(data[i,0]);
            }
            l = 0;
            for (int i = 0; i < n; i++)
            {

                if ((X[i] != 0) || (Y[i] != 0))
                    l += 1;
                //calcul taille pure_data 
            }
            //Console.WriteLine("L" + l);
            if (l > 2)
            {
                pure_data = new double[l, data.GetLength(1)];
                int ii = 0;
                for (int i = 0; i < n; i++)
                {

                    if ((X[i] != 0) || (Y[i] != 0))
                    {
                        pure_data[ii, 0] = X[i];
                        pure_data[ii, 1] = Y[i];
                        ii += 1;
                    }

                }


                pure_data_1 = new double[l];

                pure_data_0 = new double[l];
                for (int i = 0; i < l; i++)
                {
                    pure_data_0[i] = pure_data[i, 0];
                    pure_data_1[i] = pure_data[i, 1];

                }




                // Console.WriteLine("taille pure" + pure_data.GetLength(0));
                maxi_x = max_matrice(pure_data_0);
                maxi_y = max_matrice(pure_data_1);   //% maxi_y est le maximum des ordonnées
                mini_x = min_matrice(pure_data_0);         // % mini_x est le minimum des abscisses
                mini_y = min_matrice(pure_data_1);        // % mini_y est le minimum des ordonnées
                                                          // m = maxi_y - mini_y;

                m_y = maxi_y - mini_y;   // Normalisation suivant la hauteur
                m_x = maxi_x - mini_x;   // Normalisation suivant la largeur
                if (m_x > 0)
                {
                    if ((m_y / m_x) > 1.6)
                        rapport_norm_l = rapport_norm_h / 1;  // 2
                                                              //rapport_norm_l = rapport_norm_h * (m_x / m_y);   // 2
                    else if ((m_y / m_x) < 0.625)
                        rapport_norm_l = rapport_norm_h * 1;
                    //rapport_norm_l = rapport_norm_h * (m_x / m_y);
                    else
                        rapport_norm_l = rapport_norm_h;
                    //rapport_norm_l = rapport_norm_h * (m_x / m_y); 
                }
                else
                    //rapport_norm_l = rapport_norm_h;
                    rapport_norm_l = rapport_norm_h * (m_x / m_y);


                xn = new double[n];
                yn = new double[n];
                //xs = new double[n - 2];
                if ((m_y != 0) && (m_x != 0))
                {
                    for (int i = 0; i < n; i++)
                    {
                        if ((X[i] != 0) || (Y[i] != 0))
                        {
                            xn[i] = (rapport_norm_l * ((X[i] - mini_x) / m_x)) + 1;
                            xs[i] = xn[i];
                            yn[i] = rapport_norm_h * ((Y[i] - mini_y) / m_y);
                            ys[i] = yn[i];

                            /*xn = rapport_norm * ((X[i] - mini_x) / m);
                            xs[i] = xn;
                            yn = rapport_norm * ((Y[i] - mini_y) / m);
                            ys[i] = yn;*/
                        }
                        else
                        {
                            xs[i] = 0;//xs = [xs; 0];
                            yn[i] = rapport_norm_h * ((Y[i] - mini_y) / m_y);
                            ys[i] = 0;//ys = [ys; 0];
                        }


                    }
                }
                else
                {
                    xs = X;
                    ys = Y;

                }

                /*for (int i = 0; i < n; i++)
                {
                    data_norm[i,0] = xs[i];
                    data_norm[i,1] = ys[i];
                }*/

                // data_norm = [xs, ys];
                for (int k = 0; k < n; k++)
                {
                    data_norm[k, 0] = xs[k];
                    data_norm[k, 1] = ys[k];
                }
            }
            else
            {
                for (int k = 0; k < n; k++)
                {
                    data_norm[k, 0] = data[k, 0];
                    data_norm[k, 1] = data[k, 1];
                }
            }

            return data_norm;
        }

        public double max_matrice(double[] tab)
        {

            if (tab != null)
            {
                maximum = tab[0];
                for (int i = 1; i < tab.Length; i++)
                {
                    if (tab[i] > maximum)
                        maximum = tab[i];
                }

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
