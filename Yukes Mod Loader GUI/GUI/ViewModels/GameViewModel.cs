using Yukes_Mod_Loader_GUI.GUI.Models;

namespace Yukes_Mod_Loader_GUI.GUI.ViewModels;

public class GameViewModel : ViewModelBase
{
    private readonly Game? _originalGame;
    private string _isoDir;
    private string _extDir;
    private string _modDir;
    private bool _keepDip;

    public GameViewModel(Game originalGame)
    {
        _originalGame = originalGame;
        _editableName = originalGame?.Name ?? string.Empty;
        _isoDir = originalGame?.IsoDir ?? string.Empty;
        _extDir = originalGame?.ExtDir ?? string.Empty;
        _modDir = originalGame?.ModDir ?? string.Empty;
        _keepDip = originalGame?.KeepDip ?? false;
    }
    
    private string? _editableName;

    public string? EditableName
    {
        get => _editableName;
        set
        {
            if (_editableName == value) return;
            _editableName = value;
            OnPropertyChanged(nameof(EditableName));
        }
    }
    
    public string IsoDir
    {
        get => _isoDir;
        set
        {
            if (_isoDir == value) return;
            _isoDir = value;
            OnPropertyChanged(nameof(IsoDir));
        }
    }

    public string ExtDir
    {
        get => _extDir;
        set
        {
            if (_extDir == value) return;
            _extDir = value;
            OnPropertyChanged(nameof(ExtDir));
        }
    }

    public string ModDir
    {
        get => _modDir;
        set
        {
            if (_modDir == value) return;
            _modDir = value;
            OnPropertyChanged(nameof(ModDir));
        }
    }

    public bool KeepDip
    {
        get => _keepDip;
        set
        {
            if (_keepDip == value) return;
            _keepDip = value;
            OnPropertyChanged(nameof(KeepDip));
        }
    }
}