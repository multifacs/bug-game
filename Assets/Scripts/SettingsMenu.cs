using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var trees = GameObject.Find("ShowTrees").GetComponent<Toggle>();
        trees.isOn = LoadSituations.showTrees;
        var hills = GameObject.Find("ShowHills").GetComponent<Toggle>();
        hills.isOn = LoadSituations.showHills;
    }

    public void ShowTrees()
    {
        var toggle = GameObject.Find("ShowTrees").GetComponent<Toggle>();
 
        LoadSituations.showTrees = toggle.isOn;
    }

    public void ShowHills()
    {
        var toggle = GameObject.Find("ShowHills").GetComponent<Toggle>();

        LoadSituations.showHills = toggle.isOn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
