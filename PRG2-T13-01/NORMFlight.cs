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
            double RequestFee = 0;
            if (Origin == "Singapore")
            {
                if (ExpectedTime.Hour < 11 || ExpectedTime.Hour > 21)
                {
                    return RequestFee + 800 - 110; // Discount applied for flights arriving or departing outside of peak hours.
                }
                else
                {
                    return RequestFee + 800;
                }
            }
            else if (Destination == "Singapore")
            {
                if (Origin == "Dubai" || Origin == "Bangkok" || Origin == "Tokyo")
                {
                    if (ExpectedTime.Hour < 11 || ExpectedTime.Hour > 21)
                    {
                        return RequestFee + 500 - 110 - 25; // Discount applied for flights arriving or departing outside of peak hours.
                    }
                    else
                    {
                        return RequestFee + 500 - 25;
                    }
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
