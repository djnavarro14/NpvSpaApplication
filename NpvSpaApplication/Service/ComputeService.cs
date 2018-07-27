using NpvSpaApplication.Models;
using System;
using System.Linq;

namespace NpvSpaApplication.Service
{
    public class ComputeService : IComputeService
    {
        public NpvResultModel NpvCollection(NpvObjectModel model)
        {
            var result = new NpvResultModel();
            var discountRate = model.LowerBoundDiscountRate;
            while (discountRate <= model.UpperBoundDiscountRate)
            {
                result.Labels.Add($"{discountRate}%");
                result.Values.Add(Npv(model, discountRate));
                discountRate = Math.Round(discountRate + model.DiscountRateIncrement, 2);
            };
            return result;
        }

        public double Npv(NpvObjectModel model, double discountRate)
        {
            if (model.InitialInvestment != 0)
            {
                model.CashFlows.Insert(0, -model.InitialInvestment);
                model.InitialInvestment = 0;
            }

            var npv = model.CashFlows.Select((c, n) => c / Math.Pow(1 + (discountRate / 100), n)).Sum();
            return Math.Round(npv, 2);
        }
    }
}