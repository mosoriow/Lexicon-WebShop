using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Models
{
    public class Manufacture : BaseEntity
    {
        public string Name { get; set; }
        public Manufacture() { }

        public Manufacture(String Name)
        {
            this.Name = Name;
        }
    }
}
