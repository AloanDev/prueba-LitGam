using Unity.VisualScripting;
using UnityEngine;

namespace Weapons
{
    public class WeaponsSwitching : MonoBehaviour
    {
        [SerializeField] private ListWeapons listWeapons;

        void Start()
        {
            foreach (var t in listWeapons.weapon) //Instance the weapons of the weapons list scriptable object
            {
                var weapon = Instantiate(t.prefabWeapon, transform.position, transform.rotation);
                weapon.transform.parent = transform;
                SelectWeapon();
            }
        }


        void Update()
        {
            int previousSelectedWeapon = listWeapons.selectedWeapon;

            if (Input.GetAxis("Mouse ScrollWheel") > 0) //If you roll the mouse upwards, it changes weapon and vice-versa.
            {
                if (listWeapons.selectedWeapon >= transform.childCount - 1)
                    listWeapons.selectedWeapon = 0;
                else
                    listWeapons.selectedWeapon++;
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (listWeapons.selectedWeapon <= 0)
                    listWeapons.selectedWeapon = transform.childCount - 1;
                else
                    listWeapons.selectedWeapon--;
            }

            if (previousSelectedWeapon != listWeapons.selectedWeapon) //If the number is different from the list of weapons, do the SelectWeapon() function. 
            {
                SelectWeapon();
            }
        }

        private void SelectWeapon()//
        {
            int i = 0;
            foreach (Transform weapon in transform) //Scroll through the list of instantiated weapons based on the list of the scriptable object of weapons
            {
                weapon.gameObject.SetActive(i == listWeapons.selectedWeapon);

                i++;
            }
        }
    }
}