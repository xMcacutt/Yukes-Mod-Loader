using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Yukes_Mod_Loader_GUI.GUI.Models;
using Yukes_Mod_Loader_GUI.GUI.ViewModels;

namespace Yukes_Mod_Loader_GUI.GUI;

public class SettingsHandler
{
    public static ObservableCollection<GameViewModel> LoadGames()
    {
        var games = new ObservableCollection<GameViewModel>();

        try
        {
            var json = File.ReadAllText("./GameSettings.json");
            var gameList = JsonConvert.DeserializeObject<List<Game>>(json);
            games = new ObservableCollection<GameViewModel>(
                gameList?.Select(game => new GameViewModel(game))
            );
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., file not found, invalid JSON, etc.)
            Console.WriteLine($"Error loading games from JSON: {ex.Message}");
        }

        return games;
    }

    public static ObservableCollection<ModViewModel> LoadMods(string gameFilter = "All")
    {
        var mods = new ObservableCollection<ModViewModel>();
        var modDirs = new List<string>();
        if (gameFilter == "All")
        {
            foreach (var game in LoadGames())
            {
                if (!Directory.Exists(game.ModDir)) continue;
                modDirs = Directory.GetDirectories(game.ModDir)
                    .Where(folder => Directory.GetFiles(folder, "mod.json").Any()).ToList();
            }
        }
        else
        {
            var game = LoadGames().FirstOrDefault(g => g.EditableName == gameFilter);
            if (game != null && !string.IsNullOrEmpty(game.ModDir))
            {
                modDirs.Add(game.ModDir);
            }
        }
        foreach (var folder in modDirs)
        {
            {
                var json = File.ReadAllText(Path.Combine(folder, "mod.json"));
                var mod = JsonConvert.DeserializeObject<Mod>(json);
                if (mod != null) mods.Add(new ModViewModel(mod));
            }
        }
        return mods;
    }
}