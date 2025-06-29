using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElbruzWebPj.Models.MVVM
{
    public class Category
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] //primary key, identity=yes
        [DisplayName("ID")]
        public int CategoryID { get; set; }



        [Required(ErrorMessage = "Kategori adı boş bırakılamaz")] //kategori adı boş bırakılamaz kodu
        [StringLength(50, ErrorMessage = "En fazla 50 karakter")] //Karakter uzunluğu 50 yi geçemez kodu
        [DisplayName("Kategori Adı")]
        public string? CategoryName { get; set; }



        [DisplayName("Üst kategori")]
        public int? ParentID { get; set; } //Üst Kategori



        [DisplayName("Aktif/Pasif")]
        public bool Active { get; set; }

    }
}
