namespace AppointmentScheduler.Models
{
    /// <summary>
    /// Schedule input class.
    /// </summary>
    public class Schedule
    {
        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        public DateTime EndTime { get; set; }
    }
}
