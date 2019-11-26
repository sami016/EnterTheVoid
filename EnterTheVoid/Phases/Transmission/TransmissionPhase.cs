using Forge.Core.Components;
using Forge.UI.Glass;
using EnterTheVoid.Flight;
using EnterTheVoid.UI.Upgrades;
using EnterTheVoid.Upgrades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnterTheVoid.Phases.Transmission
{
    class TransmissionPhase : Phase
    {
        private UIDispose _dispose;

        public static readonly UpgradeBase[] PotentialUpgrades = new UpgradeBase[]
        {
            new PrecisionRocketry(),
            new EfficientRocketry(),
            new BlastRocketry(),
            new EnhancedRotation(),
            //new ProficientRepair(),
            new HullReinforcement(),
            new ShieldAmplification(),
            new ShieldFortification()
        };

        [Inject] UserInterfaceManager UserInterfaceManager { get; set; }
        FlightShip Ship { get; set; }
        FlightCameraControl _camera;
        private float _oldCameraScale;

        public TransmissionPhase()
        {
            Title = "Incoming Transmission - Origin Unknown";
            Description = "Upgrade blueprints partially decryped. You may select an upgrade to decrypt.";
        }

        public override void Start()
        {
            Ship = Entity.EntityManager.GetAll<FlightShip>().First();
            _camera = Entity.EntityManager.GetAll<FlightCameraControl>().First();

            _oldCameraScale = _camera.CameraScale;
            _camera.CameraScale = 5f;

            var random = new Random();
            var takenUpgrades = Ship.Upgrades.ToArray();
            var possible = PotentialUpgrades
                .Where(x => takenUpgrades.All(y => y.GetType() != x.GetType()))
                .Select(x => (x, random.Next(1000)))
                .ToList() as List<(UpgradeBase, int)>;
            possible
                .Sort((x, y) => x.Item2 - y.Item2);
            var candidates = possible.Select(x => x.Item1)
                .ToArray();
            if (candidates.Length < 3)
            {
                candidates = PotentialUpgrades;
            }

            _dispose = UserInterfaceManager.Create(new UpgradeMenu(
               new List<UpgradeBase>()
               {
                    candidates[0],
                    candidates[1],
                    candidates[2]
               },
               OnSelect
            ));
        }

        private void OnSelect(UpgradeBase upgrade)
        {
            if (Ship.UpgradeSlotFree())
            {
                Ship.ApplyUpgrade(upgrade);
            }
            Ended = true;
        }

        public override void Stop()
        {
            _camera.CameraScale = _oldCameraScale;
            _dispose?.Invoke();
        }
    }
}
