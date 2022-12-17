using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_CNMB_V2.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace MVC_CNMB_V2.Controllers
{
    public class SchoolController1 : Controller
    {
        // GET: SchoolController1
        //HTTP Get --all 
        School school = new School();
        List<School> _schools = new List<School>();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<School>>GetAllSchools()
        {
            _schools = new List<School>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7021/api/Schools"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _schools = JsonConvert.DeserializeObject<List<School>>(apiResponse);
                }
            }
            return _schools;
        }

        [HttpGet]
        public async Task<School> GetSchoolById(int schoolId)
        {
            
            var school = new School();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7021/api/Schools"+schoolId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    school = JsonConvert.DeserializeObject<School>(apiResponse);
                }
            }
            return school; 
        }

        [HttpDelete]
        public async Task<School> DeleteSchool(int schoolId)
        {

            var school = new School();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7021/api/Schools" + schoolId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    school = JsonConvert.DeserializeObject<School>(apiResponse);
                }
            }
            return school;
        }

        [HttpPost]
        public async Task<School> UpdateSchool(School school)
        {

            var _school = new School();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(school), Encoding.UTF8, "application.json");
                
                using (var response = await httpClient.PostAsync("https://localhost:7021/api/Schools", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    school = JsonConvert.DeserializeObject<School>(apiResponse);
                }
            }
            return school;
        }

        //public ActionResult Index()
        //{
        //    {
        //        School school = new School();
        //        List<School> _schools = new List<School>();
        //        using (var client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri("https://localhost:7021/api/");
        //            //http Get
        //            var responseTask = client.GetAsync("schools");
        //            responseTask.Wait();
        //            var result = responseTask.Result;
        //            if (result.IsSuccessStatusCode)
        //            {
        //                var readTask = result.Content.ReadAsAsync<IList<School>>();
        //                readTask.Wait();

        //                _schools = readTask.Result;
        //            }
        //            else  //Error 
        //            {
        //                _schools = Enumerable.Empty<School>();
        //                ModelState.AddModelError(string.Empty, "Error, no schools found");
        //            }
        //            return View(_schools);
        //        }
        //    }
        //}

        // GET: SchoolController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SchoolController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SchoolController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: SchoolController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SchoolController1/Edit/5
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

        // GET: SchoolController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SchoolController1/Delete/5
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
