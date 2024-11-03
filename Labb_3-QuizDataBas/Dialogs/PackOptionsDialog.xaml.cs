using Labb_3_QuizDataBas.Model;
using Labb_3_QuizDataBas.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Labb_3_QuizDataBas.Dialogs
{
    /// <summary>
    /// Interaction logic for PackOptionsDialog.xaml
    /// </summary>
    public partial class PackOptionsDialog : Window
    {
        public QuestionPackViewModel PackViewModel { get; private set; }

        public PackOptionsDialog(QuestionPackViewModel packViewModel)
        {
            InitializeComponent();
            DataContext = packViewModel;
        }

        private void CancelChangesBTN_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void SaveSettingsBTN_Click(object sender, RoutedEventArgs e)
        {

            var viewModel = DataContext as QuestionPackViewModel;

            if (viewModel != null)
            {
            this.DialogResult = true;
            }
            this.Close();
        }
    }
}

