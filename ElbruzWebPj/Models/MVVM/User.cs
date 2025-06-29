using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElbruzWebPj.Models.MVVM
{
    public class User
    {






        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }



        [StringLength(100, ErrorMessage = "En fazla 100 karakter")]
        [Required(ErrorMessage = "Ad Soyad zorunlu alan")]
        [DisplayName("Ad Soyad")]
        public string? NameSurname { get; set; }






        [EmailAddress(ErrorMessage = "Doğru email girmediniz")]
        [Required(ErrorMessage = "Email zorunlu alan")]
        [StringLength(100, ErrorMessage = "En fazla 100 karakter")]
        [DataType(DataType.EmailAddress)]

        public string? Email { get; set; }







        [StringLength(100, ErrorMessage = "En fazla 100 karakter")]
        [Required(ErrorMessage = "Şifre zorunlu alan")]
        [DisplayName("Şifre")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }








        [DisplayName("Telefon")]
        [StringLength(10, ErrorMessage = "En fazla 10 karakter")]
        [DataType(DataType.PhoneNumber)]
        public string? Telephone { get; set; }





        [DisplayName("Fatura Adresi")]
        public string? InvoicesAddres { get; set; }


        public bool IsAdmin { get; set; }



        [DisplayName("Aktif/Pasif")]
        [BindNever]
        public bool Active { get; set; }

    }
}
