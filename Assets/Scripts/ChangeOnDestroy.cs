using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOnDestroy : MonoBehaviour
{
    public GameObject NewObject;

    private void OnDestroy() {
        Instantiate(NewObject, transform.position, transform.rotation);
    }
}
