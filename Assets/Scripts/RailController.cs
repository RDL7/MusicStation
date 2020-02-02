using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RailController : MonoBehaviour
{

    // public GameObject stick;
    public bool canShowStick = true;
    public bool doOnce = false;

    public GameObject[] sticks;
    public Sprite[] houses;
    public Sprite[] decors;

    public GameObject[] decorObjects;    

    public int stickCount;

    // int id = 0;

    void OnEnable ()
    {
        if (canShowStick)
        {
            ShowSticks (stickCount);
        }
        else
        {
            HideSticks ();
        }
        ChangeHome ();
    }

    void Update ()
    {

    }

    void HideSticks ()
    {
        for (int i = 0; i < sticks.Length; i++)
        {
            sticks[i].SetActive (false);
        }
    }

    public void ShowSticks (int count = 10)
    {
        if (count == 10)
        {
            count = stickCount;
        }
        canShowStick = true;
        HideSticks ();
        sticks[count].SetActive (true);
    }

    void ChangeHome ()
    {
        int randomHouse = UnityEngine.Random.Range (0, houses.Length);
        int randomDecor = UnityEngine.Random.Range (0, decors.Length);

        transform.GetChild (transform.childCount - 2).GetComponent<SpriteRenderer> ().sprite = houses[randomHouse];
        transform.GetChild (transform.childCount - 1).GetComponent<SpriteRenderer> ().sprite = houses[randomHouse];

        for (int i = 0; i < 2; i++)
        {
            decorObjects[i].GetComponent<SpriteRenderer> ().sprite = decors[randomDecor];
        }
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
                EventManager.em.GameOverEvent ();
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