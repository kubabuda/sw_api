namespace StarWars.DataAccess.Models
{
    public class CharacterFriendship
    {
        public int FriendId { get; set; }
        public Character Friend { get; set; }
        
        public int FriendOfId { get; set; }
        public Character FriendOf { get; set; }
    }
}
