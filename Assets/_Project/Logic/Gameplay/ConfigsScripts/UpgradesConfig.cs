using UnityEngine;

namespace _Project.Logic.Gameplay.ConfigsScripts
{
    [CreateAssetMenu(fileName = "UpgradesConfig", menuName = "Meta/UpgradesConfig", order = 1)]
    public class UpgradesConfig : ScriptableObject
    {
        [field: SerializeField]
        public float TresholdDamageUpgradeInPercent { get; private set; }
        [field: SerializeField]
        public float TresholdSpeedUpgradeInPercent { get; private set; }
        [field: SerializeField]
        public float TresholdHealthUpgradeInPercent { get; private set; }

        [field: SerializeField]
        public float NumericDamageUpgradePerLevelInPercent { get; private set; }
        [field: SerializeField]
        public float NumericHealthUpgradePerLevelInPercent { get; private set; }
        [field: SerializeField]
        public float NumericSpeedUpgradePerLevelInPercent { get; private set; }

        [field: SerializeField]
        public int HealthUpgradeCost { get; private set; }

        [field: SerializeField]
        public int DamageUpgradeCost { get; private set; }

        [field: SerializeField]
        public int SpeedUpgradeCost { get; private set; }

        [field: SerializeField]
        public int LevelDamage { get; private set; }
        
        [field: SerializeField]
        public int LevelHealth { get; private set; }
        
        [field: SerializeField]
        public int LevelSpeed { get; private set; }

        public void IncreaseLevelDamage()
        {
            LevelDamage++;
        }

        public void IncreaseLevelHealth()
        {
            LevelHealth++;
        }

        public void IncreaseLevelSpeed()
        {
            LevelSpeed++;
        }
    }
}