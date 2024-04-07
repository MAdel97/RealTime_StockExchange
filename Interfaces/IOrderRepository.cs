using RealTime_StockExchange.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealTime_StockExchange.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> AddOrder(Order _obj);
        Task<List<Order>> GetOrders();


    }
}
