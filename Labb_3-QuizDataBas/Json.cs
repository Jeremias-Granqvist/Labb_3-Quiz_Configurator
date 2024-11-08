using Labb_3_QuizDataBas.Model;
using Labb_3_QuizDataBas.ViewModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Labb_3_Quiz_Configurator
{
    public class Json : INotifyPropertyChanged
    {
        private string filePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        private ObservableCollection<QuestionPackViewModel> _packs;

        public event PropertyChangedEventHandler? PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ObservableCollection<QuestionPackViewModel> Packs
        {
            get => _packs;
            set
            {
                _packs = value;
                RaisePropertyChanged("Packs");
            }
        }
        private QuestionPack QuestionPack;

        public QuestionPack _questionPack
        {
            get { return QuestionPack; }
            set { QuestionPack = value; }
        }


        public async void SaveQuestionPack(ObservableCollection<QuestionPackViewModel> questionPack)
        {
            string folderName = @"Laboration3";
            string folderPath = Path.Combine(filePath, folderName);
            string fullPath = Path.Combine(folderPath, "questions.Json");

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                IgnoreReadOnlyFields = true,
                IgnoreReadOnlyProperties = true
            };

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string json = JsonSerializer.Serialize(questionPack, options);
            if (!File.Exists(fullPath))
            {
                File.Create(fullPath);
            }

            await File.WriteAllTextAsync(fullPath, json);

        }

        public async Task<ObservableCollection<QuestionPackViewModel>> LoadQuestionPack()
        {
            string folderName = @"Laboration3";
            string folderPath = Path.Combine(filePath, folderName);
            string fullPath = Path.Combine(folderPath, "questions.Json");

            if (File.Exists(fullPath))
            {
                if (new FileInfo(fullPath).Length != 0)
                {
                    string json = await File.ReadAllTextAsync(fullPath);
                    var qPack = JsonSerializer.Deserialize<ObservableCollection<QuestionPackViewModel>>(json);
  //                  Packs = new ObservableCollection<QuestionPackViewModel>();
                    return qPack;
                    //returna qPack istället
                }
                else
                {
                    return null;
                }
            }
            else
            {
                Packs = new ObservableCollection<QuestionPackViewModel>();
                
                return Packs;
            }
        }
    }
}
