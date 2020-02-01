using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailController : MonoBehaviour
{

    public GameObject stick;
    public bool canShowStick = true;
    public bool doOnce = false;

    GameObject BtnUI;

    // Start is called before the first frame update
    void Start ()
    {
        BtnUI = GameObject.Find("GAME MANAGER").GetComponent<InputManager>().GameUI;

        if (BtnUI)
        {
            BtnUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update ()
    {

        if (canShowStick)
        {

            stick.SetActive (canShowStick);
        }
        else
        {
            stick.SetActive (canShowStick);
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Back Trigger")
        {
            EventManager.em.RailsLeave();
        }

        if (other.tag == "SenceTrigger")
        {
            print("InactiveRail");
            //BtnUI
            if (!gameObject.GetComponent<RailController>().stick.activeSelf)
            {
                BtnUI.SetActive(true);

            }
        }

        if (other.tag == "SenceTrigger")
        {
            //print("Nextrail");
            //BtnUI
            if (gameObject.GetComponent<RailController>().stick.activeSelf)
            {
                BtnUI.SetActive(false);
            }
        }

        //print("Nextrail222");

        //game pause suff
        //if (!canShowStick)
        //{
        //    if (other.tag == "Front Trigger")
        //    {
        //        Debug.Log (transform.name);
        //        Time.timeScale = 0f;
        //    }
        //}
    }
}