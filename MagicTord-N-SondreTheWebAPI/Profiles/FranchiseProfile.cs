using MagicTord_N_SondreTheWebAPI.Models;
using AutoMapper;
using MagicTord_N_SondreTheWebAPI.Models.Dtos.Franchises;

namespace MagicTord_N_SondreTheWebAPI.Profiles
{
    /// <summary>
    /// Mapperclass for the Franchise table in the database.
    /// </summary>
    public class FranchiseProfile : Profile
    {

        /// <summary>
        /// Constructor for the Franchise mapperclass
        /// </summary>
        public FranchiseProfile() {

            CreateMap<FranchisePostDto, Franchise>();

            CreateMap<Franchise, FranchiseDto>()
                    .ForMember(dto => dto.Movies, opt => opt
                    .MapFrom(src => src.Movies));
        }
       

    }
}
