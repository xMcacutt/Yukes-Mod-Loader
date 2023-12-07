using System;
using System.Collections.Generic;
using System.IO;
using Path = System.Windows.Shapes.Path;

namespace Yukes_Mod_Loader_GUI.GUI.Models;

public class Mod
{
    public string Game;
    public string Name;
    public List<string> Exclusions;
    public string Date;

    public Mod(string name, string game, string date, List<string> exclusions)
    {
        Game = game;
        Name = name;
        Date = date;
        Exclusions = exclusions;
    }
}