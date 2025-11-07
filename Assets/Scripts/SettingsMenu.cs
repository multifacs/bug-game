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
        var speed = GameObject.Find("SpeedMult").GetComponent<Toggle>();
        speed.isOn = Configuration.speed_mult != 1;
        Debug.Log("config speed " + Configuration.speed_mult);
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

    public void SpeedMult()
    {
        var toggle = GameObject.Find("SpeedMult").GetComponent<Toggle>();
        Configuration.speed_mult = toggle.isOn ? 2 : 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
