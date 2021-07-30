using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AnThanhLam.Web.Models
{
    public class SizeViewModel
    {
        public int ID { set; get; }
        public string Name { set; get; }

        public int? DisplayOrder { set; get; }

        public string Description { get; set; }

        public virtual IEnumerable<ProductViewModel> Products { get; set; }

        public DateTime? CreatedDate { set; get; }


        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }


        public string UpdatedBy { set; get; }


        public string MetaKeyword { set; get; }

        public string MetaDescription { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập trạng thái")]
        public bool Status { set; get; }
    }
}