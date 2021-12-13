using EC2_1601226.Data;
using EC2_1601226.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EC2_1601226.ViewModels
{
    public partial class ShoppingCartViewModel
    {
        public Cart Cart { get; set; }

        public decimal CartTotal { get; set; }

    }
}
