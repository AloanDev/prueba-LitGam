using UnityEngine;
using UnityEngine.Serialization;

namespace Weapons
{
    [CreateAssetMenu(fileName = "New Weapon Data", menuName = "WeaponData")]
    public class WeaponData : ScriptableObject
    {
        public new string name;
        public string description;

        public GameObject prefabWeapon;

        public GameObject bullet;
        public GameObject effectImpact;

        public int damage;
        public int range;
        public int attraction;
        public float life;
    }
}