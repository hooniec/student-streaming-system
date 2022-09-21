using Microsoft.AspNetCore.Mvc;
using Moq;
using StreamTec.Controllers;
using StreamTec.Models;
using System.Net;

namespace StreamTecTest
{
    [TestClass]
    public class UnitTest
    {
        //https://stackoverflow.com/questions/8818207/how-should-one-unit-test-a-net-mvc-controller
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

            Console.Write(test);
            Assert.IsTrue(test.IsCompleted);
        }

        [TestMethod]
        public void SearchAdminWithoutEntry()
        {
            var controller = new AdminController(_context);
            var result = controller.Index() as ViewResult;
            var test = controller.Search(searchID);
            
            Assert.IsTrue(test.IsCompleted);
        }

        //Do i create a mock?
        [TestMethod]
        public void RegisterTest()
        {
            var student = new Student { Email = "register@email.com", StudentId = "4569874" };

            var controller = new HomeController(_context);
            var result = controller.Register(student);
            Console.WriteLine(result.Exception);

            //Assert.AreEqual(result, student.StudentId);
            //Assert.AreEqual(result.Email, "register@email.com");            

            Assert.IsTrue(result.IsCompleted);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ValidLoginTest()
        {
            var student = new Student { Email = "ethan@email.com", StudentId = "2208266" };

            var controller = new HomeController(_context);
            var result = controller.Index(student);
            var index = controller.Index() as ViewResult;

            Console.WriteLine(result);
            Assert.IsNotNull(result);
            Assert.IsTrue(index.ViewName == "Index");
            Assert.IsInstanceOfType(result, typeof(Task<IActionResult>));
        }

        [TestMethod]
        public void InvalidLoginTest()
        {
            var student = new Student { StudentId = "2208266", Email = "ethan@email.com" };

            var controller = new HomeController(_context);
            var result = controller.Index(student);
            Console.Write(result);
            var index = controller.Index() as ViewResult;
            //Assert.AreEqual(HttpStatusCode.OK, result.Exception);
            Console.WriteLine(result.Exception);
           

            Console.Write(index);
            Assert.IsTrue(result.IsCompleted);
            Assert.IsTrue(index.ViewName == "Index");
        }
        [TestMethod]
        public void ValidLogoutTest()
        {
            var student = new Student { Email = "ethan@email.com", StudentId = "2208266" };

            var controller = new HomeController(_context);
            var result = controller.Index(student);
            var logout = controller.Logout();

            Assert.IsTrue(result.IsCompleted);
            Assert.IsTrue(logout.IsCompleted);
            Console.WriteLine(logout);
        }
    }
}



