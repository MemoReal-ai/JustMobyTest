using System;
using _Project.Logic.Gameplay.ConfigsScripts;
using _Project.Logic.Gameplay.PlayerLogic.Shooting;
using _Project.Logic.Gameplay.Service.TimeForInteract;
using _Project.Logic.Meta.ObjectPool;
using UnityEngine;
using Zenject;

namespace _Project.Logic.Gameplay.PlayerLogic
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        public Action<float> OnHealthChanged;
        public Action OnDead;

        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private ShootPoint _shootPoint;


        private ObjectPool<Bullet> _bulletPool;
        private ITimeService _timeService;
        private float _currentSpeed;
        private int _currentHealth;
        private int _currentDamage;
        private float _rotationSpeed;
        private Rigidbody _rigidbody;

        public int CurrentMaxHealth { get; private set; }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            SetupPlayerStats();
        }

        [Inject]
        public void Construct(ITimeService timeService, ObjectPool<Bullet> bulletPool)
        {
            _timeService = timeService;
            _bulletPool = bulletPool;
        }

        public void Move(Vector3 movement)
        {
            if (movement == Vector3.zero)
            {
                _rigidbody.velocity = Vector3.zero;
                return;
            }

            Quaternion targetRotation = Quaternion.LookRotation(movement);
            _rigidbody.MoveRotation(Quaternion.RotateTowards(
                _rigidbody.rotation,
                targetRotation,
                _rotationSpeed * _timeService.GetDeltaTime()));

            Vector3 force = movement.normalized * (_currentSpeed * _timeService.GetDeltaTime());
            _rigidbody.AddForce(force, ForceMode.VelocityChange);

            if (_rigidbody.velocity.sqrMagnitude > _currentSpeed * _currentSpeed)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * _currentSpeed;
            }
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                Debug.LogWarning("Damage is negative");
                return;
            }

            _currentHealth = Mathf.Max(_currentHealth - damage, 0);
            OnHealthChanged?.Invoke((float)_currentHealth / _playerConfig.MaxHealth);

            if (_currentHealth == 0)
            {
                OnDead?.Invoke();
            }
        }

        private void SetupPlayerStats()
        {
            _currentSpeed = _playerConfig.MaxSpeed;
            CurrentMaxHealth = _playerConfig.MaxHealth;
            _currentDamage = _playerConfig.CurrentDamage;
            _currentHealth = CurrentMaxHealth;
            _rotationSpeed = _playerConfig.RotationSpeed;
        }

        public void IncreaseHealth(int amount)
        {
            _currentHealth += amount;
            CurrentMaxHealth += amount;
        }

        public void IncreaseDamage(int amount)
        {
            _currentDamage += amount;
        }

        public void IncreaseSpeed(int amount)
        {
            _currentSpeed += amount;
        }

        public void Shoot()
        {
            var bullet = _bulletPool.GetObject();
            if (bullet == null)
            {
                Debug.LogWarning("Bullet Reload");
                return;
            }

            bullet.Setup(_currentDamage, _shootPoint.transform.position, _shootPoint.transform.rotation);
        }
    }
}