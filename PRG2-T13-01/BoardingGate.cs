using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_T13_01
{
    class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; }

        public BoardingGate(string gateName, bool supportsCFFT, bool supportsDDJB, bool supportsLWTT, Flight flight)
        {
            GateName = gateName;
            SupportsCFFT = supportsCFFT;
            SupportsDDJB = supportsDDJB;
            SupportsLWTT = supportsLWTT;
            Flight = flight;
        }
        public BoardingGate() { }
        public double CalculateFees()
        {
            if (Flight is CFFTFlight)
            {
                return Flight.CalculateFees() + 300.00;
            }
            else if (Flight is DDJBFlight)
            {
                return Flight.CalculateFees() + 300.00;
            }
            else if (Flight is LWTTFlight)
            {
                return Flight.CalculateFees() + 300.00;
            }
            else if (Flight is NORMFlight)
            {
                return Flight.CalculateFees() + 300.00;
            }
            else
            {
                return 0.0;
            }
        }
    }
}
