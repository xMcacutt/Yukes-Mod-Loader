using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Yukes_Mod_Loader_GUI.GUI.Views;

public partial class Settings : Window
{
    private bool IsEditing;
    private ObservableCollection<string> _gamesList = new ObservableCollection<string>();

    public Settings()
    {
        InitializeComponent();
        this.MouseDown += delegate{DragMove();};
        // Assuming you have a method to load existing games from your JSON settings file
        LoadGamesFromSettings();
    }

    private void AddGame_Click(object sender, RoutedEventArgs e)
    {
        // Logic to add a new game to the list
        _gamesList.Add("New Game");
    }

    private void RemoveGame_Click(object sender, RoutedEventArgs e)
    {
        // Logic to remove the selected game from the list
        if (GameListBox.SelectedIndex != -1)
        {
            _gamesList.RemoveAt(GameListBox.SelectedIndex);
        }
    }

    private void GameListBox_SelectionChanged(object sender, RoutedEventArgs e)
    {
        // Logic to handle selection change in the games list
        if (GameListBox.SelectedIndex != -1)
        {
            // Load settings for the selected game and populate fields
            LoadSettingsForSelectedGame();
            GameSettingsPanel.Visibility = Visibility.Visible;
        }
        else
        {
            // No game selected, hide the settings fields
            GameSettingsPanel.Visibility = Visibility.Collapsed;
        }
    }

    private void LoadGamesFromSettings()
    {
        // Logic to load existing games from your JSON settings file
        // Add the loaded games to the ObservableCollection
        _gamesList.Add("Game1");
        _gamesList.Add("Game2");
        GameListBox.ItemsSource = _gamesList;
    }

    private void LoadSettingsForSelectedGame()
    {
        // Logic to load settings for the selected game from your JSON settings file
        // Populate the fields accordingly
        IsoDirectoryTextBox.Text = "Loaded Iso Directory";
    }

    private void SaveSettings_Click(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void SelectIsoDirectory_Click(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void SelectFilesDirectory_Click(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void SelectModsDirectory_Click(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void MaximizeButton_Click(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void MinimizeButton_Click(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
    
}