using System.Collections.Generic;
using _Project.Logic.Gameplay.Enemy;
using _Project.Logic.Gameplay.PlayerLogic;
using _Project.Logic.Gameplay.PlayerLogic.Shooting;
using _Project.Logic.Gameplay.Service.InputForGameplay;
using _Project.Logic.Gameplay.Spawners;
using _Project.Logic.Gameplay.Spawners.PointsToSpawn.Enemy;
using _Project.Logic.Gameplay.Spawners.PointsToSpawn.PlayerPoints;
using _Project.Logic.Meta.Mobile;
using _Project.Logic.Meta.ObjectPool;
using _Project.Logic.Meta.Service.DeviceIdentifier;
using _Project.Logic.Meta.UI.Health;
using UnityEngine;
using Zenject;

namespace _Project.Logic.Infrastructure.Installers.SceneInstallers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        private const int DEFAULT_SIZE_ENEMY_POOL = 10;
        private const int DEFAULT_SIZE_BULLET_POOL = 20;

        [SerializeField] private TransformToSpawnPlayer _pointToSpawnPlayer;
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private HealthView _healthView;
        [SerializeField] private MobileTools _mobileTools;
        [SerializeField] private List<EnemyAbstract> _enemyPrefabs;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private ContainersForPools _containersForPools;
        [SerializeField] private ContainerEnemyPoints _containerEnemyPoints;

        private readonly List<ObjectPool<EnemyAbstract>> _enemiesPool = new();

        public override void InstallBindings()
        {
            BindCheckerDevice();
            BindStartGame();
            BindingPlayer();
            BindingUIPlayerStats();
        }

        private void CreateAndBindingPools()
        {
            var instantiator = Container.Resolve<IInstantiator>();
            foreach (var enemyAbstract in _enemyPrefabs)
            {
                var poolEnemy = CreateObjectPool<EnemyAbstract>(enemyAbstract,
                    DEFAULT_SIZE_ENEMY_POOL,
                    _containersForPools.ContainerForEnemy,
                    false,
                    instantiator);
                _enemiesPool.Add(poolEnemy);
            }

            Container.Bind<List<ObjectPool<EnemyAbstract>>>().FromInstance(_enemiesPool).AsSingle().NonLazy();

            var bullet = CreateObjectPool<Bullet>(_bulletPrefab,
                DEFAULT_SIZE_BULLET_POOL,
                _containersForPools.ContainerForBullet,
                false,
                instantiator);

            Container.Bind<ObjectPool<Bullet>>().FromInstance(bullet).AsSingle().NonLazy();
        }


        private void BindCheckerDevice()
        {
            Container.Bind<MobileTools>().FromInstance(_mobileTools).AsSingle();
            Container.Bind<DeviceIdentifier>().AsSingle().NonLazy();
        }

        private void BindStartGame()
        {
            CreateAndBindingPools();
            Container.Bind<ContainerEnemyPoints>().FromInstance(_containerEnemyPoints).AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
        }

        private void BindingUIPlayerStats()
        {
            Container.BindInterfacesAndSelfTo<HealthViewModel>().AsSingle().NonLazy();
            Container.Bind<HealthView>().FromComponentInNewPrefab(_healthView).AsSingle();
        }

        private void BindingPlayer()
        {
            Container.BindInterfacesAndSelfTo<Player>().FromComponentInNewPrefab(_playerPrefab)
                .UnderTransform(_pointToSpawnPlayer.PointToSpawn.transform).AsSingle().NonLazy();

            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                Container.BindInterfacesAndSelfTo<MobileInput>().AsSingle();
            }
            else
            {
                Container.BindInterfacesAndSelfTo<KeybordInput>().AsSingle();
            }

            Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle().NonLazy();
        }

        private ObjectPool<T> CreateObjectPool<T>(T prefab, int size, Transform container, bool autoExpand,
            IInstantiator instantiator) where T : MonoBehaviour
        {
            var objPool = new ObjectPool<T>(prefab, size, container, autoExpand, instantiator);
            return objPool;
        }
    }
}