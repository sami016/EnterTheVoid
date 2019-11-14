using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreatSpaceRace.Phases.Asteroids
{
    public class AsteroidSpawner : Component, IInit, ITick
    {
        private static readonly Random Random = new Random();
        private IList<Entity> _entities = new List<Entity>();

        public void Initialise()
        {
        }

        public void Tick(TickContext context)
        {
            if (_entities.Count() > 20)
            {
                return;
            }
            var ent = Entity.Create();
            ent.Add(new Transform()
            {
                Location = new Vector3((float)Random.NextDouble(), 0f, (float)Random.NextDouble()) * 20
            });
            ent.Add(new Asteroid());

            _entities.Add(ent);
        }
    }
}
