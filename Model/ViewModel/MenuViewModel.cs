using System;
using Model.EnityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    [Serializable]
    public class MenuViewModel
    {
        public Menu Menu { get; set; } 
        public MenuGroup MenuGroup { get; set; }
    }
}
