using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour {

    public static ScreenFader S;
    public Color col1;
    Image imageToWork;

    void Awake()
    {
        S = this;
    }

	// Use this for initialization
	void Start () {

        imageToWork.color = col1;
        imageToWork.CrossFadeAlpha(0.0f, 0.0f, true);
	
	}

    void Flash()
    {
        imageToWork.CrossFadeAlpha(0.9f, 0.10f, true);
        imageToWork.CrossFadeAlpha(0.0f, 0.10f, true);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
