using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using RealTime_StockExchange.BL;
using RealTime_StockExchange.DTO;
using RealTime_StockExchange.Models;
using System.Collections.Generic;
using AcademicCoursesCRUD.Common;
using System;
using SignalRDemo.Hub;
using RealTime_StockExchange.Hub;
using Microsoft.AspNetCore.SignalR;

namespace RealTime_StockExchange.Helper { 
internal interface IScopedProcessingService
{
    Task DoWork(CancellationToken stoppingToken);
}

    public class ScopedProcessingService : IScopedProcessingService
    {
        private int executionCount = 0;
        private StockBLL stockBLL = null;
        private OrderBL orderBL = null;



        private readonly ILogger _logger;

        public ScopedProcessingService(ILogger<ScopedProcessingService> logger)
        {
            _logger = logger;
            stockBLL = new StockBLL();
            orderBL= new OrderBL();

        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;

                _logger.LogInformation(
                    "Scoped Processing Service is working. Count: {Count}", executionCount);
               List<StockDTO> response= WebAPIConsumer.AddComponent();
                    //.List<StockDTO> postresponse = WebAPIConsumer.GetComponentAsync().Result;
                if (response!=null)
                {
                    try
                    {
                        await stockBLL.AddStock(response);
                       // await stockBLL.GetAllStocks();
                       

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }

                
              

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}