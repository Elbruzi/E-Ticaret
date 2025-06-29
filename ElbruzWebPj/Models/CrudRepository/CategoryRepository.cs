using ElbruzWebPj.Models.CrudRepository.Interfaces;
using ElbruzWebPj.Models.MVVM;
using Microsoft.EntityFrameworkCore;


namespace ElbruzWebPj.Models.CrudRepositery
{
    public class CategoryRepository : ICrudRepository<Category>
    {

        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<bool> Create(Category category)
        {
            try
            {
                if (category.ParentID == null)
                {
                    category.ParentID = 0;
                }
                _context.Add(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        public async Task<List<Category>> Read(string Main_Or_All)
        {
            if (Main_Or_All == "Main")
            {

                List<Category> list = await _context.Categories.Where(c => c.ParentID == 0).ToListAsync();

                return list;

            }
            if (Main_Or_All == "All")
            {
                List<Category> list = await _context.Categories.ToListAsync();
                return list;
            }

            throw new ArgumentException("Kategori Listeleme Sorgusu yapılamadı , Main ya da All parametresi bekleniyor !!! ");

        }



        public async Task<bool> Update(Category category)
        {

            try
            {
                if (category.ParentID == null)
                {
                    category.ParentID = 0;

                }

                _context.Update(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        public async Task Delete(Category category)
        {
            try
            {
                if (category.Active)
                {
                    category.Active = false;
                }
                else
                {
                    category.Active = true;
                }

                _context.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ArgumentException(" Active değeri gelmedi !!! ");
            }
        }




        public async Task<bool> CreateControl(Category category)
        {
            var ControlName = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == category.CategoryName);


            if (ControlName == null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }





        public async Task<Category> GetDetails(int id)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(s => s.CategoryID == id);

            return category;
        }









    }
}
