using System;
using System.Collections.Generic;
namespace ExtraVert
{
    public class Plant 
    {
        public string Species { get; set; }
        public int LightNeeds { get; set; }
        public decimal AskingPrice { get; set; }
        public string City { get; set; }
        public int ZIP { get; set; }
        public bool Sold { get; set; }
        public Plant(string species, int lightNeeds, decimal askingPrice, string city, int zIP, bool sold)
        {
            Species = species;
            LightNeeds = lightNeeds;
            AskingPrice = askingPrice;
            City = city;
            ZIP = zIP;
            Sold = sold;
        }
    }

}
