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
    class NORMFlight:Flight
    {
        public NORMFlight(string fn, string o, string d, DateTime et,string s = "Scheduled") : base(fn, o, d, et,s) { }
        public override double CalculateFees()
        {
            double RequestFee = 0;
            return RequestFee;
        }
     
        public override string ToString()
        {
            return base.ToString() + ", Type: Normal Flight";
        }

    }
}
