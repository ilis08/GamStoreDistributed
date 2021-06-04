using ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace MVC.ViewModels
{
    public class OrderVM
    {
        public int Id { get; set; }
        [StringLength(25)]
        [Required]
        public string BuyerName { get; set; }
        [Required]
        public string Address { get; set; }

        public string Phone { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Game")]
        public int GameId { get; set; }

        public GameVM Game { get; set; }

        public SelectList GameSelectList { get; set; }

        public OrderVM()
        {

        }

        public OrderVM(OrderDTO orderDTO)
        {
            Id = orderDTO.Id;
            BuyerName = orderDTO.BuyerName;
            Address = orderDTO.Address;
            Phone = orderDTO.Phone;
            Email = orderDTO.Email;

            GameId = orderDTO.Game.Id;

            Game = new GameVM
            {
                Id = orderDTO.Game.Id,
                Name = orderDTO.Game.Name,
                ShortDescription = orderDTO.Game.ShortDescription,
                LongDescription = orderDTO.Game.LongDescription,
                Release = orderDTO.Game.Release,
                Price = orderDTO.Game.Price
            };
        }
    }
}