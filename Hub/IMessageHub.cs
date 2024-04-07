using RealTime_StockExchange.DTO;
using RealTime_StockExchange.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealTime_StockExchange.Hub
{
    public interface IMessageHubClient
    {
         Task SendStocksToUser(List<StockDTO> stockDTOs);
    }
}

