namespace StarWars.DataAccess.Models
{
    public class SwCharacterFriendship
    {
        public int FriendId { get; set; }
        public int FriendOfId { get; set; }
        public virtual SwCharacter Friend { get; set; }
        public virtual SwCharacter FriendOf { get; set; }
    }
}
