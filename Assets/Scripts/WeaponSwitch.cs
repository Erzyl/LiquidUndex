using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{

    public int selectedWeapon = 0;
    int maxWeps = 2; //Change to inventory

    public WeaponStats[] loadout;
    private GameObject currentWeapon;

    void Start(){
        SelectWeapon();   
    }

    // Update is called once per frame
    void Update(){

        int prevWep = selectedWeapon;
       // maxWeps = transform.childCount;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
            selectedWeapon = (selectedWeapon >= maxWeps - 1 ? 0 : selectedWeapon+1);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f){
            selectedWeapon = (selectedWeapon <= 0 ? maxWeps - 1 : selectedWeapon-1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && maxWeps >= 1) {
            selectedWeapon = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && maxWeps >= 2) {
            selectedWeapon = 1;
        }

        if (prevWep != selectedWeapon) {
            SelectWeapon();
        }
    }

   void SelectWeapon() {
        if (currentWeapon != null)
            Destroy(currentWeapon);

        Vector3 offset = new Vector3(loadout[selectedWeapon].modelOffsetX, loadout[selectedWeapon].modelOffsetY, loadout[selectedWeapon].modelOffsetZ);
        GameObject newWeapon = Instantiate(loadout[selectedWeapon].prefab,transform.TransformPoint(offset), transform.rotation,transform) as GameObject;
       //newWeapon.transform.localPosition = Vector3.zero;
       //newWeapon.transform.localEulerAngles = Vector3.zero;

        //Should anchor to weapon state!

        currentWeapon = newWeapon;

        //int i = 0;
        //foreach (Transform weapon in transform) {
        //    if (i == selectedWeapon)
        //        weapon.gameObject.SetActive(true);
        //    else
        //        weapon.gameObject.SetActive(false);
        //    i++;
        //}
    }

}
