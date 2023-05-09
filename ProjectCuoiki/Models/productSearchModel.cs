using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectCuoiki.Models
{
    public class productSearchModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public string meta { get; set; }
        public string typeProductMeta { get; set; }
        public Nullable<bool> hide { get; set; }
        public Nullable<System.DateTime> datebegin { get; set; }
        public Nullable<int> order { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<int> idType { get; set; }
        public Nullable<int> quantity { get; set; }
        public string imgname { get; set; }
    }
}