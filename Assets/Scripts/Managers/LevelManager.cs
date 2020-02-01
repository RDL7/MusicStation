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
    public List<GameObject> railOrigin;
    public List<GameObject> emptyRailPool;
    
    void Start ()
    {
        EventManager.OnRailsLeave += RailLeave;

        for (int i = 0; i < levelWrapper.transform.childCount; i++)
        {
            railPool.Add (levelWrapper.transform.GetChild (i).gameObject);
            railOrigin.Add (levelWrapper.transform.GetChild (i).gameObject);
        }

        FirstStickCheck ();
    }

    private void Update ()
    {
        if (Input.GetKeyDown (KeyCode.Space))
        {
            if (emptyRailPool.Count > 0)
            {
                emptyRailPool[0].GetComponent<RailController> ().canShowStick = true;
                emptyRailPool.RemoveAt (0);
            }
        }
    }

    void RailLeave ()
    {
        SpawnRail ();
    }

    void SpawnRail ()
    {
        Debug.Log ("Spawn Rail");

        GameObject stick = railPool[0].gameObject;
        stick.GetComponent<RailController> ().canShowStick = RandomiseSticks ();

        bool cantShow = stick.GetComponent<RailController> ().canShowStick;

        if (!cantShow)
        {
            emptyRailPool.Add (stick);
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
}