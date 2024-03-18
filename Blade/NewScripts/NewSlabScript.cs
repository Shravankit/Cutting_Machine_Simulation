using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSlabScript : MonoBehaviour
{
    public BladeGameObject bladeCutter;
    public Rigidbody bladeRigidBody;

    public BladeCollisions bladeCollisions;

    BladeColl bladecol;


    struct Triangle {
        public Vector3 v1;
        public Vector3 v2;
        public Vector3 v3;

        public Vector3 getNormals(){
            return Vector3.Cross(v1 - v2, v1 - v3).normalized;
        }

        public void matchDirection(Vector3 dir) {
        if (Vector3.Dot(getNormals(), dir) > 0) {
            return;
        }
        else {
            Vector3 vect = v1;
            v1 = v3;
            v3 = vect;
        }
    }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SliceObject(Collision other)
    {

    }

    void OnCollisionEnter(Collision collision) {
    if (collision.gameObject == bladeCutter.blade) {
        Debug.Log("Collided With BladeCutter");

        ContactPoint[] contacts = collision.contacts;
        foreach (ContactPoint contact in contacts) {
            Debug.Log("Contact Point: " + contact.point);
        }
    }
}

    void OnTriggerEnter(Collider other) {
        if(other.gameObject == bladeCutter.blade)
        {  
            Debug.Log("Collided With BladeCutter");
        }    
    }
}

[System.Serializable]
public class BladeGameObject
{
    public GameObject blade;
}

[System.Serializable]
public class BladeCollisions
{
    public Collider bladeCollisions;
}