using MidiJack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class InputManager : MonoBehaviour
{
    /*public List<AudioClip> audioQueue = new List<AudioClip>();

    
    int index = 0;*/

    public static InputManager im;
    public AudioSource audioSource;

    public AudioClip Do;
    public AudioClip Re;
    public AudioClip Mi;
    public AudioClip Fa;
    public AudioClip Sol;
    public AudioClip La;
    public AudioClip Si;

    public GameObject GameUI;
    List<int> PressedKeys = new List<int>();


    private void Awake() {
        if (im == null) 
        {
            im = this;
        }
    }

    //red, oran, dzelt, zals, sky blue, zils, violets,
    void NoteOn(MidiChannel channel, int note, float velocity)
    {
        //Debug.Log("NoteOn: " + channel + "," + note + "," + velocity);
        //if (note == (int)NoteColors.Red)
        //{
        //    print("Red");
        //}

        switch (note)
        {
            case (int)NoteColors.Red:
                print("Red");
                audioSource.PlayOneShot(Do);
                PressedKeys.Add((int)NoteColors.Red);
                break;
            case (int)NoteColors.Orange:
                print("Orange");
                audioSource.PlayOneShot(Re);
                PressedKeys.Add((int)NoteColors.Orange);
                break;
            case (int)NoteColors.Yellow:
                print("Yellow");
                audioSource.PlayOneShot(Mi);
                PressedKeys.Add((int)NoteColors.Yellow);
                break;
            case (int)NoteColors.Green:
                print("Green");
                audioSource.PlayOneShot(Fa);
                PressedKeys.Add((int)NoteColors.Green);
                break;
            case (int)NoteColors.SkyBlue:
                print("SkyBlue");
                audioSource.PlayOneShot(Sol);
                PressedKeys.Add((int)NoteColors.SkyBlue);
                break;
            case (int)NoteColors.Blue:
                print("Blue");
                audioSource.PlayOneShot(La);
                PressedKeys.Add((int)NoteColors.Blue);
                break;
            case (int)NoteColors.purple:
                print("purple");
                audioSource.PlayOneShot(Si);
                PressedKeys.Add((int)NoteColors.purple);
                break;
            default:
                print("No Enum");
                PressedKeys.Add(-1);//for noot views value, for not usefull
                break;
        }
    }

    void NoteOff(MidiChannel channel, int note)
    {
        //Debug.Log("NoteOff: " + channel + "," + note);
    }

    void Knob(MidiChannel channel, int knobNumber, float knobValue)
    {
        //Debug.Log("Knob: " + knobNumber + "," + knobValue);
    }

    void OnEnable()
    {
        MidiMaster.noteOnDelegate += NoteOn;
        MidiMaster.noteOffDelegate += NoteOff;
        MidiMaster.knobDelegate += Knob;
    }

    void OnDisable()
    {
        MidiMaster.noteOnDelegate -= NoteOn;
        MidiMaster.noteOffDelegate -= NoteOff;
        MidiMaster.knobDelegate -= Knob;
    }
}
