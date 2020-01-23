using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour{

    public GameObject projFab;
    public float fireRate = 1;
    public float shootDistance;

    private EnemyMove enemyMove;
    private float shootTimer;
    

    private void Awake() {
        enemyMove = GetComponent<EnemyMove>();
    }

    private void Update() {

        shootTimer = shootTimer > 0 ? shootTimer - Time.deltaTime*fireRate : 0;

        if (enemyMove.agent.remainingDistance < shootDistance) {
            Shoot();
            shootTimer = 1;
        }
    }

    void Shoot() {
        Instantiate(projFab);
    }

}
