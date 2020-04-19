using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour{

    public GameObject projFab;
    public float fireRate = 0.15f;
    public float shootDistance = 30f;
    public Transform shootPoint;
    private GameObject player;

    private EnemyMove enemyMove;
    private float shootTimer;
    public AudioSource shootSound;

    private float distToPlayer;
    private void Awake() {
        enemyMove = GetComponent<EnemyMove>();
        player = GameObject.Find("Player");
    }

    private void Update() {

        shootTimer = shootTimer > 0 ? shootTimer - Time.deltaTime*fireRate : 0;

        distToPlayer = Vector3.Distance(transform.position, player.transform.position);


        if ((shootDistance > distToPlayer) && shootTimer == 0) {
            Shoot();
            shootTimer = 1;
        }
    }

    void Shoot() {
        shootSound.Play();
        GameObject bullet = Instantiate(projFab, shootPoint.position, shootPoint.rotation);
        bullet.GetComponent<EnemyBulletScript>().dir = (player.transform.position-shootPoint.position).normalized;
    }

}
