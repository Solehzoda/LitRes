using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    public class LibraryService 
    {
        public List<User> Users { get; set; }
        public List<Book> Books { get; set; }

        public LibraryService()
        {
            Users = new List<User>();
            Books = new List<Book>();
        }

        public void RegisterUser(string username, string password)
        {
            if (Users.Any(u => u.Username == username))
            {
                Console.WriteLine("Пользователь с таким именем уже существует");
                return;
            }
            Users.Add(new User(username, password));
        }
        


    }
}