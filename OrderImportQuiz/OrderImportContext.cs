using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderImportQuiz
{
    class OrderImportContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public OrderImportContext (DbContextOptions<OrderImportContext> options)
            :base(options)
        { }
    }
}
