using System;
using _Project.Logic.Gameplay.ConfigsScripts;
using _Project.Logic.Gameplay.PlayerLogic.Shooting;
using _Project.Logic.Gameplay.Service.TimeForInteract;
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
        [SerializeField] private ShootPoint _playerTransform;

        private ITimeService _timeService;
        private float _currentSpeed;
        private float _currentHealth;
        private float _currentDamage;
        private Rigidbody _rigidbody;

        public float CurrentMaxHealth { get; private set; }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            SetupPlayerStats();
        }

        [Inject]
        public void Construct(ITimeService timeService)
        {
            _timeService = timeService;
        }

        public void Move(Vector3 movement)
        {
            if (movement == Vector3.zero)
            {
                _rigidbody.velocity = Vector3.zero;
                return;
            }

            _rigidbody.AddForce(movement * (_currentSpeed * _timeService.GetDeltaTime()), ForceMode.Impulse);

            if (_rigidbody.velocity.sqrMagnitude > _currentSpeed * _currentSpeed)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * _currentSpeed;
            }

            Rotate(movement);
        }

        public void TakeDamage(float damage)
        {
            if (damage < 0)
            {
                Debug.LogWarning("Damage is negative");
                return;
            }

            _currentHealth = Mathf.Max(_currentHealth - damage, 0);
            OnHealthChanged?.Invoke(_currentHealth / _playerConfig.MaxHealth);

            if (_currentHealth == 0)
            {
                OnDead?.Invoke();
            }
        }

        private void Rotate(Vector3 rotation)
        {
            transform.forward = rotation;
        }

        private void SetupPlayerStats()
        {
            _currentSpeed = _playerConfig.MaxSpeed;
            CurrentMaxHealth = _playerConfig.MaxHealth;
            _currentDamage = _playerConfig.CurrentDamage;
            _currentHealth = CurrentMaxHealth;
        }

        public void Shoot()
        {
        }
    }
}