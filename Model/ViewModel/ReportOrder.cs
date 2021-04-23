using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    [Serializable]
    public class ReportOrder
    {
        public int MonthOrder { get; set; }
        public int YearOrder { get; set; }
        public int SumOrder { get; set; }
    }
}
