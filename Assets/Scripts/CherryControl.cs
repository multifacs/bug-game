using UnityEngine;

public class CherryControl : MonoBehaviour
{
    public float speed = 0.2f;
    private bool isRun = true;
    public float dx = 0;
    public float dy = 0;
//    private AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
 //       sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRun)
        {
            transform.Rotate(0.0f, speed, 0.0f);

            if (Time.timeSinceLevelLoad > Configuration.start_pause) {
                float xLimit = Configuration.xOffset;
                transform.Translate(Vector3.left * Time.deltaTime * -dx * 5, Space.World);
                transform.Translate(Vector3.forward * Time.deltaTime * -dy * 5, Space.World);

                if (transform.position.x > xLimit)
                {
                    transform.position = new Vector3(xLimit, transform.position.y, transform.position.z);
                }
                if (transform.position.x < -xLimit)
                {
                    transform.position = new Vector3(-xLimit, transform.position.y, transform.position.z);
                }
                if (transform.position.z > 50f)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, 50f);
                }
                if (transform.position.z < 0f)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
                }
            }

        }

    }


    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Cherry Trigger tag:" + collider.tag);
 
        isRun = false;
        if (collider.tag == "LadyBug")
        {
 //           sound.Play();
            GameObject.FindGameObjectWithTag("AudioWin").GetComponent<AudioInterScene>().PlayMusic();
            //set velocity 0
            //adjust the object position (the object may overlap with the block)
        }
    }
}
