using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Models
{
    public class Image : BaseEntity
    {
        public string Path { get; set; }
        public Image() { }
        public Image(String Path)
        {
            this.Path = Path;
        }
    }
}
