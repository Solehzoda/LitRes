#nullable enable
using System;
using System.Collections.Generic;

namespace Library
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        #nullable disable
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Role UserRole { get; set; }
        #nullable disable
        public List<Book> BorrowedBooks { get; set; }


        public User()
        {
            Id = Guid.NewGuid();
            BorrowedBooks = new List<Book>();
            Username = string.Empty;
            Password = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
            UserRole = Role.User;
            
            
        }

        public User(string admin, string adminPassword)
        {
            admin = admin.ToUpper();
            adminPassword = adminPassword.ToUpper();
        }
    }

    public class Role
    {
        public static Role User { get; set; }
    }
}