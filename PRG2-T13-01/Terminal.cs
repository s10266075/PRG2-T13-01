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
        public bool AddFlight(Flight f)
        {
            try
            {
                Flights.Add(f.FlightNumber, f);
                return true;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("This flight has already been added!");
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
            foreach (Flight fl in Flights.Values)
            {
                if(fl.Gate == false)
                {
                    Console.WriteLine("Not all flights have been assigned to a gate! Please assign all flights to a gate before trying again.");
                    return;
                }
            }
            Console.WriteLine("==============================================\nAirline Fees for Changi Airport Terminal 5\n==============================================");
            Console.WriteLine("{0,-15}{1,-20}{2,-15}{3,-20}{4}","Airline Code","Airline Name","Subtotal Fees", "Discounts", "Final Fees");
            double total = 0; 
            double totaldiscount = 0;
            double grandtotal = 0;
            foreach (Airline a in Airlines.Values)
            {
                double airlinetotal = a.CalculateFees();
                double discount = 0;
                double finalvalue = 0;
                int count = 1;
                foreach (BoardingGate b in BoardingGates.Values)
                {
                    if (a.Flights.ContainsValue(b.Flight))
                    {
                        airlinetotal += b.CalculateFees();
                    }
                }
                foreach (Flight f in a.Flights.Values)
                {
                    if (count % 3 == 0 && count > 0)
                    {
                        discount += 350;
                    }
                    if (f.ExpectedTime.Hour < 11 || (f.ExpectedTime.Hour > 20 && f.ExpectedTime.Minute>0))
                    {
                        discount += 110;
                    }
                    if (f is NORMFlight)
                    {
                        discount += 50;
                    }
                    if (f.Origin == "Dubai (DXB)" || f.Origin == "Bangkok (BKK)" || f.Origin == "Tokyo (NRT)")
                    {
                        discount += 25;
                    }
                    if(a.Flights.Values.Count > 5)
                    {
                        discount+=((f.CalculateFees())*0.03);
                    }
                    count++;
                }
                total += airlinetotal;
                finalvalue += (airlinetotal - discount);
                totaldiscount += discount;
                grandtotal += finalvalue;
                Console.WriteLine("{0,-15}{1,-20}{2,-15}{3,-20}{4}", a.Code, a.Name, airlinetotal.ToString("C2"), discount.ToString("C2"), finalvalue.ToString("C2"));
            }
            Console.WriteLine($"Subtotal of all Airline fees: {total.ToString("C2")}");
            Console.WriteLine($"Subtotal of all Airline discounts: {totaldiscount.ToString("C2")}");
            Console.WriteLine($"Grand total of Airline fees: {grandtotal.ToString("C2")}");
            Console.WriteLine($"Percentage of the subtotal discounts over final fees: {((totaldiscount / grandtotal) * 100).ToString("F2")}%");
        }
    }
}


