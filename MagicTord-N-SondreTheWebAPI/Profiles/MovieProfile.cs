using MagicTord_N_SondreTheWebAPI.Models;
using AutoMapper;
using MagicTord_N_SondreTheWebAPI.Models.Dtos.Movies;

namespace MagicTord_N_SondreTheWebAPI.Profiles
{
    /// <summary>
    /// Mapperclass for the Movie table in the database.
    /// </summary>
    public class MovieProfile : Profile
    {

        /// <summary>
        /// Constructor for the Movie mapperclass
        /// </summary>
        public MovieProfile() {

            CreateMap<MoviePostDto, Movie>();

            CreateMap<Movie, MovieDto>()
                    .ForMember(dto => dto.Characters, opt => opt
                    .MapFrom(src => src.Characters));
        }
    }
}
