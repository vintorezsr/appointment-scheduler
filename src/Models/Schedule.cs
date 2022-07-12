using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentScheduler.Models
{
    public class Schedule
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
