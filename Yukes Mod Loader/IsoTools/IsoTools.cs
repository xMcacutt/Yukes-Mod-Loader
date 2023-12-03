using Ps2IsoTools.UDF;
using Ps2IsoTools.UDF.Files;

namespace IsoTools;

internal static class FileExtractor 
{
    public static bool ExtractDipFromIso(string isoPath, string outPath)
    {
        using var reader = new UdfReader(isoPath);
        var fileNames = reader.GetAllFileFullNames();
        var dip = fileNames.FirstOrDefault(x => x.EndsWith(".dip", StringComparison.CurrentCultureIgnoreCase));
        if (dip == null)
        {
            Console.WriteLine("ISO does not contain .DIP file.");
            return false;
        }
        var dipId = reader.GetFileByName(dip);
        if (dipId == null)
        {
            Console.WriteLine("Something went wrong.");
            return false;
        }
        reader.CopyFile(dipId, outPath);
        reader.Dispose();
        return true;
    }
    
}

internal static class IsoBuilder
{
    
    public static async Task Build(string path, string gameName, string outDir)
    {
        var builder = new UdfBuilder() { VolumeIdentifier = gameName };
        
        foreach(var file in Directory.GetFiles(path)) builder.AddFile(Path.GetFileName(file), File.ReadAllBytes(file));
            
        foreach(var directory in Directory.GetDirectories(path))
        {
            var subDirInfo = new DirectoryInfo(directory);
            builder.AddDirectory(subDirInfo.Name);
                
            foreach (var file in Directory.GetFiles(directory))
            {
                using var fs = File.Open(file, FileMode.Open);
                builder.AddFile(subDirInfo.Name + '\\' + Path.GetFileName(file), fs, true);
            }
            RecursiveAdd(builder, directory, path);
        }
        builder.Build(Path.Combine(outDir, gameName + ".iso"));
    }

    private static void RecursiveAdd(UdfBuilder builder, string dirPath, string rootPath)
    {
        foreach (var directory in Directory.GetDirectories(dirPath))
        {
            var relPath = Path.GetRelativePath(rootPath, directory);
            builder.AddDirectory(relPath);
            foreach (var file in Directory.GetFiles(directory))
            {
                using var fs = File.Open(file, FileMode.Open);
                builder.AddFile(Path.Combine(relPath, Path.GetFileName(file)), fs, true);
            }
            RecursiveAdd(builder, directory, rootPath);
        }
    }
}