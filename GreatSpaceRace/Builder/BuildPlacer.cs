using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using Forge.Core.Rendering;
using Forge.Core.Rendering.Cameras;
using Forge.Core.Resources;
using Forge.Core.Space;
using Forge.Core.Utilities;
using GreatSpaceRace.Constants;
using GreatSpaceRace.Ships;
using GreatSpaceRace.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Builder
{
    public class BuildPlacer : Component, IInit, ITick, IRenderable
    {
        private ShipSectionRenderer _shipRenderer;

        [Inject] RayCollider RayCollider { get; set; }
        [Inject] GraphicsDevice GraphicsDevice { get; set; }
        [Inject] KeyControls KeyControls { get; set; }
        [Inject] MouseControls MouseControls { get; set; }
        [Inject] ResourceManager<SoundEffect> SoundEffects { get; set; }

        Camera Camera { get; }
        Transform CameraTransform { get; }

        private readonly ShipTopology _shipTopology;
        private readonly ProductionLine _productionLine;

        public uint RenderOrder { get; set; } = 100;

        public bool AutoRender { get; set; } = true;

        public BuildNode HoverNode { get; set; } = null;
        public Section PlacingSection { get; private set; }
        

        public BuildPlacer(Camera camera, ShipTopology shipTopology, ProductionLine productionLine)
        {
            Camera = camera;
            CameraTransform = camera.Entity.Get<Transform>();
            _shipTopology = shipTopology;
            _productionLine = productionLine;
        }

        public void StartPlacing(Section section)
        {
            PlacingSection = section;
        }

        public void CancelPlacing()
        {
            PlacingSection = null;
        }

        public void AttemptConfirmPlacement()
        {

        }

        public void Initialise()
        {
            _shipRenderer = Entity.Add(new ShipSectionRenderer());
        }

        //private (Point point, BuildNode buildNode) GetSelectedGridPoint()
        //{
        //}

        public void Tick(TickContext context)
        {
            var mouse = Mouse.GetState();
            var screenPos = new Vector2(
                mouse.X / (float)GraphicsDevice.Viewport.Width,
                1.0f - (mouse.Y / (float)GraphicsDevice.Viewport.Height)
            ) * 2 - new Vector2(1, 1);
            //Console.WriteLine($"screen {screenPos}");

            //Console.WriteLine(screenPos);
            //Console.WriteLine(worldDirectionVector);
            var ray = Camera.CreateRay(screenPos);
            var res = RayCollider.RayCast(ray, (byte)HitLayers.BuildTile);
            // Console.WriteLine($"Location: {res.location}");

            var buildNode = res.entity?.Get<BuildNode>();
            if (buildNode != null)
            {
                //Console.WriteLine($"{buildNode.GridLocation}");
            }
            this.Update(() => HoverNode = buildNode);

            if (PlacingSection != null)
            {
                if (KeyControls.HasBeenPressed(Keys.R))
                {
                    PlacingSection.Rotate(1);
                }
                if (MouseControls.RightClicked)
                {
                    CancelPlacing();
                }
                else if (buildNode != null && MouseControls.LeftClicked)
                {
                    var gridLocation = buildNode.GridLocation;
                    if (_shipTopology.SectionAt(gridLocation) == null
                        && _shipTopology.LegalSectionCheck(PlacingSection, gridLocation))
                    {
                        this.Update(() =>
                        {
                            _shipTopology.SetSection(gridLocation, PlacingSection);
                            _productionLine.Remove(PlacingSection);
                            PlacingSection = null;
                            SoundEffects.Get("Hammer")?.Play();
                        });
                    }else
                    {

                        SoundEffects.Get("Click")?.Play();
                    }
                }
            }
        }

        public void Render(RenderContext context)
        {
            if (PlacingSection != null)
            {
                if (HoverNode != null) {
                    _shipRenderer.Render(
                        context,
                        new Transform
                        {
                            Location = HexagonHelpers.GetGridWorldPosition(HoverNode.GridLocation)
                        }.WorldTransform,
                        PlacingSection
                    );
                }
            }
        }
    }
}
