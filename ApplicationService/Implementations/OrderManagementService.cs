using ApplicationService.DTOs;
using Data.Context;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Implementations
{
    public class OrderManagementService
    {
        private GameStore1DistributedDBContext ctx = new GameStore1DistributedDBContext();

        public List<OrderDTO> Get()
        {
            List<OrderDTO> orderDto = new List<OrderDTO>();

            foreach (var item in ctx.Orders.ToList())
            {
                orderDto.Add(new OrderDTO
                {
                    Id = item.Id,
                    BuyerName = item.BuyerName,
                    Address = item.Address,
                    Phone = item.Phone,
                    Email = item.Email,
                    TimeOfOrder = item.TimeOfOrder,
                    GameId = item.GameId,
                    Game = new GameDTO
                    {
                        Id = item.GameId,
                        Name = item.Game.Name,
                        ShortDescription = item.Game.ShortDescription,
                        LongDescription = item.Game.LongDescription,
                        Release = item.Game.Release,
                        Price = item.Game.Price
                    }
                });
            }
            return orderDto;
        }

        public OrderDTO GetById(int id)
        {
             Order order = ctx.Orders.Find(id);

            OrderDTO orderDTO = new OrderDTO
            {
                Id = order.Id,
                BuyerName = order.BuyerName,
                Address = order.Address,
                Phone = order.Phone,
                Email = order.Email,
                TimeOfOrder = order.TimeOfOrder,
                GameId = order.GameId,
                Game = new GameDTO
                {
                    Id = order.GameId,
                    Name = order.Game.Name,
                    ShortDescription = order.Game.ShortDescription,
                    LongDescription = order.Game.LongDescription,
                    Release = order.Game.Release,
                    Price = order.Game.Price
                }
            };
            return orderDTO;
        }

        public bool Save(OrderDTO orderDto)
        {
            if (orderDto.Game == null || orderDto.GameId == 0)
            {
                return false;
            }

            /* Category category = new Category
             {
                 Id = gameDto.CategoryId,
                 Title = gameDto.Category.Title,
                 Description = gameDto.Category.Description
             };*/

            Order order = new Order
            {
                BuyerName = orderDto.BuyerName,
                Address = orderDto.Address,
                Phone = orderDto.Phone,
                Email = orderDto.Email,
                TimeOfOrder = orderDto.TimeOfOrder,
                GameId = orderDto.GameId,
            };

            try
            {
                ctx.Orders.Add(order);
                ctx.SaveChanges();

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
                Order order = ctx.Orders.Find(id);
                ctx.Orders.Remove(order);
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
