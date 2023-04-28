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
           
            return View(ListCart);
        }
        public ActionResult CheckOut()
        {
            List<Cart> ListCart = GetCarts();
            ViewBag.subTotal = ListCart.Sum(i => i.total);
            ViewBag.shipping = ListCart.Sum(i => i.quantity) * 5000;
            ViewBag.total = ViewBag.subTotal + ViewBag.shipping;
            return View(ListCart.ToList());
        }
        public ActionResult OrderCart(string name,string email,string phone,string address)
        {
            List<Cart> ListCart = GetCarts();
            ViewBag.subTotal = ListCart.Sum(i => i.total);
            ViewBag.shipping = ListCart.Sum(i => i.quantity) * 5000;
            ViewBag.total = (ViewBag.subTotal + ViewBag.shipping);
            Invoice invoice = new Invoice();
            invoice.id = DateTime.Now.ToString("yyMMddhhmmss");
            invoice.NameCustomer = name;
            invoice.EmailCustomer = email;
            invoice.PhoneCustomer = phone;
            invoice.AddressCustomer = address;
            invoice.Shippingfee = ViewBag.shipping;
            invoice.Total = ViewBag.total;
            invoice.delivered = false;
            invoice.status = false;
            invoice.DateOrder = DateTime.Now;
            db.Invoices.Add(invoice);
            db.SaveChanges();
            foreach(var i in ListCart)
            {
                InvoiceDetail detail = new InvoiceDetail();
                detail.idInvoice = invoice.id;
                detail.idProduct = i.idProduct;
                detail.price = i.price;
                detail.quantity=i.quantity;
                detail.total= i.total;
                db.InvoiceDetails.Add(detail);
                db.SaveChanges();
            }
           

            return View();
        }
        public ActionResult Cart()
        {
            List<Cart> ListCart = GetCarts();
            ViewBag.subTotal = ListCart.Sum(i => i.total);
            ViewBag.shipping = ListCart.Sum(i => i.quantity) * 5000;
            ViewBag.total = ViewBag.subTotal + ViewBag.shipping;
            return PartialView(ListCart.ToList());
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
        public ActionResult AddCart(int idProduct,int? quantity)
        {
            List<Cart> ListCart=GetCarts();
            Cart p=ListCart.Find (n=>n.idProduct==idProduct);
        
          
            
            if(p==null)
            {
                p=new Cart(idProduct);
                if (quantity != null)
                {
                    p.quantity = (int)quantity;
                }
                ListCart.Add(p);
                return Redirect("~/gio-hang");
            }
            else
            {
                if (quantity != null)
                {
                    p.quantity = (int)quantity;
                }
                p.quantity++;
               
                return Redirect("~/gio-hang");
            }
        }
        public ActionResult RemoveCart(int idProduct)
        {
            List<Cart> ListCart = GetCarts();
            var p = ListCart.FirstOrDefault(i => i.idProduct == idProduct);
            ListCart.Remove(p);
            ViewBag.subTotal = ListCart.Sum(i => i.total);
            ViewBag.shipping = ListCart.Sum(i => i.quantity) * 5000;
            ViewBag.total = ViewBag.subTotal + ViewBag.shipping;
            return PartialView("Cart",ListCart.ToList());
        }
         public ActionResult ChangeQuantity(int idProduct,int quantity)
        {
            List<Cart> ListCart = GetCarts();
            ListCart.FirstOrDefault(i => i.idProduct == idProduct).quantity=quantity;
           
            ViewBag.subTotal = ListCart.Sum(i => i.total);
            ViewBag.shipping = ListCart.Sum(i => i.quantity) * 5000;
            ViewBag.total = ViewBag.subTotal + ViewBag.shipping;
            return PartialView("Cart", ListCart.ToList());
        }


    }
}