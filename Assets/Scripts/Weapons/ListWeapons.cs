using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "New List Weapons", menuName = "ListWeapons")]
    public class ListWeapons: ScriptableObject
    {
        public int selectedWeapon = 0;
        public WeaponData[] weapon;
    }
}