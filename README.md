### 1. Models ###
Question: Represents individual quiz questions.
QuestionPack: Represents a collection of Question objects.
You don’t typically instantiate Models directly in the Views; instead, you'll use them in your ViewModels.

### 2. ViewModels ###
ViewModelBase: This can contain shared properties or methods for your other ViewModels, such as implementing INotifyPropertyChanged.

MainWindowViewModel: This will serve as the root ViewModel for your application, managing navigation between different Views (like ConfigurationView, PlayerView, and MenuView).

ConfigurationViewModel: This is responsible for managing the configuration of quiz packs, handling user input for creating or modifying QuestionPack and Question objects.

PlayerViewModel: This will manage the logic for playing the quiz.

QuestionPackViewModel: This might handle individual packs of questions, including adding/removing questions and any pack-specific logic.

### 3. Views ###
ConfigurationView: Binds to ConfigurationViewModel to allow users to create and edit question packs.

PlayerView: Binds to PlayerViewModel for playing the quiz.

MenuView: This can bind to MainWindowViewModel to navigate between different sections of your application.


### Instantiation Overview ###
1. Main Application Entry (usually in App.xaml.cs): Instantiate MainWindowViewModel, which will be set as the DataContext for the MainWindow.

2. MainWindowViewModel: This ViewModel can manage which View is currently displayed (Configuration, Player, Menu).
3. ConfigurationViewModel: This ViewModel will handle the creation and management of QuestionPack and Question instances.
4. Views: In your MainWindow.xaml, bind the content to the CurrentViewModel of MainWindowViewModel.

### Dialogs ###
For the dialogs (CreateNewPackDialog and PackOptionsDialog), you can either:

Instantiate them directly in the ViewModels and pass data back, or
Use a service that manages dialogs, allowing the ViewModels to invoke them without directly depending on the UI.

### Summary ###

Models: Used in ViewModels.
ViewModels: Manage data and logic; instantiated in a hierarchical manner, with MainWindowViewModel as the top-level.
Views: Bind to their corresponding ViewModels.
Commands: Use DelegateCommand to handle user actions.
Dialogs: Manage user input for creating packs and options.
If you have specific parts of the implementation you want to delve into, let me know!


### Personal notes ### 
när man trycker på "add question" så skapas ett nytt ListboxLtem, som har en Titel som är "Question:" följt av frågan som tas direkt av QuestionTB. 
om man inte fyller i någon fråga eller svar, sparas fortfarande frågan och man kan köra med tomma frågor. (**frågan sparas direkt, sen får vi uppdatera med propertychange när man fyller i?**)

när man skapar en ny QuestionPack är den tom, sen får man fylla på med frågor själv allt eftersom. 

