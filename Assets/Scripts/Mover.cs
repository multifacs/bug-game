using UnityEngine;

public class Mover : MonoBehaviour
{
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
            transform.Translate(Vector3.left * Time.deltaTime);
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
