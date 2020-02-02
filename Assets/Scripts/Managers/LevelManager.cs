using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject levelWrapper;

    public GameObject playerObject;
    public GameObject railPrefab;

    public List<GameObject> railPool;
    public List<GameObject> emptyRailPool;
    public List<int> randomColors;

    private GameObject BtnUI;
    private int randomCount = 0;

    public Color[] colors;
    public List<Vector3> railPositions;

    public bool showCombo = false;

    private void Awake ()
    {

        for (int i = 0; i < levelWrapper.transform.childCount; i++)
        {
            railPool.Add (levelWrapper.transform.GetChild (i).gameObject);
            // railPositions.Add (levelWrapper.transform.GetChild (i).localPosition);
        }

        for (int i = 0; i < levelWrapper.transform.childCount; i++)
        {
            // railPool.Add (levelWrapper.transform.GetChild (i).gameObject);
            railPositions.Add (levelWrapper.transform.GetChild (i).position);
        }

        FirstStickCheck ();
    }

    void Start ()
    {
        print (levelWrapper.transform.childCount);
        EventManager.OnRailsLeave += RailLeave;
        EventManager.OnShowCombo += ShowCombo;

        BtnUI = InputManager.im.GameUI;

    }

    private void Update ()
    {
        if (emptyRailPool.Count > 0 && !BtnUI.activeSelf)
        {
            if (!showCombo)
            {
                ShowCombo (emptyRailPool[0]);
                showCombo = true;
            }
        }

        if (Input.GetKeyDown (KeyCode.Space) && !GameManager.instance.isPaused)
        {
            if (emptyRailPool.Count > 0)
            {
                showCombo = false;
                emptyRailPool[0].GetComponent<RailController> ().ShowSticks ();
                emptyRailPool.RemoveAt (0);
            }
        }

        if (!showCombo)
        {
            HideCombo ();
        }
    }

    void RailLeave ()
    {
        SpawnRail ();
        ChangeOrder ();
    }

    void SpawnRail ()
    {
        GameObject stick = railPool[0].gameObject;
        stick.SetActive (false);
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
        stick.SetActive (true);
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

    void ChangeOrder ()
    {
        for (int i = 0; i < railPool.Count; i++)
        {
            railPool[i].transform.GetChild (railPool[i].transform.childCount - 1).GetComponent<SpriteRenderer> ().sortingOrder = i * -1;
            railPool[i].transform.GetChild (railPool[i].transform.childCount - 2).GetComponent<SpriteRenderer> ().sortingOrder = i;
        }
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

            // bool cantShow = stick.GetComponent<RailController> ().canShowStick;
            // if (!cantShow)
            // {
            //     emptyRailPool.Add (stick);
            // }
            stick.GetComponent<RailController> ().stickCount = UnityEngine.Random.Range (0, 3);
        }
        ChangeOrder ();
    }

    void ShowCombo (GameObject stick)
    {
        randomCount = 0;
        randomCount = UnityEngine.Random.Range (0, 3);
        stick.GetComponent<RailController> ().stickCount = randomCount;

        if (!BtnUI.activeSelf)
        {
            randomColors.Clear ();
            for (int i = 0; i < 3; i++)
            {
                int randomColor = UnityEngine.Random.Range (0, 7);
                if (!randomColors.Contains (randomColor))
                {
                    randomColors.Add (randomColor);
                    ChangeComboColor (i, randomColor);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                BtnUI.transform.GetChild (i).gameObject.SetActive (false);
            }
            for (int i = 0; i <= randomCount; i++)
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

    void ChangeComboColor (int id, int color)
    {
        Image box = BtnUI.transform.GetChild (id).GetChild (0).GetComponent<Image> ();
        box.color = colors[color];
    }

    public void RestartAll ()
    {
        for (int i = 0; i < levelWrapper.transform.childCount; i++)
        {
            levelWrapper.transform.GetChild (i).transform.position = railPositions[i];
        }

        playerObject.transform.position = Vector3.zero;
    }
}