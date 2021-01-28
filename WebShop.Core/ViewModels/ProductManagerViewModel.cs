using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.Models;

namespace WebShop.Core.ViewModels
{
    public class ProductManagerViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<Image> Images { get; set; }
        /*
        public Image file { get; set; }
        public Image file2 { get; set; }
        public Image file3 { get; set; }
        public Image file4 { get; set; }
        */
    }
}
