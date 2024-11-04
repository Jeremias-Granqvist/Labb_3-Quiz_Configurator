using Labb_3_QuizDataBas.Dialogs;
using Labb_3_QuizDataBas.ViewModel;
using System.Windows.Controls;

namespace Labb_3_QuizDataBas.Views
{
    public partial class MenuView : UserControl
    {
        public MenuView()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var viewModel = this.DataContext as ConfigurationViewModel;
            if (viewModel != null && viewModel.ActivePack != null)
            {
                var settingsDialog = new PackOptionsDialog();
                if (settingsDialog.ShowDialog() == true)
                {

                }
            }

        }
    }
}

    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>