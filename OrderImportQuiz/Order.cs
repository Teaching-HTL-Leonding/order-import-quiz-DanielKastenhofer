using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderImportQuiz
{
    class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; }
        
        [Column(TypeName = "decimal(8,2)")]
        public double OrderValue { get; set; }

        public Customer Customer { get; set; }

    }
}
