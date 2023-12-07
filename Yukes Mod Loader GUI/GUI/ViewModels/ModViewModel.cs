using System.Collections.Generic;
using Yukes_Mod_Loader_GUI.GUI.Models;

namespace Yukes_Mod_Loader_GUI.GUI.ViewModels;

public class ModViewModel : ViewModelBase
{
    private readonly Mod? _originalMod;
    private string _editableName;
    private string _date;
    private List<string> _exclusions;

    public ModViewModel(Mod originalMod)
    {
        _originalMod = originalMod;
        Game = originalMod.Game;
        _editableName = originalMod.Name;
        _exclusions = originalMod.Exclusions;
        _date = originalMod.Date;
    }
    
    public string Game { get; }

    public string EditableName
    {
        get => _editableName;
        set
        {
            if (_editableName == value) return;
            _editableName = value;
            OnPropertyChanged(nameof(EditableName));
        }
    }
    

    public List<string> Exclusions
    {
        get => _exclusions;
        set
        {
            if (_exclusions == value) return;
            _exclusions = value;
            OnPropertyChanged(nameof(Exclusions));
        }
    }

    public string Date
    {
        get => _date;
        set
        {
            if (_date == value) return;
            _date = value;
            OnPropertyChanged(nameof(Date));
        }
    }
}