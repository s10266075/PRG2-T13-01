// See https://aka.ms/new-console-template for more information
using PRG2_T13_01;

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
    }
}

