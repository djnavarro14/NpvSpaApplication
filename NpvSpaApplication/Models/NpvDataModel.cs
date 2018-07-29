using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NpvSpaApplication.Models
{
    public class NpvDataModel
    {
        public double InitialInvestment { get; set; }

        public List<double> CashFlows { get; set; } = new List<double>();

        public double DiscountRate { get; set; } = 0.1;
    }
}