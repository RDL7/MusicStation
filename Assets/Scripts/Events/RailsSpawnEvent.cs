using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RailsSpawnEvent : MonoBehaviour
{
    private void OnTriggerEnter2D (Collider2D other)
    {
        EventManager.em.RailsLeave ();
    }
}