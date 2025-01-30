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
            double total = 300;
            if (Flight.Origin == "Singapore (SGP)")
            {
                total += 800;
            }
            else if (Flight.Destination == "Singapore (SGP)")
            {
                total += 500;
            }
            total+=Flight.CalculateFees();
            return total;

        }
        public override string ToString()
        {
            return "Gate Name: " + GateName + "\nSupports CFFT: " + SupportsCFFT + "\nSupports DDJB: " + SupportsDDJB + "\nSupports LWTT: " + SupportsLWTT + "\nFlight: " + Flight;
        }
    }
}
