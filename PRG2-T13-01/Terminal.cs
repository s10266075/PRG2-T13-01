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
            GateFees = new Dictionary<string, double>();
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
        public double CalculateFees()
        {
            double discount = 0;
            foreach (Airline airline in Airlines.Values)
            {
                int count = 0;
                if (airline.Flights.Count > 5)
                {
                    foreach (Flight f in airline.Flights.Values)
                    {
                        if (count % 3 == 0)
                        {
                            discount += 350;
                        }
                        discount += f.CalculateFees() * 0.03;
                        if (f.ExpectedTime.Hour < 11 || f.ExpectedTime.Hour > 21)
                        {
                            discount += 110;
                            if (f.Origin == "Singapore (SGP)")
                            {
                                if (f is NORMFlight)
                                {
                                    discount += 50;
                                    count++;
                                }
                                else
                                {
                                    count++;
                                }
                            }
                            else if (f.Origin == "Dubai (DXB)" || f.Origin == "Bangkok" || f.Origin == "Tokyo")
                            {
                                discount += 25;
                                if (f is NORMFlight)
                                {
                                    discount += 50;
                                    count++;
                                }
                                else
                                {
                                    count++;
                                }
                            }
                        }
                        else
                        {
                            if (f.Origin == "Singapore (SGP)")
                            {
                                if (f is NORMFlight)
                                {
                                    discount += 50;
                                    count++;
                                }
                                else
                                {
                                    count++;
                                }
                            }
                            else if (f.Origin == "Dubai (DXB)" || f.Origin == "Bangkok" || f.Origin == "Tokyo")
                            {
                                discount += 25;
                                if (f is NORMFlight)
                                {
                                    discount += 50;
                                    count++;
                                }
                                else
                                {
                                    count++;
                                }
                            }
                        }
                    }

                }
                else
                {
                    foreach (Flight f in airline.Flights.Values)
                    {
                        if (count % 3 == 0)
                        {
                            discount += 350;
                        }
                        if (f.ExpectedTime.Hour < 11 || f.ExpectedTime.Hour > 21)
                        {
                            discount += 110;
                            if (f.Origin == "Singapore (SGP)")
                            {
                                if (f is NORMFlight)
                                {
                                    discount += 50;
                                    count++;
                                }
                                else
                                {
                                    count++;
                                }
                            }
                            else if (f.Origin == "Dubai (DXB)" || f.Origin == "Bangkok" || f.Origin == "Tokyo")
                            {
                                discount += 25;
                                if (f is NORMFlight)
                                {
                                    discount += 50;
                                    count++;
                                }
                                else
                                {
                                    count++;
                                }
                            }
                        }
                        else
                        {
                            if (f.Origin == "Singapore (SGP)")
                            {
                                if (f is NORMFlight)
                                {
                                    discount += 50;
                                    count++;
                                }
                                else
                                {
                                    count++;
                                }
                            }
                            else if (f.Origin == "Dubai (DXB)" || f.Origin == "Bangkok" || f.Origin == "Tokyo")
                            {
                                discount += 25;
                                if (f is NORMFlight)
                                {
                                    discount += 50;
                                    count++;
                                }
                                else
                                {
                                    count++;
                                }
                            }
                        }
                    }
                }
            }
            foreach (Flight flight in Flights.Values)
            {
                if (flight.ExpectedTime.Hour < 11 || flight.ExpectedTime.Hour > 21)
                {
                    discount += (110 + ((flight.CalculateFees() + 800) * 0.03));
                }
            }
            return discount;
        }
        public void PrintAirlineFees()
        {

            foreach(BoardingGate b in BoardingGates.Values)
            {
                if (b.Flight == null)
                {
                    Console.WriteLine("There are flights which have not been assigned! Please assign all flights to a boarding gate first.");
                    //break;
                }
                else
                {
                    foreach (Airline a in Airlines.Values)
                    {
                        {
                            Console.WriteLine("Airline: " + a.Name + " (" + a.Code + ")");
                            Console.WriteLine("Total Fees: " + a.CalculateFees());
                        }

                    }
                }
            }
            

            foreach (Airline a in Airlines.Values)
            {
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


                    Console.WriteLine($"Original subtotal: {a.CalculateFees()}");
                }
            }

            Console.WriteLine();

        }
    }
}
