﻿using Labb_3_QuizDataBas.Dialogs;
using Labb_3_QuizDataBas.ViewModel;
using System.Windows;

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
    }
}
