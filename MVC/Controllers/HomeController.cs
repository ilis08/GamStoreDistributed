using MVC.Models;
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
    public class HomeController : Controller
    {
        private readonly Uri url = new Uri("http://localhost:44331/api");
        // GET: Game
        private static async Task<string> GetAccessToken()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44331");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("username","ilis08"),
                    new KeyValuePair<string, string>("password","123456"),
                    new KeyValuePair<string, string>("grant_type","password")
                };

                FormUrlEncodedContent content = new FormUrlEncodedContent(postData);

                HttpResponseMessage responseMessage = await client.PostAsync("token", content);

                string jsonString = await responseMessage.Content.ReadAsStringAsync();
                object responseData = JsonConvert.DeserializeObject(jsonString);

                return ((dynamic)responseData).access_token;
            }
        }

        public async Task<ActionResult> Index(string query)
        {
            string accessToken = await GetAccessToken();

            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                HttpResponseMessage responseMessage = await client.GetAsync("api/game?query=" + query);

                string jsonString = await responseMessage.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<List<GameVM>>(jsonString);

                return View(responseData);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Create(string query)
        {
            OrderVM orderVM = new OrderVM();

            string accessToken = await GetAccessToken();

            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                HttpResponseMessage responseMessage = await client.GetAsync("api/game?query=" + query);
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
                string accessToken = await GetAccessToken();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                    var content = JsonConvert.SerializeObject(orderVM);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    HttpResponseMessage responseMessage = await client.PostAsync("api/order", byteContent);

                    string jsonString = await responseMessage.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<OrderVM>(jsonString);
                }
                return RedirectToAction("Success");
            }

            catch (Exception)
            {
                return View();
            }
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}