using System.ComponentModel.DataAnnotations;

namespace RailWayAPI_VNR.Models
{
    public class TrainsDTO
    {
        [Required]
        public int TrainId { get; set; }
        [Required]
        public string TrainName { get; set; } = "";
        [Required]
        public string StartLocation { get; set; } = "";
        [Required]
        public string EndLocation { get; set; } = "";
        [Required]
        public string StartTime { get; set; } = "";
        [Required]
        public string EndTime { get; set; } = "";
        [Required]
        public int Capacity { get; set; }
        [Required]
        public decimal Distance { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}