using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_T13_01
{
    class Airline
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string, Flight> Flights { get; set; }

        public Airline(string code, string name)
        {
            Code = code;
            Name = name;
            Flights = new Dictionary<string, Flight>();
        }
        public void AddFlight(Flight flight)
        {
            Flights[flight.FlightNumber] = flight;
        }
        public double CalculateFees()
        {
            double fees = 0;
            foreach (Flight flight in Flights.Values)
            {
                fees += flight.CalculateFees();
            }
            return fees;

        }
        public override string ToString()
        {
            return $"{Name} ({Code})";
        }
    }
}
