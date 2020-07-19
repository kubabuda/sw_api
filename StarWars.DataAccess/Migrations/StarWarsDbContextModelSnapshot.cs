﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StarWars.DataAccess;

namespace StarWars.DataAccess.Migrations
{
    [DbContext(typeof(StarWarsDbContext))]
    partial class StarWarsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6");

            modelBuilder.Entity("StarWars.DataAccess.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Planet")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("StarWars.DataAccess.Models.CharacterFriendship", b =>
                {
                    b.Property<int>("FriendId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FriendOfId")
                        .HasColumnType("INTEGER");

                    b.HasKey("FriendId", "FriendOfId");

                    b.HasIndex("FriendOfId");

                    b.ToTable("CharacterFriendship");
                });

            modelBuilder.Entity("StarWars.DataAccess.Models.Episode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("StarWars.DataAccess.Models.EpisodeCharacter", b =>
                {
                    b.Property<int>("EpisodeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CharacterId")
                        .HasColumnType("INTEGER");

                    b.HasKey("EpisodeId", "CharacterId");

                    b.HasIndex("CharacterId");

                    b.ToTable("EpisodeCharacter");
                });

            modelBuilder.Entity("StarWars.DataAccess.Models.CharacterFriendship", b =>
                {
                    b.HasOne("StarWars.DataAccess.Models.Character", "Friend")
                        .WithMany("FriendsOf")
                        .HasForeignKey("FriendId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StarWars.DataAccess.Models.Character", "FriendOf")
                        .WithMany("Friends")
                        .HasForeignKey("FriendOfId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StarWars.DataAccess.Models.EpisodeCharacter", b =>
                {
                    b.HasOne("StarWars.DataAccess.Models.Character", "Character")
                        .WithMany("EpisodeCharacters")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StarWars.DataAccess.Models.Episode", "Episode")
                        .WithMany("EpisodeCharacters")
                        .HasForeignKey("EpisodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
