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
    class DDJBFlight : Flight
    {
        public double RequestFee { get; set; }
        public DDJBFlight(string fn, string o, string d, DateTime et, string s = "Scheduled", double rf = 300) : base(fn, o, d, et,s)
        {
            RequestFee = rf;
        }
        public override double CalculateFees()
        {
            double total = base.CalculateFees();
            RequestFee = 300;
            total += RequestFee;
            return total;
        }
        public override string ToString()
        {
            return base.ToString() + $", Type: DDJB Flight, Additional Fee: {RequestFee}";
        }
    }
}
    
