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
    [RoutePrefix("api/game")]
    public class GameController : BaseController
    {
        private readonly GameManagementService _service = null;

        public GameController()
        {
            _service = new GameManagementService();
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
        public IHttpActionResult Save(GameDTO gameDTO)
        {
            if (gameDTO.Name == null)
            {
                return Json(new ResponseMessage { Code = 500, Body = "Data is not valid" });
            }

            ResponseMessage responseMessage = new ResponseMessage();

            if (_service.Save(gameDTO))
            {
                responseMessage.Code = 201;
                responseMessage.Body = "Game was saved";
            }
            else
            {
                responseMessage.Code = 200;
                responseMessage.Body = "Game was not saved";
            }

            return Json(responseMessage);
        }
        
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            ResponseMessage responseMessage = new ResponseMessage();

            if (_service.Delete(id))
            {
                responseMessage.Code = 200;
                responseMessage.Body = "Game was deleted";
            }
            else
            {
                responseMessage.Code = 200;
                responseMessage.Body = "Game was not deleted";
            }
            return Json(responseMessage);
        }
    }
}
