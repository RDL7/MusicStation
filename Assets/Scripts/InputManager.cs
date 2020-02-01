using MidiJack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class InputManager : MonoBehaviour
{
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
                break;
            case (int)NoteColors.Orange:
                print("Orange");
                break;
            case (int)NoteColors.Yellow:
                print("Yellow");
                break;
            case (int)NoteColors.Green:
                print("Green");
                break;
            case (int)NoteColors.SkyBlue:
                print("SkyBlue");
                break;
            case (int)NoteColors.Blue:
                print("Blue");
                break;
            case (int)NoteColors.purple:
                print("purple");
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
