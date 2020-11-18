using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderImportQuiz
{
    class Customer
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public double CreditLimit { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
