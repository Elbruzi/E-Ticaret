using ElbruzWebPj.Models;
using ElbruzWebPj.Models.CrudRepository.Interfaces;
using ElbruzWebPj.Models.Mapper;
using ElbruzWebPj.Models.MVVM;
using ElbruzWebPj.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ElbruzWebPj.Controllers
{
    public class HomeController : Controller
    {

        private readonly Cls_User _cls_User;
        private readonly Cls_Fetch _cls_Fetch;
        private readonly Cls_VM_Mapping _cls_VM_Mapping;
        private readonly ICrudRepository<Product> _productRepository;

        public HomeController(Cls_User cls_User , Cls_Fetch cls_Fetch, Cls_VM_Mapping cls_VM_Mapping, ICrudRepository<Product> productRepository)
        {

            _cls_User = cls_User;

            _cls_Fetch = cls_Fetch;

            _cls_VM_Mapping = cls_VM_Mapping;

            _productRepository = productRepository;

        }

        public IActionResult Index()
        {

            var model = _cls_Fetch.Fetcher(0 , null , FetchType.Index).ToList();

            return View(model);

        }

        public IActionResult Product(FetchType type, int id)
        {
            ViewBag.Category_Supplier_ID = id;
            ViewBag.FetchType = type.ToString();

            var model = _cls_Fetch.Fetcher(0, id, type).ToList();

            return View(model);

        }

        public IActionResult LoadMore(int pageno, int? Category_Supplier_ID, FetchType type)
        {

            var model = _cls_Fetch.Fetcher(pageno , Category_Supplier_ID, type).ToList();

            return PartialView("_ProductPartial", model); 

        }

        public IActionResult Search(string Text)
        {
            var model = _cls_Fetch.Search(Text).ToList();

            return PartialView("PartialSearch", model);
        }

        public async Task<IActionResult> Product_Page(int id)
        {

            Product product = await _productRepository.GetDetails(id);

            return View(product);

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Order()
        {
            return View();
        }
    }
}
