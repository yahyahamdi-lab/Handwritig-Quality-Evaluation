using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{
    class VS_parametres_quart_ellipse
    {
        /*
         * Cette fonction permet de calculer les paramètres de l'équation du quart d'ellipse modélisant 
         un segment de la trajectoire dont le grand axe ait la direction de la tangente au point limite correspondant au maximum de vitesse 
         */
        public int drap;
        public double xMG, yMG, xMP, yMP, alfa1, alfa2, AA1,CC1, AA2,CC2,x0,y0,a,b, haP, haG, det;
        public double[] BB, HA, K;
        public double[,] AA, compx, compxT,inv, inverse;
        VS_angle_reyon_ellipse angle;
       public double Method_a(double [] M_2points, double[] M_2KMH, double teta, int num_fig, int RRR)
        {
            K = new double[2];
            xMG = M_2points[0];
            yMG = M_2points[1];
            xMP = M_2points[2];
            yMP = M_2points[3];
            BB = new double[2];
            AA = new double[2,2];
            inverse = new double[2, 2];
            if (teta == (Math.PI / 2))
                teta = (Math.PI / 2) - (1.0000e-10);
            else if (teta == -(Math.PI / 2))
               teta = -(Math.PI / 2) + (1.0000e-10);
            else if (teta == 0)
               teta = 0 - (1.0000e-10);
            else if (teta == -Math.PI)
               teta = (-Math.PI) + (1.0000e-10);

            alfa1 = teta + (Math.PI / 2);
            alfa2 = teta;

            AA1 = Math.Tan(alfa1);
            CC1 = yMG - (AA1 * xMG);

            AA2 = Math.Tan(alfa2);  
            CC2 = yMP - (AA2 * xMP);

            AA[0, 0] = AA1;  // AA = [AA1 - 1; AA2 - 1];
            AA[0, 1] = -1;
            AA[1, 0] = AA2;
            AA[1, 1] = -1;

            
            BB[0] = -CC1; // BB = [-CC1; -CC2];
            BB[1] = -CC2;


            inverse = method_inv(AA);
            // K = method_inv(AA) * BB;
            K[0] = inverse[0, 0] * BB[0] + inverse[0,1]*BB[1];
            K[1] = inverse[1, 0] * BB[0] + inverse[1, 1] * BB[1];
            x0 = K[0];
            y0 = K[1];

            a = Math.Sqrt(Math.Pow((xMP - x0) ,2) + Math.Pow((yMP - y0), 2));

            return a;
            
        }


        public double[,] method_inv(double[,] m)
        {
            compx = new double[m.Length / m.Rank, m.Rank];
            compxT = new double[m.Length / m.Rank, m.Rank];
            inv = new double[m.Length / m.Rank, m.Rank];
            det = m[0, 0] * m[1, 1] - m[0, 1] * m[1, 0];  // déterminant
            compx[0, 0] = m[1, 1]; //compx =[x(2, 2) - x(2, 1); -x(1, 2) x(1, 1) ]; complèment
            compx[0, 1] = -m[1, 0];
            compx[1, 0] = -m[0, 1];
            compx[1, 1] = m[0, 0];
            // transposé du complèment
            compxT[0,0] = compx[0, 0];
            compxT[0, 1] = compx[1, 0];
            compxT[1, 0] = compx[0, 1];
            compxT[1, 1] = compx[1, 1];
            // inverse A
            inv[0,0] = 1/det * compxT[0, 0];
            inv[0, 1] = 1/det * compxT[0, 1];
            inv[1, 0] = 1/det * compxT[1, 0];
            inv[1, 1] = 1/det * compxT[1, 1];

            return inv;
        }



    public double Method_b(double[] M_2points, double[] M_2KMH, double teta, int num_fig, int RRR)
        {
            xMG = M_2points[0];
            yMG = M_2points[1];
            xMP = M_2points[2];
            yMP = M_2points[3];
            BB = new double[2];
            if (teta == (Math.PI / 2))
                teta = (Math.PI / 2) - (1.0000e-10);
            else if (teta == -(Math.PI / 2))
                teta = -(Math.PI / 2) + (1.0000e-10);
            else if (teta == 0)
                teta = 0 - (1.0000e-10);
            else if (teta == -Math.PI)
                teta = (-Math.PI) + (1.0000e-10);

            alfa1 = teta + (Math.PI / 2);
            alfa2 = teta;

            AA1 = Math.Tan(alfa1);
            CC1 = yMG - (AA1 * xMG);

            AA2 = Math.Tan(alfa2);
            CC2 = yMP - (AA2 * xMP);


            AA[0, 0] = AA1;  // AA = [AA1 - 1; AA2 - 1];
            AA[0, 1] = -1;
            AA[1, 0] = AA2;
            AA[1, 1] = -1;

            BB[0] = -CC1; // BB = [-CC1; -CC2];
            BB[1] = -CC2;


            inverse = method_inv(AA);
            // K = method_inv(AA) * BB;
            K[0] = inverse[0, 0] * BB[0] + inverse[0, 1] * BB[1];
            K[1] = inverse[1, 0] * BB[0] + inverse[1, 1] * BB[1];

            x0 = K[0];
            y0 = K[1];

            a = Math.Sqrt(Math.Pow((xMP - x0), 2) + Math.Pow((yMP - y0), 2));
            b = Math.Sqrt(Math.Pow((xMG - x0), 2) + Math.Pow((yMG - y0), 2));

            return b;

        }


        public double Method_x0(double[] M_2points, double[] M_2KMH, double teta, int num_fig, int RRR)
        {
            xMG = M_2points[0];
            yMG = M_2points[1];
            xMP = M_2points[2];
            yMP = M_2points[3];
            BB = new double[2];
            if (teta == (Math.PI / 2))
                teta = (Math.PI / 2) - (1.0000e-10);
            else if (teta == -(Math.PI / 2))
                teta = -(Math.PI / 2) + (1.0000e-10);
            else if (teta == 0)
                teta = 0 - (1.0000e-10);
            else if (teta == -Math.PI)
                teta = (-Math.PI) + (1.0000e-10);

            alfa1 = teta + (Math.PI / 2);
            alfa2 = teta;

            AA1 = Math.Tan(alfa1);
            CC1 = yMG - (AA1 * xMG);

            AA2 = Math.Tan(alfa2);
            CC2 = yMP - (AA2 * xMP);


            AA[0, 0] = AA1;  // AA = [AA1 - 1; AA2 - 1];
            AA[0, 1] = -1;
            AA[1, 0] = AA2;
            AA[1, 1] = -1;


            BB[0] = -CC1; // BB = [-CC1; -CC2];
            BB[1] = -CC2;


            inverse = method_inv(AA);
            // K = method_inv(AA) * BB;
            K[0] = inverse[0, 0] * BB[0] + inverse[0, 1] * BB[1];
            K[1] = inverse[1, 0] * BB[0] + inverse[1, 1] * BB[1];

            x0 = K[0];

            return x0;

        }

        public double Method_y0(double[] M_2points, double[] M_2KMH, double teta, int num_fig, int RRR)
        {
            xMG = M_2points[0];
            yMG = M_2points[1];
            xMP = M_2points[2];
            yMP = M_2points[3];
            BB = new double[2];
            if (teta == (Math.PI / 2))
                teta = (Math.PI / 2) - (1.0000e-10);
            else if (teta == -(Math.PI / 2))
                teta = -(Math.PI / 2) + (1.0000e-10);
            else if (teta == 0)
                teta = 0 - (1.0000e-10);
            else if (teta == -Math.PI)
                teta = (-Math.PI) + (1.0000e-10);

            alfa1 = teta + (Math.PI / 2);
            alfa2 = teta;

            AA1 = Math.Tan(alfa1);
            CC1 = yMG - (AA1 * xMG);

            AA2 = Math.Tan(alfa2);
            CC2 = yMP - (AA2 * xMP);


            AA[0, 0] = AA1;  // AA = [AA1 - 1; AA2 - 1];
            AA[0, 1] = -1;
            AA[1, 0] = AA2;
            AA[1, 1] = -1;


            BB[0] = -CC1; // BB = [-CC1; -CC2];
            BB[1] = -CC2;


            inverse = method_inv(AA);
            // K = method_inv(AA) * BB;
            K[0] = inverse[0, 0] * BB[0] + inverse[0, 1] * BB[1];
            K[1] = inverse[1, 0] * BB[0] + inverse[1, 1] * BB[1];

            y0 = K[1];


            return y0;

        }

        public double Method_teta(double[] M_2points, double[] M_2KMH, double teta, int num_fig, int RRR)
        {
            xMG = M_2points[0];
            yMG = M_2points[1];
            xMP = M_2points[2];
            yMP = M_2points[3];
            BB = new double[2];
            if (teta == (Math.PI / 2))
                teta = (Math.PI / 2) - (1.0000e-10);
            else if (teta == -(Math.PI / 2))
                teta = -(Math.PI / 2) + (1.0000e-10);
            else if (teta == 0)
                teta = 0 - (1.0000e-10);
            else if (teta == -Math.PI)
                teta = (-Math.PI) + (1.0000e-10);


            return teta;

        }

        public int Method_drap (double[] M_2points, double[] M_2KMH, double teta, int num_fig, int RRR)
        {
          
            drap = 1;


            return drap;

        }
        public double[] Method_HA(double[] M_2points, double[] M_2KMH, double teta, int num_fig, int RRR)
        {
            xMG = M_2points[0];
            yMG = M_2points[1];
            xMP = M_2points[2];
            yMP = M_2points[3];
            BB = new double[2];
            if (teta == (Math.PI / 2))
                teta = (Math.PI / 2) - (1.0000e-10);
            else if (teta == -(Math.PI / 2))
                teta = -(Math.PI / 2) + (1.0000e-10);
            else if (teta == 0)
                teta = 0 - (1.0000e-10);
            else if (teta == -Math.PI)
                teta = (-Math.PI) + (1.0000e-10);

            alfa1 = teta + (Math.PI / 2);
            alfa2 = teta;

            AA1 = Math.Tan(alfa1);
            CC1 = yMG - (AA1 * xMG);

            AA2 = Math.Tan(alfa2);
            CC2 = yMP - (AA2 * xMP);


            AA[0, 0] = AA1;  // AA = [AA1 - 1; AA2 - 1];
            AA[0, 1] = -1;
            AA[1, 0] = AA2;
            AA[1, 1] = -1;


            BB[0] = -CC1; // BB = [-CC1; -CC2];
            BB[1] = -CC2;


            inverse = method_inv(AA);
            // K = method_inv(AA) * BB;
            K[0] = inverse[0, 0] * BB[0] + inverse[0, 1] * BB[1];
            K[1] = inverse[1, 0] * BB[0] + inverse[1, 1] * BB[1];

            x0 = K[0];
            y0 = K[1];

            a = Math.Sqrt(Math.Pow((xMP - x0), 2) + Math.Pow((yMP - y0), 2));
            b = Math.Sqrt(Math.Pow((xMG - x0), 2) + Math.Pow((yMG - y0), 2));
            drap = 1;

            angle = new VS_angle_reyon_ellipse();
            haG = angle.Method_VS_angle_reyon_ellipse(xMG, yMG, x0, y0, teta);
            haP = angle.Method_VS_angle_reyon_ellipse(xMP, yMP, x0, y0, teta); // //[haP]= VS_angle_reyon_ellipse(xMP, yMP, x0, y0, teta);
            HA[0] = haG;   // HA = [haG, haP];
            HA[1] = haP;
            return HA;

        }
    }
}
