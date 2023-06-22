using System.ComponentModel;
using System.Windows;


namespace FerryTerminalNamespace
{
   
    public class MainViewModel : INotifyPropertyChanged
    {

        public FerryTerminal Terminal { get; set; } // Property for the ferry terminal

        public MainViewModel()
        {
            Terminal = new FerryTerminal(); // Create a new ferry terminal object
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainViewModel(); // Create a new view model
            DataContext = viewModel; // Set the data context of the window to the view model
        }

        private void AddRandomVehicle_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Terminal.AddRandomVehicle(); // Call the method to add a random vehicle to the ferry terminal
        }

        private void ProcessNextVehicle_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Terminal.ProcessNextVehicle(); // Call the method to process the next vehicle in the ferry terminal
        }

        private void RefuelCurrentVehicle_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Terminal.RefuelCurrentVehicle(); // Call the method to refuel the current vehicle in the ferry terminal
        }

        private void ToggleGate_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Terminal.ToggleGate(); // Call the method to toggle the gate for the current vehicle in the ferry terminal
        }
    }
}
