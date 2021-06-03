using ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.ViewModels
{
    public class GameVM
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }

        [Display(Name = "Date of release")]
        [DataType(DataType.Date)]
        public DateTime? Release { get; set; }

        public double Price { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public CategoryVM Category { get; set; }

        public SelectList CategorySelectList { get; set; }

        public GameVM()
        {

        }

        public GameVM(GameDTO gameDTO)
        {
            Id = gameDTO.Id;
            Name = gameDTO.Name;
            ShortDescription = gameDTO.ShortDescription;
            LongDescription = gameDTO.LongDescription;
            Release = gameDTO.Release;
            Price = gameDTO.Price;
            CategoryId = gameDTO.Category.Id;

            Category = new CategoryVM
            {
                Id = gameDTO.Category.Id,
                Title = gameDTO.Category.Title,
                Description = gameDTO.Category.Description
            };
        }

        public IEnumerable<GameDTO> allGames { get; set; }
    }
}