using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CarMaintenance.Models
{
    public class OilChange
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? Vin { get; set; }

        [Required]
        public DateTime? DateTime { get; set; }

        [Required]
        public int? Mileage { get; set; }
    }
}
