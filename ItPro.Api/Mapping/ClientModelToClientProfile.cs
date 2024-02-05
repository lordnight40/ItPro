using AutoMapper;
using ItPro.Api.Models;
using ItPro.Data.Entities;

namespace ItPro.Api.Mapping;

public sealed class ClientModelToClientProfile : Profile
{
    public ClientModelToClientProfile()
    {
        CreateMap<ClientModel, Client>();
    }
}