using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    [Serializable]
    public class ReportProduct
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int SumProduct { get; set; }
    }
}
