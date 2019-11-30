using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using EnterTheVoid.Projectiles;
using EnterTheVoid.Ships;
using EnterTheVoid.Upgrades;
using EnterTheVoid.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forge.Core.Resources;
using Microsoft.Xna.Framework.Audio;

namespace EnterTheVoid.Flight
{
    public class FlightShip : Component, IInit, ITick
    {
        private static Random Random = new Random();
        public Guid ShipGuid { get; }

        private readonly ShipTopology _topology;
        public ShipTopology Topology => _topology;
        public Vector3 Velocity { get; set; } = Vector3.Zero;
        public float Rotation { get; set; } = 0f;
        public float RotationalSpeed { get; set; } = 0f;
        [Inject] public Transform Transform { get; set; }
        [Inject] ResourceManager<SoundEffect> SoundEffects { get; set; }
        public int Health => _topology.Health;
        public int MaxHealth => _topology.MaxHealth;

        public int Fuel { get; private set; } = 0;
        public int Energy { get; private set; } = 0;

        public int MaxFuel => _topology.MaxFuel;
        public int MaxEnergy => _topology.MaxEnergy;

        public IEnumerable<UpgradeBase> Upgrades => _topology.Upgrades;

        private FlightNode[,] _flightNode;

        public FlightShip(ShipTopology topology, Guid? shipGuid = null)
        {
            _topology = topology;
            ShipGuid = shipGuid ?? Guid.NewGuid();
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
                        child.Add(new Transform(Transform)
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

        public Vector3 GetCenterGlobalLocation()
        {
            var sum = Vector3.Zero;
            var count = 0;
            foreach (var location in _topology.AllSections
                .Select(x => GetNodeForSection(x.GridLocation))
                .Select(x => x.GlobalLocation))
            {
                sum += location;
                count++;
            }
            return sum / count;
        }

        public IEnumerable<FlightNode> GetNodes()
        {
            var nodes = new List<FlightNode>();
            foreach (var flightNode in _flightNode)
            {
                if (flightNode.Active)
                {
                    nodes.Add(flightNode);
                }
            }
            return nodes;
        }

        public FlightNode GetRandomNode()
        {
            var nodes = GetNodes().ToArray();
            return nodes[Random.Next(nodes.Length)];
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
                if (Velocity != Vector3.Zero)
                {
                    Transform.Location += Velocity * context.DeltaTimeSeconds;
                }
                Rotation += RotationalSpeed;
                var rotationQuaternion = Quaternion.CreateFromYawPitchRoll(Rotation, 0, 0);
                Transform.Rotation = rotationQuaternion;
                //Console.WriteLine($"Ship location: {Transform.Location}   (Velocity: {Velocity})");
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
                Energy = Math.Min(MaxEnergy, Energy + amount);
            });
        }

        public bool TryTakeEnergy(int amount)
        {
            if (Energy < amount)
            {
                return false;
            }
            Energy -= amount;
            return true;
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

        public void Push(Vector3 direction, float magnitude)
        {
            this.Update(() =>
            {
                Velocity += direction * magnitude;
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
                    _topology.SectionAt(gridLocation)?.Module?.OnDestruction(this, GetNodeForSection(gridLocation));
                    _topology.Remove(gridLocation);

                    SoundEffects.Get("ChunkyExplosion")?.Play();
                    //PerformPartsAnalysis();
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

        public bool HasUpgrade<T>()
        {
            var upgrades = Upgrades
                .Select(x => x.GetType())
                .ToArray();
            return upgrades.Contains(typeof(T));
        }


        /// <summary>
        /// Used to identify a split ship.
        /// </summary>
        public void PerformPartsAnalysis()
        {
            var directions = new[] { 0, 1, 2, 3, 4, 5 };
            var available = Topology.AllSections.ToList();
            if (available.Count < 1)
            {
                return;
            }
            var clusters = new List<List<Section>>();
            while (available.Count > 0)
            {
                var localCluster = new List<Section>();
                var stack = new Stack<Section>();
                stack.Push(available[0]);
                available.RemoveAt(0);
                while (stack.Count > 0)
                {
                    var processing = stack.Pop();
                    localCluster.Add(processing);
                    foreach (var direction in directions)
                    {
                        var gridLoc = HexagonHelpers.GetFromDirection(processing.GridLocation, direction);
                        var neighbour = Topology.SectionAt(gridLoc);
                        if (neighbour != null && available.Contains(neighbour))
                        {
                            available.Remove(neighbour);
                        }
                    }
                }
                clusters.Add(localCluster);
            }

            var orderedClusters = clusters.OrderByDescending(x => x.Count()).ToArray();

            if (orderedClusters.Length > 1)
            {
                for (var i = 1; i < orderedClusters.Length; i++)
                {
                    foreach (var section in orderedClusters[i])
                    {
                        Damage(section.GridLocation, 999);
                    }
                }
            }
        }
    }
}
