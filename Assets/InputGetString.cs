using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class InputGetString : MonoBehaviour {

    public string activeString;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
       /* detectPressedKeyOrButton();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            RuneSlots.S.activateRitual(activeString);
            activeString = "";
        }*/
	}

    public void detectPressedKeyOrButton()
    {
        /*foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode) && kcode.ToString().Length == 1)
            {
                Debug.Log("KeyCode down: " + kcode);
                activeString = activeString + kcode.ToString();
            }
        }*/
    }
}
