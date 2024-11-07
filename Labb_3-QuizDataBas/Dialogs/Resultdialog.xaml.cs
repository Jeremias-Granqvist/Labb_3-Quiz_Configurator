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

namespace Labb_3_Quiz_Configurator.Dialogs
{
    /// <summary>
    /// Interaction logic for Resultdialog.xaml
    /// </summary>
    public partial class Resultdialog : Window
    {
        public Resultdialog(int correctAnswers, int totalQuestions)
        {
            InitializeComponent();
            ResultLabel.Text = $"You answered {correctAnswers} out of {totalQuestions} questions correctly!";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
