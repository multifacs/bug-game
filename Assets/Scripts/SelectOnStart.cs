using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnStart : MonoBehaviour
{
    // Start is called before the first frame update
//    void Start()
    void OnEnable()
    {
        Debug.Log("OnEnable");

        Invoke("Select", 0.3f);




        //EventSystem.current.SetSelectedGameObject(null);
        //EventSystem.current.SetSelectedGameObject(this.gameObject);
        //EventSystem.current.firstSelectedGameObject = this.gameObject;
    }


    public void Select()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(this.gameObject);
        EventSystem.current.firstSelectedGameObject = this.gameObject;
    }
}
