using DIPTools;
using IsoTools;
using System.CommandLine;
using System.Diagnostics;
using System.Xml;

namespace ModLoader
{
    public static class Program
    {
        private static async Task Main(string[] args)
        {
            var extractDir = new Option<string>(
                name: "-e",
                description: "Path of extracted files root directory."
            ) { IsRequired = true };
            
            var isoDir = new Option<string>(
                name: "-i",
                description: "Path of directory containing iso files."
            ) { IsRequired = true };
            
            var modDir = new Option<string>(
                name: "-m",
                description: "Path of mod directory."
            ) { IsRequired = true };
            
            var keepDip = new Option<bool>(
                name: "-k",
                description: "Move DIP to mod directory after building iso? Else delete DIP."
            );

            var inPath = new Option<string>
            (
                name: "-i",
                description: "Path to DIP or ISO containing DIP."
            ) { IsRequired = true };
            
            var outPath = new Option<string>(
                name: "-o",
                description: "Path to output directory. Defaults to ./"
            );

            var rootCommand = new RootCommand("Yuke's Mod Loader");
            var extractCommand = new Command("extract", "Extract DIP from DIP or ISO path.") { inPath, outPath };
            var buildCommand = new Command("build", "Repack DIP and build ISO") { extractDir, isoDir, modDir, keepDip };
            rootCommand.Add(extractCommand);
            rootCommand.Add(buildCommand);
            extractCommand.SetHandler(ExtractDip, inPath, outPath);
            buildCommand.SetHandler(PackageMod, extractDir, isoDir, modDir, keepDip);
            

            await rootCommand.InvokeAsync(args);
        }

        private static async Task ExtractDip(string inPath, string outPath = "./")
        {
            if (!File.Exists(inPath)
                || (!inPath.EndsWith(".iso", StringComparison.CurrentCultureIgnoreCase)
                    && !inPath.EndsWith(".dip", StringComparison.CurrentCultureIgnoreCase)))
            {
                Console.WriteLine("Invalid path. Make sure path exists and is .iso or .dip.");
                Console.WriteLine("See README.md or -h for a full explanation of the usage.");
                return;
            }
            
            Console.Clear();
            Console.WriteLine("Yuke's Mod Loader");
            Console.WriteLine("DIP extraction process started");
            Console.WriteLine();
            
            bool deleteDip = false;
            if (inPath.EndsWith(".iso"))
            {
                Console.WriteLine("Extracting DIP From ISO ...");
                Console.WriteLine();
                var result = FileExtractor.ExtractDipFromIso(inPath, "./_DATA.DIP");
                inPath = "./_DATA.DIP";
                if (!result)
                    return;
                deleteDip = true;
            }
            Console.WriteLine("Extracting Files ...");
            Console.WriteLine("*WARNING* This may take several minutes ...");
            var extract = Task.Run(() => Extractor.Extract(inPath, outPath));
            var timerThread = new Thread(() => RunTimer(extract));
            timerThread.Start();
            await extract;
            timerThread.Join();
            Console.WriteLine();
            
            if(deleteDip) File.Delete(inPath);
            
            Console.WriteLine($"Files successfully extracted to {outPath}");
        }

        private static async Task PackageMod(string extractDir, string isoDir, string modDir, bool keepDip = false)
        {
            if (!Directory.Exists(extractDir) || !Directory.Exists(isoDir) || !Directory.Exists((modDir)))
            {
                Console.WriteLine("Invalid directory path(s). Make sure both paths exist.");
                Console.WriteLine("See README.md for a full explanation of the usage.");
                return;
            }
            
            Console.Clear();
            Console.WriteLine("Yuke's Mod Loader");
            Console.WriteLine("ISO Generation process started");
            Console.WriteLine();
            
            //CREATE TEMP DIRECTORY
            const string tempDir = "./TEMPDIR";
            var cancellationTokenSource = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;
                cancellationTokenSource.Cancel();
            };
            try
            {
                Console.WriteLine($"Creating temporary directory ...");
                Directory.CreateDirectory(tempDir);
                Console.WriteLine();
                
                //COPY FILES FROM EXTRACT DIRECTORY TO TEMP DIRECTORY
                Console.WriteLine($"Copying game files to temporary directory ...");
                Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(extractDir, tempDir);
                Console.WriteLine();

                //CHECK FOR MOD.INFO
                if (!File.Exists(Path.Combine(modDir, "mod.info")))
                {
                    Console.WriteLine("mod.info file not found.");
                    Console.WriteLine("See README.md for a full explanation of the usage.");
                    Directory.Delete(tempDir, true);
                    return;
                }

                //PARSE MOD.INFO
                Console.WriteLine($"Parsing mod.info ...");
                var modInfo = ModInfoHandler.Parse(Path.Combine(modDir, "mod.info"));

                //REMOVE EXCLUSIONS
                if (modInfo.Exclusions.Count != 0) Console.WriteLine("Removing exclusions ...");
                modInfo.Exclusions
                    .Where(path => File.Exists(Path.Combine(extractDir, path)))
                    .ToList()
                    .ForEach(path => File.Delete(Path.Combine(tempDir, path)));
                Console.WriteLine();

                //COPY MODDED FILES
                Console.WriteLine("Copying modified files ...");
                foreach (var sourceFilePath in Directory.GetFiles(Path.Combine(modDir, "root"), "*",
                             SearchOption.AllDirectories))
                {
                    var relativePath = Path.GetRelativePath(Path.Combine(modDir, "root"), sourceFilePath);
                    Console.WriteLine(relativePath);
                    var destinationFilePath = Path.Combine(tempDir, relativePath);
                    if (File.Exists(destinationFilePath)) File.Copy(sourceFilePath, destinationFilePath, true);
                    else
                    {
                        var destinationDirectory = Path.GetDirectoryName(destinationFilePath);
                        if (!Directory.Exists(destinationDirectory))
                            Directory.CreateDirectory(destinationDirectory);
                        File.Copy(sourceFilePath, destinationFilePath, true);
                    }
                }

                Console.WriteLine();

                //REPACK TEMP DIRECTORY
                Console.WriteLine("Repacking temporary directory to DIP ...");
                Console.WriteLine("*WARNING* This may take several minutes ...");
                var dipOutPath = Path.Combine(isoDir, "_DATA.DIP");
                if (File.Exists(dipOutPath)) File.Delete(dipOutPath);
                var repack = Task.Run(() => Repacker.Repack(tempDir, dipOutPath), cancellationTokenSource.Token);
                var timerThread = new Thread(() => RunTimer(repack));
                timerThread.Start();
                await repack;
                timerThread.Join();
                Console.WriteLine();

                //REBUILD ISO
                Console.WriteLine("Rebuilding ISO ...");
                Console.WriteLine("*WARNING* This may take several minutes ...");
                var build = Task.Run(() => IsoBuilder.Build(isoDir, modInfo.Game + " " + modInfo.Name, modDir), cancellationTokenSource.Token);
                timerThread = new Thread(() => RunTimer(build));
                timerThread.Start();
                await build;
                timerThread.Join();
                Console.WriteLine();

                //DELETE TEMP DIRECTORY
                Directory.Delete(tempDir, true);

                //DELETE _DATA.DIP
                if (keepDip)
                    File.Move(Path.Combine(isoDir, "_DATA.DIP"), Path.Combine(modDir, "_DATA.DIP"));
                else
                    File.Delete(Path.Combine(isoDir, "_DATA.DIP"));

                Console.WriteLine(
                    $"ISO successfully built at {Path.Combine(modDir, modInfo.Game + " " + modInfo.Name)}");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"Cancelled. Deleting temporary directory ... ");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Deleting temporary directory ... ");
            }
            finally
            {
                if (Directory.Exists(tempDir))
                    Directory.Delete(tempDir, true);
            }
        }
    
        
        private static void RunTimer(IAsyncResult task)
        {
            Console.CursorVisible = false;
            var stopwatch = new Stopwatch();
            Console.WriteLine("Elapsed Time: ");
            stopwatch.Start();
            while (!task.IsCompleted)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine($"Elapsed Time: {stopwatch.Elapsed.Seconds}s");
                Thread.Sleep(500); // Adjust the sleep duration as needed
            }
            stopwatch.Stop();
            Console.CursorVisible = true;
        }
    }
}
