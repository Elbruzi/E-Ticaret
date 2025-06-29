using ElbruzWebPj.Models.MVVM;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElbruzWebPj.Models.ViewModel
{
    public class VM_FetchProduct
    {

        public int Product_ID { get; set; }

        public string Product_Name { get; set; }

        public string  Product_PhotoPath { get; set; }

        public decimal Product_Price { get; set; }

        public bool Product_IsActive { get; set; }

    }
}
