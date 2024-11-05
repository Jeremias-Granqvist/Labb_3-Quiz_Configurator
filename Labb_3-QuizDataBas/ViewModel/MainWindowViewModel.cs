using Labb_3_Quiz_Configurator.Model;
using Labb_3_QuizDataBas.Command;
using Labb_3_QuizDataBas.Dialogs;
using Labb_3_QuizDataBas.Model;
using Labb_3_QuizDataBas.Views;
using System.Collections.ObjectModel;
using System.Drawing.Drawing2D;
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
                ConfigurationViewModel.RaisePropertyChanged("ActivePack");
            }
        }
        public MainWindowViewModel()
        {
            Packs = new ObservableCollection<QuestionPackViewModel>();
            ConfigurationViewModel = new ConfigurationViewModel(this);
            ActivePack = new QuestionPackViewModel(new QuestionPack("My Question Pack"));
            Packs.Add(ActivePack);
            PlayerViewModel = new PlayerViewModel(this);

            CurrentView = Visibility.ConfigMode;

            CreateNewPackCommand = new DelegateCommand(OnCreateNewPack);
            SaveNewPackCommand = new DelegateCommand(OnSaveNewPack);


            PlayCommand = new DelegateCommand(OnPlayCommandExecuted);
            SetFullScreenCommand = new DelegateCommand(OnSetFullScreen);
        }

        public ICommand SetFullScreenCommand { get; }
        public ICommand CreateNewPackCommand { get; }
        public ICommand SaveNewPackCommand { get; }

        private void OnCreateNewPack(object obj)
        {

            var questionPack = new QuestionPackViewModel(new QuestionPack("Name your questionpack"));
            newPack = questionPack;
            CreateNewPackDialog createNewPack = new CreateNewPackDialog();
            createNewPack.DataContext = this;
            createNewPack.Show();
            
        }
        private void OnSaveNewPack(object obj)
        {
            if (obj is CreateNewPackDialog createNewPackDialog)
            {
            Packs.Add(newPack);
             ActivePack = newPack;
            RaisePropertyChanged("ActivePack");
            createNewPackDialog.Close();
            }
        }

        private void OnSetFullScreen(object obj)
        {
//            FullscreenControls fullscreen = new FullscreenControls();

        }

        private Visibility _currentView;

        public Visibility CurrentView
        {
            get { return _currentView; }
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
        public bool IsConfigViewVisible => CurrentView == Visibility.ConfigMode;
        public bool IsPlayerViewVisible => CurrentView == Visibility.PlayerMode;

        public bool IsPlayButtonVisible => CurrentView == Visibility.ConfigMode;
        public ICommand PlayCommand { get; }

        private void OnPlayCommandExecuted(object obj)
        {
            CurrentView = Visibility.PlayerMode;
            RaisePropertyChanged("CurrentView");
            RaisePropertyChanged();
        }


        private QuestionPackViewModel _newPack;

        public QuestionPackViewModel newPack
        {
            get { return _newPack; }
            set { _newPack = value; }
        }

    }
}
