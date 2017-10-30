using Client2.DomainClasses;
using Microsoft.EntityFrameworkCore;
using Social2.Data;
using Social2.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Social2.Client
{
    class Program
    {
        public static void Main()
        {
            using (ContextSocialNetwork2 db = new ContextSocialNetwork2())
            {
                db.Database.Migrate();
                //1
                // SeedUsers(db);
                //SeedFriends(db);
                // PrintUsersWithFriendsCount(db);
                //PrintActiveUsersWithMoreThanFiveFriends(db);

                //2
                //SeedAlbums(db);
                //SeedPictures(db);
                //SeedAlbumsPictures(db);
                //PrintAlbumsOwnerAlbumTitlePictCount(db);
                //PrintPictTitleAlbumNamesOwners(db);
                //PrintUserAlbumsStatusPictTitlesPictPaths(db);

                //3
                //SeedTags(db);
                //SeedAlbumsTags(db);

                //4
                //PrintTitleAndOwnerForGivenTag(db);
                //PrintAllAlbumsWithMoreThanThreeTags(db);

                //5
                //SeedSharedAlbums(db);
                //PrintUsersFriendsAlbums(db);
                //PrintAlbumNamePeopleSharedToCountStatus(db);
                // PrintAlbumNamesAndPictureCount(db);

                //6
                //ChangeUpdateAlbumNames(db);
                //PrintAlbumsOwners(db);
                //PrintAlbumsNumbersOwnsNumbersAlbumsView(db);
                 PrintUsersViewersNamesAndPublicAlbumsCanView(db);



            }
        }

        private static void PrintUsersViewersNamesAndPublicAlbumsCanView(ContextSocialNetwork2 db)
        {

            var viewersIds = db
                .SharedAlbums
               // .Include(a=>a.User)
                .Where(a => a.Album.IsPublic == true)
                .GroupBy(x => x.SharedToId)
                .Select(u => new
                {
                    PublicAlbumsCount = u.Count(),
                    Name = u.Select(n=>n.SharedTo.Username).FirstOrDefault()

                })
                .OrderByDescending(s=>s.PublicAlbumsCount)
                .ToList();

            foreach (var viewer in viewersIds)
            {

                Console.WriteLine($"User Name -{viewer.Name}, Public albums count  user can view -{viewer.PublicAlbumsCount}");
            }


            //var viewersIds = db
            //    .SharedAlbums
            //    .OrderBy(u => u.SharedToId)
            //    .Where(a => a.Album.IsPublic == true)
            //    .Select(u => new
            //    {
            //        ViewerId= u.SharedToId,

            //    } )
            //    .ToList();

            //3.List all the users that are viewers of at least 1 album.Print the name of the user and the number of public albums they can view.
            Console.WriteLine();
        }









        private static void PrintAlbumsNumbersOwnsNumbersAlbumsView(ContextSocialNetwork2 db)
        {

            var givenUser = "User10";
            var givenUserId = db.Users.Where(n => n.Username == givenUser).Select(s => s.Id).FirstOrDefault();
            var countAsOwner = db
                .SharedAlbums
                .Where(o => o.UserId == givenUserId)
                .Select(c => c.AlbumId).Distinct().Count();

            var countAsViewer = db
                .SharedAlbums
                .Where(o => o.SharedToId == givenUserId)
                .Select(c => c.AlbumId).Distinct().Count();

            Console.WriteLine($"{givenUser} is Owner at {countAsOwner} albums and viewer at {countAsViewer} albums");


            //2.Find a user with a given name. Print the number of albums they are owners of and the number of albums they are viewers of.
        }


        //Change Album Names to make them Unique -originally seeded were not unique (this confuses a bit)
        private static void ChangeUpdateAlbumNames(ContextSocialNetwork2 db)
        {
            foreach (var album in db.Albums)
            {
                album.Name = album.Name + album.Id;
            }
            db.SaveChanges();
        }

        private static void PrintAlbumsOwners(ContextSocialNetwork2 db)
        {

            var result = db
                 .SharedAlbums
                 .Select(sa => new
                 {
                     AlbumId = sa.AlbumId,
                     AlbumName = sa.Album.Name,

                     OwnerId = sa.UserId,
                     OwnerName = sa.User.Username,

                     SharedToId = sa.SharedToId,
                     SharedToName = sa.SharedTo.Username,

                 })
                 .GroupBy(k => new { k.AlbumId, k.OwnerId, k.AlbumName, k.OwnerName }, k => new { k.AlbumName, k.OwnerName, k.SharedToName, k.SharedToId })
                 .ToList()
                 .OrderBy(k => k.Key.OwnerName)
                 .ThenByDescending(vk => vk.Select(n => n.SharedToName).Count())
                 .ToList();


            int counter = 0;


            foreach (var group in result)
            {
                counter++;
                // Console.WriteLine(counter);
                Console.WriteLine($"{group.Key.AlbumName} -Album Name");  /*With Id {group.Key.AlbumId}*/
                Console.WriteLine($"{group.Key.OwnerName} - Owner "); /*Owner With Id {group.Key.OwnerId}*/

                foreach (var res in group)
                {
                    Console.WriteLine($"{res.SharedToName} - Viewer ");
                }
                Console.WriteLine();


            }
            //foreach (var item in result)
            //{
            //    counter++;
            //    Console.WriteLine($"row#  {counter}    Album- {item.AlbumName} Id- {item.AlbumId}- , Owner {item.OwnerName} Id {item.OwnerId} , viewer {item.SharedToName} Id {item.SharedToId}");
            //}




            //1.List all albums with their users.Print the name of the album and the name of each user. For each user print information whether they are owner or viewer of the album. Order the albums by name of the owner(ascending) and then by the count of the viewers(descending).

        }

        private static void PrintAlbumNamesAndPictureCount(ContextSocialNetwork2 db)
        {
            //get random name from database
            Random random = new Random();
            var usersCount = db.Users.Select(s => s.Id).Count();
            var userName = db.Users.Find(random.Next(1, usersCount)).Username;

            var randomUserId = db.Users.Where(n => n.Username == userName).Select(i => i.Id);

            var result = db
                .Albums
                .Where(u => u.Users.Select(n => n.SharedToId).FirstOrDefault() == randomUserId.FirstOrDefault())
                .Select(a => new
                {
                    AlbumName = a.Name,
                    PicturesCount = a.Pictures.Count
                })
                .OrderByDescending(p => p.PicturesCount)
                .ThenBy(a => a.AlbumName)
                .ToList();

            Console.WriteLine($"User Name (random) {userName}");

            foreach (var album in result)
            {
                Console.WriteLine($"Albnum name {album.AlbumName} Pictures Count {album.PicturesCount}");


            }
            //3.List all the albums, shared with a user with a given name. Print the names of the albums and the count of the pictures in each album. Order the albums by the number of pictures(descending) and then by name(ascending).
        }

        private static void PrintAlbumNamePeopleSharedToCountStatus(ContextSocialNetwork2 db)
        {



            var result = db
                .Albums
                .Where(s => s.Users.Select(u => u.SharedToId).Count() > 2)
                .Select(a => new
                {
                    AlbumName = a.Name,
                    CountPeopleSharedTo = a.Users.Select(u => u.SharedToId).Count(),
                    Status = a.IsPublic ? "Public" : "Private"

                })
               .ToList();

            var groupedQuery = result.GroupBy(a => a.AlbumName)
                                     .Select(a => new
                                     {
                                         TotalAlbumsSum = a.Select(s => s.CountPeopleSharedTo).Sum(),
                                         AlbumName = a.Key,
                                         Status = a.Select(s => s.Status).FirstOrDefault()

                                     })
                                     .OrderByDescending(a => a.TotalAlbumsSum)
                                     .ThenBy(a => a.AlbumName);


            foreach (var album in groupedQuery)
            {
                Console.WriteLine($"{album.AlbumName} {album.TotalAlbumsSum} {album.Status}");
            }

            //2.List all albums, shared with more than 2 people.Print the name of the album, the number of people and the information whether the album is private static 
            // public or not.Order the albums by number of people(descending) and by name (ascending).



        }

        private static void PrintUsersFriendsAlbums(ContextSocialNetwork2 db)
        {

            var results = db
                .Users
                .Select(u => new
                {

                    UserName = u.Username,

                    FriendsAlbums = u.SharedAlbums
                                    .Select(fn => new
                                    {
                                        NamesOfFriends = db
                                            .Users
                                            .Where(uu => uu.Id == fn.SharedToId)
                                            .Select(n => n.Username)
                                            .FirstOrDefault(),

                                        SharedAlbumName = fn.Album.Name
                                    }).OrderBy(o => o.NamesOfFriends)
                })
                .OrderBy(o => o.UserName)
                .ToList();

            foreach (var user in results)
            {
                //ignore users didnt share albums
                if (user.UserName == null || user.UserName.Count() == 0)
                {
                    continue;
                }

                Console.WriteLine($"User Name {user.UserName}");

                foreach (var friednsAlbums in user.FriendsAlbums)
                {
                    Console.WriteLine($"Friend name {friednsAlbums.NamesOfFriends} Album name {friednsAlbums.SharedAlbumName}");

                }

            }
            //1.List all users with the users they have shared albums with. Print the name of the user, the names of their friends and the names of all albums shared with that friend. Order users by name(ascending).
        }

        private static void SeedSharedAlbums(ContextSocialNetwork2 db)
        {
            Random random = new Random();
            var userIds = db.Users.Select(s => s.Id).ToList();

            for (int i = 0; i < userIds.Count; i++)
            {
                var currentUserId = i + 1;

                //can share obly to friends
                var currentUserFriendIds = db.UsersFriends
                    .Where(u => u.FriendId == currentUserId)
                    .Select(u => u.FriendFriendId)
                    .ToList();
                //or to friendsfriends
                var currentUserFriendsOfFriends = db.UsersFriends
                    .Where(u => u.FriendFriendId == currentUserId)
                    .Select(u => u.FriendId)
                    .ToList();

                //all friendsIdsPossible to share with
                currentUserFriendIds.AddRange(currentUserFriendsOfFriends);

                var numberOfFriendsToShareAlbumWith = random.Next(1, currentUserFriendIds.Count);

                for (int j = 0; j < numberOfFriendsToShareAlbumWith; j++)
                {
                    var userIdShareWith = currentUserFriendIds[random.Next(0, currentUserFriendIds.Count)];
                    currentUserFriendIds.Remove(userIdShareWith);


                    var curentUserAlbumsIds = db.Albums
                        .Where(u => u.UserId == currentUserId)
                        .Select(a => a.Id).ToList();


                    //if (currentUserFriendIds.Count == 0 || currentUserFriendIds == null)
                    //{

                    //    continue;
                    //}


                    var AlbumsCountToShare = random.Next(0, curentUserAlbumsIds.Count); //random number of albums to share

                    for (int k = 0; k < AlbumsCountToShare; k++)  //add random album to table
                    {
                        var randomAlbumIdToShare = curentUserAlbumsIds[random.Next(0, curentUserAlbumsIds.Count)];

                        if (db.Users.Find(currentUserId).SharedAlbums.Any(sa => sa.AlbumId == randomAlbumIdToShare && sa.SharedToId == userIdShareWith))

                        {
                            k--;
                            continue;
                        }



                        db.Users
                            .Find(currentUserId)
                            .SharedAlbums.Add(new SharedAlbum
                            {
                                AlbumId = randomAlbumIdToShare,
                                SharedToId = userIdShareWith
                            });

                        db.SaveChanges();
                    }
                }


            }
        }

        private static void PrintAllAlbumsWithMoreThanThreeTags(ContextSocialNetwork2 db)
        {

            var result = db
                .Users
                .Where(u => u.Albums.Any(a => a.Tags.Count > 3))
                .Select(u => new
                {
                    UserName = u.Username,
                    AlbumTitle = u.Albums
                    .Where(a => a.Tags.Count > 3)
                    .Select(a => new
                    {
                        AlbumName = a.Name,
                        Tags = a.Tags.Select(t => t.Tag.Name)
                    })
                    .ToList()

                })
                .OrderByDescending(a => a.AlbumTitle.Count())
                .ThenByDescending(t => t.AlbumTitle.Sum(tt => tt.Tags.Count()))
                .ThenBy(n => n.UserName)
                .ToList();

            foreach (var user in result)
            {
                Console.WriteLine($"User name {user.UserName}");

                foreach (var album in user.AlbumTitle)
                {
                    Console.WriteLine($"--------------Album {album.AlbumName}");
                    Console.WriteLine(String.Join(",", album.Tags));



                }



            }



            //2.List all users that have albums with more than 3 tags with the albums that have more than 3 tags.Print the name of the user, the title of the album and list the tags for each album. Order the users by number of albums with more than 3 tags(descending), then by number of tags(descending) and then by name(ascending).
        }

        private static void PrintTitleAndOwnerForGivenTag(ContextSocialNetwork2 db)
        {
            Random random = new Random();
            var TagIdStart = db.Tags.Select(s => s.Id).OrderBy(t => t).FirstOrDefault();
            var TagIdEnd = db.Tags.Select(s => s.Id).OrderBy(t => t).LastOrDefault();

            var givenTagId = random.Next(TagIdStart, TagIdEnd + 1);

            var result = db
                .Albums
                .Where(a => a.Tags.Any(t => t.TagId == givenTagId))
                .OrderByDescending(a => a.Tags.Count()).ThenBy(a => a.Name)
                .Select(a => new
                {
                    a.Name,
                    a.User.Username
                })
                .ToList();


            Console.WriteLine($"Given Tag Id {givenTagId}");

            foreach (var album in result)
            {
                Console.WriteLine($"Album title {album.Name} Owner name {album.Username}");
            }



        }

        private static void SeedAlbumsTags(ContextSocialNetwork2 db)
        {
            Random random = new Random();
            //var albums = db.Albums.ToList();
            var tags = db.Tags.ToList();

            var albumIds = db.Albums.Select(a => a.Id).ToList();
            var tagIds = tags.Select(t => t.Id).ToList();

            for (int i = 0; i < albumIds.Count; i++)
            {
                var currentAlbumId = i + 1;

                var tagsCount = random.Next(0, 15);


                var varTagsMinId = db.Tags.Select(s => s.Id).OrderBy(x => x).FirstOrDefault();

                for (int j = varTagsMinId; j < varTagsMinId + tagsCount; j++)
                {
                    var currentTagId = random.Next(varTagsMinId, varTagsMinId + tagsCount);

                    if (db.Albums.Find(currentAlbumId).Id == currentAlbumId
                     && db.Albums.Find(currentAlbumId).Tags.Any(t => t.TagId == currentTagId))
                    {
                        j--;
                        continue;
                    }


                    db.Albums.Find(currentAlbumId)
                         .Tags
                         .Add(new AlbumTag
                         {
                             TagId = currentTagId
                         });

                    db.SaveChanges();


                }




            }




        }

        private static void SeedTags(ContextSocialNetwork2 db)
        {
            const int tagNumber = 50;

            List<Tag> tags = new List<Tag>();
            for (int i = 0; i < tagNumber; i++)
            {
                var tag = new Tag

                {
                    Name = TagTransofrmer.Transform($"Tag{i + 1}")

                };

                tags.Add(tag);
            }
            db.Tags.AddRange(tags);
            db.SaveChanges(); //saves tags
        }

        private static void PrintUserAlbumsStatusPictTitlesPictPaths(ContextSocialNetwork2 db)
        {
            var userId = 7;
            var result = db
               .Albums
               // .Any()
               .Where(a => a.UserId == userId)
               .Select(a => new
               {
                   Username = a.User.Username,
                   AlbumName = a.Name,
                   PictureTitleAndPath = a.Pictures.Select(p => new
                   {
                       PictTitle = p.Picture.Title,
                       PictPath = p.Picture.Path
                   }),

                   Status = a.IsPublic

               })
               .OrderBy(a => a.AlbumName);

            foreach (var album in result)
            {
                Console.WriteLine($"{album.Username} {album.AlbumName} ");
                if (album.Status)
                {
                    foreach (var item in album.PictureTitleAndPath)
                    {
                        Console.WriteLine($"{item.PictTitle} {item.PictPath}");
                    }
                }
                else
                {
                    Console.WriteLine("Private content!");
                }

            }

            //3.List all albums of a given user id with information about the pictures in each album. Print the name of the user. If the album is public, print its name and the titles of the pictures in it with the path for each picture.If the album is not public, print only the name of the album and the message "Private content!". Order the albums by name(ascending).

        }

        private static void PrintPictTitleAlbumNamesOwners(ContextSocialNetwork2 db)
        {

            var result = db
                .Pictures
                .Where(p => p.Albums.Count > 2)
                .Select(p => new
                {
                    PictureTitle = p.Title,
                    AlbumNameAndUser = p.Albums.Select(a =>
                                            new
                                            {
                                                AlbumName = a.Album.Name,
                                                UserName = a.Album.User.Username
                                            })

                }).ToList()
                 .OrderByDescending(a => a.AlbumNameAndUser.Select(b => b.AlbumName).Count())
                 .ThenBy(a => a.PictureTitle).ToList();

            foreach (var pict in result)
            {
                Console.WriteLine($"Picture title {pict.PictureTitle} ");
                foreach (var alb in pict.AlbumNameAndUser)
                {
                    Console.WriteLine($"..........Album name {alb.AlbumName}  Owner {alb.UserName}");

                }

            }

            //2.List the pictures, that are included in more than 2 albums.Print the picture title, the names of the albums and the names of the owners of the albums.Order pictures by number of albums(descending) and then by the title(ascending).

        }

        private static void PrintAlbumsOwnerAlbumTitlePictCount(ContextSocialNetwork2 db)
        {
            Random random = new Random();
            var result =
                db.Albums
                .Select(a => new
                {
                    a.Name,
                    OwnerName = a.User.Username,
                    PicturesCount = a.Pictures.Count
                })
                .OrderByDescending(p => p.PicturesCount)
                .ThenBy(o => o.OwnerName)
                .ToList();

            foreach (var album in result)
            {
                Console.WriteLine($"{album.Name} {album.OwnerName} {album.PicturesCount}");
            }

            //1.List all albums with the name of their owner and the count of their pictures. Print album title, owner name and pictures count.Order them by the number of pictures(descending) and then by the name of the owner(ascending).
        }

        private static void SeedAlbumsPictures(ContextSocialNetwork2 db)
        {
            var radndom = new Random();
            var albumIds = db.Albums.Select(s => s.Id).ToList();
            var picturesIds = db.Pictures.Select(p => p.Id).ToList();

            for (int i = 1; i <= albumIds.Count(); i++)
            {
                var albumIdToAdd = i;
                var picturesPerAlbumCount = radndom.Next(1, 50);


                for (int j = 0; j < picturesPerAlbumCount; j++)
                {
                    var pictureIdToAdd = radndom.Next(1, picturesIds.Count);

                    //if value pair exists
                    if (db.Albums.Find(albumIdToAdd).Pictures.Any(p => p.PictureId == pictureIdToAdd))
                    {
                        j--;
                        continue;

                    }

                    db.Albums.Find(albumIdToAdd)
                        .Pictures
                        .Add(new AlbumPicture
                        { PictureId = pictureIdToAdd });

                    db.SaveChanges();

                };
            }
        }

        private static void SeedPictures(ContextSocialNetwork2 db)
        {
            const int picturesCount = 500;
            Random random = new Random();

            for (int i = 0; i < picturesCount; i++)
            {
                var picture = new Picture
                {
                    Caption = $"Caption{random.Next(1, 2345)}-{i + 1}",
                    Path = $"c:\\MyPicts\\Folder{random.Next(0, 21)}",
                    Title = $"Picture {i + 1}"

                };

                db.Pictures.Add(picture);
            }

            db.SaveChanges();


        }

        private static void SeedAlbums(ContextSocialNetwork2 db)
        {
            var random = new Random();
            var userIds = db.Users.Select(u => u.Id).ToList();
            for (int i = 1; i <= userIds.Count - 1; i++)
            {
                var currentUserId = i;
                var numberOfAlbums = random.Next(0, 10);

                for (int j = 0; j < numberOfAlbums; j++)
                {

                    var album = new Album
                    {
                        BackgroundColor = random.Next(300, 500).ToString(),
                        IsPublic = random.Next(0, 2) == 1 ? true : false,
                        Name = $"Album{j}",
                        UserId = currentUserId
                    };

                    db.Albums.Add(album);
                }




            }

            db.SaveChanges();
        }

        private static void PrintActiveUsersWithMoreThanFiveFriends(ContextSocialNetwork2 db)
        {
            var result = db.
                Users.Where(x => x.IsDeleted == false && (x.FriendFriends.Count + x.Friends.Count) > 5)
                .OrderBy(x => x.RegisteredOn)
                .ThenByDescending(x => x.Friends.Count + x.FriendFriends.Count)
                .Select(s => new
                {
                    s.Username,
                    FriedndsNumber = s.Friends.Count + s.FriendFriends.Count,
                    DaysInNetwork = DateTime.Now.Subtract((DateTime)s.RegisteredOn).TotalDays
                }).ToList();

            foreach (var user in result)
            {
                Console.WriteLine($"{user.Username} user- {user.FriedndsNumber} friends {user.DaysInNetwork} days");
            }
            //2.List all active users(not deleted ones) with more than 5 friends.Order users by registration date(ascending) and by friends count(descending). Print the name, the number of friends and the period(in days) the user has been part of the network(the difference between current date and the registration date).
        }

        private static void PrintUsersWithFriendsCount(ContextSocialNetwork2 db)
        {

            var result = db.Users
                .Select(u => new
                {
                    Name = u.Username,
                    FriendsNumber = u.FriendFriends.Count + u.Friends.Count,
                    Status = u.IsDeleted == true ? "Active " : "Inactive",


                })
                .OrderByDescending(f => f.FriendsNumber)
                .ThenBy(n => n.Name).ToList();

            foreach (var user in result)
            {
                Console.WriteLine($"Name {user.Name} Friends Count {user.FriendsNumber} Status {user.Status}");

            }


            //1.List all users with the count of their friends. Order them by friends count(descending) and then by name(ascending). Print the name, the number of friends and the status of each user. If the user is not deleted, their status is Active, otherwise their status is Inactive.
        }

        private static void SeedFriends(ContextSocialNetwork2 db)
        {
            Random random = new Random();

            var userIds = db.Users.Select(u => u.Id).ToList();

            //add friends to each user

            for (int i = 0; i < userIds.Count; i++)

            {
                var currentFriendId = i + 1;
                int friendsNumber = random.Next(0, 7);

                for (int j = 0; j < friendsNumber; j++)
                {
                    var friendIdToAdd = random.Next(1, userIds.Count);
                    bool isValid = true;



                    //user can not be friend with himself
                    if (currentFriendId == friendIdToAdd)
                    {
                        isValid = false;
                    }

                    //reverse friend check
                    if (db.UsersFriends.Any(f => f.FriendId == friendIdToAdd && f.FriendFriendId == currentFriendId))
                    {
                        isValid = false;
                    }


                    //if they are already friends;
                    if (db.UsersFriends.Any(f => f.FriendId == currentFriendId && f.FriendFriendId == friendIdToAdd))
                    {
                        isValid = false;
                    }

                    //reverse friend check

                    if (!isValid)
                    {
                        j--;
                        isValid = true;
                        continue;
                    }

                    else
                    {

                        db.UsersFriends.Add(new UserFriend
                        {
                            FriendId = currentFriendId,
                            FriendFriendId = friendIdToAdd

                        });


                        db.SaveChanges();
                    }

                }
            }

        }

        private static void SeedUsers(ContextSocialNetwork2 db)
        {
            const int usersCount = 25;
            Random random = new Random();

            List<User> users = new List<User>();

            for (int i = 1; i <= usersCount; i++)
            {
                User user = new User
                {
                    Username = $"User{i}",
                    Age = random.Next(16, 100),
                    Email = $"user{i}_mail@abv.bg",
                    IsDeleted = random.Next(0, 2) == 1 ? true : false,
                    RegisteredOn = new DateTime(random.Next(2005, 2017), random.Next(1, 13), random.Next(1, 29)),
                    LastTimeLoggedIn = DateTime.Now.AddDays(-random.Next(1, 300)),
                    Password = $"paSs{random.Next(100, 1000)}w!ord",
                    ProfilePictue = Encoding.Unicode.GetBytes($"cweqwfcqwfcwq{random.Next(1, 1000000)}")
                };

                users.Add(user);
            }
            db.AddRange(users);
            db.SaveChanges();

        }
    }

}