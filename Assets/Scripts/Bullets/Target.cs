using System;
using Managers;
using UnityEngine;

namespace Bullets
{
    public class Target : MonoBehaviour
    {
        public float health = 50f;
        private GamePlayManager _instance;

        private void Start()
        {
            _instance = GamePlayManager.Instance;
        }

        public void TakeDamage(float amount)
        {
            health -= amount;
            if (health <= 0f)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
            _instance.countTargets++;
        }
    }
}
