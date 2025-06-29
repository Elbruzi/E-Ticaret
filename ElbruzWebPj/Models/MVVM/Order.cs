using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElbruzWebPj.Models.MVVM
{
    public class Order
    {





        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }



        public DateTime OrderDate { get; set; }



        [StringLength(30)]
        public string? OrderGroupGUID { get; set; } //sipariş no = 2025135951 replace



        public int UserID { get; set; } //inner join Users



        public int ProductID { get; set; } //inner join Products



        public int Quantity { get; set; }





    }
}
