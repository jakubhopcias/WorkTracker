using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkTracker.Data;
using WorkTracker.Models;
using WorkTracker.ViewModels;

namespace WorkTracker.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AddDbContext _context;
        private MainViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();

            _context = new AddDbContext();
            _viewModel = new MainViewModel(_context);

            DataContext = _viewModel;
            _viewModel.HourlyRate = 50;
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var start = StartDate.SelectedDate ?? DateTime.Now;
            var end = EndDate.SelectedDate ?? DateTime.Now;
            if (end < start) return;

            var stage = new WorkStage
            {
                Name = Name.Text,
                StartTime = start,
                EndTime = end
            };
            _viewModel.AddWorkStage(stage);
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var stage = WorkStagesListView.SelectedItem as WorkStage;
            _viewModel.RemoveWorkStage(stage);
        }
        
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var id = int.Parse(IdEdit.Text);
            var editStart = StartDateEdit.SelectedDate ?? DateTime.Now;
            var editEnd = EndDateEdit.SelectedDate ?? DateTime.Now;

            var editedStage = new WorkStage
            {
                Id = id,
                Name = NameEdit.Text,
                StartTime = editStart,
                EndTime = editEnd
            };
            _viewModel.UpdateWorkStage(editedStage);
        }

        private void SetHourlyRateButton_Click(object sender, RoutedEventArgs e)
        {
            var rate = double.Parse(HourlyRate.Text);
            _viewModel.HourlyRate = rate;
        }
    }
}