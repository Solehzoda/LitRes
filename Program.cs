using System;
using System.Collections.Generic;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            LibraryService libraryService = new LibraryService();
            User admin = new User
            {
                UserName = "Admin",
                Password = "admin",
                UserRole = Role.Admin
            };
            libraryService.Registration(admin.UserName, admin.Password);

            User currentUser = null;

            while (true)
            {
                if (currentUser == null)
                {
                    Console.Clear();
                    Console.WriteLine("=== Авторизация ===");
                    Console.Write("Введите логин: ");
                    string userName = Console.ReadLine();
                    Console.Write("Введите пароль: ");
                    string password = Console.ReadLine();

                    currentUser = libraryService.Login(userName, password);

                    if (currentUser == null)
                    {
                        Console.WriteLine("Неверный логин или пароль, попробуйте снова.");
                        Console.ReadKey();
                        continue;
                    }
                    else
                    {
                        libraryService.CurrentUser = currentUser;
                    }
                }

                if (libraryService.IsAdmin())
                {
                    Console.Clear();
                    Console.WriteLine("=== Панель администратора ===");
                    Console.WriteLine("Список пользователей:");
                    var users = libraryService.GetUsers();
                    foreach (var user in users)
                    {
                        Console.WriteLine($"Логин: {user.UserName}, Роль: {user.UserRole}");
                    }

                    Console.Write("Введите имя пользователя для удаления (или нажмите Enter для пропуска): ");
                    string deleteUsername = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(deleteUsername))
                    {
                        if (libraryService.DeleteUser(deleteUsername))
                        {
                            Console.WriteLine("Пользователь успешно удалён.");
                        }
                        else
                        {
                            Console.WriteLine("Ошибка удаления пользователя.");
                        }
                        Console.ReadKey();
                    }
                }
                
                Console.Clear();
                Console.WriteLine("=== Меню ===");
                Console.WriteLine("1. Регистрация");
                Console.WriteLine("2. Добавить книгу");
                Console.WriteLine("3. Поиск книги");
                Console.WriteLine("4. Выдать книгу");
                Console.WriteLine("5. Вернуть книгу");
                Console.WriteLine("6. Выйти из программы");
                Console.Write("Выберите параметр: ");

                string input = Console.ReadLine();
                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Ошибка ввода. Попробуйте снова.");
                    Console.ReadKey();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Write("Введите имя пользователя: ");
                        string regUsername = Console.ReadLine();
                        Console.Write("Введите пароль: ");
                        string regPassword = Console.ReadLine();
                        if (libraryService.Registration(regUsername, regPassword))
                        {
                            Console.WriteLine("Регистрация успешна!");
                        }
                        else
                        {
                            Console.WriteLine("Ошибка регистрации.");
                        }
                        Console.ReadKey();
                        break;

                    case 2:
                        Console.Write("Введите название книги: ");
                        string bookTitle = Console.ReadLine();
                        Console.Write("Введите автора книги: ");
                        string bookAuthor = Console.ReadLine();
                        Console.Write("Введите жанр книги: ");
                        string bookGenre = Console.ReadLine();
                        Console.Write("Введите год выпуска книги: ");
                        if (!int.TryParse(Console.ReadLine(), out int bookYear))
                        {
                            Console.WriteLine("Ошибка: некорректный ввод года.");
                            Console.ReadKey();
                            break;
                        }

                        libraryService.AddBook(bookTitle, bookAuthor, bookGenre, bookYear);
                        Console.ReadKey();
                        break;

                    case 3:
                        Console.Write("Введите название книги: ");
                        string searchTitle = Console.ReadLine();
                        Console.Write("Введите автора книги: ");
                        string searchAuthor = Console.ReadLine();
                        Console.Write("Введите жанр книги: ");
                        string searchGenre = Console.ReadLine();

                        var books = libraryService.SearchBooks(searchTitle, searchAuthor, searchGenre);
                        foreach (var book in books)
                        {
                            Console.WriteLine($"Название: {book.Title}, Автор: {book.Author}, Жанр: {book.Genre}, Год: {book.Year}, Доступна: {book.IsAvailable}");
                        }
                        Console.ReadKey();
                        break;

                    case 4:
                        Console.Write("Введите имя пользователя: ");
                        string issueUserName = Console.ReadLine();
                        Console.Write("Введите название книги: ");
                        string issueBookTitle = Console.ReadLine();

                        if (libraryService.IssueBook(issueBookTitle, issueUserName))
                        {
                            Console.WriteLine("Книга успешно выдана.");
                        }
                        else
                        {
                            Console.WriteLine("Ошибка выдачи книги.");
                        }
                        Console.ReadKey();
                        break;

                    case 5:
                        Console.Write("Введите имя пользователя: ");
                        string returnUserName = Console.ReadLine();
                        Console.Write("Введите название книги: ");
                        string returnBookTitle = Console.ReadLine();

                        if (libraryService.ReturnBook(returnBookTitle, returnUserName))
                        {
                            Console.WriteLine("Книга успешно возвращена.");
                        }
                        else
                        {
                            Console.WriteLine("Ошибка возврата книги.");
                        }
                        Console.ReadKey();
                        break;

                    case 6:
                        Console.WriteLine("Выход из программы...");
                        return;

                    default:
                        Console.WriteLine("Ошибка: неизвестный параметр.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}