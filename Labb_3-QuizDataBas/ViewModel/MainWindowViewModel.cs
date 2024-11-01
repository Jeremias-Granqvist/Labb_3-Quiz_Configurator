using Labb_3_QuizDataBas.Command;
using Labb_3_QuizDataBas.Model;
using Labb_3_QuizDataBas.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Labb_3_QuizDataBas.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<QuestionPackViewModel> Packs { get; set; }
        public ConfigurationViewModel ConfigurationViewModel { get; }
        public PlayerViewModel PlayerViewModel{ get; }


        private QuestionPackViewModel? _activePack;
        
        public QuestionPackViewModel? ActivePack
        {
            get => _activePack;
            set
            {
                _activePack = value;
                RaisePropertyChanged();
                //ConfigurationViewModel.RaisePropertyChanged("ActivePack");

            }
        }
        public MainWindowViewModel()
        {
            Packs = new ObservableCollection<QuestionPackViewModel>();
            ActivePack = new QuestionPackViewModel(new QuestionPack("My Question Pack"));
            ConfigurationViewModel = new ConfigurationViewModel(this);
            Packs.Add(ActivePack);
            PlayerViewModel = new PlayerViewModel(this);
            

        }

        private void AddQuestion(object parameter)
        {
            var newQuestion = new Question();
        }
    }
}
