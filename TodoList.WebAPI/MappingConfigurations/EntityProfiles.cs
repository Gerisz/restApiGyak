using AutoMapper;
using ELTE.TodoList.Persistence;
using ELTE.TodoList.DTO;

namespace ELTE.TodoList.WebApi.MappingConfigurations
{
    public class ListProfile : Profile
    {
        public ListProfile()
        {
            CreateMap<List, ListDto>();
        }
    }

    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemDto>();
        }
    }
}
