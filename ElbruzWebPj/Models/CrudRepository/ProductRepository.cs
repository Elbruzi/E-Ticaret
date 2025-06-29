using ElbruzWebPj.Models.CrudRepository.Interfaces;
using ElbruzWebPj.Models.MVVM;
using Microsoft.EntityFrameworkCore;

namespace ElbruzWebPj.Models.CrudRepository
{
    public class ProductRepository : ICrudRepository<Product>
    {

        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<bool> Create(Product product)
        {
            try
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
          
           
        }



        public  async Task<List<Product>> Read(string Main_Or_All)
        {
           
            if (Main_Or_All == "All")
            {
                List<Product> list = await _context.Products.ToListAsync();
                return list;
            }

            throw new ArgumentException("Kategori Listeleme Sorgusu yapılamadı , Main ya da All parametresi bekleniyor !!! ");

        }



        public async Task<bool> Update(Product product)
        {

            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        public async Task Delete(Product product)
        {
            try
            {
                if (product.Active)
                {
                    product.Active = false;
                }
                else
                {
                    product.Active = true;
                }

                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ArgumentException(" Active değeri gelmedi !!! ");
            }
        }




        public async Task<bool> CreateControl(Product product)
        {
            var ControlName = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == product.ProductName);


            if (ControlName == null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }





        public async Task<Product> GetDetails(int id)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == id);

            return product;
        }


































    }
}
