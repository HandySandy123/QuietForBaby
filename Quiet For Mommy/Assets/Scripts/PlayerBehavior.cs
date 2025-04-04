using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    SpriteRenderer spr; 
    Rigidbody rb;

    private Vector2 Movement; 
    [SerializeField] private float MoveSpeed;

    [SerializeField] private InputActionReference move;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>(); 
    }
    
    // Update is called once per frame
    void Update()
    {
        Movement = move.action.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(Movement.x * MoveSpeed, Movement.y * MoveSpeed);
    }
}
