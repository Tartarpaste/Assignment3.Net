using MagicTord_N_SondreTheWebAPI.Models;
using MagicTord_N_SondreTheWebAPI.Models.Dtos.Characters;
using AutoMapper;
using MagicTord_N_SondreTheWebAPI.Models.Dtos.Movies;

namespace MagicTord_N_SondreTheWebAPI.Profiles
{
    public class MovieProfile : Profile
    {

        public MovieProfile()
        {

            CreateMap<MoviePostDto, Movie>();
            CreateMap<List<MovieDto>, MovieDto>()
            .ForMember(dto => dto.Characters, opt => opt.MapFrom(src => src.SelectMany(m => m.Characters)));
            CreateMap<Movie, MovieDto>()
                .ForMember(dto => dto.Characters, opt => opt.MapFrom(src => src.Characters));




        }
    }
    }
