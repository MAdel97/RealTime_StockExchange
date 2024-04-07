using RealTime_StockExchange.BL;
using RealTime_StockExchange.Common;
using RealTime_StockExchange.DTO;
using RealTime_StockExchange.Interfaces;
using RealTime_StockExchange.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealTime_StockExchange.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockNotifierController : ControllerBase
    {
        private OrderBL orderBL=null;

        private readonly ILogger<StockNotifierController> _logger;

        public StockNotifierController(ILogger<StockNotifierController> logger)
        {
            orderBL = new OrderBL();
            _logger = logger;
        }

        [HttpPost("AddOrder")]
        
        public  async Task<Order> AddOrder([FromBody] OrderDTO clientDTO)
            {
             GenaricResponse<int> response = new GenaricResponse<int>();
            Status status = new Status  
            {
                Errors = null,
                Reason = "",
                StatusCode = 200
            };
            response.status = status;
            return await orderBL.Addorder(clientDTO);
        }
        [HttpGet("GetOrders")]
        public async Task<List<OrderDTO>>GetOrders()
        {
           

            GenaricResponse<int> response = new GenaricResponse<int>();
            Status status = new Status
            {
                Errors = null,
                Reason = "",
                StatusCode = 200
            };
            response.status = status;
             return await orderBL.GetOrders();
        }
      
     
    }
}
