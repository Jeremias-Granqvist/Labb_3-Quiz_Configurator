using Labb_3_Quiz_Configurator.Dialogs;
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
        public int TotalQuestionsNumber { get; set; }
        public int CorrectNumberOfQuestions { get; set; }
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


        private System.Windows.Media.Brush _answer1Color;
        public System.Windows.Media.Brush Answer1Color
        {
            get => _answer1Color;
            set
            {
                _answer1Color = value;
                RaisePropertyChanged(nameof(Answer1Color));
            }
        }

        private System.Windows.Media.Brush _answer2Color;
        public System.Windows.Media.Brush Answer2Color
        {
            get => _answer2Color;
            set
            {
                _answer2Color = value;
                RaisePropertyChanged(nameof(Answer2Color));
            }
        }

        private System.Windows.Media.Brush _answer3Color;
        public System.Windows.Media.Brush Answer3Color
        {
            get => _answer3Color;
            set
            {
                _answer3Color = value;
                RaisePropertyChanged(nameof(Answer3Color));
            }
        }

        private System.Windows.Media.Brush _answer4Color;
        public System.Windows.Media.Brush Answer4Color
        {
            get => _answer4Color;
            set
            {
                _answer4Color = value;
                RaisePropertyChanged(nameof(Answer4Color));
            }
        }





        public PlayerViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            
            this.mainWindowViewModel = mainWindowViewModel;


            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(TimerTicking);

            SelectedOptionCommand = new DelegateCommand(SelectedAnswer);
        }

        private string _feedbackMessage;
        public string FeedbackMessage
        {
            get => _feedbackMessage;
            set
            {
                _feedbackMessage = value;
                RaisePropertyChanged("FeedbackMessage");
            }
        }

        private System.Windows.Media.Brush _feedbackMessageColor;
        public System.Windows.Media.Brush FeedbackMessageColor
        {
            get => _feedbackMessageColor;
            set
            {
                _feedbackMessageColor = value;
                RaisePropertyChanged("FeedbackMessageColor");
            }
        }

        private void SelectedAnswer(object obj)
        {
            ObservableCollection<Question> questionlistcopy = QuestionListCopy;

            if (obj.ToString() == DisplayedQuestion.CorrectAnswer.ToString())
            {
                FeedbackMessage = "Correct";
                FeedbackMessageColor = System.Windows.Media.Brushes.Green;
                CorrectNumberOfQuestions++;

                Answer1Color = (obj.ToString() == Answer1) ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;
                Answer2Color = (obj.ToString() == Answer2) ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;
                Answer3Color = (obj.ToString() == Answer3) ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;
                Answer4Color = (obj.ToString() == Answer4) ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;
            }
            else
            {
                FeedbackMessage = $"Incorrect, correct answer is {DisplayedQuestion.CorrectAnswer}";
                FeedbackMessageColor = System.Windows.Media.Brushes.Red;
                Answer1Color = (Answer1 == DisplayedQuestion.CorrectAnswer.ToString()) ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;
                Answer2Color = (Answer2 == DisplayedQuestion.CorrectAnswer.ToString()) ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;
                Answer3Color = (Answer3 == DisplayedQuestion.CorrectAnswer.ToString()) ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;
                Answer4Color = (Answer4 == DisplayedQuestion.CorrectAnswer.ToString()) ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;
            }
            timer.Stop();
            DispatcherTimer nextQuestionTimer = new DispatcherTimer();
            nextQuestionTimer.Interval = TimeSpan.FromSeconds(2);


            nextQuestionTimer.Tick += (sender, e) =>
            {
                nextQuestionTimer.Stop();
                CurrentQuestionNumber++;
                Time = mainWindowViewModel.ActivePack.TimeLimitInSeconds;
                ResetButtonColors();
                QuizRunning(questionlistcopy); // Call QuizRunning with your questions list
            };
            nextQuestionTimer.Start();
        }


        public ICommand SelectedOptionCommand { get; private set; }

        public void ResetButtonColors()
        {
            Answer1Color = System.Windows.Media.Brushes.Transparent;
            Answer2Color = System.Windows.Media.Brushes.Transparent;
            Answer3Color = System.Windows.Media.Brushes.Transparent;
            Answer4Color = System.Windows.Media.Brushes.Transparent;
        }

        private void TimerTicking(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            Time--;
            if (Time == 0)
            {
                timer.Stop();
                CurrentQuestionNumber++;
                QuizRunning(QuestionListCopy);
            }
        }
        ObservableCollection<Question> QuestionListCopy = new ObservableCollection<Question>();
        public void StartQuiz(ObservableCollection<Question> questionList)
        {
            Time = mainWindowViewModel.ActivePack.TimeLimitInSeconds;
            QuestionListCopy = questionList;
            TotalQuestionsNumber = QuestionListCopy.Count;
            CurrentQuestionNumber = 0;
            Shuffle(QuestionListCopy);
            QuizRunning(QuestionListCopy);
        }
        public void QuizRunning(ObservableCollection<Question> questionList)
        {

            if (CurrentQuestionNumber < TotalQuestionsNumber)
            {
                QuizLoop(questionList, questionList[CurrentQuestionNumber]);
                timer.Start();
            }
            else if (CurrentQuestionNumber >= TotalQuestionsNumber)
            {
                PreCheck(questionList);
            }
        }

        public void QuizLoop(ObservableCollection<Question> questionList, Question question)
        {
            ShuffleAnswers(question);
            
            DisplayedQuestion = question;
            RaisePropertyChanged("CurrentQuestion");
            RaisePropertyChanged("DisplayedQuestion");
        }

        //CurrentQuestionNumber++;
        //    PreCheck(questionList);

        public void PreCheck(ObservableCollection<Question> questionList)
        {
            if (CurrentQuestionNumber < TotalQuestionsNumber)
            {
                QuizRunning(questionList);
            }
            else
            {
                Resultdialog resultDialog = new Resultdialog(CorrectNumberOfQuestions, TotalQuestionsNumber);
                resultDialog.ShowDialog();
            }
        }



        //startar
        //shufflar frågor
        //quizloop på frågan som stämmer med index för currentquesitonnumber.
        //shuffla svar på frågan
        //sätt displayedQuestion till frågan som skickades in.
        //öka currentquestionnumber
        //starta timer.







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
