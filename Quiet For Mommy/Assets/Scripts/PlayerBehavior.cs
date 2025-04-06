using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerBehavior : MonoBehaviour
{
    SpriteRenderer spr;
    Rigidbody rb;

    private Vector2 Movement;
    [SerializeField] private float groundDist;
    [FormerlySerializedAs("MoveSpeed")] [SerializeField] private float moveSpeed = 10f;

    private PlayerInput _playerInput;
    [SerializeField] private Sprite[] sprites;
    
    //public Vector3 camPos;

    //[SerializeField] private float distToCam = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        groundDist = spr.bounds.extents.x;
    }

    void Awake()
    {
        Ray r = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        //camPos = r.GetPoint(distToCam);
    }

    // Update is called once per frame
    void Update()
    {
        Movement = _playerInput.actions["OnMovement"].ReadValue<Vector2>();

        // Raycast to adjust Y position based on ground distance
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;
        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }
        }
    }

    void FixedUpdate()
    {
        // Apply movement based on input using rb.velocity
        Vector3 movementDirection = new Vector3(Movement.x, 0, Movement.y) * moveSpeed;
        rb.linearVelocity = movementDirection;
        //rb.AddForce(movementDirection * moveSpeed, ForceMode.Impulse);
        Debug.Log(rb.GetAccumulatedForce());

        // Debugging movement
        //Debug.Log(rb.linearVelocity);

        if (Movement.x != 0 || Movement.y != 0)
        {
            controlAnimation(Movement);
        }
        else
        {
            spr.sprite = sprites[3]; // Idle sprite
        }
    }

    void controlAnimation(Vector2 input)
    {
        float y = input.y;
        float x = input.x;
        
        // Conditions for each direction
        bool upWalk = (x == 0 && Mathf.Approximately(y, 1)) || // Up
                       Mathf.Approximately(x, 0.71f) && Mathf.Approximately(y, 0.71f) || // Up-right
                       Mathf.Approximately(x, 1) && y == 0; // Right
        bool rightWalk = !Mathf.Approximately(x, 1) && !Mathf.Approximately(x, -1) && Mathf.Approximately(y, 0.71f);
        bool RDwalk = (x == 0 && Mathf.Approximately(y, -1)) ||
                       y == 0 && Mathf.Approximately(x, -1); // Down-left

        bool downWalk = !upWalk || !rightWalk || !RDwalk;

        // Assign correct sprite based on movement direction
        if (upWalk)
        {
            spr.sprite = sprites[0]; // Up
        }
        else if (rightWalk)
        {
            spr.sprite = sprites[1]; // Right
            spr.flipX = (Mathf.Approximately(x, 0.71f) && Mathf.Approximately(y, 0.71f)) ? true : false;
        }
        else if (RDwalk)
        {
            spr.sprite = sprites[2]; // Down-left
            spr.flipX = (Mathf.Approximately(x, -1) && y == 0) ? true : false;
        }
    }
}