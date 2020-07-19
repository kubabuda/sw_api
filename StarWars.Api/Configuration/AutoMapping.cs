using AutoMapper;
using StarWars.BusinessLogic.Models;
using StarWars.DataAccess.Models;
using System.Linq;

namespace StarWars.Api.Configuration
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Character, SwCharacter>()
                .ForMember(dst => dst.Episodes, opt => opt.MapFrom(src => 
                    src.EpisodeCharacters.Select(e => e.Episode.Name)))
                .ForMember(dst => dst.Friends, conf => conf.MapFrom(src =>
                    src.Friends.Select(e => e.Friend.Name)));

            CreateMap<SwCharacter, Character>()
                .ForMember(dst => dst.EpisodeCharacters, opt => opt.Ignore())
                .ForMember(dst => dst.Friends, opt => opt.Ignore())
                .ForMember(dst => dst.FriendsOf, opt => opt.Ignore())
                .ForMember(dst => dst.Id, opt => opt.Ignore());
        }
    }
}
