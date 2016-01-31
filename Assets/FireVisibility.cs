using UnityEngine;
using System.Collections;

public class FireVisibility : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        ParticleSystem ps = this.GetComponent<ParticleSystem>();
        ParticleSystem.EmissionModule pse = ps.emission;
        if (!RuneSlots.S.circleActive)
            pse.enabled = false;
        else
            pse.enabled = true;
    }
}
