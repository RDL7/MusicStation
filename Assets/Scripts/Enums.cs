using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class Enums : MonoBehaviour
{
    //red, oran, dzelt, zals, sky blue, zils, violets,

    public enum NoteColors
    {
        Red = 48,
        Orange = 50,
        Yellow = 52,
        Green = 53,
        SkyBlue = 55,
        Blue = 57,
        Purple = 59
    }
}

public class GenColorStructure
{
    public int Color {get; set; }
    public NoteColors ColorEnum {get; set; }
    public int BtnNumber { get; set; }
}

public class GenColorStructureKeyboard
{
    public int Color {get; set; }
    public KeyCode ColorEnum {get; set; }
    public int BtnNumber { get; set; }
}
