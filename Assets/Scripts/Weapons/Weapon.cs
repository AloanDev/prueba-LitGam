using System;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponData weaponData;
        public Transform refShot;

        private void Update()
        {
            if (!GamePlayManager.Instance.isPaused)
                if (Input.GetButtonDown("Fire1"))
                {
                    Shoot();
                }
        }

        private void Shoot()
        {
            var instBullet = Instantiate(weaponData.bullet, refShot.position, refShot.rotation);
            instBullet.GetComponent<Rigidbody>().velocity = refShot.forward * weaponData.range;
        }
    }
}