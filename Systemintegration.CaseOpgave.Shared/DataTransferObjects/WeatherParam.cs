namespace Systemintegration.CaseOpgave.Shared.DataTransferObjects
{
    public class WeatherParam
    {
        public DateTime DateTime { get; set; }
        public float? Temp { get; set; }
        public string? Conditions { get; set; }
        public string Date { get { return DateTime.ToString("dd-MM-yyyy"); } }
        public string Time { get { return DateTime.ToString("HH:mm (zzz)"); } }
    }
}
