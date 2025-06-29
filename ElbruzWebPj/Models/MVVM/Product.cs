using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElbruzWebPj.Models.MVVM
{
    public class Product
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID")]
        public int ProductID { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Ürün adı zorunlu alan")]
        [DisplayName("Ürün Adı")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Fiyat zorunlu alan")]
        [DisplayName("Fiyat")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Kategori Adı zorunlu alan")]
        [DisplayName("Kategori")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Marka Adı zorunlu alan")]
        [DisplayName("Marka")]
        public int SupplierID { get; set; }

        [Required(ErrorMessage = "Stok zorunlu alan")]
        [DisplayName("Stok")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "KDV zorunlu alan")]
        public int KDV { get; set; }

        [StringLength(500, ErrorMessage = "Açıklama 500 karakteri geçemez")]
        [DisplayName("Açıklama")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Renk adı zorunlu alan")]
        [StringLength(100, ErrorMessage = "Renk adı en fazla 100 karakter olabilir.")]
        [DisplayName("Renk")]
        public string Color { get; set; }


        [StringLength(50)]
        [DisplayName("Beden")]
        public string? Size { get; set; }

        [DisplayName("Resim Seç")]
        [DataType(DataType.Upload)]
        public string? PhotoPath { get; set; }


        [DisplayName("Aktif")]
        public bool Active { get; set; }



        [DisplayName("İndirim")]
        public int Discount { get; set; }
    }
}
