using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RealTime_StockExchange.BL;
using RealTime_StockExchange.Common;
using RealTime_StockExchange.DTO;
using RealTime_StockExchange.Hub;
using SignalRDemo.Hub;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealTime_StockExchange.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        private StockBLL stockBLL = new StockBLL();
        private IHubContext<MessageHub, IMessageHubClient> messageHub;

        public StockController(IHubContext<MessageHub, IMessageHubClient> _messageHub)
        {
            stockBLL = new StockBLL();

            messageHub = _messageHub;

        }

        [HttpPost("GetStocks")]
        public async Task<bool> GetStocks()
        {


            GenaricResponse<int> response = new GenaricResponse<int>();
            Status status = new Status
            {
                Errors = null,
                Reason = "",
                StatusCode = 200
            };
            response.status = status;
            var stocks= await stockBLL.GetAllStocks();
            await messageHub.Clients.All.SendStocksToUser(stocks);
            return true;
        }

    }
}
