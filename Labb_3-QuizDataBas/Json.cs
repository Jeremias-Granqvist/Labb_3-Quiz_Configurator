using Labb_3_QuizDataBas.Model;
using Labb_3_QuizDataBas.ViewModel;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace Labb_3_Quiz_Configurator
{
    public class Json
    {
        // använd för att konkatenera /sätta ihop med filePath       Path path
        // kontrollera att mappen finns och filen finns, annars skapa mapp och skapa fil, kan använda klass "Directory".
        private string filePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public void SaveQuestionPack(ObservableCollection<QuestionPackViewModel> questionPack)
        {
            string folderName = @"Laboration3";
            string folderPath = Path.Combine(filePath, folderName);
            string fullPath = Path.Combine(folderPath, "questions.Json");

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
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
            File.WriteAllText(fullPath, json);

        }

        public ObservableCollection<QuestionPackViewModel> LoadQuestionPack()
        {
            string folderName = @"Laboration3";
            string folderPath = Path.Combine(filePath, folderName);
            string fullPath = Path.Combine(folderPath, "questions.Json");

            if (File.Exists(fullPath))
            {
                string json = File.ReadAllText(fullPath);

                var questionPack = JsonSerializer.Deserialize<ObservableCollection<QuestionPackViewModel>>(json);

                return questionPack;
            }
            else
            {
                return null;
            }
        }
    }
}
