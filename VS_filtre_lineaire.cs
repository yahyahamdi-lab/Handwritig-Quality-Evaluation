using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{
    class VS_filtre_lineaire
    {
        /*
         Cette fonction permet d'appliquer un filtrage de lissage sur le vecteur de données 'signal' en coulissant une fenêtre gaussiènne de rayon 'rayon'     
        d'écart type 'sigma_p'. les données filtées sont retournee dans 'signal_lisse'
         */
        public double poids_courant, somme_poids, poids_p, signal_lisse_jk;
        public double[] poids, signal_lisse;
        public int indice_voisin_gch, indice_voisin_drt;
        public double[] method_VS_filtre_lineaire(int rayon, double sigma_p, double[] signal)
        {
            poids = new double[rayon]; 
            signal_lisse = new double [signal.Length];

            poids_courant = 1 / (sigma_p * 2.5066);
            poids[0] = poids_courant;
            somme_poids = poids_courant;
            for (int i = 1; i < rayon; i++)
            {
                poids_p = poids_courant * Math.Exp(-Math.Pow(((- i) / sigma_p), 2) / 2);
                poids[i] = poids_p;
                somme_poids += (poids_p * 2);

            }
            signal_lisse[0] = signal[0];
            for (int jk = 1; jk < signal.Length - 1; jk++) ////
            {
                signal_lisse_jk = signal[jk] * poids[0];
                for (int i = 1; i < rayon; i++)
                {
                    indice_voisin_gch = jk - i ;
                    if (indice_voisin_gch < 0)
                        indice_voisin_gch = 0;
                    indice_voisin_drt = jk + i ;
                    if (indice_voisin_drt > signal.Length-1)
                        indice_voisin_drt = signal.Length-1;
                    signal_lisse_jk = signal_lisse_jk + (signal[indice_voisin_gch] * poids[i]) + (signal[indice_voisin_drt] * poids[i]);
                }
                signal_lisse_jk = signal_lisse_jk / somme_poids;
                signal_lisse[jk] = signal_lisse_jk; //signal_lisse = [signal_lisse; signal_lisse_jk];

            }

            signal_lisse [signal_lisse.Length-1] =  signal[signal.Length-1]; ///  ????
            return signal_lisse;
        }
    }
}
