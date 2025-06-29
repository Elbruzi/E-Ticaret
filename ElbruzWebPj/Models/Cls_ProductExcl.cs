using System.ComponentModel;
using System.Diagnostics;
using System.Security.AccessControl;
using ElbruzWebPj.Models.MVVM;
using ElbruzWebPj.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace ElbruzWebPj.Models
{

    public class Cls_ProductExcl
    {


        readonly AppDbContext _context;

        public Cls_ProductExcl(AppDbContext context)
        {
            _context = context;
        }



        public async Task<string> NameFinder(int id, NameType type)
        {

            string Name;

            if (type == NameType.Category)
            {

               Name = await _context.Categories.Where(c => c.CategoryID == id).Select(c => c.CategoryName).FirstOrDefaultAsync();

            }
            else if (type == NameType.Supplier)
            {

                 Name = await _context.Suppliers.Where(s => s.SupplierID == id).Select(s => s.BrandName).FirstOrDefaultAsync();

            }
            else
            {

                throw new Exception("Gelen id de bir sıkıtnı var");

            }

            return Name ?? throw new Exception("id gelmedi");
             
        }


        public async Task<string> CodeFilterer(int id)
        {
            string PhotoPath_Finder = await _context.Products.Where(p => p.ProductID == id).Select(p => p.PhotoPath).FirstOrDefaultAsync();

            return PhotoPath_Finder;

        }















    }
}
