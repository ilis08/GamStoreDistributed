using ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.ViewModels
{
    public class CategoryVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public CategoryVM()
        {

        }

        public CategoryVM(CategoryDTO categoryDTO)
        {
            Id = categoryDTO.Id;
            Title = categoryDTO.Title;
            Description = categoryDTO.Description;
        }
    }
}