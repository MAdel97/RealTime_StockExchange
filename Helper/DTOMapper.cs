using AutoMapper;
using RealTime_StockExchange.DTO;
using RealTime_StockExchange.Models;


namespace RealTime_StockExchange.Helper
{
    public class DTOMapper : Profile
    {
        public static IMapper mapper { get; set; }
        static DTOMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrderDTO, Order>().ReverseMap()  ;
                //cfg.CreateMap<Response, StockDTO>().ReverseMap();
                cfg.CreateMap<Stock,StockDTO>().ReverseMap().ForMember(o=>o.Id,opt=>opt.Ignore());
           });
            configuration.AssertConfigurationIsValid();
            mapper = configuration.CreateMapper();
        }
    }
}
