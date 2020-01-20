using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
      
    private GameObject fpsCam;
    private GameObject weaponCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private Vector3 destination;
    public Transform bulletHole;

    float shootTimerTick;
    private float shootTimer = 0f;
    float range = 100f;
    public LayerMask mask;

    //Weapon vars -- Move to spec script
    //public WeaponStats[] loadout;
    private WeaponSwitch switchParent;
    private PlayerInput playerInput;

    float aimSpeed;
    float impactForce;
    float damage;
    float bloomRate;
    float recoil;
    float kickback;

    Vector3 startPos;
    Quaternion startRot;

    public bool isAiming;

    private void Start() {
        fpsCam = GameObject.Find("CameraMain");

        playerInput = transform.GetComponentInParent<PlayerInput>();
        switchParent = transform.GetComponentInParent<WeaponSwitch>();
        UpdateWeaponStats();

        startPos = transform.localPosition;
        startRot = transform.localRotation;  
    }

    // Update is called once per frame
    void Update(){
       

        if (playerInput.shoot) {

            shootTimer = (shootTimer > 0 ? shootTimer - shootTimerTick : 0);

            if (shootTimer == 0){
                Shoot();
                shootTimer = 1;
            }
        }

        if (playerInput.run)
            isAiming = Aim(false);
        else
            isAiming = Aim(playerInput.aim);

        //Weapon elasticity
        transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, Time.deltaTime * 4f);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, startRot, Time.deltaTime * 4f);
    }

    void UpdateWeaponStats() {
         int selWep = switchParent.inventory[switchParent.selectedInventorySlot];

        shootTimerTick = switchParent.loadout[selWep].firerate;
        aimSpeed = switchParent.loadout[selWep].aimSpeed;
        impactForce = switchParent.loadout[selWep].impactForce;
        damage = switchParent.loadout[selWep].damage;
        bloomRate = switchParent.loadout[selWep].bloomRate;
        recoil = switchParent.loadout[selWep].recoil;
        kickback = switchParent.loadout[selWep].kickback;
    }

    bool Aim(bool p_isAiming) {
        Transform t_anchor = transform.Find("Anchor");
        Transform t_state_ads = transform.Find("States/ADS");
        Transform t_state_hip = transform.Find("States/Hip");

        if (p_isAiming) {
            //aim
            t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_ads.transform.position, Time.deltaTime * aimSpeed);
            
        }
        else {
            //hip
            t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_hip.transform.position, Time.deltaTime * aimSpeed);
        }

        return p_isAiming;
    }

    void Shoot() {
        

      
        //Gun effects
        muzzleFlash.Play();
        transform.Rotate(-recoil, 0, 0);
        transform.position -= transform.forward * kickback; 

        //Bloom
        
        Vector3 bloom = fpsCam.transform.position + fpsCam.transform.forward * 1000f;
        bloom += Random.Range(-bloomRate, bloomRate) * fpsCam.transform.up;
        bloom += Random.Range(-bloomRate, bloomRate) * fpsCam.transform.right;
        bloom -= transform.position;
        bloom.Normalize();

        //Ray
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, bloom, out hit, range, mask)) {

            Target target = hit.transform.GetComponent<Target>();
            if (target != null){
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null) { //If rigidbody apply force
                hit.rigidbody.AddForce(-hit.normal*impactForce);
            }

            destination = hit.point + hit.normal * .0015f;
            if (hit.transform.tag == "Block") {
                Transform hole = Instantiate(bulletHole, destination, Quaternion.LookRotation(hit.normal) * Quaternion.Euler(0, 180f, 0));
                hole.SetParent(hit.transform);
            }
            else if (hit.transform.tag == "Enemy") {
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                enemy.TakeDamage(1);

                //Vector3 knockForce = new Vector3(1, 1, 1);
                //enemy.ragRigidHip.AddForce(knockForce);

            }

            //Create hit effect
            GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGo, 2f);
        }
    }
}
