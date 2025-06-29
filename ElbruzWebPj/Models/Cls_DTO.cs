using ElbruzWebPj.Models.DTOs;
using ElbruzWebPj.Models.Mapper;
using ElbruzWebPj.Models.MVVM;
using ElbruzWebPj.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace ElbruzWebPj.Models
{
    public class Cls_DTO
    {

        private readonly AppDbContext _context;

        private readonly Cls_VM_Mapping _cls_VM_Mapping;

        public Cls_DTO(AppDbContext context, Cls_VM_Mapping cls_VM_Mapping)
        {
            _context = context;

            _cls_VM_Mapping = cls_VM_Mapping;

        }



        public async Task<List<DTO_Category>> DTO_Fill_Category()
        {
            return await _context.Categories
                .Where(c => c.Active == true)
                .Select(_cls_VM_Mapping.DTO_Category)
                .ToListAsync();
        }


        public async Task<List<DTO_Supplier>> DTO_Fill_Supplier()
        {
            return await _context.Suppliers
                .Where(c => c.Active == true)
                .Select(_cls_VM_Mapping.DTO_Supplier)
                .ToListAsync();
        }


        public async Task<VM_HeaderDropDown> DTO_Filler()
        {
            var categories = await DTO_Fill_Category();
            var suppliers = await DTO_Fill_Supplier();

            return new VM_HeaderDropDown
            {
                Categories = categories,
                Suppliers = suppliers
            };
        }




    }
}
