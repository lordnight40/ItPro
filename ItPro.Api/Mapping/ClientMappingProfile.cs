using AutoMapper;
using ItPro.Api.Models;
using ItPro.Core.Repository;
using ItPro.Data.Entities;

namespace ItPro.Api.Mapping;

public sealed class ClientMappingProfile : Profile
{
    public ClientMappingProfile()
    {
        CreateMap<ClientModel, Client>();
        CreateMap<Client, ClientModel>();
        
        CreateMap<PagedObject<Client>, PagedObject<ClientModel>>()
            .ConstructUsing((x, ctx) => new PagedObject<ClientModel>(
                x.Items.Select(item => ctx.Mapper.Map<ClientModel>(item)),
                x.TotalCount,
                x.CurrentPage,
                x.PageSize));
    }
}