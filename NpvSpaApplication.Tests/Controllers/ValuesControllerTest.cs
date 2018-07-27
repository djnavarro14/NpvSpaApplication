using Microsoft.VisualStudio.TestTools.UnitTesting;
using NpvSpaApplication.Controllers;
using NpvSpaApplication.Models;
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
            var controller = new ValuesController();

            // Act
            dynamic result = controller.Post(new NpvObjectModel()
            {
                InitialInvestment = 150000,
                CashFlows = new List<double>{ 50000, 25000 },
                LowerBoundDiscountRate = 1.2,
                UpperBoundDiscountRate = 1.4,
                DiscountRateIncrement = 0.1
            });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is IHttpActionResult);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(3, result.Content.Labels.Count);
            Assert.AreEqual(3, result.Content.Values.Count);
        }
    }
}
