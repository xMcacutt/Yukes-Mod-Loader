using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Yukes_Mod_Loader_GUI.GUI;

public class GameSettings
{
    public string? Name { get; set; }
    public string? IsoDir { get; set; }
    public string? ExtDir { get; set; }
    public string? ModDir { get; set; }
    public bool KeepDip { get; set; }

    public GameSettings(string name, string isoDir, string extDir, string modDir, bool keepDip)
    {
        Name = name;
        IsoDir = isoDir;
        ExtDir = extDir;
        ModDir = modDir;
        KeepDip = keepDip;
    }
}

public class SettingsHandler
{
    public static Dictionary<string, GameSettings> Games { get; set; } = new();

    public static void Initialize()
    {
        var jsonContent = System.IO.File.ReadAllText("./GameSettings.json");
        var gameSettingsList = JsonConvert.DeserializeObject<List<GameSettings>>(jsonContent);
        Games = gameSettingsList.ToDictionary(game => game.Name, game => game);
    }

    public static void Save()
    {
        var jsonContent = JsonConvert.SerializeObject(Games.Values, Formatting.Indented);
        System.IO.File.WriteAllText("./GameSettings.json", jsonContent);
    }
}