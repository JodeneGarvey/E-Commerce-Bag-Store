using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EC2_1601226.Data;
using EC2_1601226.Models;
using EC2_1601226.Repository;
using EC2_1601226.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EC2_1601226.Controllers
{
    [AllowAnonymous]
    public class ShoppingCartController : Controller
    {

        private readonly IBagRepository _bagRepository;
        private readonly Cart _shoppingcart;
        //private readonly EC2_1601226Context _context;

        public ShoppingCartController(IBagRepository bagRepository,Cart shoppingcart)
        {
            _bagRepository = bagRepository;
            _shoppingcart = shoppingcart;
        }

        public ViewResult Index()
        {
            var items = _shoppingcart.GetShoppingCartItems();
            _shoppingcart.ShoppingCartItem = items;

            var sCVM = new ShoppingCartViewModel
            {
                Cart = _shoppingcart,
                CartTotal = _shoppingcart.GetShoppingCartTotal()
            };

            return View(sCVM);

        }

        public RedirectToActionResult AddToShoppingCart(int bagId)
        {


            var selectedBag = _bagRepository.Bags.FirstOrDefault(p => p.Id == bagId);
            if (selectedBag != null)
            {
                _shoppingcart.AddToCart(selectedBag, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult ReoveFromCart(int bagId)
        {
            var selectedBag = _bagRepository.Bags.FirstOrDefault(p => p.Id == bagId);

            if(selectedBag != null)
            {
                _shoppingcart.RemoveFromCart(selectedBag);
            }
            return RedirectToAction("Index");
        }
        
    }
}