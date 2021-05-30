using ApplicationService.DTOs;
using MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            List<CategoryVM> categoriesVM = new List<CategoryVM>();

            using (ServiceReference1.Service1Client service = new ServiceReference1.Service1Client())
            {
                foreach (var item in service.GetCategories())
                {
                    categoriesVM.Add(new CategoryVM(item));
                }
            }
            return View(categoriesVM);
        }

        public ActionResult Create(CategoryVM categoryVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (ServiceReference1.Service1Client service = new ServiceReference1.Service1Client())
                    {
                        CategoryDTO categoryDTO = new CategoryDTO
                        {
                            Title = categoryVM.Title,
                            Description = categoryVM.Description
                        };
                        service.PostCategory(categoryDTO);

                        return RedirectToAction("Index");
                    }
                }
                return View();
            }
            catch (Exception)
            {
                return View();
            }

        }

        

        public ActionResult Delete(int id)
        {
            using (ServiceReference1.Service1Client service = new ServiceReference1.Service1Client())
            {
                service.DeleteCategory(id);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            CategoryVM model;
            using (ServiceReference1.Service1Client service = new ServiceReference1.Service1Client())
            {
                 model = new CategoryVM(service.GetCategoryById(id));
            }

            return View(model);
        }
    } 
}