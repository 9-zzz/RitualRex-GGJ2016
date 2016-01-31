using UnityEngine;
using System.Collections;

public class FocalSymbolRender : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (RuneSlots.S.circleActive)
            this.GetComponent<SpriteRenderer>().sprite = RuneSlots.S.symbolTextures[RuneSlots.S.focalSymbol];
        else
            this.GetComponent<SpriteRenderer>().sprite = null;
    }
}
