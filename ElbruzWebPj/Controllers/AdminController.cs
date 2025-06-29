using System.CodeDom;
using System.Net.WebSockets;
using ElbruzWebPj.Models;
using ElbruzWebPj.Models.CrudRepositery;
using ElbruzWebPj.Models.CrudRepository;
using ElbruzWebPj.Models.CrudRepository.Interfaces;
using ElbruzWebPj.Models.MVVM;
using ElbruzWebPj.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;


namespace ElbruzWebPj.Controllers
{
    public class AdminController : Controller
    {


        private readonly ICrudRepository<Category> _categoryRepository;
        private readonly ICrudRepository<Supplier> _supplierRepository;
        private readonly ICrudRepository<ProductColor> _colorRepository;
        private readonly ICrudRepository<Product> _productRepository;
        private readonly Cls_CategoryExcl _cls_CategoryExcl;
        private readonly Cls_ProductExcl _cls_productExcl;

        public AdminController(ICrudRepository<Category> categoryRepository, ICrudRepository<Supplier> supplierRepository, ICrudRepository<ProductColor> colorRepository, ICrudRepository<Product> productRepository, Cls_ProductExcl cls_ProductExcl , Cls_CategoryExcl cls_CategoryExcl)
        {
            _categoryRepository = categoryRepository;
            _cls_CategoryExcl = cls_CategoryExcl;
            _supplierRepository = supplierRepository;
            _colorRepository = colorRepository;
            _productRepository = productRepository;
            _cls_productExcl = cls_ProductExcl;
        }





        public IActionResult Index()
        {
            return View();
        }





        public async Task<IActionResult> Categories()
        {

            List<Category> categories = await _categoryRepository.Read("All");
            return View(categories);

        }






        [HttpPost]
        public async Task<IActionResult> Categories(int id)
        {

            Category category = await _categoryRepository.GetDetails(id);

            await _categoryRepository.Delete(category);

            return Ok();

        }





        [HttpGet]  
        public async Task<IActionResult> CategoryCreate()
        {

            var vm_Category = new VM_Category
            {
                SingleCategory = new Category(),

                IECategory = await _categoryRepository.Read("All")
            };

            return View(vm_Category);

        }






        [HttpPost]
        public async Task<IActionResult> CategoryCreate(VM_Category model)
        {

            ModelState.Remove(nameof(VM_Category.MainCategoryFinder));

            ModelState.Remove(nameof(VM_Category.IECategory));

            if (ModelState.IsValid == true)
            {
                var category = model.SingleCategory; // SingleCategory içindeki Category'yi alıyoruz

                bool trigger = await _categoryRepository.CreateControl(category);

                if (trigger)
                {
                    bool answer = await _categoryRepository.Create(category);

                    if (answer)
                    {
                       //TempData["Message"] = Messages.CRUD_Create_Succesful;
                       TempData["Message"] = "başarılı";
                    }
                    else
                    {
                       // TempData["Message"] = Messages.CRUD_Create_Unsuccesful;
                        TempData["Message"] = "başarısız";

                    }
                }
                else
                {
                   // TempData["Message"] = Messages.CRUD_Create_NameExists;
                    TempData["Message"] = "bu isimde var ";
                }
            }
            else
            {
                TempData["Message"] = " Kayıt Yapılamadı ! ❌";

            }
            return RedirectToAction("CategoryCreate");

        }






        [HttpGet]
        public async Task<IActionResult> CategoryEdit(int id, int Finder_ParentID)
        {

            var SelectedCategory = await _categoryRepository.GetDetails(id);

            if (Finder_ParentID != 0)
            {

                string mainCategoryFinder = await _cls_productExcl.NameFinder(Finder_ParentID,NameType.Category);

                var viewModel = new VM_Category
                {

                    SingleCategory = SelectedCategory,

                    MainCategoryFinder = mainCategoryFinder,

                    IECategory = await _categoryRepository.Read("All")

                };

                return View(viewModel);

            }
            else
            {

                var mainCategoryFinder = "Ana Kategori";

                var viewModel = new VM_Category
                {

                    SingleCategory = SelectedCategory,

                    MainCategoryFinder = mainCategoryFinder,

                    IECategory = await _categoryRepository.Read("All")

                };

                return View(viewModel);

            }
        }





        [HttpPost]
        public async Task<IActionResult> CategoryEdit(VM_Category vM_Category)
        {
            ModelState.Remove(nameof(VM_Category.IECategory));

            ModelState.Remove(nameof(VM_Category.MainCategoryFinder));

            var category = new Category
            {
                CategoryID = vM_Category.SingleCategory.CategoryID,
                CategoryName = vM_Category.SingleCategory.CategoryName,
                ParentID = vM_Category.SingleCategory.ParentID,
                Active = vM_Category.SingleCategory.Active,
            };
            if (ModelState.IsValid == true)
            {
                _cls_CategoryExcl.Gatherer(category);

                category = _cls_CategoryExcl.CategoryCleaner(category);

                bool answer = await _categoryRepository.Update(category);
                if (answer)
                {
                       // TempData["Message"] = Messages.CRUD_Create_Succesful;
                    TempData["Message"] = "başarılı";

                }
                else
                {
                   // TempData["Message"] = Messages.CRUD_Create_Unsuccesful;
                        TempData["Message"] = "başarısız";
                }
            }
            else
            {
                throw new Exception("Model State Valid Değil");
            }
            return RedirectToAction("CategoryEdit");
        }






        [HttpGet]
        public async Task<IActionResult> Suppliers()
        {
            List<Supplier> suppliers = await _supplierRepository.Read("All");

            return View(suppliers);
        }





        [HttpPost]
        public async Task<IActionResult> Suppliers(int id)
        {
            Supplier supplier = await _supplierRepository.GetDetails(id);

            await _supplierRepository.Delete(supplier);

            return Ok();
        }




        [HttpGet]
        public IActionResult SupplierCreate()
        {

            return View();

        }






        [HttpPost]
        public async Task<IActionResult> SupplierCreate(Supplier supplier)
        {
            if (ModelState.IsValid == true)
            {

                bool trigger = await _supplierRepository.CreateControl(supplier);

                if (trigger)
                {
                    bool answer = await _supplierRepository.Create(supplier);

                    if (answer)
                    {
                       // TempData["Message"] = Messages.CRUD_Create_Succesful;
                        TempData["Message"] = "başarılı";

                    }
                    else
                    {
                        // TempData["Message"] = Messages.CRUD_Create_Unsuccesful;
                        TempData["Message"] = "başarısız";

                    }
                }
                else
                {
                        TempData["Message"] = "aynı isimde var";
                    // TempData["Message"] = Messages.CRUD_Create_NameExists;
                }
            }
            else
            {
                TempData["Message"] = " Kayıt Yapılamadı ! ❌";

            }
            return RedirectToAction("SupplierCreate");
        }








        [HttpGet]
        public async Task<IActionResult> SupplierEdit(int id)
        {
            var supplier = await _supplierRepository.GetDetails(id);

            return View(supplier);
        }







        [HttpPost]
        public async Task<IActionResult> SupplierEdit(Supplier supplier)
        {
            if (supplier.PhotoPath == null)
            {
                var Old_Supplier = await _supplierRepository.GetDetails(supplier.SupplierID);

                supplier.PhotoPath = Old_Supplier.PhotoPath;
            }

            bool answer = await _supplierRepository.Update(supplier);
            if (answer)
            {
               // TempData["Message"] = Messages.CRUD_Create_Succesful;
                TempData["Message"] = "başarılı";

            }
            else
            {
              // TempData["Message"] = Messages.CRUD_Create_Unsuccesful;
                TempData["Message"] = "başarısız";

            }

            return RedirectToAction("SupplierEdit");
        }







        [HttpGet]
        public async Task<IActionResult> Colors()
        {
            List<ProductColor> colors = await _colorRepository.Read("All");

            return View(colors);
        }

        [HttpGet]
        public IActionResult ColorCreate()
        {

            return View();
        }




        [HttpPost]
        public async Task<IActionResult> ColorCreate(ProductColor CreateColor)
        {

           // if (ModelState.IsValid == true)
           // {

                bool trigger = await _colorRepository.CreateControl(CreateColor);

                if (trigger)
                {
                    bool answer = await _colorRepository.Create(CreateColor);

                    if (answer)
                    {
                TempData["Message"] = "başarılı";

                    }
                    else
                    {
                TempData["Message"] = "başarısız";

                    }
                }
                else
                {

                //TempData["Message"] = Messages.CRUD_Create_NameExists;
                TempData["Message"] = "bu isimde öğe var";

            }

            return RedirectToAction("ColorCreate");
        }





        [HttpGet]
        public async Task<IActionResult> Products()
        {
            List<Product> product = await _productRepository.Read("All");

            return View(product);
        }





        public  async Task<IActionResult> ProductCreate()
        {


            var vm_Product = new VM_Product
            {

                Product = new Product(),


                Categories = await _categoryRepository.Read("All"),


                Suppliers = await _supplierRepository.Read("All"),


                colors = await _colorRepository.Read("All"),

            };

            return View(vm_Product);
        }






        [HttpPost]
        public async Task<IActionResult> ProductCreate(VM_Product model)
        {

            ModelState.Remove(nameof(VM_Product.Suppliers));
            ModelState.Remove(nameof(VM_Product.SupplierName));
            ModelState.Remove(nameof(VM_Product.Categories));
            ModelState.Remove(nameof(VM_Product.CategoryName));
            ModelState.Remove(nameof(VM_Product.colors));

            if (ModelState.IsValid)
            {

                var product = model.Product;

                bool trigger = await _productRepository.CreateControl(product);

                if (trigger)
                {

                    bool answer = await _productRepository.Create(product);

                    if (answer)
                    {
                        TempData["Message"] = "Messages.CRUD_Create_Succesful";
                    }
                    else
                    {
                        TempData["Message"] = "Messages.CRUD_Create_Unsuccesful";
                    }
                }
                else
                {
                    TempData["Message"] = product.ProductName + "Messages.CRUD_Create_NameExists";
                }

            }
            else
            {
                TempData["Message"] = " Kayıt Yapılamadı ! ❌";

            }

            return RedirectToAction("ProductCreate");

        }






        [HttpGet]
        public async Task<IActionResult> ProductEdit(int id)
        {

            Product product = await _productRepository.GetDetails(id);

            var vm_Product = new VM_Product
            {

                Product = product,

                CategoryName = await _cls_productExcl.NameFinder(product.CategoryID,NameType.Category),

                Categories = await _categoryRepository.Read("All"),


                SupplierName = await _cls_productExcl.NameFinder(product.SupplierID,NameType.Supplier),

                Suppliers = await _supplierRepository.Read("All"),


                colors = await _colorRepository.Read("All"),

            };

            return View(vm_Product);
        }





        [HttpPost]
        public async Task<IActionResult> ProductEdit(VM_Product model)
        {
            ModelState.Remove(nameof(VM_Product.Categories));
            ModelState.Remove(nameof(VM_Product.CategoryName));
            ModelState.Remove(nameof(VM_Product.Suppliers));
            ModelState.Remove(nameof(VM_Product.SupplierName));
            ModelState.Remove(nameof(VM_Product.colors));


            if (model.Product.PhotoPath == null)
            {
                model.Product.PhotoPath = await _cls_productExcl.CodeFilterer(model.Product.ProductID);
            }

            if (ModelState.IsValid == true)
            {

                bool answer =  await _productRepository.Update(model.Product);
                if (answer)
                {
                    TempData["Message"] = "Messages.CRUD_Create_Succesful";
                }
                else
                {
                    TempData["Message"] = "Messages.CRUD_Create_Succesfu";
                }
            }
            else
            {
                throw new Exception("Model State Valid Değil");
            }
            return RedirectToAction("ProductEdit");
        }




        //var routeData = HttpContext.GetRouteData(); //nereden geldiği bulursun yanlış bir değer geliyorsa
        //var query = HttpContext.Request.Query;
        //[HttpGet("Admin/ProductDetails/{id:int}")] sadece int türünde id gelecek , 
        [HttpGet("Admin/ProductDetails/{id:int}")]
        public async Task<IActionResult> ProductDetails(int id)
        {
           
            Product product = await _productRepository.GetDetails(id);

            var vm_Product = new VM_Product
            {

                Product = product,

                CategoryName = await _cls_productExcl.NameFinder(product.CategoryID, NameType.Category),

                Categories = await _categoryRepository.Read("All"),


                SupplierName = await _cls_productExcl.NameFinder(product.SupplierID, NameType.Supplier),

                Suppliers = await _supplierRepository.Read("All"),


                colors = await _colorRepository.Read("All"),

            };

            return View(vm_Product);
        }






    }
}
