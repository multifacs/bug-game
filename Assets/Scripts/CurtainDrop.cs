using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainDrop : MonoBehaviour
{
    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad < 5)
        {
            //transform.Translate();
            rectTransform.Translate(Vector2.down * Time.deltaTime * 300);
        }

        if (Time.timeSinceLevelLoad > 7)
        {
            Destroy(rectTransform.gameObject);
        }
    }
}
