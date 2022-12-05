using lab_6.Models;
using Microsoft.EntityFrameworkCore;

namespace lab_6.Interfejs
{
    public class BookServiceEF : IBookService
    {

        private readonly AppDbContext _context;
        private readonly IClockProvider _clock;
        

        public BookServiceEF(AppDbContext context) : base()
        {
            _context = context;
            _clock = new DefaultClock();
        }

        public int Save(Book book)
        {
            var entityEntry = _context.Books.Add(book);
            _context.SaveChanges();
            return entityEntry.Entity.Id;
        }

        public bool Delete(int? id)
        {
            if (id is null)
            {
                return false;
            }
            var find = _context.Books.Find(id);
            if (find is not null)
            {
                _context.Books.Remove(find);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Update(Book book)
        {
            try
            {
                var find = _context.Books.Find(book.Id);
                if (find is not null)
                {
                    find.Title = book.Title;
                    find.ReleaseDate = book.ReleaseDate;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public Book? FindBy(int? id)
        {
            if (id is null)
            {
                return null;
            }
            return _context.Books.Find(id);
        }

        public ICollection<Book> FindAll()
        {
            return _context.Books.ToList();
        }

        public ICollection<Book> FindByAuthor(Author author)
        {
            return _context.Books.Where(book => book.Authors.Contains(author)).ToList();
        }

        public ICollection<Book> FindPage(int page = 0,int size = 10)
        {
            return _context.Books
            .Skip(page * size)
            .Take(page)
            .ToHashSet();
        }
        public (string,int?) BookAge(int? id)
        {
            if (id is null)
            {
                return ("Book not found",null);
            }
            var book = _context.Books.Find(id);
            if (book is null)
            {
                return ("Book not found",null);
            }
            var age = _clock.Now().Year - book.ReleaseDate.Year;
            return ($"Book age is",age);
        }
    }
}
