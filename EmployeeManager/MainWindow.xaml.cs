using EmployeeManager.ViewModels;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeeManager
{
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            DataContext = _viewModel;

            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(100);
            _ = Task.Run(async () => await _viewModel.LoadEmployeesPublicAsync());
        }
    }
}
