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
        public bool AddFlight(Flight flight)
        {
            if (!Flights.ContainsKey(flight.FlightNumber))
            {
                Flights[flight.FlightNumber] = flight;
                return true;
            }
            return false;
        }
        /*public double CalculateFees()
        {
            foreach (Flight flight in Flights.Values)
            {
                fees += flight.CalculateFees();
            }
        public bool RemoveFlight(Flight flight)
        {
            return Flights.Remove(flight.FlightNumber);
        }
        public override string ToString()
        {
            return $"{Name} ({Code})";
        }
    }
}
