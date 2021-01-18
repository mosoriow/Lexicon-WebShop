using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Models
{
    public class UserReviews : BaseEntity
    {
        
        public string UserId { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }

    }
}
