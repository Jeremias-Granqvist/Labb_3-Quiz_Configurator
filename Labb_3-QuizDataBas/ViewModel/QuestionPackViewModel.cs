using Labb_3_QuizDataBas.Model;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Labb_3_QuizDataBas.ViewModel
{
    public class QuestionPackViewModel : ViewModelBase
    {
        private readonly QuestionPack model;
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public QuestionPack Model => model;


        public ObservableCollection<Question> Questions { get;}

        public string Name
        {
            get => model.Name;
            set
            {
                model.Name = value;
                RaisePropertyChanged();
            }
        }
        public Difficulty Difficulty 
        {
            get => model.Difficulty;
            set
            {
                model.Difficulty = value;
                RaisePropertyChanged();
            }
        }
        public int TimeLimitInSeconds 
        {
            get => model.TimeLimitInSeconds; 
            set
            {
                model.TimeLimitInSeconds = value;
                RaisePropertyChanged();
            }
        }

        public QuestionPackViewModel()
        {
            model = new QuestionPack(); 
            Questions = new ObservableCollection<Question>();
        }
        public QuestionPackViewModel(QuestionPack model)
        {
            this.model = model;
            this.Questions = new ObservableCollection<Question>(model.Questions ?? Enumerable.Empty<Question>());
        }
    }
}
