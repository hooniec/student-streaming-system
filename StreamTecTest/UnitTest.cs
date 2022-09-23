using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using StreamTec.Controllers;
using StreamTec.Models;
using System.IO;
using System.Net;

namespace StreamTecTest
{
    [TestClass]
    public class UnitTest
    {
        //https://stackoverflow.com/questions/8818207/how-should-one-unit-test-a-net-mvc-controller
        string searchID = "2208266";

        private WelTecContext _context;
        public List<Student> StudentList()
        {
            var studentsList = _context.Students.ToList();

            return studentsList;
        }

        //private Mock<WelTecContext> _mockRepository;
        //private ModelStateDictionary _modelState;
        //private IContactManagerService _service;

        [TestInitialize]
        public void Initialize()
        {
            //_mockRepository = new Mock<WelTecContext>();
            //_modelState = new ModelStateDictionary();
            //_service = new ContactManagerService(new ModelStateWrapper(_modelState), _mockRepository.Object);

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
            Console.WriteLine(test.Exception);
            
            Assert.IsTrue(test.IsCompleted);
        }

        //Do i create a mock?
        [TestMethod]
        public void RegisterTest()
        {
            var studentsList = _context.Students.ToList();
            var student = new Student { StudentId = "2208266", Email = "ethan@email.com" };
            var studentObj = _context.Students.Where(s => s.StudentId.Equals(studentsList) && s.Email.Equals(student.Email));
            var controller = new HomeController(_context);
            var result = controller.Register(student);
            Console.WriteLine(result.Exception);      

            Assert.IsTrue(result.IsCompleted);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ValidLoginTest()
        {
            var student = new Student { StudentId = "2208266", Email = "ethan@email.com" };

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
            var obj = _context.Students.Where(s => s.StudentId.Equals(student.StudentId) && s.Email.Equals(student.Email)).FirstOrDefault();
            var controller = new HomeController(_context);
            //https://dzone.com/articles/unit-testing-data-access-in-aspnet-core
            Console.WriteLine(obj);
            var result = controller.Index(obj);

            Console.WriteLine(student.Email + " " + student.StudentId);
            var index = controller.Index() as ViewResult;
            //Assert.AreEqual(HttpStatusCode.OK, result.Exception);
            Console.WriteLine(result.Exception);

            //Assert.AreEqual(student, result);
            Console.Write(result.Result);
            //Assert.IsTrue(result.IsCompletedSuccessfully);
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



