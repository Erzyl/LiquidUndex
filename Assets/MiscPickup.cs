using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscPickup : MonoBehaviour
{

    public Transform playerTransform;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerTransform.position,playerTransform.forward,out hit,10f)){

        }
    }
}
