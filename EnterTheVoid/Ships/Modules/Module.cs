using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Ships.Modules
{
    /// <summary>
    /// Represents a type of ship module.
    /// </summary>
    public abstract class Module
    {
        public abstract string FullName { get; }
        public abstract string ShortName { get; }
        public abstract string DescriptionShort { get; }

        public virtual int MaxHealth { get; set; } = 100;
    }
}
