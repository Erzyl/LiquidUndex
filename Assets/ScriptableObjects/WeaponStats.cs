using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class WeaponStats : ScriptableObject{
    public string name;    
    public GameObject prefab;

    public float firerate = 0.05f;
    public float aimSpeed = 20f;
    public float impactForce = 600f;
    public float damage = 10f;
    public float bloomRate = 20f;
    public float recoil = 2.5f;
    public float kickback = 0.1f;

    public float modelOffsetX = 5;
    public float modelOffsetY = 0;
    public float modelOffsetZ = 0;
}
