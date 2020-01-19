using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int hp;
    private int hp_max = 5;

    public Rigidbody ragRigidHip;

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

        if (hp <= 0)
            Death();
    }



    void Death() {
      

        GetComponent<Animator>().enabled = false;
        EnemyMove getMove = GetComponent<EnemyMove>();
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
