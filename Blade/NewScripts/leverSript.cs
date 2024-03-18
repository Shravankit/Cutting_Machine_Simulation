using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverSript : MonoBehaviour
{

    [SerializeField] GameObject LeverRotate;
    [SerializeField] GameObject LeverMove;

    [SerializeField] float leverRotateSpeed = 100f;
    [SerializeField] float leverMoveSpeed = 10f;

    private Vector3 initialRotatePosition;
    private Vector3 initialMovePosition;

    // Start is called before the first frame update
    void Start()
    {
        initialRotatePosition = LeverRotate.transform.localPosition;
        initialMovePosition = LeverMove.transform.localPosition; 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("w"))
        {
            LeverRotation(LeverRotate);
            LeverMovement(LeverMove);
        }

        if(Input.GetKey("s"))
        {
            LeverBackRotation(LeverRotate);
            LeverBackMovement(LeverMove);
        }
    }

    void LeverRotation( GameObject lever)
    {
        lever.transform.Rotate(Vector3.forward, -leverRotateSpeed *  Time.deltaTime);
        lever.transform.Translate(Vector3.forward * -leverMoveSpeed *  Time.deltaTime);
    }

    void LeverMovement(GameObject lever)
    {
        lever.transform.Translate(Vector3.forward * -leverMoveSpeed * Time.deltaTime);
    }

    void LeverBackRotation( GameObject lever)
    {
        lever.transform.Rotate(Vector3.forward, leverRotateSpeed *  Time.deltaTime);
        lever.transform.Translate(Vector3.forward * leverMoveSpeed *  Time.deltaTime);
    }

    void LeverBackMovement(GameObject lever)
    {
        lever.transform.Translate(Vector3.forward * leverMoveSpeed * Time.deltaTime);
    }
}
