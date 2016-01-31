using UnityEngine;
using System.Collections;

public class StarDefiner : MonoBehaviour {

    private EffekseerEmitter test;
    public int alchemyCirclePart;

	// Use this for initialization
	void Start () {
        test = this.GetComponent("EffekseerEmitter") as EffekseerEmitter;
	}

    // Update is called once per frame
    void Update() {
        //Internal Star
        if (alchemyCirclePart == 0) { 
            int points = RuneSlots.S.numPoints;
            if (points >= 3 && test.effectName != points + "pointstar")
            {
                test.Stop();
                test.Play(points + "pointstar");
            }
        }

        //Outer Ring
        if (alchemyCirclePart >= 1 && alchemyCirclePart <= 3 && RuneSlots.S.inscribedRings.Length >= alchemyCirclePart)
        {
            bool inscribed = RuneSlots.S.inscribedRings[alchemyCirclePart-1];
            string newName = "ring" + (alchemyCirclePart - 1) + "inscribed" + RuneSlots.S.colorID;
            if (!inscribed)
                newName = "ring" + (alchemyCirclePart - 1) + "plain" + RuneSlots.S.colorID;
            if (test.effectName != newName)
            {
                test.Stop();
                test.Play(newName);
            }
            if (RuneSlots.S.circleActive)
            {
                bool clockwise = RuneSlots.S.ringClockwise[alchemyCirclePart - 1];
                if (clockwise)
                    this.transform.Rotate(0, 0, 0.25f);
                else
                    this.transform.Rotate(0, 0, -0.25f); 
            }
        }

        //Inner Ring

        //Tertiary Ring
    }
}
