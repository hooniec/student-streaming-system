using Microsoft.AspNetCore.Mvc;
using Moq;
using StreamTec.Controllers;
using StreamTec.Models;
namespace StreamTecTest
{
    [TestClass]
    public class UnitTest
    {
        string searchID = "2208266";

        public WelTecContext _context;

        [TestInitialize]
        public void Initialize()
        {

        }

        [TestMethod]
        public void TestHomeView()
        {
            var controller = new HomeController(_context);
            var result = controller.Index() as ViewResult;
            Assert.IsTrue(result.ViewName == "Index");
        }

        [TestMethod]
        public void TestAdminHome()
        {
            var controller = new AdminController(_context);
            var result = controller.Index() as ViewResult;
            Assert.IsTrue(result.ViewName == "AdminHome");
        }

        [TestMethod]
        public void SearchAdminWithEntry()
        {
            var controller = new AdminController(_context);
            //var result = controller.Index() as ViewResult;
            var test =  controller.Search(searchID);

            Assert.IsNotNull(test, "Expected not Null");
            Console.Write(test);
            Assert.IsTrue(test.IsCompleted);
        }

        [TestMethod]
        public void SearchAdminWithoutEntry()
        {
            var controller = new AdminController(_context);
            var result = controller.Index() as ViewResult;
            var test = controller.Search(searchID);
            
            Assert.IsNotNull(test, "Expected Not Null");
            Assert.IsTrue(test.IsCompleted);
        }

        [TestMethod]
        public void RegisterTest()
        {
            var student = new Student { Email = "register@email.com", StudentId = "4569874" };

            var controller = new HomeController(_context);
            var result = controller.Register(student);

            Assert.AreEqual(student.StudentId, "4569874");
            Assert.AreEqual(student.Email, "register@email.com");

            Assert.IsTrue(result.IsCompleted);

        }

        [TestMethod]
        public void ValidLoginTest()
        {
            var student = new Student { Email = "ethan@email.com", StudentId = "2208266" };

            var controller = new HomeController(_context);
            var result = controller.Index(student);

            var index = controller.Index() as ViewResult;

            Assert.AreEqual(student.StudentId, "2208266");
            Assert.AreEqual(student.Email, "ethan@email.com");

            Assert.IsTrue(result.IsCompleted);
            Assert.IsTrue(index.ViewName == "Index");

        }

        [TestMethod]
        public void InvalidLoginTest()
        {
            var student = new Student { Email = "badEmail", StudentId = "Test" };

            var controller = new HomeController(_context);
            var result = controller.Index(student);

            var index = controller.Index() as ViewResult;

            Assert.AreEqual(student.StudentId, "Test");
            Assert.AreEqual(student.Email, "badEmail");

            Assert.IsTrue(result.IsCompleted);
            Console.WriteLine(result);
            Assert.IsTrue(index.ViewName == "Index");
        }
    }
}



