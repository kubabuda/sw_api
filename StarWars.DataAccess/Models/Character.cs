using System.Collections.Generic;

namespace StarWars.DataAccess.Models
{
    public class Character // todo change name which is the same name as domain model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Planet { get; set; }
        
        public virtual ICollection<EpisodeCharacter> Episodes { get; set; }
        public virtual ICollection<CharacterFriendship> Friends { get; set; }
        public virtual ICollection<CharacterFriendship> FriendsOf { get; set; }
    }
}
