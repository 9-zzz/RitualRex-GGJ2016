using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuVisibility : MonoBehaviour {

    public int menuID;
    private float ystart;

	// Use this for initialization
	void Start () {
        ystart = this.transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        int activeID = RuneSlots.S.openMenu;
        Button myButton = this.GetComponent<Button>();
        Text myText = this.GetComponent<Text>();
        InputField myInput = this.GetComponent<InputField>();

        if (menuID != activeID && myInput == null)
        {
            //hide            
            if (myButton != null) { myButton.interactable = false; myButton.transform.localScale = new Vector3(0, 0, 0); }
            if (myText != null) { myText.transform.localScale = new Vector3(0, 0, 0); }
            //if (myInput != null) { //this.gameObject.SetActive(false); } //myInput.enabled = false; }//myInput.interactable = false; myInput.transform.position = new Vector3(myInput.transform.position.x, 9999, myInput.transform.position.z); }
        } else {
            //show
            if (myButton != null) { myButton.interactable = true; myButton.transform.localScale = new Vector3(1, 1, 1); }
            if (myText != null) { myText.transform.localScale = new Vector3(1, 1, 1); }
            //if (myInput != null) { this.gameObject.SetActive(true); } //myInput.interactable = true; myInput.transform.position = new Vector3(myInput.transform.position.x, ystart, myInput.transform.position.z); }
        }
    }
}
