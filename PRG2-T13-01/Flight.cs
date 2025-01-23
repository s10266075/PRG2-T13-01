using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_T13_01
{
    class Flight
    {
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }

        public Flight (string fn, string o, string d, DateTime et, string s) 
        {
            FlightNumber = fn;
            Origin = o;
            Destination = d;
            ExpectedTime = et;
            Status = "Scheduled";    
        }
        public virtual double CalculateFees() { return 0.0; }
        public override string ToString()
        {
            return "Flight Number: " + FlightNumber + "\nOrigin: " + Origin + "\nDestination: " + Destination + "\nExpected Time: " + ExpectedTime + "\nStatus: " + Status;
        }
    }
}
