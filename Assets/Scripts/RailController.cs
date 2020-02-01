using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailController : MonoBehaviour
{

    // public GameObject stick;
    public bool canShowStick = true;
    public bool doOnce = false;

    public GameObject[] sticks;

    public int stickCount;

    void Start ()
    {

    }

    void Update ()
    {
        if (canShowStick)
        {
            ShowSticks (stickCount);
        }
        else
        {
            HideSticks ();
        }
    }

    void HideSticks ()
    {
        for (int i = 0; i < sticks.Length; i++)
        {
            sticks[i].SetActive (false);
        }
    }

    void ShowSticks (int count)
    {
        HideSticks ();
        sticks[count].SetActive (true);
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Back Trigger")
        {
            EventManager.em.RailsLeave ();
        }

        if (other.tag == "SenceTrigger")
        {
            if (!canShowStick)
            {
                EventManager.em.ShowCombo (gameObject);
            }
        }

        if (!canShowStick)
        {
            if (other.tag == "Front Trigger")
            {
                //    Debug.Log (transform.name);
                //    Time.timeScale = 0f;
            }
        }
    }

    private void OnTriggerStay2D (Collider2D other)
    {
        if (other.tag == "SenceTrigger")
        {

        }
    }

    private void OnTriggerExit2D (Collider2D other)
    {

        if (other.tag == "SenceTrigger")
        {
            // BtnUI.SetActive (false);
        }
    }

    // void RandomiseCombo ()
    // {
    //     randomCount = Random.Range (0, 3);

    //     for (int i = 0; i < randomCount ; i++)
    //     {
    //         BtnUI.transform.GetChild(i).gameObject.SetActive(true);
    //     }

    //     // sticks[randomCount].SetActive (true);
    // }
}