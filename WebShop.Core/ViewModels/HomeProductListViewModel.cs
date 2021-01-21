using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.Models;


namespace WebShop.Core.ViewModels
{
    public class HomeProductListViewModel
    {
        public IEnumerable<Product> SuggestedProducts { get; set; }
        public IEnumerable<Product> LatestProducts { get; set; }
    }
}
