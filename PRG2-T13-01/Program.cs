// See https://aka.ms/new-console-template for more information

using PRG2_T13_01;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

//==========================================================
// Student Number: S10266075F
// Student Name: Low Day Gene
// Student Number: S10266842H
// Partner Name: Goh Yu Chong Ansel 
//==========================================================

Terminal terminal = new Terminal("Terminal 5");

//feature 1
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
    bool thereisassginedflights = false;
    foreach (var gate in boardingGates.Values)
    {
        // Only display gates that have an assigned flight
        if (gate.Flight != null)
        {
            thereisassginedflights = true;
            break;
        }
    }
    if (thereisassginedflights)
    {
        Console.WriteLine("{0,-10}{1,-10}{2,-10}{3,-10}{4,-20}",
                        "Gate Name", "DDJB", "CFFT", "LWTT", "Assigned Flight");
    }
    else
    {
        Console.WriteLine("{0,-10}{1,-10}{2,-10}{3,-10}",
                        "Gate Name", "DDJB", "CFFT", "LWTT");
    }

    foreach (var gate in boardingGates.Values)
    {
        // Only display gates that have an assigned flight
        if (gate.Flight != null)
        {
            Console.WriteLine("{0,-10}{1,-10}{2,-10}{3,-10}{4,-20}",
                                gate.GateName,
                                gate.SupportsDDJB.ToString(),
                                gate.SupportsCFFT.ToString(),
                                gate.SupportsLWTT.ToString(),
                                gate.Flight.FlightNumber);
        }
        else
        {
            Console.WriteLine("{0,-10}{1,-10}{2,-10}{3,-10}",
                                gate.GateName,
                                gate.SupportsDDJB.ToString(),
                                gate.SupportsCFFT.ToString(),
                                gate.SupportsLWTT.ToString());
        }
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
    foreach (var flight in terminal.Flights.Values)
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

    Console.WriteLine($"List of Flights for {airlineName}");
    Console.WriteLine("{0,-15} {1,-25} {2,-25} {3,-25} {4,-15}",
                    "Flight Number", "Airline Name", "Origin", "Destination", "Departure/Arrival Time");
    foreach (var flight in airlineFlights)
    {
        Console.WriteLine($"{flight.FlightNumber,-15} {airlineName,-25} {flight.Origin,-25} {flight.Destination,-25} {flight.ExpectedTime}");
    }

}
//feature 8

void ModifyFlightDetails(Terminal terminal)
{
    try
    {
        DisplayAirlineFlights(terminal.Airlines, terminal.Flights);
        Console.WriteLine("Choose an existing Flight to modify or delete:");
        string flightNum = Console.ReadLine().ToUpper();

    if (!terminal.Flights.ContainsKey(flightNum))
    {
        Console.WriteLine("Error: Flight not found.");
        return;
    }

    Flight flight = terminal.Flights[flightNum];
    Console.WriteLine("1. Modify Flight");
    Console.WriteLine("2. Delete Flight");
    Console.WriteLine("Choose an option:");
    string option = Console.ReadLine();
    switch (option)
    {
        case "1":
            Console.WriteLine("1. Modify Basic Information");
            Console.WriteLine("2. Modify Status");
            Console.WriteLine("3. Modify Special Request Code");
            Console.WriteLine("4. Modify Boarding Gate");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter new Origin: ");
                    flight.Origin = Console.ReadLine();
                    Console.Write("Enter new Destination: ");
                    flight.Destination = Console.ReadLine();
                    Console.Write("Enter new Expected Departure/Arrival Time (dd/MM/yyyy HH:mm): ");
                    string inputDate = Console.ReadLine();

                    if (!DateTime.TryParseExact(inputDate, new[] { "dd/MM/yyyy HH:mm", "d/M/yyyy H:mm" },
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None, out DateTime newTime))
                    {
                        Console.WriteLine("Invalid date format. Use dd/MM/yyyy HH:mm.");
                        return;
                    }

                        flight.ExpectedTime = newTime;
                        Console.WriteLine("Flight time updated!.");
                        Console.WriteLine($"Flight Number: {flight.FlightNumber}");
                        Console.WriteLine($"Airline Name: {terminal.GetAirlineFromFlight(flight).Name}");
                        Console.WriteLine($"Origin: {flight.Origin}");
                        Console.WriteLine($"Destination: {flight.Destination}");
                        Console.WriteLine($"Expected Departure/Arrival Time: {flight.ExpectedTime:dd/MM/yyyy h:mm:ss tt}");
                        Console.WriteLine($"Status: {flight.Status}");
                        if (flight is CFFTFlight)
                        {
                            Console.WriteLine($"Special Request Code: CFFT");
                        }
                        else if (flight is DDJBFlight)
                        {
                            Console.WriteLine($"Special Request Code: DDJB");
                        }
                        else if (flight is LWTTFlight)
                        {
                            Console.WriteLine($"Special Request Code: LWTT");
                        }
                        else if (flight is NORMFlight)
                        {
                            Console.WriteLine("Special Request Code: None");
                        }

                        string assignedGate = "Unassigned";
                        foreach (var gate in terminal.BoardingGates)
                        {
                            if (gate.Value.Flight == flight)
                            {
                                assignedGate = gate.Key; // Get the gate name
                                break;
                            }
                        }
                        Console.WriteLine($"Boarding Gate: {assignedGate}");
                        break;

                case "2":
                    Console.WriteLine("1. Delayed");
                    Console.WriteLine("2. Boarding");
                    Console.WriteLine("3. On Time");
                    Console.Write("Choose new status: ");
                    string newStatus = Console.ReadLine();

                    if (newStatus == "1") flight.Status = "Delayed";
                    else if (newStatus == "2") flight.Status = "Boarding";
                    else if (newStatus == "3") flight.Status = "On Time";
                    else
                    {
                        Console.WriteLine("Invalid option.");
                        return;
                    }
                    Console.WriteLine("Flight status updated!.");
                    Console.WriteLine("Flight time updated!.");
                    Console.WriteLine($"Flight Number: {flight.FlightNumber}");
                    Console.WriteLine($"Airline Name: {terminal.GetAirlineFromFlight(flight).Name}");
                    Console.WriteLine($"Origin: {flight.Origin}");
                    Console.WriteLine($"Destination: {flight.Destination}");
                    Console.WriteLine($"Expected Departure/Arrival Time: {flight.ExpectedTime:dd/MM/yyyy h:mm:ss tt}");
                    Console.WriteLine($"Status: {flight.Status}");
                    if (flight is CFFTFlight)
                    {
                        Console.WriteLine($"Special Request Code: CFFT");
                    }
                    else if (flight is DDJBFlight)
                    {
                        Console.WriteLine($"Special Request Code: DDJB");
                    }
                    else if (flight is LWTTFlight)
                    {
                        Console.WriteLine($"Special Request Code: LWTT");
                    }
                    else if (flight is NORMFlight)
                    {
                        Console.WriteLine("Special Request Code: None");
                    }

                    string assignedGate1 = "Unassigned";
                    foreach (var gate in terminal.BoardingGates)
                    {
                        if (gate.Value.Flight == flight)
                        {
                            assignedGate1 = gate.Key; // Get the gate name
                            break;
                        }
                    }
                    Console.WriteLine($"Boarding Gate: {assignedGate1}");
                    break;

                case "3": // Modify Special Request Code
                    Console.Write("Enter new Special Request Code (CFFT/DDJB/LWTT/NONE): ");
                    string specialcode = Console.ReadLine().ToUpper();

                    // Validate input
                    if (specialcode != "CFFT" && specialcode != "DDJB" && specialcode != "LWTT" && specialcode != "NONE")
                    {
                        Console.WriteLine("Invalid code. Please enter CFFT, DDJB, LWTT, or NONE.");
                    }
                    else
                    {
                        // Identify current flight type
                        Flight newFlight;
                        if (flight is CFFTFlight && specialcode != "CFFT")
                        {
                            Console.WriteLine($"Flight {flight.FlightNumber} is a CFFTFlight. ");
                        }
                        else if (flight is DDJBFlight && specialcode != "DDJB")
                        {
                            Console.WriteLine($"Flight {flight.FlightNumber} is a DDJBFlight. ");
                        }
                        else if (flight is LWTTFlight && specialcode != "LWTT")
                        {
                            Console.WriteLine($"Flight {flight.FlightNumber} is a LWTTFlight. ");
                        }
                        else if (flight is NORMFlight && specialcode != "NONE")
                        {
                            Console.WriteLine($"Flight {flight.FlightNumber} is a Normal Flight. ");
                        }

                            // Create a new flight instance based on the new special request code
                            switch (specialcode)
                            {
                                case "CFFT":
                                    newFlight = new CFFTFlight(flight.FlightNumber, flight.Origin, flight.Destination, flight.ExpectedTime, flight.Status, 150.0);
                                    break;
                                case "DDJB":
                                    newFlight = new DDJBFlight(flight.FlightNumber, flight.Origin, flight.Destination, flight.ExpectedTime, flight.Status, 300.0);
                                    break;
                                case "LWTT":
                                    newFlight = new LWTTFlight(flight.FlightNumber, flight.Origin, flight.Destination, flight.ExpectedTime, flight.Status, 500.0);
                                    break;
                                default: // NONE (Normal Flight)
                                    newFlight = new NORMFlight(flight.FlightNumber, flight.Origin, flight.Destination, flight.ExpectedTime, flight.Status);
                                    break;
                            }


                        // Replace old flight instance with new one
                        terminal.Flights[flightNum] = newFlight;
                        Console.WriteLine("Special Request Code updated!");
                        Console.WriteLine("Flight time updated!.");
                        Console.WriteLine($"Flight Number: {flight.FlightNumber}");
                        Console.WriteLine($"Airline Name: {terminal.GetAirlineFromFlight(flight).Name}");
                        Console.WriteLine($"Origin: {flight.Origin}");
                        Console.WriteLine($"Destination: {flight.Destination}");
                        Console.WriteLine($"Expected Departure/Arrival Time: {flight.ExpectedTime:dd/MM/yyyy h:mm:ss tt}");
                        Console.WriteLine($"Status: {flight.Status}");
                        Console.WriteLine($"Special Request Code: {specialcode}");
                        string assignedGate2 = "Unassigned";
                        foreach (var gate in terminal.BoardingGates)
                        {
                            if (gate.Value.Flight == flight)
                            {
                                assignedGate2 = gate.Key; // Get the gate name
                                break;
                            }
                        }
                        Console.WriteLine($"Boarding Gate: {assignedGate2}");
                    }
                    break;

                case "4": // Modify Boarding Gate
                    Console.Write("Enter new Boarding Gate: ");
                    string newGate = Console.ReadLine().ToUpper();

                    if (!terminal.BoardingGates.ContainsKey(newGate))
                    {
                        Console.WriteLine("Invalid boarding gate.");
                        return;
                    }

                    if (terminal.BoardingGates[newGate].Flight != null)
                    {
                        Console.WriteLine("Error: This boarding gate is already assigned to another flight.");
                        return;
                    }

                    terminal.BoardingGates[newGate].Flight = flight;
                    Console.WriteLine($"Boarding Gate updated to {newGate}.");
                    Console.WriteLine("Flight time updated!.");
                    Console.WriteLine($"Flight Number: {flight.FlightNumber}");
                    Console.WriteLine($"Airline Name: {terminal.GetAirlineFromFlight(flight).Name}");
                    Console.WriteLine($"Origin: {flight.Origin}");
                    Console.WriteLine($"Destination: {flight.Destination}");
                    Console.WriteLine($"Expected Departure/Arrival Time: {flight.ExpectedTime:dd/MM/yyyy h:mm:ss tt}");
                    Console.WriteLine($"Status: {flight.Status}");
                    if (flight is CFFTFlight)
                    {
                        Console.WriteLine($"Special Request Code: CFFT");
                    }
                    else if (flight is DDJBFlight)
                    {
                        Console.WriteLine($"Special Request Code: DDJB");
                    }
                    else if (flight is LWTTFlight)
                    {
                        Console.WriteLine($"Special Request Code: LWTT");
                    }
                    else if (flight is NORMFlight)
                    {
                        Console.WriteLine("Special Request Code: None");
                    }

                    Console.WriteLine($"Boarding Gate: {newGate}");
                    break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
                break;
            case "2":
                {
                    Console.Write("Are you sure you want to delete this flight? (Y/N): ");
                    string confirm = Console.ReadLine()?.ToUpper();
                    if (confirm == "Y")
                    {
                        if (!terminal.Flights.ContainsKey(flightNum))
                        {
                            Console.WriteLine($"Flight {flightNum} not found.");
                            return;
                        }

                    bool flightFound = false;

                    foreach (var kvp in terminal.Flights.ToList()) // Convert to list to allow removal
                    {
                        if (kvp.Value.FlightNumber == flightNum)
                        {
                            terminal.Flights.Remove(kvp.Key);
                            Console.WriteLine($"Flight {flightNum} deleted successfully.");
                            flightFound = true;
                            break; // Prevent modifying dictionary while iterating
                        }
                    }

                        if (!flightFound)
                        {
                            Console.WriteLine($"Flight {flightNum} not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Flight deletion cancelled.");
                    }
                    break;
                }
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}
    







//feature 2


void LoadFlights()
{
    using (StreamReader sr = new StreamReader("flights.csv"))
    {
        sr.ReadLine();
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            string[] parts = line.Split(",");
            string flightnum = parts[0];
            string origin = parts[1];
            string Destination = parts[2];
            DateTime expectedTime = DateTime.Parse(parts[3]);
            string? requestCode = parts[4];
            if (requestCode == "DDJB")
            {
                Flight addFlight = new DDJBFlight(flightnum, origin, Destination, expectedTime, "Scheduled");
                terminal.GetAirlineFromFlight(addFlight).AddFlight(addFlight);
                terminal.AddFlight(addFlight);
            }
            else if (requestCode == "CFFT")
            {
                Flight addFlight = new CFFTFlight(flightnum, origin, Destination, expectedTime, "Scheduled");
                terminal.GetAirlineFromFlight(addFlight).AddFlight(addFlight);
                terminal.AddFlight(addFlight);
            }
            else if (requestCode == "LWTT")
            {
                Flight addFlight = new LWTTFlight(flightnum, origin, Destination, expectedTime, "Scheduled");
                terminal.GetAirlineFromFlight(addFlight).AddFlight(addFlight);
                terminal.AddFlight(addFlight);
            }
            else
            {
                Flight addFlight = new NORMFlight(flightnum, origin, Destination, expectedTime, "Scheduled");
                terminal.GetAirlineFromFlight(addFlight).AddFlight(addFlight);
                terminal.AddFlight(addFlight);
            }
        }

    }
}


LoadFlights();
//feature 3
void DisplayInfo()
{
    Console.WriteLine("=============================================\n" +
        "List of Flights for Changi Airport Terminal 5\n" +
        "=============================================\n");
    Console.WriteLine("{0, -15}{1,-27}{2,-23}{3,-23}{4,-10}", "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time");
    foreach (KeyValuePair<string, Flight> flight in terminal.Flights)
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
    if (terminal.Flights.ContainsKey(flightnum) && terminal.BoardingGates.ContainsKey(gateName))
    {
        if ((terminal.BoardingGates[gateName].Flight == null))
        {
            terminal.BoardingGates[gateName].Flight = terminal.Flights[flightnum];
            terminal.Flights[flightnum].Gate = true;
            Console.WriteLine("Flight has been assigned to the gate!");
            Flight temp = terminal.Flights[flightnum];
            Console.WriteLine($"Flight Number: {flightnum}");
            Console.WriteLine($"Origin: {temp.Origin}");
            Console.WriteLine($"Destination: {temp.Destination}");
            Console.WriteLine($"Boarding Gate Name: {gateName}");
            if (temp is CFFTFlight)
            {
                Console.WriteLine("Special Request Code: CFFT");
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
                                terminal.BoardingGates[gateName].Flight = temp;
                                Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                                break;
                            }
                            else if (statuschoice == 2)
                            {
                                temp.Status = "Boarding";
                                terminal.BoardingGates[gateName].Flight = temp;
                                Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                                break;
                            }
                            else if (statuschoice == 3)
                            {
                                temp.Status = "On Time";
                                terminal.BoardingGates[gateName].Flight = temp;
                                Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                                break;
                            }
                        }
                        else if (choice == "N")
                        {
                            temp.Status = "On Time";
                            terminal.BoardingGates[gateName].Flight = temp;
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
                                terminal.BoardingGates[gateName].Flight = temp;
                                Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                                break;
                            }
                            else if (statuschoice == 2)
                            {
                                temp.Status = "Boarding";
                                terminal.BoardingGates[gateName].Flight = temp;
                                Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                                break;
                            }
                            else if (statuschoice == 3)
                            {
                                temp.Status = "On Time";
                                terminal.BoardingGates[gateName].Flight = temp;
                                Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                                break;
                            }
                        }
                        else if (choice == "N")
                        {
                            temp.Status = "On Time";
                            terminal.BoardingGates[gateName].Flight = temp;
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
                Console.WriteLine("Would you like to update the status of the flight? (Y/N):");
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
                                terminal.BoardingGates[gateName].Flight = temp;
                                Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                                break;
                            }
                            else if (statuschoice == 2)
                            {
                                temp.Status = "Boarding";
                                terminal.BoardingGates[gateName].Flight = temp;
                                Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                                break;
                            }
                            else if (statuschoice == 3)
                            {
                                temp.Status = "On Time";
                                terminal.BoardingGates[gateName].Flight = temp;
                                Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                                break;
                            }
                        }
                        else if (choice == "N")
                        {
                            temp.Status = "On Time";
                            terminal.BoardingGates[gateName].Flight = temp;
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
                                terminal.BoardingGates[gateName].Flight = temp;
                                Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                                break;
                            }
                            else if (statuschoice == 2)
                            {
                                temp.Status = "Boarding";
                                terminal.BoardingGates[gateName].Flight = temp;
                                Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                                break;
                            }
                            else if (statuschoice == 3)
                            {
                                temp.Status = "On Time";
                                terminal.BoardingGates[gateName].Flight = temp;
                                Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                                break;
                            }
                        }
                        else if (choice == "N")
                        {
                            temp.Status = "On Time";
                            terminal.BoardingGates[gateName].Flight = temp;
                            Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid option!");
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
void CreateNewFlight()
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
    string code = Console.ReadLine();
    if (code == "DDJB")
    {
        Flight newFlight = new DDJBFlight(flightnum, origin, destination, expectedTime);
        Console.WriteLine("Would you like to add another flight (Y/N):");
        string ans = Console.ReadLine();
        if (ans == "Y")
        {
            terminal.AddFlight(newFlight);
            CreateNewFlight();


        }
        else if (ans == "N")
        {
            terminal.AddFlight(newFlight);
        }

    }
    else if (code == "CFFT")
    {
        Flight newFlight = new CFFTFlight(flightnum, origin, destination, expectedTime);
        Console.WriteLine("Would you like to add another flight (Y/N):");
        string ans = Console.ReadLine();
        if (ans == "Y")
        {
            terminal.AddFlight(newFlight);
            CreateNewFlight();
        }
        else if (ans == "N")
        {
            terminal.AddFlight(newFlight);
        }
    }
    else if (code == "LWTT")
    {
        Flight newFlight = new LWTTFlight(flightnum, origin, destination, expectedTime);
        Console.WriteLine("Would you like to add another flight (Y/N):");
        string ans = Console.ReadLine();
        if (ans == "Y")
        {
            terminal.AddFlight(newFlight);
            CreateNewFlight();

        }
        else if (ans == "N")
        {
            terminal.AddFlight(newFlight);
        }
    }
    else if (code == "None")
    {
        Flight newFlight = new NORMFlight(flightnum, origin, destination, expectedTime);
        Console.WriteLine("Would you like to add another flight (Y/N):");
        string ans = Console.ReadLine();
        if (ans == "Y")
        {
            terminal.AddFlight(newFlight);
            CreateNewFlight();

        }
        else if (ans == "N")
        {
            terminal.AddFlight(newFlight);
        }
    }
    else
    {
        Console.WriteLine("Invalid special request code!");
        CreateNewFlight();
    }
}


//feature 9
void SortFlights()
{
    List<Flight> sortList = new List<Flight>();
    foreach (Flight f in terminal.Flights.Values)
    {
        sortList.Add(f);
        sortList.Sort();
    }
    Console.WriteLine("{0, -15}{1,-25}{2,-20}{3,-18}{4,-35}{5,-15}{6,-17}{7}","Flight Number","Airline Name","Origin","Destination","Expected Departure/Arrival Time","Status","Boarding Gate","Special Request Code");
    string gatename = "Unassigned";
    string type = "None";
    foreach (Flight f in sortList)
    {
        if (terminal.BoardingGates.ContainsKey(f.FlightNumber))
        {
            gatename = terminal.BoardingGates[f.FlightNumber].GateName;
        }
        if (f is CFFTFlight)
        {
            type = "CFFT";
        }
        else if (f is DDJBFlight)
        {
            type = "DDJB";
        }
        else if (f is LWTTFlight)
        {
            type = "LWTT";
        }
        else if (f is NORMFlight)
        {
            type = "None";
        }
        Console.WriteLine("{0,-15}{1,-25}{2,-20}{3,-18}{4,-35}{5,-15}{6,-17}{7}",f.FlightNumber,terminal.GetAirlineFromFlight(f).Name,f.Origin,f.Destination,f.ExpectedTime,f.Status,gatename,type);
    }
}
//advanced feature a
void AdvancedTaskA()
{
    Queue<Flight> unassignedFlightsQueue = new Queue<Flight>();
    List<BoardingGate> availableGates = new List<BoardingGate>();
    List<string> assignedFlightNumbers = new List<string>();

    // Create a list of all flights
    List<Flight> flightList = new List<Flight>(terminal.Flights.Values);

    // Find already assigned flights and populate available gates
    int preAssignedFlightCount = 0;
    foreach (var gate in terminal.BoardingGates.Values)
    {
        if (gate.Flight != null)
        {
            assignedFlightNumbers.Add(gate.Flight.FlightNumber);
            preAssignedFlightCount++;
        }
        else
        {
            availableGates.Add(gate);
        }
    }


    // Add unassigned flights to queue
    foreach (Flight flight in flightList)
    {
        if (!assignedFlightNumbers.Contains(flight.FlightNumber))
        {
            unassignedFlightsQueue.Enqueue(flight);
        }
    }

    Console.WriteLine($"Total Flights without Boarding Gate: {unassignedFlightsQueue.Count}");
    Console.WriteLine($"Total Unassigned Boarding Gates: {availableGates.Count}");
    Console.WriteLine($"{"Flight Number",-7} {"Flight Name",-19} {"Origin",-19} {"Destination",-19} {"ExpectedTime",-15} {"Code",-7} {"Gate Name",-15}");
    // Assign flights to matching boarding gates
    int processedFlightsCount = 0;
    while (unassignedFlightsQueue.Count > 0)
    {
        Flight flight = unassignedFlightsQueue.Dequeue();
        string code = "None";
        if (flight is CFFTFlight)
        {
            code = "CFFT";
        }
        else if (flight is DDJBFlight)
        {
            code = "DDJB";
        }
        else if (flight is LWTTFlight)
        {
            code = "LWTT";
        }
        else if (flight is NORMFlight)
        {
            code = "None";
        }
        BoardingGate assignedGate = null;

        foreach (var gate in availableGates.ToList()) // Convert to List to avoid modification errors
        {
            if ((code == "CFFT" && gate.SupportsCFFT) ||
                (code == "DDJB" && gate.SupportsDDJB) ||
                (code == "LWTT" && gate.SupportsLWTT) ||
                (code == "None" && !gate.SupportsCFFT && !gate.SupportsDDJB && !gate.SupportsLWTT))
            {
                assignedGate = gate;
                break;
            }
        }

        if (assignedGate != null)
        {
            terminal.BoardingGates[assignedGate.GateName].Flight = flight;
            flight.Gate = true;
            availableGates.Remove(assignedGate);
            Console.WriteLine($"{flight.FlightNumber,-7} {terminal.GetAirlineFromFlight(flight).Name,-19} {flight.Origin,-19} {flight.Destination,-19} {flight.ExpectedTime,-15} {code,-7} {assignedGate.GateName,-15}");
            processedFlightsCount++;
        }
    }

    Console.WriteLine($"Total Flights Assigned: {processedFlightsCount}");

    // Avoid division by zero when calculating percentage

    if (preAssignedFlightCount > 0)
    {
        double percentage = (processedFlightsCount / (double)(processedFlightsCount + preAssignedFlightCount)) * 100;
        Console.WriteLine("All flights were assigned automatically.");
        Console.WriteLine($"Percentage of Flights Assigned Automatically: {percentage:F2}%");
    }
    else
    {
        Console.WriteLine("All flights were assigned automatically.");
        Console.WriteLine($"Percentage of Flights Assigned Automatically: 100%");
    }

}
//advanced feature b

void DisplayAirlineFees()
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
        "9. Process all unassigned Flights to Boarding Gates\r\n" +
        "0. Exit\r\n" +
        "Please select your option:");
    int choice = int.Parse(Console.ReadLine());
    if (choice == 1)
    {
        DisplayInfo();
    }
    else if (choice == 2)
    {
        ListAllBoardingGates(terminal.BoardingGates);
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
    else if (choice == 8)
    {
        DisplayAirlineFees();
    }
    else if (choice == 9)
    {
        AdvancedTaskA();
    }
    else if (choice == 0)
    {
        break;
    }
}

