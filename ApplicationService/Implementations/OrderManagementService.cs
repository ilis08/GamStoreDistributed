using ApplicationService.DTOs;
using Data.Context;
using Data.Entities;
using Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Implementations
{
    public class OrderManagementService
    {
        private GameStore2DistributedDBContext ctx = new GameStore2DistributedDBContext();

        public List<OrderDTO> Get()
        {
            List<OrderDTO> orderDto = new List<OrderDTO>();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (var item in unitOfWork.OrderRepository.Get())
                {
                    orderDto.Add(new OrderDTO
                    {
                        Id = item.Id,
                        BuyerName = item.BuyerName,
                        Address = item.Address,
                        Phone = item.Phone,
                        Email = item.Email,
                        Game = new GameDTO
                        {
                            Id = item.Game.Id,
                            Name = item.Game.Name,
                            ShortDescription = item.Game.ShortDescription,
                            LongDescription = item.Game.LongDescription,
                            Release = item.Game.Release,
                            Price = item.Game.Price
                        }
                    });
                }
            }
            return orderDto;
        }

        public OrderDTO GetById(int id)
        {
            OrderDTO orderDto = new OrderDTO();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Order order = unitOfWork.OrderRepository.GetByID(id);

                if (order != null)
                {
                    orderDto = new OrderDTO
                    {
                        Id = order.Id,
                        BuyerName = order.BuyerName,
                        Address = order.Address,
                        Phone = order.Phone,
                        Email = order.Email,
                        Game = new GameDTO
                        {
                            Id = order.Game.Id,
                            Name = order.Game.Name,
                            ShortDescription = order.Game.ShortDescription,
                            LongDescription = order.Game.LongDescription,
                            Release = order.Game.Release,
                            Price = order.Game.Price
                        }
                    };
                }
            }
            return orderDto;
        }

        public bool Save(OrderDTO orderDto)
        {

            Order order = new Order
            {
                Id = orderDto.Id,
                BuyerName = orderDto.BuyerName,
                Address = orderDto.Address,
                Phone = orderDto.Phone,
                Email = orderDto.Email,
                GameId = orderDto.Game.Id
            };

            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    if (orderDto.Id == 0)
                    {
                        unitOfWork.OrderRepository.Insert(order);
                    }
                    else
                    {
                        unitOfWork.OrderRepository.Update(order);
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
                    Order order = unitOfWork.OrderRepository.GetByID(id);
                    unitOfWork.OrderRepository.Delete(order);
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
