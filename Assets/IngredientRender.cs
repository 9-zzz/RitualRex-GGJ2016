using UnityEngine;
using System.Collections;

public class IngredientRender : MonoBehaviour {

    public int myId;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (myId < RuneSlots.S.numIngredients)
        {
            int sid = RuneSlots.S.ingredientIds[myId];
            this.GetComponent<SpriteRenderer>().sprite = RuneSlots.S.ingredientTextures[sid]; 
        }
        else
            this.GetComponent<SpriteRenderer>().sprite = null;
    }
}
