using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.Serialization;

public class PlayerBehavior : MonoBehaviour
{
    SpriteRenderer spr;
    Rigidbody rb;
    public AudioSource footstepsSound;
    public GameObject floorboard;
    private CreakyFloorboards CF;



    private bool isWalking;
    private bool onFirstFloor;
    private Vector2 Movement;
    private float interact;
    [SerializeField] private float groundDist;
    [FormerlySerializedAs("MoveSpeed")] [SerializeField] private float moveSpeed = 10f;
    
    private ActionInput _playerInput;
    private InputAction onMovement;
    [SerializeField] private Sprite[] sprites;
    private GameObject playerSprite;
    [SerializeField] private GameObject bottomOfStairs;
    [SerializeField] private GameObject topOfStairs;
    private HID.Button interactButton;
    
    //public Vector3 camPos;

    //[SerializeField] private float distToCam = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        spr = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
        _playerInput = new ActionInput();
        groundDist = spr.bounds.extents.x;
        CF = floorboard.GetComponent<CreakyFloorboards>();
    }

    void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Movement.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
        rb.linearVelocity = Movement * moveSpeed;
        //Movement = context.ReadValue<Vector2>();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        _playerInput.Enable();
        interact = _playerInput.Movement.OnSelect.ReadValue<float>();
        if (_playerInput.Movement.OnSelect.IsPressed())
        {
            Debug.Log(_playerInput.Movement.OnSelect.IsPressed());
        }
    }
    void Awake()
    {
        _playerInput = new ActionInput();
        Ray r = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        //camPos = r.GetPoint(distToCam);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Movement);
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("stairTrigger"))

        {
            if (other.gameObject == bottomOfStairs)
            {
                Debug.Log("bottomOfStairs");
                if (_playerInput.Movement.OnSelect.IsPressed())
                {
                    Debug.Log("Transporting!");
                    transform.position = topOfStairs.transform.position;
                    onFirstFloor = true;
                };
            } else if (other.gameObject == topOfStairs)
            {
                Debug.Log("topOfStairs");
                if(_playerInput.Movement.OnSelect.IsPressed())
                {
                    Debug.Log("Transporting!");
                    transform.position = bottomOfStairs.transform.position;
                    onFirstFloor = false;
                };
            }
        }
        Debug.Log("CreakyFloorboard");
        if (other.gameObject == floorboard)
        {
            CF.source.Play();
           
        }
    }

    void FixedUpdate()
    {
        
        // Apply movement based on input using rb.velocity
        Vector3 movementDirection = new Vector3(Movement.x, 0, Movement.y) * moveSpeed;
        rb.linearVelocity = movementDirection;

        if (Movement.x != 0 || Movement.y != 0)
        {
            controlAnimation(Movement);
            footstepsSound.enabled = true;
        }
        else
        {
            spr.sprite = sprites[3]; // Idle sprite
            footstepsSound.enabled = false;
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