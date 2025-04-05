using UnityEngine;
using UnityEngine.InputSystem;

public class TimerClock : MonoBehaviour
{
    /*
     * This script is used by the clock which appears when the player has to complete a task with timing.
     * The pointer will spin around it's center, placed in the middle of the clock.
     * It has to be stopped at a certain point, or the player is punished.
     * The pointer rotates around the Z-axis
     */

    private PlayerInput playerInput;
    private InputAction selectAsset;
    
    [SerializeField] private float rotationSpeed = 20f;
    [SerializeField] public float correctSpace = 60f; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 181f);
        playerInput = GetComponent<PlayerInput>();
        selectAsset = playerInput.actions["Select"];
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = transform.rotation;
        rotation.z += rotationSpeed;
        transform.Rotate(new Vector3(0, 0, 1), rotationSpeed * Time.deltaTime);
        
    }

    public void OnSelect(InputValue value)
    {
        Debug.Log(transform.rotation.eulerAngles.z);
        if (transform.eulerAngles.z <= correctSpace && transform.eulerAngles.z >= 360 - correctSpace)
        {
            Debug.Log("Succeeded");
        } else 
        {
            Debug.Log("Failed");
        }
    }
}
