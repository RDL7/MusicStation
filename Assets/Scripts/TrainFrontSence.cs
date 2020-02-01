using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainFrontSence : MonoBehaviour
{
    //public GameObject BtnUI;
    //List<Image> KeysToPress = new List<Image>();

    // Start is called before the first frame update
    void Start()
    {
        //BtnUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //if (col.tag == "Rails")
        //{
        //    //print("Nextrail");
        //    //BtnUI
        //    if (!col.gameObject.GetComponent<RailController>().stick.activeSelf)
        //    {
        //        //BtnUI.SetActive(true);

        //    }
        //}

        //if (col.tag == "Rails")
        //{
        //    //print("Nextrail");
        //    //BtnUI
        //    if (col.gameObject.GetComponent<RailController>().stick.activeSelf)
        //    {
        //        BtnUI.SetActive(false);
        //    }
        //}
        ////print("Nextrail222");
    }
}
