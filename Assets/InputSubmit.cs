using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputSubmit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        string text = this.GetComponent<InputField>().text;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            RuneSlots.S.activateRitual(text);
            this.GetComponent<InputField>().text = "";
        }
    }
}
