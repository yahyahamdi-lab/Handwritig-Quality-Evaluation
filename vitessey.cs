using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{
   public class vitessey
    {
        
        double[] dt, dDy,VY;
        public double[] method_vitessex(double[] Y,double[]t)
        {
            dt = new double[t.Length - 1];
            for (int i = 0; i < t.Length; i++) // dDx = diff(XX);
                dt[i] = t[i + 1] - t[i];

            dDy = new double[Y.Length - 1];
            for (int i = 0; i < Y.Length; i++) // dDx = diff(XX);
                dDy[i] = Y[i + 1] - Y[i];

            VY = new double[Y.Length];
            VY[0] = 0;
            int ind = 1;
            for (int i = 0; i < dDy.Length; i++) // dDx = diff(XX);
            {
                VY[ind] = dDy[i] / dt[i];
                ind++;
            }
            VY[VY.Length -1] = 0;
            

            return VY;
        }
    }
}
