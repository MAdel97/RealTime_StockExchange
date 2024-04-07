using RealTime_StockExchange.Common;
using RealTime_StockExchange.Helper;
using RealTime_StockExchange.Interfaces;
using RealTime_StockExchange.Models;
using RealTime_StockExchange.Repositories;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using RealTime_StockExchange.DTO;

namespace RealTime_StockExchange.BL
{
    public class OrderBL : DTOMapper
    {
        private IOrderRepository repository = null;
        private readonly IConfiguration _configuration;
        public OrderBL()
        {

            this.repository = new OrderRepository();
        }

        public async Task<Order> Addorder(OrderDTO orderDTO )
        {
            var obj = DTOMapper.mapper.Map<Order>(orderDTO);

            return await repository.AddOrder(obj);
           

        }
        public async Task<List<OrderDTO>> GetOrders()
        {
            var orders = await repository.GetOrders();

            var objDTO = DTOMapper.mapper.Map<List<OrderDTO>>(orders);
            return objDTO;
        }


    }
}

