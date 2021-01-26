using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Core.ViewModels
{
    public class ProductDetailsViewModel
    {
        public Product product { get; set; }
        public IEnumerable<Product> relatedProducts { get; set; }
        public UserReview userReview { get; set; }
    }
}
