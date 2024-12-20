﻿using Labb_3_Quiz_Configurator;
using Labb_3_Quiz_Configurator.Model;
using Labb_3_QuizDataBas.Command;
using Labb_3_QuizDataBas.Dialogs;
using Labb_3_QuizDataBas.Model;
using Labb_3_QuizDataBas.Views;
using System.Collections.ObjectModel;
using System.Drawing.Drawing2D;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Labb_3_QuizDataBas.ViewModel
{
    public enum Visibility
    {
        ConfigMode,
        PlayerMode
    }
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<QuestionPackViewModel> Packs { get; set; }
        public ConfigurationViewModel ConfigurationViewModel { get; }
        public PlayerViewModel PlayerViewModel{ get; }

        public bool visibilityConverter { get; set; } = true;


        private QuestionPackViewModel? _activePack;
        
        public QuestionPackViewModel? ActivePack
        {
            get => _activePack;
            set
            {
                _activePack = value;
                RaisePropertyChanged();
                var manager = new Json();
                manager.SaveQuestionPack(Packs);
                ConfigurationViewModel.RaisePropertyChanged("ActivePack");
            }
        }
        public MainWindowViewModel()
        {
            Packs = new ObservableCollection<QuestionPackViewModel>();
            ConfigurationViewModel = new ConfigurationViewModel(this);
            
            PlayerViewModel = new PlayerViewModel(this);

            CurrentView = new ConfigurationView();

            CreateNewPackCommand = new DelegateCommand(OnCreateNewPack);
            SaveNewPackCommand = new DelegateCommand(OnSaveNewPack);
            SelectPackCommand = new DelegateCommand(OnSelectPack);

            SwitchToPlayerViewCommand = new DelegateCommand(SwitchToPlayerView);
            SwitchToConfigurationViewCommand = new DelegateCommand(SwitchToConfigurationView);

            SetFullScreenCommand = new DelegateCommand(OnSetFullScreen);
            AddQuestionCommand = new DelegateCommand(OnAddQuestion);
            WindowClosingCommand = new DelegateCommand(OnWindowClosing);
        }
        public ICommand SelectPackCommand { get; }
        public ICommand SetFullScreenCommand { get; }
        public ICommand CreateNewPackCommand { get; }
        public ICommand SaveNewPackCommand { get; }
        public ICommand AddQuestionCommand { get; }
        private void OnAddQuestion(object parameter)
        {
            ActivePack.Questions.Add(new Question());
            RaisePropertyChanged();
        }
        public async Task LoadPacks()
        {
            var manager = new Json();
            Packs = await manager.LoadQuestionPack();
            RaisePropertyChanged("Packs");
            if (Packs == null)
            {
                ActivePack = new QuestionPackViewModel(new QuestionPack("My Question Pack"));
                Packs.Add(ActivePack);

            }
            else
            {
                ActivePack = Packs.FirstOrDefault();
                
            }
        }

        private void OnCreateNewPack(object obj)
        {

            var questionPack = new QuestionPackViewModel(new QuestionPack("Name your questionpack"));
            newPack = questionPack;
            CreateNewPackDialog createNewPack = new CreateNewPackDialog();
            createNewPack.DataContext = this;
            createNewPack.Show();

        }
        private void OnSelectPack(object obj)
        {
            ActivePack = (QuestionPackViewModel)obj;
            RaisePropertyChanged("ActivePack");
       }
        private void OnSaveNewPack(object obj)
        {
            if (obj is CreateNewPackDialog createNewPackDialog)
            {
            Packs.Add(newPack);
             ActivePack = newPack;
            RaisePropertyChanged("ActivePack");
            createNewPackDialog.Close();

                var manager = new Json();
                manager.SaveQuestionPack(Packs);
            }
        }

        private void OnSetFullScreen(object obj)
        {
            if(obj is Window win)
            {
                FullscreenControls.Fullscreen(win);
            }
        }

        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set 
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    RaisePropertyChanged("CurrentView");
                    RaisePropertyChanged("IsConfigViewVisible");
                    RaisePropertyChanged("IsPlayerViewVisible");
                }
            }
        }
        public Visibility CurrentVisibility { get; set; }

        public bool IsConfigViewVisible => CurrentVisibility == Visibility.ConfigMode;
        public bool IsPlayerViewVisible => CurrentVisibility == Visibility.PlayerMode;

        public bool IsPlayButtonVisible => CurrentVisibility == Visibility.ConfigMode;
        public ICommand PlayCommand { get; }

        public ICommand SwitchToConfigurationViewCommand { get; }
        public ICommand SwitchToPlayerViewCommand { get; }
        public ICommand WindowClosingCommand { get; }

        private void SwitchToPlayerView(object obj)
        {

            CurrentView = new PlayerView();
            PlayerViewModel.StartQuiz(ActivePack.Questions);
            RaisePropertyChanged();
        }
        private void SwitchToConfigurationView(object obj)
        {
            CurrentView = new ConfigurationView();
            RaisePropertyChanged();
        }


        private QuestionPackViewModel _newPack;

        public QuestionPackViewModel newPack
        {
            get { return _newPack; }
            set { _newPack = value; }
        }
        public void OnWindowClosing(object obj)
        {
                var manager = new Json();
                manager.SaveQuestionPack(Packs);
            App.Current.Shutdown();
        }



    }
}
