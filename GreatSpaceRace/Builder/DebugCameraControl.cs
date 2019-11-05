using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using Forge.Core.Rendering.Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Builder
{
    public class DebugCameraControl : Component, ITick
    {
        public float MoveSpeed { get; set; } = 3f;

        [Inject] Transform Transform { get; set; }
        [Inject] Camera Camera { get; set; }

        public void Tick(TickContext context)
        {
            var speed = new Vector3(0, 0, 0);
            var keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.A))
            {
                speed += new Vector3(-1, 0, 0);
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                speed += new Vector3(1, 0, 0);
            }
            if (keyboard.IsKeyDown(Keys.W))
            {
                speed += new Vector3(0, 1, 0);
            }
            if (keyboard.IsKeyDown(Keys.S))
            {
                speed += new Vector3(0, -1, 0);
            }

            speed = Vector3.Transform(speed, Camera.Position.Rotation);
            speed *= MoveSpeed;

            Transform.Update(() =>
            {
                Transform.Location += speed * context.DeltaTimeSeconds;
                Camera.Recalculate();
            });
        }
    }
}
