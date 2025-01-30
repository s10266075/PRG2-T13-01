using System;

//==========================================================
// Student Number: S10266075F
// Student Name: Low Day Gene
// Student Number: S10266842H
// Partner Name: Goh Yu Chong Ansel 
//==========================================================

namespace PRG2_T13_01
{
    class LWTTFlight : Flight
    {
        public double RequestFee { get; set; }

        public LWTTFlight(string fn, string o, string d, DateTime et, double rf = 500) 
            : base(fn, o, d, et)
        {
            RequestFee = rf;
        }

        public override double CalculateFees()
        {
            if (Origin == "Singapore")
            {
                if (ExpectedTime.Hour < 11 || ExpectedTime.Hour > 21)
                {
                    return RequestFee+800-110; // Discount applied for flights arriving or departing outside of peak hours.
                }
                else
                {
                    return RequestFee + 800;
                }    
        public override string ToString()
        {
            return base.ToString() + $", Type: LWTT Flight, Additional Fee: {RequestFee}";
        }
    }
}