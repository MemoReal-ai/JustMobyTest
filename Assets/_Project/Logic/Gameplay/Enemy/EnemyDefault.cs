using _Project.Logic.Gameplay.PlayerLogic;
using UnityEngine;

namespace _Project.Logic.Gameplay.Enemy
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyDefault : EnemyAbstract
    {
        private Player _target;
        private Rigidbody _rigidbody;

        protected override void Behaviour()
        {
            if (_target == null)
            {
                _rigidbody.velocity = Vector3.zero;
                return;
            }

            var direction = (_target.transform.position - transform.position).normalized;

            _rigidbody.velocity = direction * Speed;
        }

        public override void SetupBehaviourDependency(Vector3 position, Player target = null)
        {
            transform.position = position;
            _target = target;
        }

        protected override void Setup()
        {
            base.Setup();
            _rigidbody = GetComponent<Rigidbody>();
        }
    }
}