using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EC2_1601226.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int BagId { get; set; }

        public int Amount { get; set; }

        public decimal UnitPrice { get; set; }

        public virtual Bag Bag { get; set; }

        public virtual Order Order { get; set; }
    }
}
