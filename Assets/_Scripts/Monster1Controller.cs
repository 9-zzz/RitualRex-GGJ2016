using UnityEngine;
using System.Collections;

public class Monster1Controller : MonoBehaviour
{

    public float moveSpeed;
    public GameObject targetGO;
    Vector3 targetPos;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {
        targetPos = targetGO.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);

        if (transform.position == targetPos)
            anim.SetBool("toMelee", true);
    }

}
