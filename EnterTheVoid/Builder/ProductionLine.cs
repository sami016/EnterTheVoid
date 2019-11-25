using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using Forge.Core.Utilities;
using EnterTheVoid.Ships;
using EnterTheVoid.Ships.Generation;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnterTheVoid.Builder
{
    public class ProductionLine : Component, IInit, ITick
    {
        public SectionGenerator SectionGenerator { get; set; } = new SectionGenerator(StandardGeneration.Distribution);

        private CompletionTimer _partTimer = new CompletionTimer(TimeSpan.FromSeconds(4));
        private CompletionTimer _shiftMovetimer = new CompletionTimer(TimeSpan.FromSeconds(1));

        private CompletionTimer _redrawCooldownTimer = new CompletionTimer(TimeSpan.FromSeconds(5));

        public IList<Section> Line { get; set; } = new List<Section>();
        public float SectionFractionalProgress => _shiftMovetimer.CompletedFraction;

        public void Initialise()
        {
            _redrawCooldownTimer.Complete();
            for (var i=0; i<7; i++)
            {
                Generate();
            }
        }

        public void Tick(TickContext context)
        {
            _partTimer.Tick(context.DeltaTime);
            _shiftMovetimer.Tick(context.DeltaTime);
            if (_partTimer.Completed)
            {
                _partTimer.Restart();
                _shiftMovetimer.Restart();
                Generate();
            } else
            {
                //Console.WriteLine($"Next in {_partTimer.RemainingTime.TotalMilliseconds} ms");
            }

            var keys = Keyboard.GetState();
            if (_redrawCooldownTimer.Completed && keys.IsKeyDown(Keys.Q))
            {
                Shuffle();
            }else
            {
                _redrawCooldownTimer.Tick(context.DeltaTime);
            }
        }

        private void Shuffle()
        {
            this.Update(() =>
            {
                _redrawCooldownTimer.Restart();
                Line.Clear();
                for (var i = 0; i < 7; i++)
                {
                    Generate();
                }
            });
        }

        private void Generate()
        {
            var generated = SectionGenerator.Generate();
            this.Update(() =>
            {
                Line.Add(generated);
                if (Line.Count() > 7)
                {
                    Line.RemoveAt(0);
                }
            });
        }

        public void Remove(Section placingSection)
        {
            this.Update(() =>
            {
                Line.Remove(placingSection);
            });
        }
    }
}
