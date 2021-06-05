using ApplicationService.DTOs;
using MVC.Helper;
using MVC.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class GameController : Controller
    {
        private readonly Uri url = new Uri("https://localhost:44331/api");
        // GET: Game
        public async Task<ActionResult> Index(string query)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = await client.GetAsync("api/game?query=" + query);

                string jsonString = await responseMessage.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<List<GameVM>>(jsonString);

                return View(responseData);
            }
        }

        public async Task<ActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = await client.GetAsync("api/game/"+id);

                string jsonString = await responseMessage.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<GameVM>(jsonString);

                return View(responseData);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id, string query)
        {
   
            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = await client.GetAsync("api/game/"+id);
                
                string jsonString = await responseMessage.Content.ReadAsStringAsync();
                var gameVM = JsonConvert.DeserializeObject<GameVM>(jsonString);

                responseMessage = await client.GetAsync("api/category?query="+ query);
                jsonString = await responseMessage.Content.ReadAsStringAsync();
                List<CategoryVM> categories = JsonConvert.DeserializeObject<List<CategoryVM>>(jsonString);
                gameVM.CategorySelectList = new SelectList(
                    categories,
                    "Id",
                    "Title");

                return View(gameVM);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(GameVM gameVM)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = JsonConvert.SerializeObject(gameVM);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    HttpResponseMessage responseMessage = await client.PostAsync("api/game", byteContent);

                    string jsonString = await responseMessage.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<GameVM>(jsonString);
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }
        }


        [HttpGet]
        public async Task<ActionResult> Create(string query)
        {
            GameVM gameVM = new GameVM();

            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = await client.GetAsync("api/category?query="+ query);
                string jsonString = await responseMessage.Content.ReadAsStringAsync();
                List<CategoryVM> categories = JsonConvert.DeserializeObject<List<CategoryVM>>(jsonString);
                gameVM.CategorySelectList = new SelectList(
                    categories,
                    "Id",
                    "Title");

                return View(gameVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(GameVM gameVM)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var content = JsonConvert.SerializeObject(gameVM);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    HttpResponseMessage responseMessage = await client.PostAsync("api/game", byteContent);

                    string jsonString = await responseMessage.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<GameVM>(jsonString);
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

                    HttpResponseMessage responseMessage = await client.DeleteAsync("api/game/" + id);

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
