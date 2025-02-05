using System;
using System.Collections.Generic;

namespace Library
{
    public class LibraryService
    {
        private List<Book> books = new List<Book>();
        private List<User> users = new List<User>();
        public User CurrentUser { get; set; }

        public void AddBook(string title, string author, string genre, int year)
        {
            foreach (var book in books)
            {
                if (book.Title == title && book.Author == author)
                {
                    Console.WriteLine("Эта книга уже добавлена.");
                    return;
                }
            }

            var newBook = new Book
            {
                Id = Guid.NewGuid(),
                Title = title,
                Author = author,
                Genre = genre,
                Year = year,
                IsAvailable = true
            };
            books.Add(newBook);
            Console.WriteLine($"Книга '{title}' успешно добавлена в библиотеку.");
        }

        public List<Book> SearchBooks(string title, string author, string genre)
        {
            List<Book> result = new List<Book>();

            foreach (var book in books)
            {
                bool matches = true;

                if (!string.IsNullOrEmpty(title) && !book.Title.ToLower().Contains(title.ToLower()))
                {
                    matches = false;
                }
                if (!string.IsNullOrEmpty(author) && !book.Author.ToLower().Contains(author.ToLower()))
                {
                    matches = false;
                }
                if (!string.IsNullOrEmpty(genre) && !book.Genre.ToLower().Contains(genre.ToLower()))
                {
                    matches = false;
                }

                if (matches)
                {
                    result.Add(book);
                }
            }

            return result;
        }

        public bool IssueBook(string bookTitle, string userName)
        {
            User user = null;
            Book book = null;

            foreach (var u in users)
            {
                if (u.UserName == userName)
                {
                    user = u;
                    break;
                }
            }

            foreach (var b in books)
            {
                if (b.Title == bookTitle)
                {
                    book = b;
                    break;
                }
            }

            if (book == null)
            {
                Console.WriteLine("Книга не найдена.");
                return false;
            }

            if (user == null)
            {
                Console.WriteLine("Пользователь не найден.");
                return false;
            }

            if (!book.IsAvailable)
            {
                Console.WriteLine("Книга уже занята.");
                return false;
            }

            book.IsAvailable = false;
            book.Borrower = user;
            Console.WriteLine($"Книга '{book.Title}' выдана пользователю {user.UserName}.");
            return true;
        }

        public bool ReturnBook(string bookTitle, string userName)
        {
            User user = null;
            Book book = null;

            foreach (var u in users)
            {
                if (u.UserName == userName)
                {
                    user = u;
                    break;
                }
            }

            foreach (var b in books)
            {
                if (b.Title == bookTitle)
                {
                    book = b;
                    break;
                }
            }

            if (book == null || user == null || book.Borrower == null || book.Borrower.UserName != user.UserName)
            {
                Console.WriteLine("Неверные данные. Книга или пользователь не найдены, либо книга не была взята.");
                return false;
            }

            book.IsAvailable = true;
            book.Borrower = null;
            Console.WriteLine($"Книга '{book.Title}' возвращена пользователем {user.UserName}.");
            return true;
        }

        public List<User> GetUsers()
        {
            if (!IsAdmin())
            {
                Console.WriteLine("Только администратор может просматривать список пользователей.");
                return new List<User>();
            }

            return users;
        }

        public bool DeleteUser(string userName)
        {
            if (!IsAdmin())
            {
                Console.WriteLine("Только администратор может удалять пользователей.");
                return false;
            }

            foreach (var user in users)
            {
                if (user.UserName == userName)
                {
                    users.Remove(user);
                    Console.WriteLine($"Пользователь {user.UserName} удалён.");
                    return true;
                }
            }

            Console.WriteLine("Пользователь не найден.");
            return false;
        }

        public User Login(string userName, string password)
        {
            foreach (var user in users)
            {
                if (user.UserName == userName && user.Password == password)
                {
                    return user;
                }
            }

            return null;
        }

        public bool IsAdmin()
        {
            return CurrentUser != null && CurrentUser.UserRole == Role.Admin;
        }

        public bool Registration(string userName, string password)
        {
            foreach (var user in users)
            {
                if (user.UserName == userName)
                {
                    Console.WriteLine("Имя пользователя уже занято.");
                    return false;
                }
            }

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                UserName = userName,
                Password = password,
                UserRole = Role.User
            };

            users.Add(newUser);
            Console.WriteLine("Регистрация успешна!");
            return true;
        }
    }
}
