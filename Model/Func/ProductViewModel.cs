using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Func
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        public int CateID { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string CateName { get; set; }
        public string CateAlias { get; set; }
        public string Alias { get; set; }
        public DateTime? CreatedDay { get; set; }
    }
}
