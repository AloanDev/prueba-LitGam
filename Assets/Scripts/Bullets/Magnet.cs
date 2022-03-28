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
            Destroy(gameObject, weaponData.life); //Destroy the bullet within the time set in the scriptable object

            foreach (Rigidbody rgTarget in targets)
            {
                if (rgTarget != null)
                {
                    rgTarget.AddForce((transform.position - rgTarget.position) * weaponData.attraction * Time.deltaTime); //Adds an opposing force of the objects in the list. 
                }
                else
                {
                    return; 
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Target"))
            {
                targets.Add(other.GetComponent<Rigidbody>()); //Adds objects to the list if it touches them
            }

            Target target = other.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(weaponData.damage);
                Destroy(gameObject, weaponData.life);
            }
            else
            {
                targets.Remove(other.GetComponent<Rigidbody>()); //Removes objects from the list if it touches them in case they are null
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Target"))
            {
                targets.Remove(other.GetComponent<Rigidbody>()); //Removes objects from the list if it touches them
            }
        }
    }
}