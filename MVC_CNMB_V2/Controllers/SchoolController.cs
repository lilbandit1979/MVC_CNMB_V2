using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_CNMB_V2.Models;
using Newtonsoft.Json;
using NuGet.Packaging.Core;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace MVC_CNMB_V2.Controllers
{
    public class SchoolController : Controller
    {
        // GET: SchoolController1
        //HTTP Get 
        School school = new School();
        List<School> _schools = new List<School>();

        //[HttpGet]
        public ActionResult Index()
        {

            var result = GetAllSchools().Result;
            return View(result);
           
        }

        
        public async Task<List<School>> GetAllSchools()
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
        public async Task<ActionResult<School>> GetSchoolById(int id)
        {
            var school = new School();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7021/api/Schools/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    school = JsonConvert.DeserializeObject<School>(apiResponse);
                }
            }
            return View(school); 
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost] //used async
        public async Task<ActionResult> create(School school)
        {
            if(school==null)
            {
                return View(school);
            }
            else
            {
                var json = JsonConvert.SerializeObject(school);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    var url = "https://localhost:7021/api/Schools";
                    var response = await client.PostAsync(url, data);
                    var result1 = response.Content.ReadAsStringAsync().Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }

                ModelState.AddModelError(string.Empty, "Error");

                return View(school);
            }
           
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var school = new School();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7021/api/Schools/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    school = JsonConvert.DeserializeObject<School>(apiResponse);
                }
            }
            if (school == null)
            {
                return NotFound();
            }
            return View(school);
        }

        [HttpPost] //used async
        public async Task<ActionResult> EditConfirmed([Bind("SchoolId", "SchoolName", "SchoolPhone", "SchoolAddress", "SchoolEircode")] School school)
        {
            if (school == null)
            {
                return View(school);
            }
            else
            {
                var json = JsonConvert.SerializeObject(school);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    var url = "https://localhost:7021/api/Schools";
                    var response = await client.PostAsync(url, data);
                    //var result1 = response.Content.ReadAsStringAsync().Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }

                ModelState.AddModelError(string.Empty, "Error");

                return View(school);
            }
        }

            public async Task<IActionResult> Delete(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            var school = new School();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7021/api/Schools/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    school = JsonConvert.DeserializeObject<School>(apiResponse);
                }
            }
            if (school == null)
            {
                return NotFound();
            }
            return View(school);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteConfirm(int id)
        {
            string info = "";

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:7021/api/Schools/" + id))
                {
                    info = await response.Content.ReadAsStringAsync();
                    if(response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return NotFound();
        }
    }
}
