using UnityEngine;

public class Hover : MonoBehaviour
{
    [SerializeField] private float amplitude = 0.1f;
    [SerializeField] private float speed = 0.5f;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        var newYPosition = initialPosition.y + amplitude * Mathf.Cos(Time.time * speed);
        transform.position = new Vector3(initialPosition.x, newYPosition, initialPosition.z);
    }
}
