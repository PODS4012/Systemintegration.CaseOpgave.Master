using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systemintegration.CaseOpgave.Shared.DataTransferObjects.ElPriceDto
{
    public class Elspotprice
    {
        public DateTime HourUTC { get; set; }
        public DateTime HourDK { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string PriceArea { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public float SpotPriceDKK { get; set; }
        public float SpotPriceEUR { get; set; }
    }
}
