using UnityEngine;
using System.Collections;

public class RITFieldEffect : MonoBehaviour
{

    Renderer mren;

    void Awake()
    {
        mren = GetComponent<Renderer>();
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(FlashScale(0.27f, 30));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CallFlashScaleCo()
    {
        //StartCoroutine(FlashScale(1.0f, 8));
    }

    IEnumerator FlashScale(float waitTime, int flashCount)
    {
        for (int i = 0; i < flashCount; i++)
        {
            yield return new WaitForSeconds(waitTime);
            mren.enabled = false;
            yield return new WaitForSeconds(waitTime);
            mren.enabled = true;

            if (waitTime >= 0) waitTime -= 0.03f;

            print("do");
        }
    }

}
