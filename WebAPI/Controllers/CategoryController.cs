using ApplicationService.DTOs;
using ApplicationService.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Messages;

namespace WebAPI.Controllers
{
   
    [RoutePrefix("api/category")]
    public class CategoryController : BaseController
    {
        private readonly CategoryManagementService _service = null;

        public CategoryController()
        {
            _service = new CategoryManagementService();
        }


        [HttpGet]
        [Route("")]
        public IHttpActionResult Get(string query)
        {
            return Json(_service.Get(query));
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            return Json(_service.GetById(id));
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Save(CategoryDTO categoryDto)
        {
            if (categoryDto.Title == null && categoryDto.Description == null)
            {
                return Json(new ResponseMessage { Code = 500, Error = "Data is not valid" });
            }

            ResponseMessage response = new ResponseMessage();

            if (_service.Save(categoryDto))
            {
                response.Code = 201;
                response.Body = "Category was saved";
            }
            else
            {
                response.Code = 200;
                response.Body = "Category was not saved";
            }

            return Json(response);
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            ResponseMessage response = new ResponseMessage();

            if (_service.Delete(id))
            {
                response.Code = 200;
                response.Body = "Category has been deleted";
            }
            else
            {
                response.Code = 200;
                response.Body = "category has not been saved";
            }
            return Json(response);
        }
    }
}
