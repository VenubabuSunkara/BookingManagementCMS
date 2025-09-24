using Booking.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class ScheduleConflictViewModel
    {
        public int Id { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? Description { get; set; }
        public ConflictType Type { get; set; }
        //public DriverVehicleAssignment Assignment { get; set; }
        public int AssignmentId { get; set; }

        public bool HasConflict(ScheduleConflictViewModel other)
        {
            return StartTime < other.EndTime && EndTime > other.StartTime;
        }

        public TimeSpan GetDuration()
        {
            return EndTime - StartTime;
        }
    }
}
