using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System;

public class CrashCounter : MonoBehaviour
{
    public TMP_Text tmp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float Accident = GameObject.FindWithTag("Car").GetComponent<NewBehaviourScript>().Accident;
        Debug.LogWarning(Accident);
        tmp.text = "Crash count : " + Accident;
        

    }
}
