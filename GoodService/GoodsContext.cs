using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelLib;
using System.Data.Entity;

namespace GoodsService
{
    public class GoodsContext : DbContext
    {
        public GoodsContext()
            : base("GoodsConnection")
        {

        }
        public DbSet<Good> Goods { get; set; }
    }
}
