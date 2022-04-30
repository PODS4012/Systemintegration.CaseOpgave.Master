using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systemintegration.CaseOpgave.Shared.DataTransferObjects
{
    public class Temperatur
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public decimal Temp { get; set; }
        public string DateStr { get { return Date.ToString("dd-MM-yyyy"); } }
    }
}
