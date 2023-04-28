using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectCuoiki.Models;
namespace ProjectCuoiki.Models
{
    public class Cart
    {
        ProjectCuoiKiEntities db = new ProjectCuoiKiEntities();
        public int idProduct { get; set; }
        public string nameProduct { get; set; }
        public string imgProduct { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
        public decimal total { get { return (price * quantity); } }

        public Cart(int idProduct)
        {

            this.idProduct = idProduct;
            product p=db.products.Single(i=>i.id == idProduct);
            this.nameProduct = p.name;
            this.imgProduct = p.imgname;
            this.price = (decimal)p.price;
            this.quantity = 1;
        }
    }
}