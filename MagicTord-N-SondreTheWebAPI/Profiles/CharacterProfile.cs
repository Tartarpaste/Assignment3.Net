using MagicTord_N_SondreTheWebAPI.Models;
using MagicTord_N_SondreTheWebAPI.Models.Dtos.Characters;
using AutoMapper.QueryableExtensions;
using System.Linq;

using AutoMapper;
using MagicTord_N_SondreTheWebAPI.Models.Dtos.Movies;

namespace MagicTord_N_SondreTheWebAPI.Profiles
{
    public class CharacterProfile : Profile
    {
  
        public CharacterProfile() {

            CreateMap<CharacterPostDto, Character>();

            CreateMap<Character, CharacterDto>()
                    .ForMember(dto => dto.Movies, opt => opt
                    .MapFrom(src => src.Movies));

        }


    }
}
