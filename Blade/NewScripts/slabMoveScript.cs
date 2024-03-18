using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slabMoveScript : MonoBehaviour
{
    public float slabSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("a"))
        {
            transform.Translate(Vector3.forward * slabSpeed * Time.deltaTime);
            Debug.Log("'a' key is pressed");
        }

        if(Input.GetKey("d"))
        {
            transform.Translate(Vector3.forward * -slabSpeed * Time.deltaTime);
            Debug.Log("'d' key is pressed");
        }
    }
}
