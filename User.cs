#nullable enable
using System;
using System.Collections.Generic;

namespace Library
{
    public enum Role
    {
        Admin,
        User
    }

    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Role UserRole { get; set; }
        public List<Book> BorrowedBooks { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
            UserName = string.Empty;
            Password = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
            UserRole = Role.User;
            BorrowedBooks = new List<Book>();
        }

        public User(string userName, string password, Role role)
        {
            Id = Guid.NewGuid();
            UserName = userName;
            Password = password;
            UserRole = role;
        }
    }
}