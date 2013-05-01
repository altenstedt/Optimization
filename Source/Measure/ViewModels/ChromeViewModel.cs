using System.Windows;

namespace Measure.ViewModels
{
    public class ChromeViewModel
    {
        public ChromeViewModel()
        {
            ExitCommand = new RelayCommand(() => Application.Current.Shutdown());
        }

        public RelayCommand ExitCommand { get; private set; }
    }
}
