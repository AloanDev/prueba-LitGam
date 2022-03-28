using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons;

namespace Bullets
{
    public class Magnet : MonoBehaviour
    {
        [SerializeField] private WeaponData weaponData;

        [FormerlySerializedAs("caughtRigidbodies")] [SerializeField]
        private List<Rigidbody> targets = new List<Rigidbody>();

        void FixedUpdate()
        {
            Destroy(gameObject, weaponData.life);

            foreach (Rigidbody rgTarget in targets)
            {
                if (rgTarget != null)
                    rgTarget.AddForce((transform.position - rgTarget.position) * weaponData.attraction *
                                      Time.deltaTime);
                else return;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Target"))
            {
                targets.Add(other.GetComponent<Rigidbody>());
            }

            Target target = other.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(weaponData.damage);
                Destroy(gameObject, weaponData.life);
            }
            else
            {
                targets.Remove(other.GetComponent<Rigidbody>());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Target"))
            {
                targets.Remove(other.GetComponent<Rigidbody>());
            }
        }
    }
}