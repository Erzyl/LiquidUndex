using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour{

    public Camera mainCamera;
    public float lootRange = 100f;
    private WeaponSwitch weaponSwitch;
    public LayerMask mask;

    void Start(){
        weaponSwitch = GetComponent<WeaponSwitch>();
    }

    // Update is called once per frame
    void Update(){

        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.position + mainCamera.transform.forward * 1000f, out hit, lootRange, mask)){

            if (Input.GetKey(KeyCode.E)){
                if(hit.transform.tag == "WeaponPickup") {

                    int pickupWeaponNumber = hit.transform.GetComponent<PickupID>().whatWeapon;

                    if (!weaponSwitch.inventory.Contains(pickupWeaponNumber)){
                        Destroy(hit.transform.gameObject);
                        weaponSwitch.inventory.Add(pickupWeaponNumber);

                        weaponSwitch.selectedInventorySlot = weaponSwitch.inventory.Count-1;
                        weaponSwitch.SelectWeapon();
                    }
                    else {
                        //Do denial effect
                    }
                }
            }
        }
    }
}
