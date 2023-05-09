using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectCuoiki.Models
{
    public class CartHistory
    {
        public int idProduct { get; set; }
        public string nameProduct { get; set; }
        public string imgProduct { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
        public decimal total { get { return (price * quantity); } }
    }
}