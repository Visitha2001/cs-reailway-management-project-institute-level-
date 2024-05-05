namespace RailWayAPI_VNR.Models
{
    public class Trains
    {
        public int TrainId { get; set; }
        public string TrainName { get; set; } = "";
        public string StartLocation { get; set; } = "";
        public string EndLocation { get; set; } = "";
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Capacity { get; set; }
        public decimal Distance { get; set; }
        public decimal Price { get; set; }
    }
}