using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EC2_1601226.Models
{
    public class ShoppingCartItems
    {
        [Key]
        public int ShoppingCartItemsId { get; set; }
        public Bag Bag { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
