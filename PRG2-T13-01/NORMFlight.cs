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
                double fees = 800+300;

                return fees;
            }
            else if(Destination == "Singapore")
            {
                double fees = 500+300;
                return fees;
            }
            else
            {
                return 0.0;
            }
        }
    }
}
