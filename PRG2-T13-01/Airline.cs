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
        public bool AddFlight(Flight flight)
        {
            if (!Flights.ContainsKey(flight.FlightNumber))
            {
                Flights[flight.FlightNumber] = flight;
                return true;
            }
            return false;
        }

        public double CalculateFees()
        {
            double total = 0;
            double discount = 0;
            if (Flights.Count > 5)
            {
                int count = 0;
                if (count% 3 == 0)
                {
                    discount += 350;
                }
                foreach (Flight flight in Flights.Values)
                {
                    if (flight.Origin == "Singapore")
                    {
                        total += (flight.CalculateFees() + 800);
                        if (flight.ExpectedTime.Hour < 11 || flight.ExpectedTime.Hour > 21)
                        {
                            discount += (110+((flight.CalculateFees() + 800) * 0.03));
                        }
                    }
                    else
                    {
                        total += (flight.CalculateFees() + 500);
                        if (flight.ExpectedTime.Hour < 11 || flight.ExpectedTime.Hour > 21)
                        {
                            if (flight.Origin == "Dubai" || flight.Origin == "Bangkok" || flight.Origin == "Tokyo")
                            {
                                discount += (110 + 25 + ((flight.CalculateFees() + 500) * 0.03));
                            }
                            else
                            {
                                total += (110 + ((flight.CalculateFees() + 500) * 0.03));
                            }
                        }
                        else
                        {
                            if (flight.Origin == "Dubai" || flight.Origin == "Bangkok" || flight.Origin == "Tokyo")
                            {
                                discount += 25;
                            }
                        }
                    }
                    count++;
                }
                return total;
            }
            else
            {
                int count = 0;
                if (count % 3 == 0)
                {
                    discount += 350;
                }
                foreach (Flight flight in Flights.Values)
                {
                    if (flight.Origin == "Singapore")
                    {
                        total += ((flight.CalculateFees() + 800));
                        if (flight.ExpectedTime.Hour < 11 || flight.ExpectedTime.Hour > 21)
                        {
                            discount += (110);
                        }
                    }
                    else
                    {
                        total += (flight.CalculateFees() + 500);
                        if (flight.ExpectedTime.Hour < 11 || flight.ExpectedTime.Hour > 21)
                        {
                            if (flight.Origin == "Dubai" || flight.Origin == "Bangkok" || flight.Origin == "Tokyo")
                            {
                                discount += (110 + 25);
                            }
                            else
                            {
                                discount += 110;
                            }
                        }
                        else
                        {
                            if (flight.Origin == "Dubai" || flight.Origin == "Bangkok" || flight.Origin == "Tokyo")
                            {
                                discount += 25;
                            }
                        }
                    }
                }
                return total;
            }
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
