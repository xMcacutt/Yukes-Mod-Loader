using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Yukes_Mod_Loader_GUI.GUI.Models;

namespace Yukes_Mod_Loader_GUI.GUI.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    private ObservableCollection<GameViewModel> _games;
    private ObservableCollection<GameViewModel> _tempGames;
    private GameViewModel _selectedGame;
    
    public ObservableCollection<GameViewModel> Games
    {
        get => _games;
        set
        {
            _games = value;
            OnPropertyChanged(nameof(Games));
        }
    }
    
    public ObservableCollection<GameViewModel> TempGames
    {
        get => _tempGames;
        set
        {
            _tempGames = value;
            OnPropertyChanged(nameof(TempGames));
        }
    }

    public GameViewModel SelectedGame
    {
        get => _selectedGame;
        set
        {
            if (_selectedGame == value) return;
            _selectedGame = value;
            OnPropertyChanged(nameof(SelectedGame));
        }
    }
    
    public void Initialize()
    {
        Games = SettingsHandler.LoadGames();
        TempGames = new ObservableCollection<GameViewModel>(_games);
    }

    public void Save()
    {
        _games = new ObservableCollection<GameViewModel>(_tempGames);
    }

    public void AddGame()
    {
        TempGames.Add(new GameViewModel(new Game("New Game", "", "", "", true)));
    }

    public void RemoveGame(string name)
    {
        TempGames.Remove(TempGames.First(x => x.EditableName == name));
    }
}