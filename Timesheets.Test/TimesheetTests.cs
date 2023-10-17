using Castle.Core.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using Timesheets.Infrastructure;
using Timesheets.Models;
using Timesheets.Repositories;
using Timesheets.Services;

namespace Timesheets.Test
{
    public class TimesheetTests
    {
        [Fact]
        public void GivenAValidTimesheet_ThenAddTimesheetToInMemoryDatabase()
        {
            //Arrange
            var timesheet = new Timesheet();
            var timesheetEntry = new TimesheetEntry()
            {
                Id = 1,
                Date = "01/09/2023",
                Project = "Test Project",
                FirstName = "Test",
                LastName = "Test",
                Hours = 7.5
            };
            timesheet.Id = 1;
            timesheet.TimesheetEntry = timesheetEntry;
            timesheet.TotalHours = (double)timesheetEntry.Hours;

            var mockRepository = new Mock<ITimesheetRepository>();
            var timesheetService = new TimesheetService(mockRepository.Object);

            var timesheet1 = new Timesheet();
            var timesheetEntry1 = new TimesheetEntry()
            {
                Id = 1,
                Date = "01/09/2023",
                Project = "Test Project",
                FirstName = "Test",
                LastName = "Test",
                Hours = 7.5
            };
            timesheet.Id = 2;
            timesheet1.TimesheetEntry = timesheetEntry1;
            timesheet1.TotalHours = (double)timesheetEntry1.Hours;

                      // Act
            timesheetService.Add(timesheet);
            timesheetService.Add(timesheet1);

            // Assert
            mockRepository.Verify(repo => repo.AddTimesheet(It.IsAny<Timesheet>()), Times.AtLeast(2));
        }
    }
}
