using IntoTheVoid.Ships.Connections;
using IntoTheVoid.Ships.Modules;
using IntoTheVoid.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntoTheVoid.Ships
{
    /// <summary>
    /// A ship section.
    /// </summary>
    public class Section
    {
        public int Rotation { get; private set; } = Direction.East;
        public Module Module { get; }
        public ConnectionLayout ConnectionLayout { get; }
        public int Health { get; set; }
        public Point GridLocation { get; internal set; }

        public Section(Module module, ConnectionLayout connectionLayout, int rotation = 0)
        {
            Module = module;
            ConnectionLayout = connectionLayout;
            Rotation = rotation;

            Health = Module.MaxHealth;
        }

        public void Rotate(int amount)
        {
            Rotation = ((Rotation) + amount) % 6;
        }

        public void Damage(int amount)
        {
            Health -= amount;
            if (Health < 0)
            {
                Health = 0;
            }
        }

        public void Repair(int amount)
        {
            Health += amount;
            if (Health > Module.MaxHealth)
            {
                Health = Module.MaxHealth;
            }
        }
    }
}
