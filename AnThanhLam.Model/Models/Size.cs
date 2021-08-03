using AnThanhLam.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnThanhLam.Model.Models
{
    [Table("Sizes")]
    public class Size : Auditable
    {
        [Key]
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        public int? DisplayOrder { set; get; }

        [MaxLength(500)]
        public string Description { get; set; }

        public string Type { get; set; }

        public virtual IEnumerable<Product> Products { get; set; }
    }
}
