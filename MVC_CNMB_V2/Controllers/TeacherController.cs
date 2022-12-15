using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_CNMB_V2.Models;
using Newtonsoft.Json;
using System.Text;

namespace MVC_CNMB_V2.Controllers
{
    public class TeacherController : Controller
    {
        // GET: TeacherController
        public ActionResult Index()
        {
            IEnumerable<Teacher> _teachers = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7021/api/");
                //http Get
                var responseTask = client.GetAsync("teachers"); 
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Teacher>>();
                    readTask.Wait();

                    _teachers = readTask.Result;
                }
                else  //web api sent error response 
                {
                    //log response status here..
                    _teachers = Enumerable.Empty<Teacher>();
                    ModelState.AddModelError(string.Empty, "Error: Teacher not found");
                }
                return View(_teachers);

            }
        }

        // GET: TeacherController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TeacherController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TeacherController/Create
        [HttpPost] //used async
        public async Task<ActionResult> create(Teacher teacher)
        {
            var json = JsonConvert.SerializeObject(teacher);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var url = "https://localhost:7021/api/Teachers";
                var response = await client.PostAsync(url, data);
                //var result1 = response.Content.ReadAsStringAsync().Result;
                //client.PutAsJsonAsync(url, data); --put method in here
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Error");

            return View(teacher);
        }

        // GET: TeacherController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TeacherController/Edit/5
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

        // GET: TeacherController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TeacherController/Delete/5
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
