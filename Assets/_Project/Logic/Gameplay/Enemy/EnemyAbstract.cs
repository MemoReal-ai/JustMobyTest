using System;
using _Project.Logic.Gameplay.ConfigsScripts;
using _Project.Logic.Gameplay.PlayerLogic;
using _Project.Logic.Gameplay.Service.TimeForInteract;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Project.Logic.Gameplay.Enemy
{
    public abstract class EnemyAbstract : MonoBehaviour
    {
        public Action<int> OnDeath;

        [SerializeField] private EnemyConfig _config;

        protected float Speed;

        private DeathView _deathView;
        private int _currentHealth;
        private int _damage;
        private int _reward;
        private bool _isDead;

        [Inject]
        public void Construct(DeathView deathView)
        {
            _deathView = deathView;
        }

        private void Start()
        {
            Setup();
        }

        protected virtual void Setup()
        {
            _currentHealth = _config.MaxHealth;
            Speed = _config.Speed;
            _damage = _config.Damage;
            _reward = _config.Reward;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                player.TakeDamage(_damage);
            }
        }

        private void FixedUpdate()
        {
            if (_isDead)
            {
                return;
            }

            Behaviour();
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                Debug.LogWarning("Damage is negative");
                return;
            }

            _currentHealth = Mathf.Max(_currentHealth - damage, 0);

            if (_currentHealth == 0 && !_isDead)
            {
                _isDead = true;
                OnDeath?.Invoke(_reward);

                _deathView.InvokeDeathVisual(this);
            }
        }

        protected abstract void Behaviour();


        public abstract void SetupBehaviourDependency(Vector3 position, Player target = null);
    }
}