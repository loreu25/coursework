using System;
using System.Collections.Generic;

namespace CafeOrderManager.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MenuDishId { get; set; }
        public MenuDish MenuDish { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
