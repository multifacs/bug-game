using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed = 0.2f;
    private bool isRun = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRun)
        {
            transform.Rotate(0.0f, speed, 0.0f);
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
