﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
    abstract class Flight:IComparable<Flight>
    {
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }
        public string SpecialRequestCode { get; set; } 
        public string BoardingGate { get; set; }

        public Flight (string fn, string o, string d, DateTime et, string s = "Scheduled") 
        {
            FlightNumber = fn;
            Origin = o;
            Destination = d;
            ExpectedTime = et;
            Status = s;
            SpecialRequestCode = null; 
            BoardingGate = null;
        }
        public virtual double CalculateFees() { return 0.0; }
        public int CompareTo(Flight other)
        {
            return this.ExpectedTime.CompareTo(other.ExpectedTime);
        }
        public override string ToString()
        {
            return "Flight Number: " + FlightNumber + "\nOrigin: " + Origin + "\nDestination: " + Destination + "\nExpected Time: " + ExpectedTime + "\nStatus: " + Status;
        }
    }
}
