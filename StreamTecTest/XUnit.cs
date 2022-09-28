using Microsoft.AspNetCore.Mvc;
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
using Xunit.Sdk;
using Assert = Xunit.Assert;

namespace StreamTecTest
{
    public class XUnit
    {
        private DbContextOptions<WelTecContext> dbContextOptions;

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly WelTecContext _context;
        public XUnit(ITestOutputHelper testOutputHelper)
        {
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
                context.Enrollments.Add(new Enrollment { EnrollmentID = 2, StudentId = "1111111", StreamID = "IT-5115-Com-A-03" });
                context.Enrollments.Add(new Enrollment { EnrollmentID = 3, StudentId = "2222222", StreamID = "IT-5115-Com-A-03" });
                context.Enrollments.Add(new Enrollment { EnrollmentID = 4, StudentId = "3333333", StreamID = "IT-5115-Com-A-03" });
                context.Students.Add(new Student { StudentId = "2208266", Email = "ethan@email.com" });
                context.Streams.Add(new StreamTec.Models.Stream { StreamID = "IT-5115-Com-A-03", Room = "T605", Credits = 15, Day = "Monday", StartTime = "0800", EndTime = "1000", Capacity = 24 });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new WelTecContext(options))
            {
                var testStudentId = "2208266";                

                var adminController = new AdminController(context);
                var view = adminController.Search(testStudentId);
                var result = context.Enrollments.FirstOrDefault(s => s.StudentId == testStudentId);

                Assert.Equal(testStudentId, result.StudentId.ToString());
                Assert.True(view.IsCompletedSuccessfully);
            }
        }

        [Fact]
        public void DeleteTest()
        {
            var options = new DbContextOptionsBuilder<WelTecContext>()
                .UseInMemoryDatabase(databaseName: "localdb")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new WelTecContext(options))
            {
                context.Enrollments.Add(new Enrollment { EnrollmentID = 1, StudentId = "2208266", StreamID = "IT-5115-Com-A-03" });
                context.Enrollments.Add(new Enrollment { EnrollmentID = 2, StudentId = "1111111", StreamID = "IT-5115-Com-A-03" });
                context.Enrollments.Add(new Enrollment { EnrollmentID = 3, StudentId = "2222222", StreamID = "IT-5115-Com-A-03" });
                context.Enrollments.Add(new Enrollment { EnrollmentID = 4, StudentId = "3333333", StreamID = "IT-5115-Com-A-03" });
                context.Students.Add(new Student { StudentId = "2208266", Email = "ethan@email.com" });
                context.Streams.Add(new StreamTec.Models.Stream { StreamID = "IT-5115-Com-A-03", Room = "T605", Credits = 15, Day = "Monday", StartTime = "0800", EndTime = "1000", Capacity = 24 });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new WelTecContext(options))
            {
                //
                var deleteId = 1;

                //
                var test = context.Enrollments.ToList();
                Assert.Equal("4", test.Count.ToString());

                //
                var adminController = new AdminController(context);
                var view = adminController.Delete(deleteId);
                var testDeleted = context.Enrollments.ToList();

                //
                _testOutputHelper.WriteLine(testDeleted.Count.ToString());
                Assert.Equal("3", testDeleted.Count.ToString());
                Assert.True(view.IsCompletedSuccessfully);
            }
        }

        [Fact]
        public void AddTest()
        {
            var options = new DbContextOptionsBuilder<WelTecContext>()
                .UseInMemoryDatabase(databaseName: "localdb")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new WelTecContext(options))
            {
                context.Enrollments.Add(new Enrollment { EnrollmentID = 1, StudentId = "2208266", StreamID = "IT-5115-Com-A-03" });
                context.Enrollments.Add(new Enrollment { EnrollmentID = 2, StudentId = "1111111", StreamID = "IT-5115-Com-A-03" });
                context.Enrollments.Add(new Enrollment { EnrollmentID = 3, StudentId = "2222222", StreamID = "IT-5115-Com-A-03" });
                context.Enrollments.Add(new Enrollment { EnrollmentID = 4, StudentId = "3333333", StreamID = "IT-5115-Com-A-03" });
                context.Students.Add(new Student { StudentId = "2208266", Email = "ethan@email.com" });
                context.Streams.Add(new StreamTec.Models.Stream { StreamID = "IT-5115-Com-A-03", Room = "T605", Credits = 15, Day = "Monday", StartTime = "0800", EndTime = "1000", Capacity = 24 });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new WelTecContext(options))
            {
                //
                string studentID = "8888888";
                string streamID = "eight@email.com";

                //
                var test = context.Enrollments.ToList();
                Assert.Equal("4", test.Count.ToString());

                //
                var adminController = new AdminController(context);
                var view = adminController.Add(studentID, streamID);
                var testAdded = context.Enrollments.ToList();

                //
                _testOutputHelper.WriteLine(testAdded.Count.ToString());
                Assert.Equal("5", testAdded.Count.ToString());
                Assert.True(view.IsCompletedSuccessfully);
            }
        }

        [Fact]
        public void AdminIndexTest()
        {
            var options = new DbContextOptionsBuilder<WelTecContext>()
             .UseInMemoryDatabase(databaseName: "localdb")
             .Options;

            var adminController = new AdminController(_context);
            var result = adminController.Index() as ViewResult;

            Assert.Equal("AdminHome", result.ViewName);
        }

        [Fact]
        public void AddViewTest()
        {
            var options = new DbContextOptionsBuilder<WelTecContext>()
             .UseInMemoryDatabase(databaseName: "localdb")
             .Options;

            var adminController = new AdminController(_context);
            var result = adminController.AddView() as ViewResult;

            Assert.Equal("AdminAdd", result.ViewName);
        }

        [Fact]
        public void HomeIndexTest()
        {
            var options = new DbContextOptionsBuilder<WelTecContext>()
             .UseInMemoryDatabase(databaseName: "localdb")
             .Options;

            var adminController = new HomeController(_context);
            var result = adminController.Index() as ViewResult;

            Assert.Equal("Index", result.ViewName);
        }

        [Fact]
        public void RegisterTest()
        {
            var options = new DbContextOptionsBuilder<WelTecContext>()
               .UseInMemoryDatabase(databaseName: "localdb")
               .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new WelTecContext(options))
            {
                context.Enrollments.Add(new Enrollment { EnrollmentID = 1, StudentId = "2208266", StreamID = "IT-5115-Com-A-03" });
                context.Enrollments.Add(new Enrollment { EnrollmentID = 2, StudentId = "1111111", StreamID = "IT-5115-Com-A-03" });
                context.Enrollments.Add(new Enrollment { EnrollmentID = 3, StudentId = "2222222", StreamID = "IT-5115-Com-A-03" });
                context.Enrollments.Add(new Enrollment { EnrollmentID = 4, StudentId = "3333333", StreamID = "IT-5115-Com-A-03" });
                context.Students.Add(new Student { StudentId = "2208266", Email = "ethan@email.com" });
                context.Streams.Add(new StreamTec.Models.Stream { StreamID = "IT-5115-Com-A-03", Room = "T605", Credits = 15, Day = "Monday", StartTime = "0800", EndTime = "1000", Capacity = 24 });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new WelTecContext(options))
            {
                //
                var student = new Student { StudentId = "6666666", Email = "eight@email.com" };

                //
                var test = context.Students.ToList();
                Assert.Equal("1", test.Count.ToString());

                //
                var homeController = new HomeController(context);
                var view = homeController.Register(student);
                var testRegister = context.Students.ToList();

                //
                _testOutputHelper.WriteLine(testRegister.Count.ToString());
                //_testOutputHelper.WriteLine(view.Exception.ToString());
                Assert.Equal("2", testRegister.Count.ToString());
                Assert.True(view.IsCompletedSuccessfully);
            }
        }

        [Fact]
        public void HomeLoginTest()
        {
            var options = new DbContextOptionsBuilder<WelTecContext>()
               .UseInMemoryDatabase(databaseName: "localdb")
               .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new WelTecContext(options))
            {
                context.Enrollments.Add(new Enrollment { EnrollmentID = 1, StudentId = "2208266", StreamID = "IT-5115-Com-A-03" });
                context.Enrollments.Add(new Enrollment { EnrollmentID = 2, StudentId = "1111111", StreamID = "IT-5115-Com-A-03" });
                context.Enrollments.Add(new Enrollment { EnrollmentID = 3, StudentId = "2222222", StreamID = "IT-5115-Com-A-03" });
                context.Enrollments.Add(new Enrollment { EnrollmentID = 4, StudentId = "3333333", StreamID = "IT-5115-Com-A-03" });
                context.Students.Add(new Student { StudentId = "2208266", Email = "ethan@email.com" });
                context.Streams.Add(new StreamTec.Models.Stream { StreamID = "IT-5115-Com-A-03", Room = "T605", Credits = 15, Day = "Monday", StartTime = "0800", EndTime = "1000", Capacity = 24 });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new WelTecContext(options))
            {
                //
                var student = new Student { StudentId = "2208266", Email = "ethan@email.com" };

                //
                var test = context.Students.ToList();
                Assert.Equal("1", test.Count.ToString());

                //
                var homeController = new HomeController(context);
                var view = homeController.Index(student);
                var testLogin = context.Students.ToList();
                //RedirectToRouteResult result = (RedirectToRouteResult)homeController.Index(student);


                var controller = new StreamController(context);
                var result = controller.Index() as RedirectToRouteResult;

                //Assert.That(result.RouteValues["action"], Is.EqualTo("success"));

                //
                _testOutputHelper.WriteLine(testLogin.Count.ToString());
                //_testOutputHelper.WriteLine(view.Exception.ToString());
                Assert.Equal("1", testLogin.Count.ToString());
                Assert.True(view.IsCompletedSuccessfully);
            }
        }

        [Fact]
        public void HomeLogoutTest()
        {
            var options = new DbContextOptionsBuilder<WelTecContext>()
             .UseInMemoryDatabase(databaseName: "localdb")
             .Options;

            var adminController = new HomeController(_context);
            var result = adminController.Logout();

            
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
