using Microsoft.AspNetCore.Mvc;
using MVC_CNMB_V2.Models;
using Newtonsoft.Json;
using System.Text;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace MVC_CNMB_V2.Controllers
{
    public class SchoolController_old : Controller
    {
        //HTTP Get --all 
        public ActionResult Index()
        {
            IEnumerable<School> _schools = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7021/api/"); 
                //http Get
                var responseTask = client.GetAsync("schools"); 
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<School>>();
                    readTask.Wait();

                    _schools = readTask.Result;
                }
                else  //Error 
                { 
                    _schools = Enumerable.Empty<School>();
                    ModelState.AddModelError(string.Empty, "Error, no schools found");
                }
                return View(_schools);
            }
        }

        // GET: TestController/Details/5
        //public ActionResult Details()
        //{

        //    return View();
        //}
        public async Task<ActionResult> Details(int id)
        {
            string Baseurl = "https://localhost:7021/api/";
            School SchoolDetails = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("School/GetSchoolById/");
                if (Res.IsSuccessStatusCode)
                {
                    var record = Res.Content.ReadAsStringAsync().Result;
                    SchoolDetails = JsonConvert.DeserializeObject<School>(record);
                }
                return View(SchoolDetails);
            }
        }
    

// GET: SchoolController/Edit/5
public ActionResult Edit(int id)
        {
            return View(id);
        }

        // POST: TestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(School school,int id)
        {
            List<School> _schools = new List<School>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7021/api/");
                //http Get
                var responseTask = await client.GetAsync("/School/GetSchoolById"); //coud prepend /api/

                if (responseTask.IsSuccessStatusCode)
                {
                    var found = responseTask.Content.ReadAsStringAsync().Result;
                    _schools = JsonConvert.DeserializeObject<List<School>>(found);
                }
                return View(school);
            } 
        }


        //Http POST
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult createOld(School school)
        {
            var json = JsonConvert.SerializeObject(school);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var url = "https://localhost:7021/api/Schools";
                var response = client.PostAsync(url, data).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Error");

            return View(school);
        }
        [HttpPost] //used async
        public async Task<ActionResult> create(School school)
        {
            var json = JsonConvert.SerializeObject(school);
            var data = new StringContent(json, Encoding.UTF8, "application/json");


            using (var client = new HttpClient())
            {
                var url = "https://localhost:7021/api/Schools";
                var response = await client.PostAsync(url, data);
                //var result1 = response.Content.ReadAsStringAsync().Result;
                //client.PutAsJsonAsync(url, data); --put method in here
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Error");

            return View(school);
        }

        [HttpPut] //not working yet ----------->> comback to this tomorrow
        [Route("/api/School/EditSchool")]
        public async Task<ActionResult> edit(School school)
        {
            var json = JsonConvert.SerializeObject(school);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var url = "https://localhost:7021/api/Schools";
                var response = await client.PutAsync(url, data);
                //var result1 = response.Content.ReadAsStringAsync().Result;
                //client.PutAsJsonAsync(url, data); --put method in here
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Error");

            return View(school);
        }
    }
}
