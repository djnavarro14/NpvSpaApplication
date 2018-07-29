using Microsoft.VisualStudio.TestTools.UnitTesting;
using NpvSpaApplication.Controllers;
using NpvSpaApplication.Models;
using NpvSpaApplication.Service;
using System.Collections.Generic;
using System.Web.Http;

namespace NpvSpaApplication.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        [TestMethod]
        public void Post()
        {
            // Arrange
            var controller = new ValuesController(new ComputeService());

            // Act
            dynamic result = controller.Post(new NpvDataModel()
            {
                InitialInvestment = 150000,
                CashFlows = new List<double>{ 50000, 25000 },
                DiscountRate = 1.2
            });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is IHttpActionResult);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(1.2, result.Content.DiscountRate);
            Assert.AreEqual(-76182.26, result.Content.NetPresentValue);
        }
    }
}
