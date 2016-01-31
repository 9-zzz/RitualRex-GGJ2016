using UnityEngine;
using System.Collections;

public class RTFieldGenerator : MonoBehaviour
{
    public GameObject RTHex;
    GameObject firstChild;

    // Use this for initialization
    void Start()
    {
        firstChild = transform.GetChild(0).gameObject;
        StartCoroutine(MakeAndScaleHexagons());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(MakeAndScaleHexagons());
            CameraShake.S.shake = 1.0f;
        }
    }

    IEnumerator MakeAndScaleHexagons()
    {
        float scaleFactor = 1.0f;

        for(int i = 0; i < 25; i++)
        {
            var hexVar = Instantiate(RTHex, firstChild.transform.position, firstChild.transform.rotation) as GameObject;
            hexVar.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
            Destroy(hexVar.gameObject, 0.25f);
            scaleFactor += 1.5f;
            yield return new WaitForSeconds(0.05f);
        }
    }

}
