namespace DIPTools;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Binft;
using System.Text;
using System.Threading.Tasks;

public class DIP
{
    public uint EntryCount;
    public uint EntryListLength;
    public uint NameTableLength;
    public uint FileDataLength;
    public uint EntryListOffset;
    public uint NameTableOffset;
    public uint FileDataOffset;
}

public class Dir
{
    public List<Dir> Directories = new List<Dir>();
    public List<FileEntry> Files = new List<FileEntry>();
    public string Name = "";
    public string Path = "";
    public int RunningCount;
    public int SubEntryCount;
    public int NameIndex;
}

public class FileEntry
{
    public string Name = "";
    public uint Size;
    public uint DataOffset;
    public string Path = "";
    public int NameIndex;
}

internal static class Repacker
{
    private static int _currentNameIndex;
    private static int _runningCount = 1;
    private static readonly Dictionary<string, int> FileNames = new();

    public static async Task Repack(string baseDirPath, string outPath)
    {
        _currentNameIndex = 0;
        Dir root = new();
        root.Name = "root";
        root.RunningCount = _runningCount;
        root.SubEntryCount = Directory.GetFileSystemEntries(baseDirPath).Length;
        _runningCount += root.SubEntryCount;
        var entries = GetEntryData(baseDirPath);
        root.Files = entries.Item1;
        root.Directories = entries.Item2;
        root.NameIndex = _currentNameIndex;
        _currentNameIndex++;

        using MemoryStream fileDataStream = new();
        foreach (var directory in root.Directories)
        {
            GenerateFileData(directory, fileDataStream);
        }

        foreach (var file in root.Files)
        {
            file.DataOffset = (uint)fileDataStream.Position;
            using var fileStream = File.OpenRead(file.Path);
            fileStream.CopyTo(fileDataStream);
        }

        FileNames.Clear();
        using MemoryStream nameTableStream = new();
        FileNames.Add("root", _currentNameIndex);
        nameTableStream.Write(Encoding.ASCII.GetBytes("root"));
        var remByteCount = 32 - 4;
        nameTableStream.Write(new byte[remByteCount]);
        for (var fileIndex = 0; fileIndex < root.Files.Count; fileIndex++)
        {
            var f = root.Files[fileIndex];
            if (!FileNames.ContainsKey(f.Name))
            {
                FileNames.Add(f.Name, _currentNameIndex);
                nameTableStream.Write(Encoding.ASCII.GetBytes(f.Name));
                remByteCount = 32 - f.Name.Length;
                nameTableStream.Write(new byte[remByteCount]);
                _currentNameIndex++;
            }

            root.Files[fileIndex].NameIndex = FileNames[f.Name];
        }

        for (var dirIndex = 0; dirIndex < root.Directories.Count; dirIndex++)
        {
            var d = root.Directories[dirIndex];
            if (!FileNames.ContainsKey(d.Name))
            {
                FileNames.Add(d.Name, _currentNameIndex);
                var fileNameBytes = new byte[32];
                Encoding.ASCII.GetBytes(d.Name, 0, Math.Min(32, d.Name.Length), fileNameBytes, 0);
                nameTableStream.Write(fileNameBytes);
                _currentNameIndex++;
            }

            root.Directories[dirIndex].NameIndex = FileNames[d.Name];

            GenerateNameTable(nameTableStream, root.Directories[dirIndex]);
        }

        using MemoryStream entryListStream = new();
        entryListStream.Write(new byte[2]
            .Concat(BitConverter.GetBytes((ushort)root.NameIndex))
            .Concat(BitConverter.GetBytes((uint)root.SubEntryCount))
            .Concat(BitConverter.GetBytes(root.RunningCount)).ToArray());
        foreach (var dir in root.Directories)
        {
            entryListStream.Write(new byte[2]
                .Concat(BitConverter.GetBytes((ushort)dir.NameIndex))
                .Concat(BitConverter.GetBytes((uint)dir.SubEntryCount))
                .Concat(BitConverter.GetBytes(dir.RunningCount)).ToArray());
        }

        foreach (var file in root.Files)
        {
            entryListStream.Write(new byte[] { 0xFF, 0x00 }
                .Concat(BitConverter.GetBytes((ushort)file.NameIndex))
                .Concat(BitConverter.GetBytes(file.Size))
                .Concat(BitConverter.GetBytes(file.DataOffset)).ToArray());
        }

        foreach (var dir in root.Directories)
        {
            GenerateEntryList(entryListStream, dir);
        }

        //COMBINE DATA AND GENERATE HEADER
        DIP dip = new()
        {
            EntryListLength = (uint)entryListStream.Length,
            NameTableLength = (uint)nameTableStream.Length,
            FileDataLength = (uint)fileDataStream.Length,
            EntryListOffset = 0x20
        };
        dip.NameTableOffset = dip.EntryListOffset + dip.EntryListLength;
        dip.FileDataOffset = dip.NameTableOffset + dip.NameTableLength;

        const uint multiple = 0x800;
        var end = (dip.FileDataOffset + multiple - 1) / multiple * multiple;
        var paddingCount = end - dip.FileDataOffset;
        dip.FileDataOffset = end;

        MemoryStream headerStream = new();
        headerStream.Write(BitConverter.GetBytes(dip.EntryListLength)
            .Concat(BitConverter.GetBytes(dip.NameTableLength))
            .Concat(BitConverter.GetBytes(dip.FileDataLength))
            .Concat(BitConverter.GetBytes(dip.EntryListOffset))
            .Concat(BitConverter.GetBytes(dip.NameTableOffset))
            .Concat(BitConverter.GetBytes(dip.FileDataOffset))
            .Concat(BitConverter.GetBytes(1))
            .Concat(BitConverter.GetBytes(0))
            .ToArray());

        await using var outStream = File.OpenWrite(outPath);
        outStream.Write(headerStream.ToArray()
            .Concat(entryListStream.ToArray())
            .Concat(nameTableStream.ToArray())
            .Concat(new byte[paddingCount])
            .Concat(fileDataStream.ToArray())
            .ToArray());
    }

    private static (List<FileEntry>, List<Dir>) GetEntryData(string path)
    {
        var files = new List<FileEntry>();
        var dirs = new List<Dir>();
        var entries = Directory.GetFileSystemEntries(path);
        foreach (var entry in entries)
        {
            if (File.Exists(entry))
            {
                FileEntry file = new()
                {
                    Name = Path.GetFileName(entry),
                    Path = entry,
                    Size = (uint)new FileInfo(entry).Length / 0x20
                };
                files.Add(file);
            }
            else if (Directory.Exists(entry))
            {
                Dir subDir = new();
                var dirInfo = new DirectoryInfo(entry);
                subDir.Name = dirInfo.Name;
                subDir.RunningCount = _runningCount;
                subDir.SubEntryCount = Directory.GetFileSystemEntries(entry).Length;
                _runningCount += subDir.SubEntryCount;
                var subEntries = GetEntryData(entry);
                subDir.Path = entry;
                subDir.Files = subEntries.Item1;
                subDir.Directories = subEntries.Item2;
                dirs.Add(subDir);
            }
        }

        return (files, dirs);
    }

    private static void GenerateFileData(Dir dir, Stream stream)
    {
        foreach (var subDirectory in dir.Directories)
        {
            GenerateFileData(subDirectory, stream);
        }

        foreach (var file in dir.Files)
        {
            file.DataOffset = (uint)stream.Position;
            using var fileStream = File.OpenRead(file.Path);
            fileStream.CopyTo(stream);
        }
    }

    private static void GenerateNameTable(MemoryStream stream, Dir dir)
    {
        for (var fileIndex = 0; fileIndex < dir.Files.Count; fileIndex++)
        {
            var f = dir.Files[fileIndex];
            if (!FileNames.ContainsKey(f.Name))
            {
                FileNames.Add(f.Name, _currentNameIndex);
                var fileNameBytes = new byte[32];
                Encoding.ASCII.GetBytes(f.Name, 0, Math.Min(32, f.Name.Length), fileNameBytes, 0);
                stream.Write(fileNameBytes);
                _currentNameIndex++;
            }

            dir.Files[fileIndex].NameIndex = FileNames[f.Name];
        }

        for (var dirIndex = 0; dirIndex < dir.Directories.Count; dirIndex++)
        {
            var d = dir.Directories[dirIndex];
            if (!FileNames.ContainsKey(d.Name))
            {
                FileNames.Add(d.Name, _currentNameIndex);
                var dirNameBytes = new byte[32];
                Encoding.ASCII.GetBytes(d.Name, 0, Math.Min(32, d.Name.Length), dirNameBytes, 0);
                stream.Write(dirNameBytes);
                _currentNameIndex++;
            }

            dir.Directories[dirIndex].NameIndex = FileNames[d.Name];

            GenerateNameTable(stream, dir.Directories[dirIndex]);
        }
    }

    private static void GenerateEntryList(MemoryStream stream, Dir dir)
    {
        foreach (var subDir in dir.Directories)
        {
            stream.Write(new byte[] { 0x0, 0x0 }
                .Concat(BitConverter.GetBytes((ushort)subDir.NameIndex))
                .Concat(BitConverter.GetBytes((uint)subDir.SubEntryCount))
                .Concat(BitConverter.GetBytes(subDir.RunningCount))
                .ToArray());
        }

        foreach (var file in dir.Files)
        {
            stream.Write(new byte[] { 0xFF, 0x00 }
                .Concat(BitConverter.GetBytes((ushort)file.NameIndex))
                .Concat(BitConverter.GetBytes(file.Size))
                .Concat(BitConverter.GetBytes(file.DataOffset))
                .ToArray());
        }

        foreach (var subDir in dir.Directories)
        {
            GenerateEntryList(stream, subDir);
        }
    }
}

public static class Extractor
{
    public static async Task Extract(string path, string outDir)
    {
        var binf = Binft.OpenBinf(path, true);
        var dip = new DIP
        {
            EntryListLength = binf.ReadUInt(),
            NameTableLength = binf.ReadUInt(),
            FileDataLength = binf.ReadUInt(),
            EntryListOffset = binf.ReadUInt(),
            NameTableOffset = binf.ReadUInt(),
            FileDataOffset = binf.ReadUInt()
        };
        dip.EntryCount = dip.EntryListLength / 0xC;
        binf.GoTo(dip.EntryListOffset);

        Dir root = new();
        int directoryIndicator = binf.ReadShort();
        int nameIndex = binf.ReadUShort();
        var pos = binf.Position;
        binf.GoTo(dip.NameTableOffset + (0x20 * nameIndex));
        root.Name = binf.ReadString();
        binf.GoTo(pos);
        root.SubEntryCount = binf.ReadInt();
        binf.Skip(4);

        HandleEntry(binf, dip, root);
        GenerateFiles(root, dip, binf, outDir);
        binf.Close();
    }

    private static void HandleEntry(Binf binf, DIP dip, Dir root)
    {
        for (var i = 0; i < root.SubEntryCount; i++)
        {
            int directoryIndicator = binf.ReadShort();
            int nameIndex = binf.ReadUShort();
            var pos = binf.Position;
            binf.GoTo(dip.NameTableOffset + (0x20 * nameIndex));
            var name = binf.ReadString();
            binf.GoTo(pos);
            if (directoryIndicator == 0)
            {
                Dir subDir = new()
                {
                    Name = name,
                    SubEntryCount = binf.ReadInt()
                };
                binf.Skip(4);
                root.Directories.Add(subDir);
                continue;
            }
            FileEntry file = new()
            {
                Name = name,
                Size = binf.ReadUInt() * 0x20,
                DataOffset = binf.ReadUInt()
            };
            root.Files.Add(file);   
        }
        foreach (var dir in root.Directories)
        {
            HandleEntry(binf, dip, dir);
        }
    }

    private static void GenerateFiles(Dir dir, DIP dip, Binf binf, string outDir)
    {
        Directory.CreateDirectory(Path.Combine(outDir, dir.Name));
        foreach (var subDir in dir.Directories)
        {
            GenerateFiles(subDir, dip, binf, Path.Combine(outDir, dir.Name));
        }
        foreach (var file in dir.Files)
        {
            var o = File.Create(Path.Combine(outDir, dir.Name, file.Name));
            binf.GoTo(dip.FileDataOffset + file.DataOffset);
            o.Write(binf.ReadBytes((int)file.Size));
            o.Close();
        }
    }
}