using Labb_3_QuizDataBas.Model;
using Labb_3_QuizDataBas.ViewModel;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Labb_3_Quiz_Configurator
{
    public class Json
    {
        private string filePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
// använd för att konkatenera /sätta ihop med filePath       Path path
       // kontrollera att mappen finns och filen finns, annars skapa mapp och skapa fil, kan använda klass "Directory".
        
        public void SaveQuestionPack(ObservableCollection<QuestionPackViewModel> questionPack)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(questionPack, options);
            File.WriteAllText("questions.Json", json);
        }

        public QuestionPack LoadQuestionPack() 
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                var questionPack = JsonSerializer.Deserialize<QuestionPack>(json);

                return questionPack;
            }
            else
            {
                return null;
            }
        }
    }
}
