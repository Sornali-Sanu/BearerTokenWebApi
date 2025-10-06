using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BearerToken.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderNo { get; set; }
        public string CustomerName { get; set; }
        public bool IsPaid { get; set; }
        public string ImageUrl { get; set; }
        public virtual ICollection<OrderedItem>OrderedItems { get; set; }

    }
    public class OrderedItem 
    {
        public int OrderedItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public virtual Product Product { get; set; }

    }
    public class Product 
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
}