using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailController : MonoBehaviour
{

    public GameObject stick;
    public bool canShowStick = true;
    public bool doOnce = false;
    // Start is called before the first frame update
    void Start ()
    {

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

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Back Trigger")
        {
            EventManager.em.RailsLeave ();
        }

        if (!canShowStick)
        {
            if (other.tag == "Front Trigger")
            {
                Debug.Log (transform.name);
                Time.timeScale = 0f;
            }
        }
    }
}