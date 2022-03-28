using UnityEngine;
using Weapons;

namespace Bullets
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private WeaponData weaponData; //Weapons data variable

        private void Awake()
        {
            Destroy(gameObject, weaponData.life); //Destroy the bullet within the time set in the scriptable object
        }

        private void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject); //If it touches any object, it is destroyed
            Target target = collision.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(weaponData.damage); //Receives damage according to the scriptable object of the weapon. 
            }
        }
    }
}