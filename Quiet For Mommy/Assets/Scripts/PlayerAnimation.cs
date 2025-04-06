using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;

    private Vector2 Movement;

    private Vector3 currentPos;
    private Rigidbody rb;

    private Animation walkAnimation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponentInParent<Rigidbody>();
        Movement = rb.linearVelocity;
        walkAnimation = anim.GetComponent<Animation>();
        Debug.Log(walkAnimation.name);
    }

    // Update is called once per frame
    void Update()
    {
        currentPos = transform.position;
        if (Movement.magnitude < 0.1f)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }
    }
}
