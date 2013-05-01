using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Data.Models;
using Data.Repositories;

namespace Measure.ViewModels
{
    public class MeasureViewModel : NotificationObject
    {
        private readonly OptimizationRepository optimizationRepository;

        private TimeSpan searchUsers;
        private TimeSpan searchUsersBad;
        private TimeSpan addBook;
        private TimeSpan addBookBad;
        private TimeSpan getBooks;
        private TimeSpan getBooksBad;

        private bool isBusy;

        private bool isBusySearchUsers;
        private bool isBusySearchUsersBad;
        private bool isBusyAddBook;
        private bool isBusyAddBookBad;
        private bool isBusyGetBooks;
        private bool isBusyGetBooksBad;

        public MeasureViewModel()
        {
            optimizationRepository = new OptimizationRepository();

            SearchUsersCommand = new RelayCommand(
                () =>
                    {
                        IsBusy = true;

                        SearchUsers = new TimeSpan();
                        searchUsersBad = new TimeSpan();

                        Task.Factory.StartNew(
                            () =>
                                {
                                    Application.Current.Dispatcher.Invoke(() => IsBusySearchUsers = true);

                                    var result =
                                        StopWatch.MeasureElapsedTime(
                                            () => optimizationRepository.SearchUsers(string.Empty, 0, 100));

                                    Application.Current.Dispatcher.Invoke(() =>
                                        {
                                            SearchUsers = result.ElapsedTime;
                                            IsBusySearchUsers = false;
                                        });
                                }).ContinueWith(
                                    goodTask =>
                                        {
                                            Application.Current.Dispatcher.Invoke(() => IsBusySearchUsersBad = true);

                                            var result =
                                                StopWatch.MeasureElapsedTime(
                                                    () => optimizationRepository.SearchUsersBad(string.Empty, 0, 100));

                                            Application.Current.Dispatcher.Invoke(() =>
                                                {
                                                    SearchUsersBad = result.ElapsedTime;
                                                    IsBusySearchUsersBad = false;
                                                });
                                        }).ContinueWith(completed => IsBusy = false);
                    });

            AddBookCommand = new RelayCommand(() =>
                {
                    IsBusy = true;

                    AddBook = new TimeSpan();
                    AddBookBad = new TimeSpan();

                    var publisherIdTask = Task.Factory.StartNew(
                        () =>
                            {
                                var publishers = optimizationRepository.GetPublishers().ToList();
                                var rng = new Random();

                                return publishers.ElementAt(rng.Next(publishers.Count())).Id;
                            });

                    Task.Factory.ContinueWhenAll(
                        new[] { publisherIdTask },
                        tasks =>
                            {
                                Application.Current.Dispatcher.Invoke(() => IsBusyAddBook = true);

                                var result =
                                    StopWatch.MeasureElapsedTime(
                                        () =>
                                        optimizationRepository.AddBook(
                                            new Book
                                            {
                                                ReleaseDate = DateTime.Now
                                            }, 
                                            publisherIdTask.Result));

                                Application.Current.Dispatcher.Invoke(() =>
                                    {
                                        AddBook = result.ElapsedTime;
                                        IsBusyAddBook = false;
                                    });
                            }).ContinueWith(
                                goodTask =>
                                    {
                                        Application.Current.Dispatcher.Invoke(() => IsBusyAddBookBad = true);

                                        var result =
                                            StopWatch.MeasureElapsedTime(
                                                () =>
                                                optimizationRepository.AddBookBad(
                                                    new Book
                                                    {
                                                        ReleaseDate = DateTime.Now
                                                    }, 
                                                    publisherIdTask.Result));

                                        Application.Current.Dispatcher.Invoke(() =>
                                            {
                                                AddBookBad = result.ElapsedTime;
                                                IsBusyAddBookBad = false;
                                            });
                                    })
                        .ContinueWith(completed => Application.Current.Dispatcher.Invoke(() => IsBusy = false));
                });

            GetBooksCommand = new RelayCommand(() =>
                {
                    IsBusy = true;

                    GetBooks = new TimeSpan();
                    GetBooksBad = new TimeSpan();

                    var publisherIdTask = Task.Factory.StartNew(
                        () =>
                            {
                                var publishers = optimizationRepository.GetPublishers().ToList();
                                var rng = new Random();

                                return publishers.ElementAt(rng.Next(publishers.Count())).Id;
                            });

                    var authorIdTask = Task.Factory.StartNew(
                        () =>
                            {
                                var authors = optimizationRepository.GetAuthors().ToList();

                                var rng = new Random();
                                return authors.ElementAt(rng.Next(authors.Count())).Id;
                            });

                    Task.Factory.ContinueWhenAll(
                        new[] { publisherIdTask, authorIdTask },
                        tasks =>
                            {
                                Application.Current.Dispatcher.Invoke(() => IsBusyGetBooks = true);

                                var result =
                                    StopWatch.MeasureElapsedTime(
                                        () =>
                                        optimizationRepository.GetBooksByPublisherAndAuthor(
                                            publisherIdTask.Result, authorIdTask.Result));

                            Application.Current.Dispatcher.Invoke(() =>
                                {
                                    GetBooks = result.ElapsedTime;
                                    IsBusyGetBooks = false;
                                });
                        }).ContinueWith(
                                goodTask =>
                                    {
                                        Application.Current.Dispatcher.Invoke(() => IsBusyGetBooksBad = true);

                                        var result =
                                            StopWatch.MeasureElapsedTime(
                                                () =>
                                                optimizationRepository.GetBooksByPublisherAndAuthorBad(
                                                    publisherIdTask.Result, authorIdTask.Result));

                                        Application.Current.Dispatcher.Invoke(
                                            () =>
                                                {
                                                    GetBooksBad = result.ElapsedTime;
                                                    IsBusyGetBooksBad = false;
                                                });
                                    })
                        .ContinueWith(completed => Application.Current.Dispatcher.Invoke(() => IsBusy = false));
                });
        }

        public RelayCommand SearchUsersCommand { get; private set; }

        public RelayCommand AddBookCommand { get; private set; }

        public RelayCommand GetBooksCommand { get; private set; }

        public TimeSpan SearchUsers
        {
            get
            {
                return searchUsers;
            }

            set
            {
                searchUsers = value;
                RaisePropertyChanged();
            }
        }

        public TimeSpan SearchUsersBad
        {
            get
            {
                return searchUsersBad;
            }

            set
            {
                searchUsersBad = value;
                RaisePropertyChanged();
            }
        }

        public TimeSpan AddBook
        {
            get
            {
                return addBook;
            }

            set
            {
                addBook = value;
                RaisePropertyChanged();
            }
        }

        public TimeSpan AddBookBad
        {
            get
            {
                return addBookBad;
            }

            set
            {
                addBookBad = value;
                RaisePropertyChanged();
            }
        }

        public TimeSpan GetBooks
        {
            get
            {
                return getBooks;
            }

            set
            {
                getBooks = value;
                RaisePropertyChanged();
            }
        }

        public TimeSpan GetBooksBad
        {
            get
            {
                return getBooksBad;
            }

            set
            {
                getBooksBad = value;
                RaisePropertyChanged();
            }
        }

        public bool IsBusy
        {
            get
            {
                return isBusy;
            }

            set
            {
                isBusy = value;
                RaisePropertyChanged();
            }
        }

        public bool IsBusySearchUsers
        {
            get
            {
                return isBusySearchUsers;
            }

            set
            {
                isBusySearchUsers = value;
                RaisePropertyChanged();
            }
        }

        public bool IsBusySearchUsersBad
        {
            get
            {
                return isBusySearchUsersBad;
            }

            set
            {
                isBusySearchUsersBad = value;
                RaisePropertyChanged();
            }
        }

        public bool IsBusyAddBook
        {
            get
            {
                return isBusyAddBook;
            }

            set
            {
                isBusyAddBook = value;
                RaisePropertyChanged();
            }
        }

        public bool IsBusyAddBookBad
        {
            get
            {
                return isBusyAddBookBad;
            }

            set
            {
                isBusyAddBookBad = value;
                RaisePropertyChanged();
            }
        }

        public bool IsBusyGetBooks
        {
            get
            {
                return isBusyGetBooks;
            }

            set
            {
                isBusyGetBooks = value;
                RaisePropertyChanged();
            }
        }

        public bool IsBusyGetBooksBad
        {
            get
            {
                return isBusyGetBooksBad;
            }

            set
            {
                isBusyGetBooksBad = value;
                RaisePropertyChanged();
            }
        }
    }
}
