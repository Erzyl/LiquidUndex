using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityMarker : MonoBehaviour{

    private int quality;
    private Vector3 startPos = new Vector3();
    private float offset = 1.7f;
    private int cur_quality;

    private void Awake() {
        startPos = transform.position;
    }

    private void Update() {
        quality = QualitySettings.GetQualityLevel();

        if (cur_quality != quality){
            cur_quality = quality;
            if (quality == 1){
                transform.position = startPos;
            }
            else if (quality == 0){
                transform.position = new Vector3(transform.position.x, transform.position.y+offset, transform.position.z);
            }
        }
    }
    
}
