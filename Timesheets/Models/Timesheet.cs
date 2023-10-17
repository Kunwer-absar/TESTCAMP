using System.ComponentModel.DataAnnotations;

namespace Timesheets.Models
{
    public class Timesheet
    {
        [Key]
        public int Id { get; set; }

        public virtual TimesheetEntry TimesheetEntry { get; set; }
        public double TotalHours { get; set; }
    }
}
