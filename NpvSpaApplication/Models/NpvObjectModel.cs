using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NpvSpaApplication.Models
{
    public class NpvObjectModel
    {
        [Display(Name = "Initial Investment:")]
        public double InitialInvestment { get; set; }

        public double CashFlow { get; set; }

        public List<double> CashFlows { get; set; } = new List<double>();

        [Display(Name = "Lower Bound Discount Rate:")]
        public double LowerBoundDiscountRate { get; set; } = 1.2;

        [Display(Name = "Upper Bound Discount Rate:")]
        public double UpperBoundDiscountRate { get; set; } = 1.4;

        [Display(Name = "Discount Rate Increment:")]
        public double DiscountRateIncrement { get; set; } = 0.1;
    }
}