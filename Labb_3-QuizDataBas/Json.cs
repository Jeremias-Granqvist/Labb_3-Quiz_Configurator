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
        public void SaveQuestionPack(ObservableCollection<QuestionPackViewModel> questionPack)
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

            File.WriteAllTextAsync(fullPath, json);

        }

        public ObservableCollection<QuestionPackViewModel> LoadQuestionPack()
        {
            string folderName = @"Laboration3";
            string folderPath = Path.Combine(filePath, folderName);
            string fullPath = Path.Combine(folderPath, "questions.Json");

            if (File.Exists(fullPath))
            {
                if (new FileInfo(fullPath).Length != 0)
                {
                    string json = File.ReadAllText(fullPath);
                    var questionPacks = JsonSerializer.Deserialize<ObservableCollection<QuestionPackViewModel>>(json);
                    Packs = questionPacks ?? new ObservableCollection<QuestionPackViewModel>();
                    return Packs;

                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
