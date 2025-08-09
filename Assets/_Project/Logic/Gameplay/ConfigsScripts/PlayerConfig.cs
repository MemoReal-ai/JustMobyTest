using UnityEngine;

namespace _Project.Logic.Gameplay.ConfigsScripts
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Gameplay/PlayerConfig")]
    public class PlayerConfig:ScriptableObject
    {
        [field: SerializeField] public float MaxHealth { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float CurrentDamage { get; private set; }
    }
}
