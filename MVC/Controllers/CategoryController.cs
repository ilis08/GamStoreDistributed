using ApplicationService.DTOs;
using MVC.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly Uri url = new Uri("https://localhost:44331/api/category");
        // GET: Category
        public async Task<ActionResult> Index(string query)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = await client.GetAsync("?query="+query);

                string jsonString = await responseMessage.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<List<CategoryVM>>(jsonString);

                return View(responseData);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = await client.GetAsync("category/" +id);

                string jsonString = await responseMessage.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<CategoryVM>(jsonString);

                return View(responseData);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = await client.GetAsync("category/" + id);

                string jsonString = await responseMessage.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<CategoryVM>(jsonString);

                return View(responseData);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(CategoryVM categoryVM)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var content = JsonConvert.SerializeObject(categoryVM);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    HttpResponseMessage responseMessage = await client.PostAsync("category/", byteContent);

                    string jsonString = await responseMessage.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<CategoryVM>(jsonString);
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CategoryVM categoryVM)
        {
           
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var content = JsonConvert.SerializeObject(categoryVM);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    
                    HttpResponseMessage responseMessage = await client.PostAsync("category/", byteContent);
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }
            
        }

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = await client.DeleteAsync("category/" + id);

                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
                throw;
            }
            
            
        }
    } 
}