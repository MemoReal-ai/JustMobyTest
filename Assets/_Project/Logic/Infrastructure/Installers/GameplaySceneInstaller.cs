using _Project.Logic.Gameplay.Player;
using _Project.Logic.Gameplay.Spawners.PointsToSpawn.Player;
using UnityEngine;
using Zenject;

namespace _Project.Logic.Infrastructure.Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private TransformToSpawnPlayer _pointToSpawnPlayer;
        [SerializeField] private Player _playerPrefab;

        public override void InstallBindings()
        {
            BindingPlayer();
        }

        private void BindingPlayer()
        {
            Container.BindInterfacesAndSelfTo<Player>().FromComponentInNewPrefab(_playerPrefab)
                .UnderTransform(_pointToSpawnPlayer.PointToSpawn).AsSingle();
        }
    }
}