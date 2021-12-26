using AutoMapper;
using CommandAPI.Dtos;
using CommandAPI.Models;

namespace CommandAPI.Profiles
{
    public class CommandProfile : Profile
    {
        public CommandProfile()
		{
			CreateMap<Command, CommandReadDto>();
            
        }
    }
}