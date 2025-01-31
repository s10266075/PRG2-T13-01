using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return RequestFee;
        }
        public override string ToString()
        {
            return base.ToString() + $", Type: CFFT Flight, Additional Fee: {RequestFee}";
        }
    }
}
