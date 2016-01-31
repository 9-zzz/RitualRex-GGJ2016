using UnityEngine;
using System.Collections;

public class MaterialFade : MonoBehaviour
{
    public Material material1;
    public Material material2;
    public float duration = 2.0F;
    Renderer rend;
    int ctr = 1;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = material1;
    }

    void Update()
    {
        if(ctr > 0)
        {
            float lerp = Mathf.PingPong(Time.time, duration) / duration;
            rend.material.Lerp(material1, material2, lerp);

            if (lerp <= 0.05f)
                ctr--;
        }
    }

}
