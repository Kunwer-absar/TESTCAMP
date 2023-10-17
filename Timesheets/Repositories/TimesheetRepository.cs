using Microsoft.EntityFrameworkCore;
using Timesheets.Infrastructure;
using Timesheets.Models;

namespace Timesheets.Repositories
{
    public interface ITimesheetRepository
    {
        void AddTimesheet(Timesheet timesheet);
        IList<Timesheet> GetAllTimesheets();
        IEnumerable<Timesheet> GetAllTimesheetsbyHours();
    }

    public class TimesheetRepository : ITimesheetRepository
    {
        private DataContext _context;

        public TimesheetRepository(DataContext context)
        {
            _context = context;
        }
        public void AddTimesheet(Timesheet timesheet)
        {
            _context.Timesheets.Add(timesheet);
            _context.SaveChanges();
            //Console.Write(_context.Timesheets);
        }

        public IList<Timesheet> GetAllTimesheets()
        {
            var timesheets = _context.Timesheets.Include(x=>x.TimesheetEntry).ToList();
            timesheets = timesheets.OrderByDescending(x => x.TotalHours).ToList();
            return timesheets;
        }
        public IEnumerable<Timesheet> GetAllTimesheetsbyHours()
        {
            var timesheetsbyhour = _context.Timesheets.Include(x => x.TimesheetEntry).ToList();
            timesheetsbyhour = timesheetsbyhour.OrderByDescending(x => x.TimesheetEntry.Hours).ToList();
            return timesheetsbyhour;
        }
    }
}
