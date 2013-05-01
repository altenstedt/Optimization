using Measure.ViewModels;

namespace Measure.Views
{
    public partial class ChromeView
    {
        public ChromeView()
        {
            InitializeComponent();
            ViewModel = new ChromeViewModel();
            DataContext = ViewModel;
        }

        public ChromeViewModel ViewModel { get; set; }
    }
}
