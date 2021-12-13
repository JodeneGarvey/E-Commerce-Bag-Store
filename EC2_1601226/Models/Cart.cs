using EC2_1601226.Data;
using EC2_1601226.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EC2_1601226.Models
{
    public class Cart
    {
        private readonly EC2_1601226Context _dbContext;

        private Cart(EC2_1601226Context dbContext)
        {
            _dbContext = dbContext;
        }


        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItems> ShoppingCartItem { get; set; }


        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<EC2_1601226Context>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("cartId", cartId);

            return new Cart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Bag bag, int Amount)
        {
            var shoppingCartItem = _dbContext.ShoppingCartItems.SingleOrDefault(
                s => s.Bag.Id == bag.Id && s.ShoppingCartId == ShoppingCartId);

            if(shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItems
                {
                    ShoppingCartId = ShoppingCartId,
                    Bag = bag,
                    Amount = 1
                };

                _dbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _dbContext.SaveChanges();
        }

        public int RemoveFromCart(Bag bag)
        {
            var shoppingCartItem =
                _dbContext.ShoppingCartItems.SingleOrDefault(
                    s => s.Bag.Id == bag.Id && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if(shoppingCartItem != null)
            {
                if(shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _dbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _dbContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItems> GetShoppingCartItems()
        {
            return ShoppingCartItem ??
                (ShoppingCartItem =
                _dbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Include(s => s.Bag)
                .ToList());
        }

        public void ClearCart()
        {
            var cartItems = _dbContext.ShoppingCartItems.Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _dbContext.ShoppingCartItems.RemoveRange(cartItems);

            _dbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _dbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Bag.Price * c.Amount).Sum();
            return total;
        }
       
    }
}
