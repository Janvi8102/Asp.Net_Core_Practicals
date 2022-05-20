using Microsoft.AspNetCore.Mvc;
using Practical17_ViewModel.Helper;
using Practical17_ViewModel.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System;

namespace Practical17_ViewModel.Controllers
{
    public class HomeController : Controller
    {
        StudentAPI _api = new StudentAPI();
        
        public async Task<IActionResult> Index()
        {
            List<StudentData> students = new List<StudentData>();
            HttpClient client =  _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Student");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                students = JsonConvert.DeserializeObject<List<StudentData>>(results);
            }
            return View(students);
        }

        //Details
        public async Task<IActionResult> Details(int? id)
        {
            StudentData students = new StudentData();
            if (id == null)
            {
                return NotFound();
            }
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/Student/{id}");
            if (res.IsSuccessStatusCode)
            {
                var reslut = res.Content.ReadAsStringAsync().Result;
                students = JsonConvert.DeserializeObject<StudentData>(reslut);
            }
            return View(students);
        }

        // Create
        public IActionResult Create()
        {
            return View();
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentData student)
        {
            if (ModelState.IsValid)
            {

                HttpClient client = _api.Initial();

                var postdata = client.PostAsJsonAsync("api/Student", student);
                postdata.Wait();
                var res = postdata.Result;
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(student);
        }

        //Edit
        public async Task<IActionResult> Edit(int? id)
        {
            StudentData students = new StudentData();
            if (id == null)
            {
                return NotFound();
            }

            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Student/" + id);
            if (res.IsSuccessStatusCode)
            {
                var reslut = res.Content.ReadAsStringAsync().Result;
                students = JsonConvert.DeserializeObject<StudentData>(reslut);
            }
            return View(students);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentData student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    HttpClient client = _api.Initial();

                    var postdata = client.PutAsJsonAsync("api/Students/" + id, student);
                    postdata.Wait();
                    var res = postdata.Result;
                    if (res.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception e)
                {

                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        //Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            StudentData students = new StudentData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync("api/Student/" + id);
            return RedirectToAction(nameof(Index));
        }

    }
}
