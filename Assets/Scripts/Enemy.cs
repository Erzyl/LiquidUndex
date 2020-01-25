using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
    private int hp;
    public int hp_max = 20;

    public Rigidbody ragRigidHip;
    public AudioSource deathSound;
    public GameObject bloodFab;

    void Awake(){
        hp = hp_max;
    }

    private void Start() {
        SetRigidBodyState(true);
        SetColliderState(false);
    }

    void Update(){

        
    }

    public void TakeDamage(int damage) {
        hp -= damage;
        Vector3 offSetBlood = new Vector3();
        offSetBlood.y = 2f;
        GameObject bloodP = Instantiate(bloodFab, transform.position+ offSetBlood, transform.rotation);
        Destroy(bloodP, 1f);

        if (hp <= 0)
            Death();
    }



    void Death() {

        deathSound.Play();
        GetComponent<Animator>().enabled = false;
        EnemyMove getMove = GetComponent<EnemyMove>();
        GetComponent<EnemyShoot>().enabled = false;
        getMove.agent.speed = 0;
        getMove.enabled = false;
        

        SetRigidBodyState(false);
        SetColliderState(true);

        
        //if (rigidbody != null) {
            ragRigidHip.AddExplosionForce(2500f, transform.position, 50);
       // }
    }



    void SetRigidBodyState(bool state) {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody rigidbody in rigidbodies) {
            rigidbody.isKinematic = state;
        }

        GetComponent<Rigidbody>().isKinematic = !state;
    }

    void SetColliderState(bool state) {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders) {
            collider.enabled = state;
        }

        GetComponent<Collider>().enabled = !state;
    }
}
