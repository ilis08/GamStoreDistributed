using ApplicationService.DTOs;
using Data.Context;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationService.Implementations
{
    public class CategoryManagementService
    {
        private GameStore2DistributedDBContext ctx = new GameStore2DistributedDBContext();
        
        public List<CategoryDTO> Get()
        {
            List<CategoryDTO> categoriesDto = new List<CategoryDTO>();

            foreach (var item in ctx.Categories.ToList())
            {
                categoriesDto.Add(new CategoryDTO
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description
                });

            }

            return categoriesDto;
        }

        public CategoryDTO GetById(int id)
        {
            CategoryDTO categoryDTO = new CategoryDTO();

            Category category = ctx.Categories.Find(id);
            if (category != null)
            {
                categoryDTO.Id = category.Id;
                categoryDTO.Title = category.Title;
                categoryDTO.Description = category.Description;
            }
            return categoryDTO;
        }

        public bool Edit(int? id)
        {
            Category category = new Category();

            var data = ctx.Categories.FirstOrDefault(x => x.Id == id);

                       if (category != null)
            {
                data.Title = category.Title;
                data.Description = category.Description;
                ctx.SaveChanges();
            }
            return true;
        }


        public bool Save(CategoryDTO categoryDTO)
        {
            Category Category = new Category()
            {
                Title = categoryDTO.Title,
                Description = categoryDTO.Description
            };

            try
            {
                Console.WriteLine(Category);
                ctx.Categories.Add(Category);
                ctx.SaveChanges();
                return true;
            }
            catch
            {
                System.Console.WriteLine(Category);
                return false;
            }
        }


        public bool Delete(int id)
        {
            try
            {
                Category category = ctx.Categories.Find(id);
                ctx.Categories.Remove(category);
                ctx.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

