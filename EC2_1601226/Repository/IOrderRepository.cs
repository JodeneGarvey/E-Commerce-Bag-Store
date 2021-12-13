using EC2_1601226.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EC2_1601226.Repository
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
        
    }
}
