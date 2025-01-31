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
        public CFFTFlight(string fn, string o, string d, DateTime et, string s, double rf = 150) : base(fn, o, d, et,s)
        {
            RequestFee = rf;
        }
        public override double CalculateFees()
        {
            RequestFee = 150;
            return RequestFee;
        }
        public override string ToString()
        {
            return base.ToString() + $", Type: CFFT Flight, Additional Fee: {RequestFee}";
        }
    }
}
