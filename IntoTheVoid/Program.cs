using Forge.Core;
using Forge.Core.Debugging;
using Forge.Core.Sound;
using Forge.Core.Utilities;
using Forge.UI.Glass;
using IntoTheVoid.Scenes;
using Microsoft.Xna.Framework;
using System;

namespace IntoTheVoid
{
    class Program
    {
        static ForgeGame Game => new ForgeGameBuilder()
                .UseEnginePrimitives()
                .UseCollisionPrimitives()
                .UseGlassUI()
                .WithInitialScene(() => new StartupScene())
                .AddSingleton(() => new GlobalControls())
                .AddSingleton(() => new KeyControls())
                .AddSingleton(() => new MouseControls())
                .AddSingleton(() => new MusicManager())
                //.AddSingleton(() => new FpsDebugger())
                .UseBackgroundRefreshColour(Color.Black)
                .Create(1);
        
        [STAThread]
        static void Main(string[] args)
        {
            using (var game = Game)
                game.Run();
        }
    }
}
