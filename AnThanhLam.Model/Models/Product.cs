using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using AnThanhLam.Model.Abstract;

namespace AnThanhLam.Model.Models
{
    [Table("Products")]
    
    public class Product : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [Required]
        [MaxLength(256)]
        public string Alias { set; get; }

        [Required]
        public int CategoryID { set; get; }

        [Required]
        public int BrandID { get; set; }                        

        [MaxLength(256)]
        public string Image { set; get; }

        [Column(TypeName = "xml")]
        public string MoreImages { set; get; }

        public decimal? Price { set; get; }

        public decimal? PromotionPrice { set; get; }

        public int? Warranty { set; get; }  

        [MaxLength(500)]
        public string Description { set; get; }
        public string Content { set; get; }

        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public int? ViewCount { set; get; }

        public string Tags { set; get; }

        public string Sizes { get; set; }

        public int Quantity { set; get; }

        public decimal OriginalPrice { set; get; }

        [ForeignKey("CategoryID")]
        public virtual ProductCategory ProductCategory { set; get; }
               
        public virtual IEnumerable<ProductSize> ProductSizes { get; set; }

        [ForeignKey("BrandID")]
        public virtual Brand Brand { get; set; }

        public virtual IEnumerable<ProductTag> ProductTags { set; get; }
    }
}