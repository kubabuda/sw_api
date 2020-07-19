using System.Collections.Generic;

namespace StarWars.DataAccess.Models
{
    public class SwCharacter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Planet { get; set; }
        
        public virtual ICollection<SwEpisodeCharacter> Episodes { get; set; }
        public virtual ICollection<SwCharacterFriendship> Friends { get; set; }
        public virtual ICollection<SwCharacterFriendship> FriendsOf { get; set; }
    }
}
