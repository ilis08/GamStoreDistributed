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
    [RoutePrefix("api/order")]
    public class OrderController : BaseController
    {
        private readonly OrderManagementService _service = null;

        public OrderController()
        {
            _service = new OrderManagementService();
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult Get(string query)
        {
            return Json(_service.Get(query));
        }
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            return Json(_service.GetById(id));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Save(OrderDTO orderDto)
        {
            if (orderDto.BuyerName == null)
            {
                return Json(new ResponseMessage { Code = 505, Body = "Data is not valid" });
            }

            ResponseMessage responseMessage = new ResponseMessage();

            if (_service.Save(orderDto))
            {
                responseMessage.Code = 201;
                responseMessage.Body = "Order was saved";
            }
            else
            {
                responseMessage.Code = 200;
                responseMessage.Body = "Order was not saved";
            }
            return Json(responseMessage);
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            ResponseMessage responseMessage = new ResponseMessage();

            if (_service.Delete(id))
            {
                responseMessage.Code = 200;
                responseMessage.Body = "Order was deleted";
            }
            else
            {
                responseMessage.Code = 200;
                responseMessage.Body = "Order was not deleted";
            }
            return Json(responseMessage);
        }
    }
}
