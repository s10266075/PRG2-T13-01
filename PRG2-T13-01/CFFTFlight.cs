using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number: S10266075F
// Student Name: Low Day Gene
// Student Number: S10266842H
// Partner Name: Goh Yu Chong Ansel 
//==========================================================

namespace PRG2_T13_01
{
    class CFFTFlight : Flight
    {
        public double RequestFee { get; set; }
        public CFFTFlight(string fn, string o, string d, DateTime et, double rf = 150) : base(fn, o, d, et)
        {
            RequestFee = rf;
        }
        public override double CalculateFees()
        {
            RequestFee = 150;

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
                    if (ExpectedTime.Hour < 11 || ExpectedTime.Hour > 21)
                    {
                        return RequestFee + 500 - 110; // Discount applied for flights arriving or departing outside of peak hours.
                    }
                    else
                    {
                        return RequestFee + 500;
                    }
                }
            }
            else
            {
                return 0.0; // No fees for flights not originating or ending in Singapore.
            }
        }
        public override string ToString()
        {
            return base.ToString() + $", Type: CFFT Flight, Additional Fee: {RequestFee}";
        }
    }
}
