using UnityEngine;
using System.Collections;

public class Monster1Controller : MonoBehaviour
{
    public static Monster1Controller S;

    public bool moveForward = true;
    public bool moveBack = false;
    public float moveSpeed;
    public GameObject targetGO;
    Vector3 targetPos;
    Animator anim;
    public Vector3 startingPos;

    void Awake()
    {
        S = this;
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {
        startingPos = transform.position;
        targetPos = targetGO.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveForward)
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);

        if(moveBack)
        {
            moveForward = false;
            transform.position = Vector3.MoveTowards(transform.position, startingPos, Time.deltaTime * moveSpeed);
        }

        if (transform.position == targetPos)
            anim.SetBool("toMelee", true);
    }

}
