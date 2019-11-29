using Forge.Core.Components;
using Forge.Core.Scenes;
using EnterTheVoid.Constants;
using EnterTheVoid.Phases;
using EnterTheVoid.Phases.Asteroids;
using EnterTheVoid.Phases.Combat;
using EnterTheVoid.Phases.Open;
using EnterTheVoid.Phases.Satellite;
using EnterTheVoid.Phases.Transmission;
using EnterTheVoid.Scenes;
using EnterTheVoid.Ships;
using EnterTheVoid.Ships.Connections;
using EnterTheVoid.Ships.Modules;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using EnterTheVoid.General;

namespace EnterTheVoid.Orchestration
{
    public class Orchestrator : Component
    {
        private ShipTopology _shipTopology;
        private IDictionary<Planet, IEnumerable<Func<Phase>>> _phaseFactories = new Dictionary<Planet, IEnumerable<Func<Phase>>>();
        [Inject] SceneManager SceneManager { get; set; }
        [Inject] FadeTransition FadeTransition { get; set; }
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
                () => new GalactusBossPhase(),
                () => new AsteroidPhase(10, AsteroidDistributions.StandardAsteroidDistribution),
                () => new SwarmPhase(),
                () => new TransmissionPhase(),
                () => new BanditPhase(0),
                () => new SatellitePhase(Planet.Mars)
                //() => new DroneStrikePhase(),
                //() => new AsteroidPhase(20, AsteroidDistributions.StandardAsteroidDistribution),
                //new IceAsteroidPhase(10, AsteroidDistributions.IceAsteroidDistribution),
                //new OpenPhase(),
           );
           AddPlanet(
                Planet.Mars,
                () => new AsteroidPhase(20, AsteroidDistributions.StandardAsteroidDistribution),
                () => new DroneStrikePhase(3),
                () => new TransmissionPhase(),
                () => new StormWallBossPhase(),
                () => new SatellitePhase(Planet.Jupiter)
            );
            AddPlanet(
                Planet.Jupiter,
                () => new IceAsteroidPhase(20, AsteroidDistributions.IceAsteroidDistribution),
                () => new BanditPhase(1),
                () => new TransmissionPhase(),
                () => new AsteroidPhase(30, AsteroidDistributions.StandardAsteroidDistribution),
                () => new SatellitePhase(Planet.Saturn)
            );
            AddPlanet(
                Planet.Saturn,
                () => new IceAsteroidPhase(40, AsteroidDistributions.IceAsteroidDistribution),
                () => new TransmissionPhase(),
                () => new BanditPhase(2),
                () => new AsteroidPhase(50, AsteroidDistributions.StandardAsteroidDistribution),
                () => new SwarmPhase(),
                () => new SatellitePhase(Planet.Uranus)
            );
            AddPlanet(
                Planet.Uranus,
                () => new IceAsteroidPhase(60, AsteroidDistributions.IceAsteroidDistribution),
                () => new TransmissionPhase(),
                () => new DroneStrikePhase(5),
                () => new AsteroidPhase(70, AsteroidDistributions.StandardAsteroidDistribution),
                () => new SatellitePhase(Planet.Neptune)
            );
            AddPlanet(
                Planet.Neptune,
                () => new IceAsteroidPhase(90, AsteroidDistributions.IceAsteroidDistribution),
                () => new TransmissionPhase(),
                () => new DroneStrikePhase(6),
                () => new AsteroidPhase(100, AsteroidDistributions.StandardAsteroidDistribution),
                () => new GalactusBossPhase(),
                () => new AsteroidPhase(100, AsteroidDistributions.StandardAsteroidDistribution)
            );
        }

        public void NextBuild()
        {
            CurrentPlanet++;
            FadeTransition.StartTransition(() => SceneManager.SetScene(new BuildScene(_shipTopology, CurrentPlanet)));
        }

        public void NextFlight()
        {
            if (CurrentPlanet < Planet.Pluto && _phaseFactories.ContainsKey(CurrentPlanet))
            {
                FadeTransition.StartTransition(() => SceneManager.SetScene(new FlightScene(_shipTopology, _phaseFactories[CurrentPlanet])));
            } else
            {
                FadeTransition.StartTransition(() => SceneManager.SetScene(new WinScene()));
            }
        }

        public void PhasesComplete()
        {
            CurrentPlanet++;
            NextFlight();
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
