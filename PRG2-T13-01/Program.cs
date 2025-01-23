// See https://aka.ms/new-console-template for more information
using PRG2_T13_01;

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
