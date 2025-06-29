using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElbruzWebPj.Models.MVVM
{
    public class ProductColor
    {


        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ColorID { get; set; }


        [Required(ErrorMessage = "Renk adı zorunlu alan")]
        [StringLength(100, ErrorMessage = "Renk adı en fazla 100 karakter olabilir.")]
        [DisplayName("Renk")]
        public string Color { get; set; }

    }
}
