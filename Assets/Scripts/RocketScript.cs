using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour{

    public GameObject explosionEffect;
    private float radius = 5f;
    private float force = 500f;
    public AudioClip explosionSound;
    private float explosionVolume = 4f;


    private void OnCollisionEnter(Collision collision) {
        Explode();
    }

    void Explode() {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(explosionSound, transform.position, explosionVolume);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders) {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

        Destroy(gameObject);
    }
}
