using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{
    class read_matrice_globale
    {
        int i, j, col, nbre_ech;
        double[,] mmatrice_param_DFcar_des_echantillon, mmatrice_param_DFcar_des_echantillon_2;
        WRDFile readf = new WRDFile();
        double[][,] mat_pseudo_mot;
        int[] Vect;

        public int method_nombre_echantillon(string path)
        {
            // nombre échantillon
            mmatrice_param_DFcar_des_echantillon = readf.Method_ReadFile_Bin_Param_DFcar(path);
            nbre_ech = 0;
            i = 0;
            while (i != mmatrice_param_DFcar_des_echantillon.GetLength(1))
            {
                if (mmatrice_param_DFcar_des_echantillon[mmatrice_param_DFcar_des_echantillon.GetLength(0) - 1, i] == 1)
                    nbre_ech++;

                i++;
            }

            return nbre_ech;
        }
        public double[][,] method_read_matrice_globale(string path)
        {
            // nombre échantillon
            mmatrice_param_DFcar_des_echantillon = readf.Method_ReadFile_Bin_Param_DFcar(path);
            nbre_ech = 0;
            i = 0;
            while (i != mmatrice_param_DFcar_des_echantillon.GetLength(1))
            {
                if (mmatrice_param_DFcar_des_echantillon[mmatrice_param_DFcar_des_echantillon.GetLength(0) - 1, i] == 1)
                    nbre_ech++;

                i++;
            }
            i = 0;
            j = 0;
            Vect = new int[nbre_ech+1];
            while (i != mmatrice_param_DFcar_des_echantillon.GetLength(1))
            {
                if (mmatrice_param_DFcar_des_echantillon[mmatrice_param_DFcar_des_echantillon.GetLength(0) - 1, i] == 1)
                {
                    Vect[j] = i;
                    j++;
                }

                i++;
            }
            Vect[Vect.Length - 1] = mmatrice_param_DFcar_des_echantillon.GetLength(1);
            mat_pseudo_mot = new double[nbre_ech][,];
            for (int k = 0; k < nbre_ech; k++)
                mat_pseudo_mot[k]= new double[mmatrice_param_DFcar_des_echantillon.GetLength(0)-1, Vect[k+1] - Vect[k]];
            
            for (int k = 0; k < nbre_ech; k++)
            {
                col = 0;
                for (int jj = Vect[k]; jj < Vect[k + 1]; jj++)
                {
                  for (int ij = 0; ij < mmatrice_param_DFcar_des_echantillon.GetLength(0) - 1; ij++)                 
                     mat_pseudo_mot[k][ij, col] = mmatrice_param_DFcar_des_echantillon[ij, jj];

                  col++;

                }
                    
            }

                

                // construction matrice param

            return mat_pseudo_mot;
        }


        public int method_nombre_echantillon_BEM(string path)
        {
            // nombre échantillon
            mmatrice_param_DFcar_des_echantillon = readf.Method_ReadFile_Bin_Param(path);
            nbre_ech = 0;
            i = 0;
            while (i != mmatrice_param_DFcar_des_echantillon.GetLength(1))
            {
                if (mmatrice_param_DFcar_des_echantillon[mmatrice_param_DFcar_des_echantillon.GetLength(0) - 1, i] == 1)
                    nbre_ech++;

                i++;
            }

            return nbre_ech;
        }
        public double[][,] method_read_matrice_globale_BEM(string path)
        {
            // nombre échantillon
            mmatrice_param_DFcar_des_echantillon = readf.Method_ReadFile_Bin_Param(path);
            nbre_ech = 0;
            i = 0;
            while (i != mmatrice_param_DFcar_des_echantillon.GetLength(1))
            {
                if (mmatrice_param_DFcar_des_echantillon[mmatrice_param_DFcar_des_echantillon.GetLength(0) - 1, i] == 1)
                    nbre_ech++;

                i++;
            }
            i = 0;
            j = 0;
            Vect = new int[nbre_ech + 1];
            while (i != mmatrice_param_DFcar_des_echantillon.GetLength(1))
            {
                if (mmatrice_param_DFcar_des_echantillon[mmatrice_param_DFcar_des_echantillon.GetLength(0) - 1, i] == 1)
                {
                    Vect[j] = i;
                    j++;
                }

                i++;
            }
            Vect[Vect.Length - 1] = mmatrice_param_DFcar_des_echantillon.GetLength(1);
            mat_pseudo_mot = new double[nbre_ech][,];
            for (int k = 0; k < nbre_ech; k++)
                mat_pseudo_mot[k] = new double[mmatrice_param_DFcar_des_echantillon.GetLength(0) - 1, Vect[k + 1] - Vect[k]];

            for (int k = 0; k < nbre_ech; k++)
            {
                col = 0;
                for (int jj = Vect[k]; jj < Vect[k + 1]; jj++)
                {
                    for (int ij = 0; ij < mmatrice_param_DFcar_des_echantillon.GetLength(0) - 1; ij++)
                        mat_pseudo_mot[k][ij, col] = mmatrice_param_DFcar_des_echantillon[ij, jj];

                    col++;

                }

            }



            // construction matrice param

            return mat_pseudo_mot;
        }

        public int method_nombre_echantillon_DF(string path)
        {
            // nombre échantillon
            mmatrice_param_DFcar_des_echantillon = readf.Method_ReadFile_Bin_Param_DF(path);
            nbre_ech = 0;
            i = 0;
            while (i != mmatrice_param_DFcar_des_echantillon.GetLength(1))
            {
                if (mmatrice_param_DFcar_des_echantillon[mmatrice_param_DFcar_des_echantillon.GetLength(0) - 1, i] == 1)
                    nbre_ech++;

                i++;
            }

            return nbre_ech;
        }
        public double[][,] method_read_matrice_globale_DF(string path)
        {
            // nombre échantillon
            mmatrice_param_DFcar_des_echantillon = readf.Method_ReadFile_Bin_Param_DF(path);
            nbre_ech = 0;
            i = 0;
            while (i != mmatrice_param_DFcar_des_echantillon.GetLength(1))
            {
                if (mmatrice_param_DFcar_des_echantillon[mmatrice_param_DFcar_des_echantillon.GetLength(0) - 1, i] == 1)
                    nbre_ech++;

                i++;
            }
            i = 0;
            j = 0;
            Vect = new int[nbre_ech + 1];
            while (i != mmatrice_param_DFcar_des_echantillon.GetLength(1))
            {
                if (mmatrice_param_DFcar_des_echantillon[mmatrice_param_DFcar_des_echantillon.GetLength(0) - 1, i] == 1)
                {
                    Vect[j] = i;
                    j++;
                }

                i++;
            }
            Vect[Vect.Length - 1] = mmatrice_param_DFcar_des_echantillon.GetLength(1);
            mat_pseudo_mot = new double[nbre_ech][,];
            for (int k = 0; k < nbre_ech; k++)
                mat_pseudo_mot[k] = new double[mmatrice_param_DFcar_des_echantillon.GetLength(0) - 1, Vect[k + 1] - Vect[k]];

            for (int k = 0; k < nbre_ech; k++)
            {
                col = 0;
                for (int jj = Vect[k]; jj < Vect[k + 1]; jj++)
                {
                    for (int ij = 0; ij < mmatrice_param_DFcar_des_echantillon.GetLength(0) - 1; ij++)
                        mat_pseudo_mot[k][ij, col] = mmatrice_param_DFcar_des_echantillon[ij, jj];

                    col++;

                }

            }



            // construction matrice param

            return mat_pseudo_mot;
        }

    }
}
