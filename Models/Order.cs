using System;
using System.Collections.Generic;
using System.Reflection;

namespace RealTime_StockExchange.Models
{
    public  class Order
    {
       
        public int Id { get; set; }
        public string Ticker { get; set; }
        public int quantity { get; set; }
        public decimal v { get; set; }
        public decimal vw { get; set; }
        public decimal o { get; set; }
        public decimal c { get; set; }
        public decimal h { get; set; }
        public decimal l { get; set; }
        public decimal n { get; set; }
    }
}
