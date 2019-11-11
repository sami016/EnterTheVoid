using Forge.Core;
using Forge.Core.Debugging;
using Forge.Core.Sound;
using Forge.Core.Utilities;
using Forge.UI.Glass;
using GreatSpaceRace.Scenes;
using Microsoft.Xna.Framework;
using System;

namespace GreatSpaceRace
{
    class Program
    {
        static ForgeGame Game => new ForgeGameBuilder()
                .UseEnginePrimitives()
                .UseCollisionPrimitives()
                .UseGlassUI()
                .WithInitialScene(() => new DebugMenuScene())
                .AddSingleton(() => new GlobalControls())
                .AddSingleton(() => new KeyControls())
                .AddSingleton(() => new MouseControls())
                .AddSingleton(() => new MusicManager())
                //.AddSingleton(() => new FpsDebugger())
                .UseBackgroundRefreshColour(Color.Black)
                .Create();
        
        [STAThread]
        static void Main(string[] args)
        {
            using (var game = Game)
                game.Run();
        }
    }
}
