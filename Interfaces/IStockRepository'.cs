using RealTime_StockExchange.DTO;
using RealTime_StockExchange.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealTime_StockExchange.Interfaces
{
    public interface IStockRepository
    {
        Task<Stock> AddStock(Stock _obj);
        Task<Stock> GetLatestStock( );

        Task<List<Stock>> GetAllStocks();




    }
}
