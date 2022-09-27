using Microsoft.EntityFrameworkCore;
using StreamTec.Controllers;
using StreamTec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Assert = Xunit.Assert;

namespace StreamTecTest
{
    public class XUnit
    {
        private DbContextOptions<WelTecContext> dbContextOptions;

        private readonly ITestOutputHelper _testOutputHelper;

        public XUnit(ITestOutputHelper testOutputHelper)
        {
            var localdb = $"TestDB{DateTime.Now.ToFileTimeUtc()}";
            dbContextOptions = new DbContextOptionsBuilder<WelTecContext>()
                .UseInMemoryDatabase(localdb)
                .Options;

            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void SearchTest()
        {
            var options = new DbContextOptionsBuilder<WelTecContext>()
            .UseInMemoryDatabase(databaseName: "localdb")
            .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new WelTecContext(options))
            {
                context.Enrollments.Add(new Enrollment { EnrollmentID = 1, StudentId = "2208266", StreamID = "IT-5115-Com-A-03" });
                context.Students.Add(new Student { StudentId = "2208266", Email = "ethan@email.com" });
                context.Streams.Add(new StreamTec.Models.Stream { StreamID = "IT-5115-Com-A-03", Room = "T605", Credits = 15, Day = "Monday", StartTime = "0800", EndTime = "1000", Capacity = 24 });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new WelTecContext(options))
            {
                var adminController = new AdminController(context);
                var students = adminController.Search("2208266");
                _testOutputHelper.WriteLine(students.Result.ToString());
                Assert.True(students.IsCompletedSuccessfully);
            }
        }
    }
}
