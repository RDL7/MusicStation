using MidiJack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

public class LevelManager : MonoBehaviour
{
    public GameObject levelWrapper;

    public GameObject playerObject;
    public GameObject railPrefab;

    public List<GameObject> railPool;
    public List<GameObject> emptyRailPool;
    //public List<int> randomColors;

    private GameObject BtnUI;
    private int randomCount = 0;

    public Color[] colors;
    //GenColorStructure MyGenColorStructure = new GenColorStructure();
    public List<GenColorStructure> randomColors = new List<GenColorStructure>();

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

        if (emptyRailPool.Count > 0 && !BtnUI.activeSelf)
        {
            ShowCombo (emptyRailPool[0]);
            print ("show combo");
        }

        //if (Input.GetKeyDown (KeyCode.Space))
        //{
        //    if (emptyRailPool.Count > 0)
        //    {
        //        emptyRailPool[0].GetComponent<RailController> ().canShowStick = true;
        //        emptyRailPool.RemoveAt (0);
        //        HideCombo ();
        //    }
        //    print ("show combo press");
        //}
    }

    void OnEnable()
    {
        MidiMaster.noteOnDelegate += NoteOn;
    }

    void OnDisable()
    {
        MidiMaster.noteOnDelegate -= NoteOn;
    }

    void NoteOn(MidiChannel channel, int note, float velocity)
    {
        //print("note: " + note + "Enum: "+  (int)randomColors[0].ColorEnum + (int)randomColors[1].ColorEnum  + (int)randomColors[2].ColorEnum);

        for (int i = 0; i < randomColors.Count; i++)
        {
            if (randomColors[i] != null)
            {
                if (note == (int)randomColors[i].ColorEnum)
                {
                    Image box = BtnUI.transform.GetChild(randomColors[i].BtnNumber).GetChild(0).GetComponent<Image>();
                    box.color = new Color32(0, 0, 0, 255);

                    randomColors.RemoveAt(i);

                    if (randomColors.Count == 0)
                    {
                        //kad nospiez ko vajag
                        if (emptyRailPool.Count > 0)
                        {
                            emptyRailPool[0].GetComponent<RailController>().canShowStick = true;
                            emptyRailPool.RemoveAt(0);
                            HideCombo();
                        }
                    }
                }
            }
        }

        //    if (note == (int)randomColors[0].ColorEnum || note == (int)randomColors[1].ColorEnum || note == (int)randomColors[2].ColorEnum)
        //{
        //    if (emptyRailPool.Count > 0)
        //    {
        //        emptyRailPool[0].GetComponent<RailController>().canShowStick = true;
        //        emptyRailPool.RemoveAt(0);
        //        HideCombo();
        //    }
        //    print("show combo press");
        //}
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
            // bool cantShow = stick.GetComponent<RailController> ().canShowStick;
            // if (!cantShow)
            // {
            //     emptyRailPool.Add (stick);
            // }
            stick.GetComponent<RailController> ().stickCount = UnityEngine.Random.Range (0, 3);
        }
    }

    void ShowCombo (GameObject stick)
    {
        randomCount = 0;
        randomCount = UnityEngine.Random.Range (0, 3);
        stick.GetComponent<RailController> ().stickCount = randomCount;

        if (!BtnUI.activeSelf)
        {
            randomColors.Clear ();
            for (int i = 0; i <= randomCount; i++)
            {
                int randomColor = UnityEngine.Random.Range (0, 7);
                NoteColors ColorEnum = GetEnumForColor(randomColor);

                //if (!randomColors[i].Color.Contains(randomColor))
                //{
                    //randomColors.Add (new GenColorStructure randomColor);
                    randomColors.Add(new GenColorStructure
                    {
                        Color = randomColor,
                        ColorEnum = ColorEnum,
                        BtnNumber = i
                    });

                    ChangeComboColor (i, randomColor);
                //}
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

    NoteColors GetEnumForColor(int RandomColor)
    {
        NoteColors ColorEnum = NoteColors.Red;

        switch (RandomColor)
        {
            case 0:
                ColorEnum = NoteColors.Red;
                return ColorEnum;
                break;
            case 1:
                ColorEnum = NoteColors.Orange;
                return ColorEnum;
                break;
            case 2:
                ColorEnum = NoteColors.Yellow;
                return ColorEnum;
                break;
            case 3:
                ColorEnum = NoteColors.Green;
                return ColorEnum;
                break;
            case 4:
                ColorEnum = NoteColors.SkyBlue;
                return ColorEnum;
                break;
            case 5:
                ColorEnum = NoteColors.Blue;
                return ColorEnum;
                break;
            case 6:
                ColorEnum = NoteColors.Purple;
                return ColorEnum;
                break;
        }

        return ColorEnum;
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
}