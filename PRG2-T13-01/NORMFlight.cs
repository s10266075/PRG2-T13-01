using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_T13_01
{
    class NORMFlight:Flight
    {
        public NORMFlight(string fn, string o, string d, DateTime et) : base(fn, o, d, et) { }
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
