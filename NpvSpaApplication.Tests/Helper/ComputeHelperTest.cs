using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NpvSpaApplication.Helper;
using NpvSpaApplication.Models;

namespace NpvSpaApplication.Tests.Controllers
{
    [TestClass]
    public class ComputeHelperTest
    {
        readonly NpvObjectModel model = new NpvObjectModel()
        {
            InitialInvestment = 150000,
            CashFlows = new List<double> { 50000, 25000 },
            LowerBoundDiscountRate = 1.2,
            UpperBoundDiscountRate = 1.4,
            DiscountRateIncrement = 0.1
        };

        [TestMethod]
        public void NpvCollection()
        {
            // Arrange
            var helper = new ComputeHelper();

            // Act
            var result = helper.NpvCollection(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Labels.Count);
            Assert.AreEqual(3, result.Values.Count);
        }

        [TestMethod]
        public void Npv()
        {
            // Arrange
            var helper = new ComputeHelper();

            // Act
            var result = helper.Npv(model, model.LowerBoundDiscountRate);

            // Assert
            Assert.AreEqual(-76182.26, result);
        }
    }
}
