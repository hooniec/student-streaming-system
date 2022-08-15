using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StreamTec.Controllers;
using StreamTec.Models;
using System.Data.Entity.Infrastructure;
namespace StreamTecTest
{
    [TestClass]
    public class UnitTest
    {
        private WelTecContext _context;
        [TestMethod]
        public void TestHomeView()
        {
            var controller = new HomeController(_context);
            var result = controller.Index() as ViewResult;
            Assert.IsTrue(result.ViewName == "Index");

        }
    }
}