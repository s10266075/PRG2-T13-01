// See https://aka.ms/new-console-template for more information

using PRG2_T13_01;
using System;
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
            terminal.AddBoardingGate(addGate);
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
        Console.WriteLine("{0, -15}{1,-27}{2,-23}{3,-23}{4,-10}", flight.Key, terminal.GetAirlineFromFlight(flight.Value).Name, flight.Value.Origin, flight.Value.Destination, flight.Value.ExpectedTime);
    }
}

//feature 5
void AssignGateToFlight()
{
    Console.WriteLine("=============================================\r\n" +
        "Assign a Boarding Gate to a Flight\r\n" +
        "=============================================");
    Console.Write("Enter the flight number: ");
    string flightnum = Console.ReadLine();
    Console.Write("Enter the gate name: ");
    string gateName = Console.ReadLine();
    if (flightDict.ContainsKey(flightnum) && boardingGatesDict.ContainsKey(gateName))
    {
        if ((boardingGatesDict[gateName].Flight == null))
        {
            boardingGatesDict[gateName].Flight = flightDict[flightnum];
            Console.WriteLine("Flight has been assigned to the gate!");
            Flight temp = flightDict[flightnum];
            Console.WriteLine($"Flight Number: {flightnum}");
            Console.WriteLine($"Origin: {temp.Origin}");
            Console.WriteLine($"Destination: {temp.Destination}");
            Console.WriteLine($"Boarding Gate Name: {gateName}");
            if (temp is CFFTFlight)
            {
                Console.WriteLine("Special Request Code: None");
                Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
                string choice = Console.ReadLine();
                while (true)
                {
                    try
                    {
                        if (choice == "Y")
                        {
                            Console.WriteLine("1.Delayed\n2.Boarding\n3.On Time\nPlease select the new status of the flight:");
                            int statuschoice = Convert.ToInt32(Console.ReadLine());
                            if (statuschoice == 1)
                            {
                                temp.Status = "Delayed";
                                Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                                break;
                            }
                            else if (statuschoice == 2)
                            {
                                temp.Status = "Boarding";
                                Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                                break;
                            }
                            else if (statuschoice == 3)
                            {
                                temp.Status = "On Time";
                                Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                                break;
                            }
                        }
                        else if (choice == "N")
                        {
                            temp.Status = "On Time";
                            Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                            break;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Please enter a valid option!");
                    }
                }
            }
            else if (temp is DDJBFlight)
            {
                Console.WriteLine("Special Request Code: DDJB");
                Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
                string choice = Console.ReadLine();
                while (true)
                {
                    try
                    {
                        if (choice == "Y")
                        {
                            Console.WriteLine("1.Delayed\n2.Boarding\n3.On Time\nPlease select the new status of the flight:");
                            int statuschoice = Convert.ToInt32(Console.ReadLine());
                            if (statuschoice == 1)
                            {
                                temp.Status = "Delayed";
                                Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                                break;
                            }
                            else if (statuschoice == 2)
                            {
                                temp.Status = "Boarding";
                                Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                                break;
                            }
                            else if (statuschoice == 3)
                            {
                                temp.Status = "On Time";
                                Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                                break;
                            }
                        }
                        else if (choice == "N")
                        {
                            temp.Status = "On Time";
                            Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                            break;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Please enter a valid option!");
                    }
                }
            }
            else if (temp is LWTTFlight)
            {
                Console.WriteLine("Special Request Code: LWTT");
                Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
                string choice = Console.ReadLine();
                while (true)
                {
                    try
                    {
                        if (choice == "Y")
                        {
                            Console.WriteLine("1.Delayed\n2.Boarding\n3.On Time\nPlease select the new status of the flight:");
                            int statuschoice = Convert.ToInt32(Console.ReadLine());
                            if (statuschoice == 1)
                            {
                                temp.Status = "Delayed";
                                Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                                break;
                            }
                            else if (statuschoice == 2)
                            {
                                temp.Status = "Boarding";
                                Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                                break;
                            }
                            else if (statuschoice == 3)
                            {
                                temp.Status = "On Time";
                                Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                                break;
                            }
                        }
                        else if (choice == "N")
                        {
                            temp.Status = "On Time";
                            Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                            break;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Please enter a valid option!");
                    }
                }
            }
            else
            {
                Console.WriteLine("Special Request Code: None");
                Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
                string choice = Console.ReadLine();
                while (true)
                {
                    try
                    {
                        if (choice == "Y")
                        {
                            Console.WriteLine("1.Delayed\n2.Boarding\n3.On Time\nPlease select the new status of the flight:");
                            int statuschoice = Convert.ToInt32(Console.ReadLine());
                            if (statuschoice == 1)
                            {
                                temp.Status = "Delayed";
                                Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                                break;
                            }
                            else if (statuschoice == 2)
                            {
                                temp.Status = "Boarding";
                                Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                                break;
                            }
                            else if (statuschoice == 3)
                            {
                                temp.Status = "On Time";
                                Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                                break;
                            }
                        }
                        else if (choice == "N")
                        {
                            temp.Status = "On Time";
                            Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                            break;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Please enter a valid option!");
                    }
                }
            }
        }

        else
        {
            Console.WriteLine("Gate is already occupied!");
            AssignGateToFlight();
        }
    }
    else
    {
        Console.WriteLine("Gate does not exist!");
        AssignGateToFlight();
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
    Console.Write("Enter the expected departure/arrival time (dd/mm/yyyy hh:mm): ");
    DateTime expectedTime = DateTime.Parse(Console.ReadLine());
    Console.Write("Enter the special request code (DDJB/CFFT/LWTT/None): ");
    if (Console.ReadLine() == "DDJB")
    {
        Flight newFlight = new DDJBFlight(flightnum, origin, destination, expectedTime);
        Console.WriteLine("Would you like to add another flight (Y/N)");
        string ans = Console.ReadLine();
        if (ans == "Y")
        {
            CreateNewFlight();
            return newFlight;

        }
        else if (ans == "N")
        {
            return newFlight;
        }
        else
        {
            return null;
        }

    }
    else if (Console.ReadLine() == "CFFT")
    {
        Flight newFlight = new CFFTFlight(flightnum, origin, destination, expectedTime);
        Console.WriteLine("Would you like to add another flight (Y/N)");
        string ans = Console.ReadLine();
        if (ans == "Y")
        {
            CreateNewFlight();
            return newFlight;

        }
        else if (ans == "N")
        {
            return newFlight;
        }
        else
        {
            return null;
        }
    }
    else if (Console.ReadLine() == "LWTT")
    {
        Flight newFlight = new LWTTFlight(flightnum, origin, destination, expectedTime);
        Console.WriteLine("Would you like to add another flight (Y/N)");
        string ans = Console.ReadLine();
        if (ans == "Y")
        {
            CreateNewFlight();
            return newFlight;

        }
        else if (ans == "N")
        {
            return newFlight;
        }
        else
        {
            return null;
        }
    }
    else if (Console.ReadLine() == "None")
    {
        Flight newFlight = new NORMFlight(flightnum, origin, destination, expectedTime);
        Console.WriteLine("Would you like to add another flight (Y/N)");
        string ans = Console.ReadLine();
        if (ans == "Y")
        {
            CreateNewFlight();
            return newFlight;

        }
        else if (ans == "N")
        {
            return newFlight;
        }
        else
        {
            return null;
        }
    }
    else
    {
        Console.WriteLine("Invalid special request code!");
        return CreateNewFlight();
    }
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

//advanced feature b
void DisplayFees()
{
    terminal.PrintAirlineFees();
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
        "8. Display Airline Fees\r\n" +
        "0. Exit\r\n" +
        "Please select your option:");
    int choice = int.Parse(Console.ReadLine());
    if (choice == 1)
    {
        DisplayInfo();
    }
    else if (choice == 2)
    {
        
        ListAllBoardingGates(boardingGatesDict);
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
        DisplayFlightsForAirline(terminal.Airlines);
    }
    else if (choice == 6)
    {
        Console.WriteLine();
    }
    else if (choice == 7)
    {
        SortFlights();
    }
    else if (choice== 8)
    {
        DisplayFees();
    }
    else if (choice == 0)
    {
        break;
    }
}
