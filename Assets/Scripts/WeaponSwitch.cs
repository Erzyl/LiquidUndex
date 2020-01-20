using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{

    public int selectedInventorySlot = -1;
    int maxWeps = 2; //Change to inventory

    [HideInInspector]
    public WeaponStats[] loadout;
    private GameObject currentWeapon;
    public List<int> inventory = new List<int>();

    private void Awake() {
        loadout = GetComponent<WeaponList>().loadout;
        //inventory.Add(1);
        //inventory.Add(0);
    }

    void Start(){
        if (inventory.Count > 0)
            SelectWeapon();   
    }

    // Update is called once per frame
    void Update(){

        int prevSelectedSpot = selectedInventorySlot; //Save current selected list index
        maxWeps = inventory.Count; //Check amount of weapons in inventory

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
            selectedInventorySlot = (selectedInventorySlot >= maxWeps - 1 ? 0 : selectedInventorySlot + 1);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f){
            selectedInventorySlot = (selectedInventorySlot <= 0 ? maxWeps - 1 : selectedInventorySlot - 1);
        }

        //Remake, jeeeez
        if (Input.GetKeyDown(KeyCode.Alpha1) && maxWeps >= 1) {
            selectedInventorySlot = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && maxWeps >= 2) {
            selectedInventorySlot = 1;
        }

        if (maxWeps > 0 && prevSelectedSpot != selectedInventorySlot) {
            SelectWeapon();
        }
    }

   public void SelectWeapon() {
        if (currentWeapon != null)
            Destroy(currentWeapon);

        int weaponNumber = inventory[selectedInventorySlot];

        Vector3 offset = new Vector3(loadout[weaponNumber].modelOffsetX, loadout[weaponNumber].modelOffsetY, loadout[weaponNumber].modelOffsetZ);
        GameObject newWeapon = Instantiate(loadout[weaponNumber].prefab,transform.TransformPoint(offset), transform.rotation,transform) as GameObject;

        currentWeapon = newWeapon;
    }

}
