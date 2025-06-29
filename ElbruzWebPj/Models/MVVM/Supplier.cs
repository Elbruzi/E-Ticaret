using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElbruzWebPj.Models.MVVM
{
    public class Supplier
    {





        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID")]
        public int SupplierID { get; set; }



        [StringLength(100)]
        [Required]
        [DisplayName("Marka Adı")]
        //regular expression
        public string? BrandName { get; set; }



        [DisplayName("Resim Seç")]
        [Required]
        public string? PhotoPath { get; set; }



        [DisplayName("Aktif/Pasif")]
        public bool Active { get; set; }

        public static implicit operator Supplier?(Product? v)
        {
            throw new NotImplementedException();
        }
    }
}
