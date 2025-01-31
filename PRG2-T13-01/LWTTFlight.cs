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

        public LWTTFlight(string fn, string o, string d, DateTime et, string s,double rf = 500) 
            : base(fn, o, d, et,s)
        {
            RequestFee = rf;
        }

        public override double CalculateFees()
        {
            RequestFee = 500;
            return RequestFee;
        }
        public override string ToString()
        {
            return base.ToString() + $", Type: LWTT Flight, Additional Fee: {RequestFee}";
        }
    }
}