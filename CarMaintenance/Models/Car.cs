using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CarMaintenance.Models
{
    public class Car
    {
        public Car()
        {
            OilChanges = new HashSet<OilChange>();
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Vin { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }

        [Required]
        public int YearManufactured { get; set; }

        [JsonIgnore]
        public ICollection<OilChange> OilChanges { get; set; }
    }
}
