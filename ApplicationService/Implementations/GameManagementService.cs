using ApplicationService.DTOs;
using Data.Context;
using Data.Entities;
using Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationService.Implementations
{
    public class GameManagementService
    {
        private GameStore2DistributedDBContext ctx = new GameStore2DistributedDBContext();

        public List<GameDTO> Get(string query)
        {
            List<GameDTO> gamesDto = new List<GameDTO>();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (query == null)
                {
                    foreach (var item in unitOfWork.GameRepository.Get())
                    {
                        gamesDto.Add(new GameDTO
                        {
                            Id = item.Id,
                            Name = item.Name,
                            ShortDescription = item.ShortDescription,
                            LongDescription = item.LongDescription,
                            Release = item.Release,
                            Price = item.Price,
                            Category = new CategoryDTO
                            {
                                Id = item.Category.Id,
                                Title = item.Category.Title,
                                Description = item.Category.Description
                            }
                        });
                    }
                }
                else
                {
                    foreach (var item in unitOfWork.GameRepository.GetByQuery().Where(c => c.Name.Contains(query)).ToList())
                    {
                        gamesDto.Add(new GameDTO
                        {
                            Id = item.Id,
                            Name = item.Name,
                            ShortDescription = item.ShortDescription,
                            LongDescription = item.LongDescription,
                            Release = item.Release,
                            Price = item.Price,
                            Category = new CategoryDTO
                            {
                                Id = item.Category.Id,
                                Title = item.Category.Title,
                                Description = item.Category.Description
                            }
                        });
                    }
                }

               
            }
            return gamesDto;
        }

        public GameDTO GetById(int id)
        {
            GameDTO gameDTO = new GameDTO();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Game game = unitOfWork.GameRepository.GetByID(id);

                    gameDTO = new GameDTO
                    {
                        Id = game.Id,
                        Name = game.Name,
                        ShortDescription = game.ShortDescription,
                        LongDescription = game.LongDescription,
                        Release = game.Release,
                        Price = game.Price,
                        Category = new CategoryDTO
                        {
                            Id = game.Category.Id,
                            Title = game.Category.Title,
                            Description = game.Category.Description
                        }
                    };
            }
            return gameDTO;
        }

        public bool Save(GameDTO gameDto)
        {
            Game game = new Game
            {
                Id = gameDto.Id,
                Name = gameDto.Name,
                ShortDescription = gameDto.ShortDescription,
                LongDescription = gameDto.LongDescription,
                Release = gameDto.Release,
                Price = gameDto.Price,
                CategoryId = gameDto.Category.Id
            };

            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    if (gameDto.Id == 0)
                    {
                        unitOfWork.GameRepository.Insert(game);
                    }
                    else
                    {
                        unitOfWork.GameRepository.Update(game);
                    }
                    unitOfWork.Save();
                }

                return true;
            }
            catch 
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Game game = unitOfWork.GameRepository.GetByID(id);
                    unitOfWork.GameRepository.Delete(game);
                    unitOfWork.Save();
                }

                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
