using System;
using _Project.Logic.Gameplay.ConfigsScripts;
using UnityEngine;

namespace _Project.Logic.Gameplay.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        public Action<float> OnHealthChanged;

        [SerializeField] private PlayerConfig _playerConfig;

        private float _currentSpeed;
        private float _currentMaxHealth;
        private float _currentHealth;
        private float _currentDamage;
        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            SetupPlayerStats();
        }


        public void Move(Vector3 movement)
        {
            _rigidbody.AddForce(movement * _currentSpeed);
        }

        private void SetupPlayerStats()
        {
            _currentSpeed = _playerConfig.Speed;
            _currentMaxHealth = _playerConfig.MaxHealth;
            _currentDamage = _playerConfig.CurrentDamage;
            _currentHealth = _currentMaxHealth;
        }
    }
}