using Labb_3_Quiz_Configurator;
using Labb_3_QuizDataBas.Command;
using Labb_3_QuizDataBas.Dialogs;
using Labb_3_QuizDataBas.Model;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Labb_3_QuizDataBas.ViewModel
{
    public class ConfigurationViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;

        private QuestionPackViewModel? _mainWindowViewModel;

        public ConfigurationViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

           SelectedItem = ActivePack?.Questions.FirstOrDefault();



            AddQuestionCommand = new DelegateCommand(OnAddQuestion);
            RemoveQuestionCommand = new DelegateCommand(OnRemoveQuestion);
            OpenPackSettingsCommand = new DelegateCommand(OnOpenPackSettings);
        }

        public ICommand AddQuestionCommand { get; }
        public ICommand RemoveQuestionCommand { get; }
        public ICommand OpenPackSettingsCommand { get; }

        private void OnAddQuestion(object parameter)
        {
            ActivePack.Questions.Add(new Question());
            RaisePropertyChanged();
        }
        private void OnRemoveQuestion(object parameter)
        {
            if (SelectedItem != null)
            {
                ActivePack.Questions.Remove(SelectedItem);
                RaisePropertyChanged();
            }
        }
        private void OnOpenPackSettings(object obj)
        {

            PackOptionsDialog packOptions = new PackOptionsDialog();
            packOptions.Show();
        }



        public string ActivePackName => ActivePack?.Name ?? "No Pack Selected";
        private void ActivePack_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ActivePack.Name))
            {
                RaisePropertyChanged(nameof(ActivePackName));
            }
        }

        private QuestionPackViewModel? _activepack;
        public QuestionPackViewModel? ActivePack
        {
            get => mainWindowViewModel.ActivePack;
        }
        
        private Question? _selectedItem;
        public Question? SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }

        private Visibility _visibility;
        public Visibility visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                RaisePropertyChanged();
            }
        }

        private string _query;
        public string Query
        {
            get => SelectedItem?.Query;
            set
            {
                SelectedItem.Query = value;
                RaisePropertyChanged();
            }
        }

        private string _correctAnswer;
        public string CorrectAnswer
        {
            get => SelectedItem.CorrectAnswer;
            set
            {
                SelectedItem.CorrectAnswer = value;
                RaisePropertyChanged();
            }
        }

        private string _incorrectAnswer1;
        public string IncorrectAnswer1
        {
            get => SelectedItem.IncorrectAnswers.ElementAtOrDefault(0) ?? string.Empty;
            set
            {
                SelectedItem.IncorrectAnswers[0] = value;
                RaisePropertyChanged();
            }
        }

        private string _incorrectAnswer2;

        public string IncorrectAnswer2
        {
            get => SelectedItem.IncorrectAnswers[1];
            set
            {
                SelectedItem.IncorrectAnswers[1] = value;
                RaisePropertyChanged();
            }
        }

        private string _incorrectAnswer3;
        public string IncorrectAnswer3
        {
            get => SelectedItem.IncorrectAnswers[2];
            set
            {
                SelectedItem.IncorrectAnswers[2] = value;
                RaisePropertyChanged();
            }
        }
    }
}
