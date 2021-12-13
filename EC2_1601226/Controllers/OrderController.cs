using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EC2_1601226.Models;
using EC2_1601226.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EC2_1601226.Controllers
{
    [AllowAnonymous]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly Cart _shoppingcart;

        public OrderController(IOrderRepository orderRepository, Cart shoppingcart)
        {
            _orderRepository = orderRepository;
            _shoppingcart = shoppingcart;
        }
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var items = _shoppingcart.GetShoppingCartItems();
            _shoppingcart.ShoppingCartItem = items;

            if(_shoppingcart.ShoppingCartItem.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty, add the bags to the cart");
            }

            if(ModelState.IsValid)
            {
                _orderRepository.CreateOrder(order);
                _shoppingcart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }
            return View(order);
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thanks for your order! :)";
            return View();
        }
    }
}