using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnThanhLam.Web.Models
{
    public class ProductSizeViewModel
    {
        public int ProductID { get; set; }
      
        public string SizeID { get; set; }

       
        public virtual ProductViewModel Product { get; set; }

        public virtual SizeViewModel Size { get; set; }
    }
}