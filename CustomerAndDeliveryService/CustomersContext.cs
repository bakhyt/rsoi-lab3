using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using ModelLib;

namespace CustomerAndDeliveryService
{
    public class CustomersContext : DbContext
    {
        public CustomersContext()
            : base("CustomersConnection")
        {

        }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
