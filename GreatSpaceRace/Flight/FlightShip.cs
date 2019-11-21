using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using GreatSpaceRace.Ships;
using GreatSpaceRace.Upgrades;
using GreatSpaceRace.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreatSpaceRace.Flight
{
    public class FlightShip : Component, IInit, ITick
    {
        private readonly ShipTopology _topology;
        public Vector3 Velocity { get; set; } = Vector3.Zero;
        [Inject] Transform Transform { get; set; }
        public int Health => _topology.Health;
        public int MaxHealth => _topology.MaxHealth;

        public int Fuel { get; private set; } = 0;
        public int Energy { get; private set; } = 0;

        public int MaxFuel => _topology.MaxFuel;
        public int MaxEnergy => _topology.MaxEnergy;

        public IEnumerable<UpgradeBase> Upgrades => _topology.Upgrades;

        private FlightNode[,] _flightNode;

        public FlightShip(ShipTopology topology)
        {
            _topology = topology;
        }

        public void Initialise()
        {
            _flightNode = new FlightNode[_topology.GridWidth, _topology.GridHeight];
            this.Update(() =>
            {
                for (var gridX = 0; gridX < _topology.GridWidth; gridX++)
                {
                    for (var gridZ = 0; gridZ < _topology.GridHeight; gridZ++)
                    {
                        var gridPosition = new Point(gridX, gridZ);
                        var child = Entity.Create();
                        child.Add(new Transform
                        {
                            Location = HexagonHelpers.GetGridWorldPosition(gridPosition),
                            Rotation = Quaternion.CreateFromYawPitchRoll(0, 0, 0)
                        });
                        _flightNode[gridX, gridZ] = child.Add(new FlightNode(this, gridPosition, _topology));
                    }
                }

                Fuel = MaxFuel;
                Energy = MaxEnergy;
            });
        }

        public FlightNode GetNodeForSection(Point gridLocation)
        {
            if (gridLocation.X < 0
                || gridLocation.X > _flightNode.GetLength(0)
                || gridLocation.Y < 0
                || gridLocation.Y > _flightNode.GetLength(1))
            {
                return null;
            }
            return _flightNode[gridLocation.X, gridLocation.Y];
        }

        public void Tick(TickContext context)
        {
            Transform.Update(() =>
            {
                Transform.Location += Velocity * context.DeltaTimeSeconds;
                Console.WriteLine($"Ship location: {Transform.Location}   (Velocity: {Velocity})");
            });
        }

        public void AddFuel(int amount)
        {
            this.Update(() =>
            {
                Fuel = Math.Min(MaxFuel, Fuel + amount);
            });
        }

        public void AddEnergy(int amount)
        {
            this.Update(() =>
            {
                Energy = Math.Min(MaxEnergy, Fuel + amount);
            });
        }

        /// <summary>
        /// Repairs the ship.
        /// </summary>
        /// <param name="amount"></param>
        public void Repair(int amount)
        {
            var repairScale = 1f;
            var upgrades = _topology.Upgrades
                .Select(x => x.GetType())
                .ToArray();
            if (upgrades.Contains(typeof(HullReinforcement)))
            {
                repairScale += 1f;
            }
            this.Update(() =>
            {
                var sections = new List<Section>();
                foreach (var section in _topology.AllSections)
                {
                    sections.Add(section);
                }
                sections.Sort((x, y) => x.Health - y.Health);

                foreach (var section in sections)
                {
                    if (section.Health == section.Module.MaxHealth)
                    {
                        continue;
                    }

                    section.Repair((int)(amount * repairScale));
                    return;
                }
            });
        }

        /// <summary>
        /// Damages a section of ship.
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="amount">amount</param>
        public void Damage(Point gridLocation, int amount)
        {
            var damageScale = 1f;
            var upgrades = _topology.Upgrades
                .Select(x => x.GetType())
                .ToArray();
            if (upgrades.Contains(typeof(HullReinforcement)))
            {
                damageScale -= 0.1f;
            }
            this.Update(() =>
            {
                var section = _topology.SectionAt(gridLocation);
                if (section == null)
                {
                    return;
                }

                section.Damage((int)(amount * damageScale));
                if (section.Health == 0)
                {
                    _topology.Remove(gridLocation);
                }
            });
        }

        public bool UpgradeSlotFree()
        {
            return _topology.UpgradeSlotFree();
        }

        public void ApplyUpgrade(UpgradeBase upgrade)
        {
            this.Update(() =>
            {
                _topology.ApplyUpgrade(upgrade);
            });
        }
    }
}
