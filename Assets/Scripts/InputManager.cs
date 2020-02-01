using MidiJack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class InputManager : MonoBehaviour
{
    /*public List<AudioClip> audioQueue = new List<AudioClip>();
    int index = 0;*/
    public AudioSource audioSource;

    public AudioClip Do;
    public AudioClip Re;
    public AudioClip Mi;
    public AudioClip Fa;
    public AudioClip Sol;
    public AudioClip La;
    public AudioClip Si;


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
                break;
            case (int)NoteColors.Orange:
                print("Orange");
                audioSource.PlayOneShot(Re);
                break;
            case (int)NoteColors.Yellow:
                print("Yellow");
                audioSource.PlayOneShot(Mi);
                break;
            case (int)NoteColors.Green:
                print("Green");
                audioSource.PlayOneShot(Fa);
                break;
            case (int)NoteColors.SkyBlue:
                print("SkyBlue");
                audioSource.PlayOneShot(Sol);
                break;
            case (int)NoteColors.Blue:
                print("Blue");
                audioSource.PlayOneShot(La);
                break;
            case (int)NoteColors.purple:
                print("purple");
                audioSource.PlayOneShot(Si);
                break;
            default:
                print("No Enum");
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
