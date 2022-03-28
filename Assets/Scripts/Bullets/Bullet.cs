using UnityEngine;
using Weapons;

namespace Bullets
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private WeaponData weaponData;

        private void Awake()
        {
            Destroy(gameObject, weaponData.life);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject);
            Target target = collision.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(weaponData.damage);
            }
        }
    }
}