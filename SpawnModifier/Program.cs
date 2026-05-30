using System;
using System.IO;
using System.Linq;
using Server.MirEnvir;
using Server.MirDatabase;

namespace SpawnModifier
{
    class Program
    {
        static void Main(string[] args)
        {
            string targetDbPath = Path.Combine(Environment.CurrentDirectory, "Server.MirDB");
            if (!File.Exists(targetDbPath))
            {
                Console.WriteLine($"[Error] Could not find Server.MirDB in current directory: {Environment.CurrentDirectory}");
                return;
            }

            Envir envir = Envir.Main;
            if (!envir.LoadDB())
            {
                Console.WriteLine("[Error] Failed to load DB.");
                return;
            }

            Console.WriteLine("Index | Name | AI | CanTame | Level | HP | Image");
            Console.WriteLine("-------------------------------------------------------------");
            foreach (var monster in envir.MonsterInfoList)
            {
                bool isTarget = monster.Name.Contains("虎卫") || 
                               monster.Name.Contains("鹰卫") || 
                               monster.Name.Contains("Tiger") || 
                               monster.Name.Contains("Eagle") || 
                               monster.Name.Contains("Guard") ||
                               monster.CanTame;
                if (isTarget)
                {
                    Console.WriteLine($"{monster.Index} | {monster.Name} | {monster.AI} | {monster.CanTame} | {monster.Level} | {monster.Stats[Stat.HP]} | {monster.Image}");
                }
            }
        }
    }
}
