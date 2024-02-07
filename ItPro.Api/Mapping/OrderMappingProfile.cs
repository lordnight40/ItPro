using AutoMapper;
using ItPro.Api.Models;
using ItPro.Core.Helpful;
using ItPro.Data.Entities;

namespace ItPro.Api.Mapping;

/// <summary>
/// Настройки маппинга для заказа.
/// </summary>
public sealed class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<OrderModel, Order>()
            .ForMember(order => order.Client, config => config.MapFrom(x => new Client { Id = x.ClientId}));
        
        CreateMap<Order, OrderModel>()
            .ForMember(orderModel => orderModel.ClientId, config => config.MapFrom(x => x.Client.Id));
        
        CreateMap<PagedObject<Order>, PagedObject<OrderModel>>()
            .ConstructUsing((x, ctx) => new PagedObject<OrderModel>(
                x.Items.Select(item => ctx.Mapper.Map<OrderModel>(item)),
                x.TotalCount,
                x.CurrentPage,
                x.PageSize));
    }
}