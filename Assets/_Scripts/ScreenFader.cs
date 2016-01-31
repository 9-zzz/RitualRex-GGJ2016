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
        imageToWork = GetComponent<Image>();
    }

	// Use this for initialization
	void Start () {

        imageToWork.color = col1;
        imageToWork.CrossFadeAlpha(0.0f, 0.0f, true);
	
	}

    public void Flash()
    {
        StartCoroutine(FlashCo());
    }

    IEnumerator FlashCo()
    {
        imageToWork.CrossFadeAlpha(0.9f, 0.25f, true);
        yield return new WaitForSeconds(0.25f);
        imageToWork.CrossFadeAlpha(0.0f, 0.25f, true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
