namespace StarWars.DataAccess.Models
{
    public class CharacterFriendship
    {
        public int FriendId { get; set; }
        public int FriendOfId { get; set; }
        public virtual Character Friend { get; set; }
        public virtual Character FriendOf { get; set; }
    }
}
