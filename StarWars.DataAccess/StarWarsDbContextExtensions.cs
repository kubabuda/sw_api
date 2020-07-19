using StarWars.DataAccess.Models;
using System.Collections.Generic;

namespace StarWars.DataAccess
{
    public static class StarWarsDbContextExtensions
    {
        public static void Seed(this StarWarsDbContext dbContext)
        {
            var newhope = new Episode { Id = 4, Name = "NEWHOPE" };
            var empire = new Episode { Id = 5, Name = "EMPIRE" };
            var jedi = new Episode { Id = 6, Name = "JEDI" };

            var luke_skywalker = new Character
            {
                Id = 1,
                Name = "Luke Skywalker"
            };
            var darth_vader = new Character
            {
                Id = 2,
                Name = "Darth Vader"
            };
            var han_solo = new Character
            {
                Id = 3,
                Name = "Han Solo"
            };
            var leia_organa = new Character
            {
                Id = 4,
                Name = "Leia Organa",
                Planet = "Alderaan"
            };
            var wilhuff_tarkin = new Character
            {
                Id = 5,
                Name = "Wilhuff Tarkin"
            };
            var c_3p0 = new Character
            {
                Id = 6,
                Name = "C-3PO"
            };
            var r2_d2 = new Character
            {
                Id = 7,
                Name = "R2-D2"
            };

            luke_skywalker.EpisodeCharacters = new List<EpisodeCharacter> {
                new EpisodeCharacter { EpisodeId = newhope.Id, CharacterId = luke_skywalker.Id },
                new EpisodeCharacter { EpisodeId = empire.Id, CharacterId = luke_skywalker.Id },
                new EpisodeCharacter { EpisodeId = jedi.Id, CharacterId = luke_skywalker.Id }
            };

            darth_vader.EpisodeCharacters = new List<EpisodeCharacter> {
            new EpisodeCharacter { EpisodeId = newhope.Id, CharacterId = darth_vader.Id },
                new EpisodeCharacter { EpisodeId = empire.Id, CharacterId = darth_vader.Id },
                new EpisodeCharacter { EpisodeId = jedi.Id, CharacterId = darth_vader.Id }
            };

            han_solo.EpisodeCharacters = new List<EpisodeCharacter> {
                new EpisodeCharacter { EpisodeId = newhope.Id, CharacterId = han_solo.Id },
                new EpisodeCharacter { EpisodeId = empire.Id, CharacterId = han_solo.Id },
                new EpisodeCharacter { EpisodeId = jedi.Id, CharacterId = han_solo.Id }
            };

            leia_organa.EpisodeCharacters = new List<EpisodeCharacter> {
                new EpisodeCharacter { EpisodeId = newhope.Id, CharacterId = leia_organa.Id },
                new EpisodeCharacter { EpisodeId = empire.Id, CharacterId = leia_organa.Id },
                new EpisodeCharacter { EpisodeId = jedi.Id, CharacterId = leia_organa.Id }
            };

            wilhuff_tarkin.EpisodeCharacters = new List<EpisodeCharacter> {
                new EpisodeCharacter { EpisodeId = newhope.Id, CharacterId = wilhuff_tarkin.Id }
            };

            c_3p0.EpisodeCharacters = new List<EpisodeCharacter> {
                new EpisodeCharacter { EpisodeId = newhope.Id, CharacterId = c_3p0.Id },
                new EpisodeCharacter { EpisodeId = empire.Id, CharacterId = c_3p0.Id },
                new EpisodeCharacter { EpisodeId = jedi.Id, CharacterId = c_3p0.Id }
            };

            r2_d2.EpisodeCharacters = new List<EpisodeCharacter> {
                new EpisodeCharacter { EpisodeId = newhope.Id, CharacterId = r2_d2.Id },
                new EpisodeCharacter { EpisodeId = empire.Id, CharacterId = r2_d2.Id },
                new EpisodeCharacter { EpisodeId = jedi.Id, CharacterId = r2_d2.Id }
            };

            luke_skywalker.Friends = new List<CharacterFriendship>
            {
                new CharacterFriendship { FriendId = han_solo.Id, FriendOfId = luke_skywalker.Id },
                new CharacterFriendship { FriendId = leia_organa.Id, FriendOfId = luke_skywalker.Id },
                new CharacterFriendship { FriendId = c_3p0.Id, FriendOfId = luke_skywalker.Id },
                new CharacterFriendship { FriendId = r2_d2.Id, FriendOfId = luke_skywalker.Id }
            };
            darth_vader.Friends = new List<CharacterFriendship>
            {
                new CharacterFriendship { FriendId = wilhuff_tarkin.Id, FriendOfId = darth_vader.Id }
            };
            han_solo.Friends = new List<CharacterFriendship>
            {
                new CharacterFriendship { FriendId = luke_skywalker.Id, FriendOfId = han_solo.Id },
                new CharacterFriendship { FriendId = leia_organa.Id, FriendOfId = han_solo.Id },
                new CharacterFriendship { FriendId = r2_d2.Id, FriendOfId = han_solo.Id }
            };
            leia_organa.Friends = new List<CharacterFriendship>
            {
                new CharacterFriendship { FriendId = luke_skywalker.Id, FriendOfId = leia_organa.Id },
                new CharacterFriendship { FriendId = han_solo.Id, FriendOfId = leia_organa.Id },
                new CharacterFriendship { FriendId = c_3p0.Id, FriendOfId = leia_organa.Id },
                new CharacterFriendship { FriendId = r2_d2.Id, FriendOfId = leia_organa.Id }
            };
            wilhuff_tarkin.Friends = new List<CharacterFriendship>
            {
                new CharacterFriendship { FriendId = darth_vader.Id, FriendOfId = wilhuff_tarkin.Id }
            };
            c_3p0.Friends = new List<CharacterFriendship>
            {
                new CharacterFriendship { FriendId = luke_skywalker.Id, FriendOfId = c_3p0.Id },
                new CharacterFriendship { FriendId = han_solo.Id, FriendOfId = c_3p0.Id },
                new CharacterFriendship { FriendId = leia_organa.Id, FriendOfId = c_3p0.Id },
                new CharacterFriendship { FriendId = r2_d2.Id, FriendOfId = c_3p0.Id }
            };
            r2_d2.Friends = new List<CharacterFriendship>
            {
                new CharacterFriendship { FriendId = luke_skywalker.Id, FriendOfId = r2_d2.Id },
                new CharacterFriendship { FriendId = han_solo.Id, FriendOfId = r2_d2.Id },
                new CharacterFriendship { FriendId = leia_organa.Id, FriendOfId = r2_d2.Id }
            };

            dbContext.Episodes.AddRange(newhope, empire, jedi);
            dbContext.Characters.AddRange(
                luke_skywalker,
                darth_vader,
                han_solo,
                leia_organa,
                wilhuff_tarkin,
                c_3p0,
                r2_d2);
            dbContext.SaveChanges();
        }
    }
}
