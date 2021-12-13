using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EC2_1601226.Data;
using EC2_1601226.Models;
using EC2_1601226.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace EC2_1601226.Controllers
{
    [Authorize(Roles = "Admin,Member")]
    public class BagsController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly EC2_1601226Context _context;
        

        public BagsController(EC2_1601226Context context, IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        // GET: Bags
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bag.ToListAsync());
        }

        // GET: Bags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bag = await _context.Bag
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bag == null)
            {
                return NotFound();
            }

            return View(bag);
        }

        // GET: Bags/Create
        
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Bags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Brand,Name,Image,Model,ManufactureDate,Quantity,Price")] BagCreateViewModel bag)
        {
            if (ModelState.IsValid) //are the validations all good?
            {
                
                    string uniqueFileName = null;
                    if (bag.Image != null)
                    {
                        string uploadfolder = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + bag.Image.FileName;
                        string filePath = Path.Combine(uploadfolder, uniqueFileName);
                        bag.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                    Bag newBag = new Bag
                    {
                        Brand = bag.Brand,
                        Name = bag.Name,
                        Model = bag.Model,
                        ManufactureDate = bag.ManufactureDate,
                        Quantity = bag.Quantity,
                        Price = bag.Price,
                        Image = uniqueFileName
                    };
                    _context.Add(newBag);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                
                
            }
            return View(bag);
        }

        // GET: Bags/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Bag bag = await _context.Bag.Where(x => x.Id == id).FirstOrDefaultAsync();

            if(bag == null)
            {
                return NotFound();
            }
            return View(bag);
        }

        // POST: Bags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Brand,Name,Image,Model,ManufactureDate,Quantity,Price")] BagCreateViewModel bags, IFormFile file)
        {
            if (id == null)
            {
                return NotFound();
            }

            Bag bag = await _context.Bag.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (bag == null)
            {
                return NotFound();
            }

            if (file != null || file.Length != 0)
            {
                string filename = System.Guid.NewGuid().ToString() + ".jpg";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", filename);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                bag.Image = filename;
            }
            string uniqueFileName = null;
            if (bags.Image != null)
                    {
                        string uploadfolder = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + bags.Image.FileName;
                        string filePath = Path.Combine(uploadfolder, uniqueFileName);
                        bags.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
           
                bag.Brand = bags.Brand;
                bag.Name = bags.Name;
                bag.Model = bags.Model;
                bag.ManufactureDate = bags.ManufactureDate;
                bag.Quantity = bags.Quantity;
                bag.Price = bags.Price;

                _context.Update(bag);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            
            if (ModelState.IsValid)
            {
                try
                {
                    
                    
                    Bag newBag = new Bag
                    {
                        Brand = bags.Brand,
                        Name = bags.Name,
                        Model = bags.Model,
                        ManufactureDate = bags.ManufactureDate,
                        Quantity = bags.Quantity,
                        Price = bags.Price,
                        Image = uniqueFileName
                    };
                    _context.Update(newBag);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                    _context.Update(bag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BagExists(bag.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bags);
        }

        // GET: Bags/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bag = await _context.Bag
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bag == null)
            {
                return NotFound();
            }

            return View(bag);
        }

        // POST: Bags/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bag = await _context.Bag.FindAsync(id);
            _context.Bag.Remove(bag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BagExists(int id)
        {
            return _context.Bag.Any(e => e.Id == id);
        }
    }
}
