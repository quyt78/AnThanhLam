using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnThanhLam.Web.Models
{
    public class CateViewModel
    {
        public int ID { get; set; }
        public int parentID { get; set; }
        public string Alias { get; set; }

        public IEnumerable<CateViewModel> listChild { get; set; }

        public int level { get; set; }
    }
}