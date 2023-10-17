using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using Timesheets.Models;
using Timesheets.Services;

namespace Timesheets.Controllers
{
    public class TimesheetController : Controller
    {
        private ITimesheetService _timesheetService;

        public TimesheetController(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        public IActionResult Index()
        {
            ViewData["TimeSheet"] = _timesheetService.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult Index(TimesheetEntry timesheetEntry)
        {
            var timesheet = new Timesheet()
            {
                TimesheetEntry = timesheetEntry,
                TotalHours = (double)timesheetEntry.Hours
            };

            _timesheetService.Add(timesheet);

            var timesheets = _timesheetService.GetAllbyHours();
          
            ViewData["TimeSheets"] = timesheets;

            return View();
        }
        public FileResult DownloadReport()
        {
            var lstData =_timesheetService.GetAllbyHours();
            var sb = new StringBuilder();
            foreach (var data in lstData)
            {
                sb.AppendLine(data.Id + "," + data.TimesheetEntry.LastName + "," + data.TimesheetEntry.Project + "," 
                    + data.TimesheetEntry.FirstName + "," + data.TotalHours);
            }
            return File(new UTF8Encoding().GetBytes(sb.ToString()), "text/csv", "Timesheeet.csv");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}