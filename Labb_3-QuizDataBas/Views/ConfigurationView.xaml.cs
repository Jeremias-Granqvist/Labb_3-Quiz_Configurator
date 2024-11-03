using Labb_3_Quiz_Configurator.Model;
using Labb_3_QuizDataBas.Dialogs;
using Labb_3_QuizDataBas.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Labb_3_QuizDataBas.Views
{
    /// <summary>
    /// Interaction logic for ConfigurationView.xaml
    /// </summary>
    public partial class ConfigurationView : System.Windows.Controls.UserControl
    {
        public ConfigurationView()
        {            
            InitializeComponent();
        }
        public ConfigurationView(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
        }

        private void PackSettings_Click(object sender, RoutedEventArgs e)
        {

            var viewModel = this.DataContext as ConfigurationViewModel;
            if (viewModel != null && viewModel.ActivePack != null)
            {
                var settingsDialog = new PackOptionsDialog(new QuestionPackViewModel(PackHolder.ActivePack));
                if (settingsDialog.ShowDialog() == true)
                {

                }
            }
        }
    }
}
