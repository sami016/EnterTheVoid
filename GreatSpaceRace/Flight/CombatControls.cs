﻿using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Utilities;
using GreatSpaceRace.Projectiles;
using GreatSpaceRace.Ships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using GreatSpaceRace.Ships.Modules;
using GreatSpaceRace.Phases.Asteroids;

namespace GreatSpaceRace.Flight
{
    public class CombatControls : Component, ITick
    {
        private readonly ShipTopology _topology;

        [Inject] FlightShip FlightShip { get; set; }
        [Inject] Transform Transform { get; set; }
        [Inject] MouseControls MouseControls { get; set; }

        public CombatControls(ShipTopology topology)
        {
            _topology = topology;
        }

        public void Tick(TickContext context)
        {
            var keys = Keyboard.GetState();
            
            if (MouseControls.LeftClicked)
            {
                FireGuns();
            }
        }

        private void FireGuns()
        {
            var guns = _topology.AllSections
                .Where(x => x.Module is BlasterModule);
            var shipTransform = Transform;
            foreach (var gun in guns)
            {
                var flightNode = FlightShip.GetNodeForSection(gun.GridLocation);
                var nodeTransform = flightNode.Entity.Get<Transform>();
                var location = Vector3.Transform(Vector3.Zero, nodeTransform.WorldTransform * shipTransform.WorldTransform);
                var shell = Entity.EntityManager.Create();
                shell.Add(new Transform
                {
                    Location = location
                });
                shell.Add(new Projectile(Vector3.Transform(Vector3.Forward, shipTransform.Rotation), 20f));
            }
        }
    }
}