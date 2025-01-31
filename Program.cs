using System;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            LibraryService libraryService = new LibraryService();
            User admin = new User("Admin", "admin");
            libraryService.RegisterUser(admin.Username, admin.Password);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Авторизация ===");
                Console.WriteLine("1. Регистрация");
                Console.WriteLine("2. Добавить книгу" ); 
                Console.WriteLine("3. Поиск киги");
                Console.WriteLine("4. Выдать книгу");
                Console.WriteLine("5. Вернуть книгу");
                Console.WriteLine("6. Показать список пользователей");
                Console.WriteLine("7. Удалить пользователя");
                Console.WriteLine("8.Выйти из программы");
                Console.WriteLine("Выберите параметр:");
                
                string input = Console.ReadLine();
                if (!int.TryParse(input, out int inputInt))
                {
                    Console.WriteLine("Ошибка ввода. \nПопробуйте снова:");
                    Console.ReadKey();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Write("Введите имя пользователя: ");
                        string regUserName = Console.ReadLine();
                        Console.
                }
            }
        }
    }
}