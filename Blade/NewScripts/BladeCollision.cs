using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeCollision : MonoBehaviour
{ 
    public ParticleSystem sparksSystemPrefab; // Assign your particle system prefab in the inspector
    private Vector3 collisionPoint;
    private ParticleSystem sparksInstance; // Instance of the spawned particle system

    public AudioClip sawEffect;

    private void OnTriggerEnter(Collider other) {
        // Find the collision point on the collider's bounds
        collisionPoint = other.ClosestPointOnBounds(transform.position);
        
        Debug.Log($"Blade collided with {other.gameObject.name} at {collisionPoint}");

        // Instantiate the particle system at the collision point
        // sparksInstance = Instantiate(sparksSystemPrefab, collisionPoint, Quaternion.identity);
        
        // Parent the particle system to the collider's transform
        // sparksInstance.transform.parent = other.transform;
        
        // Play the particle system
        sparksInstance.Play();
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = sawEffect;
        audio.Play();
    }

    private void OnTriggerExit(Collider other) {
        // If the collider exiting is the one that caused the collision
        if (other == sparksInstance.GetComponent<Collider>()) {
            // Stop emitting particles
            sparksInstance.Stop();
            // Detach the particle system from its parent
            sparksInstance.transform.parent = null;
            // Destroy the particle system after it finishes playing
            Destroy(sparksInstance.gameObject, sparksInstance.main.duration);
        }
    }
}
