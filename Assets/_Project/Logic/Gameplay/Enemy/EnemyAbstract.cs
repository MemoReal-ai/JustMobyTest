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

        protected ITimeService TimeService;
        protected float Speed;

        private float _currentHealth;
        private float _damage;
        private int _reward;
        private bool _isDead;

        [Inject]
        public void Construct(ITimeService timeService)
        {
            TimeService = timeService;
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

        public void TakeDamage(float damage)
        {
            if (damage < 0)
            {
                Debug.LogWarning("Damage is negative");
                return;
            }

            _currentHealth = Mathf.Max(_currentHealth - damage, 0);

            if (_currentHealth == 0 && !_isDead)
            {
                OnDeath?.Invoke(_reward);
                VisualDeadth();
            }
        }

        private void VisualDeadth()
        {
            _isDead = true;
            gameObject.GetComponent<Collider>().enabled = false;
            transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack)
                .OnComplete(() => gameObject.SetActive(false));
        }

        protected abstract void Behaviour();


        public abstract void SetupBehaviourDependency(Vector3 position, Player target = null);
    }
}