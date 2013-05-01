using Measure.ViewModels;

namespace Measure.Views
{
    public partial class MeasureView
    {
        public MeasureView()
        {
            InitializeComponent();

            ViewModel = new MeasureViewModel();
            DataContext = ViewModel;
        }

        public MeasureViewModel ViewModel { get; set; }
    }
}
