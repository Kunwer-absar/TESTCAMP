Objective 1

The unit test runs fine as it saves the data correctly.

Issue 1:(Critical)
Discription:
While Testing the "Add Timesheet" i have faced the issues below:
The issue is in getting the records and the issue is that when you get the records it only  get the object of "TimesheetEntry"
of lastest one, and object of "TimesheetEntry" of all previously saved records were feteched as null. 
And it is because of the following reason:
	As in Entity Framework every model is treated as a table, and while geting the record we have no relation between the models
    to get that object from the records in our query.

Solution:
I Added .include(x=>x.timesheetEntry) at TimesheetRepository.cs in fuction of GetAllTimesheets(). to have a inner join relation 
with both of the models (table in database) as they have one to one relation.

now the function is look like

 public IList<Timesheet> GetAllTimesheets()
        {
            var timesheets = _context.Timesheets.Include(x=>x.TimesheetEntry).ToList();
            return timesheets;
        }

I hope i find the correct issue in it and I solved it correctly as well

Other issues are as follows:
Issue 2 (High)
In hours we can add alphabets.


Solutions
Added the pattern in it.


So there are some validation checks are missing as well.

Issue 3(High)

Also there is no exception handling in the application as well.
