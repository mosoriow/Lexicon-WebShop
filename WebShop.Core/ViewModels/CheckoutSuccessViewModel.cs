using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.ViewModels
{
    public class CheckoutSuccessViewModel
    {
        public String id { get; set; }
        public CheckoutSuccessViewModel(String id)
        {
            this.id = id;
        }
    }
}
