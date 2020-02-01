using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject levelWrapper;
    public GameObject railPrefab;

    public List<GameObject> railPool;

    // Start is called before the first frame update
    void Start ()
    {
        EventManager.OnRailsLeave += RailLeave;

        for (int i = 0; i < levelWrapper.transform.childCount; i++)
        {
            railPool.Add (levelWrapper.transform.GetChild (i).gameObject);
        }
    }

    // Update is called once per frame
    void Update ()
    {

    }

    void RailLeave ()
    {
        SpawnRail ();

    }

    void SpawnRail ()
    {
        Debug.Log ("Spawn Rail");
        MovePostion ();
        Move (railPool, 0, railPool.Count-1);

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
        Vector3 _offset = new Vector3(4.2f, 0f, 0f);
        railPool[0].transform.position = lastPosition + _offset;
    }

}