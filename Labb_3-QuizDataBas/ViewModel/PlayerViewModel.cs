using Labb_3_QuizDataBas.Command;
using System.Windows.Threading;

namespace Labb_3_QuizDataBas.ViewModel
{
    public class PlayerViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;
       
        private DispatcherTimer timer;
        private string testData;
        public DelegateCommand UpdateBUttonCommand { get; }

        public string TestData { get => testData;
            private set 
            { 
                testData = value;
                RaisePropertyChanged();
            }  
        
        }


        public PlayerViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

        }
        private void Timer_Tick(object? sender, EventArgs e)
        {
            TestData += "x";
        }
    }
}
