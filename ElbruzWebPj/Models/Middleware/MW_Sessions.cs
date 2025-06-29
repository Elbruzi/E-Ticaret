namespace ElbruzWebPj.Models.Middleware
{
    public class MW_Sessions
    {

        private readonly RequestDelegate requestDelegate;

        public MW_Sessions(RequestDelegate _requestDelegate)
        {
            requestDelegate = _requestDelegate;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value;

            // Yalnızca /Admin veya /Admin/* ile başlayan isteklere uygula
            if (path.StartsWith("/Admin", StringComparison.OrdinalIgnoreCase))
            {
                if (context.Session.GetString("Admin") == null)
                {
                    context.Response.Redirect("/Home/Index");
                    return;
                }
            }

            await requestDelegate(context);
        }




    }
}
