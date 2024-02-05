using AutoMapper;
using ItPro.Api.Models;
using ItPro.Data.Entities;

namespace ItPro.Api.Mapping;

public sealed class ClientMappingProfile : Profile
{
    public ClientMappingProfile()
    {
        CreateMap<ClientModel, Client>();
        CreateMap<Client, ClientModel>();
    }
}