using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentScheduler.Models
{
    /// <summary>
    /// User account entity class.
    /// </summary>
    public class UserAccount
    {
        /// <summary>
        /// Gets or sets the user account id.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the account's username.
        /// </summary>
        [MaxLength(50)]
        public string Username { get; set; } = default!;

        /// <summary>
        /// Gets or sets the account's email.
        /// </summary>
        [MaxLength(150)]
        public string Email { get; set; } = default!;

        /// <summary>
        /// Gets or sets the account's hashed password.
        /// </summary>
        [MaxLength(150)]
        public string Password { get; set; } = default!;
    }
}
