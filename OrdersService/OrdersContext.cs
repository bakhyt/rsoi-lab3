using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using ModelLib;

namespace OrdersService
{
    public class OrdersContext : DbContext
    {
        public OrdersContext()
            : base("OrdersConnection")
        {

        }
        public DbSet<Order> Orders { get; set; }
    }
}
