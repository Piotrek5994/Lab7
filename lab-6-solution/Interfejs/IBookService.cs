using lab_6.Models;

namespace lab_6.Interfejs
{
    public interface IBookService
    {
        public int Save(Book book);
        public bool Delete(int? id);
        public bool Update(Book book);
        public Book? FindBy(int? id);
        public ICollection<Book> FindAll();
        public ICollection<Book> FindByAuthor(Author author);
        public ICollection<Book> FindPage(int page, int size);
    }

}
