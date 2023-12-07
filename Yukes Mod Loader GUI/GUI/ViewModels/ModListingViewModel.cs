using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Yukes_Mod_Loader_GUI.GUI.ViewModels;

public class ModListingViewModel : ViewModelBase
{
    private ObservableCollection<ModViewModel> _mods;
    private ModViewModel _selectedMod;
    
    public ObservableCollection<ModViewModel> Mods
    {
        get => _mods;
        set
        {
            _mods = value;
            OnPropertyChanged(nameof(Mods));
        }
    }
    
    public ModViewModel SelectedMod
    {
        get => _selectedMod;
        set
        {
            if (_selectedMod == value) return;
            _selectedMod = value;
            OnPropertyChanged(nameof(SelectedMod));
        }
    }
    
    public void Initialize()
    {
        Mods = SettingsHandler.LoadMods("All");
    }

    public void RefreshMods(string gameFilter = "All")
    {
        Mods = SettingsHandler.LoadMods(gameFilter);
    }
}