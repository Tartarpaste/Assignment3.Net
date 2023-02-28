using MagicTord_N_SondreTheWebAPI.Models;
using MagicTord_N_SondreTheWebAPI.Models.Dtos.Characters;
using AutoMapper;
using MagicTord_N_SondreTheWebAPI.Models.Dtos.Franchises;

namespace MagicTord_N_SondreTheWebAPI.Profiles
{
    public class FranchiseProfile : Profile
    {
  
        public FranchiseProfile() {

            CreateMap<FranchisePostDto, Franchise>();

            CreateMap<Character, CharacterDto>()
                    .ForMember(dto => dto.Movies, opt => opt
                    .MapFrom(p => p.Movies.Select(s => s.MovieID).ToList()));
        }
       

    }
}
