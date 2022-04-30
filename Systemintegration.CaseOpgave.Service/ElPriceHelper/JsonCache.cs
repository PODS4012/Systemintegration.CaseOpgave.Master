using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systemintegration.CaseOpgave.Service.ElPriceHelper
{
    /// <summary>
    ///  Meget simpel cache som opbevarer præcis én json fil i cache hver dag.
    ///  Hvis du får brug for at se, hvor cache-filen ligger (så du evt kan slette den), kan du kalde
    ///  metode GetFilename 
    /// 
    /// </summary>
    public class JsonCache
    {

        public static void Put(string json)
        {
            string filename = GetFilename();
            File.WriteAllText(filename, json);
        }

        public static string GetFilename()
        {
            return $"elpris{DateOnly.FromDateTime(DateTime.Now):MM-dd-yy}.json";
        }


        public static string? Get()
        {
            var filename = GetFilename();
            if (!File.Exists(filename))
            {
                return null;
            }

            return File.ReadAllText(filename);
        }
    }
}
