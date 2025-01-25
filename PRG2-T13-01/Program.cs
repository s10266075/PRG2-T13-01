// See https://aka.ms/new-console-template for more information
using PRG2_T13_01;
using System.Collections.Generic;

//==========================================================
// Student Number: S10266075F
// Student Name: Low Day Gene
// Student Number: S10266842H
// Partner Name: Goh Yu Chong Ansel 
//==========================================================
Terminal terminal = new Terminal("Terminal 5");

//feature 1
Dictionary<string, Airline> airlineDict = new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> boardingGatesDict = new Dictionary<string, BoardingGate>();

void LoadAirlines()
{
    using (StreamReader sr = new StreamReader("airlines.csv"))
    {
        sr.ReadLine();
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            string[] parts = line.Split(',');
            string code = parts[1];
            string name = parts[0];
            Airline addairline = new Airline(code, name);
            airlineDict.Add(code, addairline);
            terminal.AddAirline(addairline);
        }
    }
}

void LoadBoardingGates()
{
    using (StreamReader sr = new StreamReader("boardinggates.csv"))
    {
        sr.ReadLine();
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            string[] parts = line.Split(',');
            string gateName = parts[0];
            bool supportsDDJB = bool.Parse(parts[1]);
            bool supportsCFFT = bool.Parse(parts[2]);
            bool supportsLWTT = bool.Parse(parts[3]);
            Flight gateFlight = null;
            BoardingGate addGate = new BoardingGate(gateName, supportsCFFT, supportsDDJB, supportsLWTT, gateFlight);
            boardingGatesDict.Add(gateName, addGate);
        }
    }
}

LoadAirlines();
LoadBoardingGates();

//feature 2
Dictionary<string, Flight> flightDict = new Dictionary<string, Flight>();
void LoadFlights()
{
    using(StreamReader sr = new StreamReader("flights.csv"))
    {
        sr.ReadLine();
        string line;
        while((line = sr.ReadLine()) != null)
        {
            string[] parts = line.Split(",");
            string flightnum = parts[0];
            string origin = parts[1];
            string Destination = parts[2];
            DateTime expectedTime = DateTime.Parse(parts[3]);
            string? requestCode = parts[4];
            if (requestCode == "DDJB")
            {
                Flight addFlight = new DDJBFlight(flightnum, origin, Destination, expectedTime);

                flightDict.Add(flightnum, addFlight);
            }
            else if (requestCode == "CFFT")
            {
                Flight addFlight = new CFFTFlight(flightnum, origin, Destination, expectedTime);
                flightDict.Add(flightnum, addFlight);
            }
            else if (requestCode == "LWTT")
            {
                Flight addFlight = new LWTTFlight(flightnum, origin, Destination, expectedTime);
                flightDict.Add(flightnum, addFlight);
            }
            else
            {
                Flight addFlight = new NORMFlight(flightnum, origin, Destination, expectedTime);
                flightDict.Add(flightnum, addFlight);
            }
        }
    }
}

LoadFlights();
//feature 3
void DisplayInfo()
{
    Console.WriteLine("=============================================\n" +
        "List of Flights for Changi Airport Terminal 5\n"+
        "=============================================\n") ;
    Console.WriteLine("{0, -15}{1,-27}{2,-23}{3,-23}{4,-10}","Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time");
    foreach (KeyValuePair<string, Flight> flight in flightDict)
    {
        Console.WriteLine("{0, -15}{1,-27}{2,-23}{3,-23}{4,-10}", flight.Key, terminal.GetAirlineFromFlight(flight.Value), flight.Value.Origin, flight.Value.Destination, flight.Value.ExpectedTime);
    }
}

//feature 5
void AssignGateToFlight()
{
    Console.Write("Enter the flight number: ");
    string flightnum = Console.ReadLine();
    Console.Write("Enter the gate name: ");
    string gateName = Console.ReadLine();
    if (flightDict.ContainsKey(flightnum) && boardingGatesDict.ContainsKey(gateName))
    {
        if (!(boardingGatesDict.ContainsKey(gateName)))
        {
            boardingGatesDict[gateName].Flight = flightDict[flightnum];
            Console.WriteLine("Flight has been assigned to the gate!");
        }
        else
        {
            Console.WriteLine("Gate is already occupied!");
        }
    }
    else
    {
        Console.WriteLine("Gate does not exist!");
    }
}

//feature 6
Flight CreateNewFlight()
{
    Console.Write("Enter the flight number: ");
    string flightnum = Console.ReadLine();
    Console.Write("Enter the origin: ");
    string origin = Console.ReadLine();
    Console.Write("Enter the destination: ");
    string destination = Console.ReadLine();
    Console.Write("Enter the expected time (): ");
    DateTime expectedTime = DateTime.Parse(Console.ReadLine());
    Flight newFlight = new NORMFlight(flightnum, origin, destination, expectedTime);
    return newFlight;
}

//feature 9
void SortFlights()
{
    List<Flight> sortList = new List<Flight>();
    foreach (Flight f in flightDict.Values)
    {
        sortList.Add(f);
        sortList.Sort();
    }
    Console.WriteLine(" {0, -15}{1,-23}{2,-23}{3,-23}{4,-10}", "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time");
    foreach (Flight f in sortList)
    {
        Console.WriteLine(" {0, -15}{1,-23}{2,-23}{3,-23}{4,-10}", f.FlightNumber, terminal.GetAirlineFromFlight(f), f.Origin, f.Destination, f.ExpectedTime);
    }
}

//menu
while (true)
{
    Console.WriteLine("=============================================\r\n" +
        "Welcome to Changi Airport Terminal 5\r\n" +
        "=============================================\r\n" +
        "1. List All Flights\r\n" +
        "2. List Boarding Gates\r\n" +
        "3. Assign a Boarding Gate to a Flight\r\n" +
        "4. Create Flight\r\n" +
        "5. Display Airline Flights\r\n" +
        "6. Modify Flight Details\r\n" +
        "7. Display Flight Schedule\r\n" +
        "0. Exit\r\n" +
        "Please select your option:");
    int choice = int.Parse(Console.ReadLine());
    if (choice == 1)
    {
        DisplayInfo();
    }
    else if (choice == 2)
    {
        Console.WriteLine();
    }
    else if (choice == 3)
    {
        AssignGateToFlight();
    }
    else if (choice == 4)
    {
        CreateNewFlight();
    }
    else if (choice == 5)
    {
        Console.WriteLine();
    }
    else if (choice == 6)
    {
        Console.WriteLine();
    }
    else if (choice == 7)
    {
        SortFlights();
    }
    else if (choice == 0)
    {
        break;
    }
}




