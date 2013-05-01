using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

using Data.Models;
using Data.Repositories;

namespace OptimizationTest
{
    public static class Measure
    {
        public static void MeasureSearchUsers()
        {
            var optimizationRepository = new OptimizationRepository();

            MeasureElapsedTime(() => optimizationRepository.SearchUsers(string.Empty, 0, 100));
            MeasureElapsedTime(() => optimizationRepository.SearchUsers(string.Empty, 0, 100));
            MeasureElapsedTime(() => optimizationRepository.SearchUsers("A", 0, 100));
            MeasureElapsedTime(() => optimizationRepository.SearchUsers("AB", 0, 100));

            MeasureElapsedTime(() => optimizationRepository.SearchUsersBad(string.Empty, 0, 100));
            MeasureElapsedTime(() => optimizationRepository.SearchUsersBad(string.Empty, 0, 100));
            MeasureElapsedTime(() => optimizationRepository.SearchUsersBad("A", 0, 100));
            MeasureElapsedTime(() => optimizationRepository.SearchUsersBad("AB", 0, 100));
        }

        public static void MeasureGetAuthorsByPublisher()
        {
            var optimizationRepository = new OptimizationRepository();

            var publisher = optimizationRepository.GetPublishers().First();

            Console.WriteLine(MeasureElapsedTime(() => optimizationRepository.GetAuthorsByPublisher(publisher.Id)).Count());
            Console.WriteLine(MeasureElapsedTime(() => optimizationRepository.GetAuthorsByPublisher(publisher.Id)).Count());
        }

        public static void MeasureAddBook()
        {
            var optimizationRepository = new OptimizationRepository();

            var publishers = optimizationRepository.GetPublishers().ToList();

            var pub = publishers.First();

            MeasureElapsedTime(
            () =>
            optimizationRepository.AddBook(
                new Book
                {
                    ReleaseDate = DateTime.Now,
                    Isbn = RandomStringGenerator.RandomString(8, null),
                    Title = RandomStringGenerator.RandomString(8, null)
                },
                pub.Id));

            MeasureElapsedTime(
                () =>
                optimizationRepository.AddBookBad(
                    new Book
                    {
                        ReleaseDate = DateTime.Now,
                        Isbn = RandomStringGenerator.RandomString(8, null),
                        Title = RandomStringGenerator.RandomString(8, null)
                    },
                    pub.Id));
        }

        public static void MeasureSearchBooks()
        {
            var optimizationRepository = new OptimizationRepository();

            MeasureElapsedTime(() => optimizationRepository.SearchBooks(string.Empty, 0, 100));
            MeasureElapsedTime(() => optimizationRepository.SearchBooks(string.Empty, 0, 100));
            MeasureElapsedTime(() => optimizationRepository.SearchBooksBad(string.Empty, 0, 100));
        }

        public static void MeasureBooksByPublisherAndAuthor()
        {
            var optimizationRepository = new OptimizationRepository();

            MeasureElapsedTime(() => optimizationRepository.GetBooksByPublisherAndAuthor(1, 5));
            MeasureElapsedTime(() => optimizationRepository.GetBooksByPublisherAndAuthorBad(1, 5));
        }

        private static T MeasureElapsedTime<T>(Expression<Func<T>> func)
        {
            T result;
            var compiled = func.Compile();

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            try
            {
                result = compiled();
            }
            finally
            {
                stopWatch.Stop();
            }

            Console.WriteLine("{0}: {1}", ((MethodCallExpression)func.Body).Method.Name, stopWatch.Elapsed);

            return result;
        }
    }
}
