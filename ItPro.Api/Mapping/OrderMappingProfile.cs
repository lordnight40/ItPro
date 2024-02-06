using AutoMapper;
using ItPro.Api.Models;
using ItPro.Data.Entities;

namespace ItPro.Api.Mapping;

public sealed class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<OrderModel, Order>()
            .ForMember(order => order.Client, config => config.MapFrom(x => new Client { Id = x.ClientId}));
        
        CreateMap<Order, OrderModel>();
    }
}