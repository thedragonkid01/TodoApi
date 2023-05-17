using AutoMapper;
using TodoListApi.DTOs;
using TodoListApi.Enums;
using TodoListApi.Models;

namespace TodoListApi.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TodoDTO, Todo>().ReverseMap();
            CreateMap<TodoCreateDTO, Todo>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)EStatus.New))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<TagDTO, Tag>().ReverseMap();
            CreateMap<TagCreateTodoDTO, Tag>().ReverseMap();

            CreateMap<TodoTagDTO, TodoTag>().ReverseMap();

            CreateMap<CommentDTO, Comment>().ReverseMap();
            CreateMap<CommentCreateTodoDTO, Comment>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
