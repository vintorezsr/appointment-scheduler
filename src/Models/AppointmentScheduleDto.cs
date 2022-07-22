using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentScheduler.Models
{
    /// <summary>
    /// Appointment schedule entity class.
    /// </summary>
    public class AppointmentScheduleDto
    {
        public Guid Id { get; set; }

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
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the appointment description (if any).
        /// </summary>
        public string? Description { get; set; }
    }
}
