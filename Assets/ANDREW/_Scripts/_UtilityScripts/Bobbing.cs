using UnityEngine;
using System;
using System.Collections;

public class Bobbing : MonoBehaviour {

    private float startY;
    public float magnitude;
    public double rate;
    public double offset;

	// Use this for initialization
	void Start () {
        startY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, 
            startY + (float)Math.Sin(Time.fixedTime * rate + offset)*magnitude,
            transform.position.z); //super shit game jam code!
	}
}
