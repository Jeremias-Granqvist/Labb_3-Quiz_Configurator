using Labb_3_Quiz_Configurator;
using Labb_3_QuizDataBas.ViewModel;
using Labb_3_QuizDataBas.Views;
using System.Collections.ObjectModel;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Labb_3_QuizDataBas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel mainWindowViewModel;
        public MainWindow()
        {

            InitializeComponent();
            DataContext = mainWindowViewModel = new MainWindowViewModel();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            await mainWindowViewModel.LoadPacks();
            
        }
        private void OpenConfigurationView()
        {
            var mainViewModel = this.DataContext as MainWindowViewModel;

            var configView = new ConfigurationView(mainViewModel)
            {
                DataContext = mainViewModel?.ConfigurationViewModel 
            };

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var viewModel = (MainWindowViewModel)this.DataContext;
            viewModel.OnWindowClosing(this);

            if (viewModel.WindowClosingCommand.CanExecute(null))
            {
                viewModel.WindowClosingCommand.Execute(null); 
            }
        }
    }
}