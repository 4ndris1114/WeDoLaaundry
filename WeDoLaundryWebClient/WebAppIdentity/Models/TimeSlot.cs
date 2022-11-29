using System.ComponentModel.DataAnnotations;

namespace WebAppIdentity.Models
{
    public class TimeSlot
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Slot { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public int Availability { get; set; }
    }
}
