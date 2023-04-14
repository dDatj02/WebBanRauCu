using ProjectCuoiki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectCuoiki.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        ProjectCuoiKiEntities db = new ProjectCuoiKiEntities();
        public ActionResult Index()
        {
            List<Cart> ListCart = GetCarts();
            ViewBag.subTotal = ListCart.Sum(i => i.total);
            ViewBag.shipping = ListCart.Sum(i => i.quantity) * 5000;
            ViewBag.total = ViewBag.subTotal + ViewBag.shipping;
            return View(ListCart);
        }
        public List<Cart> GetCarts()
        {
            List<Cart> ListCart = Session["Cart"] as List<Cart>;
            if(ListCart==null)
            {
                ListCart= new List<Cart>();
                Session["Cart"] = ListCart;
            }
            return ListCart;

        }
        public ActionResult AddCart(int idProduct)
        {
            List<Cart> ListCart=GetCarts();
            Cart p=ListCart.Find (n=>n.idProduct==idProduct);
            if(p==null)
            {
                p=new Cart(idProduct);
                ListCart.Add(p);
                return Redirect("Index");
            }
            else
            {
                p.quantity++;
                return Redirect("Index");
            }
        }
        public ActionResult RemoveCart(int idProduct)
        {
            List<Cart> ListCart = GetCarts();
            var p = ListCart.FirstOrDefault(i => i.idProduct == idProduct);
            ListCart.Remove(p);
            return Redirect("Index");
        }
    }
}