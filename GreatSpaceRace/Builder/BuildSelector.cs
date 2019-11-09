using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Rendering.Cameras;
using Forge.Core.Space;
using GreatSpaceRace.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Builder
{
    public class BuildSelector : Component, ITick
    {
        [Inject] RayCollider RayCollider { get; set; }
        [Inject] GraphicsDevice GraphicsDevice { get; set; }
        Camera Camera { get; }
        Transform CameraTransform { get; }

        public BuildSelector(Camera camera)
        {
            Camera = camera;
            CameraTransform = camera.Entity.Get<Transform>();
        }

        public void Tick(TickContext context)
        {
            var mouse = Mouse.GetState();
            var screenPos = new Vector2(
                mouse.X / (float)GraphicsDevice.Viewport.Width,
                1.0f - (mouse.Y / (float)GraphicsDevice.Viewport.Height)
            ) * 2 - new Vector2(1, 1);
            var worldDirectionVector = Camera.ScreenToWorldDirection(screenPos);

            //Console.WriteLine(screenPos);
            //Console.WriteLine(worldDirectionVector);
            var ray = new Ray(CameraTransform.Location, worldDirectionVector);
            var res = RayCollider.RayCast(ray, (byte)HitLayers.BuildTile);

            if (res.entity != null)
            {
                var buildNode = res.entity.Get<BuildNode>();
                if (buildNode != null)
                {
                    Console.WriteLine("hit "+buildNode.GridLocation);
                }
            }
        }
    }
}
