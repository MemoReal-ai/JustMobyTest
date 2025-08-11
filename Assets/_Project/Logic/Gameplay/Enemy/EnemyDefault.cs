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
            var direction = (_target.transform.position - transform.position).normalized;

            _rigidbody.velocity = direction * Speed;
        }

        public override void SetupBehaviourDependency(Vector3 position, Player target)
        {
            transform.position = position;
            _target = target;
        }

        protected override void ResetVisual()
        {
            base.ResetVisual();
            _target = null;
        }

        protected override void Setup()
        {
            base.Setup();
            _rigidbody = GetComponent<Rigidbody>();
        }
    }
}