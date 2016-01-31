using UnityEngine;
using System.Collections;

public class RuneRender : MonoBehaviour {

    public int myId;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        int sid = RuneSlots.S.runeIds[myId];
        if (sid != -1)
            this.GetComponent<SpriteRenderer>().sprite = RuneSlots.S.runeTextures[sid];
        else
            this.GetComponent<SpriteRenderer>().sprite = null;
	}
}
