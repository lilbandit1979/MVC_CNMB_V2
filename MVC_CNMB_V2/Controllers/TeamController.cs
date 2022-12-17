using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_CNMB_V2.Models;
using Newtonsoft.Json;
using System.Text;

namespace MVC_CNMB_V2.Controllers
{
    public class TeamController : Controller
    {
        // GET: TeamController
        public ActionResult Index()
        {
            IEnumerable<Team> _teams = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7021/api/");
                //http Get
                var responseTask = client.GetAsync("Teams");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Team>>();
                    readTask.Wait();

                    _teams = readTask.Result;
                }
                else  //web api sent error response 
                {
                    //log response status here..
                    _teams = Enumerable.Empty<Team>();
                    ModelState.AddModelError(string.Empty, "Error: Team not found");
                }
                return View(_teams);
            }
        }

        // GET: TeamController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TeamController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TeamController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Team team)
        {
                var json = JsonConvert.SerializeObject(team);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                using (var client=new HttpClient())
                {
                    var url = "https://localhost:7021/api/Teams";
                    var response =await client.PostAsync(url, data); //added PutAsJsonAsync here instead of PostAsync
                    if(response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                }

                ModelState.AddModelError(string.Empty, "Error creating Team");    
                return View(team);
        }
       
        

        // GET: TeamController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TeamController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TeamController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TeamController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
