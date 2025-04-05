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
    
    public Vector3 camPos;

    [SerializeField] private float distToCam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>(); 
        _playerInput = GetComponent<PlayerInput>();
        groundDist = spr.bounds.extents.x;
    }
    
    // Update is called once per frame
    void Update()
    {
        Movement = _playerInput.actions["OnMovement"].ReadValue<Vector2>();
        
        
        //Debug.Log(Movement);
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
        
        Ray r = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        camPos = r.GetPoint(distToCam);
        
    }

    void FixedUpdate()
    {
        //rb.linearVelocity = new Vector3(Movement.x * moveSpeed, 0, Movement.y * moveSpeed);
        rb.linearVelocity = (new Vector3(Movement.x * moveSpeed, 0, 0) + new Vector3(0, 0, Movement.y * moveSpeed)) / 2;
    }
}
