using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalLondon.ViewModel
{
    public class MaturityDataViewModel
    {
        public string PolicyNumber { get; set; }
        public DateTime PolicyStartDate { get; set; }
        public string PolicyType { get; set; }
        public double ManagementFeeMultiplier { get; set; }
        public double Premiums { get; set; }
        public Enums.YesNo Membership { get; set; }
        public double DiscretionaryBonus { get; set; }
        public double UpliftPercentage { get; set; }
        public double MaturityValue { get; set; }
    }
}
