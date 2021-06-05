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
    public class OrderController : Controller
    {
        private readonly Uri url = new Uri("https://localhost:44331/api");
        // GET: Order
        public async Task<ActionResult> Index(string query)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = await client.GetAsync("api/order?query="+query);

                string jsonString = await responseMessage.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<List<OrderVM>>(jsonString);

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

                HttpResponseMessage responseMessage = await client.GetAsync("api/order/" + id);

                string jsonString = await responseMessage.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<OrderVM>(jsonString);

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

                HttpResponseMessage responseMessage = await client.GetAsync("api/order/" + id);

                string jsonString = await responseMessage.Content.ReadAsStringAsync();
                var orderVM = JsonConvert.DeserializeObject<OrderVM>(jsonString);

                responseMessage = await client.GetAsync("api/game?query="+query);
                jsonString = await responseMessage.Content.ReadAsStringAsync();
                List<GameVM> games = JsonConvert.DeserializeObject<List<GameVM>>(jsonString);
                orderVM.GameSelectList = new SelectList(
                    games,
                    "Id",
                    "Name");

                return View(orderVM);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(OrderVM orderVM)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = JsonConvert.SerializeObject(orderVM);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    HttpResponseMessage responseMessage = await client.PostAsync("api/order", byteContent);

                    string jsonString = await responseMessage.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<OrderVM>(jsonString);
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
            OrderVM orderVM = new OrderVM();

            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = await client.GetAsync("api/game?query="+query);
                string jsonString = await responseMessage.Content.ReadAsStringAsync();
                List<GameVM> games = JsonConvert.DeserializeObject<List<GameVM>>(jsonString);
                orderVM.GameSelectList = new SelectList(
                    games,
                    "Id",
                    "Name");

                return View(orderVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OrderVM orderVM)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var content = JsonConvert.SerializeObject(orderVM);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    HttpResponseMessage responseMessage = await client.PostAsync("api/order", byteContent);

                    string jsonString = await responseMessage.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<OrderVM>(jsonString);
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

                    HttpResponseMessage responseMessage = await client.DeleteAsync("api/order/" + id);

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