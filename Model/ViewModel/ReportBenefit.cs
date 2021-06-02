using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    [Serializable]
    public class ReportBenefit
    {
        public int DateBenefit { get; set; }
        public int MonthBenefit { get; set; }
        public int YearBenefit { get; set; }
        public int SumOrder { get; set; }
        public decimal Revenues { get; set; }
        public decimal Benefit { get; set; }
    }
}
