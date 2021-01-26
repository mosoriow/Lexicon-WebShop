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
        public String Manufacture { get; set; }
        [Range(0, 1000)]
        public decimal Price { get; set; }
        public String Category { get; set; }
        public String SubCategory { get; set; }
        public ICollection<Image> Images { get; set; }
        public virtual ICollection<UserReview> UserReviews { get; set; }
        public int Availability { get; set; }
        public String Size { get; set; }
        public String Colour { get; set; }
        public int Discount { get; set; }

        public Product()
        {
            this.UserReviews = new List<UserReview>();
            this.Images = new List<Image>();
        }
    }
}
