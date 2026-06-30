using System;
using System.IO;
using System.Threading.Tasks;
using MultiBoxBot.Core;
using MultiBoxBot.Models;
using Newtonsoft.Json;

namespace MultiBoxBot
{
    class Program
    {
        private static WindowManager _windowManager;
        private static CommandBroadcaster _broadcaster;
        private static GameSynchronizer _synchronizer;
        private static GameConfig _gameConfig;

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║  Ngọc Rồng Online - Multi-Box Bot v1.0.0      ║");
            Console.WriteLine("║  Tác giả: Finn10205                            ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
            Console.WriteLine();

            try
            {
                LoadGameConfig();

                _windowManager = new WindowManager("NRO");
                _broadcaster = new CommandBroadcaster(_windowManager);
                _synchronizer = new GameSynchronizer(_gameConfig, _windowManager, _broadcaster);

                _broadcaster.OnBroadcastLog += (s, msg) => Console.WriteLine(msg);
                _synchronizer.OnSyncLog += (s, msg) => Console.WriteLine(msg);

                _synchronizer.Initialize();
                Console.WriteLine();

                await RunMainLoop();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static void LoadGameConfig()
        {
            Console.WriteLine("[*] Loading game configuration...");

            string configPath = "Config/settings.json";

            if (!File.Exists(configPath))
            {
                throw new FileNotFoundException($"Config file not found: {configPath}");
            }

            string json = File.ReadAllText(configPath);
            _gameConfig = JsonConvert.DeserializeObject<GameConfig>(json);

            Console.WriteLine($"✅ Config loaded: {_gameConfig.Metadata.Description}");
            Console.WriteLine($"   Version: {_gameConfig.Metadata.Version}");
            Console.WriteLine($"   Author: {_gameConfig.Metadata.Author}");
        }

        static async Task RunMainLoop()
        {
            Console.WriteLine("[*] Scanning for game windows...");
            var windows = _windowManager.ScanGameWindows();

            if (windows.Count == 0)
            {
                Console.WriteLine("⚠️  No game windows found. Please open Ngọc Rồng Online.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║           Available Hotkeys                    ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");

            foreach (var skill in _gameConfig.Skills)
            {
                if (skill.Enabled)
                {
                    Console.WriteLine($"  {skill.Hotkey} - {skill.Name} (Cooldown: {skill.Cooldown}ms)");
                }
            }

            Console.WriteLine($"  Ctrl+A - Broadcast Attack to all windows");
            Console.WriteLine($"  Ctrl+S - Toggle Sync");
            Console.WriteLine();

            Console.WriteLine("Ready to broadcast! Press hotkeys to execute...");
            Console.WriteLine("(Press 'Esc' to exit)\n");

            Console.WriteLine("[Demo] Testing command broadcaster...");
            await _broadcaster.BroadcastSkillCommand(
                skillId: 1,
                skillName: "Quyết Chiến",
                hotkey: "Q",
                cooldown: 5000,
                delay: 100
            );

            while (true)
            {
                System.Threading.Thread.Sleep(1000);

                var activeCount = _windowManager.GetActiveWindowCount();
                if (activeCount == 0)
                {
                    Console.WriteLine("⚠️  No active windows found. Waiting...");
                }
            }
        }
    }
}
