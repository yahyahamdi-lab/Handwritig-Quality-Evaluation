using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Beta_elliptic_model
{
    public class write_matrice_globale
    {
        double[,] mmatrice_param_DFcar_des_echantillon, mmatrice_param_DFcar_des_echantillon_2;
        WRDFile readf = new WRDFile();
        int ind;
        public void method_write_matrice_globale(string path, double[,] matrice_param)
        {
                        
            if(!System.IO.File.Exists(path))
            {

              FileStream fs = new FileStream(path, FileMode.Create);
              BinaryWriter bw = new BinaryWriter(fs);

              using (var binarystream = new BinaryWriter(fs))
              {
                 foreach (double i in matrice_param)
                  {
                     binarystream.Write(i);
                  }


              }
                bw.Close();
            }
            else
            {
              mmatrice_param_DFcar_des_echantillon = readf.Method_ReadFile_Bin_Param_DFcar(path);
              mmatrice_param_DFcar_des_echantillon_2 = new double[50, mmatrice_param_DFcar_des_echantillon.GetLength(1)+ matrice_param.GetLength(1)];
              for(int i = 0; i< mmatrice_param_DFcar_des_echantillon.GetLength(1); i++)
              {
                    for (int j = 0; j < mmatrice_param_DFcar_des_echantillon.GetLength(0); j++)
                        mmatrice_param_DFcar_des_echantillon_2[j,i] = mmatrice_param_DFcar_des_echantillon[j,i];
              }

               ind = mmatrice_param_DFcar_des_echantillon.GetLength(1);
               for (int i = 0; i < matrice_param.GetLength(1); i++)
               {
                    for (int j = 0; j < matrice_param.GetLength(0); j++)
                        mmatrice_param_DFcar_des_echantillon_2[j, ind] = matrice_param[j, i];
                    ind++;
               }

                readf.Method_WriteFile_Bin(path, mmatrice_param_DFcar_des_echantillon_2);
            }
   
    }


        public void method_write_matrice_globale_DF(string path, double[,] matrice_param)
        {

            if (!System.IO.File.Exists(path))
            {

                FileStream fs = new FileStream(path, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);

                using (var binarystream = new BinaryWriter(fs))
                {
                    foreach (double i in matrice_param)
                    {
                        binarystream.Write(i);
                    }


                }
                bw.Close();
            }
            else
            {
                mmatrice_param_DFcar_des_echantillon = readf.Method_ReadFile_Bin_Param_DF(path);
                mmatrice_param_DFcar_des_echantillon_2 = new double[36, mmatrice_param_DFcar_des_echantillon.GetLength(1) + matrice_param.GetLength(1)];
                for (int i = 0; i < mmatrice_param_DFcar_des_echantillon.GetLength(1); i++)
                {
                    for (int j = 0; j < mmatrice_param_DFcar_des_echantillon.GetLength(0); j++)
                        mmatrice_param_DFcar_des_echantillon_2[j, i] = mmatrice_param_DFcar_des_echantillon[j, i];
                }

                ind = mmatrice_param_DFcar_des_echantillon.GetLength(1);
                for (int i = 0; i < matrice_param.GetLength(1); i++)
                {
                    for (int j = 0; j < matrice_param.GetLength(0); j++)
                        mmatrice_param_DFcar_des_echantillon_2[j, ind] = matrice_param[j, i];
                    ind++;
                }

                readf.Method_WriteFile_Bin(path, mmatrice_param_DFcar_des_echantillon_2);
            }

        }

        public void method_write_matrice_globale_BEM(string path, double[,] matrice_param)
        {

            if (!System.IO.File.Exists(path))
            {

                FileStream fs = new FileStream(path, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);

                using (var binarystream = new BinaryWriter(fs))
                {
                    foreach (double i in matrice_param)
                    {
                        binarystream.Write(i);
                    }


                }
                bw.Close();
            }
            else
            {
                mmatrice_param_DFcar_des_echantillon = readf.Method_ReadFile_Bin_Param(path);
                mmatrice_param_DFcar_des_echantillon_2 = new double[17, mmatrice_param_DFcar_des_echantillon.GetLength(1) + matrice_param.GetLength(1)];
                for (int i = 0; i < mmatrice_param_DFcar_des_echantillon.GetLength(1); i++)
                {
                    for (int j = 0; j < mmatrice_param_DFcar_des_echantillon.GetLength(0); j++)
                        mmatrice_param_DFcar_des_echantillon_2[j, i] = mmatrice_param_DFcar_des_echantillon[j, i];
                }

                ind = mmatrice_param_DFcar_des_echantillon.GetLength(1);
                for (int i = 0; i < matrice_param.GetLength(1); i++)
                {
                    for (int j = 0; j < matrice_param.GetLength(0); j++)
                        mmatrice_param_DFcar_des_echantillon_2[j, ind] = matrice_param[j, i];
                    ind++;
                }

                readf.Method_WriteFile_Bin(path, mmatrice_param_DFcar_des_echantillon_2);
            }

        }
    }

}



