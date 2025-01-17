using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_T13_01
{
    class DDJBFlight : Flight
    {
        public double RequestFee { get; set; }
        public DDJBFlight(string fn, string o, string d, DateTime et, string s, double rf) : base(fn, o, d, et, s)
        {
            RequestFee = rf;
        }
        public override double CalculateFees()
        {
            RequestFee = 300;
            if (Origin == "Singapore")
            {
                double fees = RequestFee + 800;
                return fees;
            }
            else if (Destination == "Singapore")
            {
                if (Origin == "Dubai" || Origin == "Bangkok" || Origin == "Tokyo")
                {
                    double fees = RequestFee + 500 - 25;
                    return fees;
                }
                else
                {
                    double fees = RequestFee + 500;
                    return fees;
                }
            }
            else
            {
                return 0.0;
            }
        }
        public override string ToString()
        {
            return base.ToString() + $", Type: DDJB Flight, Additional Fee: {RequestFee}";
        }
    }
}
    
