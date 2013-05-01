using System;
using System.Collections.Generic;
using System.Linq;

using Data.Models;
using Data.Repositories;

namespace OptimizationTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PopulateUsers();
            PopulateBooks();

            Console.ReadLine();
        }

        private static void PopulateUsers()
        {
            var optimizationRepository = new OptimizationRepository();

            const int NumberOfUsers = 100000;

            Console.WriteLine("Populating users.");

            for (var i = 0; i <= NumberOfUsers; i++)
            {
                optimizationRepository.AddUser(
                    new User
                        {
                            FirstName = RandomStringGenerator.RandomString(8),
                            LastName = RandomStringGenerator.RandomString(8)
                        });

                if (i % Math.Max(NumberOfUsers / 100, 1) == 0)
                {
                    Console.Write((100 * ((double)i / Math.Max(NumberOfUsers, 1))) + "% ");
                }
            }

            Console.WriteLine();
        }

        private static void PopulateBooks()
        {
            var optimizationRepository = new OptimizationRepository();

            const int NumberOfPublishers = 3;
            const int NumberOfAuthors = 100;
            const int NumberOfBooks = 12000;

            Console.WriteLine("Populating books.");

            var publisherIds = new List<int>();
            for (var i = 0; i < NumberOfPublishers; i++)
            {
                publisherIds.Add(
                    optimizationRepository.AddPublisher(
                        new Publisher
                            {
                                Name = RandomStringGenerator.RandomString(8),
                                City = RandomStringGenerator.RandomString(8),
                                Country = RandomStringGenerator.RandomString(8)
                            }));
            }

            var authorIds = new List<int>();
            for (var i = 0; i < NumberOfAuthors; i++)
            {
                authorIds.Add(
                    optimizationRepository.AddAuthor(
                        new Author
                            {
                                FirstName = RandomStringGenerator.RandomString(8),
                                LastName = RandomStringGenerator.RandomString(8)
                            }));
            }

            var rng = new Random((int)DateTime.Now.Ticks);
            var publisherCount = publisherIds.Count;
            var authorCount = authorIds.Count;

            for (var i = 0; i <= NumberOfBooks; i++)
            {
                var publisherId = publisherIds.ElementAt(rng.Next(publisherCount));

                var bookId =
                    optimizationRepository.AddBook(
                        new Book { ReleaseDate = DateTime.Now, Title = RandomStringGenerator.RandomString(8) },
                        publisherId);

                var numberOfAuthors = rng.Next(4);

                for (var j = 0; j < numberOfAuthors; j++)
                {
                    var authorId = authorIds.ElementAt(rng.Next(authorCount));

                    optimizationRepository.AddBookToAuthor(bookId, authorId);
                }

                if (i % Math.Max(NumberOfBooks / 100, 1) == 0)
                {
                    Console.Write((100 * ((double)i / Math.Max(NumberOfBooks, 1))) + "% ");
                }
            }

            Console.WriteLine();
        }
    }
}
