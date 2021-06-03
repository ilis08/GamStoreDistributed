using ApplicationService.DTOs;
using ApplicationService.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        private CategoryManagementService categoryService = new CategoryManagementService();

        public string DeleteCategory(int id)
        {
            if (!categoryService.Delete(id))
            {
                return "Category is not deleted!";
            }
            else
            {
                return "Category is deleted!";
            }
        }

        public List<CategoryDTO> GetCategories()
        {
            return categoryService.Get();
        }

        public CategoryDTO GetCategoryById(int id)
        {
            return categoryService.GetById(id);
        }

        public string PostCategory(CategoryDTO categoryDto)
        {
            if (!categoryService.Save(categoryDto))
            {
                return "Category is not saved.";
            }
            else
            {
                return "Category is saved.";
            }
        }

        // Game

        private GameManagementService gameService = new GameManagementService();
        
        public List<GameDTO> GetGames()
        {
            return gameService.Get();
        }

        public string PostGame(GameDTO gameDto)
        {
            if (!gameService.Save(gameDto))
            {
                return "Game is not saved";
            }
            else
            {
                return "Game is saved";
            }
        }

        public string PutGame(int id)
        {
            throw new NotImplementedException();
        }

        public GameDTO GetGameById(int id)
        {
            return gameService.GetById(id);
        }

        public string DeleteGame(int id)
        {
            if (!gameService.Delete(id))
            {
                return "Game is not deleted";
            }
            else
            {
                return "Game is deleted";
            }
        }

      
    }
}
