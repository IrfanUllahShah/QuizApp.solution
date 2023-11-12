using InfraStructure.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.ViewModels
{
    public class ListPagerVm<T>
    {
        public List<T> list{ get; set; }
        public Pager pager { get; set; }
       
    }
}
