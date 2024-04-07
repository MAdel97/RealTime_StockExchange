using RealTime_StockExchange.Common;
using RealTime_StockExchange.DTO;
using RealTime_StockExchange.Helper;
using RealTime_StockExchange.Interfaces;
using RealTime_StockExchange.Models;
using RealTime_StockExchange.Repositories;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealTime_StockExchange.BL
{
    public class StockBLL : DTOMapper
    {
        private IStockRepository repository = null;
        private readonly IConfiguration _configuration;
        public StockBLL()
        {

            this.repository = new StockRepository( );
        }

        public async Task<Stock> AddStock(List<StockDTO> stock )
        {
            var obj = DTOMapper.mapper.Map<List<Stock>>(stock);
           

            var results=  await repository.AddStock(obj[0]);
            return  results;
           

        }
        public async Task<StockDTO> GetLatestStock()
        {
            var stock = await repository.GetLatestStock();

            var objDTO = DTOMapper.mapper.Map<StockDTO>(stock);
            return objDTO;
        }

        public async Task<List<StockDTO>> GetAllStocks()
        {
            var stock = await repository.GetAllStocks();

            var objDTO = DTOMapper.mapper.Map<List<StockDTO>>(stock);
            return objDTO;
        }



    }
}

