using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElbruzWebPj.Models.MVVM
{
    public class Status
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID")]

        public int StatusID { get; set; }



        [StringLength(100)]
        [Required(ErrorMessage = "Statü adı zorunlu alan")]
        [DisplayName("Statü Adı")]
        //regular expression
        public string? StatusName { get; set; } = string.Empty;



        [DisplayName("Aktif/Pasif")]
        public bool Active { get; set; }




    }
}
