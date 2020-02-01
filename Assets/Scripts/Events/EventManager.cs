using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager em;

    private void Awake () { em = this; }

    public delegate void OnRailsLeaveDelegate ();
    static public event OnRailsLeaveDelegate OnRailsLeave;

    public void RailsLeave ()
    {
        if (OnRailsLeave != null)
        {
            OnRailsLeave ();
        }
    }

    // public delegate void OnCantShowStickDelegate ();
    // static public event OnCantShowStickDelegate OnCantShowStick;

    // public void CantShowStick ()
    // {
    //     if (OnCantShowStick != null)
    //     {
    //         OnCantShowStick ();
    //     }
    // }

}

/*

    public delegate void OnTargetChangeDelegate (bool value, GameObject target);
    static public event OnTargetChangeDelegate onTargetChange;

    public void ChangeTarget (bool value, GameObject target)
    {
        if (onHealthChange != null)
        {
            onTargetChange (value, target);
        }
    }

    public event Action<float, GameObject> OnSliderChange;
    public void ChangeSlider (float value, GameObject target)
    {
        if (OnSliderChange != null)
            OnSliderChange (value, target);
    }
*/