using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.Models;

namespace WebShop.Core.ViewModels
{
    public class ProductFilterViewModel
    {
        public IEnumerable<String> subCategories { get; set; }
        public IEnumerable<String> manufactures { get; set; }
        public IEnumerable<Product> products { get; set; }
        public Product SelectedProduct { get; set; }
    }
}
