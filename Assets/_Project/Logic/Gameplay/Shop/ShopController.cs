using UnityEngine;
using _Project.Logic.Gameplay.ConfigsScripts;

namespace _Project.Logic.Meta.Shop
{
    public class ShopController
    {
        private readonly UpgradesConfig _config;

        public int CurrentDamageLevel { get; private set; }
        public int CurrentHealthLevel { get; private set; }
        public int CurrentSpeedLevel { get; private set; }

        public int DamageUpgradeCost => _config.DamageUpgradeCost;
        public int HealthUpgradeCost => _config.HealthUpgradeCost;
        public int SpeedUpgradeCost => _config.SpeedUpgradeCost;

        public ShopController(UpgradesConfig config)
        {
            _config = config;
            ResetSessionLevels();
        }

        public void ResetSessionLevels()
        {
            CurrentDamageLevel = 0;
            CurrentHealthLevel = 0;
            CurrentSpeedLevel = 0;
            
        }

        public bool TryUpgradeDamage()
        {
            CurrentDamageLevel++;
            _config.IncreaseLevelDamage();
            return true;
        }

        public bool TryUpgradeHealth()
        {
            CurrentHealthLevel++;
            _config.IncreaseLevelHealth();
            return true;
        }

        public bool TryUpgradeSpeed()
        {
            CurrentSpeedLevel++;
            _config.IncreaseLevelSpeed();
            return true;
        }
    }
}