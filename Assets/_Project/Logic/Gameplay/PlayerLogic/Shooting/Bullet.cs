using System;
using _Project.Logic.Gameplay.Enemy;
using UnityEngine;

namespace _Project.Logic.Gameplay.PlayerLogic.Shooting
{
    public class Bullet : MonoBehaviour
    {
        public int Damage { get; private set; }

        public void Setup(int damage)
        {
            Damage = damage;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out EnemyAbstract enemy))
            {
                enemy.TakeDamage(Damage);
            }
        }
    }
}