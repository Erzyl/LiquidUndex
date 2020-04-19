using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour{

    CameraController cameraController; 

    private void Awake() {
        cameraController = GameObject.Find("CameraMain").GetComponent<CameraController>();
    }

    public void PressStart() {
        cameraController.UpdateView(1);
    }

    public void PressOptions() {
        cameraController.UpdateView(2);
    }

    public void BackToMain() {
        cameraController.UpdateView(0);
    }

}
