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
// Initialize the airport terminal
Terminal terminal = new Terminal("Terminal 5");
//feature 1
// Loads airlines from a CSV file and adds them to the terminal's airline list.

void LoadAirlines()
{
    using (StreamReader sr = new StreamReader("airlines.csv")) // Open the CSV file
    {
        sr.ReadLine(); // Skip the header line
        string line;
        while ((line = sr.ReadLine()) != null) // Read each line until the end of the file
        {
            string[] parts = line.Split(','); // Split the line by comma to extract airline details
            string code = parts[1]; // Extract airline code
            string name = parts[0]; // Extract airline name

            // Create a new airline object and add it to the terminal
            Airline addairline = new Airline(code, name);
            terminal.AddAirline(addairline);
        }
    }
}
// Loads boarding gates from a CSV file and adds them to the terminal's gate list.
void LoadBoardingGates()
{
    using (StreamReader sr = new StreamReader("boardinggates.csv")) // Open the CSV file
    {
        sr.ReadLine(); // Skip the header line
        string line;
        while ((line = sr.ReadLine()) != null) // Read each line until the end of the file
        {
            string[] parts = line.Split(','); // Split the line by comma to extract gate details
            string gateName = parts[0]; // Extract gate name
            bool supportsDDJB = bool.Parse(parts[1]); // Check if gate supports DDJB flights
            bool supportsCFFT = bool.Parse(parts[2]); // Check if gate supports CFFT flights
            bool supportsLWTT = bool.Parse(parts[3]); // Check if gate supports LWTT flights
            Flight gateFlight = null; // Initially, no flight is assigned to the gate

            // Create a new boarding gate object and add it to the terminal
            BoardingGate addGate = new BoardingGate(gateName, supportsCFFT, supportsDDJB, supportsLWTT, gateFlight);
            terminal.AddBoardingGate(addGate);
        }
    }
}
// Load airlines and boarding gates when the program starts
LoadAirlines();
LoadBoardingGates();
//feature 4
// Displays a list of all boarding gates and their assigned flights.
void ListAllBoardingGates(Dictionary<string, BoardingGate> boardingGates)
{
    Console.WriteLine("==================================================");
    Console.WriteLine(" List of Boarding Gates for Changi Airport T5 ");
    Console.WriteLine("==================================================");
    bool thereisassginedflights = false; // variable to check if any gate has an assigned flight
    // Loop through each gate to check if there is at least one assigned flight
    foreach (var gate in boardingGates.Values)
    {
        if (gate.Flight != null)
        {
            thereisassginedflights = true;
            break;
        }
    }
    if (thereisassginedflights) // Only display Assigned flight header if there is atleast one assigned flight
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
// Displays all airlines and their associated flights.
// Allows the user to select an airline and view its flights.
void DisplayAirlineFlights(Dictionary<string, Airline> airlines, Dictionary<string, Flight> flights)
{
    Console.WriteLine("=============================================");
    Console.WriteLine(" List of Airlines for Changi Airport Terminal 5 ");
    Console.WriteLine("=============================================");

    // Display the header for airline list
    Console.WriteLine($"{"Airline Code",-15} {"Airline Name"}");

    // Print all airlines
    foreach (var airline in airlines.Values)
    {
        Console.WriteLine($"{airline.Code,-15} {airline.Name}");
    }

    // Ask user to enter an airline code
    Console.Write("Enter Airline Code: ");
    string airlineCode = Console.ReadLine()?.ToUpper();

    // Validate if the entered airline code exists
    if (!airlines.ContainsKey(airlineCode))
    {
        Console.WriteLine("Invalid Airline Code. Please try again.");
        return;
    }

    var airlineName = airlines[airlineCode].Name;
    List<Flight> airlineFlights = new List<Flight>();

    // Fetch all flights belonging to the selected airline
    foreach (var flight in terminal.Flights.Values)
    {
        if (flight.FlightNumber.StartsWith(airlineCode))
        {
            airlineFlights.Add(flight);
        }
    }

    // Check if the airline has any flights
    if (airlineFlights.Count == 0)
    {
        Console.WriteLine($"No flights found for {airlineName}.");
        return;
    }

    // Display all flights for the selected airline
    Console.WriteLine($"List of Flights for {airlineName}");
    Console.WriteLine("{0,-15} {1,-25} {2,-25} {3,-25} {4,-15}",
                    "Flight Number", "Airline Name", "Origin", "Destination", "Departure/Arrival Time");

    foreach (var flight in airlineFlights)
    {
        Console.WriteLine($"{flight.FlightNumber,-15} {airlineName,-25} {flight.Origin,-25} {flight.Destination,-25} {flight.ExpectedTime}");
    }
}
//feature 8
// Allows the user to modify flight details such as origin, destination, time, status, and special request code.
// The user can also delete a flight.
void ModifyFlightDetails(Terminal terminal)
{
    try
    {
        // Display the list of available flights
        DisplayAirlineFlights(terminal.Airlines, terminal.Flights);
        Console.WriteLine("Choose an existing Flight to modify or delete:");
        string flightNum = Console.ReadLine().ToUpper();

        // Validate if flight exists
        if (!terminal.Flights.ContainsKey(flightNum))
        {
            Console.WriteLine("Error: Flight not found.");
            ModifyFlightDetails(terminal); // Recursive call to retry
        }

        Flight flight = terminal.Flights[flightNum]; // Retrieve flight object

        Console.WriteLine("1. Modify Flight");
        Console.WriteLine("2. Delete Flight");
        Console.Write("Choose an option: ");
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
                    case "1": // Modify Basic Flight Information
                        Console.Write("Enter new Origin: ");
                        flight.Origin = Console.ReadLine();
                        Console.Write("Enter new Destination: ");
                        flight.Destination = Console.ReadLine();
                        Console.Write("Enter new Expected Departure/Arrival Time (dd/MM/yyyy HH:mm): ");
                        string inputDate = Console.ReadLine();

                        // Validate date format
                        if (!DateTime.TryParseExact(inputDate, new[] { "dd/MM/yyyy HH:mm", "d/M/yyyy H:mm" },
                            System.Globalization.CultureInfo.InvariantCulture,
                            System.Globalization.DateTimeStyles.None, out DateTime newTime))
                        {
                            Console.WriteLine("Invalid date format. Use dd/MM/yyyy HH:mm.");
                            return;
                        }

                        flight.ExpectedTime = newTime;
                        Console.WriteLine("Flight time updated!");

                        // Display updated flight details
                        Console.WriteLine($"Flight Number: {flight.FlightNumber}");
                        Console.WriteLine($"Airline Name: {terminal.GetAirlineFromFlight(flight).Name}");
                        Console.WriteLine($"Origin: {flight.Origin}");
                        Console.WriteLine($"Destination: {flight.Destination}");
                        Console.WriteLine($"Expected Departure/Arrival Time: {flight.ExpectedTime:dd/MM/yyyy h:mm:ss tt}");
                        Console.WriteLine($"Status: {flight.Status}");

                        break;

                    case "2": // Modify Flight Status
                        Console.WriteLine("1. Delayed");
                        Console.WriteLine("2. Boarding");
                        Console.WriteLine("3. On Time");
                        Console.Write("Choose new status: ");
                        string newStatus = Console.ReadLine();

                        // Assign status based on user selection
                        if (newStatus == "1") flight.Status = "Delayed";
                        else if (newStatus == "2") flight.Status = "Boarding";
                        else if (newStatus == "3") flight.Status = "On Time";
                        else
                        {
                            Console.WriteLine("Invalid option.");
                            return;
                        }

                        Console.WriteLine("Flight status updated!");

                        break;

                    case "3": // Modify Special Request Code
                        Console.Write("Enter new Special Request Code (CFFT/DDJB/LWTT/NONE): ");
                        string specialCode = Console.ReadLine().ToUpper();

                        // Validate input
                        if (specialCode != "CFFT" && specialCode != "DDJB" && specialCode != "LWTT" && specialCode != "NONE")
                        {
                            Console.WriteLine("Invalid code. Please enter CFFT, DDJB, LWTT, or NONE.");
                            return;
                        }

                        // Update flight type based on special request code
                        switch (specialCode)
                        {
                            case "CFFT":
                                terminal.Flights[flightNum] = new CFFTFlight(flight.FlightNumber, flight.Origin, flight.Destination, flight.ExpectedTime, flight.Status, 150.0);
                                break;
                            case "DDJB":
                                terminal.Flights[flightNum] = new DDJBFlight(flight.FlightNumber, flight.Origin, flight.Destination, flight.ExpectedTime, flight.Status, 300.0);
                                break;
                            case "LWTT":
                                terminal.Flights[flightNum] = new LWTTFlight(flight.FlightNumber, flight.Origin, flight.Destination, flight.ExpectedTime, flight.Status, 500.0);
                                break;
                            default: // NONE (Normal Flight)
                                terminal.Flights[flightNum] = new NORMFlight(flight.FlightNumber, flight.Origin, flight.Destination, flight.ExpectedTime, flight.Status);
                                break;
                        }

                        Console.WriteLine("Special Request Code updated!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
                break;

            case "2": // Delete Flight
                Console.Write("Are you sure you want to delete this flight? (Y/N): ");
                string confirm = Console.ReadLine()?.ToUpper();
                if (confirm == "Y")
                {
                    terminal.Flights.Remove(flightNum);
                    Console.WriteLine($"Flight {flightNum} deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Flight deletion cancelled.");
                }
                break;

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


// Function to load flights from a CSV file into the terminal's flight list
void LoadFlights()
{
    using (StreamReader sr = new StreamReader("flights.csv"))
    {
        sr.ReadLine(); // Skip the header row
        string line;
        while ((line = sr.ReadLine()) != null) // Read each line in the file
        {
            string[] parts = line.Split(","); // Split the line by commas
            string flightnum = parts[0]; // Extract flight number
            string origin = parts[1]; // Extract origin
            string destination = parts[2]; // Extract destination
            DateTime expectedTime = DateTime.Parse(parts[3]); // Extract expected time
            string? requestCode = parts[4]; // Extract special request code

            // Determine flight type based on request code
            Flight addFlight;
            if (requestCode == "DDJB")
            {
                addFlight = new DDJBFlight(flightnum, origin, destination, expectedTime, "Scheduled");
            }
            else if (requestCode == "CFFT")
            {
                addFlight = new CFFTFlight(flightnum, origin, destination, expectedTime, "Scheduled");
            }
            else if (requestCode == "LWTT")
            {
                addFlight = new LWTTFlight(flightnum, origin, destination, expectedTime, "Scheduled");
            }
            else
            {
                addFlight = new NORMFlight(flightnum, origin, destination, expectedTime, "Scheduled");
            }

            // Assign the flight to its respective airline
            terminal.GetAirlineFromFlight(addFlight).AddFlight(addFlight);
            terminal.AddFlight(addFlight);
        }
    }
}

// Call the function to load flights on program startup
LoadFlights();

//feature 3
// Function to display all flights in the terminal
void DisplayInfo()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Flights for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");

    // Print table header
    Console.WriteLine("{0, -15}{1,-27}{2,-23}{3,-23}{4,-10}",
                      "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time");

    // Loop through all flights and display details
    foreach (KeyValuePair<string, Flight> flight in terminal.Flights)
    {
        Console.WriteLine("{0, -15}{1,-27}{2,-23}{3,-23}{4,-10}",
                          flight.Key, terminal.GetAirlineFromFlight(flight.Value).Name,
                          flight.Value.Origin, flight.Value.Destination, flight.Value.ExpectedTime);
    }
}


//feature 5
// Function to assign a flight to a boarding gate
void AssignGateToFlight()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Assign a Boarding Gate to a Flight");
    Console.WriteLine("=============================================");

    // Prompt user for flight number and gate name
    Console.Write("Enter the flight number: ");
    string flightnum = Console.ReadLine().ToUpper();
    Console.Write("Enter the gate name: ");
    string gateName = Console.ReadLine().ToUpper();

    // Check if the flight exists
    if (terminal.Flights.ContainsKey(flightnum))
    {
        // Check if the gate exists
        if (terminal.BoardingGates.ContainsKey(gateName))
        {
            // Check if the gate is already assigned
            if (terminal.BoardingGates[gateName].Flight == null)
            {
                // Assign the flight to the gate
                terminal.BoardingGates[gateName].Flight = terminal.Flights[flightnum];
                terminal.Flights[flightnum].Gate = true;
                Console.WriteLine("Flight has been assigned to the gate!");

                Flight temp = terminal.Flights[flightnum];
                Console.WriteLine($"Flight Number: {flightnum}");
                Console.WriteLine($"Origin: {temp.Origin}");
                Console.WriteLine($"Destination: {temp.Destination}");
                Console.WriteLine($"Boarding Gate Name: {gateName}");

                // Determine flight type and handle status updates
                string specialCode = temp is CFFTFlight ? "CFFT" :
                                     temp is DDJBFlight ? "DDJB" :
                                     temp is LWTTFlight ? "LWTT" : "None";
                Console.WriteLine($"Special Request Code: {specialCode}");
                Console.WriteLine("Would you like to update the status of the flight? (Y/N)");

                string choice = Console.ReadLine().ToUpper();
                while (true)
                {
                    try
                    {
                        if (choice == "Y")
                        {
                            Console.WriteLine("1. Delayed\n2. Boarding\n3. On Time\nPlease select the new status of the flight:");
                            int statusChoice = Convert.ToInt32(Console.ReadLine());

                            if (statusChoice == 1) temp.Status = "Delayed";
                            else if (statusChoice == 2) temp.Status = "Boarding";
                            else if (statusChoice == 3) temp.Status = "On Time";
                            else throw new FormatException();

                            terminal.BoardingGates[gateName].Flight = temp;
                            Console.WriteLine($"Flight {temp.FlightNumber} has been assigned to Boarding Gate {gateName}");
                            break;
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
                Console.WriteLine("This gate is already assigned to a flight!");
            }
        }
        else
        {
            Console.WriteLine("Gate does not exist!");
        }
    }
    else
    {
        Console.WriteLine("Flight does not exist!");
    }
}


//feature 6
// Function to create a new flight and add it to the terminal
void CreateNewFlight()
{
    Console.Write("Enter the flight number: ");
    string flightnum = Console.ReadLine().ToUpper();

    try
    {
        // Validate airline existence
        string[] parts = flightnum.Split(" ");
        Airline a = terminal.Airlines[parts[0]];

        Console.Write("Enter the origin: ");
        string origin = Console.ReadLine();
        Console.Write("Enter the destination: ");
        string destination = Console.ReadLine();
        Console.Write("Enter the expected departure/arrival time (dd/mm/yyyy hh:mm): ");

        try
        {
            DateTime expectedTime = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter the special request code (DDJB/CFFT/LWTT/None): ");
            string code = Console.ReadLine().ToUpper();

            // Create a new flight based on request code
            Flight newFlight = code switch
            {
                "DDJB" => new DDJBFlight(flightnum, origin, destination, expectedTime),
                "CFFT" => new CFFTFlight(flightnum, origin, destination, expectedTime),
                "LWTT" => new LWTTFlight(flightnum, origin, destination, expectedTime),
                "NONE" => new NORMFlight(flightnum, origin, destination, expectedTime),
                _ => throw new ArgumentException("Invalid special request code!")
            };

            // Add flight to terminal
            terminal.AddFlight(newFlight);
            terminal.GetAirlineFromFlight(newFlight).AddFlight(newFlight);

            Console.WriteLine("Would you like to add another flight (Y/N):");
            if (Console.ReadLine().ToUpper() == "Y") CreateNewFlight();
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid date format!");
        }
    }
    catch (KeyNotFoundException)
    {
        Console.WriteLine("Airline not found!");
    }
}

// Function to sort flights based on their flight number and display them in a formatted table
void SortFlights()
{
    List<Flight> sortList = new List<Flight>(); // Create a list to store sorted flights

    // Add all flights to the list
    foreach (Flight f in terminal.Flights.Values)
    {
        sortList.Add(f);
        sortList.Sort(); // Sort flights using default comparison
    }

    // Print table header
    Console.WriteLine("{0, -15}{1,-25}{2,-20}{3,-18}{4,-35}{5,-15}{6,-17}{7}",
                      "Flight Number", "Airline Name", "Origin", "Destination",
                      "Expected Departure/Arrival Time", "Status", "Boarding Gate", "Special Request Code");

    string type = "None"; // Default type for normal flights

    // Iterate through sorted flights and display their details
    foreach (Flight f in sortList)
    {
        string gatename = "Unassigned"; // Default gate assignment

        // Check if the flight is assigned to a gate
        foreach (BoardingGate b in terminal.BoardingGates.Values)
        {
            if (b.Flight == f)
            {
                gatename = b.GateName; // Assign the gate name
            }
        }

        // Identify the special request code for the flight
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

        // Print the flight details in a formatted table row
        Console.WriteLine("{0,-15}{1,-25}{2,-20}{3,-18}{4,-35}{5,-15}{6,-17}{7}",
                          f.FlightNumber, terminal.GetAirlineFromFlight(f).Name,
                          f.Origin, f.Destination, f.ExpectedTime,
                          f.Status, gatename, type);
    }
}

//advanced feature a
// Advanced Feature A: Automatic Boarding Gate Assignment
void AdvancedTaskA()
{
    // Queue to store flights that haven't been assigned a gate
    Queue<Flight> unassignedFlightsQueue = new Queue<Flight>();

    // List to store available (unassigned) boarding gates
    List<BoardingGate> availableGates = new List<BoardingGate>();

    // List to keep track of flight numbers that are already assigned a gate
    List<string> assignedFlightNumbers = new List<string>();

    // Create a list of all flights in the system
    List<Flight> flightList = new List<Flight>(terminal.Flights.Values);

    // Count flights already assigned a boarding gate and store available gates
    int preAssignedFlightCount = 0;
    foreach (var gate in terminal.BoardingGates.Values)
    {
        if (gate.Flight != null)
        {
            // Add assigned flights to the list
            assignedFlightNumbers.Add(gate.Flight.FlightNumber);
            preAssignedFlightCount++;
        }
        else
        {
            // Add unassigned gates to the available gates list
            availableGates.Add(gate);
        }
    }

    // Add flights that have no assigned gate to the queue
    foreach (Flight flight in flightList)
    {
        if (!assignedFlightNumbers.Contains(flight.FlightNumber))
        {
            unassignedFlightsQueue.Enqueue(flight);
        }
    }

    // Display counts of unassigned flights and gates
    Console.WriteLine($"Total Flights without Boarding Gate: {unassignedFlightsQueue.Count}");
    Console.WriteLine($"Total Unassigned Boarding Gates: {availableGates.Count}");

    // Table header for displaying flight assignments
    Console.WriteLine($"{"Flight Number",-14} {"Flight Name",-19} {"Origin",-19} {"Destination",-19} {"ExpectedTime",-21} {"Code",-7} {"Gate Name",-15}");

    int processedFlightsCount = 0;

    // Assign flights to available gates
    while (unassignedFlightsQueue.Count > 0)
    {
        Flight flight = unassignedFlightsQueue.Dequeue();
        string code = "None";

        // Determine the flight type (Special Request Code)
        if (flight is CFFTFlight) code = "CFFT";
        else if (flight is DDJBFlight) code = "DDJB";
        else if (flight is LWTTFlight) code = "LWTT";
        else if (flight is NORMFlight) code = "None";

        BoardingGate assignedGate = null;

        // Find a suitable boarding gate that supports the flight type
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
            // Assign flight to gate
            terminal.BoardingGates[assignedGate.GateName].Flight = flight;
            flight.Gate = true; // Mark flight as assigned
            availableGates.Remove(assignedGate); // Remove gate from available list

            // Display assigned flight details
            Console.WriteLine($"{flight.FlightNumber,-14} {terminal.GetAirlineFromFlight(flight).Name,-19} {flight.Origin,-19} {flight.Destination,-19} {flight.ExpectedTime,-21} {code,-7} {assignedGate.GateName,-15}");

            processedFlightsCount++;
        }
    }

    Console.WriteLine($"Total Flights Assigned: {processedFlightsCount}");

    // Calculate and display the percentage of flights assigned automatically
    if (preAssignedFlightCount > 0)
    {
        double percentage = (processedFlightsCount / (double)(processedFlightsCount + preAssignedFlightCount)) * 100;
        Console.WriteLine($"Percentage of Flights Assigned Automatically: {percentage:F2}%");
    }
    else
    {
        Console.WriteLine("All flights were assigned automatically.");
        Console.WriteLine($"Percentage of Flights Assigned Automatically: 100%");
    }
}
// Advanced Feature B: Display Airline Fees
void DisplayAirlineFees()
{
    // Call terminal method to print airline fees
    terminal.PrintAirlineFees();
}

// Bonus Feature 1: Create a New Airline
void CreateNewAirline()
{
    // Prompt user for airline details
    Console.Write("Enter the airline code: ");
    string code = Console.ReadLine().ToUpper();
    Console.Write("Enter the airline name: ");
    string name = Console.ReadLine();

    // Create new airline and add it to the terminal
    Airline newairline = new Airline(code, name);
    terminal.AddAirline(newairline);
    Console.WriteLine("Airline added successfully!");

    // Ask user if they want to add flights to this airline
    Console.WriteLine("Would you like to add flights to this airline? (Y/N)");
    string choice = Console.ReadLine().ToUpper();

    if (choice == "Y")
    {
        // Call CreateNewFlight method to add flights
        CreateNewFlight();
    }
    else if (choice == "N")
    {
        // Ask user if they want to create another airline
        Console.WriteLine("Would you like to create another airline? (Y/N)");
        string choice1 = Console.ReadLine().ToUpper();

        if (choice1 == "Y")
        {
            CreateNewAirline();
        }
        else if (choice1 == "N")
        {
            return; // Exit function
        }
        else
        {
            Console.WriteLine("Invalid choice! Please try again.");
            CreateNewAirline(); // Retry if invalid input
        }
    }
    else
    {
        Console.WriteLine("Invalid choice! Please try again.");
        CreateNewAirline(); // Retry if invalid input
    }
}



//menu
while (true)
{

    Console.WriteLine("=============================================\r\n" +
        "Welcome to Changi Airport Terminal 5\r\n" +
        "=============================================\r\n" +
        "1.  List All Flights\r\n" +
        "2.  List Boarding Gates\r\n" +
        "3.  Assign a Boarding Gate to a Flight\r\n" +
        "4.  Create Flight\r\n" +
        "5.  Display Airline Flights\r\n" +
        "6.  Modify Flight Details\r\n" +
        "7.  Display Flight Schedule\r\n" +
        "8.  Process all unassigned Flights to Boarding Gates\r\n" +
        "9.  Display Airline fees\r\n" +
        "10. Create New Airline\r\n" +
        "0.  Exit\r\n" +
        "Please select your option:");
    try
    {
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
            AdvancedTaskA();
        }
        else if (choice == 9)
        {
            DisplayAirlineFees();
        }
        else if (choice == 10)
        {
            CreateNewAirline();
        }
        else if (choice == 0)
        {
            Console.WriteLine("Goodbye!");
            break;
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("Please enter a number from 1 to 10!");
    }
    
}

