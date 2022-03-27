using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systemintegration.CaseOpgave.Shared.DataTransferObjects
{
    public class PowerOutputParam
    {
        public DateTime DateTime { get; set; }
        public float PowerOutput { get; set; }
        public string Date { get { return DateTime.ToString("dd-MM-yyyy"); } }
        public string Time { get { return DateTime.ToString("HH:mm (zzz)"); } }
    }
}
