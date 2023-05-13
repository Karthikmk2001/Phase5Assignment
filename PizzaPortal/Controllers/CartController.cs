using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PizzaPortal.Helpers;
using PizzaPortal.Models;
using System.Data;

namespace PizzaPortal.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        /*[Authorize(Roles = "user")]*/
        public IActionResult Index()
        {
            var cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if (cart.IsNullOrEmpty())
            {
                return RedirectToAction("EmptyCart");
            }
            else
            {
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Pizza.Price * item.Quantity);
                return View();
            }
        }

        public IActionResult Buy(int id)
        {
            if (SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item() { Pizza = _context.Pizzas.SingleOrDefault(p => p.PizzaId == id), Quantity = 1 });
                SessionHelper.setObjectAsJson(HttpContext.Session, "cart", cart);
            }

            else
            {
                List<Item> cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExists(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item() { Pizza = _context.Pizzas.SingleOrDefault(p => p.PizzaId == id), Quantity = 1 });
                }
                SessionHelper.setObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        public int isExists(int id)
        {
            List<Item> cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Pizza.PizzaId == id)
                {
                    return i;
                }
            }
            return -1;
        }

        /*[Authorize(Roles = "user")]*/
        public IActionResult Remove(int id)
        {
            List<Item> cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExists(id);
            cart.RemoveAt(index);
            SessionHelper.setObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

       /* [Authorize(Roles = "user")]*/
        public IActionResult Checkout()
        {
            List<Item> cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.count = cart.Count();
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Pizza.Price * item.Quantity);
            return View();
        }
        public IActionResult MakePayment()
        {
            return View();
        }
        [HttpPost]

        /*[Authorize(Roles = "user")]*/
        public IActionResult MakePayment(Address addr)
        {
            _context.Addresses.Add(addr);
            _context.SaveChanges();
            ViewBag.address = addr;
            List<Item> cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.count = cart.Count();
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Pizza.Price * item.Quantity);
            return View();
        }

        /*[Authorize(Roles = "user")]*/
        public IActionResult EmptyCart()
        {
            List<Item> cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if (cart.IsNullOrEmpty())
            {
                return View();
            }
            else
            {
                cart.Clear();
            }
            return View();
        }


        public IActionResult Payment()
        {
            List<Item> cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.count = cart.Count();
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Pizza.Price * item.Quantity);
            var addr = _context.Addresses.FirstOrDefault();
            ViewBag.address = addr;
            return View();
        }
    }

}
    