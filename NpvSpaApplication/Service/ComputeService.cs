using NpvSpaApplication.Models;
using System;
using System.Linq;

namespace NpvSpaApplication.Service
{
    public class ComputeService : IComputeService
    {
        public NpvResultModel Npv(NpvDataModel model)
        {
            if (model.InitialInvestment != 0)
            {
                model.CashFlows.Insert(0, -model.InitialInvestment);
                model.InitialInvestment = 0;
            }

            var npv = model.CashFlows.Select((c, n) => c / Math.Pow(1 + (model.DiscountRate / 100), n)).Sum();

            return new NpvResultModel
            {
                DiscountRate = model.DiscountRate,
                NetPresentValue = Math.Round(npv, 2)
            };
        }
    }
}