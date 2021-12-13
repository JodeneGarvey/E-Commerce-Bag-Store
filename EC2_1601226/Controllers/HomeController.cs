using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EC2_1601226.Models;
using EC2_1601226.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using EC2_1601226.Data;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace EC2_1601226.Controllers
{
    [Authorize(Roles = "Admin,Member,User")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public readonly EC2_1601226Context Context;

        public readonly IHostingEnvironment hostingEnvironment;

        public HomeController(ILogger<HomeController> logger,EC2_1601226Context context, IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            Context = context;
            
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var items = Context.Bag.ToList();
            return View(items);
        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult Create(BagCreateViewModel vm)
        {
            string fileName = UploadFile(vm);
            var bag = new Bag
            {

                Brand = vm.Brand,
                Name = vm.Name,
                Model = vm.Model,
                ManufactureDate = vm.ManufactureDate,
                Quantity = vm.Quantity,
                Price = vm.Price,
                Image = fileName
            };
            Context.Add(bag);
            Context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
            

        
        private string UploadFile(BagCreateViewModel vm)
        { 
            string fileName = null;
            if(vm.Image != null)
            {
                string uploadfolder = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "_" + vm.Image.FileName;
                string filePath = Path.Combine(uploadfolder, fileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vm.Image.CopyTo(fileStream);
                }
            }
            return fileName;
        }
        

       [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Contact()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }

        
        
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
