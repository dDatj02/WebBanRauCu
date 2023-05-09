using ProjectCuoiki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectCuoiki.Controllers
{
    public class OrderHistoryController : Controller
    {
        ProjectCuoiKiEntities db = new ProjectCuoiKiEntities();
        // GET: OrderHistory
        public ActionResult Index(string name)
        {
            return View(db.Invoices.Where(x=>x.NameCustomer==name).ToList());
        }

        public ActionResult DetailOrder(string invoiceId) 
        {
            List<CartHistory> cartHistorty = db.InvoiceDetails.Where(x => x.idInvoice == invoiceId).Select(x => new CartHistory
            {
                idProduct = db.products.Where(p => p.id == x.idProduct).FirstOrDefault().id,
                nameProduct = db.products.Where(p => p.id == x.idProduct).FirstOrDefault().name,
                imgProduct = db.products.Where(p => p.id == x.idProduct).FirstOrDefault().imgname,
                price = (decimal)x.price,
                quantity = (int)x.quantity,
            }).ToList();

            ViewBag.subTotal = cartHistorty.Sum(i => i.total);
            ViewBag.shipping = cartHistorty.Sum(i => i.quantity) * 5000;
            ViewBag.total = ViewBag.subTotal + ViewBag.shipping;

            return View(cartHistorty);
        }
    }
}