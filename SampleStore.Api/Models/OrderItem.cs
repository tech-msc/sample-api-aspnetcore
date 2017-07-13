using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleStore.Api.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
