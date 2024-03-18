using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions.Comparers;

public class Blade : MonoBehaviour
{
    public BladeHandle handle;
    public BladeMeshs bladeMesh;
    public bladeRotation bladeRotation;
    // public bladeCollision bladeCol;
    // public GameObject Slab;

    public Transform startPosition;
    public Transform endPosition;

    public VelocityEstimator velocityEstimator;


    public Material upperMaterial; // Material for upper hull
    public Material lowerMaterial; // Material for lower hull

    public float rotationSpeed = 20f;
    public float bladeRotationSpeed = 500f;

    public float buttonSpeed = 2f;
    private bool isRotating = false;
    private bool hasSliced = false;

    public float cutforce = 100f;

    public float linecastDistance = 1f;
    public float linecastRadius = 0.1f;


    public LayerMask slicableLayer;
    public GameObject lowerhullSlice;


    // Start is called before the first frame update
    void Start()
    {
        handle.Handle.localRotation = Quaternion.Euler(-800f * Mathf.Deg2Rad, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isRotating = true;
        }

        if (isRotating)
        {
            // Rotate the handle towards 0 radians
            RotateHandleTowardsZero();
            
            // Rotate the blade
            bladeRotation.bladeTransform.Rotate(Vector3.right * Time.deltaTime * bladeRotationSpeed);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
            ResetHandleToRest();
        }

        // if(handle.Handle.localRotation == Quaternion.Euler(5f, 0f, 0f) && !hasSliced)
        // {
        //     RaycastHit hit;
        //     Vector3 endPosition = transform.position + transform.forward * linecastDistance;
        //     if (Physics.Linecast(transform.position, endPosition, out hit, slicableLayer))
        //     {
        //         // If linecast hits a slicable object, slice it
        //         Slice(hit.collider.gameObject);
        //     }
            
        //     hasSliced = true;
        // }

        if(Input.GetKey("x"))
        {
            Application.Quit();
        }
    }

    void FixedUpdate() {
        bool hashit = Physics.Linecast(startPosition.position, endPosition.position, out RaycastHit hit, slicableLayer);
        if(hashit)
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }    
    }

    void RotateHandleTowardsZero()
    {
        // Rotate the handle towards 0 radians
        handle.Handle.localRotation = Quaternion.RotateTowards(handle.Handle.localRotation, Quaternion.Euler(5f, 0f, 0f), rotationSpeed * Time.deltaTime);
    }

    void ResetHandleToRest()
    {
        handle.Handle.localRotation = Quaternion.Euler(-800f * Mathf.Deg2Rad, 0f, 0f);
    }

    //slicing object when the blade reaches end
    void Slice(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endPosition.position - startPosition.position, velocity);
        planeNormal.Normalize();
        SlicedHull hull = target.Slice(endPosition.position, planeNormal);
    
        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, upperMaterial);
            SliceObject(upperHull);


            GameObject lowerHull = hull.CreateLowerHull(target, lowerMaterial);
            // SliceObject(lowerHull);
            lowerHull.AddComponent<slabMoveScript>();
            SliceObject(lowerHull);

            lowerHull.layer = LayerMask.NameToLayer("SliceableLayer");
            
            
            Destroy(target);
        }
    }
    void SliceObject(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        rb.useGravity = true;
        MeshCollider meshCollider = slicedObject.AddComponent<MeshCollider>();
        meshCollider.convex = true;
        rb.AddExplosionForce(cutforce, slicedObject.transform.position, 1);
    }




    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    // void OnTriggerEnter(Collider collision)
    // {
    //     if (collision.gameObject == Slab)
    //     {
    //         Debug.Log("Blade collided with NewSlab");
    //     }
    // }



}
[System.Serializable]
public class BladeHandle
{
    public Transform Handle;
}

[System.Serializable]
public class BladeMeshs
{
    public MeshRenderer Blade;
}

[System.Serializable]
public class bladeRotation
{
    public Transform bladeTransform;
}

[System.Serializable]
public class bladeCollision
{
    public Collider bladeCollider;
}
