using System;

namespace Prb.Ladders.Core
{
    public class Ladder
    {
        public string Brand { get; set; }
        public byte Sections { get; set; }
        public byte StepsPerSection { get; set; }
        public double MaxHeight { get; set; }
        public decimal SalePrice { get; set; }
        public int Stock { get; set; }

        public Ladder()
        { }
        public Ladder(string brand, byte sections, byte stepsPerSection, double maxHeight, decimal salePrice, int stock)
        {
            Brand = brand;
            Sections = sections;
            StepsPerSection = stepsPerSection;
            MaxHeight = maxHeight;
            SalePrice = salePrice;
            Stock = stock;
        }
        public override string ToString()
        {
            return $"{Brand} - {Sections}x{StepsPerSection}";
        }
    }
}
