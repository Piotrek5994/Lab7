using lab_6.Models;
using Microsoft.EntityFrameworkCore;


namespace lab_6.Interfejs
{
    public class AuthorServiceEF : IAuthorService 
    {
        private readonly AppDbContext _context;

        public AuthorServiceEF(AppDbContext context) : base()
        {
            _context = context;
        }
      

        public bool Delete(int? id)
        {
            if (id is null)
            {
                return false;
            }
            var find = _context.Authors.Find(id);
            if (find is not null)
            {
                _context.Authors.Remove(find);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public ICollection<Author> FindAll()
        {
            return _context.Authors.ToList();
        }

        public Author? FindBy(int? id)
        {
            if (id is null)
            {
                return null;
            }
            return _context.Authors.Find(id);
        }

        public ICollection<Author> FindByAuthor(Author author)
        {
            return _context.Authors.Where(a => a.FirstName == author.FirstName && a.LastName == author.LastName).ToList();
        }

        public ICollection<Author> FindPage(int page, int size)
        {
            return _context.Authors
            .Skip(page * size)
            .Take(page)
            .ToHashSet();
        }

        public int Save(Author author)
        {
            var entityEntry = _context.Authors.Add(author);
            _context.SaveChanges();
            return entityEntry.Entity.Id;
        }

        public bool Update(Author author)
        {
            try
            {
                var find = _context.Authors.Find(author.Id);
                if (find is not null)
                {
                    find.FirstName = author.FirstName;
                    find.LastName = author.LastName;
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
    }
}
