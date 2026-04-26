using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] public GameObject[] doors; // Array of doors that the lever will control
    [SerializeField] public GameObject door; // Single door that the lever will control (if not using the array)

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            // Deactivate the single door if assigned 
            if(door != null)
                door.SetActive(false); // Deactivate the door when the player enters the trigger area
            if(doors != null && doors.Length > 0)
                ToggleDoors(); // Toggle the doors in the array if assigned
        }
    }
    public void ToggleDoors()
    {
        foreach(GameObject door in doors)
        {
            if(door != null)
                door.SetActive(false); // Deactivate each door in the array when the lever is toggled
        }
    }
}
