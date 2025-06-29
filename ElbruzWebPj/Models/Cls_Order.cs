using ElbruzWebPj.Models.MVVM;

namespace ElbruzWebPj.Models
{
    public class Cls_Order
    {

        private readonly AppDbContext _context;

        public Cls_Order(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }



        public int ProductID { get; set; }

        public int Quantity { get; set; } //adet

        public string? MyCart { get; set; }

        public decimal UnitPrice { get; set; }

        public string? ProductName { get; set; }

        public int Kdv { get; set; }

        public string? PhotoPath { get; set; }



        //Sepete ekle



        //Add to my cart


        public bool AddToMyCart(string id)
        {
            bool exists = false; //Bu metod bittiğinde gala false ise ürün sepete eklendi

            if (MyCart == "") //sepete ilk defa ürün ekleyeceğiz 
            {
                //& yok 10 = 1  
                MyCart = id + "=" + Quantity;
            }
            else //sepete daha önceden eklenmiş ürün(ler) var
            {
                string[] MyCartArray = MyCart.Split('&');
                for (int i = 0; i < MyCartArray.Length; i++)
                {
                    //1.dönüş 10=1
                    //2.dönüş 20=1
                    //3.dönüş 30=1
                    string[] MyCartArrayLoop = MyCartArray[i].Split("=");
                    string ProductID = MyCartArrayLoop[0];
                    if (ProductID == id)
                    {
                        //aynı ürünü sepete eklemeye çalışıyor
                        exists = true;
                    }
                }
                if (exists == false) //ürün daha önce speete eklenmemiş
                {
                    //10=1&20=1&30=1&40=1
                    MyCart = MyCart + "&" + id.ToString() + "=1";
                }
            }
            return exists;
        }










        public List<Cls_Order> SelectMyCart()
        {


            List<Cls_Order> list = new List<Cls_Order>();
            string[] MyCartArray = MyCart.Split('&');

            if (MyCart != "")
            {


                for (int i = 0; i < MyCartArray.Length; i++)
                {
                    string[] MyCartArrayLoop = MyCartArray[i].Split('=');
                    // MyCartArrayLoop[0] = ProductID
                    // MyCartArrayLoop[1] = Quantity

                    int ProductID = Convert.ToInt32(MyCartArrayLoop[0]);

                    Product? prd = _context.Products.FirstOrDefault(p => p.ProductID == ProductID);
                    // prd içinde veritabanındaki verileri kayıtları var, bunları propertylere yazdırıyorum

                    Cls_Order ord = new Cls_Order(_context);
                    ord.ProductID = prd.ProductID;
                    ord.Quantity = Convert.ToInt32(MyCartArrayLoop[1]);
                    ord.UnitPrice = prd.UnitPrice;
                    ord.ProductName = prd.ProductName;
                    ord.PhotoPath = prd.PhotoPath;
                    ord.Kdv = prd.KDV;

                    list.Add(ord);
                }
            }
            return list;


        }




        public void DeleteFromMyCart(string id)
        {
            string[] MyCartArray = MyCart.Split('&'); //ürünler birbirinden ayrı
            string NewMyCart = "";
            int count = 1;

            for (int i = 0; i < MyCartArray.Length; i++)
            {
                // ProductID ile adet ayrıldı
                string[] MyCartArrayLoop = MyCartArray[i].Split('=');
                string ProductID = MyCartArrayLoop[0];
                if (ProductID != id)
                {
                    if (count == 1)
                    {

                        // & yok
                        // yeni sepetin icine ilk ürünü ekliyorum , & ampersand yok
                        NewMyCart = MyCartArrayLoop[0] + "=" + MyCartArrayLoop[1];
                        count++;

                    }
                    else
                    {

                        // & var
                        // yeni sepetin icindedaha önce silinmeyecek olan ürün(ler) var , & ampersand ProductID nin önüne ekliyorum
                        NewMyCart += "&" + MyCartArrayLoop[0] + "=" + MyCartArrayLoop[1];

                    }

                }

            }
            MyCart = NewMyCart;
        }











        public string OrderCreate(string Email)
        {
            List<Cls_Order> sipList = SelectMyCart();
            string OrderGroupGUID = DateTime.Now.ToString().Replace(":", "").Replace(".", "").Replace("/", "").Replace(" ", "");

            DateTime OrderDate = DateTime.Now;

            foreach (var item in sipList)
            {
                Order order = new Order();
                order.OrderDate = OrderDate;
                order.OrderGroupGUID = OrderGroupGUID;
                order.UserID = _context.Users.FirstOrDefault(u => u.Email == Email).UserID;
                order.ProductID = item.ProductID;
                order.Quantity = item.Quantity;
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            return OrderGroupGUID;
        }







        
        public List<Vw_MyOrders> SelectMyOrders(string Email)
        {
            int UserID = _context.Users.FirstOrDefault(u => u.Email == Email).UserID;
            List<Vw_MyOrders> myOrders = _context.Vw_MyOrders.Where(o => o.UserID == UserID).ToList();
            return myOrders;
        }
        







    }
}
