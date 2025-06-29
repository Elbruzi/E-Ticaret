using ElbruzWebPj.Models.MVVM;

namespace ElbruzWebPj.Models
{
    public class Cls_CategoryExcl
    {
        private readonly AppDbContext _context;

        public Cls_CategoryExcl(AppDbContext context)
        {
            _context = context;
        }

        public Category CategoryCleaner(Category category)
        {

            if (category.CategoryID == category.ParentID)
            {
                category.ParentID = 0;

                return category;
            }
            else
            {
                return category;
            }

        }



        public void Gatherer(Category category)
        {
            List<Category> UnBinder = _context.Categories.Where(c => c.ParentID == category.CategoryID).ToList();


            foreach (var item in UnBinder)
            {
                item.ParentID = 5;


                _context.Update(item);
            }
            _context.SaveChanges();
        }



        public void Activater(Category category)
        {

            if (category.Active == true)
            {
                category.Active = false;

            }
            else
            {
                category.Active = true;

            }
            _context.Update(category);
            _context.SaveChanges();
        }


     




    }
}
