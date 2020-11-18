using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderImportQuiz
{
    class Controller
    {
        private string[] _args;
        private OrderImportContext _context;
        private OrderImportContextFactory _factory;

        public Controller(String[] args)
        {
            _factory = new OrderImportContextFactory();
            _context = _factory.CreateDbContext(args);
        }


        public async Task Delete()
        {
            _context.Customers.RemoveRange(_context.Customers);
            _context.Orders.RemoveRange(_context.Orders);
            await _context.SaveChangesAsync();
        }

        public async Task Import(string[] customerText, string[] orderText)
        {
            foreach (var customer in customerText.Skip(1).ToList())
            {
                var customerToAdd = new Customer{
                    Name = customer.Split("\t")[0],
                    CreditLimit = (double)decimal.Parse(customer.Split("\t")[1])
                };

                await _context.Customers.AddAsync(customerToAdd);
            }

            await _context.SaveChangesAsync();
            var customers = await _context.Customers.ToListAsync();
            foreach (var customer in customers)
            {
                List<Order> orders = new();
                foreach (var order in orderText.Skip(1).ToList())
                {
                    if (!order.Split("\t")[0].Equals(customer.Name)) continue;

                    var newOrder = new Order
                    {
                        OrderDate = DateTime.Parse(order.Split("\t")[1]),
                        OrderValue = int.Parse(order.Split("\t")[2]),
                        Customer = customer,
                        CustomerId = customer.Id
                    };
                    orders.Add(newOrder);
                    await _context.Orders.AddAsync(newOrder);
                }

                customer.Orders = orders;
            }

            await _context.SaveChangesAsync();
        }

        public async Task Check()
        {
            var customers = await _context.Customers.ToListAsync();
            var orders = await _context.Orders.ToListAsync();

            foreach (var customer in customers)
            {
                var orderValueSum = orders
                    .Where(x => customer.Id == x.CustomerId)
                    .Sum(x => x.OrderValue);
                if (orderValueSum > customer.CreditLimit)
                {
                    Console.WriteLine($"{customer.Name}:\tLimit: {customer.CreditLimit} OrderValue: {orderValueSum}");
                }
            }
        }


    }
}
