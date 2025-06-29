using System.Linq.Expressions;
using ElbruzWebPj.Models.DTOs;
using ElbruzWebPj.Models.MVVM;
using ElbruzWebPj.Models.ViewModel;

namespace ElbruzWebPj.Models.Mapper
{
    public class Cls_VM_Mapping
    {


        public Expression<Func<Product, VM_FetchProduct>> VM_ProductMapping => p => new VM_FetchProduct
        {
            Product_ID = p.ProductID,
            Product_Name = p.ProductName,
            Product_PhotoPath = p.PhotoPath,
            Product_Price = p.UnitPrice,
            Product_IsActive = p.Active
        };





        public Expression<Func<Category, DTO_Category>> DTO_Category => c => new DTO_Category
        {
            CategoryID = c.CategoryID,
            CategoryName = c.CategoryName,
            ParentID = c.ParentID,
        };

        public Expression<Func<Supplier, DTO_Supplier>>  DTO_Supplier => s => new DTO_Supplier
        {

            BrandName = s.BrandName,
            SupplierID = s.SupplierID

        };



    }
}
