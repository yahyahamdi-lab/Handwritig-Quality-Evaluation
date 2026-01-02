using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{

    class Traitement_frequentiel_V3
    {
        double[,] transformee_de_Fourier_traite_du_Signal;
        int taille_du_Signal;
        double transformee_de_Fourier_traite_du_Signal_i, transformee_de_Fourier_du_Signal_i, reponse_frequentielle_formule_1, val_min, ic1, ic2;
        double[] Vect_reponse_frequentielle_formule_1;
        public double [,] method_Traitement_frequentiel_V3(double[,] transformee_de_Fourier_du_Signal, double f_Hz, double fc1, double fc2, double n, double Gain, string type_du_filtre)
        {
            transformee_de_Fourier_traite_du_Signal = new double[transformee_de_Fourier_du_Signal.GetLength(0), transformee_de_Fourier_du_Signal.GetLength(1)];
            taille_du_Signal = transformee_de_Fourier_du_Signal.GetLength(0);

            ic1 = (fc1 / f_Hz) * taille_du_Signal;
            ic2 = (fc2 / f_Hz) * taille_du_Signal;
            val_min = 0.5;

            for (int i=0; i < taille_du_Signal; i++)
            {
                if (type_du_filtre.Equals("Pass_Low"))
                {
                    reponse_frequentielle_formule_1 = 1 / Math.Sqrt(1 + (2 * Math.Pow((i / ic1), n)));
                    reponse_frequentielle_formule_1 = (0.95 * reponse_frequentielle_formule_1) + (1 - val_min + val_min);
                }
                else if (type_du_filtre.Equals("Cut__Low"))
                {
                    reponse_frequentielle_formule_1 = 1 - (1 / Math.Sqrt(1 + (2 * Math.Pow((i / ic1), n))));
                }
                else if (type_du_filtre.Equals("PassBand"))
                {
                    reponse_frequentielle_formule_1 = (1 / Math.Sqrt(1 + (2 * Math.Pow((i / ic2),n)))) - (1 / Math.Sqrt(1 + Math.Pow(2 * (i / ic1), n)));
                }
                else if (type_du_filtre.Equals("Cut_Band"))
                {
                    reponse_frequentielle_formule_1 = (1 / Math.Sqrt(1 + (2 * Math.Pow((i / ic1), n)))) + (1 - (1 / Math.Sqrt(1 + (2 * Math.Pow((i / ic2), n)))));
                }
                else if (type_du_filtre.Equals("AmplBand"))
                {
                    reponse_frequentielle_formule_1 = 1 + (Gain * ((1 / Math.Sqrt(1 + (2 * Math.Pow((i / ic2), n))) - (1 / Math.Sqrt(1 + (2 * Math.Pow((i / ic1),n)))))));
                }
                for (int j = 0; j < transformee_de_Fourier_du_Signal.GetLength(1); j++)
                {
                    transformee_de_Fourier_du_Signal_i = transformee_de_Fourier_du_Signal[i, j];

                    transformee_de_Fourier_traite_du_Signal_i = transformee_de_Fourier_du_Signal_i * reponse_frequentielle_formule_1;

                    transformee_de_Fourier_traite_du_Signal[i,j] = transformee_de_Fourier_traite_du_Signal_i;
                }

                //Vect_reponse_frequentielle_formule_1 = [Vect_reponse_frequentielle_formule_1; reponse_frequentielle_formule_1];
                //vect_frequence = [vect_frequence, i];

            }
            return transformee_de_Fourier_traite_du_Signal; 
        }
    }
}
