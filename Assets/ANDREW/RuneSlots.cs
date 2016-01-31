using UnityEngine;
using System.Collections;

public class RuneSlots : MonoBehaviour {

    public static RuneSlots S;
    public int[] runeIds = new int[9];
    public int activeRune = 0;
    public Sprite[] runeTextures = new Sprite[8];

    void Awake()
    {
        S = this;
    }

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 9; i++)
            runeIds[i] = -1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setActiveRune(int rune)
    {
        runeIds[activeRune] = rune;
        nextActiveRune();
    }

    public void prevActiveRune()
    {
        activeRune = (activeRune - 1) % runeIds.Length;
    }

    public void nextActiveRune()
    {
        activeRune = (activeRune + 1) % runeIds.Length;
    }
}
