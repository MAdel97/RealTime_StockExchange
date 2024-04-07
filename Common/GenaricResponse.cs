using System.Collections.Generic;

namespace RealTime_StockExchange.Common
{

    public class GenaricResponse<T>
    {
        public Status status { get; set; }
        public T item { get; set; }
    }
    public class GenaricResponseMany<T>
    {
        public Status status { get; set; }
        public T items { get; set; }
    }

    public class Status
    {
        public int StatusCode { get; set; }
        public string Reason { get; set; }
        public Error Errors { get; set; }
    }
    public class Error
    {
        public string key { get; set; }
        public List<string> ValidationErrors { get; set; }
    }
}

