using Newtonsoft.Json;

namespace AUD_AppRailWayVNR.Trains
{
    public class TrainsDTO
    {
        [JsonProperty("trainId")]
        public int TrainId { get; set; }

        [JsonProperty("trainName")]
        public string TrainName { get; set; }

        [JsonProperty("startLocation")]
        public string StartLocation { get; set; }

        [JsonProperty("endLocation")]
        public string EndLocation { get; set; }

        [JsonProperty("startTime")]
        public string StartTime { get; set; }

        [JsonProperty("endTime")]
        public string EndTime { get; set; }

        [JsonProperty("capacity")]
        public int Capacity { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }
    }
}
