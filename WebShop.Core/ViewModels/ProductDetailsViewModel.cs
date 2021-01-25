﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.Models;

namespace WebShop.Core.ViewModels
{
    public class ProductDetailsViewModel
    {
        public Product product { get; set; }
        public IEnumerable<Product> relatedProducts { get; set; }
        public UserReview userReview { get; set; }
        public IEnumerable<String> subCategories { get; set; }
        public IEnumerable<String> manufactures { get; set; }
    }
}
