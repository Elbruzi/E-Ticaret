using ElbruzWebPj.Models.CrudRepository.Interfaces;
using ElbruzWebPj.Models.MVVM;
using Microsoft.EntityFrameworkCore;

namespace ElbruzWebPj.Models.CrudRepository
{
    public class ColorRepository : ICrudRepository<ProductColor>
    {
        private readonly AppDbContext _context;

        public ColorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(ProductColor color)
        {
            try
            {
                _context.Add(color);
                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception)
            {

                return false;

            }
        }


        public async Task<List<ProductColor>> Read(string Main_Or_All)
        {
            List<ProductColor> color = await _context.ProductColors.ToListAsync();
            return color;
        }

        public async Task<bool> Update(ProductColor color)
        {
            try
            {
                _context.Update(color);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        public async Task Delete(ProductColor color)
        {
            throw new ArgumentException("Color kısmında bu Metod çalışmaz !!! ");
        }



        public async Task<bool> CreateControl(ProductColor color)
        {
            var ProductColorControl = await _context.ProductColors.FirstOrDefaultAsync(s => s.Color == color.Color);
            if (ProductColorControl == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<ProductColor> GetDetails(int id)
        {
            ProductColor? color = await _context.ProductColors.FirstOrDefaultAsync(s => s.ColorID == id);

            if (color == null)
            {
            throw new ArgumentException("Color kısmında bu Metod çalışmaz !!! ");

            }
            return color;
        }























    }

}
