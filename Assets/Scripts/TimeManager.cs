using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour{

    public float slowdownFactor = 0.3f;
    public float slowdownLength = 2f;
    private bool slowed = false;

    private void Update() {
        //Time.timeScale += (1f / slowdownLength)*Time.deltaTime;
        //Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    public void SlowTime() {

        if (!slowed){
            slowed = true;
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * .02f;
        }
        else {
            slowed = false;
            Time.timeScale = 1;
            Time.fixedDeltaTime = Time.timeScale * .02f;
        }
    }

}
