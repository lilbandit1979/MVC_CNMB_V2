using Microsoft.AspNetCore.Mvc;
using MVC_CNMB_V2.Models;

namespace MVC_CNMB_V2.Controllers
{
    public class SchoolController : Controller
    {
        //HTTP Get --all 
        public ActionResult Index()
        {
            IEnumerable<SchoolViewModel> _schools = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5021/api/");
                //http Get
                var responseTask = client.GetAsync("schools"); //plural???
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<SchoolViewModel>>();
                    readTask.Wait();

                    _schools = readTask.Result;
                }
                else  //web api sent error response 
                {
                    //log response status here..
                    _schools = Enumerable.Empty<SchoolViewModel>();
                    ModelState.AddModelError(string.Empty, "Error, no school found");
                }
                return View(_schools);
            }
        }

        //Http POST
        public ActionResult create()
        {
            return View();
        }
    }
}

