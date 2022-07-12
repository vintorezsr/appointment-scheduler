﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentScheduler.Models
{
    public class AppointmentSchedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid UserAccountId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public UserAccount? UserAccount { get; set; }
    }
}
