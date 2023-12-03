namespace ModLoader;

public static class ModInfoHandler
{
    public static ModInfo Parse(string path)
    {
        var info = new ModInfo();
        var lines = File.ReadAllLines(path);
        for(var lineIdx = 0; lineIdx <  lines.Length; lineIdx++)
        {
            var line = lines[lineIdx];
            if (line.StartsWith("["))
            {
                switch (line)
                {
                    case "[GAME]":
                        info.Game = lines[++lineIdx];
                        Console.WriteLine(info.Game == "Default"
                            ? "Warning, Game name has not been set."
                            : $"[GAME] - {info.Game}");
                        break;
                    case "[MOD]":
                        info.Name = lines[++lineIdx];
                        Console.WriteLine(info.Name == "Default"
                            ? "Warning, Game name has not been set."
                            : $"[NAME] - {info.Name}");
                        break;
                    case "[EXCLUSIONS]":
                        Console.WriteLine("Fetching Exclusions\n[EXCLUSIONS]");
                        var excl = lines[++lineIdx];
                        while (excl != "<end>" && lineIdx + 1 < lines.Length)
                        {
                            Console.WriteLine($" - {excl}");
                            info.Exclusions.Add(excl);
                            excl = lines[++lineIdx];
                        }
                        break;
                }
            }
        }
        return info;
    }
}

public class ModInfo
{
    public string? Name;
    public  string? Game;
    public List<string> Exclusions = new ();
}