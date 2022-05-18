using AutoMapper;
using Challenge_Sbi.Dto;

namespace Challenge_Sbi
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<ServerPost, Salida>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.id))
                .ForMember(dest => dest.Titulo, opts => opts.MapFrom(src => src.title));
        }
    }
}
