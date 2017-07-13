using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleStore.Api.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}
