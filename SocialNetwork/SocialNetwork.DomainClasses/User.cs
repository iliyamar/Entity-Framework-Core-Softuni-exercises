
namespace Client2.DomainClasses
{
    using Client2.DomainClasses.CustomAttrValidators;
    using Social2.DomainClasses;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        [PasswordValidator]
        public string Password { get; set; }

        [Required]
        [EmailValidatorAttrtibute]
        public string Email { get; set; }

        [MaxLength(1024)]
        public byte[] ProfilePictue { get; set; }

        public DateTime? RegisteredOn { get; set; }

        public DateTime? LastTimeLoggedIn { get; set; }

        [AgeRange]
        public int? Age { get; set; }

        public bool IsDeleted { get; set; }

        public List<UserFriend> Friends { get; set; } = new List<UserFriend>();

        public List<UserFriend> FriendFriends { get; set; } = new List<UserFriend>();

        public List<Album> Albums { get; set; } = new List<Album>();

        public List<SharedAlbum> SharedAlbums { get; set; } = new List<SharedAlbum>();

       
     


        //        1.	Id – Primary Key(number in range[1, 231 - 1])
        //2.	Username – Text with length between 4 and 30 symbols.Required.
        //3.	Password – Required field.Text with length between 6 and 50 symbols.Should contain at least:
        //a.  1 lowercase letter
        //b.  1 uppercase letter
        //c.  1 digit
        //d.	1 special symbol (!, @, #, $, %, ^, &, *, (, ), _, +, <, >, ?)
        //4.	E-mail – Required field. Text is in format<user>@<host> where:
        //a.  <user> is a sequence of letters and digits, where '.', '-' and '_' can appear between them (they cannot appear at the beginning or at the end of the sequence). 
        //b.  <host> is a sequence of at least two words, separated by dots '.' (dots cannot appear at the beginning or at the end of the sequence)

        //5.	ProfilePicture – Image file with size maximum of 1MB
        //6.	RegisteredOn – Date and time of user registration
        //7.	LastTimeLoggedIn – Date and time of the last time the user logged in
        //8.	Age –  number in range[1, 120]
        //9.	IsDeleted – Shows whether the user is deleted or not

    }
}
