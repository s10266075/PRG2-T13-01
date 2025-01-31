using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_T13_01
{
    class Terminal
    {
        public string TerminalName { get; set; }
        public Dictionary<string, Airline> Airlines { get; set; }
        public Dictionary<string, Flight> Flights { get; set; }
        public Dictionary<string, BoardingGate> BoardingGates { get; set; }
        public Dictionary <string, double> GateFees { get; set; }

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
            
        }
    }
}
