using Forge.Core.Components;
using Forge.Core.Scenes;
using IntoTheVoid.Constants;
using IntoTheVoid.Phases;
using IntoTheVoid.Phases.Asteroids;
using IntoTheVoid.Phases.Combat;
using IntoTheVoid.Phases.Open;
using IntoTheVoid.Phases.Satellite;
using IntoTheVoid.Phases.Transmission;
using IntoTheVoid.Scenes;
using IntoTheVoid.Ships;
using IntoTheVoid.Ships.Connections;
using IntoTheVoid.Ships.Modules;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntoTheVoid.Orchestration
{
    public class Orchestrator : Component
    {
        private ShipTopology _shipTopology;
        private IDictionary<Planet, IEnumerable<Func<Phase>>> _phaseFactories = new Dictionary<Planet, IEnumerable<Func<Phase>>>();
        [Inject] SceneManager SceneManager { get; set; }
        public Planet CurrentPlanet { get; set; } = Planet.None;

        private void AddPlanet(Planet planet, params Func<Phase>[] phaseFactories)
        {
            _phaseFactories[planet] = phaseFactories;
        }

        public Orchestrator(ShipTopology shipTopology = null)
        {
            _shipTopology = shipTopology;
            if (_shipTopology == null)
            {
                _shipTopology = new ShipTopology(6, 5);
                _shipTopology.SetSection(new Point(2, 2), new Section(
                    new ResearchCenterModule(),
                    ConnectionLayouts.FullyConnected
                ));
            }
            AddPlanet(
                Planet.Earth,
                () => new AsteroidPhase(10, AsteroidDistributions.StandardAsteroidDistribution),
                //() => new TransmissionPhase(),
                () => new DroneStrikePhase(5),
                () => new SatellitePhase(Planet.Mars)
                //() => new DroneStrikePhase(),
                //() => new AsteroidPhase(20, AsteroidDistributions.StandardAsteroidDistribution),
                //new IceAsteroidPhase(10, AsteroidDistributions.IceAsteroidDistribution),
                //new OpenPhase(),
           );
           AddPlanet(
                Planet.Mars,
                () => new AsteroidPhase(20, AsteroidDistributions.StandardAsteroidDistribution),
                () => new TransmissionPhase(),
                () => new DroneStrikePhase(4),
                () => new SatellitePhase(Planet.Jupiter)
            );
            AddPlanet(
                Planet.Jupiter,
                () => new IceAsteroidPhase(20, AsteroidDistributions.StandardAsteroidDistribution),
                () => new TransmissionPhase(),
                () => new DroneStrikePhase(5),
                () => new AsteroidPhase(30, AsteroidDistributions.StandardAsteroidDistribution),
                () => new SatellitePhase(Planet.Saturn)
            );
            AddPlanet(
                Planet.Saturn,
                () => new IceAsteroidPhase(40, AsteroidDistributions.StandardAsteroidDistribution),
                () => new TransmissionPhase(),
                () => new DroneStrikePhase(5),
                () => new AsteroidPhase(50, AsteroidDistributions.StandardAsteroidDistribution),
                () => new SatellitePhase(Planet.Uranus)
            );
            AddPlanet(
                Planet.Uranus,
                () => new IceAsteroidPhase(60, AsteroidDistributions.StandardAsteroidDistribution),
                () => new TransmissionPhase(),
                () => new DroneStrikePhase(5),
                () => new AsteroidPhase(70, AsteroidDistributions.StandardAsteroidDistribution),
                () => new SatellitePhase(Planet.Neptune)
            );
            AddPlanet(
                Planet.Neptune,
                () => new IceAsteroidPhase(90, AsteroidDistributions.StandardAsteroidDistribution),
                () => new TransmissionPhase(),
                () => new DroneStrikePhase(5),
                () => new AsteroidPhase(100, AsteroidDistributions.StandardAsteroidDistribution),
                // TODO - end game here.
                () => new SatellitePhase(Planet.Pluto)
            );
        }

        public void NextBuild()
        {
            CurrentPlanet++;
            SceneManager.SetScene(new BuildScene(_shipTopology, CurrentPlanet));
        }

        public void NextFlight()
        {
            if (CurrentPlanet < Planet.Pluto && _phaseFactories.ContainsKey(CurrentPlanet))
            {
                SceneManager.SetScene(new FlightScene(_shipTopology, _phaseFactories[CurrentPlanet]));
            } else
            {
                SceneManager.SetScene(new MenuScene());
            }
        }

        public void Quit()
        {

        }

        public void Save()
        {

        }
       // private IDictionary<Planet, IEnumerable<Func<Phase>> _planetPhases;

            //public Orchestrator()
            //{
            //    new Phase[] {
            //            Create().Add(new SatellitePhase(Planet.Mars)),
            //            Create().Add(new DroneStrikePhase()),
            //            Create().Add(new TransmissionPhase()),
            //            Create().Add(new AsteroidPhase(20, AsteroidDistributions.StandardAsteroidDistribution)),
            //            Create().Add(new IceAsteroidPhase(10, AsteroidDistributions.IceAsteroidDistribution)),
            //            Create().Add(new OpenPhase()),
            //        }
            //}
        }
}
