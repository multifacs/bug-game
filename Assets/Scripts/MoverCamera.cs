using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverCamera : MonoBehaviour
{
    private float speed = 0.2f;
    private bool isRun = true;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("MoverCamera - Start");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("f1"))
        {
            Debug.Log("F1 pressed");
        }


        transform.Rotate(0.0f, Input.GetAxis("Horizontal") * speed, 0.0f);
        if (isRun)
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider collider)
    {

        isRun = false;
        if (collider.tag == "")
        {
            //set velocity 0
            //adjust the object position (the object may overlap with the block)
        }
    }

}
