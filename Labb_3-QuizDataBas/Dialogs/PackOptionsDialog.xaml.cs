using Labb_3_QuizDataBas.Model;
using Labb_3_QuizDataBas.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class PackOptionsDialog : Window, INotifyPropertyChanged
    {
        public QuestionPackViewModel PackViewModel { get; private set; }

        public PackOptionsDialog()
        {
            InitializeComponent();
            DataContext = (App.Current.MainWindow as MainWindow).DataContext;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void CancelChangesBTN_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
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

