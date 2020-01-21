using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
      
    private GameObject fpsCam;
    private GameObject weaponCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private Vector3 destination;
    public Transform bulletHole;

    float shootTimerTick;
    
    float range = 100f;
    public LayerMask mask;

    private WeaponSwitch switchParent;
    private PlayerInput playerInput;

    private float aimSpeed;
    private float impactForce;
    private int damage;
    private float bloomRate;
    private float recoil;
    private float kickback;
    private float shootTimer;
    public float pitchRandom;

    Vector3 startPos;
    Quaternion startRot;

    public bool isAiming;
    private AudioSource sfx;

    public AudioClip equipSound;
    public AudioClip hitMarkerSound;
    private Image hitMarkerImage;
    private float hitMarkerWait;
    private Color CLEARWHITE = new Color(1, 1, 1, 0);
    public float gunShootVolume = 1f;

    private Reticle reticleCheck;
    private float bloomScaler; 

    private void Awake() {
        sfx = GetComponent<AudioSource>();
    }

    private void Start() {
        reticleCheck = GameObject.Find("Crosshair").GetComponent<Reticle>();
        fpsCam = GameObject.Find("CameraMain");
        hitMarkerImage = GameObject.Find("UI/Hitmarker/Image").GetComponent<Image>();
        hitMarkerImage.color = CLEARWHITE;

        playerInput = transform.GetComponentInParent<PlayerInput>();
        switchParent = transform.GetComponentInParent<WeaponSwitch>();
        UpdateWeaponStats();

        startPos = transform.localPosition;
        startRot = transform.localRotation;

        //Sound
        sfx.PlayOneShot(equipSound, 1f);
    }

    // Update is called once per frame
    void Update(){

        shootTimer = (shootTimer > 0 ? shootTimer - shootTimerTick : 0);

        if (playerInput.shoot && !playerInput.run) {      

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

        if (hitMarkerWait > 0) {
            hitMarkerWait -= Time.deltaTime;
        }
        else {
            hitMarkerImage.color = Color.Lerp(hitMarkerImage.color, CLEARWHITE, Time.deltaTime *2);
        }
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
        sfx.clip = switchParent.loadout[selWep].gunShotSound;
        pitchRandom = switchParent.loadout[selWep].pitchRandomization;
    }

    bool Aim(bool p_isAiming) {
        Transform t_anchor = transform.Find("Anchor");
        Transform t_state_ads = transform.Find("States/ADS");
        Transform t_state_hip = transform.Find("States/Hip");
        Transform t_state_run = transform.Find("States/run");

        if (playerInput.run) {
            t_anchor.rotation = Quaternion.Lerp(t_anchor.rotation, t_state_run.transform.rotation, Time.deltaTime * aimSpeed);
            t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_ads.transform.position, Time.deltaTime * aimSpeed);
        }
        else if (p_isAiming) {
            t_anchor.rotation = Quaternion.Lerp(t_anchor.rotation, t_state_ads.transform.rotation, Time.deltaTime * aimSpeed);
            t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_ads.transform.position, Time.deltaTime * aimSpeed);
            
        }
        else {
            t_anchor.rotation = Quaternion.Lerp(t_anchor.rotation, t_state_hip.transform.rotation, Time.deltaTime * aimSpeed);
            t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_hip.transform.position, Time.deltaTime * aimSpeed);
        }

        return p_isAiming;
    }

    void Shoot() {


        reticleCheck.isShooting = true;

        //Gun effects
        muzzleFlash.Play();
        transform.Rotate(-recoil, 0, 0);
        transform.position -= transform.forward * kickback;

        //Sound
        sfx.Stop();
        sfx.volume = gunShootVolume;
        sfx.pitch = 1 - pitchRandom + Random.Range(-pitchRandom, pitchRandom);
        sfx.Play();

        //Bloom
        bloomScaler = reticleCheck.currentSize*0.03f;

        Vector3 bloom = fpsCam.transform.position + fpsCam.transform.forward * 1000f;
        bloom += Random.Range(-bloomRate* bloomScaler, bloomRate* bloomScaler) * fpsCam.transform.up;
        bloom += Random.Range(-bloomRate* bloomScaler, bloomRate* bloomScaler) * fpsCam.transform.right;
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
                enemy.TakeDamage(damage);

                hitMarkerImage.color = Color.white;
                hitMarkerWait = 0.1f;
                sfx.PlayOneShot(hitMarkerSound,20f);

            }

            //Create hit effect
            GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGo, 2f);
        }
    }
}
