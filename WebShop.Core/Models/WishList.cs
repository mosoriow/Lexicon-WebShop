﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Models
{
    public class WishList : BaseEntity
    {
        public  String productId { get; set; }
        public  String UserName { get; set; }

    }
}
