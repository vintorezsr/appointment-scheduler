using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentScheduler.Models
{
    public class UserAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
