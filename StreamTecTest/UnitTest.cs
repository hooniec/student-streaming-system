using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.Protocol.Core.Types;
using StreamTec.Controllers;
using StreamTec.Models;
using System.Data.Entity.Infrastructure;
namespace StreamTecTest
{
    [TestClass]
    public class UnitTest
    {
        string searchID = "2208266";

        private WelTecContext _context;
        [TestMethod]
        public void TestHomeView()
        {
            var controller = new HomeController(_context);
            var result = controller.Index() as ViewResult;
            Assert.IsTrue(result.ViewName == "Index");
        }
        //[TestMethod]
        //public void TestStreamIndex()
        //{
        //    var controller = new StreamController(_context);
        //    var result = controller.Index() as ViewResult;
        //    Assert.IsTrue(result.ViewName == "");
        //}
        [TestMethod]
        public void TestAdminHome()
        {
            var controller = new AdminController(_context);
            var result = controller.Index() as ViewResult;
            Assert.IsTrue(result.ViewName == "AdminHome");
        }

        [TestMethod]
        public void SearchAdmin()
        {
            var controller = new AdminController(_context);
            var result = controller.Index() as ViewResult;

            var test = controller.Search(searchID);



            Assert.IsTrue(result.ViewName == "AdminHome");
        }
    }
}