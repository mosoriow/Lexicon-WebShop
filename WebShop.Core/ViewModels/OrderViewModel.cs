using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.Models;

namespace WebShop.Core.ViewModels {

    public class OrderViewModel {
        public Order Order { get; set; }
        public BasketItemViewModel BasketItemViewModel { get; set; }

    public OrderViewModel(Order Order, BasketItemViewModel BasketItemViewModel)
        {
            this.Order = Order;
            this.BasketItemViewModel = BasketItemViewModel;
        }
    }


}
