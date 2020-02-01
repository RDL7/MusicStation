using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject levelWrapper;

    public GameObject playerObject;
    public GameObject railPrefab;

    public List<GameObject> railPool;

    public List<GameObject> emptyRailPool;

    private GameObject BtnUI;
    int randomCount = 0;

    void Start ()
    {
        EventManager.OnRailsLeave += RailLeave;
        EventManager.OnShowCombo += ShowCombo;

        for (int i = 0; i < levelWrapper.transform.childCount; i++)
        {
            railPool.Add (levelWrapper.transform.GetChild (i).gameObject);
        }

        FirstStickCheck ();
        BtnUI = InputManager.im.GameUI;
    }

    private void Update ()
    {
        if (Input.GetKeyDown (KeyCode.Space))
        {
            if (emptyRailPool.Count > 0)
            {
                emptyRailPool[0].GetComponent<RailController> ().canShowStick = true;
                emptyRailPool.RemoveAt (0);
                HideCombo ();
            }
        }
        if (emptyRailPool.Count > 0 && !BtnUI.activeSelf)
        {
            ShowCombo (emptyRailPool[0]);
            print ("show combo");
        }
    }

    void RailLeave ()
    {
        SpawnRail ();
    }

    void SpawnRail ()
    {
        GameObject stick = railPool[0].gameObject;
        stick.GetComponent<RailController> ().canShowStick = RandomiseSticks ();

        bool cantShow = stick.GetComponent<RailController> ().canShowStick;

        if (!cantShow && emptyRailPool.Count < 3)
        {
            emptyRailPool.Add (stick);
        }
        else
        {
            stick.GetComponent<RailController> ().canShowStick = true;
        }

        MovePostion ();
        Move (railPool, 0, railPool.Count - 1);
    }

    public void Move<T> (List<T> list, int oldIndex, int newIndex)
    {
        T item = list[oldIndex];
        list.RemoveAt (oldIndex);
        list.Insert (newIndex, item);
    }

    public void MovePostion ()
    {
        Vector3 lastPosition = railPool[railPool.Count - 1].transform.position;
        Vector3 _offset = new Vector3 (4.3f, 0f, 0f);
        railPool[0].transform.position = lastPosition + _offset;
    }

    public bool RandomiseSticks ()
    {
        bool canStick = false;
        int randomStickInt = UnityEngine.Random.Range (0, 10);

        if (randomStickInt < 5)
        {
            canStick = false;
        }
        else
        {
            canStick = true;
        }

        return canStick;
    }

    void FirstStickCheck ()
    {
        for (int i = 0; i < railPool.Count; i++)
        {
            GameObject stick = railPool[i].gameObject;
            bool cantShow = stick.GetComponent<RailController> ().canShowStick;
            if (!cantShow)
            {
                emptyRailPool.Add (stick);
            }
        }
    }

    void ShowCombo (GameObject stick)
    {

        randomCount = 0;
        randomCount = UnityEngine.Random.Range (0, 3);

        stick.GetComponent<RailController> ().stickCount = randomCount;

        if (!BtnUI.activeSelf)
        {
            for (int i = 0; i < 3; i++)
            {
                BtnUI.transform.GetChild (i).gameObject.SetActive (false);
            }
            for (int i = 0; i < randomCount; i++)
            {
                BtnUI.transform.GetChild (i).gameObject.SetActive (true);
            }

            BtnUI.SetActive (true);
        }

    }

    void HideCombo ()
    {
        BtnUI.SetActive (false);
    }
}