using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour{
    public Vector3 dir;
    private float spd = 25f;
    public LayerMask mask;
    [SerializeField]
    private int damage = 5;
    private float lifeTime = 5f;

    private void Update() {
        Move();
        CheckCollision();

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) {
            Destroy(gameObject);
        }
    }

    void Move() {
        transform.Translate(dir * spd * Time.deltaTime,Space.World);
    }

    void CheckCollision() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit,1f, mask)) {
            if (hit.transform.tag == "Player") {
                GameObject.Find("Health").GetComponent<HealthText>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }



}
