using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Models
{
    public class Product : BaseEntity
    {

        [StringLength(20)]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0, 1000)]
        public decimal Price { get; set; }
        public string Category { get; set; }
        public ICollection<string> Images { get; set; }
        public virtual ICollection<UserReview> UserReviews { get; set; }

        public Product()
        {
            this.UserReviews = new List<UserReview>();
            this.Images = new List<String>();
        }
    }
}
