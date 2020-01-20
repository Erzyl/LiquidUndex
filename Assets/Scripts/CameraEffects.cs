using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour{

    private float baseFOV = 90f;
    [Range(0, 1)] private float mainFOV = 0.6f; //Change to weapon specific! (Scope spec?)
    [Range(0, 1)] private float weaponFOV = 0.7f;
    private float sprintFOV = 1.1f;

    private PlayerController playerController;
    private Gun gunData;
    private Camera mainCam;
    private Camera weaponCam;

    bool isAiming;

    private void Start() {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        mainCam = GameObject.Find("CameraMain").GetComponent<Camera>();
        weaponCam = GameObject.Find("WeaponCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update(){
        gunData = GetComponentInChildren<Gun>();

        UpdateFOV();
    }

    void UpdateFOV() {

        bool isRunning = playerController.playerInput.run;
        Status status = playerController.status;

        if (gunData != null && gunData.isAiming) {
            isAiming = gunData.isAiming;
        }
        else {
            isAiming = false;
        }

        if (isRunning) {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, baseFOV * sprintFOV, Time.deltaTime * 8f);
            weaponCam.fieldOfView = Mathf.Lerp(weaponCam.fieldOfView, baseFOV * sprintFOV, Time.deltaTime * 8f);
        }
        else if (isAiming) {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, baseFOV * mainFOV, Time.deltaTime * 8f);
            weaponCam.fieldOfView = Mathf.Lerp(weaponCam.fieldOfView, baseFOV * weaponFOV, Time.deltaTime * 8f);
        }
        else {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, baseFOV, Time.deltaTime * 8f);
            weaponCam.fieldOfView = Mathf.Lerp(weaponCam.fieldOfView, baseFOV, Time.deltaTime * 8f);
        }
    }
}
