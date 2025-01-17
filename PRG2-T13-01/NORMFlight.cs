using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_T13_01
{
    class NORMFlight:Flight
    {
        public NORMFlight(string fn, string o, string d, DateTime et, string s) : base(fn, o, d, et, s) { }
        public override double CalculateFees()
        {
            if (Origin == "Singapore")
            {
                double fees = 800;

                return fees;
            }
            else if(Destination == "Singapore")
            {
                if (Origin == "Dubai" || Origin == "Bangkok" || Origin == "Tokyo")
                {
                    double fees = 500 - 25;
                    return fees;
                }
                else
                {
                    double fees = 500;
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
            return base.ToString() + ", Type: Normal Flight";
        }

    }
}
