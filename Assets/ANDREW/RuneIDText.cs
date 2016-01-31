using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RuneIDText : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Text>().text = ""+(RuneSlots.S.activeRune+1);
    }
}
