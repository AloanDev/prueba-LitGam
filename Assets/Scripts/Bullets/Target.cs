using System;
using Managers;
using UnityEngine;

namespace Bullets
{
    public class Target : MonoBehaviour
    {
        public float health = 50f;
        private GamePlayManager _instance; // Singleton

        private void Start()
        {
            _instance = GamePlayManager.Instance;
        }

        public void TakeDamage(float amount) //Receives damage depending on the set value "hardcore mode".
        {
            health -= amount;
            if (health <= 0f)
            {
                Die();
            }
        }

        private void Die() //Destroy game object and add 1 point in counter targets
        {
            Destroy(gameObject);
            _instance.countTargets++;
        }
    }
}
