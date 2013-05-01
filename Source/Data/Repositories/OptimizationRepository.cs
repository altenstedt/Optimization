using System.Collections.Generic;
using System.Linq;

using Data.Models;

namespace Data.Repositories
{
    public class OptimizationRepository
    {
        public IEnumerable<User> SearchUsers(string pattern, int index, int batchCount)
        {
            using (var context = new OptimizationContext())
            {
                var users =
                    context.Users.Where(item => item.FirstName.Contains(pattern) || item.LastName.Contains(pattern));

                return users.OrderBy(item => item.Id).Skip(index).Take(batchCount).ToList();
            }
        }

        public IEnumerable<User> SearchUsersBad(string pattern, int index, int batchCount)
        {
            using (var context = new OptimizationContext())
            {
                var users =
                    context.Users.Where(item => item.FirstName.Contains(pattern) || item.LastName.Contains(pattern))
                    .ToList();

                return users.OrderBy(item => item.Id).Skip(index).Take(batchCount).ToList();
            }
        }

        public IEnumerable<Book> GetBooksByPublisherAndAuthor(int publisherId, int authorId)
        {
            using (var context = new OptimizationContext())
            {
                return
                    context.Books.Where(book => book.Publisher.Id == publisherId)
                           .Where(book => book.Authors.Any(author => author.Id == authorId)).ToList();
            }
        }

        public IEnumerable<Book> GetBooksByPublisherAndAuthorBad(int publisherId, int authorId)
        {
            using (var context = new OptimizationContext())
            {
                var publisher = context.Publishers.Single(item => item.Id == publisherId);

                var result = new List<Book>();

                foreach (var book in publisher.Books)
                {
                    if (book.Authors.Any(item => item.Id == authorId))
                    {
                        result.Add(book);
                    }
                }

                return result;
            }
        }

        public int AddBook(Book book, int publisherId)
        {
            using (var context = new OptimizationContext())
            {
                var created = new Book { Isbn = book.Isbn, ReleaseDate = book.ReleaseDate, Title = book.Title };

                var publisher = context.Publishers.Single(item => item.Id == publisherId);
                created.Publisher = publisher;

                context.Books.Add(created);

                context.SaveChanges();

                return created.Id;
            }
        }

        public int AddBookBad(Book book, int publisherId)
        {
            using (var context = new OptimizationContext())
            {
                var created = new Book { Isbn = book.Isbn, ReleaseDate = book.ReleaseDate, Title = book.Title };

                var publisher = context.Publishers.Single(item => item.Id == publisherId);
                publisher.Books.Add(created);

                context.Books.Add(created);

                context.SaveChanges();

                return created.Id;
            }
        }

        public int AddUser(User user)
        {
            using (var context = new OptimizationContext())
            {
                var created = new User { FirstName = user.FirstName, LastName = user.LastName };

                context.Users.Add(created);

                context.SaveChanges();

                return created.Id;
            }
        }

        public User GetUser(int id)
        {
            using (var context = new OptimizationContext())
            {              
                return context.Users.Single(item => item.Id == id);
            }
        }

        public int AddPublisher(Publisher publisher)
        {
            using (var context = new OptimizationContext())
            {
                var created = new Publisher
                              {
                                  City = publisher.City,
                                  Country = publisher.Country,
                                  Name = publisher.Name
                              };

                context.Publishers.Add(created);

                context.SaveChanges();

                return created.Id;
            }
        }

        public Publisher GetPublisher(int id)
        {
            using (var context = new OptimizationContext())
            {
                return context.Publishers.Single(item => item.Id == id);
            }
        }

        public IEnumerable<Publisher> GetPublishers()
        {
            using (var context = new OptimizationContext())
            {
                return context.Publishers.ToList();
            }
        }

        public Book GetBook(int id)
        {
            using (var context = new OptimizationContext())
            {
                return context.Books.Single(item => item.Id == id);
            }
        }

        public IEnumerable<Book> SearchBooks(string pattern, int index, int batchCount)
        {
            using (var context = new OptimizationContext())
            {
                var books = context.Books.Where(item => item.Title.Contains(pattern));

                return books.OrderBy(item => item.Title).Skip(index).Take(batchCount).ToList();
            }
        }

        public IEnumerable<Book> SearchBooksBad(string pattern, int index, int batchCount)
        {
            using (var context = new OptimizationContext())
            {
                var books = context.Books.Where(item => item.Title.Contains(pattern)).ToList();

                return books.OrderBy(item => item.Title).Skip(index).Take(batchCount).ToList();
            }
        }

        public int AddAuthor(Author author)
        {
            using (var context = new OptimizationContext())
            {
                var created = new Author { FirstName = author.FirstName, LastName = author.LastName };

                context.Authors.Add(created);

                context.SaveChanges();

                return created.Id;
            }
        }

        public Author GetAuthor(int id)
        {
            using (var context = new OptimizationContext())
            {
                return context.Authors.Single(item => item.Id == id);
            }
        }

        public IEnumerable<Author> GetAuthors()
        {
            using (var context = new OptimizationContext())
            {
                return context.Authors.ToList();
            }
        }

        public void AddBookToAuthor(int bookId, int authorId)
        {
            using (var context = new OptimizationContext())
            {
                var book = context.Books.Single(item => item.Id == bookId);
                var author = context.Authors.Single(item => item.Id == authorId);

                book.Authors.Add(author);

                context.SaveChanges();
            }
        }

        public IEnumerable<Author> GetAuthorsByPublisher(int publisherId)
        {
            using (var context = new OptimizationContext())
            {
                var authors = context.Authors.Where(
                    author => author.Books.Any(book => book.Publisher.Id == publisherId));

                return authors.ToList();
            }
        }

        public IEnumerable<Author> GetAuthorsByPublisherBad(int publisherId)
        {
            using (var context = new OptimizationContext())
            {
                var publisherBookIds = context.Books.Where(item => item.Publisher.Id == publisherId).Select(item => item.Id).ToList();

                var authors = context.Authors.Where(author => author.Books.Any(book => publisherBookIds.Contains(book.Id)));

                return authors.ToList();
            }
        }
    }
}