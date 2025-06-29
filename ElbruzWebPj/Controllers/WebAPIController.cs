using ElbruzWebPj.Models.MVVM;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

namespace ElbruzWebPj.Controllers
{
    public class WebAPIController : Controller
    {

        public IActionResult ArtAndCulture()
        {

            string json = new WebClient().DownloadString("https://openapi.izmir.bel.tr/api/ibb/kultursanat/etkinlikler");

            var Events = JsonConvert.DeserializeObject<List<Events>>(json);

            return View(Events);
        }

    }
}
