using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeCollision : MonoBehaviour
{ 
    private Vector3 collisionPoint;
    private void OnTriggerEnter(Collider other) {
        collisionPoint = other.ClosestPointOnBounds(transform.position);
        Debug.Log($"Blade collided with {other.gameObject.name} at {collisionPoint}");
    }
}
