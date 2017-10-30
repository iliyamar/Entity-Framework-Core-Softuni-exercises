using Client2.DomainClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Social2.DomainClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Social2.Data
{
    public class ContextSocialNetwork2 : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<UserFriend> UsersFriends { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<SharedAlbum> SharedAlbums { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Sn2;Trusted_connection=True");

        }

        

        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
 




            



            //Validator.ValidateObject();
            #region SharedAlbums ManyToMany UsersAlbums
            modelBuilder
                .Entity<SharedAlbum>()
                .HasKey(k => new { k.AlbumId, k.UserId,k.SharedToId});

            modelBuilder
                .Entity<SharedAlbum>()
                .HasOne(a => a.Album)
                .WithMany(u => u.Users)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<SharedAlbum>()
                .HasOne(u => u.User)
                .WithMany(a => a.SharedAlbums)
                .OnDelete(DeleteBehavior.Restrict);
#endregion           
            //modelBuilder
            //   .Entity<SharedAlbum>()
            //   .HasOne(u => u.SharedTo)
            //   .WithOne(a => a.)
            //   .OnDelete(DeleteBehavior.Restrict);



            #region Album-Tag Many to Many
            modelBuilder
            .Entity<AlbumTag>()
            .HasKey(k => new { k.AlbumId, k.TagId });

            modelBuilder
                .Entity<AlbumTag>()
                .HasOne(a => a.Album)
                .WithMany(t => t.Tags)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<AlbumTag>()
                .HasOne(a => a.Tag)
                .WithMany(l => l.Albums)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Many to many Album Pictures
            modelBuilder
                .Entity<AlbumPicture>()
                .HasKey(k => new { k.AlbumId, k.PictureId });

            modelBuilder
                .Entity<AlbumPicture>()
                .HasOne(a => a.Album)
                .WithMany(p => p.Pictures )
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<AlbumPicture>()
                .HasOne(p => p.Picture)
                .WithMany(a => a.Albums)
                .OnDelete(DeleteBehavior.Restrict);
#endregion


            #region Many To Many USER-FRIENDS DEFINITION
            modelBuilder.Entity<UserFriend>()
                .HasKey(k => new
                {
                    k.FriendId,
                    k.FriendFriendId
                });

            modelBuilder.Entity<UserFriend>()
                .HasOne(u => u.Friend)
                .WithMany(f => f.Friends)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserFriend>()
                .HasOne(f => f.FriendFriend)
                .WithMany(u => u.FriendFriends)
                .OnDelete(DeleteBehavior.Restrict);
#endregion

        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {


            var serviceProvider = this.GetService<IServiceProvider>();
            var items = new Dictionary<object, object>();

            foreach (var entry in this.ChangeTracker.Entries().Where(e => (e.State == EntityState.Added) || (e.State == EntityState.Modified)))

            {
                var entity = entry.Entity;
                var context = new ValidationContext(entity, serviceProvider, items);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(entity, context, results, true) == false)
                {
                    foreach (var result in results)
                    {
                        if (result != ValidationResult.Success)
                        {
                            throw new ValidationException(result.ErrorMessage);
                        }
                    }
                }
            }

            return base.SaveChanges(acceptAllChangesOnSuccess);


        }


    }
}
