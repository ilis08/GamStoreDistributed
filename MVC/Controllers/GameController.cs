using ApplicationService.DTOs;
using MVC.Helper;
using MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            List<GameVM> games = new List<GameVM>();

            using (ServiceReference1.Service1Client service = new ServiceReference1.Service1Client())
            {
                foreach (var item in service.GetGames())
                {
                    games.Add(new GameVM(item));
                }
            }
            return View(games);
        }


        public ActionResult Create()
        {
            ViewBag.Categories = Helper.LoadDataHelper.LoadCategoryData();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GameVM gameVM)
        {
            try
            {
                    using (ServiceReference1.Service1Client service = new ServiceReference1.Service1Client())
                    {
                        GameDTO gameDTO = new GameDTO
                        {
                            Name = gameVM.Name,
                            ShortDescription = gameVM.ShortDescription,
                            LongDescription = gameVM.ShortDescription,
                            Release = gameVM.Release,
                            Price = gameVM.Price,
                            CategoryId = gameVM.CategoryId,
                            Category = new CategoryDTO
                            {
                                Id = gameVM.CategoryId,
                            }
                        };
                        service.PostGame(gameDTO);
                    }
                return RedirectToAction("Index");
            }
           
            catch (Exception)
            {
                ViewBag.Categories = Helper.LoadDataHelper.LoadCategoryData();
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            GameVM model = new GameVM();

            using (ServiceReference1.Service1Client service = new ServiceReference1.Service1Client())
            {
                var gameDto = service.GetGameById(id);
                model = new GameVM(gameDto);
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            using(ServiceReference1.Service1Client service = new ServiceReference1.Service1Client())
            {
                service.DeleteGame(id);
            }
            return RedirectToAction("Index");
        }
        
    }
    
}
