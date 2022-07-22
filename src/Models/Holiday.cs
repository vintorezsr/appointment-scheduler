using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentScheduler.Models
{
    /// <summary>
    /// Appointment schedule entity class.
    /// </summary>
    public class Holiday
    {
        public DateTime Date { get; set; }
    }
}
