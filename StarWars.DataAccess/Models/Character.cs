using System.Collections.Generic;

namespace StarWars.DataAccess.Models
{
    public class Character // todo change name which is the same name as domain model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Planet { get; set; }
        
        public List<EpisodeCharacter> EpisodeCharacters { get; set; }
        //public List<CharacterFriendship> Friends { get; set; }
        //public List<CharacterFriendship> FriendsOf { get; set; }
    }
}
