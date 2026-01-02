using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{
    class VS_angle_reyon_ellipse
    {
        public double ha, XXM,YYM,hai;
        public double Method_VS_angle_reyon_ellipse(double xM, double yM, double x0, double y0, double teta)
        {
            XXM = ((xM - x0) * Math.Cos(teta)) + ((yM - y0) * Math.Sin(teta));
            YYM = ((yM - y0) * Math.Cos(teta)) + ((x0 - xM) * Math.Sin(teta));
            if (XXM == 0)
               hai = Math.PI / 2;
            else
                hai = Math.Atan(Math.Abs(YYM) / Math.Abs(XXM));
            if (((Math.Sign(XXM) == 1) || (Math.Sign(XXM) == 0)) && ((Math.Sign(YYM) == 1) || (Math.Sign(YYM) == 0)))
                     ha = hai;
            else if ((Math.Sign(XXM) == -1) && (Math.Sign(YYM) == 1))
                ha = Math.PI - hai;
            else if(((Math.Sign(XXM) == -1) || (Math.Sign(XXM) == 0)) && ((Math.Sign(YYM) == -1) || (Math.Sign(YYM) == 0)))
                ha = hai + Math.PI;
            else if((Math.Sign(XXM) == 1) & (Math.Sign(YYM) == -1))
                ha = (2 * Math.PI) - hai;
            else
                ha = 0;

            return ha;
        }
    }
}
