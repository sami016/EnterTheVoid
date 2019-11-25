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

namespace EnterTheVoid.Builder
{
    public class BuildCameraControl : Component, ITick
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
                speed += Vector3.Left;
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                speed += Vector3.Right;
            }

            speed = Vector3.Transform(speed, Camera.Position.Rotation);
            if (keyboard.IsKeyDown(Keys.W))
            {
                speed += Vector3.Forward;
            }
            if (keyboard.IsKeyDown(Keys.S))
            {
                speed += Vector3.Backward;
            }
            speed *= MoveSpeed;

            Transform.Update(() =>
            {
                Transform.Location += speed * context.DeltaTimeSeconds;
                Camera?.Recalculate();
            });
        }
    }
}
