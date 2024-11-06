using Labb_3_Quiz_Configurator;
using Labb_3_QuizDataBas.ViewModel;
using Labb_3_QuizDataBas.Views;
using System.Windows;

namespace Labb_3_QuizDataBas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            var manager = new Json();
            manager.LoadQuestionPack();
            InitializeComponent();
            DataContext = new MainWindowViewModel();

        }
        private void OpenConfigurationView()
        {
            var mainViewModel = this.DataContext as MainWindowViewModel;

            var configView = new ConfigurationView(mainViewModel)
            {
                DataContext = mainViewModel?.ConfigurationViewModel 
            };

        }
    }
}