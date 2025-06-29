using ElbruzWebPj.Models;
using ElbruzWebPj.Models.MVVM;
using ElbruzWebPj.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ElbruzWebPj.ViewComponents
{
    public class Headers : ViewComponent
    {

        private readonly AppDbContext _context;

        private readonly Cls_DTO _clsDto;

        public Headers(AppDbContext context, Cls_DTO clsDto)
        {

            _context = context;

            _clsDto = clsDto;

        }



        public async Task<IViewComponentResult> InvokeAsync()
        {
            VM_HeaderDropDown vm = await _clsDto.DTO_Filler();
            return View(vm);
        }


    }
}
