namespace Yukes_Mod_Loader_GUI.GUI.Models;

public class Game
{
    public string Name;
    public string IsoDir;
    public string ExtDir;
    public string ModDir;
    public bool KeepDip;
    
    public Game(string name, string isoDir, string extDir, string modDir, bool keepDip)
    {
        Name = name;
        IsoDir = isoDir;
        ExtDir = extDir;
        ModDir = modDir;
        KeepDip = keepDip;
    }
}