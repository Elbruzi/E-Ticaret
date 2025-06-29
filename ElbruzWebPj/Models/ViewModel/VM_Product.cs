using ElbruzWebPj.Models.MVVM;

namespace ElbruzWebPj.Models.ViewModel
{
    public class VM_Product
    {

        public Product? Product { get; set; }

        public string CategoryName { get; set; }               
        public List<Category>? Categories { get; set; }

        public string SupplierName { get; set; }    
        public List<Supplier>? Suppliers { get; set; }

        public List<ProductColor>? colors { get; set; }

    }
}
