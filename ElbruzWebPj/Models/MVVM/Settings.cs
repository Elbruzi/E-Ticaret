using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElbruzWebPj.Models.MVVM
{
    public class Settings
    {






        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SettingID { get; set; }


        [DisplayName("Telefon")]
        public string? Telephone { get; set; }



        [EmailAddress(ErrorMessage = "Doğru email girmediniz")]
        [Required(ErrorMessage = "Email zorunlu alan")]
        [StringLength(100, ErrorMessage = "En fazla 100 karakter")]
        public string? Email { get; set; }



        [DisplayName("Adres")]
        public string? Address { get; set; }



        [DisplayName("Ana Sayfa Ürün Sayısı")]
        public int MainPageCount { get; set; }



        [DisplayName("Alt Sayfa Ürün Sayısı")]
        public int SubpageCount { get; set; }





    }
}
