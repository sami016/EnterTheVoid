using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using Forge.Core.Rendering;
using Forge.Core.Rendering.Cameras;
using Forge.Core.Space.Shapes;
using Forge.Core.Utilities;
using GreatSpaceRace.Flight;
using GreatSpaceRace.Projectiles;
using GreatSpaceRace.Ships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Phases.Asteroids
{
    public class Asteroid : AsteroidBase
    {
        private static Model _asteroid1;

        public override void Initialise()
        {
            if (_asteroid1 == null)
            {
                _asteroid1 = Content.Load<Model>("Models/asteroid1");
                _asteroid1.EnableDefaultLighting();
                _asteroid1.SetDiffuseColour(Color.RosyBrown);
            }
            base.Initialise();
        }

        public override void Render(RenderContext context)
        {
            context.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            var camera = CameraManager.ActiveCamera;
            _asteroid1.Draw(Transform.WorldTransform, camera.View, camera.Projection);
        }

        //public override void OnHit(FlightNode node, FlightShip ship, Point gridLocation, Vector3 nodeLocation, Section section)
        //{
        //    ship.Damage(gridLocation, 10);
        //    Entity.Delete();
        //}

        //public override void OnHit(Entity projectileEntity, Projectile projectile)
        //{
        //    Entity.Delete();
        //}
    }
}
