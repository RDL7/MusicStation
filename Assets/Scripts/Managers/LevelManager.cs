using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MidiJack;
using UnityEngine;
using UnityEngine.UI;
using static Enums;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public GameObject levelWrapper;

    public GameObject playerObject;
    public GameObject railPrefab;

    public List<GameObject> railPool;
    public List<GameObject> emptyRailPool;

    private GameObject BtnUI;
    private int randomCount = 0;

    public Color[] colors;
    public List<Vector3> railPositions = new List<Vector3> ();

    public float SpeedIncreseIfWrongNote;

    public GameManager MyGameManager;
    bool OkKey = false;

    KeyCode keyCode;

    //GenColorStructure MyGenColorStructure = new GenColorStructure();

    public List<GenColorStructure> randomColors = new List<GenColorStructure> ();
    public List<GenColorStructureKeyboard> randomColorsKeyboard = new List<GenColorStructureKeyboard> ();

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

        print (railPositions.Count + " 0 ");
        FirstStickCheck ();
    }

    void Start ()
    {
        EventManager.OnRailsLeave += RailLeave;
        EventManager.OnShowCombo += ShowCombo;

        BtnUI = InputManager.im.GameUI;
    }

    private void Update ()
    {
        if (emptyRailPool.Count > 0 && !BtnUI.activeSelf && !GameManager.instance.isPaused)
        {
            if (!showCombo)
            {
                ShowCombo (emptyRailPool[0]);
                showCombo = true;
            }
        }
        else if (GameManager.instance.isPaused && showCombo)
        {
            HideCombo ();
        }
        else if (!showCombo)
        {
            HideCombo ();
        }

    }

    private void LateUpdate ()
    {
        PressKey ();
    }

    void OnEnable ()
    {
        MidiMaster.noteOnDelegate += NoteOn;
        EventManager.OnPressKey += Note;
    }

    void OnDisable ()
    {
        MidiMaster.noteOnDelegate -= NoteOn;
    }

    void PressKey ()
    {
        foreach (KeyCode kcode in Enum.GetValues (typeof (KeyCode)))
        {
            if (Input.GetKeyDown (kcode))
            {
                keyCode = kcode;
                Debug.Log ("KeyCode down: " + kcode);
                EventManager.em.PressKey (keyCode);
            }
        }
    }

    void NoteOn (MidiChannel channel, int note, float velocity)
    {
        //print("note: " + note + "Enum: "+  (int)randomColors[0].ColorEnum + (int)randomColors[1].ColorEnum  + (int)randomColors[2].ColorEnum);
        OkKey = false;

        for (int i = 0; i < randomColors.Count; i++)
        {
            if (randomColors[i] != null)
            {
                if (note == (int) randomColors[i].ColorEnum)
                {
                    Image box = BtnUI.transform.GetChild (randomColors[i].BtnNumber).GetChild (0).GetComponent<Image> ();
                    box.color = new Color32 (0, 0, 0, 255);

                    randomColors.RemoveAt (i);
                    //bool Ok key true
                    OkKey = true;

                    if (randomColors.Count == 0)
                    {
                        if (emptyRailPool.Count > 0)
                        {
                            emptyRailPool[0].GetComponent<RailController> ().ShowSticks ();
                            emptyRailPool.RemoveAt (0);
                            HideCombo ();
                        }
                    }
                }
            }
        }

        //increase speed if bool is false, that i set in if (note == (int) randomColors[i].ColorEnum) 
        if (!OkKey)
        {
            //increase speed as  penalty
            MyGameManager.playerSpeed += SpeedIncreseIfWrongNote;
            //print("Speed: " + MyGameManager.playerSpeed);
        }
    }

    void Note (KeyCode keyCode)
    {
        print ("keyboard event");
        OkKey = false;

        for (int i = 0; i < randomColorsKeyboard.Count; i++)
        {
            if (randomColorsKeyboard[i] != null)
            {
                if (keyCode == randomColorsKeyboard[i].ColorEnum)
                {
                    Image box = BtnUI.transform.GetChild (randomColorsKeyboard[i].BtnNumber).GetChild (0).GetComponent<Image> ();
                    box.color = new Color32 (0, 0, 0, 255);

                    randomColorsKeyboard.RemoveAt (i);
                    //bool Ok key true
                    OkKey = true;

                    if (randomColorsKeyboard.Count == 0)
                    {
                        if (emptyRailPool.Count > 0)
                        {
                            emptyRailPool[0].GetComponent<RailController> ().ShowSticks ();
                            emptyRailPool.RemoveAt (0);
                            HideCombo ();
                        }
                    }
                }
            }
        }

        if (!OkKey)
        {
            //increase speed as  penalty
            MyGameManager.playerSpeed += SpeedIncreseIfWrongNote;
            //print("Speed: " + MyGameManager.playerSpeed);
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

            bool cantShow = stick.GetComponent<RailController> ().canShowStick;

            if (!cantShow)
            {
                stick.GetComponent<RailController> ().ShowSticks ();

            }
            stick.GetComponent<RailController> ().stickCount = UnityEngine.Random.Range (0, 3);
        }
        ChangeOrder ();
    }

    void ShowCombo (GameObject stick)
    {
        randomCount = 0;
        randomCount = UnityEngine.Random.Range (0, 3);
        stick.GetComponent<RailController> ().stickCount = randomCount;
        stick.GetComponent<RailController> ().stickColors.Clear();

        if (!BtnUI.activeSelf)
        {
            randomColors.Clear ();
            for (int i = 0; i <= randomCount; i++)
            {
                int randomColor = UnityEngine.Random.Range (0, 7);
                NoteColors ColorEnum = GetEnumForColor (randomColor);
                KeyCode ColorEnum2 = GetEnumForColorKeyboard (randomColor);

                randomColors.Add (new GenColorStructure
                {
                    Color = randomColor,
                        ColorEnum = ColorEnum,
                        BtnNumber = i
                });

                randomColorsKeyboard.Add (new GenColorStructureKeyboard
                {
                    Color = randomColor,
                        ColorEnum = ColorEnum2,
                        BtnNumber = i
                });

                ChangeComboColor (i, randomColor, ColorEnum2);
                stick.GetComponent<RailController> ().stickColors.Add(colors[randomColor]);
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

    NoteColors GetEnumForColor (int RandomColor)
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

    KeyCode GetEnumForColorKeyboard (int RandomColor)
    {
        KeyCode ColorEnum = KeyCode.None;

        switch (RandomColor)
        {
            case 0:
                ColorEnum = KeyCode.Z;
                return ColorEnum;
                break;
            case 1:
                ColorEnum = KeyCode.X;
                return ColorEnum;
                break;
            case 2:
                ColorEnum = KeyCode.C;
                return ColorEnum;
                break;
            case 3:
                ColorEnum = KeyCode.V;
                return ColorEnum;
                break;
            case 4:
                ColorEnum = KeyCode.B;
                return ColorEnum;
                break;
            case 5:
                ColorEnum = KeyCode.N;
                return ColorEnum;
                break;
            case 6:
                ColorEnum = KeyCode.M;
                return ColorEnum;
                break;
        }

        return ColorEnum;
    }

    void HideCombo ()
    {
        BtnUI.SetActive (false);
        showCombo = false;
    }

    void ChangeComboColor (int id, int color, KeyCode kCode )
    {
        Image box = BtnUI.transform.GetChild (id).GetChild (0).GetComponent<Image> ();
        BtnUI.transform.GetChild (id).GetChild (0).GetChild(0).GetComponent<TextMeshProUGUI>().text = kCode.ToString();

        box.color = colors[color];
    }

    public void RestartAll ()
    {

        railPool.Clear ();
        emptyRailPool.Clear ();
        randomColors.Clear ();
        showCombo = false;

        for (int i = 0; i < levelWrapper.transform.childCount; i++)
        {
            railPool.Add (levelWrapper.transform.GetChild (i).gameObject);
        }

        for (int i = 0; i < levelWrapper.transform.childCount; i++)
        {
            levelWrapper.transform.GetChild (i).transform.position = railPositions[i];
        }

        FirstStickCheck ();
        playerObject.transform.position = Vector3.zero;
    }
}