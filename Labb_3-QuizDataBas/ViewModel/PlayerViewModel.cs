using Labb_3_QuizDataBas.Command;
using Labb_3_QuizDataBas.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace Labb_3_QuizDataBas.ViewModel
{
    public class PlayerViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;

        private DispatcherTimer timer;
        private int _time;
        
        public int Time
        {
            get => _time;
            private set 
            { 
                _time = value;
                RaisePropertyChanged();
            }
        }

        public int CurrentQuestionNumber { get; set; }
        public Question DisplayedQuestion { get; set; }


        private ObservableCollection<string> _allAnswers = new ObservableCollection<string>();
        ObservableCollection<string> AllAnswers 
        {

            get { return _allAnswers; }

            set 
            {  
                _allAnswers = value;
                RaisePropertyChanged();
                RaisePropertyChanged("Answer1");
                RaisePropertyChanged("Answer2");
                RaisePropertyChanged("Answer3");
                RaisePropertyChanged("Answer4");
            }
        }
        private ObservableCollection<string> allAnswers = new ObservableCollection<string>();

        private string _answer1;
        public string Answer1 => AllAnswers.Count > 0 ? AllAnswers[0] : string.Empty;

        private string _answer2;
        public string Answer2 => AllAnswers.Count > 1 ? AllAnswers[1] : string.Empty;

        private string _answer3;
        public string Answer3 => AllAnswers.Count > 2 ? AllAnswers[2] : string.Empty;
        
        private string _answer4;
        public string Answer4 => AllAnswers.Count > 3 ? AllAnswers[3] : string.Empty;


        public PlayerViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            
            this.mainWindowViewModel = mainWindowViewModel;

            Time = mainWindowViewModel.ActivePack.TimeLimitInSeconds;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(TimerTicking);

            SelectedOptionCommand = new DelegateCommand(SelectedAnswer);
        }

        private void SelectedAnswer(object obj)
        {
            
            if (obj.ToString() == DisplayedQuestion.CorrectAnswer.ToString())
            {

            }
        }

        public ICommand SelectedOptionCommand { get; private set; }

        public void CheckAnswer(object obj, Question question)
        {
            //kanske behöver den här metoden? är inte helt säker...
        }

        private void TimerTicking(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            Time--;
            if (Time == 0)
            {
                timer.Stop();
            }
        }

        public void StartQuiz(ObservableCollection<Question> questionList)
        {
            ObservableCollection<Question> QuestionListCopy = questionList;


            Shuffle(QuestionListCopy);
            if (CurrentQuestionNumber <= QuestionListCopy.Count)
            {
            QuizLoop(QuestionListCopy[CurrentQuestionNumber]);
            timer.Start();
            }
            if (CurrentQuestionNumber > QuestionListCopy.Count)
            {
                //här ska vi breaka till resultscreen.
            }
        }
        //startar
        //shufflar frågor
        //quizloop på frågan som stämmer med index för currentquesitonnumber.
        //shuffla svar på frågan
        //sätt displayedQuestion till frågan som skickades in.
        //öka currentquestionnumber
        //starta timer.


        public void QuizLoop(Question question)
        {
            ShuffleAnswers(question);
            
            DisplayedQuestion = question;
            CurrentQuestionNumber++;
            RaisePropertyChanged("CurrentQuestion");
        }
        //tryck play, shuffla frågor, shuffla svar, visa första frågan.
        //när användare tryckt på svar, shuffla svar för fråga 2, visa fråga 2.

        public ObservableCollection<string> ShuffleAnswers(Question question)
        {
            for (int i = 0; i < question.IncorrectAnswers.Length; i++)
            {
                AllAnswers.Add(question.IncorrectAnswers[i]);
            }
            string correctAnswer = question.CorrectAnswer;

            AllAnswers.Add(question.CorrectAnswer);
            Shuffle(AllAnswers);
            RaisePropertyChanged("AllAnswers");
            return AllAnswers;
        }

        private void Shuffle<T>(IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
