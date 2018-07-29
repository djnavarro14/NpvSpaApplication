using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NpvSpaApplication.Service;
using NpvSpaApplication.Models;

namespace NpvSpaApplication.Tests.Service
{
    [TestClass]
    public class ComputeServiceTest
    {
        [TestMethod]
        public void Npv()
        {
            // Arrange
            var helper = new ComputeService();
            var model = new NpvDataModel()
            {
                InitialInvestment = 150000,
                CashFlows = new List<double> { 50000, 25000 },
                DiscountRate = 1.2
            };

            // Act
            var result = helper.Npv(model);

            // Assert
            Assert.IsTrue(result is NpvResultModel);
            Assert.AreEqual(1.2, result.DiscountRate);
            Assert.AreEqual(-76182.26, result.NetPresentValue);
        }
    }
}
