using ElbruzWebPj.Models.CrudRepository.Interfaces;
using ElbruzWebPj.Models.MVVM;
using Microsoft.EntityFrameworkCore;

namespace ElbruzWebPj.Models.CrudRepository
{
    public class SupplierRepository : ICrudRepository<Supplier>
    {
        public readonly AppDbContext _context;

        public SupplierRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Supplier supplier)
        {
            try
            {
                _context.Add(supplier);
                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception)
            {

                return false;

            }
        }


        public async Task<List<Supplier>> Read(string Main_Or_All)
        {
            List<Supplier> suppliers = await _context.Suppliers.ToListAsync();
            return suppliers;
        }

        public async Task<bool> Update(Supplier supplier)
        {
            try
            {
                _context.Update(supplier);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        public async Task Delete(Supplier supplier)
        {
            try
            {
                if (supplier.Active)
                {
                    supplier.Active = false;
                }
                else
                {
                    supplier.Active = true;
                }
                _context.Update(supplier);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new ArgumentException(" Active değeri gelmedi !!! ");

            }
        }



        public async Task<bool> CreateControl(Supplier supplier)
        {
            var SupplierControl = await _context.Suppliers.FirstOrDefaultAsync(s => s.BrandName == supplier.BrandName);
            if (SupplierControl == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public async Task<Supplier> GetDetails(int id)
        {

            Supplier supplier = await _context.Suppliers.AsNoTracking().FirstOrDefaultAsync(s => s.SupplierID == id);

            return supplier;
        }







    }
}
