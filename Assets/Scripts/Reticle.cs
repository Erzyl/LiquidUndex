using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{
    private RectTransform reticle;

    public float restingSize;
    public float walkSize;
    public float runSize;
    public float aimSize;
    public float speed;

    [HideInInspector]
    public float currentSize;

    PlayerController playerController;
    GameObject player;

    public bool isShooting = false;

    private void Start() {

        reticle = GetComponent<RectTransform>();
        player = GameObject.Find("Player");
        playerController =player.GetComponent<PlayerController>();


    }

    ///FIXA!
    private void Update() {

        if (isAiming){
            changeReticle(aimSize);
        }
        if (playerController.playerInput.run || isAirborn) {
            changeReticle(runSize);
        }
        else if (isMoving || isShooting) {
            changeReticle(walkSize);
        }
        else {
            changeReticle(restingSize);
        }

        reticle.sizeDelta = new Vector2(currentSize, currentSize);

        isShooting = false;
    }



    private void changeReticle(float inputSize) {
        currentSize = Mathf.Lerp(currentSize, inputSize, Time.deltaTime * speed);
    }



    private bool isAiming {
        get {
            Gun gun = player.GetComponentInChildren<Gun>();

            if (gun != null) {
                return gun.isAiming;
            }
            else
                return false;
        }
    }


    private bool isAirborn {
        get {
            if (Mathf.Abs(playerController.movement.controller.velocity.y) > 0.1f)
                return true;
            else
                return false;
        }
    }


    private bool isMoving {
        get{
            if (playerController.movement.controller.velocity.magnitude > 0f)
                return true;
            else
                return false;
        }
    }

}
