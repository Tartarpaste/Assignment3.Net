using MagicTord_N_SondreTheWebAPI.Models;
using MagicTord_N_SondreTheWebAPI.Models.Dtos.Characters;
using AutoMapper;

namespace MagicTord_N_SondreTheWebAPI.Profiles
{
    /// <summary>
    /// Mapperclass for the Character table in the database.
    /// </summary>
    public class CharacterProfile : Profile
    {
        /// <summary>
        /// Constructor for the character mapperclass
        /// </summary>
        public CharacterProfile() {

            CreateMap<CharacterPostDto, Character>();

            CreateMap<Character, CharacterDto>()
                    .ForMember(dto => dto.Movies, opt => opt
                    .MapFrom(src => src.Movies));

        }
    }
}
