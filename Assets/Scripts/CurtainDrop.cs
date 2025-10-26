using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainDrop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad < 5)
        {
            transform.Translate(Vector3.back * Time.deltaTime * 2);
        }
    }
}
