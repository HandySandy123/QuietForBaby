using UnityEngine;

public class TimerClock : MonoBehaviour
{
    /*
     * This script is used by the clock which appears when the player has to complete a task with timing.
     * The pointer will spin around it's center, placed in the middle of the clock.
     * It has to be stopped at a certain point, or the player is punished.
     * The pointer rotates around the Z-axis
     */

    [SerializeField] private float rotationSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Quaternion rotation = Quaternion.Euler(0f, 0f, 270f);
        transform.rotation = rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Quaternion rotation = transform.rotation;
        // rotation.z += rotationSpeed;
        // transform.rotation = rotation;
    }
}
