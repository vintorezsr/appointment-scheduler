using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentScheduler.Models
{
    /// <summary>
    /// Appointment schedule entity class.
    /// </summary>
    public class AppointmentSchedule
    {
        /// <summary>
        /// Gets or sets the appointment schedule id.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the user account foreign key id.
        /// </summary>
        public Guid UserAccountId { get; set; }

        /// <summary>
        /// Gets or sets the start time (in UTC).
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time (in UTC).
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Gets or sets the appointment title (if any).
        /// </summary>
        [MaxLength(150)]
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the appointment description (if any).
        /// </summary>
        [MaxLength(450)]
        public string? Description { get; set; }

        /// <summary>
        /// Reference to the <see cref="UserAccount"/> instance.
        /// </summary>
        public UserAccount? UserAccount { get; set; }

        public static AppointmentSchedule CreateFrom(Guid userAccountId, AppointmentScheduleDto appointmentScheduleDto)
        {
            return new AppointmentSchedule
            {
                Id = appointmentScheduleDto.Id,
                UserAccountId = userAccountId,
                Title = appointmentScheduleDto.Title,
                Description = appointmentScheduleDto.Description,
                StartTime = appointmentScheduleDto.StartTime.ToUniversalTime(),
                EndTime = appointmentScheduleDto.EndTime.ToUniversalTime(),
            };
        }
    }
}
