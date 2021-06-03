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
        public IHttpActionResult Get()
        {
            return Json(_service.Get());
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            return Json(_service.GetById(id));
        }

        [HttpPost]
        public IHttpActionResult Save(CategoryDTO categoryDto)
        {
            if (categoryDto.Title == null || categoryDto.Description == null)
            {
                return Json(new ResponseMessage { Code = 500, Error = "Data is not valid" });
            }

            ResponseMessage response = new ResponseMessage();

            if (_service.Save(categoryDto))
            {
                response.Code = 200;
                response.Body = "Category was saved";
            }
            else
            {
                response.Code = 500;
                response.Body = "Category was not saved";
            }

            return Json(response);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            return Json(_service.Delete(id));
        }
    }
}
