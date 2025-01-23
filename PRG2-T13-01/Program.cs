// See https://aka.ms/new-console-template for more information
using PRG2_T13_01;

//==========================================================
// Student Number: S10266075F
// Student Name: Low Day Gene
// Student Number: S10266842H
// Partner Name: Goh Yu Chong Ansel 
//==========================================================


//feature 1
Dictionary<string, Airline> airlineDict = new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> boardingGatesDict = new Dictionary<string, BoardingGate>();

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
                Flight addFlight = new Flight(flightnum, origin, Destination, expectedTime, requestCode);
                flightDict.Add(flightnum, addFlight);
            }
            else if (requestCode == "LWTT")
            {
                Flight addFlight = new Flight(flightnum, origin, Destination, expectedTime, requestCode);
                flightDict.Add(flightnum, addFlight);
            }
            else
            {
                Flight addFlight = new Flight(flightnum, origin, Destination, expectedTime,null);
                flightDict.Add(flightnum, addFlight);
            }
        }
    }
}



