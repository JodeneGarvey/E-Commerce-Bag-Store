using EC2_1601226.Data;
using EC2_1601226.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EC2_1601226.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Cart _shoppingcart;
        private readonly EC2_1601226Context _context;

        public OrderRepository(EC2_1601226Context context, Cart shoppingcart)
        {
            _context = context;
            _shoppingcart = shoppingcart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderDate = DateTime.Now;
            _context.Order.Add(order);

            var shoppingCartItems = _shoppingcart.GetShoppingCartItems();

            foreach(var item in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = item.Amount,
                    BagId = item.Bag.Id,
                    OrderId = order.Id,
                    UnitPrice = item.Bag.Price
                };
                _context.OrderDetails.Add(orderDetail);
            }
            _context.SaveChanges();
        }
    }
}
