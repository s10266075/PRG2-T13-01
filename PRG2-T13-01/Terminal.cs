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
    class Terminal
    {
        public string TerminalName { get; set; }
        public Dictionary<string, Airline> Airlines { get; set; }
        public Dictionary<string, Flight> Flights { get; set; }
        public Dictionary<string, BoardingGate> BoardingGates { get; set; }
        public Dictionary<string, double> GateFees { get; set; }

        public Terminal(string tn)
        {
            TerminalName = tn;
            Airlines = new Dictionary<string, Airline>();
            Flights = new Dictionary<string, Flight>();
            BoardingGates = new Dictionary<string, BoardingGate>();
            GateFees = new Dictionary<string, double> { };
        }

        public Terminal() { }
        public bool AddAirline(Airline a)
        {
            try
            {
                Airlines.Add(a.Code, a);
                return true;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("This airline has already been added!");
                return false;
            }
        }
        public bool AddBoardingGate(BoardingGate b)
        {
            try
            {
                BoardingGates.Add(b.GateName, b);
                return true;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("This boarding gate has already been added!");
                return false;
            }
        }
        public Airline GetAirlineFromFlight(Flight f)
        {
            string airlinecode = f.FlightNumber.Substring(0, 2);
            try
            {
                return Airlines[airlinecode];
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Airline not found!");
                return null;
            }
        }
        public void PrintAirlineFees()
        {
            foreach (Airline a in Airlines.Values)
            {
                Console.WriteLine($"Airline name: {a.Name}");
                foreach (Flight fl in a.Flights.Values)
                {
                    if (BoardingGates.ContainsKey(fl.FlightNumber))
                    {
                        Console.WriteLine("1");
                        BoardingGate gate = BoardingGates[fl.FlightNumber];
                        GateFees.Add(gate.GateName, gate.CalculateFees());
                    }
                    else
                    {
                        Console.WriteLine("Not all flights have been added to a gate! Please assign all flights to a gate before trying again.");
                        break;
                    }
                    if (a.Flights.Values.Count > 5)
                    {
                        double discount = 0;
                        int count = 0;
                        foreach (Flight f in a.Flights.Values)
                        {
                            if (count % 3 == 0 && count > 0)
                            {
                                discount += 350;
                            }
                            discount += f.CalculateFees() * 0.03;
                            if (f.ExpectedTime.Hour < 11 || f.ExpectedTime.Hour > 21)
                            {
                                discount += 110;
                            }
                            if (f.Origin == "Singapore (SGP)")
                            {
                                if (f is NORMFlight)
                                {
                                    discount += 50;
                                }
                            }
                            else if (f.Origin == "Dubai (DXB)" || f.Origin == "Bangkok" || f.Origin == "Tokyo")
                            {
                                discount += 25;
                                if (f is NORMFlight)
                                {
                                    discount += 50;
                                }
                            }
                            count++;
                        }
                        Console.WriteLine($"Original subtotal: ${a.CalculateFees().ToString("F2")}");
                        Console.WriteLine($"Total discount: ${discount.ToString("F2")}");
                        Console.WriteLine($"Total: ${(a.CalculateFees() - discount).ToString("F2")}");
                    }
                    else
                    {
                        double discount = 0;
                        int count = 0;
                        foreach (Flight f in a.Flights.Values)
                        {
                            if (BoardingGates.ContainsKey(f.FlightNumber))
                            {
                                BoardingGate gate = BoardingGates[f.FlightNumber];
                                GateFees.Add(gate.GateName, gate.CalculateFees());
                            }
                            else
                            {
                                Console.WriteLine("Not all flights have been added to a gate! Please assign all flights to a gate before trying again.");
                                break;
                            }
                            if (count % 3 == 0 && count > 0)
                            {
                                discount += 350;
                            }
                            if (f.ExpectedTime.Hour < 11 || f.ExpectedTime.Hour > 21)
                            {
                                discount += 110;
                            }
                            if (f.Origin == "Singapore (SGP)")
                            {
                                if (f is NORMFlight)
                                {
                                    discount += 50;
                                }
                            }
                            else if (f.Origin == "Dubai (DXB)" || f.Origin == "Bangkok (BKK)" || f.Origin == "Tokyo (NRT)")
                            {
                                discount += 25;
                                if (f is NORMFlight)
                                {
                                    discount += 50;
                                }
                            }
                            count++;
                        }
                        Console.WriteLine($"Original subtotal: ${a.CalculateFees().ToString("F2")}");
                        Console.WriteLine($"Total discount: ${discount.ToString("F2")}");
                        Console.WriteLine($"Total: ${(a.CalculateFees() - discount).ToString("F2")}\n");
                    }
                }
            }
        }
    }
}
