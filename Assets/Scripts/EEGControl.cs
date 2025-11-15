using System.Collections;
using TMPro;
using UnityEngine;

public class EEGControl : MonoBehaviour
{
    private GameObject EEGCircle;
    private RectTransform _rectTransform;
    private TextMeshProUGUI _tmpElem;

    private Coroutine _currentBlink;

    void Start()
    {
        Configuration.Load();

        EEGCircle = GameObject.Find("EEGCircle");
        _rectTransform = EEGCircle.GetComponent<RectTransform>();
        _tmpElem = EEGCircle.GetComponent<TextMeshProUGUI>();
        _tmpElem.color = Color.white;
        _rectTransform.anchoredPosition = new Vector2(Configuration.circle_x, Configuration.circle_y);
    }

    // Single blink for movement start
    public void BlinkOnce()
    {
        if (_currentBlink != null)
            StopCoroutine(_currentBlink);

        _currentBlink = StartCoroutine(BlinkCoroutine(1));
    }

    // Double blink for collision
    public void BlinkTwice()
    {
        if (_currentBlink != null)
            StopCoroutine(_currentBlink);

        _currentBlink = StartCoroutine(BlinkCoroutine(2));
    }

    private IEnumerator BlinkCoroutine(int blinkCount)
    {
        for (int i = 0; i < blinkCount; i++)
        {
            // Turn black
            _tmpElem.color = Color.black;
            yield return new WaitForSeconds(Configuration.circle_black_time);

            // Turn white
            _tmpElem.color = Color.white;

            // If more blinks coming, wait a bit between them
            //yield return new WaitForSeconds(Configuration.circle_black_time);
            if (i < blinkCount - 1)
            {
                yield return new WaitForSeconds(Configuration.circle_black_time);
            }
        }

        _currentBlink = null;
    }

    // Keep old method if you still need it elsewhere
    public void Blink()
    {
        BlinkOnce();
    }
}
