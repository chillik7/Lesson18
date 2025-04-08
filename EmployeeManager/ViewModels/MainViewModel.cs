using EmployeeManager.Data;
using EmployeeManager.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EmployeeManager.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly EmployeeRepository _repository;
        private readonly AppDbContext _context;

        public ObservableCollection<Employee> Employees { get; set; }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set { _selectedEmployee = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand UpdateCommand { get; }

        public MainViewModel()
        {
            _context = new AppDbContext();
            _repository = new EmployeeRepository(_context);
            Employees = new ObservableCollection<Employee>();

            AddCommand = new RelayCommand(async _ => await AddEmployeeAsync());
            DeleteCommand = new RelayCommand(async _ => await DeleteEmployeeAsync());
            UpdateCommand = new RelayCommand(async _ => await UpdateEmployeeAsync());
        }

        public async Task LoadEmployeesPublicAsync()
        {
            await LoadEmployeesAsync();
        }

        private async Task LoadEmployeesAsync()
        {
            var employees = await _repository.GetEmployeesAsync();

            // Обновляем ObservableCollection на UI-потоке
            Application.Current.Dispatcher.Invoke(() =>
            {
                Employees.Clear();
                foreach (var e in employees)
                    Employees.Add(e);
            });
        }

        private async Task AddEmployeeAsync()
        {
            var newEmployee = new Employee
            {
                FullName = "Новый сотрудник",
                Position = "Должность",
                Salary = 0
            };

            await _repository.AddEmployeeAsync(newEmployee);
            await LoadEmployeesAsync();
        }

        private async Task DeleteEmployeeAsync()
        {
            if (SelectedEmployee != null)
            {
                await _repository.DeleteEmployeeAsync(SelectedEmployee);
                await LoadEmployeesAsync();
            }
        }

        private async Task UpdateEmployeeAsync()
        {
            if (SelectedEmployee != null)
            {
                await _repository.UpdateEmployeeAsync(SelectedEmployee);
                await LoadEmployeesAsync();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
