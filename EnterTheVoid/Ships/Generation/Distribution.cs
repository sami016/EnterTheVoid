using System;
using System.Collections.Generic;
using System.Linq;

namespace IntoTheVoid.Ships.Generation
{
    public class Distribution<T> : List<(float probability, T value)>
    {
        private static Random _random = new Random();
        public float Total => this
            .Select(x => x.probability)
            .Aggregate((x, y) => x + y);

        public T Sample()
        {
            var r = (float)(_random.NextDouble() * Total);
            var total = 0f;
            foreach (var entry in this)
            {
                total += entry.probability;
                if (total > r)
                {
                    return entry.value;
                }
            }
            return this.Last().value;
        }
    }
}
