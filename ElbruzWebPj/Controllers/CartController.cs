using ElbruzWebPj.Models;
using ElbruzWebPj.Models.MVVM;
using Microsoft.AspNetCore.Mvc;

namespace ElbruzWebPj.Controllers
{
    public class CartController : Controller
    {

        readonly private AppDbContext _context;

        readonly private Cls_Order cls_Order;

        public CartController(AppDbContext context,Cls_Order cls_order)
        {
            _context = context;

            cls_Order = cls_order;

        }

        [Route("home/cartprocess/{id}")]
        public IActionResult CartProcces(int id)
        {
            cls_Order.Quantity = 1;

            //sepetim isminde daha önceden yaratılmış çerezim (cookie) var mı ?
            var cookieOptions = new CookieOptions();

            //tarayıcıda sepetim isminde çerez var mı ? , varsa bilgilerini cookie denen değişken içine koyacak
            var cookie = Request.Cookies["sepetim"];
            if (cookie == null)
            {
                //sepetim boş
                cookieOptions.Expires = DateTime.Now.AddDays(1); //1 günlük çerez
                cookieOptions.Path = "/";
                cls_Order.MyCart = "";
                cls_Order.AddToMyCart(id.ToString()); //Sepete Ekle
                Response.Cookies.Append("sepetim", cls_Order.MyCart, cookieOptions); //property deki sepet bilgilerini tarayıcıya gönderdim
                TempData["Message"] = "Ürün sepetinize eklendi";
            }
            else
            {
                cls_Order.MyCart = cookie;

                if (cls_Order.AddToMyCart(id.ToString()) == false)
                {
                    HttpContext.Response.Cookies.Append("sepetim", cls_Order.MyCart, cookieOptions);
                    cookieOptions.Expires = DateTime.Now.AddDays(1);


                    TempData["Message"] = "Ürün sepetinize eklendi";
                }
                else
                {
                    TempData["Message"] = "Bu ürün zaten sepetinizde var ";

                }
            }

            string refererUrl = Request.Headers["Referer"].ToString();
            string url = "";
            Uri refererUri = new Uri(refererUrl, UriKind.Absolute);
            url = refererUri.AbsolutePath;


            if (url.Contains("DpProducts") || refererUrl.Contains("http://localhost:7074"))
            {
                return RedirectToAction("index");
            }
            return Redirect(url);
        }


        public IActionResult Cart()
        {

            var cookie = Request.Cookies["sepetim"];

            if (HttpContext.Request.Query["ProductID"].ToString() == "")
            {
                if (cookie == null)
                {
                    cls_Order.MyCart = "";
                    ViewBag.Sepetim = cls_Order.SelectMyCart() ?? new List<Cls_Order>();
                }
                else
                {
                    cls_Order.MyCart = cookie;
                    ViewBag.Sepetim = cls_Order.SelectMyCart();
                }

            }
            else
            {
                //ürünü sepetten sil butonu tıklanınca
                string? ProductID = HttpContext.Request.Query["ProductID"];
                cls_Order.MyCart = cookie;
                cls_Order.DeleteFromMyCart(ProductID);
                var cookieOptions = new CookieOptions();
                Response.Cookies.Append("sepetim", cls_Order.MyCart, cookieOptions);
                cookieOptions.Expires = DateTime.Now.AddDays(1);
                TempData["Message"] = "Ürün sepetten silindi";
                ViewBag.Sepetim = cls_Order.SelectMyCart();



            }
            return View();

        }




    }
}
