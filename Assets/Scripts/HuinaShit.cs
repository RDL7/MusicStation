/*using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using MidiJack;



public class HuinaShit : MonoBehaviour
{

    public bool _getKeyUp;
    private int counter;
    private KeyCode newKey;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (MidiMaster.GetKeyUp(kcode))
                {
                    ++counter;
                    if (counter >= 2)
                    {
                        newKey = kcode;
                    }
                    _getKeyUp = false;
                    Debug.Log(" Counter : " + counter + " / keycode : " + kcode + " ");
                    if (counter == 1)
                    {
                        break;
                    }
                }
            }
        
    }
}*/
