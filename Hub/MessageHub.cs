using Microsoft.AspNetCore.SignalR;
using RealTime_StockExchange.DTO;
using RealTime_StockExchange.Hub;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalRDemo.Hub
{
    public class MessageHub : Hub<IMessageHubClient>
    {
        public async Task SendStocksToUser(List<StockDTO> message)
        {                                                                                                   
            await Clients.All.SendStocksToUser(message);
        }
    }
}