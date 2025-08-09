using UnityEngine;

namespace _Project.Logic.Gameplay.Spawners.PointsToSpawn.Player
{
    public class TransformToSpawnPlayer : MonoBehaviour
    {
        [field: SerializeField]
        public Transform PointToSpawn {get; private set; }
    }
}
