using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaPortal.Models;
using System.Data;

namespace PizzaPortal.Controllers
{
  
        public class PizzaController : Controller
        {
            private ApplicationDbContext _context;

            public PizzaController(ApplicationDbContext context)
            {
                _context = context;
            }
        
        public IActionResult Index()
            {
                var pizzas = _context.Pizzas.Select(p => new
                {
                    Id = p.PizzaId,
                    Name = p.Name,
                    Price = p.Price,
                    Image = p.Image,
                    Quantity = p.AvailableQuantity,
                    CategoryName = p.Category.Name,
                    Date = p.PizzaAddedDate,
                }).ToList();
                return View(pizzas);
            }
            public IActionResult ViewDetails(int id)
            {
                var pizzas = _context.Pizzas.Where(p => p.PizzaId == id).Select(p => new
                {
                    Id = p.PizzaId,
                    Name = p.Name,
                    Price = p.Price,
                    Image = p.Image,
                    Quantity = p.AvailableQuantity,
                    CategoryName = p.Category.Name,
                    Date = p.PizzaAddedDate,
                }).ToList();
                ViewBag.Result = pizzas;
                return View(pizzas);
            }
        }
    }

