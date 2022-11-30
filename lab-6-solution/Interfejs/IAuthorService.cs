using lab_6.Models;

namespace lab_6.Interfejs
{
    public interface IAuthorService
    {
        public int Save(Author author);
        public bool Delete(int? id);
        public bool Update(Author author);
        public Author? FindBy(int? id);
        public ICollection<Author> FindAll();
        public ICollection<Author> FindByAuthor(Author author);
        public ICollection<Author> FindPage(int page, int size);
        
    }
}
