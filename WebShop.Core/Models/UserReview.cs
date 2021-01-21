using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Core.Models
{
    public class UserReview : BaseEntity
    {
        public UserReview() { }
        public UserReview(String UserName, String Review, int Rating) {
            this.UserName = UserName;
            this.Review = Review;
            this.Rating = Rating;
                }
        public string UserName { get; set; }
        [DataType(DataType.MultilineText)]
        public string Review { get; set; }
        public int Rating { get; set; }
    }
}
