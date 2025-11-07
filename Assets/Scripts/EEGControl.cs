using TMPro;
using UnityEngine;

public class EEGControl : MonoBehaviour
{
    private GameObject EEGCircle;
    private RectTransform rectTransform;
    private TextMeshProUGUI tmpElem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Configuration.Load();

        EEGCircle = GameObject.Find("EEGCircle");
        rectTransform = EEGCircle.GetComponent<RectTransform>();
        tmpElem = EEGCircle.GetComponent<TextMeshProUGUI>();
        tmpElem.color = Color.black;
        rectTransform.anchoredPosition = new Vector2(Configuration.circle_x, Configuration.circle_y);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad > Configuration.circle_black_time)
        {
            tmpElem.color = Color.white;
        }
    }
}
