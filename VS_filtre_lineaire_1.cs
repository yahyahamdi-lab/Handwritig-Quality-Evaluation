using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{
    class VS_filtre_lineaire_1
    {
        public double poids_courant, somme_poids, poids_p, Xpointt, ypointt;
        public double[] poids;
        public int indice_voisin_gch, indice_voisin_drt;
        public double[,] XYpoint;
        
        public double[,] Method_VS_filtre_lineaire_1_points(int rayon, double sigma_p, int indice_deb, int indice_fin, double[,] points) 
        {
            
            poids_courant = 1 / (sigma_p * 2.5066);
            poids = new double[rayon]; 
            poids[0] = poids_courant;
            somme_poids = poids_courant;
            XYpoint = new double[points.GetLength(0),points.GetLength(1)]; /////////
           
            for (int i=1;i<rayon;i++)
            {
                poids_p = poids_courant * Math.Exp(- Math.Pow(((- i) / sigma_p), 2) / 2);
                poids[i] = poids_p;
                somme_poids += (poids_p * 2);
                
            }

           
            for (int jk = indice_deb;jk < indice_fin;jk++) 
            {
                Xpointt = points[jk, 0] * poids[0];
                ypointt = points[jk, 1] * poids[0];
               // Ypointt = points[jk, 2] * poids[1];

                for (int i = 1; i < rayon; i++)
                {
                    indice_voisin_gch = jk - i ;
                    if (indice_voisin_gch < 0)    //1
                        indice_voisin_gch = 0;         // 1
                    indice_voisin_drt = jk + i ;
                    if (indice_voisin_drt > points.GetLength(0)-1)
                        indice_voisin_drt = points.GetLength(0)-1;    //
                    Xpointt = Xpointt + (points[indice_voisin_gch, 0] * poids[i]) + (points[indice_voisin_drt, 0] * poids[i]); 
                    ypointt = ypointt + (points[indice_voisin_gch, 1] * poids[i]) + (points[indice_voisin_drt, 1] * poids[i]);
                }
                Xpointt = Xpointt / somme_poids;
                ypointt = ypointt / somme_poids;

                XYpoint[jk, 0] = Xpointt;                  
                XYpoint[jk, 1] = ypointt;
                //XYpoint = [XYpoint; Xpointt Ypointt];
                //System.//Console.WriteLine(XYpoint[jk, 0]);
            }
            //System.//Console.WriteLine(indice_deb);
            for (int jk = indice_deb; jk < indice_fin; jk++) // verifier indice
            {
                points[jk, 0] = XYpoint[jk - indice_deb , 0]; /////
                points[jk, 1] = XYpoint[jk - indice_deb , 1];
            }
            
            return points;
        }






        public double[,] Method_VS_filtre_lineaire_1_XYpoint(int rayon, double sigma_p, int indice_deb, int indice_fin, double[,] points) // verifier
        {
           poids_courant = 1 / (sigma_p * 2.5066);
            poids = new double[rayon]; 
            poids[0] = poids_courant;
            somme_poids = poids_courant;
            XYpoint = new double[points.GetLength(0),points.GetLength(1)]; /////////
           
            for (int i=1;i<rayon;i++)
            {
                poids_p = poids_courant * Math.Exp(- Math.Pow(((- i) / sigma_p), 2) / 2);
                poids[i] = poids_p;
                somme_poids += (poids_p * 2);
                
            }

            for (int jk = indice_deb; jk < indice_fin; jk++) /////
            {
                Xpointt = points[jk, 0] * poids[0];
                ypointt = points[jk, 1] * poids[0];
                // Ypointt = points[jk, 2] * poids[1];

                for (int i = 1; i < rayon; i++)
                {
                    indice_voisin_gch = jk - i ;
                    if (indice_voisin_gch < 0)    //1
                        indice_voisin_gch = 0;         // 1
                    indice_voisin_drt = jk + i ;
                    if (indice_voisin_drt > points.GetLength(0) - 1)
                        indice_voisin_drt = points.GetLength(0) - 1;    //
                    Xpointt = Xpointt + (points[indice_voisin_gch, 0] * poids[i]) + (points[indice_voisin_drt, 0] * poids[i]);
                    ypointt = ypointt + (points[indice_voisin_gch, 1] * poids[i]) + (points[indice_voisin_drt, 1] * poids[i]);
                }
                Xpointt = Xpointt / somme_poids;
                ypointt = ypointt / somme_poids;

                XYpoint[jk, 0] = Xpointt;                  //verifier indice
                XYpoint[jk, 1] = ypointt;
                //XYpoint = [XYpoint; Xpointt Ypointt];
                //System.//Console.WriteLine(XYpoint[jk, 0]);
            }


            return XYpoint;// verifier
        }
    }
}


