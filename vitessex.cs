using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{
   public class vitessex
    {
        
        double[] dt, dDx,VX;
        public double[] method_vitessex(double[] X,double[]t)
        {
            dt = new double[t.Length - 1];
            for (int i = 0; i < t.Length; i++) // dDx = diff(XX);
                dt[i] = t[i + 1] - t[i];

            dDx = new double[X.Length - 1];
            for (int i = 0; i < X.Length; i++) // dDx = diff(XX);
                dDx[i] = X[i + 1] - X[i];

            VX = new double[X.Length];
            VX[0] = 0;
            int ind = 1;
            for (int i = 0; i < dDx.Length; i++) // dDx = diff(XX);
            {
                VX[ind] = dDx[i] / dt[i];
                ind++;
            }
            VX[VX.Length -1] = 0;
            

            return VX;
        }
    }
}
