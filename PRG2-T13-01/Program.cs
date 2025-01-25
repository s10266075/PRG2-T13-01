// See https://aka.ms/new-console-template for more information
using PRG2_T13_01;
using System;
using System.Collections.Generic;

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
            string airlineCode = parts[4];
            Flight gateFlight = null;
            BoardingGate addGate = new BoardingGate(gateName, supportsCFFT, supportsDDJB, supportsLWTT, gateFlight);
            boardingGatesDict.Add(gateName, addGate);
        }
    }
}

//feature 4

void ListAllBoardingGates(Dictionary<string, BoardingGate> boardingGates)
{
    Console.WriteLine("===============================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport T5");
    Console.WriteLine("===============================================");
    Console.WriteLine("Gate Name\tDDJB\tCFFT\tLWTT\tAssigned Flight");
    foreach (var gate in boardingGates.Values)
    {
        Console.WriteLine($"{gate.GateName}\t{gate.SupportsDDJB}\t{gate.SupportsCFFT}\t{gate.SupportsLWTT}\t{gate.Flight}");
    }
}
//feature 7
void DisplayFlightsForAirline(Dictionary<string, Airline> airlines)
{
    
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Airline Code",-15}{"Airline Name",-30}");
    foreach (var airline in airlines.Values)
    {
        Console.WriteLine($"{airline.Code,-15}{airline.Name,-30}");
    }

    Console.Write("\nEnter Airline Code: ");
    string code = Console.ReadLine()?.ToUpper();

    if (!airlines.ContainsKey(code))
    {
        Console.WriteLine("Invalid Airline Code.");
        return;
    }

    var selectedAirline = airlines[code];

    Console.WriteLine("=============================================");
    Console.WriteLine($"List of Flights for {selectedAirline.Name}");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Flight Number",-15}{"Airline Name",-30}{"Origin",-20}{"Destination",-20}{"Expected",-20}");
    Console.WriteLine($"{"Departure/Arrival Time",-20}");

    foreach (var kvp in selectedAirline.Flights)
    {
        var flight = kvp.Value;
        Console.WriteLine($"{flight.FlightNumber,-15}{selectedAirline.Name,-30}{flight.Origin,-20}{flight.Destination,-20}{flight.ExpectedTime:dd/MM/yyyy hh:mm tt,-20}");
    }
}
//feature 8
void ModifyFlightDetails(Dictionary<string, Airline> airlines)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Airline Code",-15}{"Airline Name",-30}");
    foreach (var airline in airlines.Values)
    {
        Console.WriteLine($"{airline.Code,-15}{airline.Name,-30}");
    }

    Console.Write("Enter Airline Code: ");
    string code = Console.ReadLine()?.ToUpper();

    if (!airlines.ContainsKey(code))
    {
        Console.WriteLine("Invalid Airline Code.");
        return;
    }

    var selectedAirline = airlines[code];

    Console.WriteLine($"List of Flights for {selectedAirline.Name}");
    Console.WriteLine($"{"Flight Number",-15}{"Airline Name",-30}{"Origin",-20}{"Destination",-20}{"Expected",-25}");
    Console.WriteLine(new string('-', 95));

    foreach (var kvp in selectedAirline.Flights)
    {
        var flight = kvp.Value;
        Console.WriteLine($"{flight.FlightNumber,-15}{selectedAirline.Name,-30}{flight.Origin,-20}{flight.Destination,-20}{flight.ExpectedTime:dd/MM/yyyy hh:mm tt,-25}");
    }

    Console.Write("\nChoose an existing Flight to modify or delete: ");
    string flightNumber = Console.ReadLine()?.ToUpper();

    if (!selectedAirline.Flights.ContainsKey(flightNumber))
    {
        Console.WriteLine("Invalid Flight Number.");
        return;
    }

    var flightToModify = selectedAirline.Flights[flightNumber];

    Console.WriteLine("\n1. Modify Flight");
    Console.WriteLine("2. Delete Flight");
    Console.Write("Choose an option: ");
    string option = Console.ReadLine();

    if (option == "1")
    {
        Console.WriteLine("\n1. Modify Basic Information");
        Console.WriteLine("2. Modify Status");
        Console.WriteLine("3. Modify Special Request Code");
        Console.WriteLine("4. Modify Boarding Gate");
        Console.Write("Choose an option: ");
        string modifyOption = Console.ReadLine();

        if (modifyOption == "1")
        {
            Console.WriteLine("\nModify Basic Information");
            Console.Write("Enter new Origin: ");
            flightToModify.Origin = Console.ReadLine();
            Console.Write("Enter new Destination: ");
            flightToModify.Destination = Console.ReadLine();
            Console.Write("Enter new Expected Departure/Arrival Time (dd/MM/yyyy hh:mm): ");
            flightToModify.ExpectedTime = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("\nFlight updated successfully!");
        }
        else if (modifyOption == "2")
        {
            Console.WriteLine("\nModify Status");
            Console.Write("Enter new Status (e.g., Scheduled, Delayed): ");
            flightToModify.Status = Console.ReadLine();
            Console.WriteLine("\nStatus updated successfully!");
        }
        else if (modifyOption == "3")
        {
            Console.WriteLine("\nModify Special Request Code");
            Console.Write("Enter new Special Request Code (e.g., CFFT, DDJB, LWTT): ");
            flightToModify.SpecialRequestCode = Console.ReadLine();
            Console.WriteLine("\nSpecial Request Code updated successfully!");
        }
        else if (modifyOption == "4")
        {
            Console.WriteLine("\nModify Boarding Gate");
            Console.Write("Enter new Boarding Gate: ");
            flightToModify.BoardingGate = Console.ReadLine();
            Console.WriteLine("\nBoarding Gate updated successfully!");
        }
        else
        {
            Console.WriteLine("\nInvalid option. No changes were made.");
        }


        Console.WriteLine("\nFlight updated!");
    }
    else if (option == "2")
    {
        Console.Write("Are you sure you want to delete this flight? (Y/N): ");
        string confirm = Console.ReadLine()?.ToUpper();
        if (confirm == "Y")
        {
            selectedAirline.Flights.Remove(flightNumber);
            Console.WriteLine("Flight deleted successfully.");
        }
        else
        {
            Console.WriteLine("Flight deletion cancelled.");
        }
    }
    else
    {
        Console.WriteLine("Invalid option.");
        return;
    }

    Console.WriteLine("\nUpdated Flight Details:");
    Console.WriteLine($"{"Flight Number",-15}{"Airline Name",-30}{"Origin",-20}{"Destination",-20}{"Expected",-25}");
    Console.WriteLine(new string('-', 95));
    foreach (var kvp in selectedAirline.Flights)
    {
        var flight = kvp.Value;
        Console.WriteLine($"{flight.FlightNumber,-15}{selectedAirline.Name,-30}{flight.Origin,-20}{flight.Destination,-20}{flight.ExpectedTime:dd/MM/yyyy hh:mm tt,-25}");
    }
}