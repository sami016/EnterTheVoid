using Forge.Core;
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
                .UseGlassUI()
                .WithInitialScene(() => new DebugMenuScene())
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
