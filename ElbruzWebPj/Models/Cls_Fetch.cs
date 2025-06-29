using System.Collections.Generic;
using ElbruzWebPj.Models.CrudRepositery;
using ElbruzWebPj.Models.CrudRepository;
using ElbruzWebPj.Models.Mapper;
using ElbruzWebPj.Models.MVVM;
using ElbruzWebPj.Models.ViewModel;

namespace ElbruzWebPj.Models
{
    public class Cls_Fetch
    {
        readonly private AppDbContext _context;

        readonly private Cls_VM_Mapping _vm_mapping;


        public Cls_Fetch(AppDbContext context, Cls_VM_Mapping _Cls_VM_Mapping)
        {
            _context = context;

            _vm_mapping = _Cls_VM_Mapping;
        }



        public const int PageSize = 6;



        public IQueryable<VM_FetchProduct> Fetcher(int PageNumber , int? Category_Supplier_ID , FetchType type)
        {

            IQueryable<VM_FetchProduct> vm_FetchProducts = null;

            switch (type)
            {

                case FetchType.Index: 
                    vm_FetchProducts = _context.Products.Where( p => p.Active == true).OrderByDescending(p => p.ProductID ).Select(_vm_mapping.VM_ProductMapping);
                    break;

                case FetchType.Category:
                    vm_FetchProducts = _context.Products.Where(p => p.CategoryID == Category_Supplier_ID && p.Active == true).OrderByDescending(p => p.ProductID).Select(_vm_mapping.VM_ProductMapping);
                    break;

                case FetchType.Supplier:
                    vm_FetchProducts = _context.Products.Where(p => p.SupplierID == Category_Supplier_ID && p.Active == true).OrderByDescending(p => p.ProductID).Select(_vm_mapping.VM_ProductMapping);
                    break;

            }

                vm_FetchProducts = vm_FetchProducts.Skip(PageSize * PageNumber).Take(PageSize);

            return vm_FetchProducts;

        }


        public IQueryable<VM_FetchProduct> Search(string Text)
        {

            IQueryable<VM_FetchProduct> vm_FetchProducts = null;

            if (String.IsNullOrWhiteSpace(Text))
            {
                return vm_FetchProducts;
            }
            else
            {
                vm_FetchProducts = _context.Products.Where(p => p.ProductName.Contains(Text) && p.Active == true).Select(_vm_mapping.VM_ProductMapping).Take(6);
            }

            return vm_FetchProducts;

        }













    }
}
