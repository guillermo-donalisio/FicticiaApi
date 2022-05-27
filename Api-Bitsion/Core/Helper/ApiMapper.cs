using Api_Bitsion.Core.Models.Clients;
using Api_Bitsion.Entities;
using AutoMapper;

namespace Api_Bitsion.Core.Helper;

public class ApiMapper : Profile
{
    public ApiMapper()
    {
        // GetRequest -> Client
        CreateMap<Client, ClientGetModelDTO>().ReverseMap();

        // InsertRequest -> Client
        CreateMap<Client, ClientCreateModelDTO>().ReverseMap();
        
        // UpdateRequest -> Client
        CreateMap<Client, ClientUpdateModelDTO>().ReverseMap();
    }
}
