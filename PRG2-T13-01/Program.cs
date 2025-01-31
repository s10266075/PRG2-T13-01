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
Dictionary<string, Flight> flightDict = new Dictionary<string, Flight>();
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
LoadAirlines();
LoadBoardingGates();
//feature 4

void ListAllBoardingGates(Dictionary<string, BoardingGate> boardingGates)
{
    Console.WriteLine("==================================================");
    Console.WriteLine(" List of Boarding Gates for Changi Airport T5 ");
    Console.WriteLine("==================================================");
    Console.WriteLine("{0,-10}{1,-10}{2,-10}{3,-10}{4,-20}",
                      "Gate Name", "DDJB", "CFFT", "LWTT", "Assigned Flight");

    foreach (var gate in boardingGates.Values)
    {
        string assignedFlight = gate.Flight != null ? gate.Flight.FlightNumber : "None";

        Console.WriteLine("{0,-10}{1,-10}{2,-10}{3,-10}{4,-20}",
                          gate.GateName,
                          gate.SupportsDDJB.ToString(),
                          gate.SupportsCFFT.ToString(),
                          gate.SupportsLWTT.ToString(),
                          assignedFlight);
    }

}
//feature 7
void DisplayAirlineFlights(Dictionary<string, Airline> airlines, Dictionary<string, Flight> flights)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Airline Code",-15} {"Airline Name"}");
    foreach (var airline in airlines.Values)
    {   
        Console.WriteLine($"{airline.Code,-15} {airline.Name}");
    }
    Console.Write("Enter Airline Code: ");
    string airlineCode = Console.ReadLine()?.ToUpper();
    if (!airlines.ContainsKey(airlineCode))
    {
        Console.WriteLine("Invalid Airline Code. Please try again");
        return;
    }
    var airlineName = airlines[airlineCode].Name;
    List<Flight> airlineFlights = new List<Flight>();
    foreach (var flight in flightDict.Values)
    {
        if (flight.FlightNumber.StartsWith(airlineCode))
        {
            airlineFlights.Add(flight);
        }
    }
    if (airlineFlights.Count == 0)
    {
        Console.WriteLine($"No flights found for {airlineName}.");
        return;
    }

    Console.WriteLine("=============================================");
    Console.WriteLine($"List of Flights for {airlineName}");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Flight Number",-15} {"Airline Name",-25} {"Origin",-25} {"Destination",-25} {"Expected"}");
    Console.WriteLine("Departure/Arrival Time");

    foreach (var flight in airlineFlights)
    {
        Console.WriteLine($"{flight.FlightNumber,-15} {airlineName,-25} {flight.Origin,-25} {flight.Destination,-25} {flight.ExpectedTime}");
    }
}
//feature 8
void ModifyFlightDetails(Terminal terminal)
{
    Console.WriteLine("\n=============================================");
    Console.WriteLine("Modify Flight Details");
    Console.WriteLine("=============================================");

    // Show all flights before modifying
    DisplayAirlineFlights(terminal.Airlines,terminal.Flights);

    Console.Write("\nChoose a Flight Number to modify or delete: ");
    string flightNumber = Console.ReadLine()?.ToUpper();

    if (!terminal.Flights.ContainsKey(flightNumber))
    {
        Console.WriteLine("Invalid Flight Number.");
        return;
    }

    Flight selectedFlight = terminal.Flights[flightNumber];

    Console.WriteLine("\n1. Modify Flight");
    Console.WriteLine("2. Delete Flight");
    Console.Write("Choose an option: ");
    string option = Console.ReadLine();

    if (option == "1")
    {
        Console.WriteLine("\n1. Modify Basic Information");
        Console.WriteLine("2. Modify Status");
        Console.Write("Choose an option: ");
        string modifyOption = Console.ReadLine();

        if (modifyOption == "1")
        {
            Console.Write("\nEnter new Origin: ");
            selectedFlight.Origin = Console.ReadLine();
            Console.Write("Enter new Destination: ");
            selectedFlight.Destination = Console.ReadLine();

            Console.Write("Enter new Expected Departure/Arrival Time (dd/MM/yyyy HH:mm): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime newTime))
            {
                Console.WriteLine("Invalid date format. Use dd/MM/yyyy HH:mm.");
                return;
            }
            selectedFlight.ExpectedTime = newTime;

            Console.WriteLine("\nFlight updated successfully!");
        }
        else if (modifyOption == "2")
        {
            Console.WriteLine("\nModify Status:");
            Console.WriteLine("1. Delayed");
            Console.WriteLine("2. Boarding");
            Console.WriteLine("3. On Time");
            Console.Write("Enter new Status: ");

            if (!int.TryParse(Console.ReadLine(), out int statusChoice) || statusChoice < 1 || statusChoice > 3)
            {
                Console.WriteLine("Invalid status choice.");
                return;
            }

            selectedFlight.Status = statusChoice switch
            {
                1 => "Delayed",
                2 => "Boarding",
                _ => "On Time"
            };

            Console.WriteLine("\nStatus updated successfully!");
        }
            else
        {
            Console.WriteLine("\nInvalid option. No changes were made.");
        }
    }
    else if (option == "2")
    {
        Console.Write("Are you sure you want to delete this flight? (Y/N): ");
        string confirm = Console.ReadLine()?.ToUpper();
        if (confirm == "Y")
        {
            terminal.Flights.Remove(flightNumber);
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

    Console.WriteLine("\nUpdated Flight Schedule:");
    DisplayAirlineFlights(terminal.Airlines, terminal.Flights);
}




//feature 2


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
                Flight addFlight = new DDJBFlight(flightnum, origin, Destination, expectedTime,"DDJB");

                flightDict.Add(flightnum, addFlight);
            }
            else if (requestCode == "CFFT")
            {
                Flight addFlight = new CFFTFlight(flightnum, origin, Destination, expectedTime,"CFFT");
                flightDict.Add(flightnum, addFlight);
            }
            else if (requestCode == "LWTT")
            {
                Flight addFlight = new LWTTFlight(flightnum, origin, Destination, expectedTime,"LWTTF");
                flightDict.Add(flightnum, addFlight);
            }
            else
            {
                Flight addFlight = new NORMFlight(flightnum, origin, Destination, expectedTime,"On time");
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
        Flight newFlight = new DDJBFlight(flightnum, origin, destination, expectedTime, "DDJB");
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
        Flight newFlight = new CFFTFlight(flightnum, origin, destination, expectedTime, "CFFT");
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
        Flight newFlight = new LWTTFlight(flightnum, origin, destination, expectedTime, "LWTTF");
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
        Flight newFlight = new NORMFlight(flightnum, origin, destination, expectedTime,"On Time");
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
        DisplayAirlineFlights(terminal.Airlines, terminal.Flights);
    }
    else if (choice == 6)
    {
        ModifyFlightDetails(terminal);
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
