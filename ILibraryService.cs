namespace Library
{
    public interface ILibraryService
    {
        bool RegisterUser(string username, string password);
        bool Login(string username, string password);
        void Authenticate(string username, string password);
        void AddBook(string title, string author, string publisher);
        bool DeleteBook(string title, string author, string publisher);
        bool DeleteUser(string username, string password);
    }
}